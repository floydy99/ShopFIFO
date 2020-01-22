using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASA.Encje
{
    class Klient_Produkt : Encja_Encja
    {
        public int id1 { get; set; }
        public int id2 { get; set; }
        public int idklienta
        {
            get
            {
                return id1;
            }
            set
            {
                id1 = value;
            }
        }
        public int idproduktu
        {
            get
            {
                return id2;
            }
            set
            {
                id2 = value;
            }
        }
        public static void Usun(Klient klient, Produkt produkt)
        {
            Sklep_Produkt kp = new Sklep_Produkt
            {
                id1 = klient.id,
                id2 = produkt.id
            };
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Konfigurator.ConnectionString("cnKASA")))
            {
                connection.Execute("DELETE FROM Klient_Produkt WHERE idklienta=@idklienta AND idproduktu=@idproduktu", kp);
            }
        }
        public static void Wstaw(Klient klient, Produkt produkt)
        {
            if (BazaDanych.ZnajdzRekord<Klient>(klient.id) == null)
            {
                klient = BazaDanych.Wstaw(klient);
            }
            if (BazaDanych.ZnajdzRekord<Produkt>(produkt.id) == null)
            {
                produkt = BazaDanych.Wstaw(produkt);
            }
            Klient_Produkt kp = new Klient_Produkt
            {
                id1 = klient.id,
                id2 = produkt.id
            };
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Konfigurator.ConnectionString("cnKASA")))
            {
                connection.Execute("INSERT INTO Klient_Produkt(idklienta, idproduktu) VALUES (@idklienta,@idproduktu)", kp);
            }
        }
        public static List<Klient_Produkt> Kolekcje()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Konfigurator.ConnectionString("cnKASA")))
            {
                try
                {
                    return connection.Query<Klient_Produkt>("SELECT * FROM Klient_Produkt").ToList();
                }
                catch (InvalidOperationException)
                {
                    return null;
                }
            }
        }
    }
}