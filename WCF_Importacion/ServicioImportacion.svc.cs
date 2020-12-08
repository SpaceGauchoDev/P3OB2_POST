using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Datos;
using Datos.Repositorios;
using Dominio.Entidades;
using System.IO;


namespace WCF_Importacion
{
    public class ServicioImportacion : IServicioImportacion
    {
        private RepositorioSolicitante rS = new RepositorioSolicitante();
        private RepositorioInversor rI = new RepositorioInversor();
        private RepositorioProyecto rP = new RepositorioProyecto();

        //TODOMDA: esto esta feo, lo mejor sería capturar el path desde el boton del MVC con un file upload, 
        // o mejor recibir el string entero desde el MVC como parametro de ImportarSolicitantes() y no tener que leer el txt aca
        private string pathAUsuarios = "D:\\MyStuff\\ORT\\AP\\S50\\P3\\P3OB2\\Prestamos-P2P\\Datos\\Archivos\\usuarios.txt";
        private string pathAProyectos = "D:\\MyStuff\\ORT\\AP\\S50\\P3\\P3OB2\\Prestamos-P2P\\Datos\\Archivos\\proyectos.txt";
        private string pathAImagenes = "D:\\MyStuff\\ORT\\AP\\S50\\P3\\P3OB2\\Prestamos-P2P\\MVC_Presentacion\\img_proy2\\";


        public IEnumerable<DtoSolicitante> ImportarSolicitantes()
        {
            try
            {
                using (StreamReader sr = new StreamReader(pathAUsuarios))
                {
                    string linea = sr.ReadLine();
                    while (linea != null)
                    {
                        var lineaVec = linea.Split("|".ToCharArray());
                        // solo importamos usuarios solicitantes, que no compartan id con un inversor, que no compartan email con un inversor
                        if (lineaVec[2].ToString() == "S" 
                            && rI.FindById(int.Parse(lineaVec[0])) == null  
                            && !rI.ExistsByEmail (lineaVec[6].ToString()))
                        {
                            Solicitante sol = new Solicitante
                            {
                                // ejemplo de linea en el txt
                                // 31832575|Aa1234|S|Gordon|Gordoñez|1991-02-17 12:00:00 AM|gordon@gmail.com|099879997
                                IdUsuario = int.Parse(lineaVec[0]),
                                Pass = PassTemporal(lineaVec[3].ToString(), lineaVec[4].ToString(), lineaVec[0].ToString()),
                                Nombre = lineaVec[3].ToString(),
                                Apellido = lineaVec[4].ToString(),
                                FechaDeNacimiento = ConvertirAFecha(lineaVec[5].ToString()),
                                Email = lineaVec[6].ToString(),
                                Cell = lineaVec[7].ToString(),
                                TienePassTemporal = true
                            };

                            rS.Add(sol);
                        }
                        linea = sr.ReadLine();
                    }
                }
                return ObtenerTodosLosSolicitantes();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return ObtenerTodosLosSolicitantes();
            }
        }


        private DateTime ConvertirAFecha(string fechaString)
        {
            int anio = int.Parse(fechaString.Substring(0, 4));
            int mes = int.Parse(fechaString.Substring(5, 2));
            int dia = int.Parse(fechaString.Substring(8, 2));
            DateTime fechaDate = new DateTime(anio, mes, dia);

            return fechaDate;
        }

        private string PassTemporal(string nombre, string apellido, string CI)
        {
            /*
             Como la contraseña anterior no se lee, le será asignada automáticamente unanueva formada
             por la inicial de su nombre en minúscula, la inicial de su apellido en mayúscula y los dígitos de su cédula). 
             Por ejemplo, si su nombre es Antonia Gurméndezy tiene cédula 12345678 le asignaremos el string aG12345678 como nueva contraseña.
             */
            string pass = nombre.Substring(0, 1).ToLower();
            pass += apellido.Substring(0, 1).ToUpper();
            pass += CI;

            return pass;
        }

