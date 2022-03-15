using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6BP_OudouoirGuillaume_SOTrim2
{
    internal class Medicijn
    {
        public string Naam;//declareert de public values
        public string Soort;
        public int AantalPillenPerDoos;
        public int AantalPillenPerDoosOrg;
        public int AantalBeschikbareDozen;
        public bool Voorschrift;

        public Medicijn(string Naam, string Soort, int pillen,int dozen, bool voorschrift)//maakt zodat alles gemakkelijk kan toegevoegd worden
        {
            this.Naam = Naam;
            this.Soort = Soort;
            AantalPillenPerDoosOrg = pillen;
            AantalBeschikbareDozen += dozen;
            Voorschrift = voorschrift;
        }

        public string getNaam//vraagt de naam op
        {
            get { return Naam; }
            set { Naam = value; }
        }
        public string getSoort//vraagt de soort op
        {
            get { return Soort; }
            set { Soort = value; }
        }
        public int voegDoosjeToe(int amount)//voegt doosjes toe
        {
            AantalBeschikbareDozen += amount;
            return AantalBeschikbareDozen;
        }
        public int neemPilWeg(int amount)//trekt pillen af van het totale
        {
            int lastKnown = AantalPillenPerDoos;
            int lastKnowndoos = AantalBeschikbareDozen;
            if (amount > AantalPillenPerDoos && AantalBeschikbareDozen >= 1)
            {
                for(int i = amount; i > AantalPillenPerDoos;)
                {
                    AantalPillenPerDoos += AantalPillenPerDoosOrg;
                    AantalBeschikbareDozen--;
                    if (AantalBeschikbareDozen== 0)
                    {
                        AantalBeschikbareDozen = lastKnowndoos;
                        AantalPillenPerDoos = lastKnown;
                        return -1;
                    }
                    
                }
                
                AantalPillenPerDoos -= amount;
            }
            else
            {
                AantalPillenPerDoos -= amount;
            }
            return AantalPillenPerDoos;
        }
        public bool opVoorschrift//kijkt of een voorschrift nodig is
        {
            get { return Voorschrift; }
            set { Voorschrift = value; }
        }
    }
}
