using KASA.Encje;
using MWS.Lines;
using MWS.Pages;
using System;

namespace KASA.Strony
{
    internal class DodajSklep : _Page
    {
        Sklep sklep = new Sklep();
        public DodajSklep(Sklep sklep = null, StaticLine Note = null) : base(Note)
        {
            if (sklep != null)
                this.sklep = sklep;
            Contents.Add(new StaticLine("DODAWANIE SKLEPU"));
            Contents.Add(new ActiveLine("Nazwa: " + this.sklep.nazwa));
            Contents.Add(new ActiveLine("Adres: " + this.sklep.adres));
            Contents.Add(new ActiveLine("Dodaj"));
            Contents.Add(new ActiveLine("Powrót"));
            Contents.Add(this.Note);
        }

        public override void React(_Line line)
        {
            switch(line.Index)
            {
                case 1:
                    sklep.nazwa = Console.ReadLine();
                    break;
                case 2:
                    sklep.adres = Console.ReadLine();
                    break;
                case 3:
                    if(!sklep.IsAnyNullOrEmpty())
                    {
                        sklep = BazaDanych.Wstaw(sklep);
                        MWS.DisplayAdapter.Display(new Zaloguj(sklep));
                    }
                    break;
                case 4:
                    MWS.DisplayAdapter.Display(new Sklepy());
                    break;
            }
            MWS.DisplayAdapter.Display(new DodajSklep(sklep));
        }
    }
}