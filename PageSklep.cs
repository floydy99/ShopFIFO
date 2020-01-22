using KASA.Encje;
using MWS.Lines;
using MWS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASA.Strony
{
    abstract class PageSklep : _Page
    {
        public Sklep sklep { get; set; }

        public PageSklep(Sklep sklep, StaticLine note = null): base(note)
        {
            this.sklep = sklep;
        }
    }
}
