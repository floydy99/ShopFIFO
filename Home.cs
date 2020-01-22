using MWS.Lines;
using MWS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASA.Strony
{
    class Home : _Page
    {
        public Home(StaticLine Note = null) : base(Note)
        {
            Contents.Add(new StaticLine("Strona Główna"));
            Contents.Add(new ActiveLine("Rozpocznij pracę"));
            Contents.Add(new ActiveLine("Dodaj bazę danych"));
            Contents.Add(this.Note);
        }

        public override void React(_Line line)
        {
            switch(line.Index)
            {
                case 1:
                    if(Konfigurator.CheckConnection())
                    {
                        MWS.DisplayAdapter.Display(new Sklepy());
                    }
                    break;
                case 2:
                    if(!Konfigurator.CheckConnection())
                        Konfigurator.InstallDatabase();
                    MWS.DisplayAdapter.Display(new Home());
                    break;
            }
        }
    }
}
