using KASA.Encje;
using MWS.Lines;
using MWS.Pages;
using System;

namespace KASA.Strony
{
    internal class Rejestracja : PageSklep
    {
        public Logowanie logowanie = new Logowanie();
        public Rejestracja(Sklep sklep, Logowanie logowanie = null, StaticLine Note = null) : base(sklep, Note)
        {
            if (logowanie != null)
                this.logowanie = logowanie;
            Contents.Add(new StaticLine("REJESTRACJA"));
            Contents.Add(new ActiveLine("Login: " + this.logowanie.nazwa));
            Contents.Add(new ActiveLine("Hasło: " + this.logowanie.haslo));
            Contents.Add(new ActiveLine("Zarejestruj się"));
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
                        logowanie.idsklepu = sklep.id;
                        BazaDanych.Wstaw(logowanie);
                        MWS.DisplayAdapter.Display(new Zaloguj(sklep));
                    }
                    break;
                case 4:
                    MWS.DisplayAdapter.Display(new Zaloguj(sklep));
                    break;
            }
            MWS.DisplayAdapter.Display(new Rejestracja(sklep, logowanie));
        }
    }
}