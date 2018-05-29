using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectExpenseControl.Models
{
    public class LoginView
    {
        [Required]
        [Display(Name = "CORREO")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [Display(Name = "CONTRASEÑA")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "RECORDARME")]
        public bool RememberMe { get; set; }
    }

    public class CustomSerializeModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> RoleName { get; set; }

    }

    public class RegistrationView
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "NOMBRE")]
        public string Username { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "APELLIDO PATERNO")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "APELLIDO MATERNO")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "CORREO")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "ÁREA")]
        public string Area { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "PUESTO")]
        public string Position { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "TELÉFONO")]
        public string Phone { get; set; }

        [Required]
        public Guid ActivationCode { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.Password)]
        [Display(Name = "CONTRASEÑA")]
        public string Password { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.Password)]
        [Display(Name = "CONFIRMAR CONTRASEÑA")]
        //[Compare("password", ErrorMessage = "Error : la contraseña no es igual.")]
        public string ConfirmPassword { get; set; }

        public IEnumerable<Area> CmbList1 { get; set; }
    }
}