        private IEnumerable<DtoSolicitante> ObtenerTodosLosSolicitantes()
        {
            List<Solicitante> solicitantes = rS.FindAll().ToList();
            List<DtoSolicitante> listaDeDTOSolicitantes = new List<DtoSolicitante>();

            foreach (Solicitante s in solicitantes) {
                DtoSolicitante dtoSolicitante = new DtoSolicitante
                {
                    Nombre = s.Nombre,
                    Apellido = s.Apellido,
                    FechaDeNacimiento = s.FechaDeNacimiento,
                    Email = s.Email,
                    Cell = s.Cell
                };
                listaDeDTOSolicitantes.Add(dtoSolicitante); 
            }

            return listaDeDTOSolicitantes;
        }

        public IEnumerable<DtoProyecto> ImportarProyectos()
        {
            try
            {
                using (StreamReader sr = new StreamReader(pathAProyectos))
                {
                    string linea = sr.ReadLine();
                    while (linea != null)
                    {
                        var lineaVec = linea.Split("|".ToCharArray());
                        // solo importamos proyectos aprobados, que hayan sido presentados por usuarios solicitantes previamente registrados
                        if (lineaVec[1].ToString() == "A" && rS.FindById(int.Parse(lineaVec[9])) != null)
                        {
                            Proyecto pro = new Proyecto
                            {
                                // ejemplo de linea en el txt
                                // 1|P|I|Baubax Travel Jacket|TRAVEL JACKET with built-in Neck Pillow, Eye Mask, Gloves, Earphone Holders, Drink Pocket, Tech Pockets of all sizes! Comes in 4 Styles|baubaxTravelJacket.jpg|1|bla bla bla|2016-06-15 12:00:00 AM|57560340|10|545.00|5000.00|0.0900

                                IdProyecto = int.Parse(lineaVec[0]),
                                Estado = "A", // todos los proyectos se importan como estado Abierto inicialmente
                                TipoDeEquipo = (lineaVec[2].ToString() == "I") ? "P" : "C", // I = individual proyecto anterior, analogo a personal en este proycto
                                Titulo = lineaVec[3].ToString(),
                                Descripcion = lineaVec[4].ToString(),
                                ImgURL = lineaVec[5].ToString(),
                                CantidadDeIntegrantes = int.Parse(lineaVec[6]),
                                ExperienciaPersonal = lineaVec[7].ToString(),
                                FechaDePresentacion = ConvertirAFecha(lineaVec[8]),
                                IdSolicitante = int.Parse(lineaVec[9]),
                                //TODOMDA: verificar que los ultimos valores estan siendo cargados correctamente en el DTO, creo que aparecen en desorden 
                                // en la vista de proyectos
                                Cuotas = int.Parse(lineaVec[10]),
                                PrecioPorCuota = decimal.Parse(lineaVec[11]),
                                MontoSolicitado = decimal.Parse(lineaVec[12]),
                                PorcentajeDeInteres = decimal.Parse(lineaVec[13]),
                                MontoConseguido = 0 // todos los proyectos se importan como teniendo 0 MontoConseguido
                            };

                            rP.Add(pro);
                        }
                        linea = sr.ReadLine();
                    }
                }
                return ObtenerTodosLosProyectos();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return ObtenerTodosLosProyectos();
            }
        }

        private IEnumerable<DtoProyecto> ObtenerTodosLosProyectos()
        {
            List<Proyecto> proyectos = rP.FindAll().ToList();
            List<DtoProyecto> listaDeDTOProyectos = new List<DtoProyecto>();

            foreach (Proyecto p in proyectos)
            {
                DtoProyecto dtoProyecto = new DtoProyecto
                {
                    Estado = (p.Estado == "A") ? "Abierto" : "Cerrado",
                    TipoDeEquipo = (p.TipoDeEquipo == "P") ? "Personal" : "Cooperativo",
                    Titulo = p.Titulo,
                    Descripcion = p.Descripcion,
                    ImgURL = p.ImgURL,
                    FechaDePresentacion = p.FechaDePresentacion,
                    CantidadDeIntegrantes = p.CantidadDeIntegrantes,
                    ExperienciaPersonal = p.ExperienciaPersonal,
                    Cuotas = p.Cuotas,
                    PrecioPorCuota = p.PrecioPorCuota,
                    MontoSolicitado = p.MontoSolicitado,
                    PorcentajeDeInteres = p.PorcentajeDeInteres,
                    MontoConseguido = p.MontoConseguido,
                    IdSolicitante = p.IdSolicitante,
                };
                listaDeDTOProyectos.Add(dtoProyecto);
            }

            return listaDeDTOProyectos;
        }



    }
}
