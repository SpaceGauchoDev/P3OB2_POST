using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC_Presentacion.Models
{
    public class CambiarPassModel
    {
        [Required(ErrorMessage = "Ingrese contraseña!")]
        [Display(Name = "Contraseña: ")]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[0-1])(?=.*[a-z])(?=.*[A-Z]).{6,}$", ErrorMessage = "Contraseña debe tener como mínimo 1 mayúscula, 1 minúscula, 1 número y 6 caracateres de largo.")]
        public string Pass { get; set; }

        [Required(ErrorMessage = "Confirme contraseña!")]
        [Display(Name = "Confirme contraseña: ")]
        [DataType(DataType.Password)]
        [Compare("Pass", ErrorMessage = "Las contraseñas ingresadas no coinciden.")]
        public string ConPass { get; set; }
    }
}