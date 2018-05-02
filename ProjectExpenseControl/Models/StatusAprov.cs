using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectExpenseControl.Models
{
    public class StatusAprov
    {
        [Key]
        [Required]
        [Display(Name = "CÓDIGO DE FACTURA")]
        //[Range(1,1000, ErrorMessage = "El campo {0} debe ser entre {1} y {2}")]
        public int STA_IDE_STATUS_APROV { get; set; }

        [Required]
        [Display(Name = "DESCRPCIÓN ESTADO")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "El rango del campo {0} es de {2} a {1}")]
        public string STA_DES_STATUS { get; set; }
        

    }
}