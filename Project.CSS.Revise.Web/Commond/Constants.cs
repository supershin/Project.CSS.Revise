using System.Configuration;
namespace Project.CSS.Revise.Web.Commond
{
    public static class Constants
    {
        public static class ThirdPartyApis
        {
            public static readonly string HousingLoanRedirect = "http://localhost:57535/Utility/AuthenticationCS?Authorize=";
        }

        public static class Ext
        {
            public static readonly int NO_INPUT = 0;
            public static readonly int QC_TYPE_QC6 = 10;
            public static readonly int QC6 = 10;
            public static readonly int Customer_Service = 31;
            public static readonly int QUEUE_TYPE_BANK = 48;
            public static readonly int UserBank = 74;
            public static readonly int Target = 181;
            public static readonly int Rolling = 182;
            public static readonly int Unit = 183;
            public static readonly int Value = 184;
            public static readonly int Actual = 185;
            public static readonly int WorkingTarget = 395;
            public static readonly int WorkingRolling = 396;
            public static readonly int MLL = 452;


            public static readonly string NO_INPUT_NAME = "ไม่ระบุ";
            public static readonly string PLEASE_INPUT_NAME = "โปรดระบุ";
            public static readonly string PLEASE_SELECT_BANK = "เลือกธนาคาร";
            public static readonly string NO_INPUT_CHECK_STATUS = "ยังไม่ตรวจ";
            public static readonly string NO_INPUT_NAME_VALUE = "9999";
            public static readonly string NO_INPUT_NAME_EN = "All";
            public static readonly int QC_STATUS_PASS = 1;
            public static readonly string QC_STATUS_PASS_TEXT = "Y";
            public static readonly int QC_STATUS_NOT_PASS = 2;
            public static readonly string QC_STATUS_NOT_PASS_TEXT = "N";
            public static readonly int QC_STATUS_PASS_REASON = 19;
            public static readonly string QC_STATUS_PASS_REASON_TEXT = "R";
            public static readonly int GROUP_USER_PROJECT_MANAGER = 33;
            public static readonly int QC_STATUS_PASS_REASON_AN = 34;

