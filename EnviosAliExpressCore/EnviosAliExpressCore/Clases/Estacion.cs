using EnviosAliExpressCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnviosAliExpressCore
{
    public class Estacion : IObtenerEstacion
    {
        public string ObtenerEstacion(int iMes)
        {
            string cEstacion = "";
            switch (iMes)
            {

                case 3:
                case 4:
                case 5:
                    cEstacion = "PRIMAVERA";
                    break;

                case 6:
                case 7:
                case 8:
                    cEstacion = "VERANO";
                    break;

                case 9:
                case 10:
                case 11:
                    cEstacion = "OTONIO";
                    break;

                case 12:
                case 1:
                case 2:
                    cEstacion = "INVIERNO";
                    break;
            }
            return cEstacion;       
    }
    }
}
