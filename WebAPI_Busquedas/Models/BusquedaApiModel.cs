using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI_Busquedas.Models
{
    public class BusquedaApiModel
    {
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string ContieneEnTitulo { get; set; }
        public string ContieneEnDescripcion { get; set; }
        public string Estado { get; set; }
        public decimal? MontoMaximo { get; set; }
        public int? CISolicitante { get; set; }
        public string TipoDeBusqueda { get; set; }
    }
}