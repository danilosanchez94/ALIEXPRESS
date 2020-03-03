using EnviosAliExpressCore.DTO;
using EnviosAliExpressCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnviosAliExpressCore.Clases
{
    public class CalculadorMargenUtilidadCostoFedex : ICalculadorMargenUtilidadCosto
    {
        public CalculadorMargenUtilidadCostoFedex()
        {
        }

        public double ObtenerMargenUtilidadCosto(double dCosto, DateTime dtFechaPedido)
        {
            double dCostoMargen = 0;
            dCostoMargen = dtFechaPedido.Month % 2 == 0 ? dCosto * 1.4 : dCosto * 1.3;
            return dCostoMargen;
        }
    }
}