            public static readonly string QC_STATUS_PASS_REASON_AN_TEXT = "AN";
            public static readonly int QC_TYPE_QC1 = 3;
            public static readonly int QC_TYPE_QC2 = 4;
            public static readonly int QC_TYPE_QC3 = 5;
            public static readonly int QC_TYPE_QC4 = 6;
            public static readonly int QC_TYPE_QC5 = 7;
            public static readonly int REPORT_OVERVIEW_MONITOR = 8;
            public static readonly int REPORT_MATRIX_MONITOR = 9;
            public static readonly int QC_TYPE_QC5_OPEN = 11;
            public static readonly int SUBJECT_STATUS_PASS = 16;
            public static readonly int SUBJECT_STATUS_NOT_PASS = 17;
            public static readonly int SUBJECT_STATUS_PASS_CONDITION = 18;
            public static readonly int DEFECT_STATUS_OPEN = 28;
            public static readonly int DEFECT_STATUS_CLOSE = 29;
            public static readonly int DEFECT_STATUS_IN_PROCESS = 30;
            public static readonly int GROUP_USER_CUSTOMER_SERVICE = 31;
            public static readonly int DEFECT_STATUS_WAIT = 42;
            public static readonly int DEFECT_STATUS_FINISH = 43;
            public static readonly int REPORT_MATRIX_QC6 = 35;
            public static readonly int APPOINT_TYPE_INSPECT = 36;
            public static readonly int REPORT_UN_SOLD_SUMMARY_DEFECT = 40;
            public static readonly int QUEUE_TYPE_INSPECT = 49;
            public static readonly int REGISTER_REASON_BANK_LOAN = 50;
            public static readonly int REPORT_SUMMARY_DEFECT = 52;
            public static readonly int UNIT_STATUS_CS_AVAILABLE = 68;
            public static readonly int REPORT_CONTACT_LOG = 72;
            public static readonly int USER_TYPE_CS = 73;
            public static readonly int USER_TYPE_BANK = 74;
            public static readonly int BANK_PROGRESS_STATUS_PREAPPROVE = 123;
            public static readonly int BANKSTATUS_APPROVE = 127;
            public static readonly string BANKSTATUS_APPROVE_TXT = "Approve";
            public static readonly string BANKSTATUS_INPROCESS_TXT = "Inprocess";
            public static readonly int BANKSTATUS_REJECT = 128;
            public static readonly string BANKSTATUS_REJECT_TXT = "Reject";
            public static readonly int BANKSTATUS_CANCEL = 129;
            public static readonly string BANKSTATUS_CANCEL_TXT = "Cancel";
            public static readonly string BANKSTATUS_WAIT_BANK_TXT = "Wait Bank";
            public static readonly int REPORT_SUMMARY_LOAN_BY_BANK = 148;
            public static readonly int GROUP_USER_AUTO_MAIL = 149;
            public static readonly int EXPECT_TRANSFER_BY_CASH = 150;
            public static readonly int EXPECT_TRANSFER_BY_LOAN = 151;
            public static readonly int REPORT_SUMMARY_LOAN_BY_PROJECT = 154;
            public static readonly int REPORT_CS_QC_STATUS_PIVOT = 155;
            public static readonly int REPORT_CS_WEEKLY_TRANSFER_STATUS = 180;
            public static readonly int PLAN_AMOUNT_UNIT = 183;
            public static readonly int PLAN_AMOUNT_VALUE = 184;
            public static readonly int REPORT_TYPE_FINPLUS_BANK_SUBMIT = 207;
            public static readonly int CUSTOMER_SATISFACTION_CS3 = 209;
            public static readonly int REPORT_CUSTOMER_SATISFACTION = 210;
            public static readonly int GROUP_USER_UN_SOLD = 211;
            public static readonly int QC_TYPE_QC6_Unsold = 212;
            public static readonly int GROUP_USER_CUSTOMER_INTELLIGENT = 213;
            public static readonly int APPOINT_TYPE_FINANCE = 229;
            public static readonly int UNIT_DOCUMENT_INSPECT = 230;
            public static readonly int UNIT_DOCUMENT_INSPECT_HIRE = 231;
            public static readonly int UNIT_DOCUMENT_RECEIVE = 232;
            public static readonly int UNIT_DOCUMENT_INSPECT_UN_SOLD = 233;
            public static readonly int UNIT_DOCUMENT_RECEIVE_ROOM_AGREEMENT = 234;
            public static readonly int APPOINT_TYPE_INSPECT_ONLINE = 235;
            public static readonly int UNIT_DOCUMENT_RECEIVE_TRANSFER = 237;
            public static readonly int ESTIMATE_STATUS_ACCEPT_ID = 260;
            public static readonly int ESTIMATE_STATUS_NOT_ACCEPT_ID = 261;
            public static readonly string ESTIMATE_STATUS_ACCEPT_NAME = "Accept";
            public static readonly string ESTIMATE_STATUS_NOT_ACCEPT_NAME = "Not Accept";
            public static readonly int ESTIMATE_STATUS_NO_ACTION_ID = 262;
            public static readonly int UNIT_DOCUMENT_PROMOTION = 263;
            public static readonly int LETTER_TYPE_TRANSFER = 264;
            public static readonly int LETTER_TYPE_CANCEL_CONTRACT = 265;
            public static readonly int LETTER_SEND_TYPE_LETTER = 266;
            public static readonly int LETTER_SEND_TYPE_EMAIL = 267;
            public static readonly int LETTER_APPROVE_STATUS_APPROVE = 268;
            public static readonly int LETTER_APPROVE_STATUS_NOT_APPROVE = 269;
            public static readonly int LETTER_SEND_STATUS_SUCCESS = 270;
            public static readonly int LETTER_SEND_STATUS_FAIL = 271;
            public static readonly int LETTER_SEND_STATUS_SENDDING = 279;
            public static readonly int LETTER_NOTIFICATION_DAY_DUE = 272;
            public static readonly int LETTER_VERIFY_STATUS_PASS = 273;
            public static readonly int LETTER_VERIFY_STATUS_NOT_PASS = 274;
            public static readonly int GROUP_USER_BUSINESS_SUPPORT = 275;
            public static readonly int TERMINATE_TRANSFER_SUCCESS = 276;
            public static readonly int TERMINATE_TRANSFER_FAIL = 277;
            public static readonly int UNIT_DOCUMENT_TERMINATE_TRANSFER_APPOINT = 278;
            public static readonly int PROJECT_STATUS_RTM = 283;
            public static readonly int UNIT_FURNITURE_CHECK_STATUS_PASS = 309;
            public static readonly int UNIT_FURNITURE_CHECK_STATUS_NOT_PASS = 310;
            public static readonly int UNIT_DOCUMENT_UNIT_FURNITURE = 312;
            public static readonly int REPORT_SUMMARY_REGISTER_INSPECT = 313;
            public static readonly int GROUP_USER_FINANCE = 314;
            public static readonly int GROUP_USER_AFTER_SALES = 315;
            public static readonly int REVISE_UNIT_PROMOTION_STATUS_PASS = 317;
            public static readonly int REVISE_UNIT_PROMOTION_STATUS_NOT_PASS = 318;
            public static readonly int REVISE_UNIT_PROMOTION_APPROVER_1 = 319;
            public static readonly int REVISE_UNIT_PROMOTION_APPROVER_2 = 320;
            public static readonly int GROUP_USER_JURICTIC = 323;
            public static readonly int[] LETTER_TYPE_CS_LIST = new int[] { 326, 327, 328, 329, 330, 331 };
            public static readonly int LETTER_TYPE_P1 = 326;
            public static readonly int LETTER_TYPE_P2 = 327;
            public static readonly int LETTER_TYPE_F1 = 328;
            public static readonly int LETTER_TYPE_F2 = 329;
            public static readonly int LETTER_TYPE_I1 = 330;
            public static readonly int LETTER_TYPE_N3 = 331;
            public static readonly int GROUP_USER_ACCOUNTING = 350;
            public static readonly int REPORT_CASH_BACK = 368;
            public static readonly int CONTACT_TEAM_CUSTOMER_SERVICE = 382;
            public static readonly int CONTACT_TEAM_CHECKER = 391;
            public static readonly int GROUP_USER_CUSTOMER_RELATION = 392;
            public static readonly int CONTACT_TEAM_SALE = 394;
            public static readonly int CONTACT_REASON_APPOINT_PAYMENT = 403;
            public static readonly int REGISTER_CAREER_FREEDOM = 407;
        }

