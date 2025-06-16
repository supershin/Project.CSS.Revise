using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

public partial class CSSDbContext : DbContext
{
    public CSSDbContext()
    {
    }

    public CSSDbContext(DbContextOptions<CSSDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BmMasterBank> BmMasterBanks { get; set; }

    public virtual DbSet<BmMasterQuestion> BmMasterQuestions { get; set; }

    public virtual DbSet<BmMasterQuestionAnswer> BmMasterQuestionAnswers { get; set; }

    public virtual DbSet<BmMasterScoreType> BmMasterScoreTypes { get; set; }

    public virtual DbSet<BmMasterSet> BmMasterSets { get; set; }

    public virtual DbSet<BmMasterSetQuestionAnswer> BmMasterSetQuestionAnswers { get; set; }

    public virtual DbSet<BmTrLoanAgeRateBank> BmTrLoanAgeRateBanks { get; set; }

    public virtual DbSet<BmTrQuestionAnswerScore> BmTrQuestionAnswerScores { get; set; }

    public virtual DbSet<BmTrSetScore> BmTrSetScores { get; set; }

    public virtual DbSet<BmTrUserAdmin> BmTrUserAdmins { get; set; }

    public virtual DbSet<BmTsMatching> BmTsMatchings { get; set; }

    public virtual DbSet<BmTsMatchingDetail> BmTsMatchingDetails { get; set; }

    public virtual DbSet<BmTsMatchingQuestionAnswer> BmTsMatchingQuestionAnswers { get; set; }

    public virtual DbSet<BmTsMatchingScoreSet> BmTsMatchingScoreSets { get; set; }

    public virtual DbSet<BmTsMatchingScoreSetDetail> BmTsMatchingScoreSetDetails { get; set; }

    public virtual DbSet<ContactlogPpc001> ContactlogPpc001s { get; set; }

    public virtual DbSet<ContactlogTuc002> ContactlogTuc002s { get; set; }

    public virtual DbSet<DefectArea> DefectAreas { get; set; }

    public virtual DbSet<DefectType20211105> DefectType20211105s { get; set; }

    public virtual DbSet<DefectTypeDesc> DefectTypeDescs { get; set; }

    public virtual DbSet<EstaBlissTemp> EstaBlissTemps { get; set; }

    public virtual DbSet<GetUnitV2> GetUnitV2s { get; set; }

    public virtual DbSet<LinePostLog> LinePostLogs { get; set; }

    public virtual DbSet<LineQrcode> LineQrcodes { get; set; }

    public virtual DbSet<LineRegister> LineRegisters { get; set; }

    public virtual DbSet<LineRegisterQrcode> LineRegisterQrcodes { get; set; }

    public virtual DbSet<LineUser> LineUsers { get; set; }

    public virtual DbSet<LineUserAppointment> LineUserAppointments { get; set; }

    public virtual DbSet<LineUserContract> LineUserContracts { get; set; }

    public virtual DbSet<PrAttachFile> PrAttachFiles { get; set; }

    public virtual DbSet<PrBankDocument> PrBankDocuments { get; set; }

    public virtual DbSet<PrContractVerify> PrContractVerifies { get; set; }

    public virtual DbSet<PrContractVerifyCam002> PrContractVerifyCam002s { get; set; }

    public virtual DbSet<PrCustomer> PrCustomers { get; set; }

    public virtual DbSet<PrCustomerCareer> PrCustomerCareers { get; set; }

    public virtual DbSet<PrCustomerDebt> PrCustomerDebts { get; set; }

    public virtual DbSet<PrCustomerIncome> PrCustomerIncomes { get; set; }

    public virtual DbSet<PrLoan> PrLoans { get; set; }

    public virtual DbSet<PrLoanBank> PrLoanBanks { get; set; }

    public virtual DbSet<PrLoanBankAttachFile> PrLoanBankAttachFiles { get; set; }

    public virtual DbSet<PrLoanBankExplain> PrLoanBankExplains { get; set; }

    public virtual DbSet<PrLoanCustomer> PrLoanCustomers { get; set; }

    public virtual DbSet<PrLoanCustomerAttach> PrLoanCustomerAttaches { get; set; }

    public virtual DbSet<PrLog> PrLogs { get; set; }

    public virtual DbSet<PrProjectBankUserMapping> PrProjectBankUserMappings { get; set; }

    public virtual DbSet<PrProjectCsMapping> PrProjectCsMappings { get; set; }

    public virtual DbSet<PrUser> PrUsers { get; set; }

    public virtual DbSet<PrUserBankMapping> PrUserBankMappings { get; set; }

    public virtual DbSet<PrUserPasswordChange> PrUserPasswordChanges { get; set; }

    public virtual DbSet<QuestionTuc002> QuestionTuc002s { get; set; }

    public virtual DbSet<ReviseArea> ReviseAreas { get; set; }

    public virtual DbSet<ReviseDefectDesc> ReviseDefectDescs { get; set; }

    public virtual DbSet<ReviseDefectType> ReviseDefectTypes { get; set; }

    public virtual DbSet<SysMasterProject> SysMasterProjects { get; set; }

    public virtual DbSet<SysMasterUnit> SysMasterUnits { get; set; }

    public virtual DbSet<SysRemFloor> SysRemFloors { get; set; }

    public virtual DbSet<SysRemTower> SysRemTowers { get; set; }

    public virtual DbSet<TempAbh003> TempAbh003s { get; set; }

    public virtual DbSet<TempAt15> TempAt15s { get; set; }

    public virtual DbSet<TempAt71> TempAt71s { get; set; }

    public virtual DbSet<TempAtchang> TempAtchangs { get; set; }

    public virtual DbSet<TempAthk> TempAthks { get; set; }

    public virtual DbSet<TempAva> TempAvas { get; set; }

    public virtual DbSet<TempBankTarget> TempBankTargets { get; set; }

    public virtual DbSet<TempBr67> TempBr67s { get; set; }

    public virtual DbSet<TempCam002> TempCam002s { get; set; }

    public virtual DbSet<TempCbr002> TempCbr002s { get; set; }

    public virtual DbSet<TempCbr003> TempCbr003s { get; set; }

    public virtual DbSet<TempChecker> TempCheckers { get; set; }

    public virtual DbSet<TempCmd004> TempCmd004s { get; set; }

    public virtual DbSet<TempCmd005> TempCmd005s { get; set; }

    public virtual DbSet<TempCsResponse> TempCsResponses { get; set; }

    public virtual DbSet<TempCustSat20240104> TempCustSat20240104s { get; set; }

    public virtual DbSet<TempDefect> TempDefects { get; set; }

    public virtual DbSet<TempDefectVendor> TempDefectVendors { get; set; }

    public virtual DbSet<TempEditLetter> TempEditLetters { get; set; }

    public virtual DbSet<TempEqc018> TempEqc018s { get; set; }

    public virtual DbSet<TempEqc019> TempEqc019s { get; set; }

    public virtual DbSet<TempEqc020> TempEqc020s { get; set; }

    public virtual DbSet<TempEqc022> TempEqc022s { get; set; }

    public virtual DbSet<TempEqc025> TempEqc025s { get; set; }

    public virtual DbSet<TempJenieUnit> TempJenieUnits { get; set; }

    public virtual DbSet<TempKaveshift> TempKaveshifts { get; set; }

    public virtual DbSet<TempKavespace> TempKavespaces { get; set; }

    public virtual DbSet<TempLetter> TempLetters { get; set; }

    public virtual DbSet<TempLine> TempLines { get; set; }

    public virtual DbSet<TempM1c001> TempM1c001s { get; set; }

    public virtual DbSet<TempModiz> TempModizs { get; set; }

    public virtual DbSet<TempNewBu> TempNewBus { get; set; }

    public virtual DbSet<TempPpc001> TempPpc001s { get; set; }

    public virtual DbSet<TempTuc001> TempTuc001s { get; set; }

    public virtual DbSet<TempTuc002> TempTuc002s { get; set; }

    public virtual DbSet<TempUnit> TempUnits { get; set; }

    public virtual DbSet<TempUnit400h006> TempUnit400h006s { get; set; }

    public virtual DbSet<TempUnitCmd005> TempUnitCmd005s { get; set; }

    public virtual DbSet<TempUnitMaxxi> TempUnitMaxxis { get; set; }

    public virtual DbSet<TempUnitStatus> TempUnitStatuses { get; set; }

    public virtual DbSet<TmAnswer> TmAnswers { get; set; }

    public virtual DbSet<TmBank> TmBanks { get; set; }

    public virtual DbSet<TmBu> TmBus { get; set; }

    public virtual DbSet<TmBuprojectMapping> TmBuprojectMappings { get; set; }

    public virtual DbSet<TmCloseProject> TmCloseProjects { get; set; }

    public virtual DbSet<TmCompany> TmCompanies { get; set; }

    public virtual DbSet<TmDataSource> TmDataSources { get; set; }

    public virtual DbSet<TmDefectArea> TmDefectAreas { get; set; }

    public virtual DbSet<TmDefectAreaTypeMapping> TmDefectAreaTypeMappings { get; set; }

    public virtual DbSet<TmDefectDescription> TmDefectDescriptions { get; set; }

    public virtual DbSet<TmDefectType> TmDefectTypes { get; set; }

    public virtual DbSet<TmEquipment> TmEquipments { get; set; }

    public virtual DbSet<TmEvent> TmEvents { get; set; }

    public virtual DbSet<TmExt> TmExts { get; set; }

    public virtual DbSet<TmExtType> TmExtTypes { get; set; }

    public virtual DbSet<TmFuniture> TmFunitures { get; set; }

    public virtual DbSet<TmHoliday> TmHolidays { get; set; }

    public virtual DbSet<TmLandOffice> TmLandOffices { get; set; }

    public virtual DbSet<TmLetterDayDue> TmLetterDayDues { get; set; }

    public virtual DbSet<TmLetterSendReason> TmLetterSendReasons { get; set; }

    public virtual DbSet<TmLineToken> TmLineTokens { get; set; }

    public virtual DbSet<TmLineTokenBk> TmLineTokenBks { get; set; }

    public virtual DbSet<TmMenu> TmMenus { get; set; }

    public virtual DbSet<TmPosition> TmPositions { get; set; }

    public virtual DbSet<TmProject> TmProjects { get; set; }

    public virtual DbSet<TmProjectUserMapping> TmProjectUserMappings { get; set; }

    public virtual DbSet<TmPromotion> TmPromotions { get; set; }

    public virtual DbSet<TmQc5CheckList> TmQc5CheckLists { get; set; }

    public virtual DbSet<TmQuestion> TmQuestions { get; set; }

    public virtual DbSet<TmResponsibleMapping> TmResponsibleMappings { get; set; }

    public virtual DbSet<TmRole> TmRoles { get; set; }

    public virtual DbSet<TmShop> TmShops { get; set; }

    public virtual DbSet<TmSubject> TmSubjects { get; set; }

    public virtual DbSet<TmTitleName> TmTitleNames { get; set; }

    public virtual DbSet<TmTopic> TmTopics { get; set; }

    public virtual DbSet<TmUnit> TmUnits { get; set; }

    public virtual DbSet<TmUnit20190614> TmUnit20190614s { get; set; }

    public virtual DbSet<TmUnit20210510> TmUnit20210510s { get; set; }

    public virtual DbSet<TmUnitBk02102018> TmUnitBk02102018s { get; set; }

    public virtual DbSet<TmUnitBk11062018> TmUnitBk11062018s { get; set; }

    public virtual DbSet<TmUnitBk30062018> TmUnitBk30062018s { get; set; }

    public virtual DbSet<TmUnitBk31082018> TmUnitBk31082018s { get; set; }

    public virtual DbSet<TmUnitCam00320191223> TmUnitCam00320191223s { get; set; }

    public virtual DbSet<TmUnitStatus> TmUnitStatuses { get; set; }

    public virtual DbSet<TmUser> TmUsers { get; set; }

    public virtual DbSet<TmVendor> TmVendors { get; set; }

    public virtual DbSet<TmWipmatrix> TmWipmatrices { get; set; }

    public virtual DbSet<TmWipmatrixQc6> TmWipmatrixQc6s { get; set; }

    public virtual DbSet<TmpBlkUnit> TmpBlkUnits { get; set; }

    public virtual DbSet<TrAnswerC> TrAnswerCs { get; set; }

    public virtual DbSet<TrApiLog> TrApiLogs { get; set; }

    public virtual DbSet<TrAppointment> TrAppointments { get; set; }

    public virtual DbSet<TrAttachFileQc> TrAttachFileQcs { get; set; }

    public virtual DbSet<TrBankTarget> TrBankTargets { get; set; }

    public virtual DbSet<TrCompanyProject> TrCompanyProjects { get; set; }

    public virtual DbSet<TrContactLog> TrContactLogs { get; set; }

    public virtual DbSet<TrCustomerSatisfaction> TrCustomerSatisfactions { get; set; }

    public virtual DbSet<TrCustomerSatisfactionDetail> TrCustomerSatisfactionDetails { get; set; }

    public virtual DbSet<TrDefectHistory> TrDefectHistories { get; set; }

    public virtual DbSet<TrDefectHistory20240518> TrDefectHistory20240518s { get; set; }

    public virtual DbSet<TrDefectTypeVendorMapping> TrDefectTypeVendorMappings { get; set; }

    public virtual DbSet<TrDefectVendor> TrDefectVendors { get; set; }

    public virtual DbSet<TrDeviceSignIn> TrDeviceSignIns { get; set; }

    public virtual DbSet<TrLetter> TrLetters { get; set; }

    public virtual DbSet<TrLetterAttach> TrLetterAttaches { get; set; }

    public virtual DbSet<TrLetterC> TrLetterCs { get; set; }

    public virtual DbSet<TrLetterLot> TrLetterLots { get; set; }

    public virtual DbSet<TrLetterLotDetail> TrLetterLotDetails { get; set; }

    public virtual DbSet<TrLetterLotResource> TrLetterLotResources { get; set; }

    public virtual DbSet<TrMenuRolePermission> TrMenuRolePermissions { get; set; }

    public virtual DbSet<TrPowerOfAttorney> TrPowerOfAttorneys { get; set; }

    public virtual DbSet<TrProjectAppointLimitMapping> TrProjectAppointLimitMappings { get; set; }

    public virtual DbSet<TrProjectCounterMapping> TrProjectCounterMappings { get; set; }

    public virtual DbSet<TrProjectEmailMapping> TrProjectEmailMappings { get; set; }

    public virtual DbSet<TrProjectFloorPlan> TrProjectFloorPlans { get; set; }

    public virtual DbSet<TrProjectLandOffice> TrProjectLandOffices { get; set; }

    public virtual DbSet<TrProjectShopEvent> TrProjectShopEvents { get; set; }

    public virtual DbSet<TrProjectStatus> TrProjectStatuses { get; set; }

    public virtual DbSet<TrProjectUnitFloorPlan> TrProjectUnitFloorPlans { get; set; }

    public virtual DbSet<TrProjectUserSign> TrProjectUserSigns { get; set; }

    public virtual DbSet<TrProjectZoneMapping> TrProjectZoneMappings { get; set; }

    public virtual DbSet<TrQc1> TrQc1s { get; set; }

    public virtual DbSet<TrQc2> TrQc2s { get; set; }

    public virtual DbSet<TrQc3> TrQc3s { get; set; }

    public virtual DbSet<TrQc4> TrQc4s { get; set; }

    public virtual DbSet<TrQc5> TrQc5s { get; set; }

    public virtual DbSet<TrQc5CheckList> TrQc5CheckLists { get; set; }

    public virtual DbSet<TrQc5CheckListDetail> TrQc5CheckListDetails { get; set; }

    public virtual DbSet<TrQc5FinishPlan> TrQc5FinishPlans { get; set; }

    public virtual DbSet<TrQc5Open> TrQc5Opens { get; set; }

    public virtual DbSet<TrQc5ProjectSendMail> TrQc5ProjectSendMails { get; set; }

    public virtual DbSet<TrQc6> TrQc6s { get; set; }

    public virtual DbSet<TrQc6106c001> TrQc6106c001s { get; set; }

    public virtual DbSet<TrQc6ProjectSendMail> TrQc6ProjectSendMails { get; set; }

    public virtual DbSet<TrQc6Unsold> TrQc6Unsolds { get; set; }

    public virtual DbSet<TrQc6UnsoldSendMail> TrQc6UnsoldSendMails { get; set; }

    public virtual DbSet<TrQcCheckList> TrQcCheckLists { get; set; }

    public virtual DbSet<TrQcContactLog> TrQcContactLogs { get; set; }

    public virtual DbSet<TrQcContactLogResource> TrQcContactLogResources { get; set; }

    public virtual DbSet<TrQcDefect> TrQcDefects { get; set; }

    public virtual DbSet<TrQcDefect20240518> TrQcDefect20240518s { get; set; }

    public virtual DbSet<TrQcDefectDraft> TrQcDefectDrafts { get; set; }

    public virtual DbSet<TrQcDefectOverDueExpect> TrQcDefectOverDueExpects { get; set; }

    public virtual DbSet<TrQcDefectOverDueExpectBk> TrQcDefectOverDueExpectBks { get; set; }

    public virtual DbSet<TrQcDefectOverDueExpectUserPermission> TrQcDefectOverDueExpectUserPermissions { get; set; }

    public virtual DbSet<TrQcDefectResource> TrQcDefectResources { get; set; }

    public virtual DbSet<TrQuestionAnswer> TrQuestionAnswers { get; set; }

    public virtual DbSet<TrQuestionC> TrQuestionCs { get; set; }

    public virtual DbSet<TrReceiveRoomAgreementSign> TrReceiveRoomAgreementSigns { get; set; }

    public virtual DbSet<TrReceiveRoomSign> TrReceiveRoomSigns { get; set; }

    public virtual DbSet<TrRegisterBank> TrRegisterBanks { get; set; }

    public virtual DbSet<TrRegisterBankCounter> TrRegisterBankCounters { get; set; }

    public virtual DbSet<TrRegisterCallStaffCounter> TrRegisterCallStaffCounters { get; set; }

    public virtual DbSet<TrRegisterLog> TrRegisterLogs { get; set; }

    public virtual DbSet<TrRegisterProjectBankStaff> TrRegisterProjectBankStaffs { get; set; }

    public virtual DbSet<TrRemarkUnitStatusMapping> TrRemarkUnitStatusMappings { get; set; }

    public virtual DbSet<TrResource> TrResources { get; set; }

    public virtual DbSet<TrReviseUnitPromotion> TrReviseUnitPromotions { get; set; }

    public virtual DbSet<TrReviseUnitPromotionDetail> TrReviseUnitPromotionDetails { get; set; }

    public virtual DbSet<TrReviseUnitPromotonApprover> TrReviseUnitPromotonApprovers { get; set; }

    public virtual DbSet<TrSignResource> TrSignResources { get; set; }

    public virtual DbSet<TrSync> TrSyncs { get; set; }

    public virtual DbSet<TrSyncLoanBank> TrSyncLoanBanks { get; set; }

    public virtual DbSet<TrSyncLog> TrSyncLogs { get; set; }

    public virtual DbSet<TrSyncQc> TrSyncQcs { get; set; }

    public virtual DbSet<TrTargetRollingPlan> TrTargetRollingPlans { get; set; }

    public virtual DbSet<TrTargetRollingPlanBk> TrTargetRollingPlanBks { get; set; }

    public virtual DbSet<TrTerminateTransferAppoint> TrTerminateTransferAppoints { get; set; }

    public virtual DbSet<TrTerminateTransferAppointDocument> TrTerminateTransferAppointDocuments { get; set; }

    public virtual DbSet<TrTransferAppointHistory> TrTransferAppointHistories { get; set; }

    public virtual DbSet<TrTransferDocument> TrTransferDocuments { get; set; }

    public virtual DbSet<TrUnitDocument> TrUnitDocuments { get; set; }

    public virtual DbSet<TrUnitDocumentNo> TrUnitDocumentNos { get; set; }

    public virtual DbSet<TrUnitEquipment> TrUnitEquipments { get; set; }

    public virtual DbSet<TrUnitEquipmentDetail> TrUnitEquipmentDetails { get; set; }

    public virtual DbSet<TrUnitEvent> TrUnitEvents { get; set; }

    public virtual DbSet<TrUnitFurniture> TrUnitFurnitures { get; set; }

    public virtual DbSet<TrUnitFurnitureDetail> TrUnitFurnitureDetails { get; set; }

    public virtual DbSet<TrUnitPromotion> TrUnitPromotions { get; set; }

    public virtual DbSet<TrUnitPromotionSign> TrUnitPromotionSigns { get; set; }

    public virtual DbSet<TrUnitPromotionSignDetail> TrUnitPromotionSignDetails { get; set; }

    public virtual DbSet<TrUnitShopEvent> TrUnitShopEvents { get; set; }

    public virtual DbSet<TrUnitUserMapping> TrUnitUserMappings { get; set; }

    public virtual DbSet<TrUnsoldRound> TrUnsoldRounds { get; set; }

    public virtual DbSet<TrUnsoldRoundUnit> TrUnsoldRoundUnits { get; set; }

    public virtual DbSet<TrUserPosition> TrUserPositions { get; set; }

    public virtual DbSet<TrUserSignResource> TrUserSignResources { get; set; }

    public virtual DbSet<UnitFixdone> UnitFixdones { get; set; }

    public virtual DbSet<VwBiBacklog> VwBiBacklogs { get; set; }

    public virtual DbSet<VwBiCrmCashbackLtv> VwBiCrmCashbackLtvs { get; set; }

    public virtual DbSet<VwBiProject> VwBiProjects { get; set; }

    public virtual DbSet<VwBiProjectInitialMonth> VwBiProjectInitialMonths { get; set; }

    public virtual DbSet<VwBiTransferActual> VwBiTransferActuals { get; set; }

    public virtual DbSet<VwBiTransferNetActual> VwBiTransferNetActuals { get; set; }

    public virtual DbSet<VwBiTransferTargetRolling> VwBiTransferTargetRollings { get; set; }

    public virtual DbSet<VwBiTransferTargetRollingActual> VwBiTransferTargetRollingActuals { get; set; }

    public virtual DbSet<VwContractBankAccount> VwContractBankAccounts { get; set; }

    public virtual DbSet<VwDefect> VwDefects { get; set; }

    public virtual DbSet<VwGetRandvalue> VwGetRandvalues { get; set; }

    public virtual DbSet<VwItfTempBlakUnit> VwItfTempBlakUnits { get; set; }

    public virtual DbSet<VwUnit> VwUnits { get; set; }

    public virtual DbSet<VwUnitMatrix> VwUnitMatrices { get; set; }

    public virtual DbSet<VwUnitMatrixQcprogress> VwUnitMatrixQcprogresses { get; set; }

    public virtual DbSet<VwUnitMatrixQcprogressV2> VwUnitMatrixQcprogressV2s { get; set; }

    public virtual DbSet<ZImportUnit> ZImportUnits { get; set; }

    public virtual DbSet<ZM1111> ZM1111s { get; set; }

    public virtual DbSet<ZW1111> ZW1111s { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=10.0.10.8;Initial Catalog=CSS;User ID=sittikron;Password=sittikron@2025;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Thai_CI_AS");

        modelBuilder.Entity<BmMasterBank>(entity =>
        {
            entity.ToTable("BM_Master_Bank");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BankId).HasColumnName("BankID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Bank).WithMany(p => p.BmMasterBanks)
                .HasForeignKey(d => d.BankId)
                .HasConstraintName("FK_BM_Master_Bank_tm_Bank");
        });

        modelBuilder.Entity<BmMasterQuestion>(entity =>
        {
            entity.ToTable("BM_Master_Question");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(1000);
            entity.Property(e => e.ParentAnswerId).HasColumnName("ParentAnswerID");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ValueFrom).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ValueTo).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<BmMasterQuestionAnswer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_BM_Master_Answer");

            entity.ToTable("BM_Master_QuestionAnswer");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CompareFrom).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CompareTo).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(1000);
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Question).WithMany(p => p.BmMasterQuestionAnswers)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK_BM_Master_QuestionAnswer_BM_Master_Question");
        });

        modelBuilder.Entity<BmMasterScoreType>(entity =>
        {
            entity.ToTable("BM_Master_ScoreType");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(1000);
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<BmMasterSet>(entity =>
        {
            entity.ToTable("BM_Master_Set");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(500);
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<BmMasterSetQuestionAnswer>(entity =>
        {
            entity.ToTable("BM_Master_Set_QuestionAnswer");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AnswerId).HasColumnName("AnswerID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.SetId).HasColumnName("SetID");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Answer).WithMany(p => p.BmMasterSetQuestionAnswers)
                .HasForeignKey(d => d.AnswerId)
                .HasConstraintName("FK_BM_Master_Set_QuestionAnswer_BM_Master_QuestionAnswer");

            entity.HasOne(d => d.Question).WithMany(p => p.BmMasterSetQuestionAnswers)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK_BM_Master_Set_QuestionAnswer_BM_Master_Question");

            entity.HasOne(d => d.Set).WithMany(p => p.BmMasterSetQuestionAnswers)
                .HasForeignKey(d => d.SetId)
                .HasConstraintName("FK_BM_Master_Set_QuestionAnswer_BM_Master_Set");
        });

        modelBuilder.Entity<BmTrLoanAgeRateBank>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_BM_TR_LoanBank_Calculate");

            entity.ToTable("BM_TR_LoanAgeRate_Bank");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AverageRate).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.BankId).HasColumnName("BankID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.Rate).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Bank).WithMany(p => p.BmTrLoanAgeRateBanks)
                .HasForeignKey(d => d.BankId)
                .HasConstraintName("FK_BM_TR_LoanAgeRate_Bank_tm_Bank");
        });

        modelBuilder.Entity<BmTrQuestionAnswerScore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_BM_TR_BankScore");

            entity.ToTable("BM_TR_QuestionAnswerScore");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AnswerId).HasColumnName("AnswerID");
            entity.Property(e => e.BankId).HasColumnName("BankID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.ScoreTypeId).HasColumnName("ScoreTypeID");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Answer).WithMany(p => p.BmTrQuestionAnswerScores)
                .HasForeignKey(d => d.AnswerId)
                .HasConstraintName("FK_BM_TR_BankScore_BM_Master_QuestionAnswer");

            entity.HasOne(d => d.Bank).WithMany(p => p.BmTrQuestionAnswerScores)
                .HasForeignKey(d => d.BankId)
                .HasConstraintName("FK_BM_TR_BankScore_tm_Bank");

            entity.HasOne(d => d.Question).WithMany(p => p.BmTrQuestionAnswerScores)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK_BM_TR_BankScore_BM_Master_Question");

            entity.HasOne(d => d.ScoreType).WithMany(p => p.BmTrQuestionAnswerScores)
                .HasForeignKey(d => d.ScoreTypeId)
                .HasConstraintName("FK_BM_TR_BankScore_BM_Master_ScoreType");
        });

        modelBuilder.Entity<BmTrSetScore>(entity =>
        {
            entity.ToTable("BM_TR_Set_Score");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BankId).HasColumnName("BankID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.ScoreTypeId).HasColumnName("ScoreTypeID");
            entity.Property(e => e.SetId).HasColumnName("SetID");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Bank).WithMany(p => p.BmTrSetScores)
                .HasForeignKey(d => d.BankId)
                .HasConstraintName("FK_BM_TR_Set_Score_tm_Bank");

            entity.HasOne(d => d.ScoreType).WithMany(p => p.BmTrSetScores)
                .HasForeignKey(d => d.ScoreTypeId)
                .HasConstraintName("FK_BM_TR_Set_Score_BM_Master_ScoreType");

            entity.HasOne(d => d.Set).WithMany(p => p.BmTrSetScores)
                .HasForeignKey(d => d.SetId)
                .HasConstraintName("FK_BM_TR_Set_Score_BM_Master_Set");
        });

        modelBuilder.Entity<BmTrUserAdmin>(entity =>
        {
            entity.ToTable("BM_TR_UserAdmin");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.BmTrUserAdmins)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_BM_TR_UserAdmin_tm_User");
        });

        modelBuilder.Entity<BmTsMatching>(entity =>
        {
            entity.ToTable("BM_TS_Matching");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DebtTotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Email)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FirstName).HasMaxLength(500);
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.IncomeTotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.LastName).HasMaxLength(1000);
            entity.Property(e => e.LoanAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MatchingDate).HasColumnType("datetime");
            entity.Property(e => e.Mobile)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<BmTsMatchingDetail>(entity =>
        {
            entity.ToTable("BM_TS_Matching_Detail");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AssessmentScorePerCent).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BankAverageRate).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.BankId).HasColumnName("BankID");
            entity.Property(e => e.BankRate).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MatchingId).HasColumnName("MatchingID");
            entity.Property(e => e.MonthlyInstallment).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Bank).WithMany(p => p.BmTsMatchingDetails)
                .HasForeignKey(d => d.BankId)
                .HasConstraintName("FK_BM_TS_Matching_Detail_tm_Bank");

            entity.HasOne(d => d.Matching).WithMany(p => p.BmTsMatchingDetails)
                .HasForeignKey(d => d.MatchingId)
                .HasConstraintName("FK_BM_TS_Matching_Detail_BM_TS_Matching");
        });

        modelBuilder.Entity<BmTsMatchingQuestionAnswer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TS_BM_Matching_QuestionAnswer");

            entity.ToTable("BM_TS_Matching_QuestionAnswer");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AnswerId).HasColumnName("AnswerID");
            entity.Property(e => e.BankId).HasColumnName("BankID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Hdp).HasColumnName("HDP");
            entity.Property(e => e.MatchingId).HasColumnName("MatchingID");
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Answer).WithMany(p => p.BmTsMatchingQuestionAnswers)
                .HasForeignKey(d => d.AnswerId)
                .HasConstraintName("FK_BM_TS_Matching_QuestionAnswer_BM_Master_QuestionAnswer");

            entity.HasOne(d => d.Bank).WithMany(p => p.BmTsMatchingQuestionAnswers)
                .HasForeignKey(d => d.BankId)
                .HasConstraintName("FK_BM_TS_Matching_QuestionAnswer_tm_Bank");

            entity.HasOne(d => d.Matching).WithMany(p => p.BmTsMatchingQuestionAnswers)
                .HasForeignKey(d => d.MatchingId)
                .HasConstraintName("FK_BM_TS_Matching_QuestionAnswer_BM_TS_Matching");

            entity.HasOne(d => d.Question).WithMany(p => p.BmTsMatchingQuestionAnswers)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK_BM_TS_Matching_QuestionAnswer_BM_Master_Question");
        });

        modelBuilder.Entity<BmTsMatchingScoreSet>(entity =>
        {
            entity.ToTable("BM_TS_Matching_ScoreSet");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BankId).HasColumnName("BankID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Hdp).HasColumnName("HDP");
            entity.Property(e => e.MatchingId).HasColumnName("MatchingID");
            entity.Property(e => e.SetId).HasColumnName("SetID");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Bank).WithMany(p => p.BmTsMatchingScoreSets)
                .HasForeignKey(d => d.BankId)
                .HasConstraintName("FK_BM_TS_Matching_ScoreSet_tm_Bank");

            entity.HasOne(d => d.Matching).WithMany(p => p.BmTsMatchingScoreSets)
                .HasForeignKey(d => d.MatchingId)
                .HasConstraintName("FK_BM_TS_Matching_ScoreSet_BM_TS_Matching");

            entity.HasOne(d => d.Set).WithMany(p => p.BmTsMatchingScoreSets)
                .HasForeignKey(d => d.SetId)
                .HasConstraintName("FK_BM_TS_Matching_ScoreSet_BM_Master_Set");
        });

        modelBuilder.Entity<BmTsMatchingScoreSetDetail>(entity =>
        {
            entity.ToTable("BM_TS_Matching_ScoreSet_Detail");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AnswerId).HasColumnName("AnswerID");
            entity.Property(e => e.MatchingId).HasColumnName("MatchingID");
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.SetId).HasColumnName("SetID");

            entity.HasOne(d => d.Answer).WithMany(p => p.BmTsMatchingScoreSetDetails)
                .HasForeignKey(d => d.AnswerId)
                .HasConstraintName("FK_BM_TS_Matching_ScoreSet_Detail_BM_Master_QuestionAnswer");

            entity.HasOne(d => d.Matching).WithMany(p => p.BmTsMatchingScoreSetDetails)
                .HasForeignKey(d => d.MatchingId)
                .HasConstraintName("FK_BM_TS_Matching_ScoreSet_Detail_BM_TS_Matching");

            entity.HasOne(d => d.Question).WithMany(p => p.BmTsMatchingScoreSetDetails)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK_BM_TS_Matching_ScoreSet_Detail_BM_Master_Question");

            entity.HasOne(d => d.Set).WithMany(p => p.BmTsMatchingScoreSetDetails)
                .HasForeignKey(d => d.SetId)
                .HasConstraintName("FK_BM_TS_Matching_ScoreSet_Detail_BM_Master_Set");
        });

        modelBuilder.Entity<ContactlogPpc001>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("contactlog_PPC001");

            entity.Property(e => e.BankCode).HasMaxLength(255);
            entity.Property(e => e.ContactDate).HasColumnType("datetime");
            entity.Property(e => e.ContactName).HasMaxLength(255);
            entity.Property(e => e.Csrespoonse)
                .HasMaxLength(255)
                .HasColumnName("CSRespoonse");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(255)
                .HasColumnName("ProjectID");
            entity.Property(e => e.Unit).HasMaxLength(255);
        });

        modelBuilder.Entity<ContactlogTuc002>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("contactlog_TUC002");

            entity.Property(e => e.BankCode).HasMaxLength(255);
            entity.Property(e => e.ContactDate).HasColumnType("datetime");
            entity.Property(e => e.ContactName).HasMaxLength(255);
            entity.Property(e => e.ContactTime)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Csrespoonse)
                .HasMaxLength(255)
                .HasColumnName("CSRespoonse");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(255)
                .HasColumnName("ProjectID");
            entity.Property(e => e.UnitCode).HasMaxLength(255);
        });

        modelBuilder.Entity<DefectArea>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("DefectArea");

            entity.Property(e => e.DefectArea1)
                .HasMaxLength(255)
                .HasColumnName("DefectArea");
        });

        modelBuilder.Entity<DefectType20211105>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("defect_type_20211105");

            entity.Property(e => e.DefectArea).HasMaxLength(255);
            entity.Property(e => e.DefectDescription).HasMaxLength(255);
            entity.Property(e => e.DefectType).HasMaxLength(255);
        });

        modelBuilder.Entity<DefectTypeDesc>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("DefectTypeDesc");

            entity.Property(e => e.DefectDescription).HasMaxLength(255);
            entity.Property(e => e.DefectType).HasMaxLength(255);
        });

        modelBuilder.Entity<EstaBlissTemp>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("EstaBliss_Temp");

            entity.Property(e => e.AdjustStatus).HasMaxLength(255);
            entity.Property(e => e.ExpectDate).HasColumnType("datetime");
            entity.Property(e => e.Transfer).HasMaxLength(255);
            entity.Property(e => e.UnitCode).HasMaxLength(255);
        });

        modelBuilder.Entity<GetUnitV2>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("GetUnit_V2");

            entity.Property(e => e.AddrNo).HasMaxLength(500);
            entity.Property(e => e.AppointDate)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Appoint_Date");
            entity.Property(e => e.Area).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BankSelectedCs)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("BankSelected_CS");
            entity.Property(e => e.BookDate)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Build)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CodeVerify)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ContactLogDate)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ContactLog_Date");
            entity.Property(e => e.ContractDate)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ContractMobile).IsUnicode(false);
            entity.Property(e => e.ContractNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Csresponse)
                .HasMaxLength(200)
                .HasColumnName("CSResponse");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.CustomerMobile)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CustomerName).HasMaxLength(500);
            entity.Property(e => e.DefectStatusName).HasMaxLength(400);
            entity.Property(e => e.ExpectTransfer)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ExpectTransferBy).HasMaxLength(50);
            entity.Property(e => e.ExpectTransferDeviate)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.FinanceCareDraft)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FinplusSubmitDate)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.FreeAll).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.InspectCount).HasColumnName("Inspect_Count");
            entity.Property(e => e.InspectDate)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Inspect_Date");
            entity.Property(e => e.LastExpectDate)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.LetterDueDateCs)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("LetterDueDate_CS");
            entity.Property(e => e.LoanBankNameFinplus)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("LoanBankName_FINPlus");
            entity.Property(e => e.LoanBankNameSelect)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("LoanBankName_Select");
            entity.Property(e => e.LoanStatusNameFinplus)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("LoanStatusName_FINPlus");
            entity.Property(e => e.MeterTypeName).HasMaxLength(100);
            entity.Property(e => e.PreapproveFinplus)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Preapprove_FINPlus");
            entity.Property(e => e.Qc5FinishDate)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("QC5_FinishDate");
            entity.Property(e => e.Qc6).HasColumnName("QC6");
            entity.Property(e => e.Qc6Date)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("QC6_Date");
            entity.Property(e => e.Qc6Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("QC6_Status");
            entity.Property(e => e.ReceiveDocument)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ReceiveRoomAgreementDate)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ReceiveRoomDate)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Redemption)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RemarkUnitStatusCs)
                .HasMaxLength(100)
                .HasColumnName("RemarkUnitStatus_CS");
            entity.Property(e => e.SaveDocument)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SellingPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TransferDate)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TransferDueDateCs)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("TransferDueDate_CS");
            entity.Property(e => e.TransferNetPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UnitCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UnitStatus).HasMaxLength(50);
            entity.Property(e => e.UnitStatusCs)
                .HasMaxLength(100)
                .HasColumnName("UnitStatus_CS");
            entity.Property(e => e.UnitType)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LinePostLog>(entity =>
        {
            entity.ToTable("Line_PostLog");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("createDate");
            entity.Property(e => e.MessageId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("message_id");
            entity.Property(e => e.MessagePackageId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("message_packageId");
            entity.Property(e => e.MessageStickerId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("message_stickerId");
            entity.Property(e => e.MessageStickerResourceType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("message_stickerResourceType");
            entity.Property(e => e.MessageText).HasColumnName("message_text");
            entity.Property(e => e.MessageType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("message_type");
            entity.Property(e => e.ReplyToken)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("replyToken");
            entity.Property(e => e.SourceGroupId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("source_groupId");
            entity.Property(e => e.SourceRoomId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("source_roomId");
            entity.Property(e => e.SourceUserId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("source_userId");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("type");
        });

        modelBuilder.Entity<LineQrcode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Line_Code");

            entity.ToTable("Line_QRCode");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<LineRegister>(entity =>
        {
            entity.ToTable("Line_Register");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.LastName).HasMaxLength(250);
            entity.Property(e => e.LineUserId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("LineUserID");
            entity.Property(e => e.Mobile)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RegisterDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<LineRegisterQrcode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Line_RegisterCode");

            entity.ToTable("Line_RegisterQRCode");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.ExpireDate).HasColumnType("datetime");
            entity.Property(e => e.QrcodeId).HasColumnName("QRCodeID");
            entity.Property(e => e.RegisterId).HasColumnName("RegisterID");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Qrcode).WithMany(p => p.LineRegisterQrcodes)
                .HasForeignKey(d => d.QrcodeId)
                .HasConstraintName("FK_Line_RegisterQRCode_Line_QRCode");

            entity.HasOne(d => d.Register).WithMany(p => p.LineRegisterQrcodes)
                .HasForeignKey(d => d.RegisterId)
                .HasConstraintName("FK_Line_RegisterQRCode_Line_Register");
        });

        modelBuilder.Entity<LineUser>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("Line_User");

            entity.Property(e => e.UserId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DisplayName).HasMaxLength(500);
            entity.Property(e => e.Language)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PictureUrl).IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<LineUserAppointment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Line_Appointment");

            entity.ToTable("Line_UserAppointment");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");
            entity.Property(e => e.LineUserId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("LineUserID");

            entity.HasOne(d => d.Appointment).WithMany(p => p.LineUserAppointments)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK_Line_Appointment_TR_Appointment");
        });

        modelBuilder.Entity<LineUserContract>(entity =>
        {
            entity.ToTable("Line_UserContracts");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ContractNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.LineUserId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("LineUserID");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.LineUser).WithMany(p => p.LineUserContracts)
                .HasForeignKey(d => d.LineUserId)
                .HasConstraintName("FK_Line_UserContracts_Line_User");

            entity.HasOne(d => d.Project).WithMany(p => p.LineUserContracts)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_Line_UserContracts_tm_Project");

            entity.HasOne(d => d.Unit).WithMany(p => p.LineUserContracts)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_Line_UserContracts_tm_Unit");
        });

        modelBuilder.Entity<PrAttachFile>(entity =>
        {
            entity.ToTable("PR_AttachFile");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AttachTypeId).HasColumnName("AttachTypeID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FileName).HasMaxLength(500);
            entity.Property(e => e.FilePath).HasMaxLength(500);
            entity.Property(e => e.IdcardNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("IDCardNo");
            entity.Property(e => e.MimeType)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserTypeId).HasColumnName("UserTypeID");

            entity.HasOne(d => d.AttachType).WithMany(p => p.PrAttachFileAttachTypes)
                .HasForeignKey(d => d.AttachTypeId)
                .HasConstraintName("FK_PR_AttachFile_tm_Ext1");

            entity.HasOne(d => d.UserType).WithMany(p => p.PrAttachFileUserTypes)
                .HasForeignKey(d => d.UserTypeId)
                .HasConstraintName("FK_PR_AttachFile_tm_Ext");
        });

        modelBuilder.Entity<PrBankDocument>(entity =>
        {
            entity.ToTable("PR_BankDocument");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.BankId).HasColumnName("BankID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DocumentName).HasMaxLength(500);
            entity.Property(e => e.FileName).HasMaxLength(500);
            entity.Property(e => e.FilePath).HasMaxLength(500);
            entity.Property(e => e.MimeType)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Bank).WithMany(p => p.PrBankDocuments)
                .HasForeignKey(d => d.BankId)
                .HasConstraintName("FK_PR_BankDocument_tm_Bank");
        });

        modelBuilder.Entity<PrContractVerify>(entity =>
        {
            entity.ToTable("PR_ContractVerify");

            entity.HasIndex(e => new { e.ProjectId, e.UnitCode, e.ContractNumber, e.CodeVerify }, "NonClusteredIndex-20201109-102759");

            entity.HasIndex(e => new { e.ProjectId, e.UnitCode, e.ContractNumber, e.CodeVerify }, "NonClusteredIndex-20220825-134044");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CodeVerify)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ContractMobile).IsUnicode(false);
            entity.Property(e => e.ContractNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ContractSellingPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.UnitCode)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PrContractVerifyCam002>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PR_ContractVerify_CAM002");

            entity.Property(e => e.CodeVerify)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ContractMobile).IsUnicode(false);
            entity.Property(e => e.ContractNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ContractSellingPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.UnitCode)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PrCustomer>(entity =>
        {
            entity.ToTable("PR_Customer");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AddressName).HasMaxLength(2000);
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.District).HasMaxLength(200);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.IdcardNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("IDCardNo");
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Mobile)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Province).HasMaxLength(200);
            entity.Property(e => e.SubDistrict).HasMaxLength(200);
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PrCustomerCareer>(entity =>
        {
            entity.ToTable("PR_CustomerCareer");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CareerId).HasColumnName("CareerID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.LoanId).HasColumnName("LoanID");

            entity.HasOne(d => d.Career).WithMany(p => p.PrCustomerCareers)
                .HasForeignKey(d => d.CareerId)
                .HasConstraintName("FK_PR_CustomerCareer_tm_Ext");

            entity.HasOne(d => d.Customer).WithMany(p => p.PrCustomerCareers)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_PR_CustomerCareer_PR_Customer");

            entity.HasOne(d => d.Loan).WithMany(p => p.PrCustomerCareers)
                .HasForeignKey(d => d.LoanId)
                .HasConstraintName("FK_PR_CustomerCareer_PR_Loan");
        });

        modelBuilder.Entity<PrCustomerDebt>(entity =>
        {
            entity.ToTable("PR_CustomerDebt");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.DebtId).HasColumnName("DebtID");
            entity.Property(e => e.LoanId).HasColumnName("LoanID");
            entity.Property(e => e.OtherValue).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Customer).WithMany(p => p.PrCustomerDebts)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_PR_CustomerDebt_PR_Customer");

            entity.HasOne(d => d.Debt).WithMany(p => p.PrCustomerDebts)
                .HasForeignKey(d => d.DebtId)
                .HasConstraintName("FK_PR_CustomerDebt_tm_Ext");

            entity.HasOne(d => d.Loan).WithMany(p => p.PrCustomerDebts)
                .HasForeignKey(d => d.LoanId)
                .HasConstraintName("FK_PR_CustomerDebt_PR_Loan");
        });

        modelBuilder.Entity<PrCustomerIncome>(entity =>
        {
            entity.ToTable("PR_CustomerIncome");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.IncomeId).HasColumnName("IncomeID");
            entity.Property(e => e.LoanId).HasColumnName("LoanID");
            entity.Property(e => e.OtherValue).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Customer).WithMany(p => p.PrCustomerIncomes)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_PR_CustomerIncome_PR_Customer");

            entity.HasOne(d => d.Income).WithMany(p => p.PrCustomerIncomes)
                .HasForeignKey(d => d.IncomeId)
                .HasConstraintName("FK_PR_CustomerIncome_tm_Ext");

            entity.HasOne(d => d.Loan).WithMany(p => p.PrCustomerIncomes)
                .HasForeignKey(d => d.LoanId)
                .HasConstraintName("FK_PR_CustomerIncome_PR_Loan");
        });

        modelBuilder.Entity<PrLoan>(entity =>
        {
            entity.ToTable("PR_Loan");

            entity.HasIndex(e => new { e.ProjectId, e.UnitCode, e.ContractNumber, e.DraftDate, e.SubmitDate }, "NonClusteredIndex-20201109-102716");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ConsentSubscribeBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ContractMobile).HasMaxLength(200);
            entity.Property(e => e.ContractNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ContractSellingPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DraftBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DraftDate).HasColumnType("datetime");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.SubmitBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SubmitDate).HasColumnType("datetime");
            entity.Property(e => e.UnitCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.UserTypeId).HasColumnName("UserTypeID");

            entity.HasOne(d => d.Project).WithMany(p => p.PrLoans)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_PR_Loan_tm_Project");

            entity.HasOne(d => d.UserType).WithMany(p => p.PrLoans)
                .HasForeignKey(d => d.UserTypeId)
                .HasConstraintName("FK_PR_Loan_tm_Ext");
        });

        modelBuilder.Entity<PrLoanBank>(entity =>
        {
            entity.ToTable("PR_LoanBank");

            entity.HasIndex(e => new { e.LoanId, e.BankId, e.BankProgressId, e.BankStatusId, e.DraftDate, e.SubmitDate }, "NonClusteredIndex-20201109-102602");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ApproveDate).HasColumnType("datetime");
            entity.Property(e => e.BankId).HasColumnName("BankID");
            entity.Property(e => e.BankProgressId).HasColumnName("BankProgressID");
            entity.Property(e => e.BankStaffMobile)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.BankStatusId).HasColumnName("BankStatusID");
            entity.Property(e => e.ClearSubmitBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ClearSubmitDate).HasColumnType("datetime");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DecorationCreditLine).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DraftBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DraftDate).HasColumnType("datetime");
            entity.Property(e => e.FireInsurance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.HousingLoanLimit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.LessFirstInstallment).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.LoanId).HasColumnName("LoanID");
            entity.Property(e => e.MarriedStatusId).HasColumnName("MarriedStatusID");
            entity.Property(e => e.MortgageTypeId).HasColumnName("MortgageTypeID");
            entity.Property(e => e.Mrta)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("MRTA");
            entity.Property(e => e.OtherApprove).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PersonalLoan2).HasColumnName("PersonalLoan_2");
            entity.Property(e => e.PersonalOwner1).HasColumnName("PersonalOwner_1");
            entity.Property(e => e.PersonalOwner2).HasColumnName("PersonalOwner_2");
            entity.Property(e => e.PersonalOwner3).HasColumnName("PersonalOwner_3");
            entity.Property(e => e.PreDecorationCreditLine).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PreFireInsurance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PreHousingLoanLimit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PreTotalApprove).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.RejectCooperative).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.RejectFn)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("RejectFN");
            entity.Property(e => e.RejectHl)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("RejectHL");
            entity.Property(e => e.RejectOther).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.RejectPl)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("RejectPL");
            entity.Property(e => e.RejectReasonId).HasColumnName("RejectReasonID");
            entity.Property(e => e.RevenueStamp).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SubmitBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SubmitDate).HasColumnType("datetime");
            entity.Property(e => e.TotalApprove).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TransferDueDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserTypeId).HasColumnName("UserTypeID");
        });

        modelBuilder.Entity<PrLoanBankAttachFile>(entity =>
        {
            entity.ToTable("PR_LoanBankAttachFile");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FileName).HasMaxLength(500);
            entity.Property(e => e.FilePath).HasMaxLength(500);
            entity.Property(e => e.LoanBankId).HasColumnName("LoanBankID");
            entity.Property(e => e.LoanId).HasColumnName("LoanID");
            entity.Property(e => e.MimeType)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.LoanBank).WithMany(p => p.PrLoanBankAttachFiles)
                .HasForeignKey(d => d.LoanBankId)
                .HasConstraintName("FK_PR_LoanBankAttachFile_PR_LoanBank");

            entity.HasOne(d => d.Loan).WithMany(p => p.PrLoanBankAttachFiles)
                .HasForeignKey(d => d.LoanId)
                .HasConstraintName("FK_PR_LoanBankAttachFile_PR_Loan");
        });

        modelBuilder.Entity<PrLoanBankExplain>(entity =>
        {
            entity.HasKey(e => e.LoanBankId);

            entity.ToTable("PR_LoanBank_Explain");

            entity.Property(e => e.LoanBankId)
                .ValueGeneratedNever()
                .HasColumnName("LoanBankID");
            entity.Property(e => e.ExBonus).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ExCommission).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ExCooperativeLoan).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ExCreditCard).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ExFinanceLoan).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ExHousingLoan).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ExIncentive).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ExOtherIncome).HasMaxLength(2000);
            entity.Property(e => e.ExOtherIncomeValue).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ExOtherLoan).HasMaxLength(2000);
            entity.Property(e => e.ExOtherLoanValue).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ExOvertime).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ExPersonalLoan).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ExSalary).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.LoanBank).WithOne(p => p.PrLoanBankExplain)
                .HasForeignKey<PrLoanBankExplain>(d => d.LoanBankId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PR_LoanBank_Explain_PR_LoanBank");
        });

        modelBuilder.Entity<PrLoanCustomer>(entity =>
        {
            entity.ToTable("PR_LoanCustomer");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.LoanId).HasColumnName("LoanID");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.PrLoanCustomers)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_PR_LoanCustomer_PR_Customer");

            entity.HasOne(d => d.Loan).WithMany(p => p.PrLoanCustomers)
                .HasForeignKey(d => d.LoanId)
                .HasConstraintName("FK_PR_LoanCustomer_PR_Loan");
        });

        modelBuilder.Entity<PrLoanCustomerAttach>(entity =>
        {
            entity.ToTable("PR_LoanCustomerAttach");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AttachFileId).HasColumnName("AttachFileID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.LoanId).HasColumnName("LoanID");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.AttachFile).WithMany(p => p.PrLoanCustomerAttaches)
                .HasForeignKey(d => d.AttachFileId)
                .HasConstraintName("FK_PR_LoanCustomerAttach_PR_AttachFile");

            entity.HasOne(d => d.Customer).WithMany(p => p.PrLoanCustomerAttaches)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_PR_LoanCustomerAttach_PR_Customer");

            entity.HasOne(d => d.Loan).WithMany(p => p.PrLoanCustomerAttaches)
                .HasForeignKey(d => d.LoanId)
                .HasConstraintName("FK_PR_LoanCustomerAttach_PR_Loan");
        });

        modelBuilder.Entity<PrLog>(entity =>
        {
            entity.ToTable("PR_Logs");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Action)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Controller)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PrProjectBankUserMapping>(entity =>
        {
            entity.ToTable("PR_ProjectBankUser_Mapping");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BankUserId).HasColumnName("BankUserID");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
        });

        modelBuilder.Entity<PrProjectCsMapping>(entity =>
        {
            entity.ToTable("PR_ProjectCS_Mapping");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<PrUser>(entity =>
        {
            entity.ToTable("PR_User");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FirstName).HasMaxLength(200);
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.LastName).HasMaxLength(200);
            entity.Property(e => e.Mobile).HasMaxLength(100);
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserTypeId).HasColumnName("UserTypeID");

            entity.HasOne(d => d.UserType).WithMany(p => p.PrUsers)
                .HasForeignKey(d => d.UserTypeId)
                .HasConstraintName("FK_PR_User_tm_Ext");
        });

        modelBuilder.Entity<PrUserBankMapping>(entity =>
        {
            entity.ToTable("PR_UserBank_Mapping");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BankId).HasColumnName("BankID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Bank).WithMany(p => p.PrUserBankMappings)
                .HasForeignKey(d => d.BankId)
                .HasConstraintName("FK_PR_UserBamk_Mapping_tm_Bank");

            entity.HasOne(d => d.User).WithMany(p => p.PrUserBankMappings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_PR_UserBamk_Mapping_PR_User");
        });

        modelBuilder.Entity<PrUserPasswordChange>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PR_PasswordChange");

            entity.ToTable("PR_User_PasswordChange");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ChangeDate).HasColumnType("datetime");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.PrUserPasswordChanges)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_PR_User_PasswordChange_PR_User");
        });

        modelBuilder.Entity<QuestionTuc002>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("question_tuc002");

            entity.Property(e => e.AnswerId).HasColumnName("AnswerID");
            entity.Property(e => e.ClearDate).HasColumnType("datetime");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.QuestionDate).HasColumnType("datetime");
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.UdateDate).HasColumnType("datetime");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
        });

        modelBuilder.Entity<ReviseArea>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("revise_Area");

            entity.Property(e => e.DefectArea).HasMaxLength(255);
            entity.Property(e => e.DefectAreaId).HasColumnName("DefectAreaID");
        });

        modelBuilder.Entity<ReviseDefectDesc>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("revise_DefectDesc");

            entity.Property(e => e.DefectDiscription).HasMaxLength(255);
            entity.Property(e => e.DefectType).HasMaxLength(255);
            entity.Property(e => e.DefectTypeId).HasColumnName("DefectTypeID");
        });

        modelBuilder.Entity<ReviseDefectType>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("revise_DefectType");

            entity.Property(e => e.DefectType).HasMaxLength(255);
            entity.Property(e => e.DefectTypeId).HasColumnName("DefectTypeID");
        });

        modelBuilder.Entity<SysMasterProject>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Sys_Master_Projects");

            entity.Property(e => e.AbProjectName).HasMaxLength(255);
            entity.Property(e => e.AccountProject).HasMaxLength(50);
            entity.Property(e => e.Accwbscode)
                .HasMaxLength(20)
                .HasColumnName("ACCWBSCode");
            entity.Property(e => e.AllocateLand).HasMaxLength(50);
            entity.Property(e => e.AllowSendSap).HasColumnName("AllowSendSAP");
            entity.Property(e => e.AreaSquareWah).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.Base64Image).HasColumnType("text");
            entity.Property(e => e.Boqid)
                .HasMaxLength(50)
                .HasColumnName("BOQID");
            entity.Property(e => e.BrandId)
                .HasMaxLength(15)
                .HasColumnName("BrandID");
            entity.Property(e => e.BudgetAlertAmt).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.Buid)
                .HasMaxLength(50)
                .HasColumnName("BUID");
            entity.Property(e => e.BuildCompleteDate).HasColumnType("datetime");
            entity.Property(e => e.CompanyId)
                .HasMaxLength(15)
                .HasColumnName("CompanyID");
            entity.Property(e => e.Comwbscode)
                .HasMaxLength(20)
                .HasColumnName("COMWBSCode");
            entity.Property(e => e.ContractType).HasMaxLength(20);
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.GradeId).HasColumnName("GradeID");
            entity.Property(e => e.ImgPath).HasMaxLength(50);
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.IsRenovate).HasColumnName("isRenovate");
            entity.Property(e => e.IsReserve).HasColumnName("isReserve");
            entity.Property(e => e.JuristicDate).HasColumnType("datetime");
            entity.Property(e => e.JuristicId).HasColumnName("JuristicID");
            entity.Property(e => e.JuristicName).HasMaxLength(50);
            entity.Property(e => e.JuristicNameEng).HasMaxLength(50);
            entity.Property(e => e.JuristicStatus).HasMaxLength(5);
            entity.Property(e => e.LineOfficial).HasMaxLength(255);
            entity.Property(e => e.MoFinanceNameEn)
                .HasMaxLength(255)
                .HasColumnName("MoFinanceNameEN");
            entity.Property(e => e.MoFinanceNameTh)
                .HasMaxLength(255)
                .HasColumnName("MoFinanceNameTH");
            entity.Property(e => e.ModifyBy).HasMaxLength(50);
            entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            entity.Property(e => e.PlantCode).HasMaxLength(20);
            entity.Property(e => e.Port).HasMaxLength(255);
            entity.Property(e => e.ProjectClose).HasColumnType("datetime");
            entity.Property(e => e.ProjectEmail).HasMaxLength(100);
            entity.Property(e => e.ProjectFax).HasMaxLength(20);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(15)
                .HasColumnName("ProjectID");
            entity.Property(e => e.ProjectImagePath).HasMaxLength(510);
            entity.Property(e => e.ProjectOpen).HasColumnType("datetime");
            entity.Property(e => e.ProjectOwner).HasMaxLength(50);
            entity.Property(e => e.ProjectStatus).HasMaxLength(15);
            entity.Property(e => e.ProjectTel).HasMaxLength(20);
            entity.Property(e => e.ProjectType).HasMaxLength(1);
            entity.Property(e => e.ProjectValues).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.ProjectValues2).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.ProjectWebsite).HasMaxLength(100);
            entity.Property(e => e.SapbandCode)
                .HasMaxLength(50)
                .HasColumnName("SAPBandCode");
            entity.Property(e => e.SapcostCenter)
                .HasMaxLength(50)
                .HasColumnName("SAPCostCenter");
            entity.Property(e => e.SapcostCenter2)
                .HasMaxLength(50)
                .HasColumnName("SAPCostCenter2");
            entity.Property(e => e.SapcostCenter47)
                .HasMaxLength(50)
                .HasColumnName("SAPCostCenter47");
            entity.Property(e => e.SapcostCenter472)
                .HasMaxLength(50)
                .HasColumnName("SAPCostCenter472");
            entity.Property(e => e.SapplantCode)
                .HasMaxLength(50)
                .HasColumnName("SAPPlantCode");
            entity.Property(e => e.SapplantCode2)
                .HasMaxLength(50)
                .HasColumnName("SAPPlantCode2");
            entity.Property(e => e.SappostCenter)
                .HasMaxLength(50)
                .HasColumnName("SAPPostCenter");
            entity.Property(e => e.SapprofitCenter)
                .HasMaxLength(50)
                .HasColumnName("SAPProfitCenter");
            entity.Property(e => e.SapprofixCenter)
                .HasMaxLength(50)
                .HasColumnName("SAPProfixCenter");
            entity.Property(e => e.Sapwbscode)
                .HasMaxLength(20)
                .HasColumnName("SAPWBSCode");
            entity.Property(e => e.Sapwbscode47)
                .HasMaxLength(50)
                .HasColumnName("SAPWBSCode47");
            entity.Property(e => e.SubBuid).HasColumnName("SubBUID");
        });

        modelBuilder.Entity<SysMasterUnit>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Sys_Master_Units");

            entity.Property(e => e.AccountRoom).HasMaxLength(4);
            entity.Property(e => e.ActualEstimatePrice).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.AddressNo).HasMaxLength(50);
            entity.Property(e => e.AddressNoEn)
                .HasMaxLength(50)
                .HasColumnName("AddressNoEN");
            entity.Property(e => e.BillPaymentCode).HasMaxLength(50);
            entity.Property(e => e.Block).HasMaxLength(50);
            entity.Property(e => e.Boiid).HasColumnName("BOIID");
            entity.Property(e => e.BuildingArea).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.CountryEn)
                .HasMaxLength(50)
                .HasColumnName("CountryEN");
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.District).HasMaxLength(50);
            entity.Property(e => e.DistrictEn)
                .HasMaxLength(50)
                .HasColumnName("DistrictEN");
            entity.Property(e => e.EcustomerRequestDate)
                .HasColumnType("datetime")
                .HasColumnName("ECustomerRequestDate");
            entity.Property(e => e.EestimatedDateComplete)
                .HasColumnType("datetime")
                .HasColumnName("EEstimatedDateComplete");
            entity.Property(e => e.ElectricIns).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.ElectricInstall).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.ElectricMeterSize).HasMaxLength(50);
            entity.Property(e => e.FloorId).HasColumnName("FloorID");
            entity.Property(e => e.Height).HasMaxLength(50);
            entity.Property(e => e.HoldBy).HasMaxLength(50);
            entity.Property(e => e.HoldDate).HasColumnType("datetime");
            entity.Property(e => e.HouseArea).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.HouseNumber).HasMaxLength(50);
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.LoftArea).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.ModelId)
                .HasMaxLength(20)
                .HasColumnName("ModelID");
            entity.Property(e => e.ModifyBy).HasMaxLength(50);
            entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            entity.Property(e => e.Moo).HasMaxLength(50);
            entity.Property(e => e.MooEn)
                .HasMaxLength(50)
                .HasColumnName("MooEN");
            entity.Property(e => e.PhaseId).HasColumnName("PhaseID");
            entity.Property(e => e.PjlandOfficeId).HasColumnName("PJLandOfficeID");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(10)
                .HasColumnName("ProjectID");
            entity.Property(e => e.Province).HasMaxLength(50);
            entity.Property(e => e.ProvinceEn)
                .HasMaxLength(50)
                .HasColumnName("ProvinceEN");
            entity.Property(e => e.PublicFee).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.PublicFeeType).HasMaxLength(10);
            entity.Property(e => e.QccustomerDate)
                .HasColumnType("datetime")
                .HasColumnName("QCCustomerDate");
            entity.Property(e => e.QcvendorDate)
                .HasColumnType("datetime")
                .HasColumnName("QCVendorDate");
            entity.Property(e => e.Road).HasMaxLength(50);
            entity.Property(e => e.RoadEn)
                .HasMaxLength(50)
                .HasColumnName("RoadEN");
            entity.Property(e => e.SapcurrStatus)
                .HasMaxLength(4)
                .HasColumnName("SAPCurrStatus");
            entity.Property(e => e.SapoldStatus)
                .HasMaxLength(4)
                .HasColumnName("SAPOldStatus");
            entity.Property(e => e.Sapremarks).HasColumnName("SAPRemarks");
            entity.Property(e => e.SapstatusFlag)
                .HasMaxLength(1)
                .HasColumnName("SAPStatusFlag");
            entity.Property(e => e.Sapwbscode)
                .HasMaxLength(50)
                .HasColumnName("SAPWBSCode");
            entity.Property(e => e.Sbuid)
                .HasMaxLength(10)
                .HasColumnName("SBUID");
            entity.Property(e => e.SellingArea).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.ServitudeRateType).HasMaxLength(5);
            entity.Property(e => e.Soi).HasMaxLength(50);
            entity.Property(e => e.SoiEn)
                .HasMaxLength(50)
                .HasColumnName("SoiEN");
            entity.Property(e => e.SubDistrict).HasMaxLength(50);
            entity.Property(e => e.SubDistrictEn)
                .HasMaxLength(50)
                .HasColumnName("SubDistrictEN");
            entity.Property(e => e.SubPhaseId).HasColumnName("SubPhaseID");
            entity.Property(e => e.TitledeedArea).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.TowerId).HasColumnName("TowerID");
            entity.Property(e => e.Transferduedate).HasColumnType("datetime");
            entity.Property(e => e.TrashRate).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.TrashRateType).HasMaxLength(5);
            entity.Property(e => e.UnholdBy).HasMaxLength(50);
            entity.Property(e => e.UnholdDate).HasColumnType("datetime");
            entity.Property(e => e.UnitCombineFlag).HasMaxLength(20);
            entity.Property(e => e.UnitId)
                .HasMaxLength(50)
                .HasColumnName("UnitID");
            entity.Property(e => e.UnitMapping).HasMaxLength(50);
            entity.Property(e => e.UnitNumber).HasMaxLength(20);
            entity.Property(e => e.UnitNumber2).HasMaxLength(20);
            entity.Property(e => e.UnitStatus).HasMaxLength(10);
            entity.Property(e => e.Village).HasMaxLength(50);
            entity.Property(e => e.VillageEn)
                .HasMaxLength(50)
                .HasColumnName("VillageEN");
            entity.Property(e => e.WaterCoolingRt)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("WaterCoolingRT");
            entity.Property(e => e.WaterIns).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.WaterInstall).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.WaterMeterSize).HasMaxLength(50);
            entity.Property(e => e.WcustomerRequestDate)
                .HasColumnType("datetime")
                .HasColumnName("WCustomerRequestDate");
            entity.Property(e => e.WestimatedDateComplete)
                .HasColumnType("datetime")
                .HasColumnName("WEstimatedDateComplete");
            entity.Property(e => e.Width).HasMaxLength(50);
            entity.Property(e => e.WorkPackage).HasMaxLength(50);
            entity.Property(e => e.WorkPackageDate).HasColumnType("datetime");
            entity.Property(e => e.X).HasMaxLength(50);
            entity.Property(e => e.Y).HasMaxLength(50);
            entity.Property(e => e.ZipCode).HasMaxLength(50);
            entity.Property(e => e.Zone).HasMaxLength(50);
        });

        modelBuilder.Entity<SysRemFloor>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Sys_REM_Floor");

            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.FloorId).HasColumnName("FloorID");
            entity.Property(e => e.FloorName).HasMaxLength(50);
            entity.Property(e => e.FloorNameEng).HasMaxLength(50);
            entity.Property(e => e.FloorPlanPath).HasMaxLength(500);
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.ModifyBy).HasMaxLength(50);
            entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(10)
                .HasColumnName("ProjectID");
            entity.Property(e => e.TowerId).HasColumnName("TowerID");
        });

        modelBuilder.Entity<SysRemTower>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Sys_REM_tower");

            entity.Property(e => e.AccountTower).HasMaxLength(1);
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.FinishDate).HasColumnType("datetime");
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.JuristicId).HasColumnName("JuristicID");
            entity.Property(e => e.JuristicName).HasMaxLength(50);
            entity.Property(e => e.LandBookPage).HasMaxLength(50);
            entity.Property(e => e.LandNumber).HasMaxLength(200);
            entity.Property(e => e.LandPortionNumber).HasMaxLength(200);
            entity.Property(e => e.LandSurveyArea).HasMaxLength(200);
            entity.Property(e => e.ModifyBy).HasMaxLength(50);
            entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(10)
                .HasColumnName("ProjectID");
            entity.Property(e => e.RegisterBuildingDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.TitleDeedArea).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.TitledeedNumber).HasMaxLength(200);
            entity.Property(e => e.TowerId).HasColumnName("TowerID");
            entity.Property(e => e.TowerLicenseNo).HasMaxLength(50);
            entity.Property(e => e.TowerName).HasMaxLength(50);
            entity.Property(e => e.TowerNameEng).HasMaxLength(50);
            entity.Property(e => e.TowerNumber).HasMaxLength(50);
            entity.Property(e => e.TowerTotalArea).HasMaxLength(50);
        });

        modelBuilder.Entity<TempAbh003>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_ABH003");

            entity.Property(e => e.AddrNo).HasMaxLength(255);
            entity.Property(e => e.AppointDate)
                .HasMaxLength(255)
                .HasColumnName("Appoint_Date");
            entity.Property(e => e.Area).HasMaxLength(255);
            entity.Property(e => e.BankSelectedCs)
                .HasMaxLength(255)
                .HasColumnName("BankSelected_CS");
            entity.Property(e => e.BookDate).HasMaxLength(255);
            entity.Property(e => e.Build).HasMaxLength(255);
            entity.Property(e => e.CodeVerify).HasMaxLength(255);
            entity.Property(e => e.ContactLogDate)
                .HasMaxLength(255)
                .HasColumnName("ContactLog_Date");
            entity.Property(e => e.ContractDate).HasMaxLength(255);
            entity.Property(e => e.ContractMobile).HasMaxLength(255);
            entity.Property(e => e.ContractName).HasMaxLength(255);
            entity.Property(e => e.ContractNumber).HasMaxLength(255);
            entity.Property(e => e.Csresponse)
                .HasMaxLength(255)
                .HasColumnName("CSResponse");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(255)
                .HasColumnName("CustomerID");
            entity.Property(e => e.CustomerMobile).HasMaxLength(255);
            entity.Property(e => e.CustomerName).HasMaxLength(255);
            entity.Property(e => e.Defect).HasMaxLength(255);
            entity.Property(e => e.DefectStatus).HasMaxLength(255);
            entity.Property(e => e.ExpectTransfer).HasMaxLength(255);
            entity.Property(e => e.ExpectTransferBy).HasMaxLength(255);
            entity.Property(e => e.FinanceCareDraft).HasMaxLength(255);
            entity.Property(e => e.Floor).HasMaxLength(255);
            entity.Property(e => e.InspectCount)
                .HasMaxLength(255)
                .HasColumnName("Inspect_Count");
            entity.Property(e => e.LastExpectDate).HasMaxLength(255);
            entity.Property(e => e.LetterDueDateCs)
                .HasMaxLength(255)
                .HasColumnName("LetterDueDate_CS");
            entity.Property(e => e.LineContract).HasMaxLength(255);
            entity.Property(e => e.LoanBankNameFinplus)
                .HasMaxLength(255)
                .HasColumnName("LoanBankName_FINPlus");
            entity.Property(e => e.LoanBankNameSelect)
                .HasMaxLength(255)
                .HasColumnName("LoanBankName_Select");
            entity.Property(e => e.LoanStatusNameFinplus)
                .HasMaxLength(255)
                .HasColumnName("LoanStatusName_FINPlus");
            entity.Property(e => e.MeterTypeName).HasMaxLength(255);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(255)
                .HasColumnName("ProjectID");
            entity.Property(e => e.ProjectName).HasMaxLength(255);
            entity.Property(e => e.Qc5FinishDate)
                .HasMaxLength(255)
                .HasColumnName("QC5_FinishDate");
            entity.Property(e => e.Qc6Date)
                .HasMaxLength(255)
                .HasColumnName("QC6_Date");
            entity.Property(e => e.Qc6Status)
                .HasMaxLength(255)
                .HasColumnName("QC6_Status");
            entity.Property(e => e.ReceiveDocument).HasMaxLength(255);
            entity.Property(e => e.ReceiveRoomAgreementDate).HasMaxLength(255);
            entity.Property(e => e.ReceiveRoomDate).HasMaxLength(255);
            entity.Property(e => e.Redemption).HasMaxLength(255);
            entity.Property(e => e.RemarkUnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("RemarkUnitStatus_CS");
            entity.Property(e => e.Room).HasMaxLength(255);
            entity.Property(e => e.SaveDocument).HasMaxLength(255);
            entity.Property(e => e.SellingPrice).HasMaxLength(255);
            entity.Property(e => e.TransferDate).HasMaxLength(255);
            entity.Property(e => e.TransferDueDateCs)
                .HasMaxLength(255)
                .HasColumnName("TransferDueDate_CS");
            entity.Property(e => e.UnitCode).HasMaxLength(255);
            entity.Property(e => e.UnitStatus).HasMaxLength(255);
            entity.Property(e => e.UnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("UnitStatus_CS");
            entity.Property(e => e.UnitType).HasMaxLength(255);
        });

        modelBuilder.Entity<TempAt15>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_AT15");

            entity.Property(e => e.BankCode).HasMaxLength(255);
            entity.Property(e => e.BankStatus).HasMaxLength(255);
            entity.Property(e => e.CustomerName).HasMaxLength(255);
            entity.Property(e => e.ProgressStatus).HasMaxLength(255);
            entity.Property(e => e.UnitCode).HasMaxLength(255);
            entity.Property(e => e.UnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("UnitStatus_CS");
        });

        modelBuilder.Entity<TempAt71>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_AT71");

            entity.Property(e => e.BankCode).HasMaxLength(255);
            entity.Property(e => e.CustomerName).HasMaxLength(255);
            entity.Property(e => e.ProgressStatus).HasMaxLength(255);
            entity.Property(e => e.UnitCode).HasMaxLength(255);
            entity.Property(e => e.UnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("UnitStatus_CS");
        });

        modelBuilder.Entity<TempAtchang>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_ATCHANG");

            entity.Property(e => e.BankCode).HasMaxLength(255);
            entity.Property(e => e.CustomerName).HasMaxLength(255);
            entity.Property(e => e.ProgressStatus).HasMaxLength(255);
            entity.Property(e => e.UnitCode).HasMaxLength(255);
            entity.Property(e => e.UnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("UnitStatus_CS");
        });

        modelBuilder.Entity<TempAthk>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_ATHK");

            entity.Property(e => e.BankCode).HasMaxLength(255);
            entity.Property(e => e.CustomerName).HasMaxLength(255);
            entity.Property(e => e.ProgressStatus).HasMaxLength(255);
            entity.Property(e => e.UnitCode).HasMaxLength(255);
            entity.Property(e => e.UnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("UnitStatus_CS");
        });

        modelBuilder.Entity<TempAva>(entity =>
        {
            entity.HasKey(e => new { e.ProjectId, e.UnitCode });

            entity.ToTable("temp_AVA");

            entity.Property(e => e.ProjectId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.UnitCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Build)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProjectName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TempBankTarget>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_bank_target");

            entity.Property(e => e.Bank)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Target).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<TempBr67>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_BR67");

            entity.Property(e => e.BankCode).HasMaxLength(255);
            entity.Property(e => e.CustomerName).HasMaxLength(255);
            entity.Property(e => e.ProgressStatus).HasMaxLength(255);
            entity.Property(e => e.UnitCode).HasMaxLength(255);
            entity.Property(e => e.UnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("UnitStatus_CS");
        });

        modelBuilder.Entity<TempCam002>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_CAM002");

            entity.Property(e => e.AddrNo).HasMaxLength(255);
            entity.Property(e => e.AppointDate)
                .HasMaxLength(255)
                .HasColumnName("Appoint_Date");
            entity.Property(e => e.Area).HasMaxLength(255);
            entity.Property(e => e.BookDate).HasMaxLength(255);
            entity.Property(e => e.Build).HasMaxLength(255);
            entity.Property(e => e.CodeVerify).HasMaxLength(255);
            entity.Property(e => e.ContactLogDate)
                .HasMaxLength(255)
                .HasColumnName("ContactLog_Date");
            entity.Property(e => e.ContractDate).HasMaxLength(255);
            entity.Property(e => e.ContractNumber).HasMaxLength(255);
            entity.Property(e => e.Csresponse)
                .HasMaxLength(255)
                .HasColumnName("CSResponse");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(255)
                .HasColumnName("CustomerID");
            entity.Property(e => e.CustomerName).HasMaxLength(255);
            entity.Property(e => e.Defect).HasMaxLength(255);
            entity.Property(e => e.DefectStatus).HasMaxLength(255);
            entity.Property(e => e.ExpectTransfer).HasMaxLength(255);
            entity.Property(e => e.ExpectTransferBy).HasMaxLength(255);
            entity.Property(e => e.FinanceCareDraft).HasMaxLength(255);
            entity.Property(e => e.Floor).HasMaxLength(255);
            entity.Property(e => e.InspectCount)
                .HasMaxLength(255)
                .HasColumnName("Inspect_Count");
            entity.Property(e => e.LetterDueDateCs)
                .HasMaxLength(255)
                .HasColumnName("LetterDueDate_CS");
            entity.Property(e => e.LoanBankNameFinplus)
                .HasMaxLength(255)
                .HasColumnName("LoanBankName_FINPlus");
            entity.Property(e => e.LoanBankNameSelect)
                .HasMaxLength(255)
                .HasColumnName("LoanBankName_Select");
            entity.Property(e => e.LoanStatusNameFinplus)
                .HasMaxLength(255)
                .HasColumnName("LoanStatusName_FINPlus");
            entity.Property(e => e.MeterTypeName).HasMaxLength(255);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(255)
                .HasColumnName("ProjectID");
            entity.Property(e => e.ProjectName).HasMaxLength(255);
            entity.Property(e => e.Qc5FinishDate)
                .HasMaxLength(255)
                .HasColumnName("QC5_FinishDate");
            entity.Property(e => e.Qc6Date)
                .HasMaxLength(255)
                .HasColumnName("QC6_Date");
            entity.Property(e => e.Qc6Status)
                .HasMaxLength(255)
                .HasColumnName("QC6_Status");
            entity.Property(e => e.ReceiveDocument).HasMaxLength(255);
            entity.Property(e => e.ReceiveRoomAgreementDate).HasMaxLength(255);
            entity.Property(e => e.ReceiveRoomDate).HasMaxLength(255);
            entity.Property(e => e.RemarkUnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("RemarkUnitStatus_CS");
            entity.Property(e => e.Room).HasMaxLength(255);
            entity.Property(e => e.SaveDocument).HasMaxLength(255);
            entity.Property(e => e.SellingPrice).HasMaxLength(255);
            entity.Property(e => e.TransferDate).HasMaxLength(255);
            entity.Property(e => e.TransferDueDateCs)
                .HasMaxLength(255)
                .HasColumnName("TransferDueDate_CS");
            entity.Property(e => e.UnitCode).HasMaxLength(255);
            entity.Property(e => e.UnitStatus).HasMaxLength(255);
            entity.Property(e => e.UnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("UnitStatus_CS");
            entity.Property(e => e.UnitStatusCs1)
                .HasMaxLength(255)
                .HasColumnName("UnitStatus_CS1");
            entity.Property(e => e.UnitType).HasMaxLength(255);
            entity.Property(e => e.เกา)
                .HasMaxLength(255)
                .HasColumnName("เก่า");
        });

        modelBuilder.Entity<TempCbr002>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_CBR002");

            entity.Property(e => e.AddrNo).HasMaxLength(255);
            entity.Property(e => e.AppointDate)
                .HasMaxLength(255)
                .HasColumnName("Appoint_Date");
            entity.Property(e => e.Area).HasMaxLength(255);
            entity.Property(e => e.BankSelectedCs)
                .HasMaxLength(255)
                .HasColumnName("BankSelected_CS");
            entity.Property(e => e.BookDate).HasMaxLength(255);
            entity.Property(e => e.Build).HasMaxLength(255);
            entity.Property(e => e.CodeVerify).HasMaxLength(255);
            entity.Property(e => e.ContactLogDate)
                .HasMaxLength(255)
                .HasColumnName("ContactLog_Date");
            entity.Property(e => e.ContractDate).HasMaxLength(255);
            entity.Property(e => e.ContractMobile).HasMaxLength(255);
            entity.Property(e => e.ContractName).HasMaxLength(255);
            entity.Property(e => e.ContractNumber).HasMaxLength(255);
            entity.Property(e => e.Csresponse)
                .HasMaxLength(255)
                .HasColumnName("CSResponse");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(255)
                .HasColumnName("CustomerID");
            entity.Property(e => e.CustomerMobile).HasMaxLength(255);
            entity.Property(e => e.CustomerName).HasMaxLength(255);
            entity.Property(e => e.Defect).HasMaxLength(255);
            entity.Property(e => e.DefectStatus).HasMaxLength(255);
            entity.Property(e => e.ExpectTransfer).HasMaxLength(255);
            entity.Property(e => e.ExpectTransferBy).HasMaxLength(255);
            entity.Property(e => e.FinanceCareDraft).HasMaxLength(255);
            entity.Property(e => e.Floor).HasMaxLength(255);
            entity.Property(e => e.InspectCount)
                .HasMaxLength(255)
                .HasColumnName("Inspect_Count");
            entity.Property(e => e.LastExpectDate).HasMaxLength(255);
            entity.Property(e => e.LetterDueDateCs)
                .HasMaxLength(255)
                .HasColumnName("LetterDueDate_CS");
            entity.Property(e => e.LineContract).HasMaxLength(255);
            entity.Property(e => e.LoanBankNameFinplus)
                .HasMaxLength(255)
                .HasColumnName("LoanBankName_FINPlus");
            entity.Property(e => e.LoanBankNameSelect)
                .HasMaxLength(255)
                .HasColumnName("LoanBankName_Select");
            entity.Property(e => e.LoanStatusNameFinplus)
                .HasMaxLength(255)
                .HasColumnName("LoanStatusName_FINPlus");
            entity.Property(e => e.MeterTypeName).HasMaxLength(255);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(255)
                .HasColumnName("ProjectID");
            entity.Property(e => e.ProjectName).HasMaxLength(255);
            entity.Property(e => e.Qc5FinishDate)
                .HasMaxLength(255)
                .HasColumnName("QC5_FinishDate");
            entity.Property(e => e.Qc6Date)
                .HasMaxLength(255)
                .HasColumnName("QC6_Date");
            entity.Property(e => e.Qc6Status)
                .HasMaxLength(255)
                .HasColumnName("QC6_Status");
            entity.Property(e => e.ReceiveDocument).HasMaxLength(255);
            entity.Property(e => e.ReceiveRoomAgreementDate).HasMaxLength(255);
            entity.Property(e => e.ReceiveRoomDate).HasMaxLength(255);
            entity.Property(e => e.Redemption).HasMaxLength(255);
            entity.Property(e => e.RemarkUnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("RemarkUnitStatus_CS");
            entity.Property(e => e.Room).HasMaxLength(255);
            entity.Property(e => e.SaveDocument).HasMaxLength(255);
            entity.Property(e => e.SellingPrice).HasMaxLength(255);
            entity.Property(e => e.TransferDate).HasMaxLength(255);
            entity.Property(e => e.TransferDueDateCs)
                .HasMaxLength(255)
                .HasColumnName("TransferDueDate_CS");
            entity.Property(e => e.UnitCode).HasMaxLength(255);
            entity.Property(e => e.UnitStatus).HasMaxLength(255);
            entity.Property(e => e.UnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("UnitStatus_CS");
            entity.Property(e => e.UnitType).HasMaxLength(255);
        });

        modelBuilder.Entity<TempCbr003>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_CBR003");

            entity.Property(e => e.AddrNo).HasMaxLength(255);
            entity.Property(e => e.AppointDate)
                .HasMaxLength(255)
                .HasColumnName("Appoint_Date");
            entity.Property(e => e.Area).HasMaxLength(255);
            entity.Property(e => e.BankSelectedCs)
                .HasMaxLength(255)
                .HasColumnName("BankSelected_CS");
            entity.Property(e => e.BookDate).HasMaxLength(255);
            entity.Property(e => e.Build).HasMaxLength(255);
            entity.Property(e => e.CodeVerify).HasMaxLength(255);
            entity.Property(e => e.ContactLogDate)
                .HasMaxLength(255)
                .HasColumnName("ContactLog_Date");
            entity.Property(e => e.ContractDate).HasMaxLength(255);
            entity.Property(e => e.ContractMobile).HasMaxLength(255);
            entity.Property(e => e.ContractName).HasMaxLength(255);
            entity.Property(e => e.ContractNumber).HasMaxLength(255);
            entity.Property(e => e.Csresponse)
                .HasMaxLength(255)
                .HasColumnName("CSResponse");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(255)
                .HasColumnName("CustomerID");
            entity.Property(e => e.CustomerMobile).HasMaxLength(255);
            entity.Property(e => e.CustomerName).HasMaxLength(255);
            entity.Property(e => e.Defect).HasMaxLength(255);
            entity.Property(e => e.DefectStatus).HasMaxLength(255);
            entity.Property(e => e.ExpectTransfer).HasMaxLength(255);
            entity.Property(e => e.ExpectTransferBy).HasMaxLength(255);
            entity.Property(e => e.FinanceCareDraft).HasMaxLength(255);
            entity.Property(e => e.Floor).HasMaxLength(255);
            entity.Property(e => e.InspectCount)
                .HasMaxLength(255)
                .HasColumnName("Inspect_Count");
            entity.Property(e => e.LastExpectDate).HasMaxLength(255);
            entity.Property(e => e.LetterDueDateCs)
                .HasMaxLength(255)
                .HasColumnName("LetterDueDate_CS");
            entity.Property(e => e.LineContract).HasMaxLength(255);
            entity.Property(e => e.LoanBankNameFinplus)
                .HasMaxLength(255)
                .HasColumnName("LoanBankName_FINPlus");
            entity.Property(e => e.LoanBankNameSelect)
                .HasMaxLength(255)
                .HasColumnName("LoanBankName_Select");
            entity.Property(e => e.LoanStatusNameFinplus)
                .HasMaxLength(255)
                .HasColumnName("LoanStatusName_FINPlus");
            entity.Property(e => e.MeterTypeName).HasMaxLength(255);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(255)
                .HasColumnName("ProjectID");
            entity.Property(e => e.ProjectName).HasMaxLength(255);
            entity.Property(e => e.Qc5FinishDate)
                .HasMaxLength(255)
                .HasColumnName("QC5_FinishDate");
            entity.Property(e => e.Qc6Date)
                .HasMaxLength(255)
                .HasColumnName("QC6_Date");
            entity.Property(e => e.Qc6Status)
                .HasMaxLength(255)
                .HasColumnName("QC6_Status");
            entity.Property(e => e.ReceiveDocument).HasMaxLength(255);
            entity.Property(e => e.ReceiveRoomAgreementDate).HasMaxLength(255);
            entity.Property(e => e.ReceiveRoomDate).HasMaxLength(255);
            entity.Property(e => e.Redemption).HasMaxLength(255);
            entity.Property(e => e.RemarkUnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("RemarkUnitStatus_CS");
            entity.Property(e => e.Room).HasMaxLength(255);
            entity.Property(e => e.SaveDocument).HasMaxLength(255);
            entity.Property(e => e.SellingPrice).HasMaxLength(255);
            entity.Property(e => e.TransferDate).HasMaxLength(255);
            entity.Property(e => e.TransferDueDateCs)
                .HasMaxLength(255)
                .HasColumnName("TransferDueDate_CS");
            entity.Property(e => e.UnitCode).HasMaxLength(255);
            entity.Property(e => e.UnitStatus).HasMaxLength(255);
            entity.Property(e => e.UnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("UnitStatus_CS");
            entity.Property(e => e.UnitType).HasMaxLength(255);
        });

        modelBuilder.Entity<TempChecker>(entity =>
        {
            entity.ToTable("temp_checker");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
        });

        modelBuilder.Entity<TempCmd004>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_CMD004");

            entity.Property(e => e.AddrNo).HasMaxLength(255);
            entity.Property(e => e.AppointDate)
                .HasMaxLength(255)
                .HasColumnName("Appoint_Date");
            entity.Property(e => e.Area).HasMaxLength(255);
            entity.Property(e => e.BankSelectedCs)
                .HasMaxLength(255)
                .HasColumnName("BankSelected_CS");
            entity.Property(e => e.BookDate).HasMaxLength(255);
            entity.Property(e => e.Build).HasMaxLength(255);
            entity.Property(e => e.CodeVerify).HasMaxLength(255);
            entity.Property(e => e.ContactLogDate)
                .HasMaxLength(255)
                .HasColumnName("ContactLog_Date");
            entity.Property(e => e.ContractDate).HasMaxLength(255);
            entity.Property(e => e.ContractMobile).HasMaxLength(255);
            entity.Property(e => e.ContractName).HasMaxLength(255);
            entity.Property(e => e.ContractNumber).HasMaxLength(255);
            entity.Property(e => e.Csresponse)
                .HasMaxLength(255)
                .HasColumnName("CSResponse");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(255)
                .HasColumnName("CustomerID");
            entity.Property(e => e.CustomerMobile).HasMaxLength(255);
            entity.Property(e => e.CustomerName).HasMaxLength(255);
            entity.Property(e => e.Defect).HasMaxLength(255);
            entity.Property(e => e.DefectStatus).HasMaxLength(255);
            entity.Property(e => e.ExpectTransfer).HasMaxLength(255);
            entity.Property(e => e.ExpectTransferBy).HasMaxLength(255);
            entity.Property(e => e.FinanceCareDraft).HasMaxLength(255);
            entity.Property(e => e.Floor).HasMaxLength(255);
            entity.Property(e => e.InspectCount)
                .HasMaxLength(255)
                .HasColumnName("Inspect_Count");
            entity.Property(e => e.LastExpectDate).HasMaxLength(255);
            entity.Property(e => e.LetterDueDateCs)
                .HasMaxLength(255)
                .HasColumnName("LetterDueDate_CS");
            entity.Property(e => e.LineContract).HasMaxLength(255);
            entity.Property(e => e.LoanBankNameFinplus)
                .HasMaxLength(255)
                .HasColumnName("LoanBankName_FINPlus");
            entity.Property(e => e.LoanBankNameSelect)
                .HasMaxLength(255)
                .HasColumnName("LoanBankName_Select");
            entity.Property(e => e.LoanStatusNameFinplus)
                .HasMaxLength(255)
                .HasColumnName("LoanStatusName_FINPlus");
            entity.Property(e => e.MeterTypeName).HasMaxLength(255);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(255)
                .HasColumnName("ProjectID");
            entity.Property(e => e.ProjectName).HasMaxLength(255);
            entity.Property(e => e.Qc5FinishDate)
                .HasMaxLength(255)
                .HasColumnName("QC5_FinishDate");
            entity.Property(e => e.Qc6Date)
                .HasMaxLength(255)
                .HasColumnName("QC6_Date");
            entity.Property(e => e.Qc6Status)
                .HasMaxLength(255)
                .HasColumnName("QC6_Status");
            entity.Property(e => e.ReceiveDocument).HasMaxLength(255);
            entity.Property(e => e.ReceiveRoomAgreementDate).HasMaxLength(255);
            entity.Property(e => e.ReceiveRoomDate).HasMaxLength(255);
            entity.Property(e => e.Redemption).HasMaxLength(255);
            entity.Property(e => e.RemarkUnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("RemarkUnitStatus_CS");
            entity.Property(e => e.Room).HasMaxLength(255);
            entity.Property(e => e.SaveDocument).HasMaxLength(255);
            entity.Property(e => e.SellingPrice).HasMaxLength(255);
            entity.Property(e => e.TransferDate).HasMaxLength(255);
            entity.Property(e => e.TransferDueDateCs)
                .HasMaxLength(255)
                .HasColumnName("TransferDueDate_CS");
            entity.Property(e => e.UnitCode).HasMaxLength(255);
            entity.Property(e => e.UnitStatus).HasMaxLength(255);
            entity.Property(e => e.UnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("UnitStatus_CS");
            entity.Property(e => e.UnitType).HasMaxLength(255);
        });

        modelBuilder.Entity<TempCmd005>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_CMD005");

            entity.Property(e => e.AddrNo).HasMaxLength(255);
            entity.Property(e => e.AppointDate)
                .HasMaxLength(255)
                .HasColumnName("Appoint_Date");
            entity.Property(e => e.Area).HasMaxLength(255);
            entity.Property(e => e.BankSelectedCs)
                .HasMaxLength(255)
                .HasColumnName("BankSelected_CS");
            entity.Property(e => e.BookDate).HasMaxLength(255);
            entity.Property(e => e.Build).HasMaxLength(255);
            entity.Property(e => e.CodeVerify).HasMaxLength(255);
            entity.Property(e => e.ContactLogDate)
                .HasMaxLength(255)
                .HasColumnName("ContactLog_Date");
            entity.Property(e => e.ContractDate).HasMaxLength(255);
            entity.Property(e => e.ContractMobile).HasMaxLength(255);
            entity.Property(e => e.ContractName).HasMaxLength(255);
            entity.Property(e => e.ContractNumber).HasMaxLength(255);
            entity.Property(e => e.Csresponse)
                .HasMaxLength(255)
                .HasColumnName("CSResponse");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(255)
                .HasColumnName("CustomerID");
            entity.Property(e => e.CustomerMobile).HasMaxLength(255);
            entity.Property(e => e.CustomerName).HasMaxLength(255);
            entity.Property(e => e.Defect).HasMaxLength(255);
            entity.Property(e => e.DefectStatus).HasMaxLength(255);
            entity.Property(e => e.ExpectTransfer).HasMaxLength(255);
            entity.Property(e => e.ExpectTransferBy).HasMaxLength(255);
            entity.Property(e => e.FinanceCareDraft).HasMaxLength(255);
            entity.Property(e => e.Floor).HasMaxLength(255);
            entity.Property(e => e.InspectCount)
                .HasMaxLength(255)
                .HasColumnName("Inspect_Count");
            entity.Property(e => e.LastExpectDate).HasMaxLength(255);
            entity.Property(e => e.LetterDueDateCs)
                .HasMaxLength(255)
                .HasColumnName("LetterDueDate_CS");
            entity.Property(e => e.LineContract).HasMaxLength(255);
            entity.Property(e => e.LoanBankNameFinplus)
                .HasMaxLength(255)
                .HasColumnName("LoanBankName_FINPlus");
            entity.Property(e => e.LoanBankNameSelect)
                .HasMaxLength(255)
                .HasColumnName("LoanBankName_Select");
            entity.Property(e => e.LoanStatusNameFinplus)
                .HasMaxLength(255)
                .HasColumnName("LoanStatusName_FINPlus");
            entity.Property(e => e.MeterTypeName).HasMaxLength(255);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(255)
                .HasColumnName("ProjectID");
            entity.Property(e => e.ProjectName).HasMaxLength(255);
            entity.Property(e => e.Qc5FinishDate)
                .HasMaxLength(255)
                .HasColumnName("QC5_FinishDate");
            entity.Property(e => e.Qc6Date)
                .HasMaxLength(255)
                .HasColumnName("QC6_Date");
            entity.Property(e => e.Qc6Status)
                .HasMaxLength(255)
                .HasColumnName("QC6_Status");
            entity.Property(e => e.ReceiveDocument).HasMaxLength(255);
            entity.Property(e => e.ReceiveRoomAgreementDate).HasMaxLength(255);
            entity.Property(e => e.ReceiveRoomDate).HasMaxLength(255);
            entity.Property(e => e.Redemption).HasMaxLength(255);
            entity.Property(e => e.RemarkUnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("RemarkUnitStatus_CS");
            entity.Property(e => e.Room).HasMaxLength(255);
            entity.Property(e => e.SaveDocument).HasMaxLength(255);
            entity.Property(e => e.SellingPrice).HasMaxLength(255);
            entity.Property(e => e.TransferDate).HasMaxLength(255);
            entity.Property(e => e.TransferDueDateCs)
                .HasMaxLength(255)
                .HasColumnName("TransferDueDate_CS");
            entity.Property(e => e.UnitCode).HasMaxLength(255);
            entity.Property(e => e.UnitStatus).HasMaxLength(255);
            entity.Property(e => e.UnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("UnitStatus_CS");
            entity.Property(e => e.UnitType).HasMaxLength(255);
        });

        modelBuilder.Entity<TempCsResponse>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_cs_response");

            entity.Property(e => e.Csresponse)
                .HasMaxLength(2000)
                .HasColumnName("CSResponse");
            entity.Property(e => e.RemarkUnitStatusId).HasColumnName("RemarkUnitStatusID");
            entity.Property(e => e.RemarkUnitStatusName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UnitCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UnitStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UnitStatusId).HasColumnName("UnitStatusID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<TempCustSat20240104>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_cust_sat_20240104");

            entity.Property(e => e.ClientIp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ClientIP");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.QuestionDate).HasColumnType("datetime");
            entity.Property(e => e.QuestionTypeId).HasColumnName("QuestionTypeID");
            entity.Property(e => e.Remark).HasColumnType("text");
            entity.Property(e => e.UnitCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<TempDefect>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_defect");
        });

        modelBuilder.Entity<TempDefectVendor>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_defect_vendor");

            entity.Property(e => e.DefectDescId).HasColumnName("DefectDescID");
            entity.Property(e => e.DefectDescName).HasMaxLength(255);
            entity.Property(e => e.DefectTypeId).HasColumnName("DefectTypeID");
            entity.Property(e => e.DefectTypeName).HasMaxLength(255);
            entity.Property(e => e.Vandor).HasMaxLength(255);
        });

        modelBuilder.Entity<TempEditLetter>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_edit_letter");

            entity.Property(e => e.ContractNumber).HasMaxLength(255);
            entity.Property(e => e.CustomerName).HasMaxLength(255);
            entity.Property(e => e.LetterDueDateCs)
                .HasColumnType("datetime")
                .HasColumnName("LetterDueDate_CS");
            entity.Property(e => e.No).HasColumnName("No#");
            entity.Property(e => e.UnitCode).HasMaxLength(255);
            entity.Property(e => e.จดหมายลงวนท)
                .HasColumnType("datetime")
                .HasColumnName("จดหมายลงวันที่");
            entity.Property(e => e.ประเภทจดหมาย).HasMaxLength(255);
        });

        modelBuilder.Entity<TempEqc018>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_EQC018");

            entity.Property(e => e.AddrNo).HasMaxLength(255);
            entity.Property(e => e.AppointDate)
                .HasMaxLength(255)
                .HasColumnName("Appoint_Date");
            entity.Property(e => e.Area).HasMaxLength(255);
            entity.Property(e => e.BankSelectedCs)
                .HasMaxLength(255)
                .HasColumnName("BankSelected_CS");
            entity.Property(e => e.BookDate).HasMaxLength(255);
            entity.Property(e => e.Build).HasMaxLength(255);
            entity.Property(e => e.CodeVerify).HasMaxLength(255);
            entity.Property(e => e.ContactLogDate)
                .HasMaxLength(255)
                .HasColumnName("ContactLog_Date");
            entity.Property(e => e.ContractDate).HasMaxLength(255);
            entity.Property(e => e.ContractMobile).HasMaxLength(255);
            entity.Property(e => e.ContractName).HasMaxLength(255);
            entity.Property(e => e.ContractNumber).HasMaxLength(255);
            entity.Property(e => e.Csresponse)
                .HasMaxLength(255)
                .HasColumnName("CSResponse");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(255)
                .HasColumnName("CustomerID");
            entity.Property(e => e.CustomerMobile).HasMaxLength(255);
            entity.Property(e => e.CustomerName).HasMaxLength(255);
            entity.Property(e => e.Defect).HasMaxLength(255);
            entity.Property(e => e.DefectStatus).HasMaxLength(255);
            entity.Property(e => e.ExpectTransfer).HasMaxLength(255);
            entity.Property(e => e.ExpectTransferBy).HasMaxLength(255);
            entity.Property(e => e.FinanceCareDraft).HasMaxLength(255);
            entity.Property(e => e.Floor).HasMaxLength(255);
            entity.Property(e => e.InspectCount)
                .HasMaxLength(255)
                .HasColumnName("Inspect_Count");
            entity.Property(e => e.LastExpectDate).HasMaxLength(255);
            entity.Property(e => e.LetterDueDateCs)
                .HasMaxLength(255)
                .HasColumnName("LetterDueDate_CS");
            entity.Property(e => e.LineContract).HasMaxLength(255);
            entity.Property(e => e.LoanBankNameFinplus)
                .HasMaxLength(255)
                .HasColumnName("LoanBankName_FINPlus");
            entity.Property(e => e.LoanBankNameSelect)
                .HasMaxLength(255)
                .HasColumnName("LoanBankName_Select");
            entity.Property(e => e.LoanStatusNameFinplus)
                .HasMaxLength(255)
                .HasColumnName("LoanStatusName_FINPlus");
            entity.Property(e => e.MeterTypeName).HasMaxLength(255);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(255)
                .HasColumnName("ProjectID");
            entity.Property(e => e.ProjectName).HasMaxLength(255);
            entity.Property(e => e.Qc5FinishDate)
                .HasMaxLength(255)
                .HasColumnName("QC5_FinishDate");
            entity.Property(e => e.Qc6Date)
                .HasMaxLength(255)
                .HasColumnName("QC6_Date");
            entity.Property(e => e.Qc6Status)
                .HasMaxLength(255)
                .HasColumnName("QC6_Status");
            entity.Property(e => e.ReceiveDocument).HasMaxLength(255);
            entity.Property(e => e.ReceiveRoomAgreementDate).HasMaxLength(255);
            entity.Property(e => e.ReceiveRoomDate).HasMaxLength(255);
            entity.Property(e => e.Redemption).HasMaxLength(255);
            entity.Property(e => e.RemarkUnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("RemarkUnitStatus_CS");
            entity.Property(e => e.Room).HasMaxLength(255);
            entity.Property(e => e.SaveDocument).HasMaxLength(255);
            entity.Property(e => e.SellingPrice).HasMaxLength(255);
            entity.Property(e => e.TransferDate).HasMaxLength(255);
            entity.Property(e => e.TransferDueDateCs)
                .HasMaxLength(255)
                .HasColumnName("TransferDueDate_CS");
            entity.Property(e => e.UnitCode).HasMaxLength(255);
            entity.Property(e => e.UnitStatus).HasMaxLength(255);
            entity.Property(e => e.UnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("UnitStatus_CS");
            entity.Property(e => e.UnitType).HasMaxLength(255);
        });

        modelBuilder.Entity<TempEqc019>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_EQC019");

            entity.Property(e => e.AddrNo).HasMaxLength(255);
            entity.Property(e => e.AppointDate)
                .HasMaxLength(255)
                .HasColumnName("Appoint_Date");
            entity.Property(e => e.Area).HasMaxLength(255);
            entity.Property(e => e.BankSelectedCs)
                .HasMaxLength(255)
                .HasColumnName("BankSelected_CS");
            entity.Property(e => e.BookDate).HasMaxLength(255);
            entity.Property(e => e.Build).HasMaxLength(255);
            entity.Property(e => e.CodeVerify).HasMaxLength(255);
            entity.Property(e => e.ContactLogDate)
                .HasMaxLength(255)
                .HasColumnName("ContactLog_Date");
            entity.Property(e => e.ContractDate).HasMaxLength(255);
            entity.Property(e => e.ContractMobile).HasMaxLength(255);
            entity.Property(e => e.ContractName).HasMaxLength(255);
            entity.Property(e => e.ContractNumber).HasMaxLength(255);
            entity.Property(e => e.Csresponse)
                .HasMaxLength(255)
                .HasColumnName("CSResponse");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(255)
                .HasColumnName("CustomerID");
            entity.Property(e => e.CustomerMobile).HasMaxLength(255);
            entity.Property(e => e.CustomerName).HasMaxLength(255);
            entity.Property(e => e.Defect).HasMaxLength(255);
            entity.Property(e => e.DefectStatus).HasMaxLength(255);
            entity.Property(e => e.ExpectTransfer).HasMaxLength(255);
            entity.Property(e => e.ExpectTransferBy).HasMaxLength(255);
            entity.Property(e => e.FinanceCareDraft).HasMaxLength(255);
            entity.Property(e => e.Floor).HasMaxLength(255);
            entity.Property(e => e.InspectCount)
                .HasMaxLength(255)
                .HasColumnName("Inspect_Count");
            entity.Property(e => e.LastExpectDate).HasMaxLength(255);
            entity.Property(e => e.LetterDueDateCs)
                .HasMaxLength(255)
                .HasColumnName("LetterDueDate_CS");
            entity.Property(e => e.LineContract).HasMaxLength(255);
            entity.Property(e => e.LoanBankNameFinplus)
                .HasMaxLength(255)
                .HasColumnName("LoanBankName_FINPlus");
            entity.Property(e => e.LoanBankNameSelect)
                .HasMaxLength(255)
                .HasColumnName("LoanBankName_Select");
            entity.Property(e => e.LoanStatusNameFinplus)
                .HasMaxLength(255)
                .HasColumnName("LoanStatusName_FINPlus");
            entity.Property(e => e.MeterTypeName).HasMaxLength(255);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(255)
                .HasColumnName("ProjectID");
            entity.Property(e => e.ProjectName).HasMaxLength(255);
            entity.Property(e => e.Qc5FinishDate)
                .HasMaxLength(255)
                .HasColumnName("QC5_FinishDate");
            entity.Property(e => e.Qc6Date)
                .HasMaxLength(255)
                .HasColumnName("QC6_Date");
            entity.Property(e => e.Qc6Status)
                .HasMaxLength(255)
                .HasColumnName("QC6_Status");
            entity.Property(e => e.ReceiveDocument).HasMaxLength(255);
            entity.Property(e => e.ReceiveRoomAgreementDate).HasMaxLength(255);
            entity.Property(e => e.ReceiveRoomDate).HasMaxLength(255);
            entity.Property(e => e.Redemption).HasMaxLength(255);
            entity.Property(e => e.RemarkUnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("RemarkUnitStatus_CS");
            entity.Property(e => e.Room).HasMaxLength(255);
            entity.Property(e => e.SaveDocument).HasMaxLength(255);
            entity.Property(e => e.SellingPrice).HasMaxLength(255);
            entity.Property(e => e.TransferDate).HasMaxLength(255);
            entity.Property(e => e.TransferDueDateCs)
                .HasMaxLength(255)
                .HasColumnName("TransferDueDate_CS");
            entity.Property(e => e.UnitCode).HasMaxLength(255);
            entity.Property(e => e.UnitStatus).HasMaxLength(255);
            entity.Property(e => e.UnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("UnitStatus_CS");
            entity.Property(e => e.UnitType).HasMaxLength(255);
        });

        modelBuilder.Entity<TempEqc020>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_EQC020");

            entity.Property(e => e.AddrNo).HasMaxLength(255);
            entity.Property(e => e.AppointDate)
                .HasMaxLength(255)
                .HasColumnName("Appoint_Date");
            entity.Property(e => e.Area).HasMaxLength(255);
            entity.Property(e => e.BankSelectedCs)
                .HasMaxLength(255)
                .HasColumnName("BankSelected_CS");
            entity.Property(e => e.BookDate).HasMaxLength(255);
            entity.Property(e => e.Build).HasMaxLength(255);
            entity.Property(e => e.CodeVerify).HasMaxLength(255);
            entity.Property(e => e.ContactLogDate)
                .HasMaxLength(255)
                .HasColumnName("ContactLog_Date");
            entity.Property(e => e.ContractDate).HasMaxLength(255);
            entity.Property(e => e.ContractMobile).HasMaxLength(255);
            entity.Property(e => e.ContractName).HasMaxLength(255);
            entity.Property(e => e.ContractNumber).HasMaxLength(255);
            entity.Property(e => e.Csresponse)
                .HasMaxLength(255)
                .HasColumnName("CSResponse");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(255)
                .HasColumnName("CustomerID");
            entity.Property(e => e.CustomerMobile).HasMaxLength(255);
            entity.Property(e => e.CustomerName).HasMaxLength(255);
            entity.Property(e => e.Defect).HasMaxLength(255);
            entity.Property(e => e.DefectStatus).HasMaxLength(255);
            entity.Property(e => e.ExpectTransfer).HasMaxLength(255);
            entity.Property(e => e.ExpectTransferBy).HasMaxLength(255);
            entity.Property(e => e.FinanceCareDraft).HasMaxLength(255);
            entity.Property(e => e.Floor).HasMaxLength(255);
            entity.Property(e => e.InspectCount)
                .HasMaxLength(255)
                .HasColumnName("Inspect_Count");
            entity.Property(e => e.LastExpectDate).HasMaxLength(255);
            entity.Property(e => e.LetterDueDateCs)
                .HasMaxLength(255)
                .HasColumnName("LetterDueDate_CS");
            entity.Property(e => e.LineContract).HasMaxLength(255);
            entity.Property(e => e.LoanBankNameFinplus)
                .HasMaxLength(255)
                .HasColumnName("LoanBankName_FINPlus");
            entity.Property(e => e.LoanBankNameSelect)
                .HasMaxLength(255)
                .HasColumnName("LoanBankName_Select");
            entity.Property(e => e.LoanStatusNameFinplus)
                .HasMaxLength(255)
                .HasColumnName("LoanStatusName_FINPlus");
            entity.Property(e => e.MeterTypeName).HasMaxLength(255);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(255)
                .HasColumnName("ProjectID");
            entity.Property(e => e.ProjectName).HasMaxLength(255);
            entity.Property(e => e.Qc5FinishDate)
                .HasMaxLength(255)
                .HasColumnName("QC5_FinishDate");
            entity.Property(e => e.Qc6Date)
                .HasMaxLength(255)
                .HasColumnName("QC6_Date");
            entity.Property(e => e.Qc6Status)
                .HasMaxLength(255)
                .HasColumnName("QC6_Status");
            entity.Property(e => e.ReceiveDocument).HasMaxLength(255);
            entity.Property(e => e.ReceiveRoomAgreementDate).HasMaxLength(255);
            entity.Property(e => e.ReceiveRoomDate).HasMaxLength(255);
            entity.Property(e => e.Redemption).HasMaxLength(255);
            entity.Property(e => e.RemarkUnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("RemarkUnitStatus_CS");
            entity.Property(e => e.Room).HasMaxLength(255);
            entity.Property(e => e.SaveDocument).HasMaxLength(255);
            entity.Property(e => e.SellingPrice).HasMaxLength(255);
            entity.Property(e => e.TransferDate).HasMaxLength(255);
            entity.Property(e => e.TransferDueDateCs)
                .HasMaxLength(255)
                .HasColumnName("TransferDueDate_CS");
            entity.Property(e => e.UnitCode).HasMaxLength(255);
            entity.Property(e => e.UnitStatus).HasMaxLength(255);
            entity.Property(e => e.UnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("UnitStatus_CS");
            entity.Property(e => e.UnitType).HasMaxLength(255);
        });

        modelBuilder.Entity<TempEqc022>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_EQC022");

            entity.Property(e => e.AddrNo).HasMaxLength(255);
            entity.Property(e => e.AppointDate)
                .HasMaxLength(255)
                .HasColumnName("Appoint_Date");
            entity.Property(e => e.Area).HasMaxLength(255);
            entity.Property(e => e.BankSelectedCs)
                .HasMaxLength(255)
                .HasColumnName("BankSelected_CS");
            entity.Property(e => e.BookDate).HasMaxLength(255);
            entity.Property(e => e.Build).HasMaxLength(255);
            entity.Property(e => e.CodeVerify).HasMaxLength(255);
            entity.Property(e => e.ContactLogDate)
                .HasMaxLength(255)
                .HasColumnName("ContactLog_Date");
            entity.Property(e => e.ContractDate).HasMaxLength(255);
            entity.Property(e => e.ContractMobile).HasMaxLength(255);
            entity.Property(e => e.ContractName).HasMaxLength(255);
            entity.Property(e => e.ContractNumber).HasMaxLength(255);
            entity.Property(e => e.Csresponse)
                .HasMaxLength(255)
                .HasColumnName("CSResponse");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(255)
                .HasColumnName("CustomerID");
            entity.Property(e => e.CustomerMobile).HasMaxLength(255);
            entity.Property(e => e.CustomerName).HasMaxLength(255);
            entity.Property(e => e.Defect).HasMaxLength(255);
            entity.Property(e => e.DefectStatus).HasMaxLength(255);
            entity.Property(e => e.ExpectTransfer).HasMaxLength(255);
            entity.Property(e => e.ExpectTransferBy).HasMaxLength(255);
            entity.Property(e => e.FinanceCareDraft).HasMaxLength(255);
            entity.Property(e => e.Floor).HasMaxLength(255);
            entity.Property(e => e.InspectCount)
                .HasMaxLength(255)
                .HasColumnName("Inspect_Count");
            entity.Property(e => e.LastExpectDate).HasMaxLength(255);
            entity.Property(e => e.LetterDueDateCs)
                .HasMaxLength(255)
                .HasColumnName("LetterDueDate_CS");
            entity.Property(e => e.LineContract).HasMaxLength(255);
            entity.Property(e => e.LoanBankNameFinplus)
                .HasMaxLength(255)
                .HasColumnName("LoanBankName_FINPlus");
            entity.Property(e => e.LoanBankNameSelect)
                .HasMaxLength(255)
                .HasColumnName("LoanBankName_Select");
            entity.Property(e => e.LoanStatusNameFinplus)
                .HasMaxLength(255)
                .HasColumnName("LoanStatusName_FINPlus");
            entity.Property(e => e.MeterTypeName).HasMaxLength(255);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(255)
                .HasColumnName("ProjectID");
            entity.Property(e => e.Q1).HasMaxLength(255);
            entity.Property(e => e.Q10).HasMaxLength(255);
            entity.Property(e => e.Q1Detail)
                .HasMaxLength(255)
                .HasColumnName("Q1_Detail");
            entity.Property(e => e.Q2).HasMaxLength(255);
            entity.Property(e => e.Q2Detail)
                .HasMaxLength(255)
                .HasColumnName("Q2_Detail");
            entity.Property(e => e.Q3).HasMaxLength(255);
            entity.Property(e => e.Q3Detail)
                .HasMaxLength(255)
                .HasColumnName("Q3_Detail");
            entity.Property(e => e.Q4).HasMaxLength(255);
            entity.Property(e => e.Q4Detail)
                .HasMaxLength(255)
                .HasColumnName("Q4_Detail");
            entity.Property(e => e.Q5).HasMaxLength(255);
            entity.Property(e => e.Q5Detail)
                .HasMaxLength(255)
                .HasColumnName("Q5_Detail");
            entity.Property(e => e.Q6).HasMaxLength(255);
            entity.Property(e => e.Q6Detail)
                .HasMaxLength(255)
                .HasColumnName("Q6_Detail");
            entity.Property(e => e.Q7).HasMaxLength(255);
            entity.Property(e => e.Q7Detail)
                .HasMaxLength(255)
                .HasColumnName("Q7_Detail");
            entity.Property(e => e.Q8).HasMaxLength(255);
            entity.Property(e => e.Q8Detail)
                .HasMaxLength(255)
                .HasColumnName("Q8_Detail");
            entity.Property(e => e.Q9).HasMaxLength(255);
            entity.Property(e => e.Q9Detail)
                .HasMaxLength(255)
                .HasColumnName("Q9_Detail");
            entity.Property(e => e.Qc5FinishDate)
                .HasMaxLength(255)
                .HasColumnName("QC5_FinishDate");
            entity.Property(e => e.Qc6Date)
                .HasMaxLength(255)
                .HasColumnName("QC6_Date");
            entity.Property(e => e.Qc6Status)
                .HasMaxLength(255)
                .HasColumnName("QC6_Status");
            entity.Property(e => e.ReceiveDocument).HasMaxLength(255);
            entity.Property(e => e.ReceiveRoomAgreementDate).HasMaxLength(255);
            entity.Property(e => e.ReceiveRoomDate).HasMaxLength(255);
            entity.Property(e => e.Redemption).HasMaxLength(255);
            entity.Property(e => e.RemarkUnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("RemarkUnitStatus_CS");
            entity.Property(e => e.Room).HasMaxLength(255);
            entity.Property(e => e.SaveDocument).HasMaxLength(255);
            entity.Property(e => e.SellingPrice).HasMaxLength(255);
            entity.Property(e => e.TransferDate).HasMaxLength(255);
            entity.Property(e => e.TransferDueDateCs)
                .HasMaxLength(255)
                .HasColumnName("TransferDueDate_CS");
            entity.Property(e => e.UnitCode).HasMaxLength(255);
            entity.Property(e => e.UnitStatus).HasMaxLength(255);
            entity.Property(e => e.UnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("UnitStatus_CS");
            entity.Property(e => e.UnitType).HasMaxLength(255);
        });

        modelBuilder.Entity<TempEqc025>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_EQC025");

            entity.Property(e => e.Active)
                .HasMaxLength(255)
                .HasColumnName("ACTIVE");
            entity.Property(e => e.BookDate).HasMaxLength(255);
            entity.Property(e => e.Csresponse)
                .HasMaxLength(255)
                .HasColumnName("CSResponse");
            entity.Property(e => e.CustomerName).HasMaxLength(255);
            entity.Property(e => e.ExpectTransfer).HasMaxLength(255);
            entity.Property(e => e.ExpectTransferBy).HasMaxLength(255);
            entity.Property(e => e.LetterDueDateCs)
                .HasMaxLength(255)
                .HasColumnName("LetterDueDate_CS");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(255)
                .HasColumnName("ProjectID");
            entity.Property(e => e.ProjectName).HasMaxLength(255);
            entity.Property(e => e.Q1).HasMaxLength(255);
            entity.Property(e => e.Q10).HasMaxLength(255);
            entity.Property(e => e.Q2).HasMaxLength(255);
            entity.Property(e => e.Q3).HasMaxLength(255);
            entity.Property(e => e.Q4).HasMaxLength(255);
            entity.Property(e => e.Q5).HasMaxLength(255);
            entity.Property(e => e.Q6).HasMaxLength(255);
            entity.Property(e => e.Q6Detail)
                .HasMaxLength(255)
                .HasColumnName("Q6_Detail");
            entity.Property(e => e.Q7).HasMaxLength(255);
            entity.Property(e => e.Q7Detail)
                .HasMaxLength(255)
                .HasColumnName("Q7_Detail");
            entity.Property(e => e.Q8).HasMaxLength(255);
            entity.Property(e => e.Q8Detail)
                .HasMaxLength(255)
                .HasColumnName("Q8_Detail");
            entity.Property(e => e.Q9).HasMaxLength(255);
            entity.Property(e => e.Q9Detail)
                .HasMaxLength(255)
                .HasColumnName("Q9_Detail");
            entity.Property(e => e.RemarkUnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("RemarkUnitStatus_CS");
            entity.Property(e => e.TransferDueDateCs)
                .HasMaxLength(255)
                .HasColumnName("TransferDueDate_CS");
            entity.Property(e => e.UnitCode).HasMaxLength(255);
            entity.Property(e => e.UnitStatus).HasMaxLength(255);
            entity.Property(e => e.UnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("UnitStatus_CS");
        });

        modelBuilder.Entity<TempJenieUnit>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_jenie_unit");

            entity.Property(e => e.AddrNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ContratNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UnitCode)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TempKaveshift>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_KAVESHIFT");

            entity.Property(e => e.BankCode).HasMaxLength(255);
            entity.Property(e => e.CustomerName).HasMaxLength(255);
            entity.Property(e => e.ProgressStatus).HasMaxLength(255);
            entity.Property(e => e.UnitCode).HasMaxLength(255);
            entity.Property(e => e.UnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("UnitStatus_CS");
        });

        modelBuilder.Entity<TempKavespace>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_KAVESPACE");

            entity.Property(e => e.BankCode).HasMaxLength(255);
            entity.Property(e => e.CustomerName).HasMaxLength(255);
            entity.Property(e => e.ProgressStatus).HasMaxLength(255);
            entity.Property(e => e.UnitCode).HasMaxLength(255);
            entity.Property(e => e.UnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("UnitStatus_CS");
        });

        modelBuilder.Entity<TempLetter>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_letter");

            entity.Property(e => e.ContractNumber).HasMaxLength(255);
            entity.Property(e => e.Csresponse)
                .HasMaxLength(255)
                .HasColumnName("CSResponse");
            entity.Property(e => e.CustomerEmail)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CustomerMobile).HasMaxLength(255);
            entity.Property(e => e.CustomerName).HasMaxLength(255);
            entity.Property(e => e.SendLang).HasMaxLength(255);
            entity.Property(e => e.UnitCode).HasMaxLength(255);
        });

        modelBuilder.Entity<TempLine>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_line");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.LineKey)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TempM1c001>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_M1C001");

            entity.Property(e => e.AddrNo).HasMaxLength(255);
            entity.Property(e => e.AppointDate)
                .HasMaxLength(255)
                .HasColumnName("Appoint_Date");
            entity.Property(e => e.Area).HasMaxLength(255);
            entity.Property(e => e.BankSelectedCs)
                .HasMaxLength(255)
                .HasColumnName("BankSelected_CS");
            entity.Property(e => e.BookDate).HasMaxLength(255);
            entity.Property(e => e.Build).HasMaxLength(255);
            entity.Property(e => e.CodeVerify).HasMaxLength(255);
            entity.Property(e => e.ContactLogDate)
                .HasMaxLength(255)
                .HasColumnName("ContactLog_Date");
            entity.Property(e => e.ContractDate).HasMaxLength(255);
            entity.Property(e => e.ContractMobile).HasMaxLength(255);
            entity.Property(e => e.ContractName).HasMaxLength(255);
            entity.Property(e => e.ContractNumber).HasMaxLength(255);
            entity.Property(e => e.Csresponse)
                .HasMaxLength(255)
                .HasColumnName("CSResponse");
            entity.Property(e => e.Csresponse1)
                .HasMaxLength(255)
                .HasColumnName("CSResponse1");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(255)
                .HasColumnName("CustomerID");
            entity.Property(e => e.CustomerMobile).HasMaxLength(255);
            entity.Property(e => e.CustomerName).HasMaxLength(255);
            entity.Property(e => e.Defect).HasMaxLength(255);
            entity.Property(e => e.DefectStatus).HasMaxLength(255);
            entity.Property(e => e.ExpectTransfer).HasMaxLength(255);
            entity.Property(e => e.ExpectTransfer1).HasMaxLength(255);
            entity.Property(e => e.ExpectTransferBy).HasMaxLength(255);
            entity.Property(e => e.ExpectTransferBy1).HasMaxLength(255);
            entity.Property(e => e.FinanceCareDraft).HasMaxLength(255);
            entity.Property(e => e.Floor).HasMaxLength(255);
            entity.Property(e => e.InspectCount)
                .HasMaxLength(255)
                .HasColumnName("Inspect_Count");
            entity.Property(e => e.LastExpectDate).HasMaxLength(255);
            entity.Property(e => e.LetterDueDateCs)
                .HasMaxLength(255)
                .HasColumnName("LetterDueDate_CS");
            entity.Property(e => e.LetterDueDateCs1)
                .HasMaxLength(255)
                .HasColumnName("LetterDueDate_CS1");
            entity.Property(e => e.LineContract).HasMaxLength(255);
            entity.Property(e => e.LoanBankNameFinplus)
                .HasMaxLength(255)
                .HasColumnName("LoanBankName_FINPlus");
            entity.Property(e => e.LoanBankNameSelect)
                .HasMaxLength(255)
                .HasColumnName("LoanBankName_Select");
            entity.Property(e => e.LoanStatusNameFinplus)
                .HasMaxLength(255)
                .HasColumnName("LoanStatusName_FINPlus");
            entity.Property(e => e.MeterTypeName).HasMaxLength(255);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(255)
                .HasColumnName("ProjectID");
            entity.Property(e => e.ProjectName).HasMaxLength(255);
            entity.Property(e => e.Qc5FinishDate)
                .HasMaxLength(255)
                .HasColumnName("QC5_FinishDate");
            entity.Property(e => e.Qc6Date)
                .HasMaxLength(255)
                .HasColumnName("QC6_Date");
            entity.Property(e => e.Qc6Status)
                .HasMaxLength(255)
                .HasColumnName("QC6_Status");
            entity.Property(e => e.ReceiveDocument).HasMaxLength(255);
            entity.Property(e => e.ReceiveRoomAgreementDate).HasMaxLength(255);
            entity.Property(e => e.ReceiveRoomDate).HasMaxLength(255);
            entity.Property(e => e.Redemption).HasMaxLength(255);
            entity.Property(e => e.RemarkUnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("RemarkUnitStatus_CS");
            entity.Property(e => e.RemarkUnitStatusCs1)
                .HasMaxLength(255)
                .HasColumnName("RemarkUnitStatus_CS1");
            entity.Property(e => e.Room).HasMaxLength(255);
            entity.Property(e => e.SaveDocument).HasMaxLength(255);
            entity.Property(e => e.SellingPrice).HasMaxLength(255);
            entity.Property(e => e.TransferDate).HasMaxLength(255);
            entity.Property(e => e.TransferDueDateCs)
                .HasMaxLength(255)
                .HasColumnName("TransferDueDate_CS");
            entity.Property(e => e.TransferDueDateCs1)
                .HasMaxLength(255)
                .HasColumnName("TransferDueDate_CS1");
            entity.Property(e => e.UnitCode).HasMaxLength(255);
            entity.Property(e => e.UnitStatus).HasMaxLength(255);
            entity.Property(e => e.UnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("UnitStatus_CS");
            entity.Property(e => e.UnitStatusCs1)
                .HasMaxLength(255)
                .HasColumnName("UnitStatus_CS1");
            entity.Property(e => e.UnitStatusCs2)
                .HasMaxLength(255)
                .HasColumnName("UnitStatus_CS2");
            entity.Property(e => e.UnitType).HasMaxLength(255);
        });

        modelBuilder.Entity<TempModiz>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_modiz");

            entity.Property(e => e.Area).HasMaxLength(255);
            entity.Property(e => e.Build).HasMaxLength(255);
            entity.Property(e => e.CustomerId)
                .HasMaxLength(255)
                .HasColumnName("CustomerID");
            entity.Property(e => e.CustomerName).HasMaxLength(255);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(255)
                .HasColumnName("ProjectID");
            entity.Property(e => e.UnitCode).HasMaxLength(255);
            entity.Property(e => e.UnitType).HasMaxLength(255);
        });

        modelBuilder.Entity<TempNewBu>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_new_bu");

            entity.Property(e => e.Buid).HasColumnName("BUID");
            entity.Property(e => e.Buname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Thai_100_CI_AS")
                .HasColumnName("BUName");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Thai_100_CI_AS")
                .HasColumnName("ProjectID");
            entity.Property(e => e.ProjectName)
                .HasMaxLength(400)
                .UseCollation("Thai_100_CI_AS");
        });

        modelBuilder.Entity<TempPpc001>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_PPC001");

            entity.Property(e => e.AddrNo).HasMaxLength(255);
            entity.Property(e => e.AppointDate)
                .HasMaxLength(255)
                .HasColumnName("Appoint_Date");
            entity.Property(e => e.Area).HasMaxLength(255);
            entity.Property(e => e.BankSelectedCs)
                .HasMaxLength(255)
                .HasColumnName("BankSelected_CS");
            entity.Property(e => e.BookDate).HasMaxLength(255);
            entity.Property(e => e.Build).HasMaxLength(255);
            entity.Property(e => e.CodeVerify).HasMaxLength(255);
            entity.Property(e => e.ContactLogDate)
                .HasMaxLength(255)
                .HasColumnName("ContactLog_Date");
            entity.Property(e => e.ContractDate).HasMaxLength(255);
            entity.Property(e => e.ContractMobile).HasMaxLength(255);
            entity.Property(e => e.ContractName).HasMaxLength(255);
            entity.Property(e => e.ContractNumber).HasMaxLength(255);
            entity.Property(e => e.Csresponse)
                .HasMaxLength(255)
                .HasColumnName("CSResponse");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(255)
                .HasColumnName("CustomerID");
            entity.Property(e => e.CustomerMobile).HasMaxLength(255);
            entity.Property(e => e.CustomerName).HasMaxLength(255);
            entity.Property(e => e.Defect).HasMaxLength(255);
            entity.Property(e => e.DefectStatus).HasMaxLength(255);
            entity.Property(e => e.ExpectTransfer).HasMaxLength(255);
            entity.Property(e => e.ExpectTransferBy).HasMaxLength(255);
            entity.Property(e => e.FinanceCareDraft).HasMaxLength(255);
            entity.Property(e => e.Floor).HasMaxLength(255);
            entity.Property(e => e.InspectCount)
                .HasMaxLength(255)
                .HasColumnName("Inspect_Count");
            entity.Property(e => e.LastExpectDate).HasMaxLength(255);
            entity.Property(e => e.LetterDueDateCs)
                .HasMaxLength(255)
                .HasColumnName("LetterDueDate_CS");
            entity.Property(e => e.LineContract).HasMaxLength(255);
            entity.Property(e => e.LoanBankNameFinplus)
                .HasMaxLength(255)
                .HasColumnName("LoanBankName_FINPlus");
            entity.Property(e => e.LoanBankNameSelect)
                .HasMaxLength(255)
                .HasColumnName("LoanBankName_Select");
            entity.Property(e => e.LoanStatusNameFinplus)
                .HasMaxLength(255)
                .HasColumnName("LoanStatusName_FINPlus");
            entity.Property(e => e.MeterTypeName).HasMaxLength(255);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(255)
                .HasColumnName("ProjectID");
            entity.Property(e => e.ProjectName).HasMaxLength(255);
            entity.Property(e => e.Qc5FinishDate)
                .HasMaxLength(255)
                .HasColumnName("QC5_FinishDate");
            entity.Property(e => e.Qc6Date)
                .HasMaxLength(255)
                .HasColumnName("QC6_Date");
            entity.Property(e => e.Qc6Status)
                .HasMaxLength(255)
                .HasColumnName("QC6_Status");
            entity.Property(e => e.ReceiveDocument).HasMaxLength(255);
            entity.Property(e => e.ReceiveRoomAgreementDate).HasMaxLength(255);
            entity.Property(e => e.ReceiveRoomDate).HasMaxLength(255);
            entity.Property(e => e.Redemption).HasMaxLength(255);
            entity.Property(e => e.RemarkUnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("RemarkUnitStatus_CS");
            entity.Property(e => e.Room).HasMaxLength(255);
            entity.Property(e => e.SaveDocument).HasMaxLength(255);
            entity.Property(e => e.SellingPrice).HasMaxLength(255);
            entity.Property(e => e.TransferDate).HasMaxLength(255);
            entity.Property(e => e.TransferDueDateCs)
                .HasMaxLength(255)
                .HasColumnName("TransferDueDate_CS");
            entity.Property(e => e.UnitCode).HasMaxLength(255);
            entity.Property(e => e.UnitStatus).HasMaxLength(255);
            entity.Property(e => e.UnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("UnitStatus_CS");
            entity.Property(e => e.UnitType).HasMaxLength(255);
        });

        modelBuilder.Entity<TempTuc001>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_TUC001");

            entity.Property(e => e.AddrNo).HasMaxLength(255);
            entity.Property(e => e.AppointDate)
                .HasMaxLength(255)
                .HasColumnName("Appoint_Date");
            entity.Property(e => e.Area).HasMaxLength(255);
            entity.Property(e => e.BookDate).HasMaxLength(255);
            entity.Property(e => e.Build).HasMaxLength(255);
            entity.Property(e => e.CodeVerify).HasMaxLength(255);
            entity.Property(e => e.ContactLogDate)
                .HasMaxLength(255)
                .HasColumnName("ContactLog_Date");
            entity.Property(e => e.ContractDate).HasMaxLength(255);
            entity.Property(e => e.ContractMobile).HasMaxLength(255);
            entity.Property(e => e.ContractName).HasMaxLength(255);
            entity.Property(e => e.ContractNumber).HasMaxLength(255);
            entity.Property(e => e.Csresponse)
                .HasMaxLength(255)
                .HasColumnName("CSResponse");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(255)
                .HasColumnName("CustomerID");
            entity.Property(e => e.CustomerMobile).HasMaxLength(255);
            entity.Property(e => e.CustomerName).HasMaxLength(255);
            entity.Property(e => e.Defect).HasMaxLength(255);
            entity.Property(e => e.DefectStatus).HasMaxLength(255);
            entity.Property(e => e.ExpectTransfer).HasMaxLength(255);
            entity.Property(e => e.ExpectTransferBy).HasMaxLength(255);
            entity.Property(e => e.FinanceCareDraft).HasMaxLength(255);
            entity.Property(e => e.Floor).HasMaxLength(255);
            entity.Property(e => e.InspectCount)
                .HasMaxLength(255)
                .HasColumnName("Inspect_Count");
            entity.Property(e => e.LetterDueDateCs)
                .HasMaxLength(255)
                .HasColumnName("LetterDueDate_CS");
            entity.Property(e => e.LoanBankNameFinplus)
                .HasMaxLength(255)
                .HasColumnName("LoanBankName_FINPlus");
            entity.Property(e => e.LoanStatusNameFinplus)
                .HasMaxLength(255)
                .HasColumnName("LoanStatusName_FINPlus");
            entity.Property(e => e.Qc5FinishDate)
                .HasMaxLength(255)
                .HasColumnName("QC5_FinishDate");
            entity.Property(e => e.Qc6Date)
                .HasMaxLength(255)
                .HasColumnName("QC6_Date");
            entity.Property(e => e.Qc6Status)
                .HasMaxLength(255)
                .HasColumnName("QC6_Status");
            entity.Property(e => e.ReceiveDocument).HasMaxLength(255);
            entity.Property(e => e.ReceiveRoomAgreementDate).HasMaxLength(255);
            entity.Property(e => e.ReceiveRoomDate).HasMaxLength(255);
            entity.Property(e => e.RemarkUnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("RemarkUnitStatus_CS");
            entity.Property(e => e.Room).HasMaxLength(255);
            entity.Property(e => e.SaveDocument).HasMaxLength(255);
            entity.Property(e => e.SellingPrice).HasMaxLength(255);
            entity.Property(e => e.TransferDate).HasMaxLength(255);
            entity.Property(e => e.TransferDueDateCs)
                .HasMaxLength(255)
                .HasColumnName("TransferDueDate_CS");
            entity.Property(e => e.UnitCode).HasMaxLength(255);
            entity.Property(e => e.UnitStatus).HasMaxLength(255);
            entity.Property(e => e.UnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("UnitStatus_CS");
            entity.Property(e => e.UnitType).HasMaxLength(255);
        });

        modelBuilder.Entity<TempTuc002>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_TUC002");

            entity.Property(e => e.AddrNo).HasMaxLength(255);
            entity.Property(e => e.AppointDate)
                .HasMaxLength(255)
                .HasColumnName("Appoint_Date");
            entity.Property(e => e.Area).HasMaxLength(255);
            entity.Property(e => e.BankSelectedCs)
                .HasMaxLength(255)
                .HasColumnName("BankSelected_CS");
            entity.Property(e => e.BookDate).HasMaxLength(255);
            entity.Property(e => e.Build).HasMaxLength(255);
            entity.Property(e => e.CodeVerify).HasMaxLength(255);
            entity.Property(e => e.ContactLogDate)
                .HasMaxLength(255)
                .HasColumnName("ContactLog_Date");
            entity.Property(e => e.ContractDate).HasMaxLength(255);
            entity.Property(e => e.ContractMobile).HasMaxLength(255);
            entity.Property(e => e.ContractName).HasMaxLength(255);
            entity.Property(e => e.ContractNumber).HasMaxLength(255);
            entity.Property(e => e.Csresponse)
                .HasMaxLength(255)
                .HasColumnName("CSResponse");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(255)
                .HasColumnName("CustomerID");
            entity.Property(e => e.CustomerMobile).HasMaxLength(255);
            entity.Property(e => e.CustomerName).HasMaxLength(255);
            entity.Property(e => e.Defect).HasMaxLength(255);
            entity.Property(e => e.DefectStatus).HasMaxLength(255);
            entity.Property(e => e.ExpectTransfer).HasMaxLength(255);
            entity.Property(e => e.ExpectTransferBy).HasMaxLength(255);
            entity.Property(e => e.FinanceCareDraft).HasMaxLength(255);
            entity.Property(e => e.Floor).HasMaxLength(255);
            entity.Property(e => e.InspectCount)
                .HasMaxLength(255)
                .HasColumnName("Inspect_Count");
            entity.Property(e => e.LastExpectDate).HasMaxLength(255);
            entity.Property(e => e.LetterDueDateCs)
                .HasMaxLength(255)
                .HasColumnName("LetterDueDate_CS");
            entity.Property(e => e.LineContract).HasMaxLength(255);
            entity.Property(e => e.LoanBankNameFinplus)
                .HasMaxLength(255)
                .HasColumnName("LoanBankName_FINPlus");
            entity.Property(e => e.LoanBankNameSelect)
                .HasMaxLength(255)
                .HasColumnName("LoanBankName_Select");
            entity.Property(e => e.LoanStatusNameFinplus)
                .HasMaxLength(255)
                .HasColumnName("LoanStatusName_FINPlus");
            entity.Property(e => e.MeterTypeName).HasMaxLength(255);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(255)
                .HasColumnName("ProjectID");
            entity.Property(e => e.ProjectName).HasMaxLength(255);
            entity.Property(e => e.Q1).HasMaxLength(255);
            entity.Property(e => e.Q10).HasMaxLength(255);
            entity.Property(e => e.Q1Detail)
                .HasMaxLength(255)
                .HasColumnName("Q1_Detail");
            entity.Property(e => e.Q2).HasMaxLength(255);
            entity.Property(e => e.Q2Detail)
                .HasMaxLength(255)
                .HasColumnName("Q2_Detail");
            entity.Property(e => e.Q3).HasMaxLength(255);
            entity.Property(e => e.Q3Detail)
                .HasMaxLength(255)
                .HasColumnName("Q3_Detail");
            entity.Property(e => e.Q4).HasMaxLength(255);
            entity.Property(e => e.Q4Detail)
                .HasMaxLength(255)
                .HasColumnName("Q4_Detail");
            entity.Property(e => e.Q5).HasMaxLength(255);
            entity.Property(e => e.Q5Detail)
                .HasMaxLength(255)
                .HasColumnName("Q5_Detail");
            entity.Property(e => e.Q6).HasMaxLength(255);
            entity.Property(e => e.Q6Detail)
                .HasMaxLength(255)
                .HasColumnName("Q6_Detail");
            entity.Property(e => e.Q7).HasMaxLength(255);
            entity.Property(e => e.Q7Detail)
                .HasMaxLength(255)
                .HasColumnName("Q7_Detail");
            entity.Property(e => e.Q8).HasMaxLength(255);
            entity.Property(e => e.Q8Detail)
                .HasMaxLength(255)
                .HasColumnName("Q8_Detail");
            entity.Property(e => e.Q9).HasMaxLength(255);
            entity.Property(e => e.Q9Detail)
                .HasMaxLength(255)
                .HasColumnName("Q9_Detail");
            entity.Property(e => e.Qc5FinishDate)
                .HasMaxLength(255)
                .HasColumnName("QC5_FinishDate");
            entity.Property(e => e.Qc6Date)
                .HasMaxLength(255)
                .HasColumnName("QC6_Date");
            entity.Property(e => e.Qc6Status)
                .HasMaxLength(255)
                .HasColumnName("QC6_Status");
            entity.Property(e => e.ReceiveDocument).HasMaxLength(255);
            entity.Property(e => e.ReceiveRoomAgreementDate).HasMaxLength(255);
            entity.Property(e => e.ReceiveRoomDate).HasMaxLength(255);
            entity.Property(e => e.Redemption).HasMaxLength(255);
            entity.Property(e => e.RemarkUnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("RemarkUnitStatus_CS");
            entity.Property(e => e.Room).HasMaxLength(255);
            entity.Property(e => e.SaveDocument).HasMaxLength(255);
            entity.Property(e => e.SellingPrice).HasMaxLength(255);
            entity.Property(e => e.TransferDate).HasMaxLength(255);
            entity.Property(e => e.TransferDueDateCs)
                .HasMaxLength(255)
                .HasColumnName("TransferDueDate_CS");
            entity.Property(e => e.UnitCode).HasMaxLength(255);
            entity.Property(e => e.UnitStatus).HasMaxLength(255);
            entity.Property(e => e.UnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("UnitStatus_CS");
            entity.Property(e => e.UnitType).HasMaxLength(255);
        });

        modelBuilder.Entity<TempUnit>(entity =>
        {
            entity.HasKey(e => e.UnitCode);

            entity.ToTable("temp_unit");

            entity.Property(e => e.UnitCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Csresponse)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CSResponse");
            entity.Property(e => e.UnitStatusCs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UnitStatus_CS");
        });

        modelBuilder.Entity<TempUnit400h006>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_unit_400H006");

            entity.Property(e => e.AddrNo).HasMaxLength(500);
            entity.Property(e => e.AppointDate)
                .HasColumnType("datetime")
                .HasColumnName("Appoint_Date");
            entity.Property(e => e.Area).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BankSelectedIdCs).HasColumnName("BankSelectedID_CS");
            entity.Property(e => e.BookDate).HasColumnType("datetime");
            entity.Property(e => e.Build)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ContractDate).HasColumnType("datetime");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerEmail)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.CustomerMobile)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CustomerName).HasMaxLength(500);
            entity.Property(e => e.DeviateRemark)
                .HasMaxLength(2000)
                .IsUnicode(false);
            entity.Property(e => e.DeviateStatusId).HasColumnName("DeviateStatusID");
            entity.Property(e => e.ExpectTransfer)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ExpectTransferById).HasColumnName("ExpectTransferByID");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.InspectCount).HasColumnName("Inspect_Count");
            entity.Property(e => e.InspectDate)
                .HasColumnType("datetime")
                .HasColumnName("Inspect_Date");
            entity.Property(e => e.InspectId).HasColumnName("Inspect_ID");
            entity.Property(e => e.LetterDueDateCs)
                .HasColumnType("datetime")
                .HasColumnName("LetterDueDate_CS");
            entity.Property(e => e.MeterTypeId).HasColumnName("MeterTypeID");
            entity.Property(e => e.Position)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.Qc1).HasColumnName("QC1");
            entity.Property(e => e.Qc1Date)
                .HasColumnType("datetime")
                .HasColumnName("QC1_Date");
            entity.Property(e => e.Qc1Id).HasColumnName("QC1_ID");
            entity.Property(e => e.Qc1StatusId).HasColumnName("QC1_StatusID");
            entity.Property(e => e.Qc2).HasColumnName("QC2");
            entity.Property(e => e.Qc2Date)
                .HasColumnType("datetime")
                .HasColumnName("QC2_Date");
            entity.Property(e => e.Qc2Id).HasColumnName("QC2_ID");
            entity.Property(e => e.Qc2StatusId).HasColumnName("QC2_StatusID");
            entity.Property(e => e.Qc3).HasColumnName("QC3");
            entity.Property(e => e.Qc3Date)
                .HasColumnType("datetime")
                .HasColumnName("QC3_Date");
            entity.Property(e => e.Qc3Id).HasColumnName("QC3_ID");
            entity.Property(e => e.Qc3StatusId).HasColumnName("QC3_StatusID");
            entity.Property(e => e.Qc4).HasColumnName("QC4");
            entity.Property(e => e.Qc4Date)
                .HasColumnType("datetime")
                .HasColumnName("QC4_Date");
            entity.Property(e => e.Qc4Id).HasColumnName("QC4_ID");
            entity.Property(e => e.Qc4StatusId).HasColumnName("QC4_StatusID");
            entity.Property(e => e.Qc5).HasColumnName("QC5");
            entity.Property(e => e.Qc5Date)
                .HasColumnType("datetime")
                .HasColumnName("QC5_Date");
            entity.Property(e => e.Qc5FinishDate)
                .HasColumnType("datetime")
                .HasColumnName("QC5_FinishDate");
            entity.Property(e => e.Qc5Id).HasColumnName("QC5_ID");
            entity.Property(e => e.Qc5Open).HasColumnName("QC5_Open");
            entity.Property(e => e.Qc5OpenDate)
                .HasColumnType("datetime")
                .HasColumnName("QC5_Open_Date");
            entity.Property(e => e.Qc5OpenId).HasColumnName("QC5_Open_ID");
            entity.Property(e => e.Qc5OpenStatusId).HasColumnName("QC5_Open_StatusID");
            entity.Property(e => e.Qc5StatusId).HasColumnName("QC5_StatusID");
            entity.Property(e => e.Qc6).HasColumnName("QC6");
            entity.Property(e => e.Qc6Date)
                .HasColumnType("datetime")
                .HasColumnName("QC6_Date");
            entity.Property(e => e.Qc6Id).HasColumnName("QC6_ID");
            entity.Property(e => e.Qc6Matrix).HasColumnName("QC6_Matrix");
            entity.Property(e => e.Qc6StatusId).HasColumnName("QC6_StatusID");
            entity.Property(e => e.Qc6WipMatrixId).HasColumnName("QC6_WIP_Matrix_ID");
            entity.Property(e => e.ReceiveRoomAgreementDate).HasColumnType("datetime");
            entity.Property(e => e.ReceiveRoomDate).HasColumnType("datetime");
            entity.Property(e => e.RemarkUnitStatusCs).HasColumnName("RemarkUnitStatus_CS");
            entity.Property(e => e.SellingPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TransferDate).HasColumnType("datetime");
            entity.Property(e => e.TransferDateCs)
                .HasColumnType("datetime")
                .HasColumnName("TransferDate_CS");
            entity.Property(e => e.TransferDueDateCs)
                .HasColumnType("datetime")
                .HasColumnName("TransferDueDate_CS");
            entity.Property(e => e.UnitCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UnitStatusCs).HasColumnName("UnitStatus_CS");
            entity.Property(e => e.UnitType)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateDateUnitStatusCs)
                .HasColumnType("datetime")
                .HasColumnName("UpdateDateUnitStatus_CS");
            entity.Property(e => e.WipMatrixId).HasColumnName("WIP_Matrix_ID");
        });

        modelBuilder.Entity<TempUnitCmd005>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_unit_cmd005");

            entity.Property(e => e.ProjectId)
                .HasMaxLength(255)
                .HasColumnName("ProjectID");
            entity.Property(e => e.UnitCode).HasMaxLength(255);
        });

        modelBuilder.Entity<TempUnitMaxxi>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_unit_maxxi");

            entity.Property(e => e.AddrNo)
                .HasMaxLength(255)
                .HasColumnName("Addr No");
            entity.Property(e => e.ContractNumber).HasMaxLength(255);
            entity.Property(e => e.CustomerId)
                .HasMaxLength(255)
                .HasColumnName("CustomerID");
            entity.Property(e => e.CustomerMobile).HasMaxLength(255);
            entity.Property(e => e.CustomerName).HasMaxLength(255);
            entity.Property(e => e.Room).HasMaxLength(255);
        });

        modelBuilder.Entity<TempUnitStatus>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_unit_status");

            entity.Property(e => e.RemarkUnitStatus).HasMaxLength(255);
            entity.Property(e => e.UnitCode).HasMaxLength(255);
            entity.Property(e => e.UnitStatusCs)
                .HasMaxLength(255)
                .HasColumnName("UnitStatus_CS");
        });

        modelBuilder.Entity<TmAnswer>(entity =>
        {
            entity.ToTable("tm_Answer");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.OtherText).HasMaxLength(200);
            entity.Property(e => e.OtherTextAfter).HasMaxLength(200);
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Question).WithMany(p => p.TmAnswers)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK_tm_Answer_tm_Question");
        });

        modelBuilder.Entity<TmBank>(entity =>
        {
            entity.ToTable("tm_Bank");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.BankCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TmBu>(entity =>
        {
            entity.ToTable("tm_BU");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TmBuprojectMapping>(entity =>
        {
            entity.ToTable("tm_BUProject_Mapping");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Buid).HasColumnName("BUID");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");

            entity.HasOne(d => d.Bu).WithMany(p => p.TmBuprojectMappings)
                .HasForeignKey(d => d.Buid)
                .HasConstraintName("FK_tm_BUProject_Mapping_tm_BU");

            entity.HasOne(d => d.Project).WithMany(p => p.TmBuprojectMappings)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_tm_BUProject_Mapping_tm_Project");
        });

        modelBuilder.Entity<TmCloseProject>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tm_CloseProject");

            entity.Property(e => e.CloseDate).HasColumnType("datetime");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
        });

        modelBuilder.Entity<TmCompany>(entity =>
        {
            entity.ToTable("tm_Company");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(2000);
            entity.Property(e => e.NameEng)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("Name_Eng");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TmDataSource>(entity =>
        {
            entity.HasKey(e => e.DatasourceId);

            entity.ToTable("tm_DataSource");

            entity.Property(e => e.DatasourceId)
                .ValueGeneratedNever()
                .HasColumnName("Datasource_ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DatasourceDesc)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DatasourceName)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TmDefectArea>(entity =>
        {
            entity.ToTable("tm_DefectArea");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(1000);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TmDefectAreaTypeMapping>(entity =>
        {
            entity.ToTable("tm_DefectAreaType_Mapping");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.DefectAreaId).HasColumnName("DefectAreaID");
            entity.Property(e => e.DefectTypeId).HasColumnName("DefectTypeID");

            entity.HasOne(d => d.DefectArea).WithMany(p => p.TmDefectAreaTypeMappings)
                .HasForeignKey(d => d.DefectAreaId)
                .HasConstraintName("FK_tm_DefectAreaType_Mapping_tm_DefectArea");

            entity.HasOne(d => d.DefectType).WithMany(p => p.TmDefectAreaTypeMappings)
                .HasForeignKey(d => d.DefectTypeId)
                .HasConstraintName("FK_tm_DefectAreaType_Mapping_tm_DefectType");
        });

        modelBuilder.Entity<TmDefectDescription>(entity =>
        {
            entity.ToTable("tm_DefectDescription");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DefectTypeId).HasColumnName("DefectTypeID");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(1000);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.DefectType).WithMany(p => p.TmDefectDescriptions)
                .HasForeignKey(d => d.DefectTypeId)
                .HasConstraintName("FK_tm_DefectDescription_tm_DefectType");
        });

        modelBuilder.Entity<TmDefectType>(entity =>
        {
            entity.ToTable("tm_DefectType");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(1000);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TmEquipment>(entity =>
        {
            entity.ToTable("tm_Equipment");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CraeteDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Unit)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TmEvent>(entity =>
        {
            entity.ToTable("tm_Event");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CraeteDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.Location).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(500);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.TmEvents)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_tm_Event_tm_Project");
        });

        modelBuilder.Entity<TmExt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tm_ExtType");

            entity.ToTable("tm_Ext");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ExtTypeId).HasColumnName("ExtTypeID");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.NameEng).HasMaxLength(200);
            entity.Property(e => e.OtherValue).IsUnicode(false);
            entity.Property(e => e.OtherValue2).HasMaxLength(50);
            entity.Property(e => e.ParentId).HasColumnName("ParentID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.ExtType).WithMany(p => p.TmExts)
                .HasForeignKey(d => d.ExtTypeId)
                .HasConstraintName("FK_tm_Ext_tm_Ext");
        });

        modelBuilder.Entity<TmExtType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tm_ExtType_1");

            entity.ToTable("tm_ExtType");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TmFuniture>(entity =>
        {
            entity.ToTable("tm_Funiture");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CraeteDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TmHoliday>(entity =>
        {
            entity.ToTable("tm_Holiday");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.HolidayDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(500);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TmLandOffice>(entity =>
        {
            entity.ToTable("tm_LandOffice");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(2000);
            entity.Property(e => e.NameEng)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("Name_Eng");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TmLetterDayDue>(entity =>
        {
            entity.ToTable("tm_LetterDayDue");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagAcive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TmLetterSendReason>(entity =>
        {
            entity.ToTable("tm_LetterSendReason");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(500);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TmLineToken>(entity =>
        {
            entity.ToTable("tm_LineToken");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BankId).HasColumnName("BankID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProjectZoneId).HasColumnName("ProjectZoneID");
            entity.Property(e => e.Token).IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Bank).WithMany(p => p.TmLineTokens)
                .HasForeignKey(d => d.BankId)
                .HasConstraintName("FK_tm_LineToken_tm_Bank");

            entity.HasOne(d => d.ProjectZone).WithMany(p => p.TmLineTokens)
                .HasForeignKey(d => d.ProjectZoneId)
                .HasConstraintName("FK_tm_LineToken_tm_Ext");
        });

        modelBuilder.Entity<TmLineTokenBk>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tm_LineToken_BK");

            entity.Property(e => e.BankId).HasColumnName("BankID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.ProjectZoneId).HasColumnName("ProjectZoneID");
            entity.Property(e => e.Token).IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TmMenu>(entity =>
        {
            entity.ToTable("tm_Menu");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ActionName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ControllerName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Icon)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.ParentId).HasColumnName("ParentID");
            entity.Property(e => e.QctypeId).HasColumnName("QCTypeID");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).IsUnicode(false);

            entity.HasOne(d => d.Qctype).WithMany(p => p.TmMenus)
                .HasForeignKey(d => d.QctypeId)
                .HasConstraintName("FK_tm_Menu_tm_Ext");
        });

        modelBuilder.Entity<TmPosition>(entity =>
        {
            entity.ToTable("tm_Position");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(2000);
            entity.Property(e => e.NameEng)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("Name_Eng");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TmProject>(entity =>
        {
            entity.HasKey(e => e.ProjectId);

            entity.ToTable("tm_Project");

            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.InterestRateUrl).HasMaxLength(1000);
            entity.Property(e => e.PartnerId).HasColumnName("PartnerID");
            entity.Property(e => e.ProjectName).HasMaxLength(500);
            entity.Property(e => e.ProjectNameEng)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("ProjectName_Eng");
            entity.Property(e => e.ProjectType)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Partner).WithMany(p => p.TmProjects)
                .HasForeignKey(d => d.PartnerId)
                .HasConstraintName("FK_tm_Project_tm_Ext");
        });

        modelBuilder.Entity<TmProjectUserMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TR_ProjectUser_Mapping");

            entity.ToTable("tm_ProjectUser_Mapping");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.GroupUserId).HasColumnName("GroupUserID");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.GroupUser).WithMany(p => p.TmProjectUserMappings)
                .HasForeignKey(d => d.GroupUserId)
                .HasConstraintName("FK_tm_ProjectUser_Mapping_tm_Ext");

            entity.HasOne(d => d.Project).WithMany(p => p.TmProjectUserMappings)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_tm_ProjectUser_Mapping_tm_Project");

            entity.HasOne(d => d.User).WithMany(p => p.TmProjectUserMappings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_tm_ProjectUser_Mapping_tm_User");
        });

        modelBuilder.Entity<TmPromotion>(entity =>
        {
            entity.ToTable("tm_Promotion");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TmQc5CheckList>(entity =>
        {
            entity.ToTable("tm_QC5_CheckList");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.ParentId).HasColumnName("ParentID");
            entity.Property(e => e.ProjectType)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TmQuestion>(entity =>
        {
            entity.ToTable("tm_Question");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TmResponsibleMapping>(entity =>
        {
            entity.ToTable("tm_Responsible_Mapping");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.QctypeId).HasColumnName("QCTypeID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserIdMapping).HasColumnName("UserID_Mapping");

            entity.HasOne(d => d.Project).WithMany(p => p.TmResponsibleMappings)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_tm_Responsible_Mapping_tm_Project");

            entity.HasOne(d => d.Qctype).WithMany(p => p.TmResponsibleMappings)
                .HasForeignKey(d => d.QctypeId)
                .HasConstraintName("FK_tm_Responsible_Mapping_tm_Ext");

            entity.HasOne(d => d.UserIdMappingNavigation).WithMany(p => p.TmResponsibleMappings)
                .HasForeignKey(d => d.UserIdMapping)
                .HasConstraintName("FK_tm_Responsible_Mapping_tm_User");
        });

        modelBuilder.Entity<TmRole>(entity =>
        {
            entity.ToTable("tm_Role");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.QctypeId).HasColumnName("QCTypeID");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Qctype).WithMany(p => p.TmRoles)
                .HasForeignKey(d => d.QctypeId)
                .HasConstraintName("FK_tm_Role_tm_Ext");
        });

        modelBuilder.Entity<TmShop>(entity =>
        {
            entity.ToTable("tm_Shop");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TmSubject>(entity =>
        {
            entity.ToTable("tm_Subject");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(1000);
            entity.Property(e => e.TopicId).HasColumnName("TopicID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Topic).WithMany(p => p.TmSubjects)
                .HasForeignKey(d => d.TopicId)
                .HasConstraintName("FK_tm_Subject_tm_Topic");
        });

        modelBuilder.Entity<TmTitleName>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tm_Title");

            entity.ToTable("tm_TitleName");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.Lang)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TmTopic>(entity =>
        {
            entity.ToTable("tm_Topic");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(1000);
            entity.Property(e => e.ProjectType)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.QctypeId).HasColumnName("QCTypeID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TmUnit>(entity =>
        {
            entity.ToTable("tm_Unit");

            entity.HasIndex(e => new { e.ProjectId, e.UnitCode, e.Id, e.Build, e.Floor, e.UnitStatus, e.WipMatrixId, e.Qc6WipMatrixId, e.ExpectTransfer, e.TransferDueDateCs, e.UnitStatusCs, e.LetterDueDateCs, e.BankSelectedIdCs, e.ExpectTransferById }, "NonClusteredIndex-20201108-164116");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AddrNo).HasMaxLength(500);
            entity.Property(e => e.AppointDate)
                .HasColumnType("datetime")
                .HasColumnName("Appoint_Date");
            entity.Property(e => e.Area).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BankSelectedIdCs).HasColumnName("BankSelectedID_CS");
            entity.Property(e => e.BookDate).HasColumnType("datetime");
            entity.Property(e => e.Build)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CardExpireDate).HasColumnType("datetime");
            entity.Property(e => e.ContractDate).HasColumnType("datetime");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CustomerEmail)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.CustomerMobile)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CustomerName).HasMaxLength(500);
            entity.Property(e => e.DeviateRemark)
                .HasMaxLength(2000)
                .IsUnicode(false);
            entity.Property(e => e.DeviateStatusId).HasColumnName("DeviateStatusID");
            entity.Property(e => e.ExpectTransfer)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ExpectTransferById).HasColumnName("ExpectTransferByID");
            entity.Property(e => e.ExpectTransferDeviate)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.FreeAll).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.InspectCount).HasColumnName("Inspect_Count");
            entity.Property(e => e.InspectDate)
                .HasColumnType("datetime")
                .HasColumnName("Inspect_Date");
            entity.Property(e => e.InspectId).HasColumnName("Inspect_ID");
            entity.Property(e => e.LetterDueDateCs)
                .HasColumnType("datetime")
                .HasColumnName("LetterDueDate_CS");
            entity.Property(e => e.MeterRemark).HasMaxLength(2000);
            entity.Property(e => e.MeterTypeId).HasColumnName("MeterTypeID");
            entity.Property(e => e.OverDueAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Position)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.Qc1).HasColumnName("QC1");
            entity.Property(e => e.Qc1Date)
                .HasColumnType("datetime")
                .HasColumnName("QC1_Date");
            entity.Property(e => e.Qc1Id).HasColumnName("QC1_ID");
            entity.Property(e => e.Qc1StatusId).HasColumnName("QC1_StatusID");
            entity.Property(e => e.Qc2).HasColumnName("QC2");
            entity.Property(e => e.Qc2Date)
                .HasColumnType("datetime")
                .HasColumnName("QC2_Date");
            entity.Property(e => e.Qc2Id).HasColumnName("QC2_ID");
            entity.Property(e => e.Qc2StatusId).HasColumnName("QC2_StatusID");
            entity.Property(e => e.Qc3).HasColumnName("QC3");
            entity.Property(e => e.Qc3Date)
                .HasColumnType("datetime")
                .HasColumnName("QC3_Date");
            entity.Property(e => e.Qc3Id).HasColumnName("QC3_ID");
            entity.Property(e => e.Qc3StatusId).HasColumnName("QC3_StatusID");
            entity.Property(e => e.Qc4).HasColumnName("QC4");
            entity.Property(e => e.Qc4Date)
                .HasColumnType("datetime")
                .HasColumnName("QC4_Date");
            entity.Property(e => e.Qc4Id).HasColumnName("QC4_ID");
            entity.Property(e => e.Qc4StatusId).HasColumnName("QC4_StatusID");
            entity.Property(e => e.Qc5).HasColumnName("QC5");
            entity.Property(e => e.Qc5Date)
                .HasColumnType("datetime")
                .HasColumnName("QC5_Date");
            entity.Property(e => e.Qc5FinishDate)
                .HasColumnType("datetime")
                .HasColumnName("QC5_FinishDate");
            entity.Property(e => e.Qc5Id).HasColumnName("QC5_ID");
            entity.Property(e => e.Qc5Open).HasColumnName("QC5_Open");
            entity.Property(e => e.Qc5OpenDate)
                .HasColumnType("datetime")
                .HasColumnName("QC5_Open_Date");
            entity.Property(e => e.Qc5OpenId).HasColumnName("QC5_Open_ID");
            entity.Property(e => e.Qc5OpenStatusId).HasColumnName("QC5_Open_StatusID");
            entity.Property(e => e.Qc5StatusId).HasColumnName("QC5_StatusID");
            entity.Property(e => e.Qc6).HasColumnName("QC6");
            entity.Property(e => e.Qc6Date)
                .HasColumnType("datetime")
                .HasColumnName("QC6_Date");
            entity.Property(e => e.Qc6Id).HasColumnName("QC6_ID");
            entity.Property(e => e.Qc6Matrix).HasColumnName("QC6_Matrix");
            entity.Property(e => e.Qc6StatusId).HasColumnName("QC6_StatusID");
            entity.Property(e => e.Qc6WipMatrixId).HasColumnName("QC6_WIP_Matrix_ID");
            entity.Property(e => e.ReceiveRoomAgreementDate).HasColumnType("datetime");
            entity.Property(e => e.ReceiveRoomDate).HasColumnType("datetime");
            entity.Property(e => e.RemarkUnitStatusCs).HasColumnName("RemarkUnitStatus_CS");
            entity.Property(e => e.SaleName).HasMaxLength(500);
            entity.Property(e => e.SellingPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TransferDate).HasColumnType("datetime");
            entity.Property(e => e.TransferDateCs)
                .HasColumnType("datetime")
                .HasColumnName("TransferDate_CS");
            entity.Property(e => e.TransferDueDateCs)
                .HasColumnType("datetime")
                .HasColumnName("TransferDueDate_CS");
            entity.Property(e => e.TransferNetPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UnitCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UnitStatusCs).HasColumnName("UnitStatus_CS");
            entity.Property(e => e.UnitStatusSale).HasColumnName("UnitStatus_Sale");
            entity.Property(e => e.UnitType)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdateDateUnitStatusCs)
                .HasColumnType("datetime")
                .HasColumnName("UpdateDateUnitStatus_CS");
            entity.Property(e => e.WipMatrixId).HasColumnName("WIP_Matrix_ID");

            entity.HasOne(d => d.MeterType).WithMany(p => p.TmUnitMeterTypes)
                .HasForeignKey(d => d.MeterTypeId)
                .HasConstraintName("FK_tm_Unit_tm_Ext");

            entity.HasOne(d => d.Project).WithMany(p => p.TmUnits)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_tm_Unit_tm_Project");

            entity.HasOne(d => d.UnitStatusNavigation).WithMany(p => p.TmUnits)
                .HasForeignKey(d => d.UnitStatus)
                .HasConstraintName("FK_tm_Unit_tm_UnitStatus");

            entity.HasOne(d => d.UnitStatusSaleNavigation).WithMany(p => p.TmUnitUnitStatusSaleNavigations)
                .HasForeignKey(d => d.UnitStatusSale)
                .HasConstraintName("FK_tm_Unit_tm_Ext1");

            entity.HasOne(d => d.WipMatrix).WithMany(p => p.TmUnits)
                .HasForeignKey(d => d.WipMatrixId)
                .HasConstraintName("FK_tm_Unit_tm_WIPMatrix");
        });

        modelBuilder.Entity<TmUnit20190614>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tm_Unit_20190614");

            entity.Property(e => e.AddrNo).HasMaxLength(500);
            entity.Property(e => e.AppointDate)
                .HasColumnType("datetime")
                .HasColumnName("Appoint_Date");
            entity.Property(e => e.Area).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BookDate).HasColumnType("datetime");
            entity.Property(e => e.Build)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ContractDate).HasColumnType("datetime");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerEmail)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.CustomerMobile)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CustomerName).HasMaxLength(500);
            entity.Property(e => e.ExpectTransfer)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ExpectTransferById).HasColumnName("ExpectTransferByID");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.InspectCount).HasColumnName("Inspect_Count");
            entity.Property(e => e.InspectDate)
                .HasColumnType("datetime")
                .HasColumnName("Inspect_Date");
            entity.Property(e => e.InspectId).HasColumnName("Inspect_ID");
            entity.Property(e => e.Position)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.Qc1).HasColumnName("QC1");
            entity.Property(e => e.Qc1Date)
                .HasColumnType("datetime")
                .HasColumnName("QC1_Date");
            entity.Property(e => e.Qc1Id).HasColumnName("QC1_ID");
            entity.Property(e => e.Qc1StatusId).HasColumnName("QC1_StatusID");
            entity.Property(e => e.Qc2).HasColumnName("QC2");
            entity.Property(e => e.Qc2Date)
                .HasColumnType("datetime")
                .HasColumnName("QC2_Date");
            entity.Property(e => e.Qc2Id).HasColumnName("QC2_ID");
            entity.Property(e => e.Qc2StatusId).HasColumnName("QC2_StatusID");
            entity.Property(e => e.Qc3).HasColumnName("QC3");
            entity.Property(e => e.Qc3Date)
                .HasColumnType("datetime")
                .HasColumnName("QC3_Date");
            entity.Property(e => e.Qc3Id).HasColumnName("QC3_ID");
            entity.Property(e => e.Qc3StatusId).HasColumnName("QC3_StatusID");
            entity.Property(e => e.Qc4).HasColumnName("QC4");
            entity.Property(e => e.Qc4Date)
                .HasColumnType("datetime")
                .HasColumnName("QC4_Date");
            entity.Property(e => e.Qc4Id).HasColumnName("QC4_ID");
            entity.Property(e => e.Qc4StatusId).HasColumnName("QC4_StatusID");
            entity.Property(e => e.Qc5).HasColumnName("QC5");
            entity.Property(e => e.Qc5Date)
                .HasColumnType("datetime")
                .HasColumnName("QC5_Date");
            entity.Property(e => e.Qc5FinishDate)
                .HasColumnType("datetime")
                .HasColumnName("QC5_FinishDate");
            entity.Property(e => e.Qc5Id).HasColumnName("QC5_ID");
            entity.Property(e => e.Qc5Open).HasColumnName("QC5_Open");
            entity.Property(e => e.Qc5OpenDate)
                .HasColumnType("datetime")
                .HasColumnName("QC5_Open_Date");
            entity.Property(e => e.Qc5OpenId).HasColumnName("QC5_Open_ID");
            entity.Property(e => e.Qc5OpenStatusId).HasColumnName("QC5_Open_StatusID");
            entity.Property(e => e.Qc5StatusId).HasColumnName("QC5_StatusID");
            entity.Property(e => e.Qc6).HasColumnName("QC6");
            entity.Property(e => e.Qc6Date)
                .HasColumnType("datetime")
                .HasColumnName("QC6_Date");
            entity.Property(e => e.Qc6Id).HasColumnName("QC6_ID");
            entity.Property(e => e.Qc6Matrix).HasColumnName("QC6_Matrix");
            entity.Property(e => e.Qc6StatusId).HasColumnName("QC6_StatusID");
            entity.Property(e => e.Qc6WipMatrixId).HasColumnName("QC6_WIP_Matrix_ID");
            entity.Property(e => e.ReceiveRoomDate).HasColumnType("datetime");
            entity.Property(e => e.SellingPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TransferDate).HasColumnType("datetime");
            entity.Property(e => e.TransferDateCs)
                .HasColumnType("datetime")
                .HasColumnName("TransferDate_CS");
            entity.Property(e => e.TransferDueDateCs)
                .HasColumnType("datetime")
                .HasColumnName("TransferDueDate_CS");
            entity.Property(e => e.UnitCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UnitStatusCs).HasColumnName("UnitStatus_CS");
            entity.Property(e => e.UnitType)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.WipMatrixId).HasColumnName("WIP_Matrix_ID");
        });

        modelBuilder.Entity<TmUnit20210510>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tm_Unit_20210510");

            entity.Property(e => e.AddrNo).HasMaxLength(500);
            entity.Property(e => e.AppointDate)
                .HasColumnType("datetime")
                .HasColumnName("Appoint_Date");
            entity.Property(e => e.Area).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BookDate).HasColumnType("datetime");
            entity.Property(e => e.Build)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ContractDate).HasColumnType("datetime");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerEmail)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.CustomerMobile)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CustomerName).HasMaxLength(500);
            entity.Property(e => e.ExpectTransfer)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ExpectTransferById).HasColumnName("ExpectTransferByID");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.InspectCount).HasColumnName("Inspect_Count");
            entity.Property(e => e.InspectDate)
                .HasColumnType("datetime")
                .HasColumnName("Inspect_Date");
            entity.Property(e => e.InspectId).HasColumnName("Inspect_ID");
            entity.Property(e => e.MeterTypeId).HasColumnName("MeterTypeID");
            entity.Property(e => e.Position)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.Qc1).HasColumnName("QC1");
            entity.Property(e => e.Qc1Date)
                .HasColumnType("datetime")
                .HasColumnName("QC1_Date");
            entity.Property(e => e.Qc1Id).HasColumnName("QC1_ID");
            entity.Property(e => e.Qc1StatusId).HasColumnName("QC1_StatusID");
            entity.Property(e => e.Qc2).HasColumnName("QC2");
            entity.Property(e => e.Qc2Date)
                .HasColumnType("datetime")
                .HasColumnName("QC2_Date");
            entity.Property(e => e.Qc2Id).HasColumnName("QC2_ID");
            entity.Property(e => e.Qc2StatusId).HasColumnName("QC2_StatusID");
            entity.Property(e => e.Qc3).HasColumnName("QC3");
            entity.Property(e => e.Qc3Date)
                .HasColumnType("datetime")
                .HasColumnName("QC3_Date");
            entity.Property(e => e.Qc3Id).HasColumnName("QC3_ID");
            entity.Property(e => e.Qc3StatusId).HasColumnName("QC3_StatusID");
            entity.Property(e => e.Qc4).HasColumnName("QC4");
            entity.Property(e => e.Qc4Date)
                .HasColumnType("datetime")
                .HasColumnName("QC4_Date");
            entity.Property(e => e.Qc4Id).HasColumnName("QC4_ID");
            entity.Property(e => e.Qc4StatusId).HasColumnName("QC4_StatusID");
            entity.Property(e => e.Qc5).HasColumnName("QC5");
            entity.Property(e => e.Qc5Date)
                .HasColumnType("datetime")
                .HasColumnName("QC5_Date");
            entity.Property(e => e.Qc5FinishDate)
                .HasColumnType("datetime")
                .HasColumnName("QC5_FinishDate");
            entity.Property(e => e.Qc5Id).HasColumnName("QC5_ID");
            entity.Property(e => e.Qc5Open).HasColumnName("QC5_Open");
            entity.Property(e => e.Qc5OpenDate)
                .HasColumnType("datetime")
                .HasColumnName("QC5_Open_Date");
            entity.Property(e => e.Qc5OpenId).HasColumnName("QC5_Open_ID");
            entity.Property(e => e.Qc5OpenStatusId).HasColumnName("QC5_Open_StatusID");
            entity.Property(e => e.Qc5StatusId).HasColumnName("QC5_StatusID");
            entity.Property(e => e.Qc6).HasColumnName("QC6");
            entity.Property(e => e.Qc6Date)
                .HasColumnType("datetime")
                .HasColumnName("QC6_Date");
            entity.Property(e => e.Qc6Id).HasColumnName("QC6_ID");
            entity.Property(e => e.Qc6Matrix).HasColumnName("QC6_Matrix");
            entity.Property(e => e.Qc6StatusId).HasColumnName("QC6_StatusID");
            entity.Property(e => e.Qc6WipMatrixId).HasColumnName("QC6_WIP_Matrix_ID");
            entity.Property(e => e.ReceiveRoomAgreementDate).HasColumnType("datetime");
            entity.Property(e => e.ReceiveRoomDate).HasColumnType("datetime");
            entity.Property(e => e.SellingPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TransferDate).HasColumnType("datetime");
            entity.Property(e => e.TransferDateCs)
                .HasColumnType("datetime")
                .HasColumnName("TransferDate_CS");
            entity.Property(e => e.TransferDueDateCs)
                .HasColumnType("datetime")
                .HasColumnName("TransferDueDate_CS");
            entity.Property(e => e.UnitCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UnitStatusCs).HasColumnName("UnitStatus_CS");
            entity.Property(e => e.UnitType)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateDateUnitStatusCs)
                .HasColumnType("datetime")
                .HasColumnName("UpdateDateUnitStatus_CS");
            entity.Property(e => e.WipMatrixId).HasColumnName("WIP_Matrix_ID");
        });

        modelBuilder.Entity<TmUnitBk02102018>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tm_Unit_BK02102018");

            entity.Property(e => e.AddrNo).HasMaxLength(500);
            entity.Property(e => e.AppointDate)
                .HasColumnType("datetime")
                .HasColumnName("Appoint_Date");
            entity.Property(e => e.Area).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BookDate).HasColumnType("datetime");
            entity.Property(e => e.Build)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.ContractDate).HasColumnType("datetime");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerEmail)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.CustomerMobile)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CustomerName).HasMaxLength(500);
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.InspectCount).HasColumnName("Inspect_Count");
            entity.Property(e => e.InspectDate)
                .HasColumnType("datetime")
                .HasColumnName("Inspect_Date");
            entity.Property(e => e.InspectId).HasColumnName("Inspect_ID");
            entity.Property(e => e.Position)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.Qc1).HasColumnName("QC1");
            entity.Property(e => e.Qc1Date)
                .HasColumnType("datetime")
                .HasColumnName("QC1_Date");
            entity.Property(e => e.Qc1Id).HasColumnName("QC1_ID");
            entity.Property(e => e.Qc1StatusId).HasColumnName("QC1_StatusID");
            entity.Property(e => e.Qc2).HasColumnName("QC2");
            entity.Property(e => e.Qc2Date)
                .HasColumnType("datetime")
                .HasColumnName("QC2_Date");
            entity.Property(e => e.Qc2Id).HasColumnName("QC2_ID");
            entity.Property(e => e.Qc2StatusId).HasColumnName("QC2_StatusID");
            entity.Property(e => e.Qc3).HasColumnName("QC3");
            entity.Property(e => e.Qc3Date)
                .HasColumnType("datetime")
                .HasColumnName("QC3_Date");
            entity.Property(e => e.Qc3Id).HasColumnName("QC3_ID");
            entity.Property(e => e.Qc3StatusId).HasColumnName("QC3_StatusID");
            entity.Property(e => e.Qc4).HasColumnName("QC4");
            entity.Property(e => e.Qc4Date)
                .HasColumnType("datetime")
                .HasColumnName("QC4_Date");
            entity.Property(e => e.Qc4Id).HasColumnName("QC4_ID");
            entity.Property(e => e.Qc4StatusId).HasColumnName("QC4_StatusID");
            entity.Property(e => e.Qc5).HasColumnName("QC5");
            entity.Property(e => e.Qc5Date)
                .HasColumnType("datetime")
                .HasColumnName("QC5_Date");
            entity.Property(e => e.Qc5Id).HasColumnName("QC5_ID");
            entity.Property(e => e.Qc5Open).HasColumnName("QC5_Open");
            entity.Property(e => e.Qc5OpenDate)
                .HasColumnType("datetime")
                .HasColumnName("QC5_Open_Date");
            entity.Property(e => e.Qc5OpenId).HasColumnName("QC5_Open_ID");
            entity.Property(e => e.Qc5OpenStatusId).HasColumnName("QC5_Open_StatusID");
            entity.Property(e => e.Qc5StatusId).HasColumnName("QC5_StatusID");
            entity.Property(e => e.Qc6).HasColumnName("QC6");
            entity.Property(e => e.Qc6Date)
                .HasColumnType("datetime")
                .HasColumnName("QC6_Date");
            entity.Property(e => e.Qc6Id).HasColumnName("QC6_ID");
            entity.Property(e => e.Qc6Matrix).HasColumnName("QC6_Matrix");
            entity.Property(e => e.Qc6StatusId).HasColumnName("QC6_StatusID");
            entity.Property(e => e.Qc6WipMatrixId).HasColumnName("QC6_WIP_Matrix_ID");
            entity.Property(e => e.ReceiveRoomDate).HasColumnType("datetime");
            entity.Property(e => e.TransferDate).HasColumnType("datetime");
            entity.Property(e => e.UnitCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UnitType)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.WipMatrixId).HasColumnName("WIP_Matrix_ID");
        });

        modelBuilder.Entity<TmUnitBk11062018>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tm_Unit_BK11062018");

            entity.Property(e => e.AddrNo).HasMaxLength(500);
            entity.Property(e => e.AppointDate)
                .HasColumnType("datetime")
                .HasColumnName("Appoint_Date");
            entity.Property(e => e.Area).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BookDate).HasColumnType("datetime");
            entity.Property(e => e.Build)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.ContractDate).HasColumnType("datetime");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerEmail)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.CustomerMobile)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CustomerName).HasMaxLength(500);
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.InspectCount).HasColumnName("Inspect_Count");
            entity.Property(e => e.InspectDate)
                .HasColumnType("datetime")
                .HasColumnName("Inspect_Date");
            entity.Property(e => e.InspectId).HasColumnName("Inspect_ID");
            entity.Property(e => e.Position)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.Qc1).HasColumnName("QC1");
            entity.Property(e => e.Qc1Date)
                .HasColumnType("datetime")
                .HasColumnName("QC1_Date");
            entity.Property(e => e.Qc1Id).HasColumnName("QC1_ID");
            entity.Property(e => e.Qc1StatusId).HasColumnName("QC1_StatusID");
            entity.Property(e => e.Qc2).HasColumnName("QC2");
            entity.Property(e => e.Qc2Date)
                .HasColumnType("datetime")
                .HasColumnName("QC2_Date");
            entity.Property(e => e.Qc2Id).HasColumnName("QC2_ID");
            entity.Property(e => e.Qc2StatusId).HasColumnName("QC2_StatusID");
            entity.Property(e => e.Qc3).HasColumnName("QC3");
            entity.Property(e => e.Qc3Date)
                .HasColumnType("datetime")
                .HasColumnName("QC3_Date");
            entity.Property(e => e.Qc3Id).HasColumnName("QC3_ID");
            entity.Property(e => e.Qc3StatusId).HasColumnName("QC3_StatusID");
            entity.Property(e => e.Qc4).HasColumnName("QC4");
            entity.Property(e => e.Qc4Date)
                .HasColumnType("datetime")
                .HasColumnName("QC4_Date");
            entity.Property(e => e.Qc4Id).HasColumnName("QC4_ID");
            entity.Property(e => e.Qc4StatusId).HasColumnName("QC4_StatusID");
            entity.Property(e => e.Qc5).HasColumnName("QC5");
            entity.Property(e => e.Qc5Date)
                .HasColumnType("datetime")
                .HasColumnName("QC5_Date");
            entity.Property(e => e.Qc5Id).HasColumnName("QC5_ID");
            entity.Property(e => e.Qc5Open).HasColumnName("QC5_Open");
            entity.Property(e => e.Qc5OpenDate)
                .HasColumnType("datetime")
                .HasColumnName("QC5_Open_Date");
            entity.Property(e => e.Qc5OpenId).HasColumnName("QC5_Open_ID");
            entity.Property(e => e.Qc5OpenStatusId).HasColumnName("QC5_Open_StatusID");
            entity.Property(e => e.Qc5StatusId).HasColumnName("QC5_StatusID");
            entity.Property(e => e.Qc6).HasColumnName("QC6");
            entity.Property(e => e.Qc6Date)
                .HasColumnType("datetime")
                .HasColumnName("QC6_Date");
            entity.Property(e => e.Qc6Id).HasColumnName("QC6_ID");
            entity.Property(e => e.Qc6Matrix).HasColumnName("QC6_Matrix");
            entity.Property(e => e.Qc6StatusId).HasColumnName("QC6_StatusID");
            entity.Property(e => e.ReceiveRoomDate).HasColumnType("datetime");
            entity.Property(e => e.TransferDate).HasColumnType("datetime");
            entity.Property(e => e.UnitCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UnitType)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.WipMatrixId).HasColumnName("WIP_Matrix_ID");
        });

        modelBuilder.Entity<TmUnitBk30062018>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tm_Unit_BK30062018");

            entity.Property(e => e.AddrNo).HasMaxLength(500);
            entity.Property(e => e.AppointDate)
                .HasColumnType("datetime")
                .HasColumnName("Appoint_Date");
            entity.Property(e => e.Area).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BookDate).HasColumnType("datetime");
            entity.Property(e => e.Build)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.ContractDate).HasColumnType("datetime");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerEmail)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.CustomerMobile)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CustomerName).HasMaxLength(500);
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.InspectCount).HasColumnName("Inspect_Count");
            entity.Property(e => e.InspectDate)
                .HasColumnType("datetime")
                .HasColumnName("Inspect_Date");
            entity.Property(e => e.InspectId).HasColumnName("Inspect_ID");
            entity.Property(e => e.Position)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.Qc1).HasColumnName("QC1");
            entity.Property(e => e.Qc1Date)
                .HasColumnType("datetime")
                .HasColumnName("QC1_Date");
            entity.Property(e => e.Qc1Id).HasColumnName("QC1_ID");
            entity.Property(e => e.Qc1StatusId).HasColumnName("QC1_StatusID");
            entity.Property(e => e.Qc2).HasColumnName("QC2");
            entity.Property(e => e.Qc2Date)
                .HasColumnType("datetime")
                .HasColumnName("QC2_Date");
            entity.Property(e => e.Qc2Id).HasColumnName("QC2_ID");
            entity.Property(e => e.Qc2StatusId).HasColumnName("QC2_StatusID");
            entity.Property(e => e.Qc3).HasColumnName("QC3");
            entity.Property(e => e.Qc3Date)
                .HasColumnType("datetime")
                .HasColumnName("QC3_Date");
            entity.Property(e => e.Qc3Id).HasColumnName("QC3_ID");
            entity.Property(e => e.Qc3StatusId).HasColumnName("QC3_StatusID");
            entity.Property(e => e.Qc4).HasColumnName("QC4");
            entity.Property(e => e.Qc4Date)
                .HasColumnType("datetime")
                .HasColumnName("QC4_Date");
            entity.Property(e => e.Qc4Id).HasColumnName("QC4_ID");
            entity.Property(e => e.Qc4StatusId).HasColumnName("QC4_StatusID");
            entity.Property(e => e.Qc5).HasColumnName("QC5");
            entity.Property(e => e.Qc5Date)
                .HasColumnType("datetime")
                .HasColumnName("QC5_Date");
            entity.Property(e => e.Qc5Id).HasColumnName("QC5_ID");
            entity.Property(e => e.Qc5Open).HasColumnName("QC5_Open");
            entity.Property(e => e.Qc5OpenDate)
                .HasColumnType("datetime")
                .HasColumnName("QC5_Open_Date");
            entity.Property(e => e.Qc5OpenId).HasColumnName("QC5_Open_ID");
            entity.Property(e => e.Qc5OpenStatusId).HasColumnName("QC5_Open_StatusID");
            entity.Property(e => e.Qc5StatusId).HasColumnName("QC5_StatusID");
            entity.Property(e => e.Qc6).HasColumnName("QC6");
            entity.Property(e => e.Qc6Date)
                .HasColumnType("datetime")
                .HasColumnName("QC6_Date");
            entity.Property(e => e.Qc6Id).HasColumnName("QC6_ID");
            entity.Property(e => e.Qc6Matrix).HasColumnName("QC6_Matrix");
            entity.Property(e => e.Qc6StatusId).HasColumnName("QC6_StatusID");
            entity.Property(e => e.Qc6WipMatrixId).HasColumnName("QC6_WIP_Matrix_ID");
            entity.Property(e => e.ReceiveRoomDate).HasColumnType("datetime");
            entity.Property(e => e.TransferDate).HasColumnType("datetime");
            entity.Property(e => e.UnitCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UnitType)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.WipMatrixId).HasColumnName("WIP_Matrix_ID");
        });

        modelBuilder.Entity<TmUnitBk31082018>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tm_Unit_BK31082018");

            entity.Property(e => e.AddrNo).HasMaxLength(500);
            entity.Property(e => e.AppointDate)
                .HasColumnType("datetime")
                .HasColumnName("Appoint_Date");
            entity.Property(e => e.Area).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BookDate).HasColumnType("datetime");
            entity.Property(e => e.Build)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.ContractDate).HasColumnType("datetime");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerEmail)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.CustomerMobile)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CustomerName).HasMaxLength(500);
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.InspectCount).HasColumnName("Inspect_Count");
            entity.Property(e => e.InspectDate)
                .HasColumnType("datetime")
                .HasColumnName("Inspect_Date");
            entity.Property(e => e.InspectId).HasColumnName("Inspect_ID");
            entity.Property(e => e.Position)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.Qc1).HasColumnName("QC1");
            entity.Property(e => e.Qc1Date)
                .HasColumnType("datetime")
                .HasColumnName("QC1_Date");
            entity.Property(e => e.Qc1Id).HasColumnName("QC1_ID");
            entity.Property(e => e.Qc1StatusId).HasColumnName("QC1_StatusID");
            entity.Property(e => e.Qc2).HasColumnName("QC2");
            entity.Property(e => e.Qc2Date)
                .HasColumnType("datetime")
                .HasColumnName("QC2_Date");
            entity.Property(e => e.Qc2Id).HasColumnName("QC2_ID");
            entity.Property(e => e.Qc2StatusId).HasColumnName("QC2_StatusID");
            entity.Property(e => e.Qc3).HasColumnName("QC3");
            entity.Property(e => e.Qc3Date)
                .HasColumnType("datetime")
                .HasColumnName("QC3_Date");
            entity.Property(e => e.Qc3Id).HasColumnName("QC3_ID");
            entity.Property(e => e.Qc3StatusId).HasColumnName("QC3_StatusID");
            entity.Property(e => e.Qc4).HasColumnName("QC4");
            entity.Property(e => e.Qc4Date)
                .HasColumnType("datetime")
                .HasColumnName("QC4_Date");
            entity.Property(e => e.Qc4Id).HasColumnName("QC4_ID");
            entity.Property(e => e.Qc4StatusId).HasColumnName("QC4_StatusID");
            entity.Property(e => e.Qc5).HasColumnName("QC5");
            entity.Property(e => e.Qc5Date)
                .HasColumnType("datetime")
                .HasColumnName("QC5_Date");
            entity.Property(e => e.Qc5Id).HasColumnName("QC5_ID");
            entity.Property(e => e.Qc5Open).HasColumnName("QC5_Open");
            entity.Property(e => e.Qc5OpenDate)
                .HasColumnType("datetime")
                .HasColumnName("QC5_Open_Date");
            entity.Property(e => e.Qc5OpenId).HasColumnName("QC5_Open_ID");
            entity.Property(e => e.Qc5OpenStatusId).HasColumnName("QC5_Open_StatusID");
            entity.Property(e => e.Qc5StatusId).HasColumnName("QC5_StatusID");
            entity.Property(e => e.Qc6).HasColumnName("QC6");
            entity.Property(e => e.Qc6Date)
                .HasColumnType("datetime")
                .HasColumnName("QC6_Date");
            entity.Property(e => e.Qc6Id).HasColumnName("QC6_ID");
            entity.Property(e => e.Qc6Matrix).HasColumnName("QC6_Matrix");
            entity.Property(e => e.Qc6StatusId).HasColumnName("QC6_StatusID");
            entity.Property(e => e.Qc6WipMatrixId).HasColumnName("QC6_WIP_Matrix_ID");
            entity.Property(e => e.ReceiveRoomDate).HasColumnType("datetime");
            entity.Property(e => e.TransferDate).HasColumnType("datetime");
            entity.Property(e => e.UnitCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UnitType)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.WipMatrixId).HasColumnName("WIP_Matrix_ID");
        });

        modelBuilder.Entity<TmUnitCam00320191223>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tm_Unit_CAM003_20191223");

            entity.Property(e => e.AddrNo).HasMaxLength(500);
            entity.Property(e => e.AppointDate)
                .HasColumnType("datetime")
                .HasColumnName("Appoint_Date");
            entity.Property(e => e.Area).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BookDate).HasColumnType("datetime");
            entity.Property(e => e.Build)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ContractDate).HasColumnType("datetime");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerEmail)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.CustomerMobile)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CustomerName).HasMaxLength(500);
            entity.Property(e => e.ExpectTransfer)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ExpectTransferById).HasColumnName("ExpectTransferByID");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.InspectCount).HasColumnName("Inspect_Count");
            entity.Property(e => e.InspectDate)
                .HasColumnType("datetime")
                .HasColumnName("Inspect_Date");
            entity.Property(e => e.InspectId).HasColumnName("Inspect_ID");
            entity.Property(e => e.MeterTypeId).HasColumnName("MeterTypeID");
            entity.Property(e => e.Position)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.Qc1).HasColumnName("QC1");
            entity.Property(e => e.Qc1Date)
                .HasColumnType("datetime")
                .HasColumnName("QC1_Date");
            entity.Property(e => e.Qc1Id).HasColumnName("QC1_ID");
            entity.Property(e => e.Qc1StatusId).HasColumnName("QC1_StatusID");
            entity.Property(e => e.Qc2).HasColumnName("QC2");
            entity.Property(e => e.Qc2Date)
                .HasColumnType("datetime")
                .HasColumnName("QC2_Date");
            entity.Property(e => e.Qc2Id).HasColumnName("QC2_ID");
            entity.Property(e => e.Qc2StatusId).HasColumnName("QC2_StatusID");
            entity.Property(e => e.Qc3).HasColumnName("QC3");
            entity.Property(e => e.Qc3Date)
                .HasColumnType("datetime")
                .HasColumnName("QC3_Date");
            entity.Property(e => e.Qc3Id).HasColumnName("QC3_ID");
            entity.Property(e => e.Qc3StatusId).HasColumnName("QC3_StatusID");
            entity.Property(e => e.Qc4).HasColumnName("QC4");
            entity.Property(e => e.Qc4Date)
                .HasColumnType("datetime")
                .HasColumnName("QC4_Date");
            entity.Property(e => e.Qc4Id).HasColumnName("QC4_ID");
            entity.Property(e => e.Qc4StatusId).HasColumnName("QC4_StatusID");
            entity.Property(e => e.Qc5).HasColumnName("QC5");
            entity.Property(e => e.Qc5Date)
                .HasColumnType("datetime")
                .HasColumnName("QC5_Date");
            entity.Property(e => e.Qc5FinishDate)
                .HasColumnType("datetime")
                .HasColumnName("QC5_FinishDate");
            entity.Property(e => e.Qc5Id).HasColumnName("QC5_ID");
            entity.Property(e => e.Qc5Open).HasColumnName("QC5_Open");
            entity.Property(e => e.Qc5OpenDate)
                .HasColumnType("datetime")
                .HasColumnName("QC5_Open_Date");
            entity.Property(e => e.Qc5OpenId).HasColumnName("QC5_Open_ID");
            entity.Property(e => e.Qc5OpenStatusId).HasColumnName("QC5_Open_StatusID");
            entity.Property(e => e.Qc5StatusId).HasColumnName("QC5_StatusID");
            entity.Property(e => e.Qc6).HasColumnName("QC6");
            entity.Property(e => e.Qc6Date)
                .HasColumnType("datetime")
                .HasColumnName("QC6_Date");
            entity.Property(e => e.Qc6Id).HasColumnName("QC6_ID");
            entity.Property(e => e.Qc6Matrix).HasColumnName("QC6_Matrix");
            entity.Property(e => e.Qc6StatusId).HasColumnName("QC6_StatusID");
            entity.Property(e => e.Qc6WipMatrixId).HasColumnName("QC6_WIP_Matrix_ID");
            entity.Property(e => e.ReceiveRoomDate).HasColumnType("datetime");
            entity.Property(e => e.SellingPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TransferDate).HasColumnType("datetime");
            entity.Property(e => e.TransferDateCs)
                .HasColumnType("datetime")
                .HasColumnName("TransferDate_CS");
            entity.Property(e => e.TransferDueDateCs)
                .HasColumnType("datetime")
                .HasColumnName("TransferDueDate_CS");
            entity.Property(e => e.UnitCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UnitStatusCs).HasColumnName("UnitStatus_CS");
            entity.Property(e => e.UnitType)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateDateUnitStatusCs)
                .HasColumnType("datetime")
                .HasColumnName("UpdateDateUnitStatus_CS");
            entity.Property(e => e.WipMatrixId).HasColumnName("WIP_Matrix_ID");
        });

        modelBuilder.Entity<TmUnitStatus>(entity =>
        {
            entity.ToTable("tm_UnitStatus");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<TmUser>(entity =>
        {
            entity.ToTable("tm_User");

            entity.HasIndex(e => new { e.UserId, e.FirstName }, "NonClusteredIndex-20190528-085408").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FirstName).HasMaxLength(200);
            entity.Property(e => e.FirstNameEng)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("FirstName_Eng");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.IsQcfinishPlan).HasColumnName("IsQCFinishPlan");
            entity.Property(e => e.LastName).HasMaxLength(200);
            entity.Property(e => e.LastNameEng)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("LastName_Eng");
            entity.Property(e => e.Mobile).HasMaxLength(100);
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.QctypeId).HasColumnName("QCTypeID");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.TitleId).HasColumnName("TitleID");
            entity.Property(e => e.TitleIdEng).HasColumnName("TitleID_Eng");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserID");

            entity.HasOne(d => d.Qctype).WithMany(p => p.TmUsers)
                .HasForeignKey(d => d.QctypeId)
                .HasConstraintName("FK_tm_User_tm_Ext");

            entity.HasOne(d => d.Role).WithMany(p => p.TmUsers)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_tm_User_tm_Role");

            entity.HasOne(d => d.Title).WithMany(p => p.TmUserTitles)
                .HasForeignKey(d => d.TitleId)
                .HasConstraintName("FK_tm_User_tm_TitleName");

            entity.HasOne(d => d.TitleIdEngNavigation).WithMany(p => p.TmUserTitleIdEngNavigations)
                .HasForeignKey(d => d.TitleIdEng)
                .HasConstraintName("FK_tm_User_tm_TitleName1");
        });

        modelBuilder.Entity<TmVendor>(entity =>
        {
            entity.ToTable("tm_Vendor");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(500);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.VendorTypeId).HasColumnName("VendorTypeID");
        });

        modelBuilder.Entity<TmWipmatrix>(entity =>
        {
            entity.ToTable("tm_WIPMatrix");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TmWipmatrixQc6>(entity =>
        {
            entity.ToTable("tm_WIPMatrix_QC6");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OtherValue)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TmpBlkUnit>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TmpBlkUnit");

            entity.Property(e => e.Agrdate)
                .HasColumnType("datetime")
                .HasColumnName("AGRDATE");
            entity.Property(e => e.Bookdate)
                .HasColumnType("datetime")
                .HasColumnName("BOOKDATE");
            entity.Property(e => e.Fadd1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FADD1");
            entity.Property(e => e.Fadd2)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FADD2");
            entity.Property(e => e.Fadd3)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FADD3");
            entity.Property(e => e.Faddrno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FADDRNO");
            entity.Property(e => e.Fcucode)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FCUCODE");
            entity.Property(e => e.Fcuname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FCUNAME");
            entity.Property(e => e.Fpostal)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("FPOSTAL");
            entity.Property(e => e.Fprovince)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FPROVINCE");
            entity.Property(e => e.Freprjnm)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FREPRJNM");
            entity.Property(e => e.Freprjno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FREPRJNO");
            entity.Property(e => e.Frestatus).HasColumnName("FRESTATUS");
            entity.Property(e => e.Fserialno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FSERIALNO");
            entity.Property(e => e.Ftel)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FTEL");
            entity.Property(e => e.Ftelno)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FTELNO");
            entity.Property(e => e.Transdate)
                .HasColumnType("datetime")
                .HasColumnName("TRANSDATE");
        });

        modelBuilder.Entity<TrAnswerC>(entity =>
        {
            entity.ToTable("TR_AnswerCS");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Question).WithMany(p => p.TrAnswerCs)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK_TR_AnswerCS_TR_QuestionCS");
        });

        modelBuilder.Entity<TrApiLog>(entity =>
        {
            entity.ToTable("TR_API_Log");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.ErrorMessage).HasColumnType("text");
            entity.Property(e => e.Inbound).HasColumnType("text");
            entity.Property(e => e.Module).HasColumnType("text");
            entity.Property(e => e.OutBound).HasColumnType("text");
        });

        modelBuilder.Entity<TrAppointment>(entity =>
        {
            entity.ToTable("TR_Appointment");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AppointDate).HasColumnType("datetime");
            entity.Property(e => e.AppointmentTypeId).HasColumnName("AppointmentTypeID");
            entity.Property(e => e.ClearDate).HasColumnType("datetime");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EndTime)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.StartTime)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.TrAppointments)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_Appointment_tm_Project");

            entity.HasOne(d => d.Unit).WithMany(p => p.TrAppointments)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_TR_Appointment_tm_Unit");
        });

        modelBuilder.Entity<TrAttachFileQc>(entity =>
        {
            entity.ToTable("TR_AttachFileQC");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FileName).HasMaxLength(500);
            entity.Property(e => e.FilePath).HasMaxLength(500);
            entity.Property(e => e.MimeType)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.QcId).HasColumnName("QC_ID");
            entity.Property(e => e.QctypeId).HasColumnName("QCTypeID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.TrAttachFileQcs)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_AttachFileQC_tm_Project");

            entity.HasOne(d => d.Qctype).WithMany(p => p.TrAttachFileQcs)
                .HasForeignKey(d => d.QctypeId)
                .HasConstraintName("FK_TR_AttachFileQC_tm_Ext");

            entity.HasOne(d => d.Unit).WithMany(p => p.TrAttachFileQcs)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_TR_AttachFileQC_tm_Unit");
        });

        modelBuilder.Entity<TrBankTarget>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tr_Bank_Target");

            entity.ToTable("TR_Bank_Target");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BankId).HasColumnName("BankID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Bank).WithMany(p => p.TrBankTargets)
                .HasForeignKey(d => d.BankId)
                .HasConstraintName("FK_tr_Bank_Target_tm_Bank");
        });

        modelBuilder.Entity<TrCompanyProject>(entity =>
        {
            entity.ToTable("TR_CompanyProject");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");

            entity.HasOne(d => d.Company).WithMany(p => p.TrCompanyProjects)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK_TR_CompanyProject_tm_Company");

            entity.HasOne(d => d.Project).WithMany(p => p.TrCompanyProjects)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_CompanyProject_tm_Project");
        });

        modelBuilder.Entity<TrContactLog>(entity =>
        {
            entity.ToTable("TR_ContactLog");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.BankId).HasColumnName("BankID");
            entity.Property(e => e.ContactDate).HasColumnType("datetime");
            entity.Property(e => e.ContactReasonId).HasColumnName("ContactReasonID");
            entity.Property(e => e.ContactTime)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CustomerTypeId).HasColumnName("CustomerTypeID");
            entity.Property(e => e.PaymentDueDate).HasColumnType("datetime");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.QcId).HasColumnName("QC_ID");
            entity.Property(e => e.QctypeId).HasColumnName("QCTypeID");
            entity.Property(e => e.TeamId).HasColumnName("TeamID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Bank).WithMany(p => p.TrContactLogs)
                .HasForeignKey(d => d.BankId)
                .HasConstraintName("FK_TR_ContactLog_tm_Bank");

            entity.HasOne(d => d.Project).WithMany(p => p.TrContactLogs)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_ContactLog_tm_Project");

            entity.HasOne(d => d.Qc).WithMany(p => p.TrContactLogs)
                .HasForeignKey(d => d.QcId)
                .HasConstraintName("FK_TR_ContactLog_TR_QC6");

            entity.HasOne(d => d.Qctype).WithMany(p => p.TrContactLogQctypes)
                .HasForeignKey(d => d.QctypeId)
                .HasConstraintName("FK_TR_ContactLog_tm_Ext1");

            entity.HasOne(d => d.Team).WithMany(p => p.TrContactLogTeams)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK_TR_ContactLog_tm_Ext");

            entity.HasOne(d => d.Unit).WithMany(p => p.TrContactLogs)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_TR_ContactLog_tm_Unit");
        });

        modelBuilder.Entity<TrCustomerSatisfaction>(entity =>
        {
            entity.ToTable("TR_CustomerSatisfaction");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ClientIp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ClientIP");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.QuestionDate).HasColumnType("datetime");
            entity.Property(e => e.QuestionTypeId).HasColumnName("QuestionTypeID");
            entity.Property(e => e.Remark).HasColumnType("text");
            entity.Property(e => e.UnitCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Project).WithMany(p => p.TrCustomerSatisfactions)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_CustomerSatisfaction_tm_Project");

            entity.HasOne(d => d.QuestionType).WithMany(p => p.TrCustomerSatisfactions)
                .HasForeignKey(d => d.QuestionTypeId)
                .HasConstraintName("FK_TR_CustomerSatisfaction_tm_Ext");

            entity.HasOne(d => d.User).WithMany(p => p.TrCustomerSatisfactions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_TR_CustomerSatisfaction_tm_User");
        });

        modelBuilder.Entity<TrCustomerSatisfactionDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TR_CustomerSatisfactionDetail");

            entity.ToTable("TR_CustomerSatisfaction_Detail");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AnswerId).HasColumnName("AnswerID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CustomerSatisfactionId).HasColumnName("CustomerSatisfactionID");
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Answer).WithMany(p => p.TrCustomerSatisfactionDetails)
                .HasForeignKey(d => d.AnswerId)
                .HasConstraintName("FK_TR_CustomerSatisfaction_Detail_TR_AnswerCS");

            entity.HasOne(d => d.CustomerSatisfaction).WithMany(p => p.TrCustomerSatisfactionDetails)
                .HasForeignKey(d => d.CustomerSatisfactionId)
                .HasConstraintName("FK_TR_CustomerSatisfaction_Detail_TR_CustomerSatisfaction");

            entity.HasOne(d => d.Question).WithMany(p => p.TrCustomerSatisfactionDetails)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK_TR_CustomerSatisfaction_Detail_TR_QuestionCS");
        });

        modelBuilder.Entity<TrDefectHistory>(entity =>
        {
            entity.ToTable("TR_DefectHistory");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DefectId).HasColumnName("DefectID");
            entity.Property(e => e.DefectStatusId).HasColumnName("DefectStatusID");
        });

        modelBuilder.Entity<TrDefectHistory20240518>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TR_DefectHistory_20240518");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DefectId).HasColumnName("DefectID");
            entity.Property(e => e.DefectStatusId).HasColumnName("DefectStatusID");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
        });

        modelBuilder.Entity<TrDefectTypeVendorMapping>(entity =>
        {
            entity.ToTable("TR_DefectTypeVendor_Mapping");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DefectTypeId).HasColumnName("DefectTypeID");
            entity.Property(e => e.VendorId).HasColumnName("VendorID");

            entity.HasOne(d => d.DefectType).WithMany(p => p.TrDefectTypeVendorMappings)
                .HasForeignKey(d => d.DefectTypeId)
                .HasConstraintName("FK_TR_DefectTypeVendor_Mapping_tm_DefectType");

            entity.HasOne(d => d.Vendor).WithMany(p => p.TrDefectTypeVendorMappings)
                .HasForeignKey(d => d.VendorId)
                .HasConstraintName("FK_TR_DefectTypeVendor_Mapping_tm_Vendor");
        });

        modelBuilder.Entity<TrDefectVendor>(entity =>
        {
            entity.HasKey(e => e.DefectId);

            entity.ToTable("TR_DefectVendor");

            entity.HasIndex(e => e.DefectId, "NonClusteredIndex-20230725-112141");

            entity.Property(e => e.DefectId)
                .ValueGeneratedNever()
                .HasColumnName("DefectID");
            entity.Property(e => e.CloseDate).HasColumnType("datetime");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.ExpectDate).HasColumnType("datetime");
            entity.Property(e => e.FinishDate).HasColumnType("datetime");
            entity.Property(e => e.InprocessDate).HasColumnType("datetime");
            entity.Property(e => e.RequestQc6date)
                .HasColumnType("datetime")
                .HasColumnName("RequestQC6Date");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.VendorId).HasColumnName("VendorID");
            entity.Property(e => e.WaitDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TrDeviceSignIn>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tr_DeviceSignIn");

            entity.ToTable("TR_DeviceSignIn");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DeviceId)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("DeviceID");
            entity.Property(e => e.SignInDate).HasColumnType("datetime");
            entity.Property(e => e.SignOutDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.TrDeviceSignIns)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_TR_DeviceSignIn_tm_User");
        });

        modelBuilder.Entity<TrLetter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TR_Letter_Detail");

            entity.ToTable("TR_Letter");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ApproveBy).HasMaxLength(1000);
            entity.Property(e => e.ApproveById).HasColumnName("ApproveByID");
            entity.Property(e => e.ApproveDate).HasColumnType("datetime");
            entity.Property(e => e.ApprovePosition).HasMaxLength(200);
            entity.Property(e => e.ApproveSignId).HasColumnName("ApproveSignID");
            entity.Property(e => e.ApproveStatusId).HasColumnName("ApproveStatusID");
            entity.Property(e => e.ContractNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerEmail)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CustomerName).HasMaxLength(1000);
            entity.Property(e => e.LetterReferenceId).HasColumnName("LetterReferenceID");
            entity.Property(e => e.LetterTypeId).HasColumnName("LetterTypeID");
            entity.Property(e => e.PrintBy).HasMaxLength(1000);
            entity.Property(e => e.PrintById).HasColumnName("PrintByID");
            entity.Property(e => e.PrintDate).HasColumnType("datetime");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.SendDueDate).HasColumnType("datetime");
            entity.Property(e => e.SendLang)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SendReasonId).HasColumnName("SendReasonID");
            entity.Property(e => e.SendStatusId).HasColumnName("SendStatusID");
            entity.Property(e => e.SendTypeId).HasColumnName("SendTypeID");
            entity.Property(e => e.SendUpdateBy).HasMaxLength(1000);
            entity.Property(e => e.SendUpdateById).HasColumnName("SendUpdateByID");
            entity.Property(e => e.SendUpdateDate).HasColumnType("datetime");
            entity.Property(e => e.TransferDueDate).HasColumnType("datetime");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.VerifyBy).HasMaxLength(1000);
            entity.Property(e => e.VerifyById).HasColumnName("VerifyByID");
            entity.Property(e => e.VerifyDate).HasColumnType("datetime");
            entity.Property(e => e.VerifyStatusId).HasColumnName("VerifyStatusID");

            entity.HasOne(d => d.ApproveSign).WithMany(p => p.TrLetters)
                .HasForeignKey(d => d.ApproveSignId)
                .HasConstraintName("ApproveSign");

            entity.HasOne(d => d.ApproveStatus).WithMany(p => p.TrLetterApproveStatuses)
                .HasForeignKey(d => d.ApproveStatusId)
                .HasConstraintName("ApproveStatus");

            entity.HasOne(d => d.LetterReference).WithMany(p => p.InverseLetterReference)
                .HasForeignKey(d => d.LetterReferenceId)
                .HasConstraintName("LetterReference");

            entity.HasOne(d => d.LetterType).WithMany(p => p.TrLetterLetterTypes)
                .HasForeignKey(d => d.LetterTypeId)
                .HasConstraintName("LetterType");

            entity.HasOne(d => d.Project).WithMany(p => p.TrLetters)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("Project");

            entity.HasOne(d => d.SendReason).WithMany(p => p.TrLetters)
                .HasForeignKey(d => d.SendReasonId)
                .HasConstraintName("SendReason");

            entity.HasOne(d => d.SendStatus).WithMany(p => p.TrLetterSendStatuses)
                .HasForeignKey(d => d.SendStatusId)
                .HasConstraintName("SendStatus");

            entity.HasOne(d => d.SendType).WithMany(p => p.TrLetterSendTypes)
                .HasForeignKey(d => d.SendTypeId)
                .HasConstraintName("SendType");

            entity.HasOne(d => d.Unit).WithMany(p => p.TrLetters)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("Unit");

            entity.HasOne(d => d.VerifyStatus).WithMany(p => p.TrLetterVerifyStatuses)
                .HasForeignKey(d => d.VerifyStatusId)
                .HasConstraintName("VerifyStatus");
        });

        modelBuilder.Entity<TrLetterAttach>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TR_Letter_Detail_Attach");

            entity.ToTable("TR_Letter_Attach");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FileName).HasMaxLength(500);
            entity.Property(e => e.FilePath).HasMaxLength(500);
            entity.Property(e => e.LetterId).HasColumnName("LetterID");
            entity.Property(e => e.MimeType)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Letter).WithMany(p => p.TrLetterAttaches)
                .HasForeignKey(d => d.LetterId)
                .HasConstraintName("FK_TR_Letter_Attach_TR_Letter");
        });

        modelBuilder.Entity<TrLetterC>(entity =>
        {
            entity.ToTable("TR_Letter_CS");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CentralValue).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CodeVerify)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ElectricMeter).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.EventEndDate).HasColumnType("datetime");
            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.EventLocation).HasMaxLength(500);
            entity.Property(e => e.EventStartDate).HasColumnType("datetime");
            entity.Property(e => e.ExpectLastDate).HasColumnType("datetime");
            entity.Property(e => e.ExpectTransfer)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.FinplusEndDate)
                .HasColumnType("datetime")
                .HasColumnName("FINPlusEndDate");
            entity.Property(e => e.FinplusStartDate)
                .HasColumnType("datetime")
                .HasColumnName("FINPlusStartDate");
            entity.Property(e => e.InspectLastDate).HasColumnType("datetime");
            entity.Property(e => e.PercentProgress).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Promotion).HasMaxLength(3000);
            entity.Property(e => e.SignUserId).HasColumnName("SignUserID");
            entity.Property(e => e.SignUserPosition).HasMaxLength(200);
            entity.Property(e => e.WaterMaintainCost).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Event).WithMany(p => p.TrLetterCs)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK_TR_Letter_CS_tm_Event");

            entity.HasOne(d => d.SignUser).WithMany(p => p.TrLetterCs)
                .HasForeignKey(d => d.SignUserId)
                .HasConstraintName("FK_TR_Letter_CS_tm_User");
        });

        modelBuilder.Entity<TrLetterLot>(entity =>
        {
            entity.ToTable("TR_Letter_Lot");

            entity.HasIndex(e => e.LotNo, "NonClusteredIndex-20220420-133306").IsUnique();

            entity.HasIndex(e => e.LotNo, "NonClusteredIndex-20220505-085742").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.LotNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TrLetterLotDetail>(entity =>
        {
            entity.ToTable("TR_Letter_Lot_Detail");

            entity.HasIndex(e => e.LetterNo, "NonClusteredIndex-20220420-133358").IsUnique();

            entity.HasIndex(e => e.LetterNo, "NonClusteredIndex-20220505-085754").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.LetterId).HasColumnName("LetterID");
            entity.Property(e => e.LetterNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LotId).HasColumnName("LotID");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Letter).WithMany(p => p.TrLetterLotDetails)
                .HasForeignKey(d => d.LetterId)
                .HasConstraintName("FK_TR_Letter_Lot_Detail_TR_Letter");

            entity.HasOne(d => d.Lot).WithMany(p => p.TrLetterLotDetails)
                .HasForeignKey(d => d.LotId)
                .HasConstraintName("FK_TR_Letter_Lot_Detail_TR_Letter_Lot");
        });

        modelBuilder.Entity<TrLetterLotResource>(entity =>
        {
            entity.ToTable("TR_Letter_Lot_Resource");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FileName).HasMaxLength(500);
            entity.Property(e => e.FilePath).HasMaxLength(500);
            entity.Property(e => e.LotId).HasColumnName("LotID");
            entity.Property(e => e.MimeType)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Lot).WithMany(p => p.TrLetterLotResources)
                .HasForeignKey(d => d.LotId)
                .HasConstraintName("FK_TR_Letter_Lot_Resource_TR_Letter_Lot_Resource");
        });

        modelBuilder.Entity<TrMenuRolePermission>(entity =>
        {
            entity.ToTable("TR_MenuRolePermission");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.MenuId).HasColumnName("MenuID");
            entity.Property(e => e.QctypeId).HasColumnName("QCTypeID");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Department).WithMany(p => p.TrMenuRolePermissionDepartments)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_TR_MenuRolePermission_tm_Ext1");

            entity.HasOne(d => d.Menu).WithMany(p => p.TrMenuRolePermissions)
                .HasForeignKey(d => d.MenuId)
                .HasConstraintName("FK_TR_MenuRolePermission_tm_Menu");

            entity.HasOne(d => d.Qctype).WithMany(p => p.TrMenuRolePermissionQctypes)
                .HasForeignKey(d => d.QctypeId)
                .HasConstraintName("FK_TR_MenuRolePermission_tm_Ext");

            entity.HasOne(d => d.Role).WithMany(p => p.TrMenuRolePermissions)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_TR_MenuRolePermission_tm_Role");
        });

        modelBuilder.Entity<TrPowerOfAttorney>(entity =>
        {
            entity.ToTable("TR_PowerOfAttorney");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.IdcardResourceId).HasColumnName("IDCardResourceID");
            entity.Property(e => e.IdcardResourceId2).HasColumnName("IDCardResourceID_2");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.SignDate).HasColumnType("datetime");
            entity.Property(e => e.SignResourceId).HasColumnName("SignResourceID");
            entity.Property(e => e.SignResourceId2).HasColumnName("SignResourceID_2");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
        });

        modelBuilder.Entity<TrProjectAppointLimitMapping>(entity =>
        {
            entity.ToTable("TR_ProjectAppointLimit_Mapping");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DayId).HasColumnName("DayID");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.TimeId).HasColumnName("TimeID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Day).WithMany(p => p.TrProjectAppointLimitMappingDays)
                .HasForeignKey(d => d.DayId)
                .HasConstraintName("FK_TR_ProjectAppointLimit_Mapping_tm_Ext");

            entity.HasOne(d => d.Project).WithMany(p => p.TrProjectAppointLimitMappings)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_ProjectAppointLimit_Mapping_tm_Project");

            entity.HasOne(d => d.Time).WithMany(p => p.TrProjectAppointLimitMappingTimes)
                .HasForeignKey(d => d.TimeId)
                .HasConstraintName("FK_TR_ProjectAppointLimit_Mapping_tm_Ext1");
        });

        modelBuilder.Entity<TrProjectCounterMapping>(entity =>
        {
            entity.ToTable("TR_ProjectCounter_Mapping");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.QueueTypeId).HasColumnName("QueueTypeID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.TrProjectCounterMappings)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_ProjectCounter_Mapping_TR_ProjectCounter_Mapping");

            entity.HasOne(d => d.QueueType).WithMany(p => p.TrProjectCounterMappings)
                .HasForeignKey(d => d.QueueTypeId)
                .HasConstraintName("FK_TR_ProjectCounter_Mapping_tm_Ext");
        });

        modelBuilder.Entity<TrProjectEmailMapping>(entity =>
        {
            entity.ToTable("TR_ProjectEmail_Mapping");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.TrProjectEmailMappings)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_ProjectEmail_Mapping_tm_Project");
        });

        modelBuilder.Entity<TrProjectFloorPlan>(entity =>
        {
            entity.ToTable("TR_ProjectFloorPlan");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FileName).HasMaxLength(500);
            entity.Property(e => e.FilePath).HasMaxLength(500);
            entity.Property(e => e.MimeType)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.TrProjectFloorPlans)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_ProjectFloorPlan_tm_Project");
        });

        modelBuilder.Entity<TrProjectLandOffice>(entity =>
        {
            entity.ToTable("TR_ProjectLandOffice");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.LandOfficeId).HasColumnName("LandOfficeID");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");

            entity.HasOne(d => d.LandOffice).WithMany(p => p.TrProjectLandOffices)
                .HasForeignKey(d => d.LandOfficeId)
                .HasConstraintName("FK_TR_ProjectLandOffice_tm_LandOffice");

            entity.HasOne(d => d.Project).WithMany(p => p.TrProjectLandOffices)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_ProjectLandOffice_TR_ProjectLandOffice");
        });

        modelBuilder.Entity<TrProjectShopEvent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tr_ProjectShopEvent");

            entity.ToTable("TR_ProjectShopEvent");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EventDate).HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.ShopId).HasColumnName("ShopID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.TrProjectShopEvents)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_tr_ProjectShopEvent_tm_Project");

            entity.HasOne(d => d.Shop).WithMany(p => p.TrProjectShopEvents)
                .HasForeignKey(d => d.ShopId)
                .HasConstraintName("FK_tr_ProjectShopEvent_tm_Shop");
        });

        modelBuilder.Entity<TrProjectStatus>(entity =>
        {
            entity.ToTable("TR_ProjectStatus");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.StatusId).HasColumnName("StatusID");

            entity.HasOne(d => d.Project).WithMany(p => p.TrProjectStatuses)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_ProjectStatus_tm_Project");

            entity.HasOne(d => d.Status).WithMany(p => p.TrProjectStatuses)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK_TR_ProjectStatus_tm_Ext");
        });

        modelBuilder.Entity<TrProjectUnitFloorPlan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TR_UnitFloorPlan");

            entity.ToTable("TR_ProjectUnitFloorPlan");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProjectFloorPlanId).HasColumnName("ProjectFloorPlanID");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.ProjectFloorPlan).WithMany(p => p.TrProjectUnitFloorPlans)
                .HasForeignKey(d => d.ProjectFloorPlanId)
                .HasConstraintName("FK_TR_ProjectUnitFloorPlan_TR_ProjectFloorPlan");

            entity.HasOne(d => d.Project).WithMany(p => p.TrProjectUnitFloorPlans)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_ProjectUnitFloorPlan_tm_Project");

            entity.HasOne(d => d.Unit).WithMany(p => p.TrProjectUnitFloorPlans)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_TR_ProjectUnitFloorPlan_tm_Unit");
        });

        modelBuilder.Entity<TrProjectUserSign>(entity =>
        {
            entity.ToTable("TR_ProjectUserSign");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Project).WithMany(p => p.TrProjectUserSigns)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_ProjectUserSign_tm_Project");

            entity.HasOne(d => d.User).WithMany(p => p.TrProjectUserSigns)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_TR_ProjectUserSign_tm_User");
        });

        modelBuilder.Entity<TrProjectZoneMapping>(entity =>
        {
            entity.HasKey(e => new { e.ProjectZoneId, e.ProjectId });

            entity.ToTable("TR_ProjectZone_Mapping");

            entity.Property(e => e.ProjectZoneId).HasColumnName("ProjectZoneID");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");

            entity.HasOne(d => d.ProjectZone).WithMany(p => p.TrProjectZoneMappings)
                .HasForeignKey(d => d.ProjectZoneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TR_ProjectZone_Mapping_tm_Ext");
        });

        modelBuilder.Entity<TrQc1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TR_QC1_1");

            entity.ToTable("TR_QC1");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.QcDate)
                .HasColumnType("datetime")
                .HasColumnName("QC_Date");
            entity.Property(e => e.QcStatusId).HasColumnName("QC_StatusID");
            entity.Property(e => e.ResponsiblePersonId).HasColumnName("ResponsiblePersonID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.TrQc1s)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_QC1_tm_Project");

            entity.HasOne(d => d.QcStatus).WithMany(p => p.TrQc1s)
                .HasForeignKey(d => d.QcStatusId)
                .HasConstraintName("FK_TR_QC1_tm_Ext");

            entity.HasOne(d => d.ResponsiblePerson).WithMany(p => p.TrQc1s)
                .HasForeignKey(d => d.ResponsiblePersonId)
                .HasConstraintName("FK_TR_QC1_tm_User");

            entity.HasOne(d => d.Unit).WithMany(p => p.TrQc1s)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_TR_QC1_tm_Unit");
        });

        modelBuilder.Entity<TrQc2>(entity =>
        {
            entity.ToTable("TR_QC2");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.QcDate)
                .HasColumnType("datetime")
                .HasColumnName("QC_Date");
            entity.Property(e => e.QcStatusId).HasColumnName("QC_StatusID");
            entity.Property(e => e.ResponsiblePersonId).HasColumnName("ResponsiblePersonID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.TrQc2s)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_QC2_tm_Project");

            entity.HasOne(d => d.QcStatus).WithMany(p => p.TrQc2s)
                .HasForeignKey(d => d.QcStatusId)
                .HasConstraintName("FK_TR_QC2_tm_Ext");

            entity.HasOne(d => d.ResponsiblePerson).WithMany(p => p.TrQc2s)
                .HasForeignKey(d => d.ResponsiblePersonId)
                .HasConstraintName("FK_TR_QC2_tm_User");

            entity.HasOne(d => d.Unit).WithMany(p => p.TrQc2s)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_TR_QC2_tm_Unit");
        });

        modelBuilder.Entity<TrQc3>(entity =>
        {
            entity.ToTable("TR_QC3");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.QcDate)
                .HasColumnType("datetime")
                .HasColumnName("QC_Date");
            entity.Property(e => e.QcStatusId).HasColumnName("QC_StatusID");
            entity.Property(e => e.ResponsiblePersonId).HasColumnName("ResponsiblePersonID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.TrQc3s)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_QC3_tm_Project");

            entity.HasOne(d => d.QcStatus).WithMany(p => p.TrQc3s)
                .HasForeignKey(d => d.QcStatusId)
                .HasConstraintName("FK_TR_QC3_tm_Ext");

            entity.HasOne(d => d.ResponsiblePerson).WithMany(p => p.TrQc3s)
                .HasForeignKey(d => d.ResponsiblePersonId)
                .HasConstraintName("FK_TR_QC3_tm_User");

            entity.HasOne(d => d.Unit).WithMany(p => p.TrQc3s)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_TR_QC3_tm_Unit");
        });

        modelBuilder.Entity<TrQc4>(entity =>
        {
            entity.ToTable("TR_QC4");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.QcDate)
                .HasColumnType("datetime")
                .HasColumnName("QC_Date");
            entity.Property(e => e.QcStatusId).HasColumnName("QC_StatusID");
            entity.Property(e => e.ResponsiblePersonId).HasColumnName("ResponsiblePersonID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.TrQc4s)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_QC4_tm_Project");

            entity.HasOne(d => d.QcStatus).WithMany(p => p.TrQc4s)
                .HasForeignKey(d => d.QcStatusId)
                .HasConstraintName("FK_TR_QC4_tm_Ext");

            entity.HasOne(d => d.ResponsiblePerson).WithMany(p => p.TrQc4s)
                .HasForeignKey(d => d.ResponsiblePersonId)
                .HasConstraintName("FK_TR_QC4_tm_User");

            entity.HasOne(d => d.Unit).WithMany(p => p.TrQc4s)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_TR_QC4_tm_Unit");
        });

        modelBuilder.Entity<TrQc5>(entity =>
        {
            entity.ToTable("TR_QC5");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ClearDate).HasColumnType("datetime");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CustRelationId).HasColumnName("CustRelationID");
            entity.Property(e => e.CustomerName).HasMaxLength(500);
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Mobile)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.QcDate)
                .HasColumnType("datetime")
                .HasColumnName("QC_Date");
            entity.Property(e => e.QcStatusId).HasColumnName("QC_StatusID");
            entity.Property(e => e.QcTime)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("QC_Time");
            entity.Property(e => e.ResponsiblePersonId).HasColumnName("ResponsiblePersonID");
            entity.Property(e => e.SignResourceId).HasColumnName("SignResourceID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.CustRelation).WithMany(p => p.TrQc5CustRelations)
                .HasForeignKey(d => d.CustRelationId)
                .HasConstraintName("FK_TR_QC5_tm_Ext1");

            entity.HasOne(d => d.Project).WithMany(p => p.TrQc5s)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_QC5_tm_Project");

            entity.HasOne(d => d.QcStatus).WithMany(p => p.TrQc5QcStatuses)
                .HasForeignKey(d => d.QcStatusId)
                .HasConstraintName("FK_TR_QC5_tm_Ext");

            entity.HasOne(d => d.ResponsiblePerson).WithMany(p => p.TrQc5s)
                .HasForeignKey(d => d.ResponsiblePersonId)
                .HasConstraintName("FK_TR_QC5_tm_User");

            entity.HasOne(d => d.SignResource).WithMany(p => p.TrQc5s)
                .HasForeignKey(d => d.SignResourceId)
                .HasConstraintName("FK_TR_QC5_TR_SignResource");

            entity.HasOne(d => d.Unit).WithMany(p => p.TrQc5s)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_TR_QC5_tm_Unit");
        });

        modelBuilder.Entity<TrQc5CheckList>(entity =>
        {
            entity.ToTable("TR_QC5_CheckList");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CheckDate).HasColumnType("datetime");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.ScoreResult).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.TrQc5CheckLists)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_QC5_CheckList_tm_Project");

            entity.HasOne(d => d.Unit).WithMany(p => p.TrQc5CheckLists)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_TR_QC5_CheckList_tm_Unit");
        });

        modelBuilder.Entity<TrQc5CheckListDetail>(entity =>
        {
            entity.ToTable("TR_QC5_CheckList_Detail");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AnswerId).HasColumnName("AnswerID");
            entity.Property(e => e.CreateDtae).HasColumnType("datetime");
            entity.Property(e => e.Qc5checkListId).HasColumnName("QC5CheckListID");
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.Score).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Answer).WithMany(p => p.TrQc5CheckListDetails)
                .HasForeignKey(d => d.AnswerId)
                .HasConstraintName("FK_TR_QC5_CheckList_Detail_tm_Ext");

            entity.HasOne(d => d.Qc5checkList).WithMany(p => p.TrQc5CheckListDetails)
                .HasForeignKey(d => d.Qc5checkListId)
                .HasConstraintName("FK_TR_QC5_CheckList_Detail_TR_QC5_CheckList");

            entity.HasOne(d => d.Question).WithMany(p => p.TrQc5CheckListDetails)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK_TR_QC5_CheckList_Detail_tm_QC5_CheckList");
        });

        modelBuilder.Entity<TrQc5FinishPlan>(entity =>
        {
            entity.ToTable("TR_QC5_FinishPlan");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Build)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.FloorId).HasColumnName("FloorID");
            entity.Property(e => e.PlanDate).HasColumnType("datetime");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TrQc5Open>(entity =>
        {
            entity.ToTable("TR_QC5_Open");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.QcDate)
                .HasColumnType("datetime")
                .HasColumnName("QC_Date");
            entity.Property(e => e.QcStatusId).HasColumnName("QC_StatusID");
            entity.Property(e => e.ResponsiblePersonId).HasColumnName("ResponsiblePersonID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.TrQc5Opens)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_QC5_Open_tm_Project");

            entity.HasOne(d => d.QcStatus).WithMany(p => p.TrQc5Opens)
                .HasForeignKey(d => d.QcStatusId)
                .HasConstraintName("FK_TR_QC5_Open_tm_Ext");

            entity.HasOne(d => d.Unit).WithMany(p => p.TrQc5Opens)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_TR_QC5_Open_tm_Unit");
        });

        modelBuilder.Entity<TrQc5ProjectSendMail>(entity =>
        {
            entity.ToTable("TR_QC5_ProjectSendMail");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.SendToType)
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.HasOne(d => d.Project).WithMany(p => p.TrQc5ProjectSendMails)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_QC5_ProjectSendMail_tm_Project");
        });

        modelBuilder.Entity<TrQc6>(entity =>
        {
            entity.ToTable("TR_QC6");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ClearDate).HasColumnType("datetime");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CustRelationId).HasColumnName("CustRelationID");
            entity.Property(e => e.CustomerName).HasMaxLength(500);
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Mobile)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.QcDate)
                .HasColumnType("datetime")
                .HasColumnName("QC_Date");
            entity.Property(e => e.QcStatusId).HasColumnName("QC_StatusID");
            entity.Property(e => e.QcTime)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("QC_Time");
            entity.Property(e => e.ResponsiblePersonId).HasColumnName("ResponsiblePersonID");
            entity.Property(e => e.SignResourceId).HasColumnName("SignResourceID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.CustRelation).WithMany(p => p.TrQc6CustRelations)
                .HasForeignKey(d => d.CustRelationId)
                .HasConstraintName("FK_TR_QC6_tm_Ext1");

            entity.HasOne(d => d.Project).WithMany(p => p.TrQc6s)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_QC6_tm_Project");

            entity.HasOne(d => d.QcStatus).WithMany(p => p.TrQc6QcStatuses)
                .HasForeignKey(d => d.QcStatusId)
                .HasConstraintName("FK_TR_QC6_tm_Ext");

            entity.HasOne(d => d.ResponsiblePerson).WithMany(p => p.TrQc6s)
                .HasForeignKey(d => d.ResponsiblePersonId)
                .HasConstraintName("FK_TR_QC6_tm_User");

            entity.HasOne(d => d.SignResource).WithMany(p => p.TrQc6s)
                .HasForeignKey(d => d.SignResourceId)
                .HasConstraintName("FK_TR_QC6_TR_SignResource");

            entity.HasOne(d => d.Unit).WithMany(p => p.TrQc6s)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_TR_QC6_tm_Unit");
        });

        modelBuilder.Entity<TrQc6106c001>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TR_QC6_106C001");

            entity.Property(e => e.ClearDate).HasColumnType("datetime");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CustRelationId).HasColumnName("CustRelationID");
            entity.Property(e => e.CustomerName).HasMaxLength(500);
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Mobile)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.QcDate)
                .HasColumnType("datetime")
                .HasColumnName("QC_Date");
            entity.Property(e => e.QcStatusId).HasColumnName("QC_StatusID");
            entity.Property(e => e.QcTime)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("QC_Time");
            entity.Property(e => e.ResponsiblePersonId).HasColumnName("ResponsiblePersonID");
            entity.Property(e => e.SignResourceId).HasColumnName("SignResourceID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TrQc6ProjectSendMail>(entity =>
        {
            entity.ToTable("TR_QC6_ProjectSendMail");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Buid).HasColumnName("BUID");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.SendToType)
                .HasMaxLength(1)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TrQc6Unsold>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TR_QC_Unsold");

            entity.ToTable("TR_QC6_Unsold");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ClearDate).HasColumnType("datetime");
            entity.Property(e => e.CloseCaseDate).HasColumnType("datetime");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.QcDate)
                .HasColumnType("datetime")
                .HasColumnName("QC_Date");
            entity.Property(e => e.QcTime)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("QC_Time");
            entity.Property(e => e.SignResourceId).HasColumnName("SignResourceID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.TrQc6Unsolds)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_QC6_Unsold_tm_Project");

            entity.HasOne(d => d.SignResource).WithMany(p => p.TrQc6Unsolds)
                .HasForeignKey(d => d.SignResourceId)
                .HasConstraintName("FK_TR_QC6_Unsold_TR_SignResource");

            entity.HasOne(d => d.Unit).WithMany(p => p.TrQc6Unsolds)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_TR_QC6_Unsold_tm_Unit");
        });

        modelBuilder.Entity<TrQc6UnsoldSendMail>(entity =>
        {
            entity.ToTable("TR_QC6_Unsold_SendMail");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.SendToType)
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.HasOne(d => d.Project).WithMany(p => p.TrQc6UnsoldSendMails)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_QC6_Unsold_SendMail_tm_Project");
        });

        modelBuilder.Entity<TrQcCheckList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TR_QC1_CheckList");

            entity.ToTable("TR_QC_CheckList");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.QcId).HasColumnName("QC_ID");
            entity.Property(e => e.QctypeId).HasColumnName("QCTypeID");
            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
            entity.Property(e => e.TopicId).HasColumnName("TopicID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.TrQcCheckLists)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_QC_CheckList_tm_Project");

            entity.HasOne(d => d.Qctype).WithMany(p => p.TrQcCheckLists)
                .HasForeignKey(d => d.QctypeId)
                .HasConstraintName("FK_TR_QC_CheckList_tm_Ext");

            entity.HasOne(d => d.Subject).WithMany(p => p.TrQcCheckLists)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK_TR_QC_CheckList_tm_Subject");

            entity.HasOne(d => d.Topic).WithMany(p => p.TrQcCheckLists)
                .HasForeignKey(d => d.TopicId)
                .HasConstraintName("FK_TR_QC_CheckList_tm_Topic");

            entity.HasOne(d => d.Unit).WithMany(p => p.TrQcCheckLists)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_TR_QC_CheckList_tm_Unit");
        });

        modelBuilder.Entity<TrQcContactLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tr_QC_ContactLog");

            entity.ToTable("TR_QC_ContactLog");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.QcId).HasColumnName("QC_ID");
            entity.Property(e => e.QctypeId).HasColumnName("QCTypeID");
            entity.Property(e => e.TempId).HasColumnName("TempID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.TrQcContactLogs)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_tr_QC_ContactLog_tr_QC_ContactLog");

            entity.HasOne(d => d.Qc).WithMany(p => p.TrQcContactLogs)
                .HasForeignKey(d => d.QcId)
                .HasConstraintName("FK_tr_QC_ContactLog_TR_QC6");

            entity.HasOne(d => d.Qctype).WithMany(p => p.TrQcContactLogs)
                .HasForeignKey(d => d.QctypeId)
                .HasConstraintName("FK_tr_QC_ContactLog_tm_Ext");

            entity.HasOne(d => d.Unit).WithMany(p => p.TrQcContactLogs)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_tr_QC_ContactLog_tm_Unit");
        });

        modelBuilder.Entity<TrQcContactLogResource>(entity =>
        {
            entity.ToTable("TR_QC_ContactLogResource");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.QccontactLogId).HasColumnName("QCContactLogID");
            entity.Property(e => e.ResourceId).HasColumnName("ResourceID");

            entity.HasOne(d => d.QccontactLog).WithMany(p => p.TrQcContactLogResources)
                .HasForeignKey(d => d.QccontactLogId)
                .HasConstraintName("FK_TR_QC_ContactLogResource_TR_ContactLog");

            entity.HasOne(d => d.Resource).WithMany(p => p.TrQcContactLogResources)
                .HasForeignKey(d => d.ResourceId)
                .HasConstraintName("FK_TR_QC_ContactLogResource_TR_Resources");
        });

        modelBuilder.Entity<TrQcDefect>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TR_QCDefect");

            entity.ToTable("TR_QC_Defect");

            entity.HasIndex(e => new { e.ProjectId, e.UnitId, e.QcId, e.QctypeId, e.DefectStatusId, e.DefectAreaId, e.DefectTypeId }, "NonClusteredIndex-20201109-102830");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ClearDate).HasColumnType("datetime");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DefectAreaId).HasColumnName("DefectAreaID");
            entity.Property(e => e.DefectDescriptionId).HasColumnName("DefectDescriptionID");
            entity.Property(e => e.DefectStatusId).HasColumnName("DefectStatusID");
            entity.Property(e => e.DefectTypeId).HasColumnName("DefectTypeID");
            entity.Property(e => e.FloorPlanId).HasColumnName("FloorPlanID");
            entity.Property(e => e.PointX).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PointY).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.QcId).HasColumnName("QC_ID");
            entity.Property(e => e.QctypeId).HasColumnName("QCTypeID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.DefectArea).WithMany(p => p.TrQcDefects)
                .HasForeignKey(d => d.DefectAreaId)
                .HasConstraintName("FK_TR_QCDefect_tm_DefectArea");

            entity.HasOne(d => d.DefectDescription).WithMany(p => p.TrQcDefects)
                .HasForeignKey(d => d.DefectDescriptionId)
                .HasConstraintName("FK_TR_QCDefect_tm_DefectDescription");

            entity.HasOne(d => d.DefectStatus).WithMany(p => p.TrQcDefects)
                .HasForeignKey(d => d.DefectStatusId)
                .HasConstraintName("FK_TR_QCDefect_tm_Ext");

            entity.HasOne(d => d.DefectType).WithMany(p => p.TrQcDefects)
                .HasForeignKey(d => d.DefectTypeId)
                .HasConstraintName("FK_TR_QCDefect_tm_DefectType");

            entity.HasOne(d => d.Project).WithMany(p => p.TrQcDefects)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_QCDefect_tm_Project");

            entity.HasOne(d => d.Unit).WithMany(p => p.TrQcDefects)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_TR_QCDefect_tm_Unit");
        });

        modelBuilder.Entity<TrQcDefect20240518>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TR_QC_Defect_20240518");

            entity.Property(e => e.ClearDate).HasColumnType("datetime");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DefectAreaId).HasColumnName("DefectAreaID");
            entity.Property(e => e.DefectDescriptionId).HasColumnName("DefectDescriptionID");
            entity.Property(e => e.DefectStatusId).HasColumnName("DefectStatusID");
            entity.Property(e => e.DefectTypeId).HasColumnName("DefectTypeID");
            entity.Property(e => e.FloorPlanId).HasColumnName("FloorPlanID");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.PointX).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PointY).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.QcId).HasColumnName("QC_ID");
            entity.Property(e => e.QctypeId).HasColumnName("QCTypeID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TrQcDefectDraft>(entity =>
        {
            entity.HasKey(e => e.DefectId).HasName("PK_TR_QC_Defect_Draft_1");

            entity.ToTable("TR_QC_Defect_Draft");

            entity.Property(e => e.DefectId)
                .ValueGeneratedNever()
                .HasColumnName("DefectID");
            entity.Property(e => e.DefectStatusId).HasColumnName("DefectStatusID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
        });

        modelBuilder.Entity<TrQcDefectOverDueExpect>(entity =>
        {
            entity.ToTable("TR_QC_Defect_OverDueExpect");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DefectId).HasColumnName("DefectID");
            entity.Property(e => e.EstimateDate).HasColumnType("datetime");
            entity.Property(e => e.EstimateStatusId).HasColumnName("EstimateStatusID");
            entity.Property(e => e.ExpectDate).HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.InprocessDate).HasColumnType("datetime");
            entity.Property(e => e.OpenDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Defect).WithMany(p => p.TrQcDefectOverDueExpects)
                .HasForeignKey(d => d.DefectId)
                .HasConstraintName("FK_TR_QC_Defect_OverDueExpect_TR_QC_Defect");

            entity.HasOne(d => d.EstimateStatus).WithMany(p => p.TrQcDefectOverDueExpects)
                .HasForeignKey(d => d.EstimateStatusId)
                .HasConstraintName("FK_TR_QC_Defect_OverDueExpect_TR_QC_Defect_OverDueExpect");
        });

        modelBuilder.Entity<TrQcDefectOverDueExpectBk>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TR_QC_Defect_OverDueExpect_BK");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DefectId).HasColumnName("DefectID");
            entity.Property(e => e.EstimateDate).HasColumnType("datetime");
            entity.Property(e => e.EstimateStatusId).HasColumnName("EstimateStatusID");
            entity.Property(e => e.ExpectDate).HasColumnType("datetime");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.InprocessDate).HasColumnType("datetime");
            entity.Property(e => e.OpenDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TrQcDefectOverDueExpectUserPermission>(entity =>
        {
            entity.ToTable("TR_QC_Defect_OverDueExpect_UserPermission");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.TrQcDefectOverDueExpectUserPermissions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_TR_QC_Defect_OverDueExpect_UserPermission_tm_User");
        });

        modelBuilder.Entity<TrQcDefectResource>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TR_DefectResource");

            entity.ToTable("TR_QC_DefectResource");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.DefectId).HasColumnName("DefectID");
            entity.Property(e => e.ResourceId).HasColumnName("ResourceID");

            entity.HasOne(d => d.Defect).WithMany(p => p.TrQcDefectResources)
                .HasForeignKey(d => d.DefectId)
                .HasConstraintName("FK_TR_QC_DefectResource_TR_QC_Defect");

            entity.HasOne(d => d.Resource).WithMany(p => p.TrQcDefectResources)
                .HasForeignKey(d => d.ResourceId)
                .HasConstraintName("FK_TR_QC_DefectResource_TR_Resources");
        });

        modelBuilder.Entity<TrQuestionAnswer>(entity =>
        {
            entity.ToTable("TR_QuestionAnswer");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AnswerId).HasColumnName("AnswerID");
            entity.Property(e => e.ClearDate).HasColumnType("datetime");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.QuestionDate).HasColumnType("datetime");
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.UdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");

            entity.HasOne(d => d.Answer).WithMany(p => p.TrQuestionAnswers)
                .HasForeignKey(d => d.AnswerId)
                .HasConstraintName("FK_TR_QuestionAnswer_tm_Answer");

            entity.HasOne(d => d.Project).WithMany(p => p.TrQuestionAnswers)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_QuestionAnswer_tm_Project");

            entity.HasOne(d => d.Question).WithMany(p => p.TrQuestionAnswers)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK_TR_QuestionAnswer_tm_Question");
        });

        modelBuilder.Entity<TrQuestionC>(entity =>
        {
            entity.ToTable("TR_QuestionCS");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.QuestionTypeId).HasColumnName("QuestionTypeID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.QuestionType).WithMany(p => p.TrQuestionCs)
                .HasForeignKey(d => d.QuestionTypeId)
                .HasConstraintName("FK_TR_QuestionCS_tm_Ext");
        });

        modelBuilder.Entity<TrReceiveRoomAgreementSign>(entity =>
        {
            entity.ToTable("TR_ReceiveRoomAgreementSign");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.ReceiveRoomAgreementDate).HasColumnType("datetime");
            entity.Property(e => e.SignResourceId).HasColumnName("SignResourceID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");

            entity.HasOne(d => d.Project).WithMany(p => p.TrReceiveRoomAgreementSigns)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_ReceiveRoomAgreementSign_tm_Project");

            entity.HasOne(d => d.SignResource).WithMany(p => p.TrReceiveRoomAgreementSigns)
                .HasForeignKey(d => d.SignResourceId)
                .HasConstraintName("FK_TR_ReceiveRoomAgreementSign_TR_SignResource");

            entity.HasOne(d => d.Unit).WithMany(p => p.TrReceiveRoomAgreementSigns)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_TR_ReceiveRoomAgreementSign_tm_Unit");
        });

        modelBuilder.Entity<TrReceiveRoomSign>(entity =>
        {
            entity.ToTable("TR_ReceiveRoomSign");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.ReceiveRoomDate).HasColumnType("datetime");
            entity.Property(e => e.SignResourceId).HasColumnName("SignResourceID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");

            entity.HasOne(d => d.Project).WithMany(p => p.TrReceiveRoomSigns)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_ReceiveRoomSign_tm_Project");

            entity.HasOne(d => d.SignResource).WithMany(p => p.TrReceiveRoomSigns)
                .HasForeignKey(d => d.SignResourceId)
                .HasConstraintName("FK_TR_ReceiveRoomSign_TR_SignResource");

            entity.HasOne(d => d.Unit).WithMany(p => p.TrReceiveRoomSigns)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_TR_ReceiveRoomSign_tm_Unit");
        });

        modelBuilder.Entity<TrRegisterBank>(entity =>
        {
            entity.ToTable("TR_RegisterBank");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.BankId).HasColumnName("BankID");
            entity.Property(e => e.RegisterLogId).HasColumnName("RegisterLogID");

            entity.HasOne(d => d.RegisterLog).WithMany(p => p.TrRegisterBanks)
                .HasForeignKey(d => d.RegisterLogId)
                .HasConstraintName("FK_TR_RegisterBank_TR_RegisterLog");
        });

        modelBuilder.Entity<TrRegisterBankCounter>(entity =>
        {
            entity.ToTable("TR_Register_BankCounter");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BankCounterStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BankId).HasColumnName("BankID");
            entity.Property(e => e.CheckInDate).HasColumnType("datetime");
            entity.Property(e => e.CheckOutDate).HasColumnType("datetime");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.InProcessDate).HasColumnType("datetime");
            entity.Property(e => e.RegisterLogId).HasColumnName("RegisterLogID");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Bank).WithMany(p => p.TrRegisterBankCounters)
                .HasForeignKey(d => d.BankId)
                .HasConstraintName("FK_TR_Register_BankCounter_tm_Bank");

            entity.HasOne(d => d.RegisterLog).WithMany(p => p.TrRegisterBankCounters)
                .HasForeignKey(d => d.RegisterLogId)
                .HasConstraintName("FK_TR_Register_BankCounter_TR_RegisterLog");
        });

        modelBuilder.Entity<TrRegisterCallStaffCounter>(entity =>
        {
            entity.ToTable("TR_Register_CallStaffCounter");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ActionDate).HasColumnType("datetime");
            entity.Property(e => e.CallStaffStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.RegisterLogId).HasColumnName("RegisterLogID");

            entity.HasOne(d => d.RegisterLog).WithMany(p => p.TrRegisterCallStaffCounters)
                .HasForeignKey(d => d.RegisterLogId)
                .HasConstraintName("FK_TR_Register_CallStaffCounter_TR_RegisterLog");
        });

        modelBuilder.Entity<TrRegisterLog>(entity =>
        {
            entity.ToTable("TR_RegisterLog");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CareerTypeId).HasColumnName("CareerTypeID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.FastFixDate).HasColumnType("datetime");
            entity.Property(e => e.FastFixFinishDate).HasColumnType("datetime");
            entity.Property(e => e.FinishDate).HasColumnType("datetime");
            entity.Property(e => e.InprocessDate).HasColumnType("datetime");
            entity.Property(e => e.LoanId).HasColumnName("LoanID");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.QctypeId).HasColumnName("QCTypeID");
            entity.Property(e => e.QueueTypeId).HasColumnName("QueueTypeID");
            entity.Property(e => e.ReasonId).HasColumnName("ReasonID");
            entity.Property(e => e.RegisterDate).HasColumnType("datetime");
            entity.Property(e => e.ResponsibleId).HasColumnName("ResponsibleID");
            entity.Property(e => e.TransferTypeId).HasColumnName("TransferTypeID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.WaitDate).HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.TrRegisterLogs)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_RegisterLog_tm_Project");

            entity.HasOne(d => d.Qctype).WithMany(p => p.TrRegisterLogs)
                .HasForeignKey(d => d.QctypeId)
                .HasConstraintName("FK_TR_RegisterLog_tm_Ext");

            entity.HasOne(d => d.Responsible).WithMany(p => p.TrRegisterLogs)
                .HasForeignKey(d => d.ResponsibleId)
                .HasConstraintName("FK_TR_RegisterLog_tm_User");

            entity.HasOne(d => d.Unit).WithMany(p => p.TrRegisterLogs)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_TR_RegisterLog_tm_Unit");
        });

        modelBuilder.Entity<TrRegisterProjectBankStaff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TR_Register_BankStaff");

            entity.ToTable("TR_Register_ProjectBankStaff");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BankId).HasColumnName("BankID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.UpdateBy).HasColumnName("updateBy");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Bank).WithMany(p => p.TrRegisterProjectBankStaffs)
                .HasForeignKey(d => d.BankId)
                .HasConstraintName("FK_TR_Register_BankStaff_tm_Bank");

            entity.HasOne(d => d.Project).WithMany(p => p.TrRegisterProjectBankStaffs)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_Register_BankStaff_tm_Project");
        });

        modelBuilder.Entity<TrRemarkUnitStatusMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TR_RemarkUnitStatus_Mapping_1");

            entity.ToTable("TR_RemarkUnitStatus_Mapping");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.RemarkUnitStatusCsId).HasColumnName("RemarkUnitStatusCS_ID");
            entity.Property(e => e.UnitStatusCsId).HasColumnName("UnitStatusCS_ID");

            entity.HasOne(d => d.RemarkUnitStatusCs).WithMany(p => p.TrRemarkUnitStatusMappingRemarkUnitStatusCs)
                .HasForeignKey(d => d.RemarkUnitStatusCsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TR_RemarkUnitStatus_Mapping_tm_Ext1");

            entity.HasOne(d => d.UnitStatusCs).WithMany(p => p.TrRemarkUnitStatusMappingUnitStatusCs)
                .HasForeignKey(d => d.UnitStatusCsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TR_RemarkUnitStatus_Mapping_tm_Ext");
        });

        modelBuilder.Entity<TrResource>(entity =>
        {
            entity.ToTable("TR_Resources");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FileName).HasMaxLength(500);
            entity.Property(e => e.FilePath).HasMaxLength(500);
            entity.Property(e => e.MimeType)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.OriginalFileName).HasMaxLength(500);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TrReviseUnitPromotion>(entity =>
        {
            entity.ToTable("TR_ReviseUnitPromotion");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ApproveBy2).HasColumnName("ApproveBy_2");
            entity.Property(e => e.ApproveDate).HasColumnType("datetime");
            entity.Property(e => e.ApproveDate2)
                .HasColumnType("datetime")
                .HasColumnName("ApproveDate_2");
            entity.Property(e => e.ApproveRemark2).HasColumnName("ApproveRemark_2");
            entity.Property(e => e.ApproveStatusId).HasColumnName("ApproveStatusID");
            entity.Property(e => e.ApproveStatusId2).HasColumnName("ApproveStatusID_2");
            entity.Property(e => e.ClearDate).HasColumnType("datetime");
            entity.Property(e => e.ContractCustomerName).HasMaxLength(500);
            entity.Property(e => e.ContractCustomerSureName).HasMaxLength(500);
            entity.Property(e => e.ContractDistrict).HasMaxLength(100);
            entity.Property(e => e.ContractMobile).HasMaxLength(100);
            entity.Property(e => e.ContractMoo).HasMaxLength(100);
            entity.Property(e => e.ContractNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ContractPostalCode).HasMaxLength(10);
            entity.Property(e => e.ContractProvince).HasMaxLength(100);
            entity.Property(e => e.ContractRoad).HasMaxLength(100);
            entity.Property(e => e.ContractSoi).HasMaxLength(100);
            entity.Property(e => e.ContractSubdistrict).HasMaxLength(100);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CustomerBankId).HasColumnName("CustomerBankID");
            entity.Property(e => e.CustomerBookBank)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CustomerResourceId).HasColumnName("CustomerResourceID");
            entity.Property(e => e.CustomerSignDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerSignId).HasColumnName("CustomerSignID");
            entity.Property(e => e.PrintDate).HasColumnType("datetime");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.SendMailDate).HasColumnType("datetime");
            entity.Property(e => e.UnitDocumentId).HasColumnName("UnitDocumentID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.ApproveStatus).WithMany(p => p.TrReviseUnitPromotionApproveStatuses)
                .HasForeignKey(d => d.ApproveStatusId)
                .HasConstraintName("FK_TR_ReviseUnitPromotion_tm_Ext");

            entity.HasOne(d => d.ApproveStatusId2Navigation).WithMany(p => p.TrReviseUnitPromotionApproveStatusId2Navigations)
                .HasForeignKey(d => d.ApproveStatusId2)
                .HasConstraintName("FK_TR_ReviseUnitPromotion_tm_Ext1");

            entity.HasOne(d => d.CustomerBank).WithMany(p => p.TrReviseUnitPromotions)
                .HasForeignKey(d => d.CustomerBankId)
                .HasConstraintName("FK_TR_ReviseUnitPromotion_tm_Bank");

            entity.HasOne(d => d.Project).WithMany(p => p.TrReviseUnitPromotions)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_ReviseUnitPromotion_tm_Project");

            entity.HasOne(d => d.Unit).WithMany(p => p.TrReviseUnitPromotions)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_TR_ReviseUnitPromotion_tm_Unit");
        });

        modelBuilder.Entity<TrReviseUnitPromotionDetail>(entity =>
        {
            entity.ToTable("TR_ReviseUnitPromotion_Detail");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.MpromotionId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MPromotionID");
            entity.Property(e => e.PdetailId).HasColumnName("PDetailID");
            entity.Property(e => e.PromotionAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PromotionId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PromotionID");
            entity.Property(e => e.ReviseUnitPromotionId).HasColumnName("ReviseUnitPromotionID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.ReviseUnitPromotion).WithMany(p => p.TrReviseUnitPromotionDetails)
                .HasForeignKey(d => d.ReviseUnitPromotionId)
                .HasConstraintName("FK_TR_ReviseUnitPromotion_Detail_TR_ReviseUnitPromotion");
        });

        modelBuilder.Entity<TrReviseUnitPromotonApprover>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TR_ReviseUnitPromoton_Apprvoe");

            entity.ToTable("TR_ReviseUnitPromoton_Approver");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ApproveRoleId).HasColumnName("ApproveRoleID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UpdateBy).HasColumnName("UpdateBY");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.ApproveRole).WithMany(p => p.TrReviseUnitPromotonApprovers)
                .HasForeignKey(d => d.ApproveRoleId)
                .HasConstraintName("FK_TR_ReviseUnitPromoton_Approve_tm_Ext");

            entity.HasOne(d => d.User).WithMany(p => p.TrReviseUnitPromotonApprovers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_TR_ReviseUnitPromoton_Approve_tm_User");
        });

        modelBuilder.Entity<TrSignResource>(entity =>
        {
            entity.ToTable("TR_SignResource");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FileName).HasMaxLength(500);
            entity.Property(e => e.FilePath).HasMaxLength(500);
            entity.Property(e => e.MimeType)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TrSync>(entity =>
        {
            entity.ToTable("TR_Sync");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Module)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.SyncDate).HasColumnType("datetime");
            entity.Property(e => e.SyncStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UnitId).HasColumnName("UnitID");

            entity.HasOne(d => d.Project).WithMany(p => p.TrSyncs)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_Sync_tm_Project");

            entity.HasOne(d => d.Unit).WithMany(p => p.TrSyncs)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_TR_Sync_tm_Unit");
        });

        modelBuilder.Entity<TrSyncLoanBank>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TR_Sync_LoanBank_CRM");

            entity.ToTable("TR_Sync_LoanBank");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AppAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.AppDecorate).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.AppFireInsurance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.AppLifeInsurance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.AppOther).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ApproveDate).HasColumnType("datetime");
            entity.Property(e => e.BankBranchId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BankBranchID");
            entity.Property(e => e.BankBranchName).HasMaxLength(2000);
            entity.Property(e => e.BankCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BankContactEmail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.BankContactName).HasMaxLength(200);
            entity.Property(e => e.BankContactPhone)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CompleteDocDate).HasColumnType("datetime");
            entity.Property(e => e.ContractName).HasMaxLength(2000);
            entity.Property(e => e.ContractNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ContractSellingPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.LoanReqAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.LoanReqDate).HasColumnType("datetime");
            entity.Property(e => e.LoanSignDate).HasColumnType("datetime");
            entity.Property(e => e.LoanStatusDate).HasColumnType("datetime");
            entity.Property(e => e.MortgageDate).HasColumnType("datetime");
            entity.Property(e => e.PayDecorate).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PayOther).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PayPeriodAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PriAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PriApproveDate).HasColumnType("datetime");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.ReasonId).HasColumnName("ReasonID");
            entity.Property(e => e.ReviewDate).HasColumnType("datetime");
            entity.Property(e => e.SentDocDate).HasColumnType("datetime");
            entity.Property(e => e.SyncId).HasColumnName("SyncID");
            entity.Property(e => e.UnitCode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Sync).WithMany(p => p.TrSyncLoanBanks)
                .HasForeignKey(d => d.SyncId)
                .HasConstraintName("FK_TR_Sync_LoanBank_CRM_TR_Sync");
        });

        modelBuilder.Entity<TrSyncLog>(entity =>
        {
            entity.ToTable("TR_Sync_Logs");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");

            entity.HasOne(d => d.Project).WithMany(p => p.TrSyncLogs)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_Sync_Logs_tm_Project");
        });

        modelBuilder.Entity<TrSyncQc>(entity =>
        {
            entity.ToTable("TR_Sync_QC");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.QcappointDate)
                .HasColumnType("datetime")
                .HasColumnName("QCAppointDate");
            entity.Property(e => e.QcappointTimeFrom)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("QCAppointTimeFrom");
            entity.Property(e => e.QcappointTimeTo)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("QCAppointTimeTo");
            entity.Property(e => e.Qcremark).HasColumnName("QCRemark");
            entity.Property(e => e.QcresponseDate)
                .HasColumnType("datetime")
                .HasColumnName("QCResponseDate");
            entity.Property(e => e.QcresponseUserId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("QCResponseUserID");
            entity.Property(e => e.QctypeId).HasColumnName("QCTypeID");
            entity.Property(e => e.SyncId).HasColumnName("SyncID");
            entity.Property(e => e.SyncTypeDetail)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.SyncTypeId).HasColumnName("SyncTypeID");
            entity.Property(e => e.UnitCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UnitId).HasColumnName("UnitID");

            entity.HasOne(d => d.Project).WithMany(p => p.TrSyncQcs)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_Sync_QC_tm_Project");

            entity.HasOne(d => d.Qctype).WithMany(p => p.TrSyncQcs)
                .HasForeignKey(d => d.QctypeId)
                .HasConstraintName("FK_TR_Sync_QC_tm_Ext");

            entity.HasOne(d => d.Sync).WithMany(p => p.TrSyncQcs)
                .HasForeignKey(d => d.SyncId)
                .HasConstraintName("FK_TR_Sync_QC_TR_Sync");

            entity.HasOne(d => d.Unit).WithMany(p => p.TrSyncQcs)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_TR_Sync_QC_tm_Unit");
        });

        modelBuilder.Entity<TrTargetRollingPlan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TR_TargetRolloing");

            entity.ToTable("TR_TargetRollingPlan");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.MonthlyDate).HasColumnType("datetime");
            entity.Property(e => e.PlanAmountId).HasColumnName("PlanAmountID");
            entity.Property(e => e.PlanTypeId).HasColumnName("PlanTypeID");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.PlanAmount).WithMany(p => p.TrTargetRollingPlanPlanAmounts)
                .HasForeignKey(d => d.PlanAmountId)
                .HasConstraintName("FK_TR_TargetRollingPlan_tm_Ext");

            entity.HasOne(d => d.PlanType).WithMany(p => p.TrTargetRollingPlanPlanTypes)
                .HasForeignKey(d => d.PlanTypeId)
                .HasConstraintName("FK_TR_TargetRolloingPlan_tm_Ext");

            entity.HasOne(d => d.Project).WithMany(p => p.TrTargetRollingPlans)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_TargetRolloingPlan_tm_Project");
        });

        modelBuilder.Entity<TrTargetRollingPlanBk>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TR_TargetRollingPlan_BK");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.MonthlyDate).HasColumnType("datetime");
            entity.Property(e => e.PlanAmountId).HasColumnName("PlanAmountID");
            entity.Property(e => e.PlanTypeId).HasColumnName("PlanTypeID");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TrTerminateTransferAppoint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TR_UnitCancelContract");

            entity.ToTable("TR_TerminateTransferAppoint");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.TerminateDate).HasColumnType("datetime");
            entity.Property(e => e.TerminateStatusId).HasColumnName("TerminateStatusID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.TerminateStatus).WithMany(p => p.TrTerminateTransferAppoints)
                .HasForeignKey(d => d.TerminateStatusId)
                .HasConstraintName("FK_TR_TerminateTransferAppoint_tm_Ext");

            entity.HasOne(d => d.Unit).WithMany(p => p.TrTerminateTransferAppoints)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_TR_TerminateTransferAppoint_tm_Unit");
        });

        modelBuilder.Entity<TrTerminateTransferAppointDocument>(entity =>
        {
            entity.ToTable("TR_TerminateTransferAppoint_Document");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.TerminateTransferAppointId).HasColumnName("TerminateTransferAppointID");
            entity.Property(e => e.UnitDocumentId).HasColumnName("UnitDocumentID");

            entity.HasOne(d => d.TerminateTransferAppoint).WithMany(p => p.TrTerminateTransferAppointDocuments)
                .HasForeignKey(d => d.TerminateTransferAppointId)
                .HasConstraintName("FK_TR_TerminateTransferAppoint_Document_TR_TerminateTransferAppoint");

            entity.HasOne(d => d.UnitDocument).WithMany(p => p.TrTerminateTransferAppointDocuments)
                .HasForeignKey(d => d.UnitDocumentId)
                .HasConstraintName("FK_TR_TerminateTransferAppoint_Document_TR_UnitDocument");
        });

        modelBuilder.Entity<TrTransferAppointHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tr_TransferAppointHistory");

            entity.ToTable("TR_TransferAppointHistory");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Bu)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BU");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.TransferAppointValue).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TransferValue).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<TrTransferDocument>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TR_Document_TransferReceive");

            entity.ToTable("TR_TransferDocument");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CashAmt)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Cash_Amt");
            entity.Property(e => e.Cashier1Amt)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Cashier1_Amt");
            entity.Property(e => e.Cashier1No)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Cashier1_No");
            entity.Property(e => e.Cashier2Amt)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Cashier2_Amt");
            entity.Property(e => e.Cashier2No)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Cashier2_No");
            entity.Property(e => e.Cashier3Amt)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Cashier3_Amt");
            entity.Property(e => e.Cashier3No)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Cashier3_No");
            entity.Property(e => e.CashierBankId).HasColumnName("Cashier_BankID");
            entity.Property(e => e.ContractC).HasColumnName("Contract_C");
            entity.Property(e => e.ContractR).HasColumnName("Contract_R");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CustomerMobile)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CustomerName).HasMaxLength(1000);
            entity.Property(e => e.MortgageC).HasColumnName("Mortgage_C");
            entity.Property(e => e.MortgageR).HasColumnName("Mortgage_R");
            entity.Property(e => e.OwnerC).HasColumnName("Owner_C");
            entity.Property(e => e.OwnerR).HasColumnName("Owner_R");
            entity.Property(e => e.PrintDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.RebateAmt)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Rebate_Amt");
            entity.Property(e => e.ReceiptC).HasColumnName("Receipt_C");
            entity.Property(e => e.ReceiptR).HasColumnName("Receipt_R");
            entity.Property(e => e.SignResourceId).HasColumnName("SignResourceID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.CashierBank).WithMany(p => p.TrTransferDocuments)
                .HasForeignKey(d => d.CashierBankId)
                .HasConstraintName("FK_TR_TransferDocument_tm_Bank");

            entity.HasOne(d => d.Project).WithMany(p => p.TrTransferDocuments)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_TransferDocument_tm_Project");

            entity.HasOne(d => d.Unit).WithMany(p => p.TrTransferDocuments)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_TR_TransferDocument_tm_Unit");
        });

        modelBuilder.Entity<TrUnitDocument>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TR_Document");

            entity.ToTable("TR_UnitDocument");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DocumentNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeID");
            entity.Property(e => e.FileName).HasMaxLength(500);
            entity.Property(e => e.FilePath).HasMaxLength(500);
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.MimeType)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.OriginalFileName).HasMaxLength(500);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.QctypeId).HasColumnName("QCTypeID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.DocumentType).WithMany(p => p.TrUnitDocumentDocumentTypes)
                .HasForeignKey(d => d.DocumentTypeId)
                .HasConstraintName("FK_TR_UnitDocument_tm_Ext1");

            entity.HasOne(d => d.Project).WithMany(p => p.TrUnitDocuments)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_UnitDocument_tm_Project");

            entity.HasOne(d => d.Qctype).WithMany(p => p.TrUnitDocumentQctypes)
                .HasForeignKey(d => d.QctypeId)
                .HasConstraintName("FK_TR_UnitDocument_tm_Ext");

            entity.HasOne(d => d.Unit).WithMany(p => p.TrUnitDocuments)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_TR_UnitDocument_tm_Unit");
        });

        modelBuilder.Entity<TrUnitDocumentNo>(entity =>
        {
            entity.ToTable("TR_UnitDocumentNo");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DocumentNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeID");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.QcId).HasColumnName("QC_ID");
            entity.Property(e => e.QctypeId).HasColumnName("QCTypeID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.DocumentType).WithMany(p => p.TrUnitDocumentNoDocumentTypes)
                .HasForeignKey(d => d.DocumentTypeId)
                .HasConstraintName("FK_TR_UnitDocumentNo_tm_Ext1");

            entity.HasOne(d => d.Project).WithMany(p => p.TrUnitDocumentNos)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_UnitDocumentNo_tm_Project");

            entity.HasOne(d => d.Qctype).WithMany(p => p.TrUnitDocumentNoQctypes)
                .HasForeignKey(d => d.QctypeId)
                .HasConstraintName("FK_TR_UnitDocumentNo_tm_Ext");

            entity.HasOne(d => d.Unit).WithMany(p => p.TrUnitDocumentNos)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_TR_UnitDocumentNo_tm_Unit");
        });

        modelBuilder.Entity<TrUnitEquipment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tr_UnitEquipment");

            entity.ToTable("TR_UnitEquipment");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ClearDate).HasColumnType("datetime");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CustomerSignDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerSignId).HasColumnName("CustomerSignID");
            entity.Property(e => e.JmsignId).HasColumnName("JMSignID");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.UnitDocumentId).HasColumnName("UnitDocumentID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.CustomerSign).WithMany(p => p.TrUnitEquipmentCustomerSigns)
                .HasForeignKey(d => d.CustomerSignId)
                .HasConstraintName("FK_tr_UnitEquipment_TR_SignResource");

            entity.HasOne(d => d.Jmsign).WithMany(p => p.TrUnitEquipmentJmsigns)
                .HasForeignKey(d => d.JmsignId)
                .HasConstraintName("FK_TR_UnitEquipment_TR_SignResource1");

            entity.HasOne(d => d.Project).WithMany(p => p.TrUnitEquipments)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_tr_UnitEquipment_tm_Project");

            entity.HasOne(d => d.UnitDocument).WithMany(p => p.TrUnitEquipments)
                .HasForeignKey(d => d.UnitDocumentId)
                .HasConstraintName("FK_tr_UnitEquipment_TR_UnitDocument");

            entity.HasOne(d => d.Unit).WithMany(p => p.TrUnitEquipments)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_tr_UnitEquipment_tm_Unit");
        });

        modelBuilder.Entity<TrUnitEquipmentDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tr_UnitEquipment_Detail");

            entity.ToTable("TR_UnitEquipment_Detail");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.UnitEquipmentId).HasColumnName("UnitEquipmentID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TrUnitEvent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tr_UnitEvent");

            entity.ToTable("TR_UnitEvent");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CraeteDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Event).WithMany(p => p.TrUnitEvents)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK_TR_UnitEvent_tm_Event");

            entity.HasOne(d => d.Unit).WithMany(p => p.TrUnitEvents)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_TR_UnitEvent_tm_Unit");
        });

        modelBuilder.Entity<TrUnitFurniture>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tr_UnitFurniture");

            entity.ToTable("TR_UnitFurniture");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CheckDate).HasColumnType("datetime");
            entity.Property(e => e.CheckStatusId).HasColumnName("CheckStatusID");
            entity.Property(e => e.ClearDate).HasColumnType("datetime");
            entity.Property(e => e.CmsignDate)
                .HasColumnType("datetime")
                .HasColumnName("CMSignDate");
            entity.Property(e => e.CmsignId).HasColumnName("CMSignID");
            entity.Property(e => e.CmsignName)
                .HasMaxLength(1000)
                .HasColumnName("CMSignName");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CustomerSignDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerSignId).HasColumnName("CustomerSignID");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.UnitDocumentId).HasColumnName("UnitDocumentID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.CheckStatus).WithMany(p => p.TrUnitFurnitures)
                .HasForeignKey(d => d.CheckStatusId)
                .HasConstraintName("FK_TR_UnitFurniture_tm_Ext");

            entity.HasOne(d => d.Cmsign).WithMany(p => p.TrUnitFurnitureCmsigns)
                .HasForeignKey(d => d.CmsignId)
                .HasConstraintName("FK_TR_UnitFurniture_TR_SignResource1");

            entity.HasOne(d => d.CustomerSign).WithMany(p => p.TrUnitFurnitureCustomerSigns)
                .HasForeignKey(d => d.CustomerSignId)
                .HasConstraintName("FK_TR_UnitFurniture_TR_SignResource");

            entity.HasOne(d => d.Project).WithMany(p => p.TrUnitFurnitures)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_UnitFurniture_tm_Project");

            entity.HasOne(d => d.UnitDocument).WithMany(p => p.TrUnitFurnitures)
                .HasForeignKey(d => d.UnitDocumentId)
                .HasConstraintName("FK_TR_UnitFurniture_TR_UnitDocument");

            entity.HasOne(d => d.Unit).WithMany(p => p.TrUnitFurnitures)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_TR_UnitFurniture_tm_Unit");
        });

        modelBuilder.Entity<TrUnitFurnitureDetail>(entity =>
        {
            entity.ToTable("TR_UnitFurniture_Detail");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.FurnitureId).HasColumnName("FurnitureID");
            entity.Property(e => e.UnitFurnitureId).HasColumnName("UnitFurnitureID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Furniture).WithMany(p => p.TrUnitFurnitureDetails)
                .HasForeignKey(d => d.FurnitureId)
                .HasConstraintName("FK_TR_UnitFurniture_Detail_tm_Funiture");

            entity.HasOne(d => d.UnitFurniture).WithMany(p => p.TrUnitFurnitureDetails)
                .HasForeignKey(d => d.UnitFurnitureId)
                .HasConstraintName("FK_TR_UnitFurniture_Detail_TR_UnitFurniture");
        });

        modelBuilder.Entity<TrUnitPromotion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TR_TransferDocument_Promotion");

            entity.ToTable("TR_UnitPromotion");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.PromotionId).HasColumnName("PromotionID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Promotion).WithMany(p => p.TrUnitPromotions)
                .HasForeignKey(d => d.PromotionId)
                .HasConstraintName("FK_TR_TransferDocument_Promotion_tm_Promotion");

            entity.HasOne(d => d.Unit).WithMany(p => p.TrUnitPromotions)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_TR_UnitPromotion_tm_Unit");
        });

        modelBuilder.Entity<TrUnitPromotionSign>(entity =>
        {
            entity.ToTable("TR_UnitPromotionSign");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.IdcardResourceId).HasColumnName("IDCardResourceID");
            entity.Property(e => e.SignDate).HasColumnType("datetime");
            entity.Property(e => e.SignResourceId).HasColumnName("SignResourceID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");

            entity.HasOne(d => d.IdcardResource).WithMany(p => p.TrUnitPromotionSignIdcardResources)
                .HasForeignKey(d => d.IdcardResourceId)
                .HasConstraintName("FK_TR_UnitPromotionSign_TR_SignResource1");

            entity.HasOne(d => d.SignResource).WithMany(p => p.TrUnitPromotionSignSignResources)
                .HasForeignKey(d => d.SignResourceId)
                .HasConstraintName("FK_TR_UnitPromotionSign_TR_SignResource");

            entity.HasOne(d => d.Unit).WithMany(p => p.TrUnitPromotionSigns)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_TR_UnitPromotionSign_tm_Unit");
        });

        modelBuilder.Entity<TrUnitPromotionSignDetail>(entity =>
        {
            entity.ToTable("TR_UnitPromotionSign_Detail");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.PromotionId).HasColumnName("PromotionID");
            entity.Property(e => e.UnitPromotionSignId).HasColumnName("UnitPromotionSignID");
        });

        modelBuilder.Entity<TrUnitShopEvent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tr_UnitShopEvent");

            entity.ToTable("TR_UnitShopEvent");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ContractNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EventDate).HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.ShopId).HasColumnName("ShopID");
            entity.Property(e => e.UnitCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TrUnitUserMapping>(entity =>
        {
            entity.ToTable("TR_UnitUser_Mapping");

            entity.HasIndex(e => new { e.ProjectId, e.UnitCode, e.UserId }, "NonClusteredIndex-20230421-095349");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.UnitCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<TrUnsoldRound>(entity =>
        {
            entity.ToTable("TR_Unsold_Round");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.RoundDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Project).WithMany(p => p.TrUnsoldRounds)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_Unsold_Round_tm_Project");
        });

        modelBuilder.Entity<TrUnsoldRoundUnit>(entity =>
        {
            entity.ToTable("TR_Unsold_RoundUnit");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.RoundId).HasColumnName("RoundID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.TrUnsoldRoundUnits)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TR_Unsold_RoundUnit_tm_Project");

            entity.HasOne(d => d.Round).WithMany(p => p.TrUnsoldRoundUnits)
                .HasForeignKey(d => d.RoundId)
                .HasConstraintName("FK_TR_Unsold_RoundUnit_TR_Unsold_Round");

            entity.HasOne(d => d.Unit).WithMany(p => p.TrUnsoldRoundUnits)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_TR_Unsold_RoundUnit_tm_Unit");
        });

        modelBuilder.Entity<TrUserPosition>(entity =>
        {
            entity.ToTable("TR_UserPosition");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.PositionId).HasColumnName("PositionID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Position).WithMany(p => p.TrUserPositions)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("FK_TR_UserPosition_tm_Position");

            entity.HasOne(d => d.User).WithMany(p => p.TrUserPositions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_TR_UserPosition_tm_User");
        });

        modelBuilder.Entity<TrUserSignResource>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_TR_UserSignResource_1");

            entity.ToTable("TR_UserSignResource");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("UserID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.SignResourceId).HasColumnName("SignResourceID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.SignResource).WithMany(p => p.TrUserSignResources)
                .HasForeignKey(d => d.SignResourceId)
                .HasConstraintName("FK_TR_UserSignResource_TR_SignResource");

            entity.HasOne(d => d.User).WithOne(p => p.TrUserSignResource)
                .HasForeignKey<TrUserSignResource>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TR_UserSignResource_tm_User");
        });

        modelBuilder.Entity<UnitFixdone>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Unit_FIXDone");

            entity.Property(e => e.CustomerName).HasMaxLength(500);
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.InspectStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.ReceiveRoomDate).HasColumnType("datetime");
            entity.Property(e => e.TransferDate).HasColumnType("datetime");
            entity.Property(e => e.UnitCode)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwBiBacklog>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_BI_Backlog");

            entity.Property(e => e.ActiveUnit).HasColumnName("Active_Unit");
            entity.Property(e => e.ActiveValue)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("Active_Value");
            entity.Property(e => e.Md)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MD");
            entity.Property(e => e.NonActiveUnit).HasColumnName("NonActive_Unit");
            entity.Property(e => e.NonActiveValue)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("NonActive_Value");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.ProjectName).HasMaxLength(500);
            entity.Property(e => e.ProjectStatus).HasMaxLength(200);
            entity.Property(e => e.TotalNonAvailableUnit).HasColumnName("TotalNonAvailable_Unit");
            entity.Property(e => e.TotalNonAvailableValue)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("TotalNonAvailable_Value");
            entity.Property(e => e.TotalTransferUnit).HasColumnName("TotalTransfer_Unit");
            entity.Property(e => e.TotalTransferValue)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("TotalTransfer_Value");
            entity.Property(e => e.TotalUnit).HasColumnName("Total_Unit");
            entity.Property(e => e.TotalValue).HasColumnName("Total_Value");
        });

        modelBuilder.Entity<VwBiCrmCashbackLtv>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_BI_CRM_CashbackLTV");

            entity.Property(e => e.CashbackLtv)
                .HasColumnType("numeric(38, 2)")
                .HasColumnName("CashbackLTV");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(10)
                .HasColumnName("ProjectID");
            entity.Property(e => e.Yyyymm)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("YYYYMM");
        });

        modelBuilder.Entity<VwBiProject>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_BI_Project");

            entity.Property(e => e.Md)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MD");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.ProjectName).HasMaxLength(500);
            entity.Property(e => e.ProjectStatus).HasMaxLength(200);
        });

        modelBuilder.Entity<VwBiProjectInitialMonth>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_BI_Project_Initial_Month");

            entity.Property(e => e.LastMonth)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Md)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MD");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.ProjectName).HasMaxLength(500);
            entity.Property(e => e.ProjectStatus).HasMaxLength(200);
            entity.Property(e => e.Yyyymm)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("YYYYMM");
        });

        modelBuilder.Entity<VwBiTransferActual>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_BI_Transfer_Actual");

            entity.Property(e => e.ActualUnit).HasColumnName("Actual_Unit");
            entity.Property(e => e.ActualValue)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("Actual_Value");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.Yyyymm)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("YYYYMM");
        });

        modelBuilder.Entity<VwBiTransferNetActual>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_BI_Transfer_NetActual");

            entity.Property(e => e.ActualUnit).HasColumnName("Actual_Unit");
            entity.Property(e => e.ActualValue)
                .HasColumnType("numeric(38, 2)")
                .HasColumnName("Actual_Value");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.Yyyymm)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("YYYYMM");
        });

        modelBuilder.Entity<VwBiTransferTargetRolling>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_BI_Transfer_TargetRolling");

            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.RollingUnit)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("Rolling_Unit");
            entity.Property(e => e.RollingValue)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("Rolling_Value");
            entity.Property(e => e.TargetUnit)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("Target_Unit");
            entity.Property(e => e.TargetValue)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("Target_Value");
            entity.Property(e => e.Yyyymm)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("YYYYMM");
        });

        modelBuilder.Entity<VwBiTransferTargetRollingActual>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_BI_Transfer_TargetRollingActual");

            entity.Property(e => e.AccumActualUnit).HasColumnName("AccumActual_Unit");
            entity.Property(e => e.AccumActualValue)
                .HasColumnType("numeric(38, 2)")
                .HasColumnName("AccumActual_Value");
            entity.Property(e => e.AccumRollingBaseUnit)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("AccumRollingBase_Unit");
            entity.Property(e => e.AccumRollingBaseValue)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("AccumRollingBase_Value");
            entity.Property(e => e.AccumTargetUnit)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("AccumTarget_Unit");
            entity.Property(e => e.AccumTargetValue)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("AccumTarget_Value");
            entity.Property(e => e.ActualUnit).HasColumnName("Actual_Unit");
            entity.Property(e => e.ActualValue)
                .HasColumnType("numeric(38, 2)")
                .HasColumnName("Actual_Value");
            entity.Property(e => e.Md)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MD");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.ProjectName).HasMaxLength(500);
            entity.Property(e => e.ProjectStatus).HasMaxLength(200);
            entity.Property(e => e.RollingUnit)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("Rolling_Unit");
            entity.Property(e => e.RollingValue)
                .HasColumnType("numeric(38, 2)")
                .HasColumnName("Rolling_Value");
            entity.Property(e => e.TargetUnit)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("Target_Unit");
            entity.Property(e => e.TargetValue)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("Target_Value");
        });

        modelBuilder.Entity<VwContractBankAccount>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_Contract_BankAccount");

            entity.Property(e => e.BankCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BankId)
                .HasMaxLength(50)
                .HasColumnName("BankID");
            entity.Property(e => e.ContractId)
                .HasMaxLength(20)
                .HasColumnName("ContractID");
            entity.Property(e => e.ContractNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CustomerBookBank)
                .HasMaxLength(8000)
                .IsUnicode(false);
            entity.Property(e => e.CustomerId)
                .HasMaxLength(50)
                .HasColumnName("CustomerID");
        });

        modelBuilder.Entity<VwDefect>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_Defect");

            entity.Property(e => e.AreaName).HasMaxLength(1000);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DefectAreaId).HasColumnName("DefectAreaID");
            entity.Property(e => e.DefectDescriptionId).HasColumnName("DefectDescriptionID");
            entity.Property(e => e.DefectTypeId).HasColumnName("DefectTypeID");
            entity.Property(e => e.DescName).HasMaxLength(1000);
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.QctypeId).HasColumnName("QCTypeID");
            entity.Property(e => e.TypeName).HasMaxLength(1000);
        });

        modelBuilder.Entity<VwGetRandvalue>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_getRANDValue");
        });

        modelBuilder.Entity<VwItfTempBlakUnit>(entity =>
        {
            entity.ToTable("vw_ITF_TempBlakUnit");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.Agrdate)
                .HasColumnType("datetime")
                .HasColumnName("AGRDATE");
            entity.Property(e => e.Bookdate)
                .HasColumnType("datetime")
                .HasColumnName("BOOKDATE");
            entity.Property(e => e.Bookdate2)
                .HasColumnType("datetime")
                .HasColumnName("BOOKDATE2");
            entity.Property(e => e.BuiltInAmt).HasColumnType("numeric(38, 2)");
            entity.Property(e => e.BuiltInLand).HasColumnType("numeric(38, 2)");
            entity.Property(e => e.CusId)
                .HasMaxLength(50)
                .HasColumnName("CusID");
            entity.Property(e => e.CustomerEmail)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Factdate)
                .HasColumnType("datetime")
                .HasColumnName("FACTDATE");
            entity.Property(e => e.Fadd1)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("FADD1");
            entity.Property(e => e.Fadd2)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("FADD2");
            entity.Property(e => e.Fadd3)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("FADD3");
            entity.Property(e => e.Faddrno)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FADDRNO");
            entity.Property(e => e.Fadisc)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("FADISC");
            entity.Property(e => e.Fadiscks)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("FADISCKS");
            entity.Property(e => e.Fagrfrom)
                .HasColumnType("datetime")
                .HasColumnName("FAGRFROM");
            entity.Property(e => e.Fagrno)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FAGRNO");
            entity.Property(e => e.Fagrterms).HasColumnName("FAGRTERMS");
            entity.Property(e => e.Fagrto)
                .HasColumnType("datetime")
                .HasColumnName("FAGRTO");
            entity.Property(e => e.Farea)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("FAREA");
            entity.Property(e => e.Fascostamt)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("FASCOSTAMT");
            entity.Property(e => e.Fbookno)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FBOOKNO");
            entity.Property(e => e.Fcat1)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.Fconafdate).HasColumnName("fconafdate");
            entity.Property(e => e.Fconcdate).HasColumnName("fconcdate");
            entity.Property(e => e.Fconfdate).HasColumnName("fconfdate");
            entity.Property(e => e.Fconsee)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("fconsee");
            entity.Property(e => e.Fcontcode)
                .HasMaxLength(50)
                .HasColumnName("FCONTCODE");
            entity.Property(e => e.Fconttnm).HasColumnName("FCONTTNM");
            entity.Property(e => e.Fconudate).HasColumnName("fconudate");
            entity.Property(e => e.Fcrdamt)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("FCRDAMT");
            entity.Property(e => e.Fcrdapamt)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("FCRDAPAMT");
            entity.Property(e => e.Fcrdapdt)
                .HasColumnType("datetime")
                .HasColumnName("FCRDAPDT");
            entity.Property(e => e.Fcrdbank)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FCRDBANK");
            entity.Property(e => e.Fcrddate)
                .HasColumnType("datetime")
                .HasColumnName("FCRDDATE");
            entity.Property(e => e.Fcucode)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FCUCODE");
            entity.Property(e => e.Fcuname)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("FCUNAME");
            entity.Property(e => e.Fdocstatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("FDOCSTATUS");
            entity.Property(e => e.Fduedate)
                .HasColumnType("datetime")
                .HasColumnName("FDUEDATE");
            entity.Property(e => e.Ffindate)
                .HasColumnType("datetime")
                .HasColumnName("FFINDATE");
            entity.Property(e => e.Fnrfamt)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("FNRFAMT");
            entity.Property(e => e.Fpackage)
                .HasMaxLength(50)
                .HasColumnName("FPACKAGE");
            entity.Property(e => e.Fpdcode)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FPDCODE");
            entity.Property(e => e.Fpostal)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("FPOSTAL");
            entity.Property(e => e.Fprovince)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("FPROVINCE");
            entity.Property(e => e.Fqty)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("FQTY");
            entity.Property(e => e.Freblock)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("FREBLOCK");
            entity.Property(e => e.Frecdate)
                .HasColumnType("datetime")
                .HasColumnName("FRECDATE");
            entity.Property(e => e.Frecrscd)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FRECRSCD");
            entity.Property(e => e.Frecrscd2).HasColumnName("FRECRSCD2");
            entity.Property(e => e.FreeAll).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Frental)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("FRENTAL");
            entity.Property(e => e.Frephase)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("FREPHASE");
            entity.Property(e => e.Freprjnm)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FREPRJNM");
            entity.Property(e => e.Freprjnm2)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Freprjno)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FREPRJNO");
            entity.Property(e => e.Frestatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("FRESTATUS");
            entity.Property(e => e.Frfdamt)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("FRFDAMT");
            entity.Property(e => e.Fserialno)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FSERIALNO");
            entity.Property(e => e.Fsmname)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("FSMNAME");
            entity.Property(e => e.Fsuname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FSUNAME");
            entity.Property(e => e.Ftel)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FTEL");
            entity.Property(e => e.Ftelno)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("FTELNO");
            entity.Property(e => e.Ftoverarea)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("FTOVERAREA");
            entity.Property(e => e.Ftprice)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("FTPRICE");
            entity.Property(e => e.Ftprice2).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.Ftyunit)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FTYUNIT");
            entity.Property(e => e.Fupdflag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("FUPDFLAG");
            entity.Property(e => e.Fwhoass).HasColumnName("FWHOASS");
            entity.Property(e => e.OverDueAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Pfreprjnosap)
                .HasMaxLength(50)
                .HasColumnName("PFREPRJNOSAP");
            entity.Property(e => e.Pksagrno)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Pkscchamt).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.Pkscostamt).HasColumnType("numeric(19, 2)");
            entity.Property(e => e.Pksprice).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.PlanQc5date).HasColumnName("PlanQC5Date");
            entity.Property(e => e.Psubphase)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PSubphase");
            entity.Property(e => e.Transdate)
                .HasColumnType("datetime")
                .HasColumnName("TRANSDATE");
            entity.Property(e => e.TransferNetPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Zfagrdateks).HasColumnType("datetime");
            entity.Property(e => e.Zfmdateks).HasColumnType("datetime");
        });

        modelBuilder.Entity<VwUnit>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_Units");

            entity.Property(e => e.Build).HasMaxLength(50);
            entity.Property(e => e.Floor).HasMaxLength(50);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(15)
                .HasColumnName("ProjectID");
            entity.Property(e => e.UnitCode).HasMaxLength(20);
        });

        modelBuilder.Entity<VwUnitMatrix>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_UnitMatrix");

            entity.Property(e => e.AppointDate).HasColumnType("datetime");
            entity.Property(e => e.Bgcolor)
                .IsUnicode(false)
                .HasColumnName("BGColor");
            entity.Property(e => e.Build)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CustomerName).HasMaxLength(500);
            entity.Property(e => e.ExpectTransfer)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ExpectTransferById).HasColumnName("ExpectTransferByID");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.InspectStatus).IsUnicode(false);
            entity.Property(e => e.LastInspectDate).HasColumnType("datetime");
            entity.Property(e => e.MatrixTypeId).HasColumnName("MatrixTypeID");
            entity.Property(e => e.MatrixTypeName).HasMaxLength(200);
            entity.Property(e => e.Prefix)
                .HasMaxLength(67)
                .IsUnicode(false);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.Qc5FinishDate)
                .HasColumnType("datetime")
                .HasColumnName("QC5_FinishDate");
            entity.Property(e => e.ReceiveRoomDate).HasColumnType("datetime");
            entity.Property(e => e.SellingPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TagTransfer)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.TransferDate).HasColumnType("datetime");
            entity.Property(e => e.UnitCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UnitStatusCs).HasColumnName("UnitStatus_CS");
        });

        modelBuilder.Entity<VwUnitMatrixQcprogress>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_UnitMatrix_QCProgress");

            entity.Property(e => e.Bgcolor)
                .IsUnicode(false)
                .HasColumnName("BGColor");
            entity.Property(e => e.Build)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CustomerName).HasMaxLength(500);
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.LastQc5date)
                .HasColumnType("datetime")
                .HasColumnName("LastQC5Date");
            entity.Property(e => e.LastQc6date)
                .HasColumnType("datetime")
                .HasColumnName("LastQC6Date");
            entity.Property(e => e.MatrixTypeId).HasColumnName("MatrixTypeID");
            entity.Property(e => e.MatrixTypeName).HasMaxLength(200);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.Qc5FinishDate)
                .HasColumnType("datetime")
                .HasColumnName("QC5_FinishDate");
            entity.Property(e => e.ReceiveRoomDate).HasColumnType("datetime");
            entity.Property(e => e.TransferDate).HasColumnType("datetime");
            entity.Property(e => e.UnitCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UnitStatusCs).HasColumnName("UnitStatus_CS");
            entity.Property(e => e.VerifyQc5CheckLis).HasColumnName("Verify_QC5_CheckLis");
        });

        modelBuilder.Entity<VwUnitMatrixQcprogressV2>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_UnitMatrix_QCProgress_V2");

            entity.Property(e => e.Bgcolor)
                .IsUnicode(false)
                .HasColumnName("BGColor");
            entity.Property(e => e.Build)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CustomerName).HasMaxLength(500);
            entity.Property(e => e.FinishPlanDate).HasColumnType("datetime");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.LastQc5date)
                .HasColumnType("datetime")
                .HasColumnName("LastQC5Date");
            entity.Property(e => e.LastQc5status)
                .HasMaxLength(200)
                .HasColumnName("LastQC5Status");
            entity.Property(e => e.LastQc6date)
                .HasColumnType("datetime")
                .HasColumnName("LastQC6Date");
            entity.Property(e => e.MatrixTypeId).HasColumnName("MatrixTypeID");
            entity.Property(e => e.MatrixTypeName).HasMaxLength(200);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.ProjectType)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Qc5CheckListScore)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("QC5_CheckList_Score");
            entity.Property(e => e.Qc5FinishDate)
                .HasColumnType("datetime")
                .HasColumnName("QC5_FinishDate");
            entity.Property(e => e.ReceiveRoomDate).HasColumnType("datetime");
            entity.Property(e => e.SyncTypeId).HasColumnName("SyncTypeID");
            entity.Property(e => e.TransferDate).HasColumnType("datetime");
            entity.Property(e => e.UnitCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UnitStatusCs).HasColumnName("UnitStatus_CS");
            entity.Property(e => e.VerifyQc5CheckLis).HasColumnName("Verify_QC5_CheckLis");
        });

        modelBuilder.Entity<ZImportUnit>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Z_importUnit");

            entity.Property(e => e.Area).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Build)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.CustomerEmail)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CustomerMobile)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CustomerName).HasMaxLength(500);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.UnitCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UnitType)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ZM1111>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Z_M1111");

            entity.Property(e => e.Faddrno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FADDRNO");
            entity.Property(e => e.Fcuname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FCUNAME");
            entity.Property(e => e.Fserialno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FSERIALNO");
            entity.Property(e => e.Ftel)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FTEL");
            entity.Property(e => e.Id).HasColumnName("id");
        });

        modelBuilder.Entity<ZW1111>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Z_W1111");

            entity.Property(e => e.Area).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Build)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.CustomerMobile)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CustomerName).HasMaxLength(500);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ProjectID");
            entity.Property(e => e.UnitCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UnitType)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
