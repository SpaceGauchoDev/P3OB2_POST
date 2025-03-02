﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Presentacion.Models
{
    public class ItemResultadoBusquedaProyectoModel
    {
        public string Estado { get; set; }
        public string TipoDeEquipo { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string ImgURL { get; set; }
        public DateTime FechaDePresentacion { get; set; }
        public int CantidadDeIntegrantes { get; set; }
        public string ExperienciaPersonal { get; set; }
        public int Cuotas { get; set; }
        public decimal PrecioPorCuota { get; set; }
        public decimal MontoSolicitado { get; set; }
        public decimal PorcentajeDeInteres { get; set; }
        public decimal MontoConseguido { get; set; }
        public int IdSolicitante { get; set; }
    }
}