using KASA.Encje;
using MWS.Lines;
using MWS.Pages;
using System;

namespace KASA.Strony
{
    internal class Zamawianie : PageSklep
    {
        Produkt produkt = new Produkt();
        public Zamawianie(Sklep sklep, Produkt produkt = null, StaticLine note = null) : base(sklep, note)
        {
            if (produkt != null)
                this.produkt = produkt;
            Contents.Add(new StaticLine("ZAMAWIANIE PRODUKTU"));
            Contents.Add(new ActiveLine("Nazwa: " + this.produkt.nazwa));
            Contents.Add(new ActiveLine($"Cena: {this.produkt.cena}zł"));
            Contents.Add(new ActiveLine("Zamów"));
            Contents.Add(new ActiveLine("Powrót"));
            Contents.Add(this.Note);
        }

        public override void React(_Line line)
        {
            switch(line.Index)
            {
                case 1:
                    produkt.nazwa = Console.ReadLine();
                    break;
                case 2:
                    decimal c;
                    if(Decimal.TryParse(Console.ReadLine(), out c))
                        produkt.cena = c;
                    break;
                case 3:
                    BazaDanych.Wstaw(produkt);
                    MWS.DisplayAdapter.Display(new Rynek(sklep));
                    break;
                case 4:
                    MWS.DisplayAdapter.Display(new Rynek(sklep));
                    break;
            }
            MWS.DisplayAdapter.Display(new Zamawianie(sklep, produkt));
        }
    }
}