        public static class ExtType
        {
            public static readonly int QCStatus = 1;
            public static readonly int QCType = 2;
            public static readonly int ReportType = 3;
            public static readonly int UnitStatus = 4;
            public static readonly int SubjectStatus = 5;
            public static readonly int AppointmentType = 6;
            public static readonly int CustomerRelation = 7;
            public static readonly int DefectStatus = 8;
            public static readonly int GroupUser = 9;
            public static readonly int RegisterReasonInspect = 10;
            public static readonly int VendorType = 11;
            public static readonly int CareerType = 12;
            public static readonly int TransferType = 13;
            public static readonly int QueueType = 14;
            public static readonly int RegisterReasonBank = 15;
            public static readonly int CSUnitStatus = 16;
            public static readonly int UserType = 17;
            public static readonly int CareerType_Duplicate = 18; // ⚠️ Duplicated name
            public static readonly int IncomeType = 19;
            public static readonly int DebtType = 20;
            public static readonly int AttachType_Main = 21;
            public static readonly int AttachType_Sub = 22;
            public static readonly int BankProgress = 23;
            public static readonly int BankStatus = 24;
            public static readonly int RejectReason = 25;
            public static readonly int QC6MatrixType = 26;
            public static readonly int ExpectTransferBy = 27;
            public static readonly int GroupQC6MatrixType = 28;
            public static readonly int ReportQCProgress = 29;
            public static readonly int QCProgressRelation = 30;
            public static readonly int QC5MatrixType = 31;
            public static readonly int TargetRollingPlan = 32;
            public static readonly int PlanAmountType = 33;
            public static readonly int ReportCSTransferStatusType = 34;
            public static readonly int MeterType = 35;
            public static readonly int MortgageType = 36;
            public static readonly int MarriedStatus = 37;
            public static readonly int ProjectZone = 38;
            public static readonly int CustomerSatisfaction = 39;
            public static readonly int ReportTypeUnSold = 40;
            public static readonly int AppointDayLimit = 41;
            public static readonly int AppointTimeLimit = 42;
            public static readonly int UnitDocumentType = 43;
            public static readonly int RemarkUnitStatus_CS = 44;
            public static readonly int QC5CheckListAnswer = 45;
            public static readonly int OverdueEstimateStatus = 46;
            public static readonly int LetterType = 47;
            public static readonly int LetterSendType = 48;
            public static readonly int LetterApproveStatus = 49;
            public static readonly int LetterSendStatus = 50;
            public static readonly int LetterNotification = 51;
            public static readonly int LetterVerifyStatus = 52;
            public static readonly int TerminateTransferAppoint = 53;
            public static readonly int ProjectStatus = 54;
            public static readonly int FixedDuration = 55;
            public static readonly int UnitFurnitureCheckStatus = 56;
            public static readonly int ReviseUnitApproveStatus = 57;
            public static readonly int ReviseUnitPromotionRoleApprove = 58;
            public static readonly int ProjectPartner = 59;
            public static readonly int QC5SyncType = 60;
            public static readonly int ContactTeam = 61;
            public static readonly int DeviateStatus = 62;
            public static readonly int ContactLogCustomerType = 63;
            public static readonly int ContactLogReason = 64;
            public static readonly int UnitStatusSale = 65;
        }

