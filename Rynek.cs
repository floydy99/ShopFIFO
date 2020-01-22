using KASA.Encje;
using MWS.Lines;
using MWS.Pages;
using System.Collections.Generic;

namespace KASA.Strony
{
    internal class Rynek : PageSklep
    {
        public Rynek(Sklep sklep, StaticLine note = null) : base(sklep, note)
        {
            Contents.Add(new StaticLine("RYNEK"));
            Contents.Add(new StaticLine("Dostępne: (wybierz by dodać do sklepu)"));
            if (BazaDanych.Rekordy<Produkt>().Count > 0)
            {
                foreach (var p in BazaDanych.Rekordy<Produkt>())
                {
                    bool interaktywna = true;
                    foreach(var pa in sklep.asortyment)
                    {
                        if(p.id == pa.id)
                        {
                            interaktywna = false;
                            Contents.Add(new StaticLine($" {p.nazwa} ({p.cena}zł)"));
                        }
                    }
                    if(interaktywna)
                     Contents.Add(new ActiveLine($" {p.nazwa} ({p.cena}zł)"));
                }
            }
            else
                Contents.Add(new StaticLine(" Rynek jest pusty."));
            Contents.Add(new ActiveLine("Zamów towar na rynek"));
            Contents.Add(new ActiveLine("Powrót"));
            Contents.Add(this.Note);
        }

        public override void React(_Line line)
        {
            if (line.Index <= BazaDanych.Rekordy<Produkt>().Count + 1)
            {
                BazaDanych.Wstaw(sklep, BazaDanych.Rekordy<Produkt>()[line.Index - 2]);
                MWS.DisplayAdapter.Display(new Rynek(sklep));
            }
            else if (line.Index == Contents.Count - 3)
            {
                MWS.DisplayAdapter.Display(new Zamawianie(sklep));
            }
            else if (line.Index == Contents.Count - 2)
            {
                MWS.DisplayAdapter.Display(new Asortyment(sklep));
            }
        }
    }
}