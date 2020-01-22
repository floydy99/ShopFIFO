using KASA.Encje;
using MWS.Lines;
using MWS.Pages;
using System.Collections.Generic;

namespace KASA.Strony
{
    internal class Koszyk : PageSklep
    {
        private Logowanie logowanie = new Logowanie();
        private List<Klient> klienci_kolejka = new List<Klient>(99);
        private List<Produkt> produkty_skasowane = new List<Produkt>(99);
        private Kasjer kasjer = new Kasjer();
        public Koszyk(Sklep sklep, Logowanie logowanie, Kasjer kasjer, List<Klient> klienci_kolejka, List<Produkt> produkty_skasowane, StaticLine note = null) : base(sklep, note)
        {
            if (logowanie != null)
                this.logowanie = logowanie;
            if (kasjer != null)
                this.kasjer = kasjer;
            if (klienci_kolejka != null)
                this.klienci_kolejka = klienci_kolejka;
            if (produkty_skasowane != null)
                this.produkty_skasowane = produkty_skasowane;

            Contents.Add(new StaticLine("WYBIERZ PRODUKT Z LISTY"));
            if (sklep.asortyment.Count == 0)
                Contents.Add(new StaticLine("Brak produktów w magazynie"));
            else
            {
                foreach(var p in sklep.asortyment)
                {
                    Contents.Add(new ActiveLine($" {p.nazwa} ({p.cena}zł)"));
                }
            }
            Contents.Add(new ActiveLine("Powrót"));
            Contents.Add(this.Note);
        }

        public override void React(_Line line)
        {
            if(line.Index <= sklep.asortyment.Count + 1)
            {
                produkty_skasowane.Add(sklep.asortyment[line.Index - 1]);
            }
            MWS.DisplayAdapter.Display(new Kasa(sklep, logowanie, kasjer, klienci_kolejka, produkty_skasowane));
        }
    }
}