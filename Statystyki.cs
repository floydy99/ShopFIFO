using KASA.Encje;
using MWS.Lines;
using MWS.Pages;
using System.Collections.Generic;

namespace KASA.Strony
{
    internal class Statystyki : PageSklep
    {
        public Statystyki(Sklep sklep, StaticLine note = null) : base(sklep, note)
        {
            Contents.Add(new StaticLine("HISTORIA"));
            Contents.Add(new ActiveLine("Powrót"));
            foreach(var k in sklep.kasjerzy)
            {
                Contents.Add(new StaticLine("========="));
                Contents.Add(new StaticLine($"KASJER: {k.logowanie.nazwa}"));
                Contents.Add(new StaticLine($" DATA ROZPOCZĘCIA: {k.data_rozpoczecia}"));
                Contents.Add(new StaticLine($" DATA ZAKOŃCZENIA: {k.data_zakonczenia}"));
                Contents.Add(new StaticLine($" CZAS PRACY: {k.data_zakonczenia-k.data_rozpoczecia}"));
                foreach(var klient in k.klienci)
                {
                    Contents.Add(new StaticLine($"  KLIENT: {klient.id}"));
                    Contents.Add(new StaticLine($"   DATA WEJŚCIA:    {klient.data_wejscia}"));
                    Contents.Add(new StaticLine($"   DATA OBSŁUŻENIA: {klient.data_obsluzenia}"));
                    Contents.Add(new StaticLine($"   CZAS OCZEKIWANIA: {klient.data_obsluzenia - klient.data_wejscia}"));
                    foreach(var produkt in klient.koszyk)
                    {
                        Contents.Add(new StaticLine($"    PRODUKT: {produkt.nazwa}, CENA: {produkt.cena}"));
                    }
                }
            }
            Contents.Add(this.Note);
        }
        public override void React(_Line line)
        {
            MWS.DisplayAdapter.Display(new Zaloguj(sklep));
        }
    }
}