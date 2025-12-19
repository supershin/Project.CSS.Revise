using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.CSS.Revise.Web.Commond;
using Project.CSS.Revise.Web.Data;
using Project.CSS.Revise.Web.Models;
using Project.CSS.Revise.Web.Models.Pages.QueueBank;
using SkiaSharp;
using System.Transactions;

namespace Project.CSS.Revise.Web.Respositories
{
    public interface IQueueBankRepo
    {
        public List<ListUnitForRegisterBankModel> GetListUnitForRegisterBank(string ProjectID);
        public string GetMessageAppointmentInspect(RegisterLog criteria);
        public RegisterLog SaveRegisterLog(RegisterLog model);
        public RegisterLog GetRegisterLogInfo(RegisterLog criteria, string UserID, string Password);
        public void SaveCustomerSubmit_FINPlus(LoanModel model);
        public string RemoveRegisterLog(int id);
    }
    public class QueueBankRepo : IQueueBankRepo
    {
        private readonly CSSContext _context;
        private readonly IUnitRepo _unitRepo;

        public QueueBankRepo(CSSContext context, IUnitRepo unitRepo)
        {
            _context = context;
            _unitRepo = unitRepo;
        }

        public List<ListUnitForRegisterBankModel> GetListUnitForRegisterBank(string ProjectID)
        {
            List<ListUnitForRegisterBankModel> result = new List<ListUnitForRegisterBankModel>();
            string connectionString = _context.Database.GetDbConnection().ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"
                                SELECT 
                                      u.ID
                                    , u.UnitCode
                                    , u.AddrNo
                                FROM tm_Unit u
                                WHERE u.ID NOT IN (
                                    SELECT UnitID
                                    FROM TR_RegisterLog
                                    WHERE FlagActive = 1  
                                      AND QCTypeID = 10
                                      AND QueueTypeID = 48
                                )
                                AND u.ProjectID = @L_ProjectID
                                AND u.FlagActive = 1
                                ORDER BY u.UnitCode;
                            ";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@L_ProjectID", ProjectID ?? string.Empty));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int idx = 1;

