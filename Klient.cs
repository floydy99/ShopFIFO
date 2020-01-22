using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASA.Encje
{
    class Klient : Encja
    {
        public DateTime data_wejscia { get; set; }
        public DateTime data_obsluzenia { get; set; }
        public int idkasjera { get; set; }
        public List<Produkt> koszyk
        {
            get
            {
                var kp = BazaDanych.Kolekcje<Klient_Produkt>();
                List<Produkt> @out = new List<Produkt>(99);
                foreach(var egz in kp)
                {
                    if(egz.idklienta == id)
                    {
                        @out.Add(BazaDanych.ZnajdzRekord<Produkt>(egz.idproduktu));
                    }
                }
                return @out;
            }
        }
        public static List<Klient> Rekordy()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Konfigurator.ConnectionString("cnKASA")))
            {
                try
                {
                    return connection.Query<Klient>("SELECT * FROM Klient").ToList();
                }
                catch (InvalidOperationException)
                {
                    return null;
                }
            }
        }
        public static void Usun(Klient obiekt)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Konfigurator.ConnectionString("cnKASA")))
            {
                connection.Execute("DELETE FROM Klient WHERE id = @id", obiekt);
            }
        }
        public static Klient Wstaw(Klient obiekt)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Konfigurator.ConnectionString("cnKASA")))
            {
                return connection.QuerySingle<Klient>("INSERT INTO Klient(data_wejscia,data_obsluzenia, idkasjera) OUTPUT INSERTED.* VALUES(@data_wejscia,@data_obsluzenia,@idkasjera)", obiekt) as Klient;
            }
        }
        public static void Zmien(Klient obiekt)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Konfigurator.ConnectionString("cnKASA")))
            {
                connection.Execute("UPDATE Klient SET  data_wejscia=@data_wejscia, data_obsluzenia=@data_obsluzenia, idkasjera=@idkasjera", obiekt);
            }
        }
        public static Klient ZnajdzRekord(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Konfigurator.ConnectionString("cnKASA")))
            {
                return connection.QuerySingle<Klient>("SELECT * FROM Klient WHERE id=@id", new { id }) as Klient;
            }
        }
    }
}