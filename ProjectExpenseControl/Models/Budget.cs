using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectExpenseControl.Models
{
    public class Budget
    {
        [Key]
        [Required]
        [Display(Name = "CÓDIGO DE FACTURA")]
        public int BUD_IDE_BUDGET { get; set; }

        [Required]
        [Display(Name = "USUARIO")]
        [HiddenInput]
        public int BUD_IDE_USER { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "CUENTA MAYOR")]
        [StringLength(6, MinimumLength = 3, ErrorMessage = "El rango del campo {0} es entre {2} y {1}")]
        public string BUD_IDE_ACCOUNT { get; set; }

        [Required]
        [Display(Name = "ÁREA")]
        [StringLength(10)]
        public string BUD_IDE_AREA { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio...")]
        [Display(Name = "CANTIDAD")]
        [Range(0.0,1000000, ErrorMessage = "La cantidad debe ser entre {1} y {2}.")]
        public Decimal BUD_DES_QUANTITY { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio...")]
        [Display(Name = "PERIODO")]
        [StringLength(12)]
        public string BUD_DES_PERIOD { get; set; }

        [Required]
        [Display(Name = "FECHA DE CREACIÓN")]
        public DateTime BUD_FH_CREATED { get; set; }


    }
}