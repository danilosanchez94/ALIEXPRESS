using EnviosAliExpressCore.DTO;
using EnviosAliExpressCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace EnviosAliExpressCore
{
    public class CalculadorVelocidadMaritimo: IObtenerVelocidadVariacion
    {


        private readonly List<EstacionesDTO> lstEstaciones;
        private readonly IObtenerEstacion obtenerEstacion;
        public CalculadorVelocidadMaritimo(List<EstacionesDTO> lstEstaciones, IObtenerEstacion obtenerEstacion)
        {
            this.lstEstaciones = lstEstaciones;
            lstEstaciones.Add(new EstacionesDTO { cEstacion="PRIMAVERA",dValor=0});
            lstEstaciones.Add(new EstacionesDTO { cEstacion = "VERANO", dValor = -0.1 });
            lstEstaciones.Add(new EstacionesDTO { cEstacion = "OTONIO", dValor = 0.15 });
            lstEstaciones.Add(new EstacionesDTO { cEstacion = "INVIERNO", dValor = -0.30 });
            this.obtenerEstacion = obtenerEstacion;
        }
        public double CalculadorVariacionVelocidad(DateTime dtFechaPedido, double dVelocidad)
        {
            string cEstacion = obtenerEstacion.ObtenerEstacion(dtFechaPedido.Month);
            double dVariacion = lstEstaciones.Where(z => z.cEstacion.ToUpper() == cEstacion.ToUpper()).Select(i=>i.dValor).FirstOrDefault();
            double dVariacionVelocidad= dVelocidad +  (dVelocidad * dVariacion);
            return dVariacionVelocidad;
        }


    }
}
