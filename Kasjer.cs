using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASA.Encje
{
    class Kasjer : Encja
    {
        public DateTime data_rozpoczecia { get; set; }
        public DateTime data_zakonczenia { get; set; }
        public int idlogowania { get; set; }
        public Logowanie logowanie
        {
            get
            {
                return BazaDanych.ZnajdzRekord<Logowanie>(idlogowania);
            }
        }
        public List<Klient> klienci
        {
            get
            {
                var @out = new List<Klient>(99);
                foreach(var k in BazaDanych.Rekordy<Klient>())
                {
                    if(k.idkasjera == id)
                    {
                        @out.Add(k);
                    }
                }
                return @out;
            }
        }
        public static List<Kasjer> Rekordy()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Konfigurator.ConnectionString("cnKASA")))
            {
                try
                {
                    return connection.Query<Kasjer>("SELECT * FROM Kasjer").ToList();
                }
                catch (InvalidOperationException)
                {
                    return null;
                }
            }
        }
        public static void Usun(Kasjer obiekt)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Konfigurator.ConnectionString("cnKASA")))
            {
                connection.Execute("DELETE FROM Kasjer WHERE id = @id", obiekt);
            }
        }
        public static Kasjer Wstaw(Kasjer obiekt)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Konfigurator.ConnectionString("cnKASA")))
            {
                return connection.QuerySingle<Kasjer>("INSERT INTO Kasjer(data_rozpoczecia, data_zakonczenia, idlogowania) OUTPUT INSERTED.* VALUES(@data_rozpoczecia,@data_zakonczenia,@idlogowania)", obiekt) as Kasjer;
            }
        }
        public static void Zmien(Kasjer obiekt)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Konfigurator.ConnectionString("cnKASA")))
            {
                connection.Execute("UPDATE Kasjer SET  data_rozpoczecia=@data_rozpoczecia, data_zakonczenia=@data_zakonczenia, idlogowania=@idlogowania", obiekt);
            }
        }
        public static Kasjer ZnajdzRekord(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Konfigurator.ConnectionString("cnKASA")))
            {
                return connection.QuerySingle<Kasjer>("SELECT * FROM Kasjer WHERE id=@id", new { id }) as Kasjer;
            }
        }
    }
}