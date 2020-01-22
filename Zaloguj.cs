using KASA.Encje;
using MWS.Lines;
using MWS.Pages;
using System;

namespace KASA.Strony
{
    internal class Zaloguj : PageSklep
    {
        Logowanie logowanie = new Logowanie();
        public Zaloguj(Sklep sklep, Logowanie logowanie = null, StaticLine Note = null): base(sklep, Note)
        {
            if(logowanie != null)
                this.logowanie = logowanie;

            Contents.Add(new StaticLine("WYBRANY SKLEP: " + this.sklep.nazwa.ToUpper()));
            Contents.Add(new ActiveLine(" Login: " + this.logowanie.nazwa));
            Contents.Add(new ActiveLine(" Hasło: " + this.logowanie.haslo));
            Contents.Add(new ActiveLine(" Zaloguj się i rozpocznij zmianę"));
            Contents.Add(new ActiveLine(" Zarejestruj kasjera"));
            Contents.Add(new ActiveLine("Statystyki sklepu"));
            Contents.Add(new ActiveLine("Asortyment sklepu"));
            Contents.Add(new ActiveLine("Powrót"));
            Contents.Add(this.Note);
        }

        public override void React(_Line line)
        {
            switch(line.Index)
            {
                case 1:
                    logowanie.nazwa = Console.ReadLine();
                    break;
                case 2:
                    logowanie.haslo = Console.ReadLine();
                    break;
                case 3:
                    if(!logowanie.IsAnyNullOrEmpty())
                    {
                        var logowania = BazaDanych.Rekordy<Logowanie>();
                        foreach(var log in logowania)
                        {
                            if(log.nazwa == logowanie.nazwa && log.haslo == logowanie.haslo && sklep.asortyment.Count > 0)
                            {
                                var kasjer = new Kasjer();
                                kasjer.idlogowania = log.id;
                                kasjer.data_rozpoczecia = DateTime.Now;
                                kasjer.data_zakonczenia = DateTime.Now;
                                kasjer = BazaDanych.Wstaw(kasjer);
                                MWS.DisplayAdapter.Display(new Kasa(sklep, log, kasjer));
                            }
                        }
                    }
                    break;
                case 4:
                    MWS.DisplayAdapter.Display(new Rejestracja(sklep));
                    break;
                case 5:
                    MWS.DisplayAdapter.Display(new Statystyki(sklep));
                    break;
                case 6:
                    MWS.DisplayAdapter.Display(new Asortyment(sklep));
                    break;
                case 7:
                    MWS.DisplayAdapter.Display(new Sklepy());
                    break;
            }
            MWS.DisplayAdapter.Display(new Zaloguj(sklep, logowanie));
        }
    }
}