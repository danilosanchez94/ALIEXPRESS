using EnviosAliExpressCore.DTO;
using EnviosAliExpressCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnviosAliExpressCore
{
    public class CalculadorCostoEnvioMaritimo : ICalculadorCostoEnvio
    {
        private readonly List<EstacionesDTO> lstEstaciones;
        private readonly List<RangosDTO> lstRangos;
        private readonly IObtenerEstacion obtenerEstacion;
        public CalculadorCostoEnvioMaritimo(List<EstacionesDTO> lstEstaciones, List<RangosDTO> lstRangos, IObtenerEstacion obtenerEstacion)
        {
            this.lstEstaciones = lstEstaciones;
            lstEstaciones.Add(new EstacionesDTO { cEstacion = "PRIMAVERA", dValor = 1 });
            lstEstaciones.Add(new EstacionesDTO { cEstacion = "VERANO", dValor = 1.1 });
            lstEstaciones.Add(new EstacionesDTO { cEstacion = "OTONIO", dValor = 1.15 });
            lstEstaciones.Add(new EstacionesDTO { cEstacion = "INVIERNO", dValor = 1.23 });
            this.obtenerEstacion = obtenerEstacion;

            this.lstRangos = lstRangos;
            lstRangos.Add(new RangosDTO { iLimiteInferior = 1, iLimiteSuperior = 100, dCosto = 1 });
            lstRangos.Add(new RangosDTO { iLimiteInferior = 101, iLimiteSuperior = 1000, dCosto = 0.5 });
            lstRangos.Add(new RangosDTO { iLimiteInferior = 1001, iLimiteSuperior = null, dCosto = .3 });

        }

        public double CalcularCostoEnvio(DateTime dtFechaPedido, double dDistancia)
        {
            string cEstacion = obtenerEstacion.ObtenerEstacion(dtFechaPedido.Month);
            double dVariacion = lstEstaciones.Where(z => z.cEstacion.ToUpper() == cEstacion.ToUpper()).Select(i => i.dValor).FirstOrDefault();
            double dCosto = lstRangos.Where(x=>dDistancia>=x.iLimiteInferior && (dDistancia<=x.iLimiteSuperior || x.iLimiteSuperior==null)).Select(i=>i.dCosto).FirstOrDefault();
            double dCostoEnvioAux = dDistancia * dCosto;
            double dCostoEnvio = dCostoEnvioAux * dVariacion;
            return dCostoEnvio;
        }

    }
}
