using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectExpenseControl.Models
{
    public class Invoice
    {
        [Key]
        [Required]
        [Display(Name = "CÓDIGO DE FACTURA")]
        public string INV_ID_INVOICE { get; set; }

        [Display(Name = "SERIE")]
        [StringLength(10)]
        public string INV_DES_SERIE { get; set; }

        [Display(Name = "FOLIO")]
        [StringLength(40)]
        public string INV_DES_FOLIO { get; set; }

        [Required]
        [Display(Name = "Fecha de creación")]
        public DateTime INV_FH_FECHA { get; set; }

        [Required]
        [Display(Name = "MONTO TOTAL")]
        public Decimal INV_DES_TOTAL { get; set; }

        [Required]
        [Display(Name = "LUGAR DE EXPEDICIÓN")]
        [StringLength(6)]
        public string INV_DES_LUGAR_EXPEDICION { get; set; }
        [Required]
        [Display(Name = "EMISOR RFC")]
        [StringLength(13)]
        public string INV_DES_EMISOR_RFC { get; set; }

        [Required]
        [Display(Name = "NOMBRE DE EMISOR")]
        [StringLength(254)]
        [DataType(DataType.MultilineText)]
        public string INV_DES_EMISOR_NOMBRE { get; set; }

        [Required]
        [Display(Name = "UUID")]
        [StringLength(70)]
        public string INV_DES_UUID { get; set; }
    }
}