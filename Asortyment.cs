using KASA.Encje;
using MWS.Lines;
using MWS.Pages;

namespace KASA.Strony
{
    internal class Asortyment : PageSklep
    {
        public Asortyment(Sklep sklep, StaticLine note = null) : base(sklep, note)
        {
            Contents.Add(new StaticLine("ASORTYMENT SKLEPU"));
            Contents.Add(new StaticLine("Dostępne: (wybierz by usunąć)"));
            if(sklep.asortyment.Count > 0)
            {
                foreach(var p in sklep.asortyment)
                {
                    Contents.Add(new ActiveLine($" {p.nazwa} ({p.cena}zł)"));
                }
            }
            else
                Contents.Add(new StaticLine(" Magazyn jest pusty."));
            Contents.Add(new ActiveLine("Rynek produktów"));
            Contents.Add(new ActiveLine("Powrót"));
            Contents.Add(this.Note);
        }

        public override void React(_Line line)
        {
            if (line.Index <= sklep.asortyment.Count + 1)
            {
                BazaDanych.Usun(sklep, sklep.asortyment[line.Index - 2]);
                MWS.DisplayAdapter.Display(new Asortyment(sklep));
            }
            else if (line.Index == Contents.Count - 3)
            {
                MWS.DisplayAdapter.Display(new Rynek(sklep));
            }
            else if (line.Index == Contents.Count - 2)
            {
                MWS.DisplayAdapter.Display(new Zaloguj(sklep));
            }
        }
    }
}