using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

public partial class CSSContext : DbContext
{
    public CSSContext()
    {
    }

    public CSSContext(DbContextOptions<CSSContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BM_Master_Bank> BM_Master_Banks { get; set; }

    public virtual DbSet<BM_Master_Question> BM_Master_Questions { get; set; }

    public virtual DbSet<BM_Master_QuestionAnswer> BM_Master_QuestionAnswers { get; set; }

    public virtual DbSet<BM_Master_ScoreType> BM_Master_ScoreTypes { get; set; }

    public virtual DbSet<BM_Master_Set> BM_Master_Sets { get; set; }

    public virtual DbSet<BM_Master_Set_QuestionAnswer> BM_Master_Set_QuestionAnswers { get; set; }

    public virtual DbSet<BM_TR_LoanAgeRate_Bank> BM_TR_LoanAgeRate_Banks { get; set; }

    public virtual DbSet<BM_TR_QuestionAnswerScore> BM_TR_QuestionAnswerScores { get; set; }

    public virtual DbSet<BM_TR_Set_Score> BM_TR_Set_Scores { get; set; }

    public virtual DbSet<BM_TR_UserAdmin> BM_TR_UserAdmins { get; set; }

    public virtual DbSet<BM_TS_Matching> BM_TS_Matchings { get; set; }

    public virtual DbSet<BM_TS_Matching_Detail> BM_TS_Matching_Details { get; set; }

    public virtual DbSet<BM_TS_Matching_QuestionAnswer> BM_TS_Matching_QuestionAnswers { get; set; }

    public virtual DbSet<BM_TS_Matching_ScoreSet> BM_TS_Matching_ScoreSets { get; set; }

    public virtual DbSet<BM_TS_Matching_ScoreSet_Detail> BM_TS_Matching_ScoreSet_Details { get; set; }

    public virtual DbSet<DefectArea> DefectAreas { get; set; }

    public virtual DbSet<DefectTypeDesc> DefectTypeDescs { get; set; }

    public virtual DbSet<EstaBliss_Temp> EstaBliss_Temps { get; set; }

    public virtual DbSet<GetUnit_V2> GetUnit_V2s { get; set; }

    public virtual DbSet<Line_PostLog> Line_PostLogs { get; set; }

    public virtual DbSet<Line_QRCode> Line_QRCodes { get; set; }

    public virtual DbSet<Line_Register> Line_Registers { get; set; }

    public virtual DbSet<Line_RegisterQRCode> Line_RegisterQRCodes { get; set; }

    public virtual DbSet<Line_User> Line_Users { get; set; }

    public virtual DbSet<Line_UserAppointment> Line_UserAppointments { get; set; }

    public virtual DbSet<Line_UserContract> Line_UserContracts { get; set; }

    public virtual DbSet<PR_AttachFile> PR_AttachFiles { get; set; }

    public virtual DbSet<PR_BankDocument> PR_BankDocuments { get; set; }

    public virtual DbSet<PR_ContractVerify> PR_ContractVerifies { get; set; }

    public virtual DbSet<PR_ContractVerify_CAM002> PR_ContractVerify_CAM002s { get; set; }

    public virtual DbSet<PR_Customer> PR_Customers { get; set; }

    public virtual DbSet<PR_CustomerCareer> PR_CustomerCareers { get; set; }

    public virtual DbSet<PR_CustomerDebt> PR_CustomerDebts { get; set; }

    public virtual DbSet<PR_CustomerIncome> PR_CustomerIncomes { get; set; }

    public virtual DbSet<PR_Loan> PR_Loans { get; set; }

    public virtual DbSet<PR_LoanBank> PR_LoanBanks { get; set; }

    public virtual DbSet<PR_LoanBankAttachFile> PR_LoanBankAttachFiles { get; set; }

    public virtual DbSet<PR_LoanBank_Explain> PR_LoanBank_Explains { get; set; }

    public virtual DbSet<PR_LoanCustomer> PR_LoanCustomers { get; set; }

    public virtual DbSet<PR_LoanCustomerAttach> PR_LoanCustomerAttaches { get; set; }

    public virtual DbSet<PR_Log> PR_Logs { get; set; }

    public virtual DbSet<PR_ProjectBankUser_Mapping> PR_ProjectBankUser_Mappings { get; set; }

    public virtual DbSet<PR_ProjectCS_Mapping> PR_ProjectCS_Mappings { get; set; }

    public virtual DbSet<PR_User> PR_Users { get; set; }

    public virtual DbSet<PR_UserBank_Mapping> PR_UserBank_Mappings { get; set; }

    public virtual DbSet<PR_User_PasswordChange> PR_User_PasswordChanges { get; set; }

    public virtual DbSet<Sys_Master_Project> Sys_Master_Projects { get; set; }

    public virtual DbSet<Sys_Master_Unit> Sys_Master_Units { get; set; }

    public virtual DbSet<Sys_REM_Floor> Sys_REM_Floors { get; set; }

    public virtual DbSet<Sys_REM_tower> Sys_REM_towers { get; set; }

    public virtual DbSet<TR_API_Log> TR_API_Logs { get; set; }

    public virtual DbSet<TR_AnswerC> TR_AnswerCs { get; set; }

    public virtual DbSet<TR_Appointment> TR_Appointments { get; set; }

    public virtual DbSet<TR_AttachFileQC> TR_AttachFileQCs { get; set; }

    public virtual DbSet<TR_CompanyProject> TR_CompanyProjects { get; set; }

    public virtual DbSet<TR_ContactLog> TR_ContactLogs { get; set; }

    public virtual DbSet<TR_CustomerSatisfaction> TR_CustomerSatisfactions { get; set; }

    public virtual DbSet<TR_CustomerSatisfaction_Detail> TR_CustomerSatisfaction_Details { get; set; }

    public virtual DbSet<TR_DefectHistory> TR_DefectHistories { get; set; }

    public virtual DbSet<TR_DefectTypeVendor_Mapping> TR_DefectTypeVendor_Mappings { get; set; }

    public virtual DbSet<TR_DefectVendor> TR_DefectVendors { get; set; }

    public virtual DbSet<TR_DeviceSignIn> TR_DeviceSignIns { get; set; }

    public virtual DbSet<TR_Event_EventType> TR_Event_EventTypes { get; set; }

    public virtual DbSet<TR_Letter> TR_Letters { get; set; }

    public virtual DbSet<TR_Letter_Attach> TR_Letter_Attaches { get; set; }

    public virtual DbSet<TR_Letter_C> TR_Letter_Cs { get; set; }

    public virtual DbSet<TR_Letter_Lot> TR_Letter_Lots { get; set; }

    public virtual DbSet<TR_Letter_Lot_Detail> TR_Letter_Lot_Details { get; set; }

    public virtual DbSet<TR_Letter_Lot_Resource> TR_Letter_Lot_Resources { get; set; }

    public virtual DbSet<TR_MenuRolePermission> TR_MenuRolePermissions { get; set; }

    public virtual DbSet<TR_PowerOfAttorney> TR_PowerOfAttorneys { get; set; }

    public virtual DbSet<TR_ProjectAppointLimit_Mapping> TR_ProjectAppointLimit_Mappings { get; set; }

    public virtual DbSet<TR_ProjectCounter_Mapping> TR_ProjectCounter_Mappings { get; set; }

    public virtual DbSet<TR_ProjectEmail_Mapping> TR_ProjectEmail_Mappings { get; set; }

    public virtual DbSet<TR_ProjectEvent> TR_ProjectEvents { get; set; }

    public virtual DbSet<TR_ProjectFloorPlan> TR_ProjectFloorPlans { get; set; }

    public virtual DbSet<TR_ProjectLandOffice> TR_ProjectLandOffices { get; set; }

    public virtual DbSet<TR_ProjectShopEvent> TR_ProjectShopEvents { get; set; }

    public virtual DbSet<TR_ProjectStatus> TR_ProjectStatuses { get; set; }

    public virtual DbSet<TR_ProjectUnitFloorPlan> TR_ProjectUnitFloorPlans { get; set; }

    public virtual DbSet<TR_ProjectUserSign> TR_ProjectUserSigns { get; set; }

    public virtual DbSet<TR_ProjectZone_Mapping> TR_ProjectZone_Mappings { get; set; }

    public virtual DbSet<TR_QC1> TR_QC1s { get; set; }

    public virtual DbSet<TR_QC2> TR_QC2s { get; set; }

    public virtual DbSet<TR_QC3> TR_QC3s { get; set; }

    public virtual DbSet<TR_QC4> TR_QC4s { get; set; }

    public virtual DbSet<TR_QC5> TR_QC5s { get; set; }

    public virtual DbSet<TR_QC5_CheckList> TR_QC5_CheckLists { get; set; }

    public virtual DbSet<TR_QC5_CheckList_Detail> TR_QC5_CheckList_Details { get; set; }

    public virtual DbSet<TR_QC5_FinishPlan> TR_QC5_FinishPlans { get; set; }

    public virtual DbSet<TR_QC5_Open> TR_QC5_Opens { get; set; }

    public virtual DbSet<TR_QC5_ProjectSendMail> TR_QC5_ProjectSendMails { get; set; }

    public virtual DbSet<TR_QC6> TR_QC6s { get; set; }

    public virtual DbSet<TR_QC6_ProjectSendMail> TR_QC6_ProjectSendMails { get; set; }

    public virtual DbSet<TR_QC6_Unsold> TR_QC6_Unsolds { get; set; }

    public virtual DbSet<TR_QC6_Unsold_SendMail> TR_QC6_Unsold_SendMails { get; set; }

    public virtual DbSet<TR_QC_CheckList> TR_QC_CheckLists { get; set; }

    public virtual DbSet<TR_QC_ContactLogResource> TR_QC_ContactLogResources { get; set; }

    public virtual DbSet<TR_QC_Defect> TR_QC_Defects { get; set; }

    public virtual DbSet<TR_QC_DefectResource> TR_QC_DefectResources { get; set; }

    public virtual DbSet<TR_QC_Defect_Draft> TR_QC_Defect_Drafts { get; set; }

    public virtual DbSet<TR_QC_Defect_OverDueExpect> TR_QC_Defect_OverDueExpects { get; set; }

    public virtual DbSet<TR_QC_Defect_OverDueExpect_BK> TR_QC_Defect_OverDueExpect_BKs { get; set; }

    public virtual DbSet<TR_QC_Defect_OverDueExpect_UserPermission> TR_QC_Defect_OverDueExpect_UserPermissions { get; set; }

    public virtual DbSet<TR_QuestionAnswer> TR_QuestionAnswers { get; set; }

    public virtual DbSet<TR_QuestionC> TR_QuestionCs { get; set; }

    public virtual DbSet<TR_ReceiveRoomAgreementSign> TR_ReceiveRoomAgreementSigns { get; set; }

    public virtual DbSet<TR_ReceiveRoomSign> TR_ReceiveRoomSigns { get; set; }

    public virtual DbSet<TR_RegisterBank> TR_RegisterBanks { get; set; }

    public virtual DbSet<TR_RegisterLog> TR_RegisterLogs { get; set; }

    public virtual DbSet<TR_Register_BankCounter> TR_Register_BankCounters { get; set; }

    public virtual DbSet<TR_Register_CallStaffCounter> TR_Register_CallStaffCounters { get; set; }

    public virtual DbSet<TR_Register_ProjectBankStaff> TR_Register_ProjectBankStaffs { get; set; }

    public virtual DbSet<TR_RemarkUnitStatus_Mapping> TR_RemarkUnitStatus_Mappings { get; set; }

    public virtual DbSet<TR_Resource> TR_Resources { get; set; }

    public virtual DbSet<TR_ReviseUnitPromotion> TR_ReviseUnitPromotions { get; set; }

    public virtual DbSet<TR_ReviseUnitPromotion_Detail> TR_ReviseUnitPromotion_Details { get; set; }

    public virtual DbSet<TR_ReviseUnitPromoton_Approver> TR_ReviseUnitPromoton_Approvers { get; set; }

    public virtual DbSet<TR_SignResource> TR_SignResources { get; set; }

    public virtual DbSet<TR_Sync> TR_Syncs { get; set; }

    public virtual DbSet<TR_Sync_LoanBank> TR_Sync_LoanBanks { get; set; }

    public virtual DbSet<TR_Sync_Log> TR_Sync_Logs { get; set; }

    public virtual DbSet<TR_Sync_QC> TR_Sync_QCs { get; set; }

    public virtual DbSet<TR_TagEvent> TR_TagEvents { get; set; }

    public virtual DbSet<TR_TargetRollingPlan> TR_TargetRollingPlans { get; set; }

    public virtual DbSet<TR_TargetRollingPlan_BK> TR_TargetRollingPlan_BKs { get; set; }

    public virtual DbSet<TR_TerminateTransferAppoint> TR_TerminateTransferAppoints { get; set; }

    public virtual DbSet<TR_TerminateTransferAppoint_Document> TR_TerminateTransferAppoint_Documents { get; set; }

    public virtual DbSet<TR_TransferDocument> TR_TransferDocuments { get; set; }

    public virtual DbSet<TR_UnitDocument> TR_UnitDocuments { get; set; }

    public virtual DbSet<TR_UnitDocumentNo> TR_UnitDocumentNos { get; set; }

    public virtual DbSet<TR_UnitEquipment> TR_UnitEquipments { get; set; }

    public virtual DbSet<TR_UnitEquipment_Detail> TR_UnitEquipment_Details { get; set; }

    public virtual DbSet<TR_UnitEvent> TR_UnitEvents { get; set; }

    public virtual DbSet<TR_UnitFurniture> TR_UnitFurnitures { get; set; }

    public virtual DbSet<TR_UnitFurniture_Detail> TR_UnitFurniture_Details { get; set; }

    public virtual DbSet<TR_UnitPromotion> TR_UnitPromotions { get; set; }

    public virtual DbSet<TR_UnitPromotionSign> TR_UnitPromotionSigns { get; set; }

    public virtual DbSet<TR_UnitPromotionSign_Detail> TR_UnitPromotionSign_Details { get; set; }

    public virtual DbSet<TR_UnitShopEvent> TR_UnitShopEvents { get; set; }

    public virtual DbSet<TR_UnitUser_Mapping> TR_UnitUser_Mappings { get; set; }

    public virtual DbSet<TR_Unsold_Round> TR_Unsold_Rounds { get; set; }

    public virtual DbSet<TR_Unsold_RoundUnit> TR_Unsold_RoundUnits { get; set; }

    public virtual DbSet<TR_UserPosition> TR_UserPositions { get; set; }

    public virtual DbSet<TR_UserSignResource> TR_UserSignResources { get; set; }

    public virtual DbSet<TmpBlkUnit> TmpBlkUnits { get; set; }

    public virtual DbSet<Unit_FIXDone> Unit_FIXDones { get; set; }

    public virtual DbSet<Z_M1111> Z_M1111s { get; set; }

    public virtual DbSet<Z_W1111> Z_W1111s { get; set; }

    public virtual DbSet<Z_importUnit> Z_importUnits { get; set; }

    public virtual DbSet<contactlog_PPC001> contactlog_PPC001s { get; set; }

    public virtual DbSet<contactlog_TUC002> contactlog_TUC002s { get; set; }

    public virtual DbSet<defect_type_20211105> defect_type_20211105s { get; set; }

    public virtual DbSet<question_tuc002> question_tuc002s { get; set; }

    public virtual DbSet<revise_Area> revise_Areas { get; set; }

    public virtual DbSet<revise_DefectDesc> revise_DefectDescs { get; set; }

    public virtual DbSet<revise_DefectType> revise_DefectTypes { get; set; }

    public virtual DbSet<temp_ABH003> temp_ABH003s { get; set; }

    public virtual DbSet<temp_AT15> temp_AT15s { get; set; }

    public virtual DbSet<temp_AT71> temp_AT71s { get; set; }

    public virtual DbSet<temp_ATCHANG> temp_ATCHANGs { get; set; }

    public virtual DbSet<temp_ATHK> temp_ATHKs { get; set; }

    public virtual DbSet<temp_AVA> temp_AVAs { get; set; }

    public virtual DbSet<temp_BR67> temp_BR67s { get; set; }

    public virtual DbSet<temp_CAM002> temp_CAM002s { get; set; }

    public virtual DbSet<temp_CBR002> temp_CBR002s { get; set; }

    public virtual DbSet<temp_CBR003> temp_CBR003s { get; set; }

    public virtual DbSet<temp_CMD004> temp_CMD004s { get; set; }

    public virtual DbSet<temp_CMD005> temp_CMD005s { get; set; }

    public virtual DbSet<temp_EQC018> temp_EQC018s { get; set; }

    public virtual DbSet<temp_EQC019> temp_EQC019s { get; set; }

    public virtual DbSet<temp_EQC020> temp_EQC020s { get; set; }

    public virtual DbSet<temp_EQC022> temp_EQC022s { get; set; }

    public virtual DbSet<temp_EQC025> temp_EQC025s { get; set; }

    public virtual DbSet<temp_KAVESHIFT> temp_KAVESHIFTs { get; set; }

    public virtual DbSet<temp_KAVESPACE> temp_KAVESPACEs { get; set; }

    public virtual DbSet<temp_M1C001> temp_M1C001s { get; set; }

    public virtual DbSet<temp_PPC001> temp_PPC001s { get; set; }

    public virtual DbSet<temp_TUC001> temp_TUC001s { get; set; }

    public virtual DbSet<temp_TUC002> temp_TUC002s { get; set; }

    public virtual DbSet<temp_checker> temp_checkers { get; set; }

    public virtual DbSet<temp_cs_response> temp_cs_responses { get; set; }

    public virtual DbSet<temp_cust_sat_20240104> temp_cust_sat_20240104s { get; set; }

    public virtual DbSet<temp_defect> temp_defects { get; set; }

    public virtual DbSet<temp_jenie_unit> temp_jenie_units { get; set; }

    public virtual DbSet<temp_letter> temp_letters { get; set; }

    public virtual DbSet<temp_modiz> temp_modizs { get; set; }

    public virtual DbSet<temp_new_bu> temp_new_bus { get; set; }

    public virtual DbSet<temp_unit> temp_units { get; set; }

    public virtual DbSet<temp_unit_cmd005> temp_unit_cmd005s { get; set; }

    public virtual DbSet<temp_unit_maxxi> temp_unit_maxxis { get; set; }

    public virtual DbSet<tm_Answer> tm_Answers { get; set; }

    public virtual DbSet<tm_BU> tm_BUs { get; set; }

    public virtual DbSet<tm_BUProject_Mapping> tm_BUProject_Mappings { get; set; }

    public virtual DbSet<tm_BU_Mapping> tm_BU_Mappings { get; set; }

    public virtual DbSet<tm_Bank> tm_Banks { get; set; }

    public virtual DbSet<tm_CloseProject> tm_CloseProjects { get; set; }

    public virtual DbSet<tm_Company> tm_Companies { get; set; }

    public virtual DbSet<tm_DataSource> tm_DataSources { get; set; }

    public virtual DbSet<tm_DefectArea> tm_DefectAreas { get; set; }

    public virtual DbSet<tm_DefectAreaType_Mapping> tm_DefectAreaType_Mappings { get; set; }

    public virtual DbSet<tm_DefectDescription> tm_DefectDescriptions { get; set; }

    public virtual DbSet<tm_DefectType> tm_DefectTypes { get; set; }

    public virtual DbSet<tm_Equipment> tm_Equipments { get; set; }

    public virtual DbSet<tm_Event> tm_Events { get; set; }

    public virtual DbSet<tm_EventType> tm_EventTypes { get; set; }

    public virtual DbSet<tm_Ext> tm_Exts { get; set; }

    public virtual DbSet<tm_ExtType> tm_ExtTypes { get; set; }

    public virtual DbSet<tm_Funiture> tm_Funitures { get; set; }

    public virtual DbSet<tm_Holiday> tm_Holidays { get; set; }

    public virtual DbSet<tm_LandOffice> tm_LandOffices { get; set; }

    public virtual DbSet<tm_LetterDayDue> tm_LetterDayDues { get; set; }

    public virtual DbSet<tm_LetterSendReason> tm_LetterSendReasons { get; set; }

    public virtual DbSet<tm_LineToken> tm_LineTokens { get; set; }

    public virtual DbSet<tm_Menu> tm_Menus { get; set; }

    public virtual DbSet<tm_MenuAction> tm_MenuActions { get; set; }

    public virtual DbSet<tm_Position> tm_Positions { get; set; }

    public virtual DbSet<tm_Project> tm_Projects { get; set; }

    public virtual DbSet<tm_ProjectUser_Mapping> tm_ProjectUser_Mappings { get; set; }

    public virtual DbSet<tm_Promotion> tm_Promotions { get; set; }

    public virtual DbSet<tm_QC5_CheckList> tm_QC5_CheckLists { get; set; }

    public virtual DbSet<tm_Question> tm_Questions { get; set; }

    public virtual DbSet<tm_Responsible_Mapping> tm_Responsible_Mappings { get; set; }

    public virtual DbSet<tm_Role> tm_Roles { get; set; }

    public virtual DbSet<tm_Shop> tm_Shops { get; set; }

    public virtual DbSet<tm_Subject> tm_Subjects { get; set; }

    public virtual DbSet<tm_Tag> tm_Tags { get; set; }

    public virtual DbSet<tm_TitleName> tm_TitleNames { get; set; }

    public virtual DbSet<tm_Topic> tm_Topics { get; set; }

    public virtual DbSet<tm_Unit> tm_Units { get; set; }

    public virtual DbSet<tm_UnitStatus> tm_UnitStatuses { get; set; }

    public virtual DbSet<tm_Unit_20190614> tm_Unit_20190614s { get; set; }

    public virtual DbSet<tm_Unit_20210510> tm_Unit_20210510s { get; set; }

    public virtual DbSet<tm_Unit_BK02102018> tm_Unit_BK02102018s { get; set; }

    public virtual DbSet<tm_Unit_BK11062018> tm_Unit_BK11062018s { get; set; }

    public virtual DbSet<tm_Unit_BK30062018> tm_Unit_BK30062018s { get; set; }

    public virtual DbSet<tm_Unit_BK31082018> tm_Unit_BK31082018s { get; set; }

    public virtual DbSet<tm_Unit_CAM003_20191223> tm_Unit_CAM003_20191223s { get; set; }

    public virtual DbSet<tm_User> tm_Users { get; set; }

    public virtual DbSet<tm_Vendor> tm_Vendors { get; set; }

    public virtual DbSet<tm_WIPMatrix> tm_WIPMatrices { get; set; }

    public virtual DbSet<tm_WIPMatrix_QC6> tm_WIPMatrix_QC6s { get; set; }

    public virtual DbSet<vw_BI_Backlog> vw_BI_Backlogs { get; set; }

    public virtual DbSet<vw_BI_CRM_CashbackLTV> vw_BI_CRM_CashbackLTVs { get; set; }

    public virtual DbSet<vw_BI_Project> vw_BI_Projects { get; set; }

    public virtual DbSet<vw_BI_Project_Initial_Month> vw_BI_Project_Initial_Months { get; set; }

    public virtual DbSet<vw_BI_Transfer_Actual> vw_BI_Transfer_Actuals { get; set; }

    public virtual DbSet<vw_BI_Transfer_NetActual> vw_BI_Transfer_NetActuals { get; set; }

    public virtual DbSet<vw_BI_Transfer_TargetRolling> vw_BI_Transfer_TargetRollings { get; set; }

    public virtual DbSet<vw_BI_Transfer_TargetRollingActual> vw_BI_Transfer_TargetRollingActuals { get; set; }

    public virtual DbSet<vw_Defect> vw_Defects { get; set; }

    public virtual DbSet<vw_ITF_TempBlakUnit> vw_ITF_TempBlakUnits { get; set; }

    public virtual DbSet<vw_Unit> vw_Units { get; set; }

    public virtual DbSet<vw_UnitMatrix> vw_UnitMatrices { get; set; }

    public virtual DbSet<vw_UnitMatrix_QCProgress> vw_UnitMatrix_QCProgresses { get; set; }

    public virtual DbSet<vw_UnitMatrix_QCProgress_V2> vw_UnitMatrix_QCProgress_V2s { get; set; }

    public virtual DbSet<vw_getRANDValue> vw_getRANDValues { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Thai_CI_AS");

        modelBuilder.Entity<BM_Master_Bank>(entity =>
        {
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Bank).WithMany(p => p.BM_Master_Banks).HasConstraintName("FK_BM_Master_Bank_tm_Bank");
        });

        modelBuilder.Entity<BM_Master_Question>(entity =>
        {
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<BM_Master_QuestionAnswer>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_BM_Master_Answer");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Question).WithMany(p => p.BM_Master_QuestionAnswers).HasConstraintName("FK_BM_Master_QuestionAnswer_BM_Master_Question");
        });

        modelBuilder.Entity<BM_Master_ScoreType>(entity =>
        {
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<BM_Master_Set>(entity =>
        {
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<BM_Master_Set_QuestionAnswer>(entity =>
        {
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Answer).WithMany(p => p.BM_Master_Set_QuestionAnswers).HasConstraintName("FK_BM_Master_Set_QuestionAnswer_BM_Master_QuestionAnswer");

            entity.HasOne(d => d.Question).WithMany(p => p.BM_Master_Set_QuestionAnswers).HasConstraintName("FK_BM_Master_Set_QuestionAnswer_BM_Master_Question");

            entity.HasOne(d => d.Set).WithMany(p => p.BM_Master_Set_QuestionAnswers).HasConstraintName("FK_BM_Master_Set_QuestionAnswer_BM_Master_Set");
        });

        modelBuilder.Entity<BM_TR_LoanAgeRate_Bank>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_BM_TR_LoanBank_Calculate");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Bank).WithMany(p => p.BM_TR_LoanAgeRate_Banks).HasConstraintName("FK_BM_TR_LoanAgeRate_Bank_tm_Bank");
        });

        modelBuilder.Entity<BM_TR_QuestionAnswerScore>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_BM_TR_BankScore");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Answer).WithMany(p => p.BM_TR_QuestionAnswerScores).HasConstraintName("FK_BM_TR_BankScore_BM_Master_QuestionAnswer");

            entity.HasOne(d => d.Bank).WithMany(p => p.BM_TR_QuestionAnswerScores).HasConstraintName("FK_BM_TR_BankScore_tm_Bank");

            entity.HasOne(d => d.Question).WithMany(p => p.BM_TR_QuestionAnswerScores).HasConstraintName("FK_BM_TR_BankScore_BM_Master_Question");

            entity.HasOne(d => d.ScoreType).WithMany(p => p.BM_TR_QuestionAnswerScores).HasConstraintName("FK_BM_TR_BankScore_BM_Master_ScoreType");
        });

