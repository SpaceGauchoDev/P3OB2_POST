using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC_Presentacion.Models
{
    public class LoginDataModel
    {
        [Required(ErrorMessage = "Ingrese cedula de identidad!")]
        [Display(Name = "CI: ")]
        public string NombreDeUsuario { get; set; }
        

        [Required(ErrorMessage = "Ingrese contraseña!")]
        [Display(Name = "Contraseña: ")]
        [DataType(DataType.Password)]
        public string Pass { get; set; }
    }
}