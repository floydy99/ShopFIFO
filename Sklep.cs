using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASA.Encje
{
    class Sklep : Encja
    {
        public string nazwa { get; set; } = "";
        public string adres { get; set; } = "";
        public List<Produkt> asortyment
        {
            get
            {
                var sp = BazaDanych.Kolekcje<Sklep_Produkt>();
                List<Produkt> @out = new List<Produkt>(99);
                foreach (var egz in sp)
                {
                    if (egz.idsklepu == id)
                    {
                        @out.Add(BazaDanych.ZnajdzRekord<Produkt>(egz.idproduktu));
                    }
                }
                return @out;
            }
        }
        public List<Kasjer> kasjerzy
        {
            get
            {
                var @out = new List<Kasjer>(99);
                foreach (var k in BazaDanych.Rekordy<Kasjer>())
                {
                    if(k.logowanie.idsklepu == id)
                    {
                        @out.Add(k);
                    }
                }
                return @out;
            }
        }
        public static List<Sklep> Rekordy()
         {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Konfigurator.ConnectionString("cnKASA")))
            {
                try
                {
                    return connection.Query<Sklep>("SELECT * FROM SKLEP").ToList();
                }
                catch (InvalidOperationException)
                {
                    return null;
                }
            }
        }

        public static void Usun(Sklep obiekt)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Konfigurator.ConnectionString("cnKASA")))
            {
                connection.Execute("DELETE FROM SKLEP WHERE id = @id", obiekt);
            }
        }

        public static Sklep Wstaw(Sklep obiekt)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Konfigurator.ConnectionString("cnKASA")))
            {
                return connection.QuerySingle<Sklep>("INSERT INTO SKLEP(nazwa,adres) OUTPUT INSERTED.* VALUES(@nazwa,@adres)", obiekt) as Sklep;
            }
        }

        public static void Zmien (Sklep obiekt)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Konfigurator.ConnectionString("cnKASA")))
            {
                connection.Execute("UPDATE SKLEP SET  nazwa=@nazwa, adres=@adres", obiekt);
            }
        }
        public static Sklep ZnajdzRekord(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Konfigurator.ConnectionString("cnKASA")))
            {
                return connection.QuerySingle<Sklep>("SELECT * FROM SKLEP WHERE id=@id", new { id }) as Sklep;
            }
        }
    }
}
