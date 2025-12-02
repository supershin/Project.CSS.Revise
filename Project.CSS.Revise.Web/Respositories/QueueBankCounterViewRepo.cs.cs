using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.CSS.Revise.Web.Data;
using Project.CSS.Revise.Web.Models.Pages.Project;
using Project.CSS.Revise.Web.Models.Pages.ProjectCounter;
using Project.CSS.Revise.Web.Models.Pages.QueueBankCounterView;

namespace Project.CSS.Revise.Web.Respositories
{
    public interface IQueueBankCounterViewRepo
    {
        public List<ListCounterModel.ListCounterItem> GetListsCounterQueueBank(ListCounterModel.Filter filter);
    }
    public class QueueBankCounterViewRepo : IQueueBankCounterViewRepo
    {
        private readonly CSSContext _context;

        public QueueBankCounterViewRepo(CSSContext context)
        {
            _context = context;
        }

        public List<ListCounterModel.ListCounterItem> GetListsCounterQueueBank(ListCounterModel.Filter filter)
        {
            List<ListCounterModel.ListCounterItem> result = new List<ListCounterModel.ListCounterItem>();
            string connectionString = _context.Database.GetDbConnection().ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"
                            -- Step 1: Generate Counter rows
                            WITH CounterList AS (
                                SELECT 
                                    T1.ID,
                                    T1.ProjectID,
                                    T1.QueueTypeID,
                                    Counter = T1.StartCounter + V.number,
                                    T1.FlagActive
                                FROM TR_ProjectCounter_Mapping T1 WITH (NOLOCK)
                                JOIN master..spt_values V 
                                    ON V.type = 'P'
                                    AND V.number BETWEEN 0 AND (T1.EndCounter - T1.StartCounter)
                                WHERE 1 = 1
                                  AND T1.ProjectID = @ProjectID
                                  AND T1.FlagActive = 1
                                  AND T1.QueueTypeID = 48
                            )

                            -- Step 2: LEFT JOIN with TR_RegisterLog
                            SELECT
                                 C.ID AS ProjectCounterMappingID
                                ,C.ProjectID
                                ,C.QueueTypeID
                                ,C.[Counter]

                                -- Fields from RegisterLog
                                ,RL.ID              AS RegisterLogID
                                ,RL.UnitID
                                ,TU.[UnitCode]
                                ,RL.[ResponsibleID]
                                ,RL.[FastFixDate]
                                ,RL.[FinishDate]
                                ,RL.[CareerTypeID]
                                ,RL.[TransferTypeID]
                                ,RL.[ReasonID]
                                ,RL.[FixedDuration]
                                ,RL.[Counter]       -- ถ้าไม่ใช้จริง ๆ ตัดออกได้

                                ,RBC.[BankID]
                                ,RBC.[BankCounterStatus]
                                ,RBC.[CheckInDate]
                                ,RBC.[InProcessDate]
                                ,RBC.[CheckOutDate]
                                ,TB.[BankCode]
                                ,TB.[BankName]
                            FROM CounterList C
                            LEFT JOIN TR_RegisterLog RL WITH (NOLOCK)
                                ON RL.ProjectID = C.ProjectID
                                AND RL.QueueTypeID = C.QueueTypeID
                                AND RL.Counter = C.Counter
                                AND RL.FlagActive = 1
                                AND RL.QueueTypeID = 48
                                AND RL.FinishDate IS NULL
                            LEFT JOIN tm_Unit TU WITH (NOLOCK)
                                ON RL.UnitID = TU.ID
                            LEFT JOIN TR_Register_BankCounter RBC WITH (NOLOCK)
                                ON RL.ID = RBC.[RegisterLogID] 
                                AND RBC.[BankCounterStatus] != 'Check Out'
                            LEFT JOIN tm_Bank TB WITH (NOLOCK)  
                                ON RBC.[BankID] = TB.ID
                            ORDER BY C.Counter;
                    ";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@ProjectID", filter.ProjectID ?? string.Empty));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var item = new ListCounterModel.ListCounterItem
                            {
                                ProjectCounterMappingID = Commond.FormatExtension.NullToString(reader["ProjectCounterMappingID"]),
                                ProjectID = Commond.FormatExtension.NullToString(reader["ProjectID"]),
                                QueueTypeID = Commond.FormatExtension.NullToString(reader["QueueTypeID"]),
                                Counter = Commond.FormatExtension.NullToString(reader["Counter"]),

                                // RegisterLog fields
                                RegisterLogID = Commond.FormatExtension.NullToString(reader["RegisterLogID"]),
                                UnitID = Commond.FormatExtension.NullToString(reader["UnitID"]),
                                UnitCode = Commond.FormatExtension.NullToString(reader["UnitCode"]),
                                ResponsibleID = Commond.FormatExtension.NullToString(reader["ResponsibleID"]),
                                FastFixDate = Commond.FormatExtension.NullToString(reader["FastFixDate"]),
                                FinishDate = Commond.FormatExtension.NullToString(reader["FinishDate"]),
                                CareerTypeID = Commond.FormatExtension.NullToString(reader["CareerTypeID"]),
                                TransferTypeID = Commond.FormatExtension.NullToString(reader["TransferTypeID"]),
                                ReasonID = Commond.FormatExtension.NullToString(reader["ReasonID"]),
                                FixedDuration = Commond.FormatExtension.NullToString(reader["FixedDuration"]),

                                // Bank Counter fields
                                BankID = Commond.FormatExtension.NullToString(reader["BankID"]),
                                BankCounterStatus = Commond.FormatExtension.NullToString(reader["BankCounterStatus"]),
                                CheckInDate = Commond.FormatExtension.NullToString(reader["CheckInDate"]),
                                InProcessDate = Commond.FormatExtension.NullToString(reader["InProcessDate"]),
                                CheckOutDate = Commond.FormatExtension.NullToString(reader["CheckOutDate"]),

                                // Bank master data
                                BankCode = Commond.FormatExtension.NullToString(reader["BankCode"]),
                                BankName = Commond.FormatExtension.NullToString(reader["BankName"])
                            };

                            result.Add(item);
                        }
                    }
                }
            }

            return result;
        }


    }
}
