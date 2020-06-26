using System;
using System.Collections.Generic;

namespace Markov
{
    public class ChainLink
    {
        private static readonly Random random = new Random();
        public string Token { get; set;}
        private Dictionary<string, ChainLink> nextLinks;
        private Dictionary<string, int> linkOcurences;
        private Dictionary<string, double> linkProbability;
        private int linkCount;

        public ChainLink(string token, ChainLink endLink)
        {
            this.Token = token;
            nextLinks = new Dictionary<string, ChainLink>();
            linkOcurences = new Dictionary<string, int>();
            linkProbability = new Dictionary<string, double>();
            if (endLink != null)
            {
                nextLinks.Add(endLink.Token, endLink);
                linkOcurences.Add(endLink.Token, 1);
                linkProbability.Add(endLink.Token, 1.0);
                linkCount = 1;
            }
            else
            {
                linkCount = 0;
            }
        }

        public void AddLink(ChainLink link)
        {
            //if (Token == "")
                //return;
            if (nextLinks.ContainsKey(link.Token))
            {
                linkOcurences[link.Token] = linkOcurences[link.Token] + 1;
            }
            else
            {
                nextLinks.Add(link.Token, link);
                linkOcurences.Add(link.Token, 1);
                linkProbability.Add(link.Token, 0.0);
            }
            linkCount++;
            updateProbability();
        }

        private void updateProbability()
        {
            //if (Token == "")
                //return;
            foreach(string key in nextLinks.Keys)
            {
                linkProbability[key] = (double)linkOcurences[key] / (double)linkCount;
            }
        }

        public ChainLink getNextLink()
        {
            //if (Token == "")
//                return null;
            double bottRange = 0;
            double topRange = 0;
            double choice = random.NextDouble();
            foreach(string key in nextLinks.Keys)
            {
                bottRange = topRange;
                topRange += linkProbability[key];
                if (choice >= bottRange && choice < topRange)
                    return nextLinks[key];
            }
            return nextLinks[""];
        }

        public Dictionary<string, double> getProbabilityes()
        {
            return linkProbability;
        }
    }
}