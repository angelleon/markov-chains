using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markov
{
    public class Token
    {
        private string contenido;
        private int clasificacion;

        public string Contenido {get{return contenido;} set{contenido = value;}}

        public int Clasificacion {get{return clasificacion;} set{clasificacion = value;}}

        protected const int PALABRA = 0; //L(L)+
        protected const int NUMERO = 1; // N(N)+
        protected const int COMA = 2;
        protected const int PUNTO = 3;
        protected const int INTERROGACION_CIERRE = 4;
        protected const int INTERROGACION_APERT = 5;
        protected const int EXCLAMACION_CIERRE = 6;
        protected const int EXCLAMACION_APERT = 7;
        protected const int PUNTO_Y_COMA = 8;
        protected const int COMILLA_DOBLE = 9;
        protected const int COMILLA_SIMPLE = 10;
        protected const int GUION = 11;
        protected const int PARENT_APERT = 12;
        protected const int PARENT_CIERRE = 13;
        protected const int DOS_PUNTOS = 14;
        protected const int MENOR_QUE = 15;
        protected const int MAYOR_QUE = 16;
        protected const int SIGNO = 17;

        protected const int EOF = 18;

        public string SAYClasificacion()
        {
            switch (clasificacion)
            {
                case PALABRA:
                    return "palabra";
                case NUMERO:
                    return "número";
                case COMA:
                    return "coma";
                case PUNTO:
                    return "punto";
                case INTERROGACION_CIERRE:
                    return "interrogación de cierre";
                case INTERROGACION_APERT:
                    return "interogación de apertura";
                case EXCLAMACION_CIERRE:
                    return "exclamación de cierre";
                case EXCLAMACION_APERT:
                    return "exclamación de apertura";
                case PUNTO_Y_COMA:
                    return "punto y coma";
                case COMILLA_DOBLE:
                    return "comilla doble";
                case COMILLA_SIMPLE:
                    return "comilla simple";
                case GUION:
                    return "guión";
                case PARENT_APERT:
                    return "párentesis de apertura";
                case PARENT_CIERRE:
                    return "párentesis de cierre";
                case DOS_PUNTOS:
                    return "dos puntos";
                case MENOR_QUE:
                    return "menor que";
                case MAYOR_QUE:
                    return "mayor que";
                case SIGNO:
                    return "signo";
                default:
                    return "sin clasificación";
            }
        }
    }
}
