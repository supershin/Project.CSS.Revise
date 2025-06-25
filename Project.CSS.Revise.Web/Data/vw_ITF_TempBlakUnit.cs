using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.CSS.Revise.Web.Data;

[Table("vw_ITF_TempBlakUnit")]
public partial class vw_ITF_TempBlakUnit
{
    [Key]
    public Guid ID { get; set; }

    [StringLength(50)]
    public string? PFREPRJNOSAP { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? FREPRJNM { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string? Fcat1 { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? FREPRJNO { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? FSERIALNO { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? FPDCODE { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? FAGRNO { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? FTYUNIT { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FFINDATE { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FRECDATE { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FDUEDATE { get; set; }

    public int? fconcdate { get; set; }

    public int? fconfdate { get; set; }

    public int? fconafdate { get; set; }

    public int? fconudate { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? FTPRICE { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal Ftprice2 { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal FASCOSTAMT { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? Pkscchamt { get; set; }

    [Column(TypeName = "numeric(19, 2)")]
    public decimal? Pkscostamt { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal Pksprice { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string FRESTATUS { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? BOOKDATE { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AGRDATE { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? TRANSDATE { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? FBOOKNO { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? BOOKDATE2 { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? FADDRNO { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? FCUCODE { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? FCUNAME { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? FTEL { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? FRECRSCD { get; set; }

    public int? FAGRTERMS { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? FRENTAL { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? FNRFAMT { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? FRFDAMT { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? FSMNAME { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FACTDATE { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? FCRDBANK { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? FCRDAMT { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? FCRDAPAMT { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FCRDAPDT { get; set; }

    public int? FWHOASS { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? FSUNAME { get; set; }

    [Column(TypeName = "decimal(38, 2)")]
    public decimal? FQTY { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? FAREA { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FAGRFROM { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FAGRTO { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Pksagrno { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? FADD1 { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? FADD2 { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? FADD3 { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? FPROVINCE { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string? FPOSTAL { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? FTELNO { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? FREPHASE { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? PSubphase { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? fconsee { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? FREBLOCK { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Zfagrdateks { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Zfmdateks { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? FDOCSTATUS { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? FUPDFLAG { get; set; }

    public int? FRECRSCD2 { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? FADISC { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? FADISCKS { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal? FTOVERAREA { get; set; }

    [StringLength(50)]
    public string? FCONTCODE { get; set; }

    public string? FCONTTNM { get; set; }

    [StringLength(50)]
    public string? FPACKAGE { get; set; }

    public int? PlanQC5Date { get; set; }

    [StringLength(1000)]
    [Unicode(false)]
    public string? Freprjnm2 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FCRDDATE { get; set; }

    [StringLength(50)]
    public string? CusID { get; set; }

    [Column(TypeName = "numeric(38, 2)")]
    public decimal? BuiltInLand { get; set; }

    [Column(TypeName = "numeric(38, 2)")]
    public decimal? BuiltInAmt { get; set; }

    [StringLength(1000)]
    [Unicode(false)]
    public string? CustomerEmail { get; set; }

    public int? OverDuePeriod { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? OverDueAmount { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? FreeAll { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TransferNetPrice { get; set; }
}
