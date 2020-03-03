using EnviosAliExpress.ClasesMedioTransporte;
using EnviosAliExpressCore.DTO;
using EnviosAliExpressCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnviosAliExpressCore.Clases
{
    public class CrearInstanciaFactory : ICrearInstanciaPaqueteriaFactory
    {


        List<IEmpresa> ICrearInstanciaPaqueteriaFactory.CrearInstancia(string cClave)
        {
            List<IEmpresa> empresa = null;
            IMediosTransporte medioTransporte = null;
            switch (cClave)
            {
                case "MARITIMO":
                    medioTransporte = new Maritimo(new CalculadorVelocidadMaritimo(new List<DTO.EstacionesDTO>(), new Estacion()), new CalculadorCostoEnvioMaritimo(new List<DTO.EstacionesDTO>(), new List<DTO.RangosDTO>(), new Estacion()));
                    empresa = new List<IEmpresa>();
                    ParametrosDTO parametros = new ParametrosDTO();
                    empresa.Add(new Fedex(medioTransporte, new CalculadorMargenUtilidadCostoFedex(), new List<TiempoRepartoDTO>(), new ParametrosDTO()));
                    empresa.Add(new DHL(medioTransporte, new CalculadorMargenUtilidadCostoDHL(), new List<TiempoRepartoDTO>(), new ParametrosDTO()));
                    empresa.Add(new Estafeta(medioTransporte, new CalculadorMargenUtilidadCostoEstafeta(new List<RangosMargenDTO>()), new List<TiempoRepartoDTO>(), new ParametrosDTO()));
                    break;
                case "TERRESTRE":
                    medioTransporte = new Terrestre(new CalculadorCostoEnvioTerrestre(new List<RangosDTO>()), new CalculadorTiempoTrasladoTerrestre(new Estacion(), new List<EstacionesDTO>()));
                    empresa = new List<IEmpresa>();

                    empresa.Add(new Fedex(medioTransporte, new CalculadorMargenUtilidadCostoFedex(), new List<TiempoRepartoDTO>(), new ParametrosDTO()));
                    empresa.Add(new DHL(medioTransporte, new CalculadorMargenUtilidadCostoDHL(), new List<TiempoRepartoDTO>(), new ParametrosDTO()));
                    empresa.Add(new Estafeta(medioTransporte, new CalculadorMargenUtilidadCostoEstafeta(new List<RangosMargenDTO>()), new List<TiempoRepartoDTO>(), new ParametrosDTO()));
                    break;
                case "AEREO":
                    medioTransporte = new Aereo(new CalculadorEscalas());
                    empresa = new List<IEmpresa>();
                    empresa.Add(new Fedex(medioTransporte, new CalculadorMargenUtilidadCostoFedex(), new List<TiempoRepartoDTO>(), new ParametrosDTO()));
                    empresa.Add(new DHL(medioTransporte, new CalculadorMargenUtilidadCostoDHL(), new List<TiempoRepartoDTO>(), new ParametrosDTO()));
                    break;
                //default:
                //    empresa.Add(new Fedex(null, new CalculadorMargenUtilidadCostoFedex(), new List<TiempoRepartoDTO>(), new ParametrosDTO()));
                //    empresa.Add(new DHL(null, new CalculadorMargenUtilidadCostoDHL(), new List<TiempoRepartoDTO>(), new ParametrosDTO()));
                //    empresa.Add(new Estafeta(null, new CalculadorMargenUtilidadCostoEstafeta(new List<RangosMargenDTO>()), new List<TiempoRepartoDTO>(), new ParametrosDTO()));
                //    break;
            }
            return empresa;
        }


    }
}