                        while (reader.Read())
                        {
                            result.Add(new ListUnitForRegisterBankModel
                            {
                                Index = idx++,
                                ID = Commond.FormatExtension.NulltoGuid(reader["ID"]),
                                UnitCode = Commond.FormatExtension.NullToString(reader["UnitCode"]),
                                AddrNo = Commond.FormatExtension.NullToString(reader["AddrNo"]),
                            });
                        }
                    }
                }
            }

            return result;
        }

        public string GetMessageAppointmentInspect(RegisterLog criteria)
        {
            var dt = DateTime.Now;
            var dtNow = new DateTime(dt.Year, dt.Month, dt.Day);
            var query = from a in _context.TR_Appointments.Where(e => e.FlagActive == true)
                        join u in _context.tm_Units.Where(e => e.FlagActive == true)
                            on a.UnitID equals u.ID
                        where u.UnitCode == criteria.UnitCode && u.ProjectID == criteria.ProjectID
                        //&& a.AppointmentTypeID == SystemConstants.Ext.APPOINT_TYPE_INSPECT
                        && a.AppointDate >= dtNow
                        orderby a.AppointDate descending
                        select new Appointment
                        {
                            AppointDate = a.AppointDate,
                            StartTime = a.StartTime
                        };
            if (query.Any())
            {
                var item = query.FirstOrDefault();
                return string.Format(Constants.WARNING.APPOINTMENT_HAS_EXISTS, item.AppointDate.AsDate().ToString("dd-MM-yyyy"), item.StartTime);
            }

            return Constants.WARNING.APPOINTMENT_DOES_NOT_EXISTS;

        }

        public RegisterLog SaveRegisterLog(RegisterLog model)
        {
            try
            {
                TransactionOptions option = new TransactionOptions();
                option.Timeout = new TimeSpan(1, 0, 0);
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, option))
                {
                    if (model.ID == 0)
                    {
                        InsertRegister(model);
                    }
                    else
                    {
                        UpdateRegisterLog(model);
                        clearRegisterCounter(model);
                        InsertRegisterBank(model);

                        //Update Bank Counter
                        if (model.FlagFinish.AsBool())
                        {
                            SaveUpdateRegisterBankCounterFinish(model);
                        }
                    }

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return model;
        }

        // เรียกตอนบันทึก Bank ของ Register หนึ่งตัว
        private void InsertRegisterBank(RegisterLog model)
        {
            // 1) ลบของเก่าทิ้งก่อน
            DeleteRegisterBank(model);

            // 2) แทรกของใหม่
            SetInsertRegisterBankData(model);
        }

        // ลบ Bank ทั้งหมดของ RegisterLog.ID นั้นๆ
        private void DeleteRegisterBank(RegisterLog model)
        {
            var items = _context.TR_RegisterBanks
                                .Where(e => e.RegisterLogID == model.ID)
                                .ToList();

            if (items.Count == 0)
                return;

            _context.TR_RegisterBanks.RemoveRange(items);

            _context.SaveChanges();
        }

        // สร้าง TR_RegisterBank ใหม่จาก BankIDs ใน model
        private void SetInsertRegisterBankData(RegisterLog model)
        {
            if (string.IsNullOrWhiteSpace(model.BankIDs))
                return;

            // split "1,2,3" → ["1","2","3"]
            var bankIdStrings = model.BankIDs
                                     .Split(',', StringSplitOptions.RemoveEmptyEntries)
                                     .ToList();

            if (!bankIdStrings.Any())
                return;

            // หา running ID เริ่มต้น (ตามสไตล์เดิมของระบบ)
            int nextId = _context.TR_RegisterBanks.Any()
                ? _context.TR_RegisterBanks.Max(e => e.ID) + 1
                : 1;

            foreach (var bankIdStr in bankIdStrings)
            {
                var item = new TR_RegisterBank
                {
                    ID = nextId++,
                    RegisterLogID = model.ID,
                    BankID = bankIdStr.ToInt()
                };

                _context.TR_RegisterBanks.Add(item);
            }

            // SaveChanges ครั้งเดียวหลังจาก Add ครบทุกตัว (ไม่วน Save ทีละรอบแล้ว)
            _context.SaveChanges();
        }
        private void SaveUpdateRegisterBankCounterFinish(RegisterLog model)
        {
            var register = new RegisterModel
            {
                ProjectID = model.ProjectID,
                QueueTypeID = model.QueueTypeID.AsInt(),
                BankCounterStatus = Constants.Register.BankCounterStatus.CHECK_OUT,
                BankCounterList = new List<RegisterBankCounter_Result>{ new RegisterBankCounter_Result {
                                                                            RegisterLogID = model.ID
                                                                       }
                }
            };

            SaveUpdateRegisterBankCounterFinishData(register);
        }
        private void SaveUpdateRegisterBankCounterFinishData(RegisterModel model)
        {
            // กัน null ไว้ก่อน เผื่ออนาคตมีเคสส่ง model.BankCounterList = null
            var bankCounterList = model.BankCounterList ?? new List<RegisterBankCounter_Result>();

            foreach (var bankCounter in bankCounterList)
            {
                // ดึงรายการที่ยังไม่ CHECK_OUT
                var registerLogs = _context.TR_Register_BankCounters
                    .Where(e => e.RegisterLogID == bankCounter.RegisterLogID
                             && e.BankCounterStatus != Constants.Register.BankCounterStatus.CHECK_OUT)
                    .ToList();

                foreach (var register in registerLogs)
                {
                    register.BankCounterStatus = model.BankCounterStatus;
                    register.CheckOutDate = DateTime.Now;
                    register.UpdateDate = DateTime.Now;
                    // ไม่ต้อง set EntityState.Modified ใน EF Core ถ้า track อยู่แล้ว
                }
            }

            // SaveChanges ครั้งเดียวพอ
            _context.SaveChanges();
        }
        private void clearRegisterCounter(RegisterLog model)
        {
            if (model.QueueTypeID == Constants.Ext.QUEUE_TYPE_INSPECT)
                if (model.FlagRegister.AsBool() && model.FlagWait.AsBool()
                    && model.FlagInprocess.AsBool()
                    && !model.FlagFastFix.AsBool()
                    && !model.FlagFinish.AsBool())
                    SaveRegisterCounterData(new RegisterCounter
                    {
                        RegisterLogID = model.ID,
                        FlagCancel = true,
                        Counter = model.Counter
                    });


        }
        private void SaveRegisterCounterData(RegisterCounter model)
        {
            // ดึงข้อมูล RegisterLog จาก DbContext ของเรา
            var item = _context.TR_RegisterLogs.FirstOrDefault(e => e.ID == model.RegisterLogID);

            if (item == null)
            {
                throw new Exception("ไม่พบข้อมูล RegisterLog ที่ต้องการอัปเดต");
            }

            // อัปเดตค่า Counter
            item.Counter = model.FlagCancel ? null : model.Counter;

            // อัปเดตวันที่ + คนแก้ไข (ควรมีตามมาตรฐาน)
            item.UpdateDate = DateTime.Now;
            item.UpdateBy = model.SaveByID;

            // บันทึก
            _context.SaveChanges();
        }
        private void InsertRegister(RegisterLog model)
        {
            // map DTO -> Entity
            var entity = SetInsertRegisterLogData(model);

            _context.TR_RegisterLogs.Add(entity);
            _context.SaveChanges();           // หลังบันทึก EF จะเติม ID (IDENTITY) ให้ entity

            // ถ้าพ่อใหญ่ต้องการรู้ ID ใหม่
            model.ID = entity.ID;
        }
        private TR_RegisterLog SetInsertRegisterLogData(RegisterLog model)
        {
            // 🔹 ไม่ต้อง Max + 1 แล้ว
            // int newId = _context.TR_RegisterLogs.Any() ? _context.TR_RegisterLogs.Max(e => e.ID) + 1 : 1;

            // ====== หา Unit จาก Project + UnitCode ======
            var unit = GetUnit(model);
            model.UnitID = unit.ID;

            // ====== Validate ก่อน insert ======
            ValidateExistsUnitRegister(model);
            ValidateExistsUnitRegisterContract(model);

            var now = DateTime.Now;

            // ====== Map ไปเป็น Entity ======
            var item = new TR_RegisterLog
            {
                // ❌ ห้ามเซ็ต ID ถ้าเป็น IDENTITY
                // ID = newId,

                ProjectID = model.ProjectID,
                UnitID = unit.ID,
                QueueTypeID = model.QueueTypeID,
                QCTypeID = Constants.Ext.QC_TYPE_QC6,
                CareerTypeID = model.CareerTypeID,
                TransferTypeID = model.TransferTypeID,
                FlagActive = true,
                RegisterDate = now,
                CreateDate = now,
                CreateBy = model.SaveByID,
                UpdateDate = now,
                UpdateBy = model.SaveByID
            };

            return item;
        }
        private void UpdateRegisterLog(RegisterLog model)
        {
            // ยังใช้ validate ตัวเดิมได้ตามปกติ
            ValidateSaveRegisterLog(model);

            // หา entity จาก DbContext ที่ inject เข้ามา
            var item = _context.TR_RegisterLogs.FirstOrDefault(e => e.ID == model.ID);

            if (item == null)
            {
                // แล้วแต่พ่อใหญ่จะใช้ข้อความไหน
                throw new Exception("ไม่พบข้อมูลคิวที่ต้องการแก้ไข (TR_RegisterLog)");
                // หรือใช้ Constants.Message.ERROR.xxx ก็ได้
            }

            // อัปเดต field ต่าง ๆ ใส่ลงใน entity ที่ track อยู่แล้ว
            SetUpdateRegisterLogData(item, model);

            // EF Core จะ track การเปลี่ยนแปลงให้เอง ไม่ต้อง set EntityState
            _context.SaveChanges();

            // sync ค่า Counter กลับไปที่ model เผื่อไปใช้ต่อ
            model.Counter = item.Counter;
        }
        private void SetUpdateRegisterLogData(TR_RegisterLog item, RegisterLog model)
        {
            item.ResponsibleID = (model.ResponsibleID.AsInt() > Constants.Ext.NO_INPUT)
                                     ? model.ResponsibleID
                                     : null;

            item.WaitDate = model.FlagWait.AsBool()
                                ? (DateTime?)DateTime.Now
                                : null;

            item.InprocessDate = model.FlagInprocess.AsBool()
                                    ? (DateTime?)DateTime.Now
                                    : null;

            item.FastFixDate = model.FlagFastFix.AsBool()
                                    ? (DateTime?)DateTime.Now
                                    : null;

            item.FastFixFinishDate = model.FlagFastFixFinish.AsBool()
                                           ? (DateTime?)DateTime.Now
                                           : null;

            item.FinishDate = model.FlagFinish.AsBool()
                                ? (DateTime?)DateTime.Now
                                : null;

            item.CareerTypeID = model.CareerTypeID;
            item.TransferTypeID = model.TransferTypeID;

            // ✅ ReasonID logic (IMPORTANT)
            if (model.ReasonID.AsInt() <= 0)
            {
                item.ReasonID = null;
                item.ReasonRemarkID = null;
            }
            else
            {
                item.ReasonID = model.ReasonID;

                if (model.ReasonID == 50) // ยื่น
                {
                    item.ReasonRemarkID = null;
                }
                else if (model.ReasonID == 51) // ไม่ยื่น
                {
                    item.ReasonRemarkID = model.ReasonRemarkID;
                }
                else
                {
                    item.ReasonRemarkID = null;
                }
            }

            item.FixedDuration = model.FlagFastFix.AsBool()
                                    ? model.FixedDuration
                                    : null;

            item.UpdateDate = DateTime.Now;
            item.UpdateBy = model.SaveByID;
        }


        private void ValidateSaveRegisterLog(RegisterLog model)
        {

            if (model.QueueTypeID == Constants.Ext.QUEUE_TYPE_INSPECT)
            {
                if (model.FlagWait.AsBool() && model.ResponsibleID.AsInt() == Constants.Ext.NO_INPUT)
                    throw new Exception(Constants.Message.ERROR.PLEASE_SELECT_RESPONSIBLE);
                if (model.FlagInprocess.AsBool())
                {
                    if (model.ResponsibleID.AsInt() == Constants.Ext.NO_INPUT)
                        throw new Exception(Constants.Message.ERROR.PLEASE_SELECT_RESPONSIBLE);
                    if (!model.FlagWait.AsBool())
                        throw new Exception(Constants.Message.ERROR.PLEASE_CHECK_WAIT);
                }
                if (model.FlagFastFix.AsBool())
                    if (model.FixedDuration.AsInt() == 0)
                        throw new Exception(Constants.Message.ERROR.PLEASE_SELECT_FIX_DURATION);
            }
            else if (model.QueueTypeID == Constants.Ext.QUEUE_TYPE_BANK)
            {
                if (model.FlagInprocess.AsBool()
                    && model.ResponsibleID.AsInt() == Constants.Ext.NO_INPUT)
                    throw new Exception(Constants.Message.ERROR.PLEASE_SELECT_RESPONSIBLE);
            }

            if ((model.FlagFinish.AsBool()) && model.ReasonID.AsInt() == Constants.Ext.NO_INPUT)
            {
                throw new Exception(Constants.Message.ERROR.PLEASE_SELECT_REASON);
            }
            else if (model.ReasonID.AsInt() > Constants.Ext.NO_INPUT && !(model.FlagFinish.AsBool()))
            {
                throw new Exception(Constants.Message.ERROR.PLEASE_CHECK_FINISH);
            }
            else if ((model.FlagFinish.AsBool()) && !(model.FlagInprocess.AsBool()))
            {
                throw new Exception(Constants.Message.ERROR.PLEASE_UN_FINISH);
            }
            else if (!(model.FlagFinish.AsBool()) && !string.IsNullOrEmpty(model.BankIDs))
            {
                throw new Exception(Constants.Message.ERROR.PLEASE_CHECK_FINISH);
            }
        }
        private tm_Unit GetUnit(RegisterLog model)
        {
            var query = _context.tm_Units.Where(e => e.FlagActive == true && e.UnitCode == model.UnitCode && e.ProjectID == model.ProjectID);
            if (query.Any())
            {
                return query.FirstOrDefault();
            }
            else
            {
                throw new Exception(Constants.Message.ERROR.UNIT_DOES_NOT_EXISTS);
            }
        }
        private void ValidateExistsUnitRegister(RegisterLog model)
        {
            var dt = DateTime.Now;
            var dtFrom = new DateTime(dt.Year, dt.Month, dt.Day);
            var dtEnd = new DateTime(dt.Year, dt.Month, dt.Day).AddDays(1);
            var query = _context.TR_RegisterLogs.Where(e => e.FlagActive == true && e.ProjectID == model.ProjectID && e.UnitID == model.UnitID
              && e.CreateDate >= dtFrom && e.CreateDate < dtEnd && e.QCTypeID == Constants.Ext.QC_TYPE_QC6
              && e.QueueTypeID == model.QueueTypeID);
            if (query.Any())
            {
                throw new Exception(Constants.Message.ERROR.UNIT_CODE_EXISTS);
            }
        }
        private void ValidateExistsUnitRegisterContract(RegisterLog model)
        {
            var query = from u in _context.tm_Units.Where(e => e.ProjectID == model.ProjectID
                        && e.ID == model.UnitID && e.FlagActive == true)
                        join ct in _context.PR_ContractVerifies.Where(e => e.FlagActive == true && e.ProjectID == model.ProjectID)
                         on new { ProjectID = u.ProjectID, UnitCode = u.UnitCode }
                         equals new { ProjectID = ct.ProjectID, UnitCode = ct.UnitCode }
                        select new { u, ct };
            if (!query.Any())
            {
                throw new Exception(Constants.Message.ERROR.UNIT_CODE_NOT_EXISTS_CONTRAT);
            }
        }

        public RegisterLog GetRegisterLogInfo(RegisterLog criteria, string UserID, string Password)
        {
            // 1) ดึงข้อมูลก้อนเดียวจาก EF ก่อน (ยังไม่ทำ external call)
            var raw =
            (
                from r in _context.TR_RegisterLogs
                where r.FlagActive == true
                      && r.ID == criteria.ID

                join u in _context.tm_Units
                    on r.UnitID equals u.ID

                join ct in _context.PR_ContractVerifies
                                .Where(e => e.FlagActive == true)
                    on new { u.ProjectID, u.UnitCode }
                    equals new { ct.ProjectID, ct.UnitCode }
                    into ctGroup   // 👈 สำคัญ (group join)

                from ct in ctGroup.DefaultIfEmpty() // 👈 LEFT JOIN ตรงนี้

                select new
                {
                    r,
                    u,
                    ct   // ct อาจเป็น null
                }
            )
            .FirstOrDefault();


            if (raw == null)
            {
                return new RegisterLog();
            }

            // 2) Map ข้อมูลหลักจาก EF -> RegisterLog (ยังไม่เรียก function ภายนอก)
            var data = new RegisterLog
            {
                ID = raw.r.ID,
                UnitCode = raw.u.UnitCode,
                ResponsibleID = raw.r.ResponsibleID.AsInt(),
                FlagRegister = raw.r.RegisterDate != null,
                FlagWait = raw.r.WaitDate != null,
                FlagInprocess = raw.r.InprocessDate != null,
                FlagFastFix = raw.r.FastFixDate != null,
                FlagFastFixFinish = raw.r.FastFixFinishDate != null,
                FlagFinish = raw.r.FinishDate != null,
                CareerTypeID = raw.r.CareerTypeID.AsInt(),
                TransferTypeID = raw.r.TransferTypeID.AsInt(),
                ReasonID = raw.r.ReasonID.AsInt(),
                ReasonRemarkID = raw.r.ReasonRemarkID.AsInt(),
                FixedDuration = raw.r.FixedDuration.AsInt(),

                ContractNumber = raw.ct != null ? raw.ct.ContractNumber : null,   // ✅ กัน ct null
                LoanID = raw.r.LoanID.AsGuid(),
                QuestionAnswersName = GetQuestionAnswer(raw.r.UnitID)
            };


            // 3) ตอนนี้ EF ปิด DataReader ไปแล้ว → ค่อยเรียก function อื่นที่ใช้ connection เดียวกัน
            data.BankIDs = GetRegisterBankList(raw.r.ID);

            // ✅ กัน ct null ก่อนเรียก RedirectHousingLoan
            if (raw.ct != null && !string.IsNullOrWhiteSpace(raw.ct.ContractNumber))
            {
                data.RedirectHousingLoan = _unitRepo.GetRedirectHousingLoan(raw.ct.ContractNumber, UserID, Password);
            }
            else
            {
                data.RedirectHousingLoan = ""; // หรือ "" แล้วแต่ชนิด property
            }

            if (data.LoanID != Guid.Empty)
            {
                data.Loan = getLoanByID_FINPlus(data.LoanID.AsGuid());
                data.LoanBankList = getLoanBank_FINPlus(data.LoanID.AsGuid());
            }

            return data;

        }

        private string GetRegisterBankList(int? ID)
        {
            var banks = _context.TR_RegisterBanks.Where(e => e.RegisterLogID == ID).Select(e => e.BankID).ToList();

            return string.Join(",", banks);
        }
        private LoanModel getLoanByID_FINPlus(Guid loanID)
        {
            var data = _context.PR_Loans.Where(e => e.ID == loanID && e.FlagActive == true)
                .Select(e => new LoanModel
                {
                    ID = e.ID,
                    SubmitDate = e.SubmitDate
                }).FirstOrDefault();
            return data;
        }
        private List<LoanBankModel> getLoanBank_FINPlus(Guid loanID)
        {
            var query = from lb in _context.PR_LoanBanks.Where(e => e.LoanID == loanID && e.FlagActive == true)
                        join b in _context.tm_Banks
                            on lb.BankID equals b.ID
                        select new
                        {
                            lb,
                            b
                        };
            var data = query.AsEnumerable().Select(e => new LoanBankModel
            {
                LoanBankID = e.lb.ID,
                BankCode = e.b.BankCode,
                CreateDate = e.lb.CreateDate
            }).OrderBy(e => e.CreateDate).ToList();
            return data;
        }
        private string GetQuestionAnswer(Guid? UnitID)
        {
            if (UnitID == null)
            {
                return string.Empty;
            }

            var answerName =
                (from qa in _context.TR_QuestionAnswers
                 join ans in _context.tm_Answers
                     on qa.AnswerID equals ans.ID into ansJoin
                 from ans in ansJoin.DefaultIfEmpty()
                 where qa.UnitID == UnitID
                       && qa.QuestionID == 1
                       && qa.FlagActive == true
                 select ans.AnswerName
                ).FirstOrDefault();

            if (string.IsNullOrWhiteSpace(answerName))
            {
                return string.Empty;
            }

            return answerName.Trim();
        }

        public void SaveCustomerSubmit_FINPlus(LoanModel model)
        {
            TransactionOptions option = new TransactionOptions();
            option.Timeout = new TimeSpan(1, 0, 0);
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, option))
            {
                var item = _context.PR_Loans.FirstOrDefault(e => e.ID == model.ID);
                if (item == null) return;

                item.SubmitDate = DateTime.Now;
                item.UpdateDate = DateTime.Now;
                item.SubmitBy = item.UpdateBy;

                _context.SaveChanges();
                scope.Complete();
            }
        }

        public string RemoveRegisterLog(int id)
        {
            var item = _context.TR_RegisterLogs
                               .FirstOrDefault(e => e.ID == id);

            if (item == null)
            {
                return "NOT_FOUND";
            }

            item.FlagActive = false;
            item.UpdateDate = DateTime.Now;

            _context.SaveChanges();

            return "SUCCESS";
        }

    }
}
