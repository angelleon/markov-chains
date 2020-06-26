using System;
using System.IO;
using System.Collections.Generic;

/**
Distancia de edicion de Levenshtine
 */

namespace Markov
{
    public class Markov : IDisposable
    {
        private string[] filePaths;
        private Lexico tokenizer;

        private StreamWriter log;

        private StreamWriter output;

        Chain chain;

        private float average;

        public Markov(string[] filePaths)
        {
            this.filePaths = filePaths;
            log = new StreamWriter("markov.log");
            output = new StreamWriter("output");
            log.AutoFlush = true;
        }

        public void Dispose()
        {
            log.Close();
            output.Close();
        }


        public void run()
        {
            buildChain();
            Dictionary<string, ChainLink> links = chain.getLinks();
            log.WriteLine("");
            foreach(string token in links.Keys)
            {
                Dictionary<string, double> probs = links[token].getProbabilityes();
                log.WriteLine(token);
                foreach(string key in probs.Keys)
                {
                    log.WriteLine(key + ": " + probs[key]);
                }
                log.WriteLine("-----------------------");
            }
            generateOutput();
        }

        private void buildChain()
        {
            int cont = 0;
            int acum = 0;
            chain = new Chain();
            foreach (string fPath in filePaths)
            {
                using (tokenizer = new Lexico(fPath))
                {
                    string token = "";
                    string prevToken = "";
                    tokenizer.NetxToken();
                    cont++;
                    while (!tokenizer.isEOF())
                    {
                        prevToken = token;
                        token = tokenizer.Contenido;
                        //Console.WriteLine("Token: " + token);
                        chain.AddLink(prevToken, token);
                        tokenizer.NetxToken();
                        cont++;
                    }
                }
                //Console.WriteLine(cont);
                acum += cont;
                cont = 0;
            }
            average = acum / filePaths.Length;
            //Console.WriteLine("promedio: " + average);
        }

        private void generateOutput()
        {
            ChainLink link = chain.getFirstLink();
            output.Write(link.Token + " ");
            for (int i=0; i < average; i++)
            {
                link = link.getNextLink();
                output.Write(link.Token + " ");
                if (i % 6 == 0)
                {
                    output.Write("\n");
                }
            }
        }
    }
}