        public static class Message
        {
            public static class SUCCESS
            {
                public static readonly string SAVE_SUCCESS = "Save Success.";
                public static readonly string LOGIN_SUCCESSFUL = "Login successful.";
            }
            public static class ERROR
            {
                public static readonly string USER_NOT_FOUND = "User or email is not found.";
                public static readonly string INVALID_USER_OR_PASSWORD = "Invalid Username or Password.";
                public static readonly string USERNAME_AND_PASSWORD_CANNOT_BE_EMPTY = "Username and Password cannot be empty.";
                public static readonly string PLEASE_INPUT_DATA = "Please input data (*)";
                public static readonly string UNIT_CODE_EXISTS = "Unit Code นี้ได้มีการ Register แล้ว";
                public static readonly string UNIT_CODE_NOT_EXISTS_CONTRAT = "Unit Code ยังไม่มีการทำสัญญา";
                public static readonly string UNIT_DOES_NOT_EXISTS = "ไม่พบ Unit นี้ในระบบ";


                public static readonly string INVALID_LOGIN = "Invalid Login.";
                public static readonly string PLEASE_INPUT_DATA_VALID = "โปรดระบุข้อมูลให้ครบถ้วน และถูกต้อง";
                public static readonly string PLEASE_INPUT_EMAIL = "Please input email for send verify code";
                public static readonly string PLEASE_INPUT_MOBILE = "Please input mobil for send verify code";
                public static readonly string PLEASE_SELECT_RESPONSIBLE = "Please select responsible.";
                public static readonly string PLEASE_SELECT_REASON = "Please select reason.";
                public static readonly string PLEASE_SELECT_REASON_THAI = "โปรดระบุเหตุผล";
                public static readonly string PLEASE_SELECT_CONTACT_DETAIL = "โปรดระบุรายละเอียดมากกว่า 10 ตัวอักษร";
                public static readonly string PLEASE_CHECK_WAIT = "Please check wait.";
                public static readonly string PLEASE_CHECK_INPROCESS = "Please check inprocess.";
                public static readonly string PLEASE_CHECK_FINISH = "Please check finish.";
                public static readonly string PLEASE_UN_FINISH = "Please un check finish.";
                public static readonly string PLEASE_SEARCH_USER = "Please Search User.";
                public static readonly string DEFECT_NOT_FOUND = "Defect not found.";
                public static readonly string FLOOR_PLAN_EXISTS_UNIT = "ไม่สามารถลบ Floor Plan นี้ได้ เนื่องจากมี Unit ใช้งานอยู่";
                public static readonly string DEFECT_FLOOR_PLAN_EXISTS_UNIT = "ไม่สามารถลบ Floor Plan นี้ได้ เนื่องจากมี Defect ใช้งานอยู่";
                public static readonly string PLEASE_CHECK_FLOOR_PLAN = "กรุณาเลือก Floor Plan";
                public static readonly string PLEASE_CHECK_UNIT = "กรุณาเลือก Unit";
                public static readonly string SAVE_FAIL_API_QC5 = "Save Fail (QC5).";
                public static readonly string SAVE_DELETE_INSPECT_FAIL = "กรุณาเลือก การตรวจรับที่ต้องการลบ";
                public static readonly string SAVE_FAIL_API_QC6 = "Save Fail (QC6).";
                public static readonly string SAVE_FAIL_API_DEFECT = "Save Fail (Defect).";
                public static readonly string SAVE_FAIL_API_DEFECT_RESOURCE = "Save Fail (Defect Resource).";
                public static readonly string SAVE_FAIL_API_SIGN_RESOURCE = "Save Fail (Sign Resource).";
                public static readonly string SAVE_FAIL_API_UPDATE_UNIT = "Save Fail (Update Unit).";
                public static readonly string INVALID_CONFIRM_PASSWORD = "Confirm Password ไม่ถูกต้อง";
                public static readonly string INVALID_USER_ID_PASSWORD = "User ID หรือ Password นี้มีในระบบแล้ว";
                public static readonly string USER_DOES_NOT_EXISTS = "User does not exists.";
                public static readonly string PLEASE_SELECT_DEFECT = "โปรดเลือก defect ที่ต้องการ";
                public static readonly string PLEASE_INPUT_EXPECT_DATE = "โปรดระบุวันที่คาดว่าจะเสร็จ";
                public static readonly string PLEASE_INPUT_REQUEST_QC6_DATE = "โปรดระบุวันที่ Request QC6";
                public static readonly string PLEASE_INPUT_CAREER_TYPE = "โปรดระบุ Career Type";
                public static readonly string PLEASE_INPUT_TRANSFER_TYPE = "โปรดระบุ Transfer Type";
                public static readonly string INVALID_CHANGE_DEFECT_STATUS = "ไม่สามารถ update ข้ามสถานะของ defect ได้";
                public static readonly string INVALID_PRINT_RECEIVE_ROOM = "ไม่สามารถ Print Receive Room ได้เนื่องจากมี Defect ที่ยังไม่ Close หรือไม่มีรายการ Defetc";
                public static readonly string INVALID_USERNAME_PASSWORD = "Username นี้มีในระบบแล้ว";
                public static readonly string INVALID_EMAIL = "Email นี้มีในระบบแล้ว";
                public static readonly string INVALID_QUESTION_ANSWER = "โปรดระบุคำตอบอย่างน้อย 1 ข้อ";
                public static readonly string PLEASE_INPUT_EXPECT_TRANSFER = "โปรดระบุ Expect Transfer";
                public static readonly string PLEASE_INPUT_EXPECT_TRANSFER_YEARLY = "โปรดระบุ Expect Transfer (Yearly) ให้ถูกต้อง";
                public static readonly string PLEASE_INPUT_UNITCODE = "กรุณาระบุเลขที่ยูนิต";
                public static readonly string PLEASE_INPUT_SATISFACTION_ANSWER = "กรุณาตอบคำถามให้ครบถ้วน";
                public static readonly string INVALID_UNITCODE = "Unit Code ไม่ถูกต้อง";
                public static readonly string PLEASE_SELECT_UNSOLD_ROUND = "กรุณาเลือกรอบการตรวจ";
                public static readonly string PLEASE_SELECT_UNSOLD_ROUND_UNIT = "กรุณาเลือก Unit ที่ต้องการตรวจ";
                public static readonly string CONTRACT_DOES_NOT_EXISTS = "ไม่พบสัญญาของยูนิตนี้";
                public static readonly string UNIT_DOCUMENT_NOT_EXISTS = "ไม่พบเอกสารนี้ในระบบ";
                public static readonly string REGISTER_COUNTER_NOT_FOUND_UNIT = "ไม่พบ Unit {0} ในการลงทะเบียน หรือ ได้ Done ไปแล้ว";
                public static readonly string PROJECT_PERMISSION = "คุณไม่มีสิทธ์เข้าโครงการนี้";
                public static readonly string PAGE_PERMISSION = "คุณไม่มีสิทธ์เข้าที่ Page นี้";
                public static readonly string BANK_COUNTER_UNIT_INVALID = "Counter และ Unit ไม่ตรงกัน หรือมีการเปลี่ยนแปลง";
                public static readonly string BANK_BALANCE_STAFF_NOT_HAS = "ผู้ให้บริการเต็มจำนวน โปรดเลือกธนาคารอื่นที่ต้องการ";
                public static readonly string BANK_HAS_CHECKIN_IN_PROCESS = "ไม่สามารถ เลือกธนาคารอื่นได้ เนื่องจาก counter นี้มีธนาคารอื่นอยู่";
                public static readonly string OVERDUE_PLEASE_SELCT_STATUS = "เลือก Status ที่ต้องการ";
                public static readonly string USED_PROMOTION_CANNOT_REMOVE = "ไม่สามารถนำโปรโมชั่นที่ใช้แล้วออกได้";
                public static readonly string PLEASE_SELECT_PROMOTION = "โปรดเลือก Promotion ที่ต้องการ";
                public static readonly string UNIT_PROMOTION_NOT_FOUND = "Promotion ที่เลือกไม่ตรงกับที่ได้กำหนดไว้ก่อนหน้า";
                public static readonly string PLEASE_INPUT_LETTER_SEND_TYPE = "โปรดระบุ ประเภทการส่ง";
                public static readonly string PLEASE_INPUT_LETTER_TRANSFER_DUE = "โปรดระบุ วันที่กำหนดโอนกรรมสิทธิ์";
                public static readonly string PLEASE_INPUT_LETTER_CANCEL_CONTRACT_DUE = "โปรดระบุ วันที่ยืนยันออกจดหมายยกเลิกสัญญาให้ถูกต้อง";
                public static readonly string PLEASE_INPUT_LETTER_SEND_DUE_DATE = "โปรดระบุ วันที่ออกจดหมาย";
                public static readonly string INVALID_LETTER_CUSTOMER_NAME_EMAIl = "ไม่พบชื่อ หรืออีเมลลูกค้าจากยูนิตนี้ในระบบ";
                public static readonly string INVALID_LETTER_CONTRACT_NUMBER = "มีรายการไม่พบ เลขที่สัญญาในระบบ";
                public static readonly string CODE_VERIFY_NOT_FOUND = "มีรายการ ไม่พบ Code Verify ในระบบ";
                public static readonly string INVALID_LETTER_CS_RESPONSE = "ข้อมูลผู้ดูแล Unit (CS Response) ไม่ครบถ้วน";
                public static readonly string INVALID_LETTER_SEND_DUE_DATE = "รอบกำหนดการส่ง (send due date) ไม่ถูกต้อง หรือเลยรอบ due นั้นๆแล้ว (ต้องแจ้งล่วงหน้า 7 วัน)";
                public static readonly string NOT_FOUND = "ไม่พบรายการนี้";
                public static readonly string LETTER_HAS_APPROVE_OR_NOT = "รายการนี้มีการทำอนุมัติ หรือไม่อนุมัติไปแล้ว";
                public static readonly string LETTER_HAS_VERIFY_OR_NOT = "รายการนี้มีการตรวจสอบไปแล้ว";
                public static readonly string LETTER_ALREADY_EXISTS = "ไม่สามารถบันทึกจดหมายที่ซ้ำกัน ในวันที่ออกจดหมายวันเดียวกันได้";
                public static readonly string LETTER_TRANSFER_NOT_FOUNT = "ไม่สามารถบันทึกจดหมายยกเลิกสัญญาได้ เนื่องจากไม่พบจดหมายนัดโอนกรรมสิทธิ์เพื่ออ้างอิง";
                public static readonly string PLEASE_SELECT_LETTER_UNIT = "โปรดเลือก Unit ที่ต้องการ";
                public static readonly string INVALID_LETTER_APPROVE_INVALID = "ข้อมูลผู้อนุมัติไม่ถูกต้อง";
                public static readonly string INVALID_LETTER_APPROVE_SIGN_NOT_FOUND = "ไม่พบลายเซ็นของผู้อนุมัติ";
                public static readonly string INVALID_LETTER_APPROVE_POSITION_NOT_FOUND = "ไม่พบตำแหน่งของผู้อนุมัติ";
                public static readonly string INVALID_LETTER_APPROVE_UNIT_HAS_PRINT = "ไม่สามารถบันทึกได้ เนื่องจากมี unit ที่ได้ทำการสั่งพิมพ์ไปแล้ว";
                public static readonly string INVALID_LETTER_VERIFY_NOT_PASS = "ไม่สามารถบันทึกได้ เนื่องจากมี unit ที่ไม่ผ่านการตรวจสอบ";
                public static readonly string INVALID_LETTER_PRINT_UNIT_NOT_APPROVE = "ไม่สามารถบันทึกได้ เนื่องจากมี unit ที่ไม่ผ่านการอนุมัติ";
                public static readonly string INVALID_LETTER_VERIFY_UNIT_HAS_APPROVE = "ไม่สามารถบันทึกได้ เนื่องจากมี unit ที่มีการอนุมัติไปแล้ว";
                public static readonly string INVALID_LETTER_PROJECT_COMPANY = "ข้อมูลบริษัทของโครงการนี้ไม่ครบถ้วน";
                public static readonly string INVALID_LETTER_PROJECT_LAND_OFFICE = "ข้อมูลสำนักงานที่ดินของโครงการนี้ ไม่ครบถ้วน";
                public static readonly string TERMINATE_TRANSFER_UNIT_NOT_FOUND = "ไม่พบยูนิตนี้ในระบบ";
                public static readonly string PLEASE_SELECT_FIX_DURATION = "โปรดระบุ Fixed Duration";
                public static readonly string PLEASE_SELECT_FIX_DURATION_OR_REASON = "โปรดระบุ Fixed Duration หรือ Reason";
                public static readonly string PLEASE_SELECT_UNIT_OR_FURNITURE = "โปรดเลือก furniture & unit ให้ครบถ้วน";
                public static readonly string FURNITURE_AMOUNT_NOT_VALID = "โปรดระบุ จำนวนของ furniture ให้ถูกต้อง";
                public static readonly string SIGN_ALREADY_EXISTS = "ไม่สามาถ save ได้ เนื่องจากมีการ sign แล้ว";
                public static readonly string FURNITURE_NAME_AREADY_EXISTS = "ชื่อ furniture นี้มีในระบบแล้ว";
                public static readonly string FURNITURE_IS_USED_ANOTHER_UNIT = "ไม่สามารถแก้ไขได้เนื่องจาก furniture มีการใช้งานที่ unit อื่นๆ";
                public static readonly string EQUIPMENT_NAME_AREADY_EXISTS = "ชื่อ อุปกรณ์นี้ นี้มีในระบบแล้ว";
                public static readonly string EQUIPMENT_IS_USED_ANOTHER_UNIT = "ไม่สามารถแก้ไขได้เนื่องจาก อุปกรณ์นี้ มีการใช้งานที่ unit อื่นๆ";
                public static readonly string EQUIPMENT_AMOUNT_NOT_VALID = "โปรดระบุ จำนวนของ อุปกรณ์ ให้ถูกต้อง";
                public static readonly string PLEASE_SELECT_UNIT_OR_EQUIPMENT = "โปรดเลือก อุปกรณ์ & unit ให้ครบถ้วน";
                public static readonly string PLEASE_INPUT_CM_SIGN_NAME = "โปรดระบุ CM Name";
                public static readonly string PLEASE_INPUT_CM_SIGN = "โปรดระบุ CM Sign";
                public static readonly string PLEASE_CHECK_UNIT_FURNITURE = "ไม่สามารถ Save ได้ เนื่องจากมีรายการที่ไม่ผ่านการตรวจสอบ";
                public static readonly string PLEASE_SELECT_UNIT_SIGN = "โปรดเลือก unit ที่ต้องการ sign";
                public static readonly string PLEASE_REGISTER_DATE = "โปรดระบุวันที่ Register ให้ครบถ้วน";
                public static readonly string REVISE_PROMOTION_CRM_NOT_FOUND = "ไม่พบการปรับโปรโมชั่นบนระบบ CRM";
                public static readonly string REVISE_PROMOTION_IN_PROCRESS = "อยู่ระหว่างการอนุมัติ";
                public static readonly string PLEASE_INPUT_SIGN = "โปรดระบุลายเซ็นให้ครบถ้วน";
                public static readonly string FILE_EXTENSION_NOT_IMAGE = "โปรดแนบเอกสารไฟล์รูปภาพ .jpg .jpeg .png เท่านั้น";
                public static readonly string UNIT_HAS_CUSTOMERSIGN = "ไม่สามารถแก้ไข unit ที่มีการ sign แล้วได้";
                public static readonly string APPOINT_TIME_UN_AVAILABLE = "เวลานัดดังกล่าวไม่ว่างแล้วขณะนี้";
                public static readonly string PLEASE_SELECT_EVENT = "โปรดเลือก event ที่ต้องการ";
                public static readonly string PLEASE_SELECT_UNIT = "โปรดเลือก unit ที่ต้องการ";
                public static readonly string POSITION_OR_SIGNATURE_NOT_FOUND = "ไม่พบตำแหน่ง หรือ ลายเซ็นต์ ผู้ลงนาม";
                public static readonly string PLEASE_INPUT_PERCENT_PROGRESS = "โปรดระบุ % Progress งานก่อสร้าง";
                public static readonly string PLEASE_INPUT_FINPLUS_START_END_DATE = "โปรดระบุ วันที่ยื่นสินเชื่อ FINPlus ให้ครบถ้วน";
                public static readonly string PLEASE_INPUT_EVENT_START_END_DATE = "โปรดระบุ วันที่กำหนดจัดงาน Event ให้ครบถ้วน";
                public static readonly string PLEASE_INPUT_EVENT_LOCATION = "โปรดระบุ สถานที่จัดงานให้ครบถ้วน";
                public static readonly string LAST_INSPECT_DATE_NOT_FOUND = "มีรายการที่ไม่พบ วันที่ลูกค้าเข้าตรวจล่าสุด Last Inspect Date";
                public static readonly string FINISH_LAST_EXPECT_DATE_NOT_FOUND = "มีรายการที่ไม่พบ วันที่กำหนดแล้วเสร็จ Last Expect date (Status = Finish)";
                public static readonly string IRPA_JOB_INVALID = "irpa {0} job invalid";
                public static readonly string LETTER_CS_TYPE_ENG_INVALID = "เลือกประเภทจดหมาย & ภาษาไม่ถูกต้อง";
                public static readonly string UNIT_SHOP_CONTRACT_INVALID = "ไม่พบสัญานี้ในระบบ";
                public static readonly string UNIT_SHOP_EVENT_NOT_FOUND = "ไม่พบบูธ หรือร้านค้าที่กำหนด";
                public static readonly string UNIT_SHOP_UNIT_QUOTA_OVER_LIMIT = "คูปองร้านค้านี้ ครบกำหนดการใช้";
                public static readonly string UNIT_SHOP_QUOTA_OVER_LIMIT = "ร้านค้า ครบจำนวนให้บริการที่กำหนด";
                public static readonly string SELECT_INVALID_CHECKER = "ไม่สามารถทำการบันทึกทีมของ Checker ได้";
                public static readonly string LOAN_NOT_FOUND_FIN_PLUS = "ไม่พบรายการยื่นสินเชื่อบนระบบ FINPlus";
                public static readonly string LOAN_NOT_SAVE_DRAFT_FIN_PLUS = "ไม่ได้มีการบันทึก Save Draft บนระบบ FINPlus";
                public static readonly string PLEASE_INPUT_PAYMENT_DUE_DATE = "Please input payment due date";
                public static readonly string LETTER_TRANSFER_APPOINT_NOT_FOUND = "ไม่พบจดหมายนัดโอนบนระบบ";

            }
        }

        public static class Menu
        {
            public static readonly int Projecttargetrolling = 21;
            public static readonly int CsResponse = 22;
            public static readonly int Appointmentlimit = 24;
            public static readonly int FurnitureAndUnitFurniture = 33;
            public static readonly int Projectandunitfloorplan = 34;
            public static readonly int Project = 36;
        }

        public static class WARNING
        {
            public static readonly string APPOINTMENT_HAS_EXISTS = "An appointment already exists on {0} at {1}.";
            public static readonly string APPOINTMENT_DOES_NOT_EXISTS = "No appointment found.";

        }

        public static class Register
        {
            public static class BankCounterStatus
            {
                public static readonly string CHECK_IN = "Check In";
                public static readonly string CHECK_OUT = "Check Out";
                public static readonly string IN_PROCESS = "In Process";
            }
            public static class CounterView
            {
                public static readonly string UNIT = "unit";
                public static readonly string BANK = "bank";
                public static readonly string QRCODE = "qrcode";
            }
            public static class CallStatus
            {
                public static readonly string START = "start";
                public static readonly string STOP = "stop";
            }

        }
    }
}
