using System;
using System.IO;

namespace Markov
{
    public class ErrorLexico : System.Exception
    {
        public ErrorLexico() { }
        public ErrorLexico(string message) : base(message) { }
        public ErrorLexico(string message, System.Exception inner) : base(message, inner) { }
        protected ErrorLexico(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public ErrorLexico(string clasificacion, string Buffer) :
            this("Error léxico, se esperaba " + clasificacion + "\nSe encontró: (" + Buffer + ")")
        //Llamada a constructor ErrorLexico(string messenge)
        {
        }
    }
}