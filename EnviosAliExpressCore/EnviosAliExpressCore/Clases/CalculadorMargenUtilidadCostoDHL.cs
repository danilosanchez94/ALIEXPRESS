using EnviosAliExpressCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnviosAliExpressCore.Clases
{
    public class CalculadorMargenUtilidadCostoDHL : ICalculadorMargenUtilidadCosto
    {
        public double ObtenerMargenUtilidadCosto(double dCosto, DateTime dtFechaPedido)
        {
            double dCostoMargen = 0;
            dCostoMargen = dtFechaPedido.Month <= 6? dCosto * 1.5 : dCosto * 1.3;
            return dCostoMargen;
        }
    }
}
