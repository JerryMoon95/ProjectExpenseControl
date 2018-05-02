using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectExpenseControl.Models
{
    public class AccountingAccount
    {
        [Key]
        [StringLength(6)]
        [Required]
        [Display(Name = "CÓDIGO DE CUENTA")]
        public string ACC_IDE_ACCOUNT { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(70, MinimumLength = 3, ErrorMessage = "El campo {0} debe tener una longitud máxima de {1} y mínma de {2}")]
        [Display(Name = "DESCRIPCIÓN DE CUENTA")]
        public string ACC_DES_ACCOUNT { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "FECHA DE CREACIÓN")]
        [Required]
        public DateTime ACC_FH_CREATED { get; set; }
    }
}