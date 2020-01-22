using KASA.Encje;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASA
{
    class BazaDanych
    {
        public static List<T> Rekordy<T>() where T : Encja
        {
            if (typeof(T) == typeof(Sklep))
            {
                return Sklep.Rekordy().Cast<T>().ToList();
            }
            else if (typeof(T) == typeof(Kasjer))
            {
                return Kasjer.Rekordy().Cast<T>().ToList();
            }
            else if (typeof(T) == typeof(Klient))
            {
                return Klient.Rekordy().Cast<T>().ToList();
            }
            else if (typeof(T) == typeof(Logowanie))
            {
                return Logowanie.Rekordy().Cast<T>().ToList();
            }
            else if (typeof(T) == typeof(Produkt))
            {
                return Produkt.Rekordy().Cast<T>().ToList();
            }
            throw new Exception("Nie sprecyzowano typu");
        }
        public static List<T> Kolekcje<T>() where T: Encja_Encja
        {
            if(typeof(T) == typeof(Sklep_Produkt))
            {
                return Sklep_Produkt.Kolekcje().Cast<T>().ToList();
            }
            else if(typeof(T) == typeof(Klient_Produkt))
            {
                return Klient_Produkt.Kolekcje().Cast<T>().ToList();
            }
            throw new Exception("Nie sprecyzowano typu");
        }
        public static T ZnajdzRekord<T>(int id) where T : Encja
        {
            if (typeof(T) == typeof(Sklep))
            {
                return Sklep.ZnajdzRekord(id) as T;
            }
            else if (typeof(T) == typeof(Kasjer))
            {
                return Kasjer.ZnajdzRekord(id) as T;
            }
            else if (typeof(T) == typeof(Klient))
            {
                return Klient.ZnajdzRekord(id) as T;
            }
            else if (typeof(T) == typeof(Logowanie))
            {
                return Logowanie.ZnajdzRekord(id) as T;
            }
            else if (typeof(T) == typeof(Produkt))
            {
                return Produkt.ZnajdzRekord(id) as T;
            }
            throw new Exception("Nie sprecyzowano typu");
        }
        public static T Wstaw<T>(T obiekt, Encja obiekt2 = null) where T : Encja
        {
            if (typeof(T) == typeof(Sklep))
            {
                if (obiekt2 is null)
                    return Sklep.Wstaw(obiekt as Sklep) as T;
                else if (obiekt2 is Produkt)
                    Sklep_Produkt.Wstaw(obiekt as Sklep, obiekt2 as Produkt);
            }
            else if (typeof(T) == typeof(Kasjer))
            {
                return Kasjer.Wstaw(obiekt as Kasjer) as T;
            }
            else if (typeof(T) == typeof(Klient))
            {
                if (obiekt2 is null)
                    return Klient.Wstaw(obiekt as Klient) as T;
                else if (obiekt2 is Produkt)
                    Klient_Produkt.Wstaw(obiekt as Klient, obiekt2 as Produkt);
            }
            else if (typeof(T) == typeof(Logowanie))
            {
                return Logowanie.Wstaw(obiekt as Logowanie) as T;
            }
            else if (typeof(T) == typeof(Produkt))
            {
                return Produkt.Wstaw(obiekt as Produkt) as T;
            }
            return null;
        }
        public static void Zmien<T>(T obiektPrzed, T obiektPo = null) where T : Encja
        {
            obiektPo.id = obiektPrzed.id;
            if (typeof(T) == typeof(Sklep))
            {
                Sklep.Zmien(obiektPo as Sklep);
            }
            else if (typeof(T) == typeof(Kasjer))
            {
                Kasjer.Zmien(obiektPo as Kasjer);
            }
            else if (typeof(T) == typeof(Klient))
            {
                Klient.Zmien(obiektPo as Klient);
            }
            else if (typeof(T) == typeof(Logowanie))
            {
                Logowanie.Zmien(obiektPo as Logowanie);
            }
            else if (typeof(T) == typeof(Produkt))
            {
                Produkt.Zmien(obiektPo as Produkt);
            }
        }
        public static void Usun<T>(T obiekt, Encja obiekt2 = null) where T : Encja
        {
            if (typeof(T) == typeof(Sklep))
            {
                if (obiekt2 is null)
                    Sklep.Usun(obiekt as Sklep);
                else if (obiekt2 is Produkt)
                    Sklep_Produkt.Usun(obiekt as Sklep, obiekt2 as Produkt);
            }
            else if (typeof(T) == typeof(Kasjer))
            {
                Kasjer.Usun(obiekt as Kasjer);
            }
            else if (typeof(T) == typeof(Klient))
            {
                if (obiekt2 is null)
                    Klient.Usun(obiekt as Klient);
                else if (obiekt2 is Produkt)
                    Klient_Produkt.Wstaw(obiekt as Klient, obiekt2 as Produkt);
            }
            else if (typeof(T) == typeof(Logowanie))
            {
                Logowanie.Usun(obiekt as Logowanie);
            }
            else if (typeof(T) == typeof(Produkt))
            {
                Produkt.Usun(obiekt as Produkt);
            }
        }
    }
}
