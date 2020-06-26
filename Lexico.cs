using System;
using System.IO;

namespace Markov
{
    public class Lexico : Token, IDisposable  // Interfaz para el metodo Dispose que es llamado por el GC
    {
        private StreamReader archivoIn;
        //protected StreamWriter log;

        private const int F = -1;
        private const int E = -2;

        private static readonly int[,] TRAND = new int[,] {
        //    0  1  2  3  4  5  6  7  8  9  10  11  12  13  14  15  16  17  18
        //   WS  L  N  ,  .  ?  ¿  !  ¡  ;   "   '   -   (   )   :   <   >  LA
            { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18},  // 0
            { F, 1, F, F, F, F, F, F, F, F,  F,  F,  F,  F,  F,  F,  F,  F,  F},  // 1  PALABRA
            { F, F, 2, F, F, F, F, F, F, F,  F,  F,  F,  F,  F,  F,  F,  F,  F},  // 2  NUMERO
            { F, F, F, F, F, F, F, F, F, F,  F,  F,  F,  F,  F,  F,  F,  F,  F},  // 3  ,
            { F, F, F, F, F, F, F, F, F, F,  F,  F,  F,  F,  F,  F,  F,  F,  F},  // 4  .
            { F, F, F, F, F, F, F, F, F, F,  F,  F,  F,  F,  F,  F,  F,  F,  F},  // 5  ?
            { F, F, F, F, F, F, F, F, F, F,  F,  F,  F,  F,  F,  F,  F,  F,  F},  // 6  ¿
            { F, F, F, F, F, F, F, F, F, F,  F,  F,  F,  F,  F,  F,  F,  F,  F},  // 7  !
            { F, F, F, F, F, F, F, F, F, F,  F,  F,  F,  F,  F,  F,  F,  F,  F},  // 8  ¡
            { F, F, F, F, F, F, F, F, F, F,  F,  F,  F,  F,  F,  F,  F,  F,  F},  // 9  ;
            { F, F, F, F, F, F, F, F, F, F,  F,  F,  F,  F,  F,  F,  F,  F,  F},  // 10 "
            { F, F, F, F, F, F, F, F, F, F,  F,  F,  F,  F,  F,  F,  F,  F,  F},  // 11 '
            { F, F, F, F, F, F, F, F, F, F,  F,  F,  F,  F,  F,  F,  F,  F,  F},  // 12 -
            { F, F, F, F, F, F, F, F, F, F,  F,  F,  F,  F,  F,  F,  F,  F,  F},  // 13 (
            { F, F, F, F, F, F, F, F, F, F,  F,  F,  F,  F,  F,  F,  F,  F,  F},  // 14 )
            { F, F, F, F, F, F, F, F, F, F,  F,  F,  F,  F,  F,  F,  F,  F,  F},  // 15 :
            { F, F, F, F, F, F, F, F, F, F,  F,  F,  F,  F,  F,  F,  F,  F,  F},  // 16 <
            { F, F, F, F, F, F, F, F, F, F,  F,  F,  F,  F,  F,  F,  F,  F,  F},  // 17 >
            { F, F, F, F, F, F, F, F, F, F,  F,  F,  F,  F,  F,  F,  F,  F,  F},  // 18 LA
        //   WS  L  N  ,  .  ?  ¿  !  ¡  ;  "    '   -   (   )   :   <   >  LA
        //    0  1  2  3  4  5  6  7  8  9  10  11  12  13  14  15  16  17  18
        };

        protected int renglon;
        protected int columna;

        public Lexico() : this(@"/archivos/prueba.cs")
        {}

        public Lexico(string nombre)
        {
            //log = new StreamWriter(Path.ChangeExtension(nombre, "log"));
            //log.WriteLine("Programador: Angel Leon <user@domain>\n");
            //log.WriteLine("Compilando: " + nombre);
            try
            {
                archivoIn = new StreamReader(nombre);
                archivoIn.Peek();
            }
            catch (UnauthorizedAccessException)
            {
                imprimirError(new CompiladorException(nombre + "\n¡No se puede leer el archivo!"));
            }
            catch (FileNotFoundException)
            {
                imprimirError(new CompiladorException(nombre + "\n¡No existe el archivo!"));
            }
            catch (CompiladorException ex)
            {
                imprimirError(ex);
            }
            renglon = 1;
            columna = 1;
        }

