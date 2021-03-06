﻿using EnviosAliExpressCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnviosAliExpressCore.ClasesConvertirTiempoResponsability
{
    public class CalculadorMeses : IConvertirTiempo
    {
        private IConvertirTiempo next;
        public IConvertirTiempo getNext()
        {
            return this.next;
        }

        public void ObtenerTiempo(double dTiempo, Dictionary<string, double> diResultado)
        {
            if (dTiempo > 30)
            {
                double dResultado = (int)dTiempo / 30;
                string result= dResultado > 1?"Meses":"Mes";
                diResultado.Add(result, dResultado);
            }
        }

        public void setNext(IConvertirTiempo iConvertirTiempo)
        {
            this.next = iConvertirTiempo;
        }
    
    }
}
