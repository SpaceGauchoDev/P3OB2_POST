using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio.Entidades;

namespace MVC_Presentacion 
{
    public class CIValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string pCi = value.ToString();
            Solicitante s = new Solicitante();
            return s.ValidarCI(pCi);
        }
    }
}