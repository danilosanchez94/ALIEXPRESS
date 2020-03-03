using EnviosAliExpressCore.DTO;
using EnviosAliExpressCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnviosAliExpressCore
{
    public class CalculadorCostoEnvioTerrestre : ICalculadorCostoEnvio
    {

        private readonly List<RangosDTO> lstRangos;
        public CalculadorCostoEnvioTerrestre(List<RangosDTO> lstRangos)
        {

            this.lstRangos = lstRangos;
            lstRangos.Add(new RangosDTO { iLimiteInferior = 1, iLimiteSuperior = 50, dCosto = 15 });
            lstRangos.Add(new RangosDTO { iLimiteInferior = 51, iLimiteSuperior = 200, dCosto = 10 });
            lstRangos.Add(new RangosDTO { iLimiteInferior = 201, iLimiteSuperior = 300, dCosto = 8 });
            lstRangos.Add(new RangosDTO { iLimiteInferior = 301, iLimiteSuperior = null, dCosto = 7 });

        }

        public double CalcularCostoEnvio(DateTime dtFechaPedido, double dDistancia)
        {
            double dCosto = lstRangos.Where(x =>  dDistancia>= x.iLimiteInferior && (  dDistancia<= x.iLimiteSuperior || x.iLimiteSuperior == null)).Select(i => i.dCosto).FirstOrDefault();
            double dCostoEnvio = dDistancia * dCosto;
            return dCostoEnvio;
        }
    }
}
