using KASA.Encje;
using MWS.Lines;
using MWS.Pages;
using System;
using System.Collections.Generic;

namespace KASA.Strony
{
    internal class Kasa : PageSklep
    {
        private Logowanie logowanie = new Logowanie();
        private List<Klient> klienci_kolejka = new List<Klient>(99);
        private List<Produkt> produkty_skasowane = new List<Produkt>(99);
        private Kasjer _kasjer;
        private Kasjer kasjer
        {
            get
            {
                if(_kasjer is null)
                {
                    var @out = new Kasjer
                    {
                        data_rozpoczecia = DateTime.Now,
                        idlogowania = logowanie.id
                    };
                    _kasjer = @out;
                }
                return _kasjer;
            }
            set
            {
                _kasjer = value;
            }
        }
        public Kasa(Sklep sklep, Logowanie logowanie, Kasjer kasjer = null, List<Klient> klienci_kolejka = null, List<Produkt> produkty_skasowane = null, StaticLine note = null) : base(sklep, note)
        {
            if (logowanie != null)
                this.logowanie = logowanie;
            if (kasjer != null)
                this.kasjer = kasjer;
            if (klienci_kolejka != null)
                this.klienci_kolejka = klienci_kolejka;
            if (produkty_skasowane != null)
                this.produkty_skasowane = produkty_skasowane;

            Contents.Add(new StaticLine("ZMIANA PRACOWNIKA " + this.logowanie.nazwa));
            Contents.Add(new StaticLine("Rozpoczęcie: " + this.kasjer.data_rozpoczecia));
            Contents.Add(new ActiveLine("Dodaj klienta do kolejki"));
            Contents.Add(new ActiveLine("Skasuj produkt"));
            Contents.Add(new ActiveLine("Finalizuj transakcję"));
            Contents.Add(new ActiveLine("Zakończ zmianę"));

            Contents.Add(new StaticLine("==SKASOWANE PRODUKTY=="));
            if (this.produkty_skasowane.Count == 0)
                Contents.Add(new StaticLine("Lista jest pusta"));
            else
            {
                decimal suma = 0;
                foreach(var p in this.produkty_skasowane)
                {
                    Contents.Add(new StaticLine($" {p.nazwa} ({p.cena}zł)"));
                    suma += p.cena;
                }
                Contents.Add(new StaticLine($"-DO zapłaty: {suma}zł"));
            }

            Contents.Add(new StaticLine("==KOLEJKA KLIENTÓW=="));
            if (this.klienci_kolejka.Count == 0)
                Contents.Add(new StaticLine("Lista jest pusta"));
            else
            {
                int licznikKlientow = 1;
                foreach (var k in this.klienci_kolejka)
                {
                    Contents.Add(new StaticLine($" Pozycja: {licznikKlientow}, wejście: {k.data_wejscia}"));
                    licznikKlientow++;
                }
            }
            Contents.Add(this.Note);
        }

        public override void React(_Line line)
        {
            switch(line.Index)
            {
                case 2:
                    var klient = new Klient
                    {
                        data_wejscia = DateTime.Now,
                        idkasjera = kasjer.id
                    };
                    klienci_kolejka.Add(klient);
                    break;
                case 3:
                    if(klienci_kolejka.Count != 0)
                        MWS.DisplayAdapter.Display(new Koszyk(sklep, logowanie, kasjer, klienci_kolejka, produkty_skasowane));
                    break;
                case 4:
                    if(klienci_kolejka.Count != 0 && produkty_skasowane.Count != 0)
                    {
                        klienci_kolejka[0].data_obsluzenia = DateTime.Now;
                        klienci_kolejka[0].idkasjera = kasjer.id;
                        klienci_kolejka[0] = BazaDanych.Wstaw(klienci_kolejka[0]);
                        foreach(var produkt in produkty_skasowane)
                        {
                            BazaDanych.Wstaw(klienci_kolejka[0], produkt);
                        }
                        klienci_kolejka.Remove(klienci_kolejka[0]);
                        produkty_skasowane = new List<Produkt>(99);
                    }
                    break;
                case 5:
                    if(klienci_kolejka.Count == 0)
                    {
                        kasjer.data_zakonczenia = DateTime.Now;
                        BazaDanych.Zmien(kasjer, kasjer);
                        MWS.DisplayAdapter.Display(new Zaloguj(sklep));
                    }
                    break;
            }
            MWS.DisplayAdapter.Display(new Kasa(sklep, logowanie, kasjer, klienci_kolejka, produkty_skasowane));
        }
    }
}