using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASA.Encje
{
    class Produkt : Encja
    {
        public string nazwa { get; set; } = "";
        public decimal cena { get; set; } = 0;
        public static List<Produkt> Rekordy()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Konfigurator.ConnectionString("cnKASA")))
            {
                try
                {
                    return connection.Query<Produkt>("SELECT * FROM Produkt").ToList();
                }
                catch (InvalidOperationException)
                {
                    return null;
                }
            }
        }
        public static void Usun(Produkt obiekt)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Konfigurator.ConnectionString("cnKASA")))
            {
                connection.Execute("DELETE FROM Produkt WHERE id = @id", obiekt);
            }
        }
        public static Produkt Wstaw(Produkt obiekt)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Konfigurator.ConnectionString("cnKASA")))
            {
                return connection.QuerySingle<Produkt>("INSERT INTO Produkt(nazwa,cena) OUTPUT INSERTED.* VALUES(@nazwa,@cena)", obiekt) as Produkt;
            }
        }
        public static void Zmien(Produkt obiekt)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Konfigurator.ConnectionString("cnKASA")))
            {
                connection.Execute("UPDATE Produkt SET nazwa=@nazwa, cena=@cena", obiekt);
            }
        }
        public static Produkt ZnajdzRekord(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Konfigurator.ConnectionString("cnKASA")))
            {
                return connection.QuerySingle<Produkt>("SELECT * FROM Produkt WHERE id=@id", new { id }) as Produkt;
            }
        }
    }
}