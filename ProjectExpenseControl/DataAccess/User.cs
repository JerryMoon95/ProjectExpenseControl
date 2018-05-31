using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectExpenseControl.DataAccess
{
    public class User
    {

        [Key]
        [Display(Name = "ID DE USUARIO")]
        public int USR_IDE_USER { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "ÁREA")]
        [StringLength(100)]
        public string USR_IDE_AREA { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "PUESTO")]
        [StringLength(60)]
        public string USR_DES_POSITION { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "NOMBRE")]
        [StringLength(100)]
        public string USR_DES_NAME { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "APELLIDO PATERNO")]
        [StringLength(60)]
        public string USR_DES_FIRST_NAME { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "APELLIDO MATERNO")]
        [StringLength(60)]
        public string USR_DES_LAST_NAME { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.Password)]
        [Display(Name = "CONTRASEÑA")]
        [StringLength(50)]
        public string USR_DES_PASSWORD { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.Password)]
        [NotMapped]
        [Display(Name = "CONFIRMAR CONTRASEÑA")]
        [Compare("USR_DES_PASSWORD", ErrorMessage = "Error : la contraseña no es igual.")]
        public string USR_DES_PASSWORD_CONFIRMATION { get; set; }


        [Display(Name = "TELÉFONO")]
        [StringLength(20)]
        public string USR_DES_PHONE { get; set; }
        
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(40)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "CORREO")]
        public string USR_DES_EMAIL { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        [Display(Name = "FECHA DE CREACIÓN")]
        public DateTime USR_FH_CREATED { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "ÚLTIMO INGRESO")]
        public DateTime USR_FH_LAST_LOGIN { get; set; }

        public virtual ICollection<Role> Roles { get; set; }

    }
}