        ~Lexico()
        {
            Dispose();
        }

        public void Dispose()
        {
            archivoIn.Close();
            //log.Close();
            //Console.WriteLine("Fin de analisis léxico");
        }

        private int columnaDe(char transicion)
        {
            if (char.IsWhiteSpace(transicion))
            {
                return 0;
            }
            else if (char.IsLetter(transicion))
            {
                return 1;
            }
            else if (char.IsDigit(transicion))
            {
                return 2;
            }
            else if (transicion == ',')
            {
                return 3;
            }
            else if (transicion == '.')
            {
                return 4;
            }
            else if (transicion == '?')
            {
                return 5;
            }
            else if (transicion == '¿')
            {
                return 6;
            }
            else if (transicion == '!')
            {
                return 7;
            }
            else if (transicion == '¡')
            {
                return 8;
            }
            else if (transicion == ';')
            {
                return 9;
            }
            else if (transicion == '"')
            {
                return 10;
            }
            else if (transicion == '\'')
            {
                return 11;
            }
            else if (transicion == '-')
            {
                return 12;
            }
            else if (transicion == '(')
            {
                return 13;
            }
            else if (transicion == ')')
            {
                return 14;
            }
            else if (transicion == ':')
            {
                return 15;
            }
            else if (transicion == '<')
            {
                return 16;
            }
            else if (transicion == '>')
            {
                return 17;
            }
            else
            {
                return 18;
            }
        }

        private int automata(int Estado, char Transicion)
        {
            int nuevoEstado = TRAND[Estado, columnaDe(Transicion)];
            switch (nuevoEstado)
            {
                case 1:
                    Clasificacion = PALABRA;
                    break;
                case 2:
                    Clasificacion = NUMERO;
                    break;
                case 3:
                    Clasificacion = COMA;
                    break;
                case 4:
                    Clasificacion = PUNTO;
                    break;
                case 5:
                    Clasificacion = INTERROGACION_CIERRE;
                    break;
                case 6:
                    Clasificacion = INTERROGACION_APERT;
                    break;
                case 7:
                    Clasificacion = EXCLAMACION_CIERRE;
                    break;
                case 8:
                    Clasificacion = EXCLAMACION_APERT;
                    break;
                case 9:
                    Clasificacion = PUNTO_Y_COMA;
                    break;
                case 10:
                    Clasificacion = COMILLA_DOBLE;
                    break;
                case 11:
                    Clasificacion = COMILLA_SIMPLE;
                    break;
                case 12:
                    Clasificacion = GUION;
                    break;
                case 13:
                    Clasificacion = PARENT_APERT;
                    break;
                case 14:
                    Clasificacion = PARENT_CIERRE;
                    break;
                case 15:
                    Clasificacion = DOS_PUNTOS;
                    break;
                case 16:
                    Clasificacion = MENOR_QUE;
                    break;
                case 17:
                    Clasificacion = MAYOR_QUE;
                    break;
                case 18:
                    Clasificacion = SIGNO;
                    break;
            }
            return nuevoEstado;
        }

        public void NetxToken()
        {
            Clasificacion = EOF;
            char transicion;
            string Buffer = "";
            int estado = 0;
                while (estado >= 0)
                {
                    transicion = char.ToLower((char)archivoIn.Peek()); // Lee, no avanza
                    estado = automata(estado, transicion);
                    if (archivoIn.EndOfStream)
                    {
                        Clasificacion = EOF;
                        break;
                    }
                    if (estado > 0)
                    {
                        Buffer += transicion;
                    }
                    if (estado >= 0)
                    {
                        columna++;
                        if (transicion == '\n')
                        {
                            renglon++;
                            columna = 1;
                        }
                        archivoIn.Read(); // Lee y avanza en el archivo, se pierde al no asignar
                    }
                }
                Contenido = Buffer;            
        }

        public bool isEOF()
        {
            return Clasificacion == EOF;
        }

        protected void imprimirError(CompiladorException ex)
        {
            ConsoleColor colorActual = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine(ex.Message);
            Console.WriteLine();
            Console.ForegroundColor = colorActual;
            //log.WriteLine(ex.Message);
            //log.Close();
            Environment.Exit(1);
        }
    }
}