        modelBuilder.Entity<BM_TR_Set_Score>(entity =>
        {
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Bank).WithMany(p => p.BM_TR_Set_Scores).HasConstraintName("FK_BM_TR_Set_Score_tm_Bank");

            entity.HasOne(d => d.ScoreType).WithMany(p => p.BM_TR_Set_Scores).HasConstraintName("FK_BM_TR_Set_Score_BM_Master_ScoreType");

            entity.HasOne(d => d.Set).WithMany(p => p.BM_TR_Set_Scores).HasConstraintName("FK_BM_TR_Set_Score_BM_Master_Set");
        });

        modelBuilder.Entity<BM_TR_UserAdmin>(entity =>
        {
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.User).WithMany(p => p.BM_TR_UserAdmins).HasConstraintName("FK_BM_TR_UserAdmin_tm_User");
        });

        modelBuilder.Entity<BM_TS_Matching>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<BM_TS_Matching_Detail>(entity =>
        {
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Bank).WithMany(p => p.BM_TS_Matching_Details).HasConstraintName("FK_BM_TS_Matching_Detail_tm_Bank");

            entity.HasOne(d => d.Matching).WithMany(p => p.BM_TS_Matching_Details).HasConstraintName("FK_BM_TS_Matching_Detail_BM_TS_Matching");
        });

        modelBuilder.Entity<BM_TS_Matching_QuestionAnswer>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_TS_BM_Matching_QuestionAnswer");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Answer).WithMany(p => p.BM_TS_Matching_QuestionAnswers).HasConstraintName("FK_BM_TS_Matching_QuestionAnswer_BM_Master_QuestionAnswer");

            entity.HasOne(d => d.Bank).WithMany(p => p.BM_TS_Matching_QuestionAnswers).HasConstraintName("FK_BM_TS_Matching_QuestionAnswer_tm_Bank");

            entity.HasOne(d => d.Matching).WithMany(p => p.BM_TS_Matching_QuestionAnswers).HasConstraintName("FK_BM_TS_Matching_QuestionAnswer_BM_TS_Matching");

            entity.HasOne(d => d.Question).WithMany(p => p.BM_TS_Matching_QuestionAnswers).HasConstraintName("FK_BM_TS_Matching_QuestionAnswer_BM_Master_Question");
        });

        modelBuilder.Entity<BM_TS_Matching_ScoreSet>(entity =>
        {
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Bank).WithMany(p => p.BM_TS_Matching_ScoreSets).HasConstraintName("FK_BM_TS_Matching_ScoreSet_tm_Bank");

            entity.HasOne(d => d.Matching).WithMany(p => p.BM_TS_Matching_ScoreSets).HasConstraintName("FK_BM_TS_Matching_ScoreSet_BM_TS_Matching");

            entity.HasOne(d => d.Set).WithMany(p => p.BM_TS_Matching_ScoreSets).HasConstraintName("FK_BM_TS_Matching_ScoreSet_BM_Master_Set");
        });

        modelBuilder.Entity<BM_TS_Matching_ScoreSet_Detail>(entity =>
        {
            entity.HasOne(d => d.Answer).WithMany(p => p.BM_TS_Matching_ScoreSet_Details).HasConstraintName("FK_BM_TS_Matching_ScoreSet_Detail_BM_Master_QuestionAnswer");

            entity.HasOne(d => d.Matching).WithMany(p => p.BM_TS_Matching_ScoreSet_Details).HasConstraintName("FK_BM_TS_Matching_ScoreSet_Detail_BM_TS_Matching");

            entity.HasOne(d => d.Question).WithMany(p => p.BM_TS_Matching_ScoreSet_Details).HasConstraintName("FK_BM_TS_Matching_ScoreSet_Detail_BM_Master_Question");

            entity.HasOne(d => d.Set).WithMany(p => p.BM_TS_Matching_ScoreSet_Details).HasConstraintName("FK_BM_TS_Matching_ScoreSet_Detail_BM_Master_Set");
        });

        modelBuilder.Entity<Line_PostLog>(entity =>
        {
            entity.Property(e => e.id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Line_QRCode>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_Line_Code");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Line_Register>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.RegisterDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Line_RegisterQRCode>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_Line_RegisterCode");

            entity.HasOne(d => d.QRCode).WithMany(p => p.Line_RegisterQRCodes).HasConstraintName("FK_Line_RegisterQRCode_Line_QRCode");

            entity.HasOne(d => d.Register).WithMany(p => p.Line_RegisterQRCodes).HasConstraintName("FK_Line_RegisterQRCode_Line_Register");
        });

        modelBuilder.Entity<Line_UserAppointment>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_Line_Appointment");

            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.HasOne(d => d.Appointment).WithMany(p => p.Line_UserAppointments).HasConstraintName("FK_Line_Appointment_TR_Appointment");
        });

        modelBuilder.Entity<Line_UserContract>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.HasOne(d => d.LineUser).WithMany(p => p.Line_UserContracts).HasConstraintName("FK_Line_UserContracts_Line_User");

            entity.HasOne(d => d.Project).WithMany(p => p.Line_UserContracts).HasConstraintName("FK_Line_UserContracts_tm_Project");

            entity.HasOne(d => d.Unit).WithMany(p => p.Line_UserContracts).HasConstraintName("FK_Line_UserContracts_tm_Unit");
        });

        modelBuilder.Entity<PR_AttachFile>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.AttachType).WithMany(p => p.PR_AttachFileAttachTypes).HasConstraintName("FK_PR_AttachFile_tm_Ext1");

            entity.HasOne(d => d.UserType).WithMany(p => p.PR_AttachFileUserTypes).HasConstraintName("FK_PR_AttachFile_tm_Ext");
        });

        modelBuilder.Entity<PR_BankDocument>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Bank).WithMany(p => p.PR_BankDocuments).HasConstraintName("FK_PR_BankDocument_tm_Bank");
        });

        modelBuilder.Entity<PR_ContractVerify>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
        });

        modelBuilder.Entity<PR_Customer>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
        });

        modelBuilder.Entity<PR_CustomerCareer>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.HasOne(d => d.Career).WithMany(p => p.PR_CustomerCareers).HasConstraintName("FK_PR_CustomerCareer_tm_Ext");

            entity.HasOne(d => d.Customer).WithMany(p => p.PR_CustomerCareers).HasConstraintName("FK_PR_CustomerCareer_PR_Customer");

            entity.HasOne(d => d.Loan).WithMany(p => p.PR_CustomerCareers).HasConstraintName("FK_PR_CustomerCareer_PR_Loan");
        });

        modelBuilder.Entity<PR_CustomerDebt>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.HasOne(d => d.Customer).WithMany(p => p.PR_CustomerDebts).HasConstraintName("FK_PR_CustomerDebt_PR_Customer");

            entity.HasOne(d => d.Debt).WithMany(p => p.PR_CustomerDebts).HasConstraintName("FK_PR_CustomerDebt_tm_Ext");

            entity.HasOne(d => d.Loan).WithMany(p => p.PR_CustomerDebts).HasConstraintName("FK_PR_CustomerDebt_PR_Loan");
        });

        modelBuilder.Entity<PR_CustomerIncome>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.HasOne(d => d.Customer).WithMany(p => p.PR_CustomerIncomes).HasConstraintName("FK_PR_CustomerIncome_PR_Customer");

            entity.HasOne(d => d.Income).WithMany(p => p.PR_CustomerIncomes).HasConstraintName("FK_PR_CustomerIncome_tm_Ext");

            entity.HasOne(d => d.Loan).WithMany(p => p.PR_CustomerIncomes).HasConstraintName("FK_PR_CustomerIncome_PR_Loan");
        });

        modelBuilder.Entity<PR_Loan>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.HasOne(d => d.Project).WithMany(p => p.PR_Loans).HasConstraintName("FK_PR_Loan_tm_Project");

            entity.HasOne(d => d.UserType).WithMany(p => p.PR_Loans).HasConstraintName("FK_PR_Loan_tm_Ext");
        });

        modelBuilder.Entity<PR_LoanBank>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<PR_LoanBankAttachFile>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.LoanBank).WithMany(p => p.PR_LoanBankAttachFiles).HasConstraintName("FK_PR_LoanBankAttachFile_PR_LoanBank");

            entity.HasOne(d => d.Loan).WithMany(p => p.PR_LoanBankAttachFiles).HasConstraintName("FK_PR_LoanBankAttachFile_PR_Loan");
        });

        modelBuilder.Entity<PR_LoanBank_Explain>(entity =>
        {
            entity.Property(e => e.LoanBankID).ValueGeneratedNever();

            entity.HasOne(d => d.LoanBank).WithOne(p => p.PR_LoanBank_Explain)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PR_LoanBank_Explain_PR_LoanBank");
        });

        modelBuilder.Entity<PR_LoanCustomer>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.HasOne(d => d.Customer).WithMany(p => p.PR_LoanCustomers).HasConstraintName("FK_PR_LoanCustomer_PR_Customer");

            entity.HasOne(d => d.Loan).WithMany(p => p.PR_LoanCustomers).HasConstraintName("FK_PR_LoanCustomer_PR_Loan");
        });

        modelBuilder.Entity<PR_LoanCustomerAttach>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.AttachFile).WithMany(p => p.PR_LoanCustomerAttaches).HasConstraintName("FK_PR_LoanCustomerAttach_PR_AttachFile");

            entity.HasOne(d => d.Customer).WithMany(p => p.PR_LoanCustomerAttaches).HasConstraintName("FK_PR_LoanCustomerAttach_PR_Customer");

            entity.HasOne(d => d.Loan).WithMany(p => p.PR_LoanCustomerAttaches).HasConstraintName("FK_PR_LoanCustomerAttach_PR_Loan");
        });

        modelBuilder.Entity<PR_Log>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
        });

        modelBuilder.Entity<PR_ProjectCS_Mapping>(entity =>
        {
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<PR_User>(entity =>
        {
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.UserType).WithMany(p => p.PR_Users).HasConstraintName("FK_PR_User_tm_Ext");
        });

        modelBuilder.Entity<PR_UserBank_Mapping>(entity =>
        {
            entity.HasOne(d => d.Bank).WithMany(p => p.PR_UserBank_Mappings).HasConstraintName("FK_PR_UserBamk_Mapping_tm_Bank");

            entity.HasOne(d => d.User).WithMany(p => p.PR_UserBank_Mappings).HasConstraintName("FK_PR_UserBamk_Mapping_PR_User");
        });

        modelBuilder.Entity<PR_User_PasswordChange>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_PR_PasswordChange");

            entity.HasOne(d => d.User).WithMany(p => p.PR_User_PasswordChanges).HasConstraintName("FK_PR_User_PasswordChange_PR_User");
        });

        modelBuilder.Entity<TR_API_Log>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
        });

        modelBuilder.Entity<TR_AnswerC>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Question).WithMany(p => p.TR_AnswerCs).HasConstraintName("FK_TR_AnswerCS_TR_QuestionCS");
        });

        modelBuilder.Entity<TR_Appointment>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Project).WithMany(p => p.TR_Appointments).HasConstraintName("FK_TR_Appointment_tm_Project");

            entity.HasOne(d => d.Unit).WithMany(p => p.TR_Appointments).HasConstraintName("FK_TR_Appointment_tm_Unit");
        });

        modelBuilder.Entity<TR_AttachFileQC>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Project).WithMany(p => p.TR_AttachFileQCs).HasConstraintName("FK_TR_AttachFileQC_tm_Project");

            entity.HasOne(d => d.QCType).WithMany(p => p.TR_AttachFileQCs).HasConstraintName("FK_TR_AttachFileQC_tm_Ext");

            entity.HasOne(d => d.Unit).WithMany(p => p.TR_AttachFileQCs).HasConstraintName("FK_TR_AttachFileQC_tm_Unit");
        });

        modelBuilder.Entity<TR_CompanyProject>(entity =>
        {
            entity.HasOne(d => d.Company).WithMany(p => p.TR_CompanyProjects).HasConstraintName("FK_TR_CompanyProject_tm_Company");

            entity.HasOne(d => d.Project).WithMany(p => p.TR_CompanyProjects).HasConstraintName("FK_TR_CompanyProject_tm_Project");
        });

        modelBuilder.Entity<TR_ContactLog>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Bank).WithMany(p => p.TR_ContactLogs).HasConstraintName("FK_TR_ContactLog_tm_Bank");

            entity.HasOne(d => d.ContactReason).WithMany(p => p.TR_ContactLogContactReasons).HasConstraintName("FK_TR_ContactLog_tm_Ext3");

            entity.HasOne(d => d.CustomerType).WithMany(p => p.TR_ContactLogCustomerTypes).HasConstraintName("FK_TR_ContactLog_tm_Ext2");

            entity.HasOne(d => d.Project).WithMany(p => p.TR_ContactLogs).HasConstraintName("FK_TR_ContactLog_tm_Project");

            entity.HasOne(d => d.QCType).WithMany(p => p.TR_ContactLogQCTypes).HasConstraintName("FK_TR_ContactLog_tm_Ext1");

            entity.HasOne(d => d.QC).WithMany(p => p.TR_ContactLogs).HasConstraintName("FK_TR_ContactLog_TR_QC6");

            entity.HasOne(d => d.Team).WithMany(p => p.TR_ContactLogTeams).HasConstraintName("FK_TR_ContactLog_tm_Ext");

            entity.HasOne(d => d.Unit).WithMany(p => p.TR_ContactLogs).HasConstraintName("FK_TR_ContactLog_tm_Unit");
        });

        modelBuilder.Entity<TR_CustomerSatisfaction>(entity =>
        {
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Project).WithMany(p => p.TR_CustomerSatisfactions).HasConstraintName("FK_TR_CustomerSatisfaction_tm_Project");

            entity.HasOne(d => d.QuestionType).WithMany(p => p.TR_CustomerSatisfactions).HasConstraintName("FK_TR_CustomerSatisfaction_tm_Ext");

            entity.HasOne(d => d.User).WithMany(p => p.TR_CustomerSatisfactions).HasConstraintName("FK_TR_CustomerSatisfaction_tm_User");
        });

        modelBuilder.Entity<TR_CustomerSatisfaction_Detail>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_TR_CustomerSatisfactionDetail");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Answer).WithMany(p => p.TR_CustomerSatisfaction_Details).HasConstraintName("FK_TR_CustomerSatisfaction_Detail_TR_AnswerCS");

            entity.HasOne(d => d.CustomerSatisfaction).WithMany(p => p.TR_CustomerSatisfaction_Details).HasConstraintName("FK_TR_CustomerSatisfaction_Detail_TR_CustomerSatisfaction");

            entity.HasOne(d => d.Question).WithMany(p => p.TR_CustomerSatisfaction_Details).HasConstraintName("FK_TR_CustomerSatisfaction_Detail_TR_QuestionCS");
        });

        modelBuilder.Entity<TR_DefectTypeVendor_Mapping>(entity =>
        {
            entity.HasOne(d => d.DefectType).WithMany(p => p.TR_DefectTypeVendor_Mappings).HasConstraintName("FK_TR_DefectTypeVendor_Mapping_tm_DefectType");
        });

        modelBuilder.Entity<TR_DefectVendor>(entity =>
        {
            entity.Property(e => e.DefectID).ValueGeneratedNever();
        });

        modelBuilder.Entity<TR_DeviceSignIn>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_tr_DeviceSignIn");

            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.HasOne(d => d.User).WithMany(p => p.TR_DeviceSignIns).HasConstraintName("FK_TR_DeviceSignIn_tm_User");
        });

        modelBuilder.Entity<TR_Event_EventType>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__TR_Event__3214EC27F35C3936");

            entity.HasOne(d => d.Event).WithMany(p => p.TR_Event_EventTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Event");

            entity.HasOne(d => d.EventType).WithMany(p => p.TR_Event_EventTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventType");
        });

        modelBuilder.Entity<TR_Letter>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_TR_Letter_Detail");

            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.HasOne(d => d.ApproveSign).WithMany(p => p.TR_Letters).HasConstraintName("ApproveSign");

            entity.HasOne(d => d.ApproveStatus).WithMany(p => p.TR_LetterApproveStatuses).HasConstraintName("ApproveStatus");

            entity.HasOne(d => d.LetterReference).WithMany(p => p.InverseLetterReference).HasConstraintName("LetterReference");

            entity.HasOne(d => d.LetterType).WithMany(p => p.TR_LetterLetterTypes).HasConstraintName("LetterType");

            entity.HasOne(d => d.Project).WithMany(p => p.TR_Letters).HasConstraintName("Project");

            entity.HasOne(d => d.SendReason).WithMany(p => p.TR_Letters).HasConstraintName("SendReason");

            entity.HasOne(d => d.SendStatus).WithMany(p => p.TR_LetterSendStatuses).HasConstraintName("SendStatus");

            entity.HasOne(d => d.SendType).WithMany(p => p.TR_LetterSendTypes).HasConstraintName("SendType");

            entity.HasOne(d => d.Unit).WithMany(p => p.TR_Letters).HasConstraintName("Unit");

            entity.HasOne(d => d.VerifyStatus).WithMany(p => p.TR_LetterVerifyStatuses).HasConstraintName("VerifyStatus");
        });

        modelBuilder.Entity<TR_Letter_Attach>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_TR_Letter_Detail_Attach");

            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Letter).WithMany(p => p.TR_Letter_Attaches).HasConstraintName("FK_TR_Letter_Attach_TR_Letter");
        });

        modelBuilder.Entity<TR_Letter_C>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.HasOne(d => d.Event).WithMany(p => p.TR_Letter_Cs).HasConstraintName("FK_TR_Letter_CS_tm_Event");

            entity.HasOne(d => d.SignUser).WithMany(p => p.TR_Letter_Cs).HasConstraintName("FK_TR_Letter_CS_tm_User");
        });

        modelBuilder.Entity<TR_Letter_Lot>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
        });

        modelBuilder.Entity<TR_Letter_Lot_Detail>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.HasOne(d => d.Letter).WithMany(p => p.TR_Letter_Lot_Details).HasConstraintName("FK_TR_Letter_Lot_Detail_TR_Letter");

            entity.HasOne(d => d.Lot).WithMany(p => p.TR_Letter_Lot_Details).HasConstraintName("FK_TR_Letter_Lot_Detail_TR_Letter_Lot");
        });

        modelBuilder.Entity<TR_Letter_Lot_Resource>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Lot).WithMany(p => p.TR_Letter_Lot_Resources).HasConstraintName("FK_TR_Letter_Lot_Resource_TR_Letter_Lot_Resource");
        });

        modelBuilder.Entity<TR_MenuRolePermission>(entity =>
        {
            entity.HasOne(d => d.Department).WithMany(p => p.TR_MenuRolePermissionDepartments).HasConstraintName("FK_TR_MenuRolePermission_tm_Ext1");

            entity.HasOne(d => d.Menu).WithMany(p => p.TR_MenuRolePermissions).HasConstraintName("FK_TR_MenuRolePermission_tm_Menu");

            entity.HasOne(d => d.QCType).WithMany(p => p.TR_MenuRolePermissionQCTypes).HasConstraintName("FK_TR_MenuRolePermission_tm_Ext");

            entity.HasOne(d => d.Role).WithMany(p => p.TR_MenuRolePermissions).HasConstraintName("FK_TR_MenuRolePermission_tm_Role");
        });

        modelBuilder.Entity<TR_PowerOfAttorney>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
        });

        modelBuilder.Entity<TR_ProjectAppointLimit_Mapping>(entity =>
        {
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Day).WithMany(p => p.TR_ProjectAppointLimit_MappingDays).HasConstraintName("FK_TR_ProjectAppointLimit_Mapping_tm_Ext");

            entity.HasOne(d => d.Project).WithMany(p => p.TR_ProjectAppointLimit_Mappings).HasConstraintName("FK_TR_ProjectAppointLimit_Mapping_tm_Project");

            entity.HasOne(d => d.Time).WithMany(p => p.TR_ProjectAppointLimit_MappingTimes).HasConstraintName("FK_TR_ProjectAppointLimit_Mapping_tm_Ext1");
        });

        modelBuilder.Entity<TR_ProjectCounter_Mapping>(entity =>
        {
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Project).WithMany(p => p.TR_ProjectCounter_Mappings).HasConstraintName("FK_TR_ProjectCounter_Mapping_TR_ProjectCounter_Mapping");

            entity.HasOne(d => d.QueueType).WithMany(p => p.TR_ProjectCounter_Mappings).HasConstraintName("FK_TR_ProjectCounter_Mapping_tm_Ext");
        });

        modelBuilder.Entity<TR_ProjectEmail_Mapping>(entity =>
        {
            entity.Property(e => e.CreateBy).IsFixedLength();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateBy).IsFixedLength();
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Project).WithMany(p => p.TR_ProjectEmail_Mappings).HasConstraintName("FK_TR_ProjectEmail_Mapping_tm_Project");
        });

        modelBuilder.Entity<TR_ProjectEvent>(entity =>
        {
            entity.HasOne(d => d.Event).WithMany(p => p.TR_ProjectEvents).HasConstraintName("FK_TR_ProjectEvent_tm_Event");

            entity.HasOne(d => d.Project).WithMany(p => p.TR_ProjectEvents).HasConstraintName("FK_TR_ProjectEvent_TR_ProjectEvent");
        });

        modelBuilder.Entity<TR_ProjectFloorPlan>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Project).WithMany(p => p.TR_ProjectFloorPlans).HasConstraintName("FK_TR_ProjectFloorPlan_tm_Project");
        });

        modelBuilder.Entity<TR_ProjectLandOffice>(entity =>
        {
            entity.HasOne(d => d.LandOffice).WithMany(p => p.TR_ProjectLandOffices).HasConstraintName("FK_TR_ProjectLandOffice_tm_LandOffice");

            entity.HasOne(d => d.Project).WithMany(p => p.TR_ProjectLandOffices).HasConstraintName("FK_TR_ProjectLandOffice_TR_ProjectLandOffice");
        });

        modelBuilder.Entity<TR_ProjectShopEvent>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_tr_ProjectShopEvent");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Event).WithMany(p => p.TR_ProjectShopEvents).HasConstraintName("FK_TR_ProjectShopEvent_Tm_Event");

            entity.HasOne(d => d.Project).WithMany(p => p.TR_ProjectShopEvents).HasConstraintName("FK_tr_ProjectShopEvent_tm_Project");

            entity.HasOne(d => d.Shop).WithMany(p => p.TR_ProjectShopEvents).HasConstraintName("FK_tr_ProjectShopEvent_tm_Shop");
        });

        modelBuilder.Entity<TR_ProjectStatus>(entity =>
        {
            entity.HasOne(d => d.Project).WithMany(p => p.TR_ProjectStatuses).HasConstraintName("FK_TR_ProjectStatus_tm_Project");

            entity.HasOne(d => d.Status).WithMany(p => p.TR_ProjectStatuses).HasConstraintName("FK_TR_ProjectStatus_tm_Ext");
        });

        modelBuilder.Entity<TR_ProjectUnitFloorPlan>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_TR_UnitFloorPlan");

            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.ProjectFloorPlan).WithMany(p => p.TR_ProjectUnitFloorPlans).HasConstraintName("FK_TR_ProjectUnitFloorPlan_TR_ProjectFloorPlan");

            entity.HasOne(d => d.Project).WithMany(p => p.TR_ProjectUnitFloorPlans).HasConstraintName("FK_TR_ProjectUnitFloorPlan_tm_Project");

            entity.HasOne(d => d.Unit).WithMany(p => p.TR_ProjectUnitFloorPlans).HasConstraintName("FK_TR_ProjectUnitFloorPlan_tm_Unit");
        });

        modelBuilder.Entity<TR_ProjectUserSign>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Project).WithMany(p => p.TR_ProjectUserSigns).HasConstraintName("FK_TR_ProjectUserSign_tm_Project");

            entity.HasOne(d => d.User).WithMany(p => p.TR_ProjectUserSigns).HasConstraintName("FK_TR_ProjectUserSign_tm_User");
        });

        modelBuilder.Entity<TR_ProjectZone_Mapping>(entity =>
        {
            entity.HasOne(d => d.ProjectZone).WithMany(p => p.TR_ProjectZone_Mappings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TR_ProjectZone_Mapping_tm_Ext");
        });

        modelBuilder.Entity<TR_QC1>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_TR_QC1_1");

            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Project).WithMany(p => p.TR_QC1s).HasConstraintName("FK_TR_QC1_tm_Project");

            entity.HasOne(d => d.QC_Status).WithMany(p => p.TR_QC1s).HasConstraintName("FK_TR_QC1_tm_Ext");

            entity.HasOne(d => d.ResponsiblePerson).WithMany(p => p.TR_QC1s).HasConstraintName("FK_TR_QC1_tm_User");

            entity.HasOne(d => d.Unit).WithMany(p => p.TR_QC1s).HasConstraintName("FK_TR_QC1_tm_Unit");
        });

        modelBuilder.Entity<TR_QC2>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Project).WithMany(p => p.TR_QC2s).HasConstraintName("FK_TR_QC2_tm_Project");

            entity.HasOne(d => d.QC_Status).WithMany(p => p.TR_QC2s).HasConstraintName("FK_TR_QC2_tm_Ext");

            entity.HasOne(d => d.ResponsiblePerson).WithMany(p => p.TR_QC2s).HasConstraintName("FK_TR_QC2_tm_User");

            entity.HasOne(d => d.Unit).WithMany(p => p.TR_QC2s).HasConstraintName("FK_TR_QC2_tm_Unit");
        });

        modelBuilder.Entity<TR_QC3>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Project).WithMany(p => p.TR_QC3s).HasConstraintName("FK_TR_QC3_tm_Project");

            entity.HasOne(d => d.QC_Status).WithMany(p => p.TR_QC3s).HasConstraintName("FK_TR_QC3_tm_Ext");

            entity.HasOne(d => d.ResponsiblePerson).WithMany(p => p.TR_QC3s).HasConstraintName("FK_TR_QC3_tm_User");

            entity.HasOne(d => d.Unit).WithMany(p => p.TR_QC3s).HasConstraintName("FK_TR_QC3_tm_Unit");
        });

        modelBuilder.Entity<TR_QC4>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Project).WithMany(p => p.TR_QC4s).HasConstraintName("FK_TR_QC4_tm_Project");

            entity.HasOne(d => d.QC_Status).WithMany(p => p.TR_QC4s).HasConstraintName("FK_TR_QC4_tm_Ext");

            entity.HasOne(d => d.ResponsiblePerson).WithMany(p => p.TR_QC4s).HasConstraintName("FK_TR_QC4_tm_User");

            entity.HasOne(d => d.Unit).WithMany(p => p.TR_QC4s).HasConstraintName("FK_TR_QC4_tm_Unit");
        });

        modelBuilder.Entity<TR_QC5>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.CustRelation).WithMany(p => p.TR_QC5CustRelations).HasConstraintName("FK_TR_QC5_tm_Ext1");

            entity.HasOne(d => d.Project).WithMany(p => p.TR_QC5s).HasConstraintName("FK_TR_QC5_tm_Project");

            entity.HasOne(d => d.QC_Status).WithMany(p => p.TR_QC5QC_Statuses).HasConstraintName("FK_TR_QC5_tm_Ext");

            entity.HasOne(d => d.ResponsiblePerson).WithMany(p => p.TR_QC5s).HasConstraintName("FK_TR_QC5_tm_User");

            entity.HasOne(d => d.SignResource).WithMany(p => p.TR_QC5s).HasConstraintName("FK_TR_QC5_TR_SignResource");

            entity.HasOne(d => d.Unit).WithMany(p => p.TR_QC5s).HasConstraintName("FK_TR_QC5_tm_Unit");
        });

        modelBuilder.Entity<TR_QC5_CheckList>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.HasOne(d => d.Project).WithMany(p => p.TR_QC5_CheckLists).HasConstraintName("FK_TR_QC5_CheckList_tm_Project");

            entity.HasOne(d => d.Unit).WithMany(p => p.TR_QC5_CheckLists).HasConstraintName("FK_TR_QC5_CheckList_tm_Unit");
        });

        modelBuilder.Entity<TR_QC5_CheckList_Detail>(entity =>
        {
            entity.HasOne(d => d.Answer).WithMany(p => p.TR_QC5_CheckList_Details).HasConstraintName("FK_TR_QC5_CheckList_Detail_tm_Ext");

            entity.HasOne(d => d.QC5CheckList).WithMany(p => p.TR_QC5_CheckList_Details).HasConstraintName("FK_TR_QC5_CheckList_Detail_TR_QC5_CheckList");

            entity.HasOne(d => d.Question).WithMany(p => p.TR_QC5_CheckList_Details).HasConstraintName("FK_TR_QC5_CheckList_Detail_tm_QC5_CheckList");
        });

        modelBuilder.Entity<TR_QC5_FinishPlan>(entity =>
        {
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<TR_QC5_Open>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Project).WithMany(p => p.TR_QC5_Opens).HasConstraintName("FK_TR_QC5_Open_tm_Project");

            entity.HasOne(d => d.QC_Status).WithMany(p => p.TR_QC5_Opens).HasConstraintName("FK_TR_QC5_Open_tm_Ext");

            entity.HasOne(d => d.Unit).WithMany(p => p.TR_QC5_Opens).HasConstraintName("FK_TR_QC5_Open_tm_Unit");
        });

        modelBuilder.Entity<TR_QC5_ProjectSendMail>(entity =>
        {
            entity.HasOne(d => d.Project).WithMany(p => p.TR_QC5_ProjectSendMails).HasConstraintName("FK_TR_QC5_ProjectSendMail_tm_Project");
        });

        modelBuilder.Entity<TR_QC6>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.CustRelation).WithMany(p => p.TR_QC6CustRelations).HasConstraintName("FK_TR_QC6_tm_Ext1");

            entity.HasOne(d => d.Project).WithMany(p => p.TR_QC6s).HasConstraintName("FK_TR_QC6_tm_Project");

            entity.HasOne(d => d.QC_Status).WithMany(p => p.TR_QC6QC_Statuses).HasConstraintName("FK_TR_QC6_tm_Ext");

            entity.HasOne(d => d.ResponsiblePerson).WithMany(p => p.TR_QC6s).HasConstraintName("FK_TR_QC6_tm_User");

            entity.HasOne(d => d.SignResource).WithMany(p => p.TR_QC6s).HasConstraintName("FK_TR_QC6_TR_SignResource");

            entity.HasOne(d => d.Unit).WithMany(p => p.TR_QC6s).HasConstraintName("FK_TR_QC6_tm_Unit");
        });

        modelBuilder.Entity<TR_QC6_Unsold>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_TR_QC_Unsold");

            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Project).WithMany(p => p.TR_QC6_Unsolds).HasConstraintName("FK_TR_QC6_Unsold_tm_Project");

            entity.HasOne(d => d.SignResource).WithMany(p => p.TR_QC6_Unsolds).HasConstraintName("FK_TR_QC6_Unsold_TR_SignResource");

            entity.HasOne(d => d.Unit).WithMany(p => p.TR_QC6_Unsolds).HasConstraintName("FK_TR_QC6_Unsold_tm_Unit");
        });

        modelBuilder.Entity<TR_QC6_Unsold_SendMail>(entity =>
        {
            entity.HasOne(d => d.Project).WithMany(p => p.TR_QC6_Unsold_SendMails).HasConstraintName("FK_TR_QC6_Unsold_SendMail_tm_Project");
        });

        modelBuilder.Entity<TR_QC_CheckList>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_TR_QC1_CheckList");

            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Project).WithMany(p => p.TR_QC_CheckLists).HasConstraintName("FK_TR_QC_CheckList_tm_Project");

            entity.HasOne(d => d.QCType).WithMany(p => p.TR_QC_CheckLists).HasConstraintName("FK_TR_QC_CheckList_tm_Ext");

            entity.HasOne(d => d.Subject).WithMany(p => p.TR_QC_CheckLists).HasConstraintName("FK_TR_QC_CheckList_tm_Subject");

            entity.HasOne(d => d.Topic).WithMany(p => p.TR_QC_CheckLists).HasConstraintName("FK_TR_QC_CheckList_tm_Topic");

            entity.HasOne(d => d.Unit).WithMany(p => p.TR_QC_CheckLists).HasConstraintName("FK_TR_QC_CheckList_tm_Unit");
        });

        modelBuilder.Entity<TR_QC_ContactLogResource>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.HasOne(d => d.QCContactLog).WithMany(p => p.TR_QC_ContactLogResources).HasConstraintName("FK_TR_QC_ContactLogResource_TR_ContactLog");

            entity.HasOne(d => d.Resource).WithMany(p => p.TR_QC_ContactLogResources).HasConstraintName("FK_TR_QC_ContactLogResource_TR_Resources");
        });

        modelBuilder.Entity<TR_QC_Defect>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_TR_QCDefect");

            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.DefectArea).WithMany(p => p.TR_QC_Defects).HasConstraintName("FK_TR_QCDefect_tm_DefectArea");

            entity.HasOne(d => d.DefectDescription).WithMany(p => p.TR_QC_Defects).HasConstraintName("FK_TR_QCDefect_tm_DefectDescription");

            entity.HasOne(d => d.DefectStatus).WithMany(p => p.TR_QC_Defects).HasConstraintName("FK_TR_QCDefect_tm_Ext");

            entity.HasOne(d => d.DefectType).WithMany(p => p.TR_QC_Defects).HasConstraintName("FK_TR_QCDefect_tm_DefectType");

            entity.HasOne(d => d.Project).WithMany(p => p.TR_QC_Defects).HasConstraintName("FK_TR_QCDefect_tm_Project");

            entity.HasOne(d => d.Unit).WithMany(p => p.TR_QC_Defects).HasConstraintName("FK_TR_QCDefect_tm_Unit");
        });

        modelBuilder.Entity<TR_QC_DefectResource>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_TR_DefectResource");

            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.HasOne(d => d.Defect).WithMany(p => p.TR_QC_DefectResources).HasConstraintName("FK_TR_QC_DefectResource_TR_QC_Defect");

            entity.HasOne(d => d.Resource).WithMany(p => p.TR_QC_DefectResources).HasConstraintName("FK_TR_QC_DefectResource_TR_Resources");
        });

        modelBuilder.Entity<TR_QC_Defect_Draft>(entity =>
        {
            entity.HasKey(e => e.DefectID).HasName("PK_TR_QC_Defect_Draft_1");

            entity.Property(e => e.DefectID).ValueGeneratedNever();
        });

        modelBuilder.Entity<TR_QC_Defect_OverDueExpect>(entity =>
        {
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Defect).WithMany(p => p.TR_QC_Defect_OverDueExpects).HasConstraintName("FK_TR_QC_Defect_OverDueExpect_TR_QC_Defect");

            entity.HasOne(d => d.EstimateStatus).WithMany(p => p.TR_QC_Defect_OverDueExpects).HasConstraintName("FK_TR_QC_Defect_OverDueExpect_TR_QC_Defect_OverDueExpect");
        });

        modelBuilder.Entity<TR_QC_Defect_OverDueExpect_BK>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<TR_QC_Defect_OverDueExpect_UserPermission>(entity =>
        {
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.User).WithMany(p => p.TR_QC_Defect_OverDueExpect_UserPermissions).HasConstraintName("FK_TR_QC_Defect_OverDueExpect_UserPermission_tm_User");
        });

        modelBuilder.Entity<TR_QuestionAnswer>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Answer).WithMany(p => p.TR_QuestionAnswers).HasConstraintName("FK_TR_QuestionAnswer_tm_Answer");

            entity.HasOne(d => d.Project).WithMany(p => p.TR_QuestionAnswers).HasConstraintName("FK_TR_QuestionAnswer_tm_Project");

            entity.HasOne(d => d.Question).WithMany(p => p.TR_QuestionAnswers).HasConstraintName("FK_TR_QuestionAnswer_tm_Question");
        });

        modelBuilder.Entity<TR_QuestionC>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.QuestionType).WithMany(p => p.TR_QuestionCs).HasConstraintName("FK_TR_QuestionCS_tm_Ext");
        });

        modelBuilder.Entity<TR_ReceiveRoomAgreementSign>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.HasOne(d => d.Project).WithMany(p => p.TR_ReceiveRoomAgreementSigns).HasConstraintName("FK_TR_ReceiveRoomAgreementSign_tm_Project");

            entity.HasOne(d => d.SignResource).WithMany(p => p.TR_ReceiveRoomAgreementSigns).HasConstraintName("FK_TR_ReceiveRoomAgreementSign_TR_SignResource");

            entity.HasOne(d => d.Unit).WithMany(p => p.TR_ReceiveRoomAgreementSigns).HasConstraintName("FK_TR_ReceiveRoomAgreementSign_tm_Unit");
        });

        modelBuilder.Entity<TR_ReceiveRoomSign>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.HasOne(d => d.Project).WithMany(p => p.TR_ReceiveRoomSigns).HasConstraintName("FK_TR_ReceiveRoomSign_tm_Project");

            entity.HasOne(d => d.SignResource).WithMany(p => p.TR_ReceiveRoomSigns).HasConstraintName("FK_TR_ReceiveRoomSign_TR_SignResource");

            entity.HasOne(d => d.Unit).WithMany(p => p.TR_ReceiveRoomSigns).HasConstraintName("FK_TR_ReceiveRoomSign_tm_Unit");
        });

        modelBuilder.Entity<TR_RegisterBank>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.HasOne(d => d.RegisterLog).WithMany(p => p.TR_RegisterBanks).HasConstraintName("FK_TR_RegisterBank_TR_RegisterLog");
        });

        modelBuilder.Entity<TR_RegisterLog>(entity =>
        {
            entity.HasOne(d => d.Project).WithMany(p => p.TR_RegisterLogs).HasConstraintName("FK_TR_RegisterLog_tm_Project");

            entity.HasOne(d => d.QCType).WithMany(p => p.TR_RegisterLogs).HasConstraintName("FK_TR_RegisterLog_tm_Ext");

            entity.HasOne(d => d.Responsible).WithMany(p => p.TR_RegisterLogs).HasConstraintName("FK_TR_RegisterLog_tm_User");

            entity.HasOne(d => d.Unit).WithMany(p => p.TR_RegisterLogs).HasConstraintName("FK_TR_RegisterLog_tm_Unit");
        });

        modelBuilder.Entity<TR_Register_BankCounter>(entity =>
        {
            entity.HasOne(d => d.Bank).WithMany(p => p.TR_Register_BankCounters).HasConstraintName("FK_TR_Register_BankCounter_tm_Bank");

            entity.HasOne(d => d.RegisterLog).WithMany(p => p.TR_Register_BankCounters).HasConstraintName("FK_TR_Register_BankCounter_TR_RegisterLog");
        });

        modelBuilder.Entity<TR_Register_CallStaffCounter>(entity =>
        {
            entity.HasOne(d => d.RegisterLog).WithMany(p => p.TR_Register_CallStaffCounters).HasConstraintName("FK_TR_Register_CallStaffCounter_TR_RegisterLog");
        });

        modelBuilder.Entity<TR_Register_ProjectBankStaff>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_TR_Register_BankStaff");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Bank).WithMany(p => p.TR_Register_ProjectBankStaffs).HasConstraintName("FK_TR_Register_BankStaff_tm_Bank");

            entity.HasOne(d => d.Project).WithMany(p => p.TR_Register_ProjectBankStaffs).HasConstraintName("FK_TR_Register_BankStaff_tm_Project");
        });

        modelBuilder.Entity<TR_RemarkUnitStatus_Mapping>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_TR_RemarkUnitStatus_Mapping_1");

            entity.HasOne(d => d.RemarkUnitStatusCS).WithMany(p => p.TR_RemarkUnitStatus_MappingRemarkUnitStatusCs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TR_RemarkUnitStatus_Mapping_tm_Ext1");

            entity.HasOne(d => d.UnitStatusCS).WithMany(p => p.TR_RemarkUnitStatus_MappingUnitStatusCs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TR_RemarkUnitStatus_Mapping_tm_Ext");
        });

        modelBuilder.Entity<TR_Resource>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<TR_ReviseUnitPromotion>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.ApproveStatus).WithMany(p => p.TR_ReviseUnitPromotionApproveStatuses).HasConstraintName("FK_TR_ReviseUnitPromotion_tm_Ext");

            entity.HasOne(d => d.ApproveStatusID_2Navigation).WithMany(p => p.TR_ReviseUnitPromotionApproveStatusID_2Navigations).HasConstraintName("FK_TR_ReviseUnitPromotion_tm_Ext1");

            entity.HasOne(d => d.CustomerBank).WithMany(p => p.TR_ReviseUnitPromotions).HasConstraintName("FK_TR_ReviseUnitPromotion_tm_Bank");

            entity.HasOne(d => d.Project).WithMany(p => p.TR_ReviseUnitPromotions).HasConstraintName("FK_TR_ReviseUnitPromotion_tm_Project");

            entity.HasOne(d => d.Unit).WithMany(p => p.TR_ReviseUnitPromotions).HasConstraintName("FK_TR_ReviseUnitPromotion_tm_Unit");
        });

        modelBuilder.Entity<TR_ReviseUnitPromotion_Detail>(entity =>
        {
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.ReviseUnitPromotion).WithMany(p => p.TR_ReviseUnitPromotion_Details).HasConstraintName("FK_TR_ReviseUnitPromotion_Detail_TR_ReviseUnitPromotion");
        });

        modelBuilder.Entity<TR_ReviseUnitPromoton_Approver>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_TR_ReviseUnitPromoton_Apprvoe");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.ApproveRole).WithMany(p => p.TR_ReviseUnitPromoton_Approvers).HasConstraintName("FK_TR_ReviseUnitPromoton_Approve_tm_Ext");

            entity.HasOne(d => d.User).WithMany(p => p.TR_ReviseUnitPromoton_Approvers).HasConstraintName("FK_TR_ReviseUnitPromoton_Approve_tm_User");
        });

        modelBuilder.Entity<TR_SignResource>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<TR_Sync>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.HasOne(d => d.Project).WithMany(p => p.TR_Syncs).HasConstraintName("FK_TR_Sync_tm_Project");

            entity.HasOne(d => d.Unit).WithMany(p => p.TR_Syncs).HasConstraintName("FK_TR_Sync_tm_Unit");
        });

        modelBuilder.Entity<TR_Sync_LoanBank>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_TR_Sync_LoanBank_CRM");

            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.HasOne(d => d.Sync).WithMany(p => p.TR_Sync_LoanBanks).HasConstraintName("FK_TR_Sync_LoanBank_CRM_TR_Sync");
        });

        modelBuilder.Entity<TR_Sync_Log>(entity =>
        {
            entity.HasOne(d => d.Project).WithMany(p => p.TR_Sync_Logs).HasConstraintName("FK_TR_Sync_Logs_tm_Project");
        });

        modelBuilder.Entity<TR_Sync_QC>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.HasOne(d => d.Project).WithMany(p => p.TR_Sync_QCs).HasConstraintName("FK_TR_Sync_QC_tm_Project");

            entity.HasOne(d => d.QCType).WithMany(p => p.TR_Sync_QCs).HasConstraintName("FK_TR_Sync_QC_tm_Ext");

            entity.HasOne(d => d.Sync).WithMany(p => p.TR_Sync_QCs).HasConstraintName("FK_TR_Sync_QC_TR_Sync");

            entity.HasOne(d => d.Unit).WithMany(p => p.TR_Sync_QCs).HasConstraintName("FK_TR_Sync_QC_tm_Unit");
        });

        modelBuilder.Entity<TR_TagEvent>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_tr_TagEvent");

            entity.HasOne(d => d.Event).WithMany(p => p.TR_TagEvents).HasConstraintName("FK_tr_TagEvent_tm_Event");

            entity.HasOne(d => d.Tag).WithMany(p => p.TR_TagEvents).HasConstraintName("FK_tr_TagEvent_tm_Tag");
        });

        modelBuilder.Entity<TR_TargetRollingPlan>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_TR_TargetRolloing");

            entity.HasOne(d => d.PlanAmount).WithMany(p => p.TR_TargetRollingPlanPlanAmounts).HasConstraintName("FK_TR_TargetRollingPlan_tm_Ext");

            entity.HasOne(d => d.PlanType).WithMany(p => p.TR_TargetRollingPlanPlanTypes).HasConstraintName("FK_TR_TargetRolloingPlan_tm_Ext");

            entity.HasOne(d => d.Project).WithMany(p => p.TR_TargetRollingPlans).HasConstraintName("FK_TR_TargetRolloingPlan_tm_Project");
        });

        modelBuilder.Entity<TR_TargetRollingPlan_BK>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<TR_TerminateTransferAppoint>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_TR_UnitCancelContract");

            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.TerminateStatus).WithMany(p => p.TR_TerminateTransferAppoints).HasConstraintName("FK_TR_TerminateTransferAppoint_tm_Ext");

            entity.HasOne(d => d.Unit).WithMany(p => p.TR_TerminateTransferAppoints).HasConstraintName("FK_TR_TerminateTransferAppoint_tm_Unit");
        });

        modelBuilder.Entity<TR_TerminateTransferAppoint_Document>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.HasOne(d => d.TerminateTransferAppoint).WithMany(p => p.TR_TerminateTransferAppoint_Documents).HasConstraintName("FK_TR_TerminateTransferAppoint_Document_TR_TerminateTransferAppoint");

            entity.HasOne(d => d.UnitDocument).WithMany(p => p.TR_TerminateTransferAppoint_Documents).HasConstraintName("FK_TR_TerminateTransferAppoint_Document_TR_UnitDocument");
        });

        modelBuilder.Entity<TR_TransferDocument>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_TR_Document_TransferReceive");

            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.PrintDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Cashier_Bank).WithMany(p => p.TR_TransferDocuments).HasConstraintName("FK_TR_TransferDocument_tm_Bank");

            entity.HasOne(d => d.Project).WithMany(p => p.TR_TransferDocuments).HasConstraintName("FK_TR_TransferDocument_tm_Project");

            entity.HasOne(d => d.Unit).WithMany(p => p.TR_TransferDocuments).HasConstraintName("FK_TR_TransferDocument_tm_Unit");
        });

        modelBuilder.Entity<TR_UnitDocument>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_TR_Document");

            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.DocumentType).WithMany(p => p.TR_UnitDocumentDocumentTypes).HasConstraintName("FK_TR_UnitDocument_tm_Ext1");

            entity.HasOne(d => d.Project).WithMany(p => p.TR_UnitDocuments).HasConstraintName("FK_TR_UnitDocument_tm_Project");

            entity.HasOne(d => d.QCType).WithMany(p => p.TR_UnitDocumentQCTypes).HasConstraintName("FK_TR_UnitDocument_tm_Ext");

            entity.HasOne(d => d.Unit).WithMany(p => p.TR_UnitDocuments).HasConstraintName("FK_TR_UnitDocument_tm_Unit");
        });

        modelBuilder.Entity<TR_UnitDocumentNo>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.FlagActive).HasDefaultValue(true);

            entity.HasOne(d => d.DocumentType).WithMany(p => p.TR_UnitDocumentNoDocumentTypes).HasConstraintName("FK_TR_UnitDocumentNo_tm_Ext1");

            entity.HasOne(d => d.Project).WithMany(p => p.TR_UnitDocumentNos).HasConstraintName("FK_TR_UnitDocumentNo_tm_Project");

            entity.HasOne(d => d.QCType).WithMany(p => p.TR_UnitDocumentNoQCTypes).HasConstraintName("FK_TR_UnitDocumentNo_tm_Ext");

            entity.HasOne(d => d.Unit).WithMany(p => p.TR_UnitDocumentNos).HasConstraintName("FK_TR_UnitDocumentNo_tm_Unit");
        });

        modelBuilder.Entity<TR_UnitEquipment>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_tr_UnitEquipment");

            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.CustomerSign).WithMany(p => p.TR_UnitEquipmentCustomerSigns).HasConstraintName("FK_tr_UnitEquipment_TR_SignResource");

            entity.HasOne(d => d.JMSign).WithMany(p => p.TR_UnitEquipmentJMSigns).HasConstraintName("FK_TR_UnitEquipment_TR_SignResource1");

            entity.HasOne(d => d.Project).WithMany(p => p.TR_UnitEquipments).HasConstraintName("FK_tr_UnitEquipment_tm_Project");

            entity.HasOne(d => d.UnitDocument).WithMany(p => p.TR_UnitEquipments).HasConstraintName("FK_tr_UnitEquipment_TR_UnitDocument");

            entity.HasOne(d => d.Unit).WithMany(p => p.TR_UnitEquipments).HasConstraintName("FK_tr_UnitEquipment_tm_Unit");
        });

        modelBuilder.Entity<TR_UnitEquipment_Detail>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_tr_UnitEquipment_Detail");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<TR_UnitEvent>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_tr_UnitEvent");

            entity.Property(e => e.CraeteDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Event).WithMany(p => p.TR_UnitEvents).HasConstraintName("FK_TR_UnitEvent_tm_Event");

            entity.HasOne(d => d.Unit).WithMany(p => p.TR_UnitEvents).HasConstraintName("FK_TR_UnitEvent_tm_Unit");
        });

        modelBuilder.Entity<TR_UnitFurniture>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_tr_UnitFurniture");

            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.CMSign).WithMany(p => p.TR_UnitFurnitureCMSigns).HasConstraintName("FK_TR_UnitFurniture_TR_SignResource1");

            entity.HasOne(d => d.CheckStatus).WithMany(p => p.TR_UnitFurnitures).HasConstraintName("FK_TR_UnitFurniture_tm_Ext");

            entity.HasOne(d => d.CustomerSign).WithMany(p => p.TR_UnitFurnitureCustomerSigns).HasConstraintName("FK_TR_UnitFurniture_TR_SignResource");

            entity.HasOne(d => d.Project).WithMany(p => p.TR_UnitFurnitures).HasConstraintName("FK_TR_UnitFurniture_tm_Project");

            entity.HasOne(d => d.UnitDocument).WithMany(p => p.TR_UnitFurnitures).HasConstraintName("FK_TR_UnitFurniture_TR_UnitDocument");

            entity.HasOne(d => d.Unit).WithMany(p => p.TR_UnitFurnitures).HasConstraintName("FK_TR_UnitFurniture_tm_Unit");
        });

        modelBuilder.Entity<TR_UnitFurniture_Detail>(entity =>
        {
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Furniture).WithMany(p => p.TR_UnitFurniture_Details).HasConstraintName("FK_TR_UnitFurniture_Detail_tm_Funiture");

            entity.HasOne(d => d.UnitFurniture).WithMany(p => p.TR_UnitFurniture_Details).HasConstraintName("FK_TR_UnitFurniture_Detail_TR_UnitFurniture");
        });

        modelBuilder.Entity<TR_UnitPromotion>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_TR_TransferDocument_Promotion");

            entity.HasOne(d => d.Promotion).WithMany(p => p.TR_UnitPromotions).HasConstraintName("FK_TR_TransferDocument_Promotion_tm_Promotion");

            entity.HasOne(d => d.Unit).WithMany(p => p.TR_UnitPromotions).HasConstraintName("FK_TR_UnitPromotion_tm_Unit");
        });

        modelBuilder.Entity<TR_UnitPromotionSign>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.HasOne(d => d.IDCardResource).WithMany(p => p.TR_UnitPromotionSignIDCardResources).HasConstraintName("FK_TR_UnitPromotionSign_TR_SignResource1");

            entity.HasOne(d => d.SignResource).WithMany(p => p.TR_UnitPromotionSignSignResources).HasConstraintName("FK_TR_UnitPromotionSign_TR_SignResource");

            entity.HasOne(d => d.Unit).WithMany(p => p.TR_UnitPromotionSigns).HasConstraintName("FK_TR_UnitPromotionSign_tm_Unit");
        });

        modelBuilder.Entity<TR_UnitShopEvent>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_tr_UnitShopEvent");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<TR_UnitUser_Mapping>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<TR_Unsold_Round>(entity =>
        {
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Project).WithMany(p => p.TR_Unsold_Rounds).HasConstraintName("FK_TR_Unsold_Round_tm_Project");
        });

        modelBuilder.Entity<TR_Unsold_RoundUnit>(entity =>
        {
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Project).WithMany(p => p.TR_Unsold_RoundUnits).HasConstraintName("FK_TR_Unsold_RoundUnit_tm_Project");

            entity.HasOne(d => d.Round).WithMany(p => p.TR_Unsold_RoundUnits).HasConstraintName("FK_TR_Unsold_RoundUnit_TR_Unsold_Round");

            entity.HasOne(d => d.Unit).WithMany(p => p.TR_Unsold_RoundUnits).HasConstraintName("FK_TR_Unsold_RoundUnit_tm_Unit");
        });

        modelBuilder.Entity<TR_UserPosition>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.HasOne(d => d.Position).WithMany(p => p.TR_UserPositions).HasConstraintName("FK_TR_UserPosition_tm_Position");

            entity.HasOne(d => d.User).WithMany(p => p.TR_UserPositions).HasConstraintName("FK_TR_UserPosition_tm_User");
        });

        modelBuilder.Entity<TR_UserSignResource>(entity =>
        {
            entity.HasKey(e => e.UserID).HasName("PK_TR_UserSignResource_1");

            entity.Property(e => e.UserID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.SignResource).WithMany(p => p.TR_UserSignResources).HasConstraintName("FK_TR_UserSignResource_TR_SignResource");

            entity.HasOne(d => d.User).WithOne(p => p.TR_UserSignResource)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TR_UserSignResource_tm_User");
        });

        modelBuilder.Entity<temp_checker>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
        });

        modelBuilder.Entity<temp_cust_sat_20240104>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<temp_new_bu>(entity =>
        {
            entity.Property(e => e.BUName).UseCollation("Thai_100_CI_AS");
            entity.Property(e => e.ProjectID).UseCollation("Thai_100_CI_AS");
            entity.Property(e => e.ProjectName).UseCollation("Thai_100_CI_AS");
        });

        modelBuilder.Entity<tm_Answer>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Question).WithMany(p => p.tm_Answers).HasConstraintName("FK_tm_Answer_tm_Question");
        });

        modelBuilder.Entity<tm_BU>(entity =>
        {
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<tm_BUProject_Mapping>(entity =>
        {
            entity.HasOne(d => d.BU).WithMany(p => p.tm_BUProject_Mappings).HasConstraintName("FK_tm_BUProject_Mapping_tm_BU");

            entity.HasOne(d => d.Project).WithMany(p => p.tm_BUProject_Mappings).HasConstraintName("FK_tm_BUProject_Mapping_tm_Project");
        });

        modelBuilder.Entity<tm_BU_Mapping>(entity =>
        {
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);

            entity.HasOne(d => d.BU).WithMany(p => p.tm_BU_Mappings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tm_BU_Mapping_BU");

            entity.HasOne(d => d.User).WithMany(p => p.tm_BU_Mappings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tm_BU_Mapping_User");
        });

        modelBuilder.Entity<tm_Bank>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<tm_Company>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<tm_DataSource>(entity =>
        {
            entity.Property(e => e.Datasource_ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<tm_DefectArea>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<tm_DefectAreaType_Mapping>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.HasOne(d => d.DefectArea).WithMany(p => p.tm_DefectAreaType_Mappings).HasConstraintName("FK_tm_DefectAreaType_Mapping_tm_DefectArea");

            entity.HasOne(d => d.DefectType).WithMany(p => p.tm_DefectAreaType_Mappings).HasConstraintName("FK_tm_DefectAreaType_Mapping_tm_DefectType");
        });

        modelBuilder.Entity<tm_DefectDescription>(entity =>
        {
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.DefectType).WithMany(p => p.tm_DefectDescriptions).HasConstraintName("FK_tm_DefectDescription_tm_DefectType");
        });

        modelBuilder.Entity<tm_DefectType>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<tm_Equipment>(entity =>
        {
            entity.Property(e => e.CraeteDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<tm_Event>(entity =>
        {
            entity.Property(e => e.CraeteDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Project).WithMany(p => p.tm_Events).HasConstraintName("FK_tm_Event_tm_Project");
        });

        modelBuilder.Entity<tm_EventType>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__tm_Event__3214EC27DD2C84BB");

            entity.Property(e => e.FlagActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<tm_Ext>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_tm_ExtType");

            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.ExtType).WithMany(p => p.tm_Exts).HasConstraintName("FK_tm_Ext_tm_Ext");
        });

        modelBuilder.Entity<tm_ExtType>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_tm_ExtType_1");

            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<tm_Funiture>(entity =>
        {
            entity.Property(e => e.CraeteDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<tm_Holiday>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<tm_LandOffice>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<tm_LetterDayDue>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagAcive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<tm_LetterSendReason>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<tm_LineToken>(entity =>
        {
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Bank).WithMany(p => p.tm_LineTokens).HasConstraintName("FK_tm_LineToken_tm_Bank");

            entity.HasOne(d => d.ProjectZone).WithMany(p => p.tm_LineTokens).HasConstraintName("FK_tm_LineToken_tm_Ext");
        });

        modelBuilder.Entity<tm_Menu>(entity =>
        {
            entity.HasOne(d => d.QCType).WithMany(p => p.tm_Menus).HasConstraintName("FK_tm_Menu_tm_Ext");
        });

        modelBuilder.Entity<tm_MenuAction>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__tm_MenuA__3214EC27F24ACE3E");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);

            entity.HasOne(d => d.Menu).WithMany(p => p.tm_MenuActions).HasConstraintName("FK_tm_MenuAction_Menu");
        });

        modelBuilder.Entity<tm_Position>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<tm_Project>(entity =>
        {
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Partner).WithMany(p => p.tm_Projects).HasConstraintName("FK_tm_Project_tm_Ext");
        });

        modelBuilder.Entity<tm_ProjectUser_Mapping>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_TR_ProjectUser_Mapping");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.GroupUser).WithMany(p => p.tm_ProjectUser_Mappings).HasConstraintName("FK_tm_ProjectUser_Mapping_tm_Ext");

            entity.HasOne(d => d.Project).WithMany(p => p.tm_ProjectUser_Mappings).HasConstraintName("FK_tm_ProjectUser_Mapping_tm_Project");

            entity.HasOne(d => d.User).WithMany(p => p.tm_ProjectUser_Mappings).HasConstraintName("FK_tm_ProjectUser_Mapping_tm_User");
        });

        modelBuilder.Entity<tm_QC5_CheckList>(entity =>
        {
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<tm_Question>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<tm_Responsible_Mapping>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Project).WithMany(p => p.tm_Responsible_Mappings).HasConstraintName("FK_tm_Responsible_Mapping_tm_Project");

            entity.HasOne(d => d.QCType).WithMany(p => p.tm_Responsible_Mappings).HasConstraintName("FK_tm_Responsible_Mapping_tm_Ext");

            entity.HasOne(d => d.UserID_MappingNavigation).WithMany(p => p.tm_Responsible_Mappings).HasConstraintName("FK_tm_Responsible_Mapping_tm_User");
        });

        modelBuilder.Entity<tm_Role>(entity =>
        {
            entity.HasOne(d => d.QCType).WithMany(p => p.tm_Roles).HasConstraintName("FK_tm_Role_tm_Ext");
        });

        modelBuilder.Entity<tm_Shop>(entity =>
        {
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<tm_Subject>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Topic).WithMany(p => p.tm_Subjects).HasConstraintName("FK_tm_Subject_tm_Topic");
        });

        modelBuilder.Entity<tm_Tag>(entity =>
        {
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<tm_TitleName>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_tm_Title");

            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<tm_Topic>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<tm_Unit>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.MeterType).WithMany(p => p.tm_UnitMeterTypes).HasConstraintName("FK_tm_Unit_tm_Ext");

            entity.HasOne(d => d.Project).WithMany(p => p.tm_Units).HasConstraintName("FK_tm_Unit_tm_Project");

            entity.HasOne(d => d.UnitStatusNavigation).WithMany(p => p.tm_Units).HasConstraintName("FK_tm_Unit_tm_UnitStatus");

            entity.HasOne(d => d.UnitStatus_SaleNavigation).WithMany(p => p.tm_UnitUnitStatus_SaleNavigations).HasConstraintName("FK_tm_Unit_tm_Ext1");

            entity.HasOne(d => d.WIP_Matrix).WithMany(p => p.tm_Units).HasConstraintName("FK_tm_Unit_tm_WIPMatrix");
        });

        modelBuilder.Entity<tm_UnitStatus>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<tm_User>(entity =>
        {
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.QCType).WithMany(p => p.tm_Users).HasConstraintName("FK_tm_User_tm_Ext");

            entity.HasOne(d => d.Role).WithMany(p => p.tm_Users).HasConstraintName("FK_tm_User_tm_Role");

            entity.HasOne(d => d.Title).WithMany(p => p.tm_UserTitles).HasConstraintName("FK_tm_User_tm_TitleName");

            entity.HasOne(d => d.TitleID_EngNavigation).WithMany(p => p.tm_UserTitleID_EngNavigations).HasConstraintName("FK_tm_User_tm_TitleName1");
        });

        modelBuilder.Entity<tm_WIPMatrix>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<tm_WIPMatrix_QC6>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<vw_BI_Backlog>(entity =>
        {
            entity.ToView("vw_BI_Backlog");
        });

        modelBuilder.Entity<vw_BI_CRM_CashbackLTV>(entity =>
        {
            entity.ToView("vw_BI_CRM_CashbackLTV");
        });

        modelBuilder.Entity<vw_BI_Project>(entity =>
        {
            entity.ToView("vw_BI_Project");
        });

        modelBuilder.Entity<vw_BI_Project_Initial_Month>(entity =>
        {
            entity.ToView("vw_BI_Project_Initial_Month");
        });

        modelBuilder.Entity<vw_BI_Transfer_Actual>(entity =>
        {
            entity.ToView("vw_BI_Transfer_Actual");
        });

        modelBuilder.Entity<vw_BI_Transfer_NetActual>(entity =>
        {
            entity.ToView("vw_BI_Transfer_NetActual");
        });

        modelBuilder.Entity<vw_BI_Transfer_TargetRolling>(entity =>
        {
            entity.ToView("vw_BI_Transfer_TargetRolling");
        });

        modelBuilder.Entity<vw_BI_Transfer_TargetRollingActual>(entity =>
        {
            entity.ToView("vw_BI_Transfer_TargetRollingActual");
        });

        modelBuilder.Entity<vw_Defect>(entity =>
        {
            entity.ToView("vw_Defect");
        });

        modelBuilder.Entity<vw_ITF_TempBlakUnit>(entity =>
        {
            entity.Property(e => e.ID).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<vw_Unit>(entity =>
        {
            entity.ToView("vw_Units");
        });

        modelBuilder.Entity<vw_UnitMatrix>(entity =>
        {
            entity.ToView("vw_UnitMatrix");
        });

        modelBuilder.Entity<vw_UnitMatrix_QCProgress>(entity =>
        {
            entity.ToView("vw_UnitMatrix_QCProgress");
        });

        modelBuilder.Entity<vw_UnitMatrix_QCProgress_V2>(entity =>
        {
            entity.ToView("vw_UnitMatrix_QCProgress_V2");
        });

        modelBuilder.Entity<vw_getRANDValue>(entity =>
        {
            entity.ToView("vw_getRANDValue");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}
