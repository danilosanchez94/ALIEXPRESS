using EnviosAliExpressCore.DTO;
using EnviosAliExpressCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnviosAliExpressCore
{
    public class CalculadorTiempoTrasladoTerrestre : ICalculadorTiempoTraslado
    {
        private readonly List<EstacionesDTO> lstEstaciones;
        private readonly IObtenerEstacion obtenerEstacion;
        public CalculadorTiempoTrasladoTerrestre(IObtenerEstacion obtenerEstacion, List<EstacionesDTO> lstEstaciones)
        {
            this.lstEstaciones = lstEstaciones;
            lstEstaciones.Add(new EstacionesDTO { cEstacion = "PRIMAVERA", dValor = 4 });
            lstEstaciones.Add(new EstacionesDTO { cEstacion = "VERANO", dValor = 6 });
            lstEstaciones.Add(new EstacionesDTO { cEstacion = "OTONIO", dValor = 5 });
            lstEstaciones.Add(new EstacionesDTO { cEstacion = "INVIERNO", dValor = 8 });
            this.obtenerEstacion = obtenerEstacion;
        }
        public double CalcularTiempoTraslado(double dTiempoTraslado,DateTime dtFechaPedido)
        {
            string cEstacion = obtenerEstacion.ObtenerEstacion(dtFechaPedido.Month);
            double dVariacion = lstEstaciones.Where(z => z.cEstacion.ToUpper() == cEstacion.ToUpper()).Select(i => i.dValor).FirstOrDefault();
            double dTiempoExtra = (dTiempoTraslado * dVariacion);
            return dTiempoExtra;
        }
    }
}
