using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Dominio.Entidades;

namespace MVC_Presentacion.Models
{
    public class InversorRegistrationModel
    {
        [Required(ErrorMessage = "Ingrese cedula de identidad!")]
        [Display(Name = "CI: ")]
        [CIValidation(ErrorMessage = "Ingrese CI con número verificador sin puntos ni guiónes.")]
        public string NombreDeUsuario { get; set; }

        [Required(ErrorMessage = "Ingrese nombre!")]
        [Display(Name = "Nombre: ")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese apellido!")]
        [Display(Name = "Apellido: ")]
        public string Apellido { get; set; }

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

        [Required(ErrorMessage = "Ingrese fecha de nacimiento!")]
        [DataType(DataType.DateTime)]
        public DateTime FechaDeNacimiento { get; set; }

        [Required(ErrorMessage = "Ingrese email!")]
        [Display(Name = "Email: ")]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage = "Formato de Email inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ingrese numero de celular!")]
        [Display(Name = "Celular: ")]
        [CellValidation(ErrorMessage = "Celular debe ser solo números, empezar con 09 y tener 9 digitos.")]
        public string Cell { get; set; }

        [Required(ErrorMessage = "Ingrese monto máximo a inversion por proyecto!")]
        [Display(Name = "Monto Maximo: ")]
        [Range(0, (double)decimal.MaxValue)]
        public decimal MaxInvPorProyecto { get; set; }

        // TODOMDA: obtener el largo maximo de la presentacion de la clase inversor en el dominio
        [Required(ErrorMessage = "Ingrese un texto de presentacion como inversor!")]
        [Display(Name = "Presentacion: ")]
        [StringLength(maximumLength:500, MinimumLength = 10, ErrorMessage = "La presentacion debe estar entre 10 y 500 caracteres")]
        public string PresentacionInversor { get; set; } // max 500 chars
    }
}