namespace Project.CSS.Revise.Web.Commond
{
    public static class Constants
    {
        public static class Ext
        {
            public static readonly int Target = 181;
            public static readonly int Rolling = 182;
            public static readonly int Actual = 185;
            public static readonly int WorkingTarget = 395;
            public static readonly int WorkingRolling = 396;
            public static readonly int MLL = 452;
            public static readonly int QUEUE_TYPE_BANK = 48;

            public static readonly int Unit = 183;
            public static readonly int Value = 184;
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
            }
        }
    }
}
