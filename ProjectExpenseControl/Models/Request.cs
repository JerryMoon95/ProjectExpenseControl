using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectExpenseControl.Models
{
    public class Request
    {
        [Key]
        [Required]
        [Display(Name = "CÓDIGO DE SOLICITUD")]
        public int REQ_IDE_REQUEST { get; set; }

        [Required]
        [Display(Name = "USUARIO QUE HIZO LA SOLICITUD")]
        public int REQ_IDE_USER { get; set; }

        [Required]
        [Display(Name = "ÁREA")]
        [StringLength(10)]
        //[NotMapped]//Se maneja en el servidor pero no en DB
        //[HiddenInput(DisplayValue = false)]
        public string REQ_IDE_AREA { get; set; }

        [Required]
        [Display(Name = "TIPO DE GASTO")]
        public Boolean REQ_DES_TYPE_GASTO { get; set; }

        [Display(Name = "CONCEPTO")]
        [StringLength(100)]
        public string REQ_DES_CONCEPT { get; set; }

        [Required]
        [Display(Name = "CANTIDAD")]
        [Range(0.0,Double.MaxValue, ErrorMessage ="El rango de cantidad del campo {0} es de {1} a {2}")]
        public Decimal REQ_DES_QUANTITY { get; set; }

        [Display(Name = "OBSERVACIONES")]
        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        public string REQ_DES_OBSERVATIONS { get; set; }

        [Display(Name = "ESTADO DE LA APROVACIÓN")]
        public int REQ_IDE_STATUS_APROV { get; set; }

        [Required]
        [Display(Name = "FECHA ME EMISIÓN")]
        public DateTime REQ_FH_CREATED { get; set; }
    }
}