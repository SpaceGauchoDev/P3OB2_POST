using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;
using Datos.Repositorios;
using System.Text.RegularExpressions;

namespace TestingConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            ProbarValidarCelular("099879995");
            ProbarValidarCelular("0998799aa");
            ProbarValidarCelular("109987aa95");
            ProbarValidarCelular("0aa9000000000005");
            ProbarValidarCelular("00000000000000000");
            */

            /*
            ProbarValidarContrasenia("Aa1234");
            ProbarValidarContrasenia("Aa12345");
            ProbarValidarContrasenia("Aa123456");
            ProbarValidarContrasenia("Aa1234567");

            ProbarValidarContrasenia("A12345");
            ProbarValidarContrasenia("a12345");
            ProbarValidarContrasenia("AAAAAA");
            ProbarValidarContrasenia("123456");
            */

            /*
            DateTime e1 = new DateTime(1990, 2, 15);
            ProbarValidarEdad(e1);
            DateTime e2 = new DateTime(2000, 4, 10);
            ProbarValidarEdad(e2);
            DateTime e3 = new DateTime(2010, 7, 22);
            ProbarValidarEdad(e3);
            DateTime e4 = new DateTime(1999, 3, 17);
            ProbarValidarEdad(e4);
            */

            //ParsearFecha("1991-02-17 12:00:00 AM");

            //ProbarAgregarInversor();

            //ProbarBuscarInversor(42935324);

            //ProbarBuscarInversorPorEmail("manucaca@gmail.com");
            //ProbarBuscarInversorPorEmail("puto@gmail.com");

            //ProbarBuscarSolicitantePorEmail("gordon@gmail.com");
            //ProbarBuscarSolicitantePorEmail("puto@gmail.com");

            ProbaRegEx("manu1502");

            Console.WriteLine("End");
            Console.ReadLine();
        }


        public static void ProbaRegEx(string s)
        {
            bool result = false;

            /*
            // source: https://www.rhyous.com/2010/06/15/csharp-email-regular-expression/
            string regexPattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                    + "@"
                                    + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z";
            */

            // source: https://regexr.com/3e48o
            string regexPattern = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";
            result = Regex.IsMatch(s, regexPattern);

            Console.WriteLine(s + " verifica regex: " + result);
        }


        public static void ProbarBuscarInversor(int idInversor)
        {
            RepositorioInversor rI = new RepositorioInversor();
            Inversor i = rI.FindById(idInversor);

            if (i != null)
            {
                Console.WriteLine("Encontró inversor");
            }
            else
            {
                Console.WriteLine("No encontró inversor");
            }
        }


        public static void ProbarBuscarSolicitantePorEmail(string email)
        {
            RepositorioSolicitante rS = new RepositorioSolicitante();
            bool s = rS.ExistsByEmail(email);

            Console.WriteLine("Existe " + email + " solicitante: " + s);
        }

        public static void ProbarBuscarInversorPorEmail(string email)
        {
            RepositorioInversor rI = new RepositorioInversor();
            bool i = rI.ExistsByEmail(email);

            Console.WriteLine("Existe "+ email +" inversor: " + i);
        }

        public static void ProbarAgregarInversor()
        {
            Inversor i = new Inversor
            {
                IdUsuario = 31832575,
                Nombre = "Gordon",
                Apellido = "Gordoñez",
                Pass = "Aa1234",
                FechaDeNacimiento = ConvertirAFecha("1991-02-17"),
                Email = "gordon@gmail.com",
                Cell = "099879997",
                TienePassTemporal = false,
                MaxInvPorProyecto = (decimal)2000.0,
                PresentacionInversor = "hola esta es mi presentacion"
            };

            RepositorioInversor rI = new RepositorioInversor();
            rI.Add(i);
        }


        public static void ParsearFecha(string fecha)
        {
            string anio = fecha.Substring(0, 4);
            string mes = fecha.Substring(5, 2);
            string dia = fecha.Substring(8, 2);

            string resultado = "año: " + anio + " ";
            resultado += "mes: " + mes + " ";
            resultado += "dia: " + dia;

            Console.WriteLine(resultado);
        }

        public static DateTime ConvertirAFecha(string fechaString)
        {
            int anio = int.Parse(fechaString.Substring(0, 4));
            int mes = int.Parse(fechaString.Substring(5, 2));
            int dia = int.Parse(fechaString.Substring(8, 2));
            DateTime fechaDate = new DateTime(anio, mes, dia);

            return fechaDate;
        }



        public static void ProbarValidarCelular(string pCell)
        {
            Inversor u = new Inversor();
            Console.WriteLine(pCell + " " + u.ValidarCelular(pCell));
        }

        public static void ProbarValidarContrasenia(string pPass)
        {
            Inversor u = new Inversor();
            Console.WriteLine(pPass + " " + u.ValidarContrasenia(pPass));
        }


        public static void ProbarValidarEdad(DateTime pEdad)
        {
            Inversor u = new Inversor();
            Console.WriteLine(pEdad.ToString("yyyyMMdd") + " " + u.ValidarEdad(pEdad));
        }


    }
}
