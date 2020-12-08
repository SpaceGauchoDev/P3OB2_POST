using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCF_Importacion
{
    [ServiceContract]
    public interface IServicioImportacion
    {
        //TODOMDA: ObtenerSolicitantes debería recibir por parametro desde el MVC una
        // url que le va a pasar al repositorio donde se va a levantar el archivo para
        // importar los solicitantes
        //11-18-01-Proyecto-MVC-WCF-WebApi - 14:07

        [OperationContract]
        IEnumerable<DtoSolicitante> ImportarSolicitantes();

        [OperationContract]
        IEnumerable<DtoProyecto> ImportarProyectos();

        /*
        //TESTING
        [OperationContract]
        public IEnumerable<DtoSolicitante> ImportarSolicitantes();
        */
    }

    
    // si este DTO se modifica y está siendo usado por MVC para scaffoldear views, hay que actualizar la referencia al servicio
    // y rescaffoldear las views
    // 1 - expandir Connected Services en MVC
    // 2 - borrar el servicio relacionado con este DTO, recordar el nombre
    // 3 - click derecho en Connected Services generar una nueva referencia con el nombre del paso 2
    // 4 - borrar la view afectada
    // 5 - en el controlador apropiado, generar una nueva view y usar este dto como modelo

    [DataContract]
    public class DtoSolicitante
    {
        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string Apellido { get; set; }

        [DataMember]
        public DateTime FechaDeNacimiento { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Cell { get; set; }
    }

    [DataContract]
    public class DtoProyecto
    {
        [DataMember]
        public string Estado { get; set; }

        [DataMember]
        public string TipoDeEquipo { get; set; }

        [DataMember]
        public string Titulo { get; set; }

        [DataMember]
        public string Descripcion { get; set; }

        [DataMember]
        public string ImgURL { get; set; }

        [DataMember]
        public DateTime FechaDePresentacion { get; set; }

        [DataMember]
        public int CantidadDeIntegrantes { get; set; }

        [DataMember]
        public string ExperienciaPersonal { get; set; }

        [DataMember]
        public int Cuotas { get; set; }

        [DataMember]
        public decimal PrecioPorCuota { get; set; }

        [DataMember]
        public decimal MontoSolicitado { get; set; }

        [DataMember]
        public decimal PorcentajeDeInteres { get; set; }

        [DataMember]
        public decimal MontoConseguido { get; set; }

        [DataMember]
        public int IdSolicitante { get; set; }
    }





}
