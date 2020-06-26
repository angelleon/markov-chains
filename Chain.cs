using System;
using System.Collections.Generic;

namespace Markov
{
    public class Chain
    {
        private Dictionary<string, ChainLink> links;

        private int linkCount;
        public Chain()
        {
            links = new Dictionary<string, ChainLink>();
            ChainLink link = new ChainLink("", null);
            Console.WriteLine(link);
            links.Add(link.Token, link);
        }

        public void AddLink(string prevToken, string newToken)
        {
            //Console.WriteLine("prevToken: " + prevToken);
            //Console.WriteLine("newToken: " + newToken);
            if (links.ContainsKey(prevToken))
            {
                if (links.ContainsKey(newToken))
                {
                    links[prevToken].AddLink(links[newToken]);
                }
                else
                {
                    ChainLink link = new ChainLink(newToken, null);
                    links.Add(link.Token, link);
                    links[prevToken].AddLink(link);
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public ChainLink getFirstLink()
        {
            return links[""].getNextLink();
        }
        public Dictionary<string, ChainLink> getLinks()
        {
            return links;
        }


    }
}