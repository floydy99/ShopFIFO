using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASA.Encje
{
    class Logowanie : Encja
    {
        public string nazwa { get; set; } = "";
        public string haslo { get; set; } = "";
        public int idsklepu { get; set; }

        public static List<Logowanie> Rekordy()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Konfigurator.ConnectionString("cnKASA")))
            {
                try
                {
                    return connection.Query<Logowanie>("SELECT * FROM Logowanie").ToList();
                }
                catch (InvalidOperationException)
                {
                    return null;
                }
            }
        }
        public static void Usun(Logowanie obiekt)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Konfigurator.ConnectionString("cnKASA")))
            {
                connection.Execute("DELETE FROM Logowanie WHERE id = @id", obiekt);
            }
        }
        public static Logowanie Wstaw(Logowanie obiekt)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Konfigurator.ConnectionString("cnKASA")))
            {
                return connection.QuerySingle<Logowanie>("INSERT INTO Logowanie(nazwa,haslo,idsklepu) OUTPUT INSERTED.* VALUES(@nazwa,@haslo,@idsklepu)", obiekt) as Logowanie;
            }
        }
        public static void Zmien(Logowanie obiekt)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Konfigurator.ConnectionString("cnKASA")))
            {
                connection.Execute("UPDATE Logowanie SET  nazwa=@nazwa, haslo=@haslo, idsklepu = @idsklepu", obiekt);
            }
        }
        public static Logowanie ZnajdzRekord(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Konfigurator.ConnectionString("cnKASA")))
            {
                return connection.QuerySingle<Logowanie>("SELECT * FROM Logowanie WHERE id=@id", new { id }) as Logowanie;
            }
        }
    }
}