using EnviosAliExpressCore.DTO;
using EnviosAliExpressCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnviosAliExpressCore.Clases
{
    public class CalculadorMargenUtilidadCostoEstafeta : ICalculadorMargenUtilidadCosto
    {
        private readonly List<RangosMargenDTO> lstRangosMargen;
        public CalculadorMargenUtilidadCostoEstafeta(List<RangosMargenDTO> lstRangosMargen)
        {
            this.lstRangosMargen = lstRangosMargen;
            lstRangosMargen.Add(new RangosMargenDTO { dtInicio = new DateTime(2020, 01, 01), dtFin = new DateTime(2020, 02, 13), dValor = 1.45 });
            lstRangosMargen.Add(new RangosMargenDTO { dtInicio = new DateTime(2020, 02, 14), dtFin = new DateTime(2020, 02, 14), dValor = 1.1 });
            lstRangosMargen.Add(new RangosMargenDTO { dtInicio = new DateTime(2020, 02, 15), dtFin = new DateTime(2020, 11, 30), dValor = 1.45 });
            lstRangosMargen.Add(new RangosMargenDTO { dtInicio = new DateTime(2020, 12, 1), dtFin = new DateTime(2020, 12, 31), dValor = 1.5 });
        }

        public double ObtenerMargenUtilidadCosto(double dCosto, DateTime dtFechaPedido)
        {
            
            double dValorMargen = lstRangosMargen.Where(i =>(  dtFechaPedido >= i.dtInicio &&  dtFechaPedido<= i.dtFin) ).Select(i => i.dValor).FirstOrDefault(); ;
            double dCostoMargen = dValorMargen*dCosto;
            return dCostoMargen;
        }
    }
}
