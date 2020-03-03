using EnviosAliExpress.ClasesMedioTransporte;
using EnviosAliExpressCore.Clases;
using EnviosAliExpressCore.ClasesConvertirTiempoResponsability;
using EnviosAliExpressCore.DTO;
using EnviosAliExpressCore.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnviosAliExpressCore
{
    public class Program 
    {

       
        static void Main(string[] args) {
            ContenedorDependencias contenedor = new ContenedorDependencias();
            IServiceProvider services = contenedor.Services();
             var instancia = services.GetService<ICrearInstanciaPaqueteriaFactory>();
            var ObtenerMasBarata = services.GetService<IObtenerMensajeMejorPaqueteria>();
            var validaPaqueteria = services.GetService<IValidarPaqueteria>();
            var validaMedioTransporte = services.GetService<IValidarMedioTransporte>();
            var EvaluadorFormatearMsg = services.GetService<IEvaluadorEstrategiaFormatearMensajeEnvio>();
            var ObtenerRangoTiempo = services.GetService<IConvertirTiempo>();

            LectorCSV objLecto = new LectorCSV();
          
            List<ParametrosDTO> lstDatosCsv = new List<ParametrosDTO>();
            ParametrosDTO imprimir = new ParametrosDTO();
            List<IMensajeEnvio> lstEstrategias = new List<IMensajeEnvio>();
            List<DatosCSV> lstDatosCSV= objLecto.ObtenerDatos();
            lstEstrategias.Add(new MensajesPaqueteriaFuturo(new List<string>(), new MensajeEnvioDTO(), new MensajesColoresDTO()));
            lstEstrategias.Add(new MensajesPaqueteriaPasado(new List<string>(), new MensajeEnvioDTO(), new MensajesColoresDTO()));

            lstDatosCsv = objLecto.ObtenerDatosDTO();
            string[] cPaqueterias = { "FEDEX", "DHL", "ESTAFETA" };
            foreach (var x in lstDatosCsv)
            {
                List<IEmpresa> paqueterias = instancia.CrearInstancia(x.cMedioTransporte);
                List<ParametrosDTO> lstResultados = new List<ParametrosDTO>();
                string cPaqueteria = x.cPaqueteria;
                string cMedioTransporte = x.cMedioTransporte;
                if (paqueterias != null)
                {
                    foreach (var i in paqueterias)
                    {
                        ParametrosDTO parametro = new ParametrosDTO();
                        parametro = i.ObtenerDatosPaqueteria(x);
                        lstResultados.Add(parametro);
                    }
                }
                Dictionary<string, double> lstTiempos = new Dictionary<string, double>();
                string cValidaPaqueteria = validaPaqueteria.ValidarPaqueteria(paqueterias,cPaqueteria, cPaqueterias);
              
                if (cValidaPaqueteria != "")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(cValidaPaqueteria+"\n");
                    continue;
                }
                string cValidarMedioTransporte = validaMedioTransporte.ValidarMedioTransporte(paqueterias, cPaqueteria, cMedioTransporte);
                if (cValidarMedioTransporte !="") {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(cValidarMedioTransporte+"\n");
                    continue;
                }
                imprimir = lstResultados.Where(i => i.cPaqueteria == cPaqueteria).Select(y => y).FirstOrDefault();

                string cClaveEstrategia = EvaluadorFormatearMsg.ObtenerClaveEstrategia(imprimir.dtFechaEntrega);
                var ObtenerMensaje = lstEstrategias.FirstOrDefault(c => c.cConjugacion == cClaveEstrategia);

                TimeSpan difFechas = DateTime.Now - imprimir.dtFechaEntrega;

                double dTotalMinutos=Math.Abs(difFechas.TotalMinutes);

                IConvertirTiempo h1 = new CalculadorMinutos();
                IConvertirTiempo h2 = new CalculadorHoras();
                IConvertirTiempo h3 = new CalculadorDias();
                IConvertirTiempo h4 = new CalculadorMeses();

                h1.setNext(h2);
                h2.setNext(h3);
                h3.setNext(h4);
                h1.ObtenerTiempo(dTotalMinutos, lstTiempos);

               MensajesColoresDTO Mensaje= ObtenerMensaje.FormatearMensaje(lstTiempos, imprimir);
               Console.ForegroundColor =(ConsoleColor) System.Enum.Parse(typeof(ConsoleColor), Mensaje.cColor) ;
               Console.Write(Mensaje.cMensaje+"\n");
               string cOpcionMaBarata=ObtenerMasBarata.ImprimirMensajePaqueteriaMasBarata(lstResultados, imprimir);
                if (cOpcionMaBarata != "")
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(cOpcionMaBarata+"\n");
                }
            }
            Console.ReadKey();
        }
    }
}
