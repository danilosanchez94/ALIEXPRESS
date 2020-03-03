using CsvHelper;
using EnviosAliExpressCore.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace EnviosAliExpressCore.Clases
{
    public class LectorCSV
    {
        public List<DatosCSV> ObtenerDatos()
        {
            List<DatosCSV> DatosCsv = new List<DatosCSV>();
            using (var reader = new StreamReader(@"..\\..\\..\\temp\\PAQUETERIAS.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                 DatosCsv = csv.GetRecords<DatosCSV>().ToList();
                
            }
            return DatosCsv;

        }

        public List<ParametrosDTO> ObtenerDatosDTO() {
            List<DatosCSV> lstDatosCSV = ObtenerDatos();
            List<ParametrosDTO> lstDatosCsv = new List<ParametrosDTO>();

            foreach (var x in lstDatosCSV)
            {
                ParametrosDTO parametros = new ParametrosDTO();
                parametros.dDistacia = Convert.ToDouble(x.cDistancia);
                parametros.dtFechaPedido = Convert.ToDateTime(x.cFechaPedido);
                parametros.cMedioTransporte = x.cMedioTransporte.ToUpper();
                parametros.cPaqueteria = x.cPaqueteria.ToUpper();
                parametros.cCiudadDestino = x.cCiudadDestino.ToUpper();
                parametros.cCiudadOrigen = x.cCiudadOrigen.ToUpper();
                parametros.cPaisOrigen = x.cPaisOrigen.ToUpper();
                parametros.cPaisDestino = x.cPaisDestino.ToUpper(); ;

                lstDatosCsv.Add(parametros);
            }
            return lstDatosCsv;


        }
    }
}
