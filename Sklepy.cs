using KASA.Encje;
using MWS.Lines;
using MWS.Pages;
using System.Collections.Generic;

namespace KASA.Strony
{
    internal class Sklepy : _Page
    {
        private List<Sklep> sklepy = new List<Sklep>(99);
        public Sklepy(StaticLine Note = null) : base(Note)
        {
            Contents.Add(new StaticLine("WYBIERZ SKLEP"));
            if(BazaDanych.Rekordy<Sklep>().Count > 0)
            {
                foreach(var sklep in BazaDanych.Rekordy<Sklep>())
                {
                    Contents.Add(new ActiveLine($" {sklep.nazwa}"));
                    sklepy.Add(sklep);
                }
            }
            else
            {
                Contents.Add(new StaticLine("Brak sklepów, dodaj nowe"));
            }
            Contents.Add(new StaticLine(""));
            Contents.Add(new ActiveLine("Dodaj sklep"));
            Contents.Add(new ActiveLine("Powrót"));
            Contents.Add(this.Note);
        }

        public override void React(_Line line)
        {
            if(line.Index > 0 && line.Index < sklepy.Count + 1)
            {
                MWS.DisplayAdapter.Display(new Zaloguj(sklepy[line.Index - 1]));
            }
            else if(line.Index == Contents.Count - 3)
            {
                MWS.DisplayAdapter.Display(new DodajSklep());
            }
            else if(line.Index == Contents.Count - 2)
            {
                MWS.DisplayAdapter.Display(new Home());
            }
        }
    }
}