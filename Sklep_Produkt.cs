using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASA.Encje
{
    class Sklep_Produkt: Encja_Encja
    {
        public int id1 { get; set; }
        public int id2 { get; set; }
        public int idsklepu
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
        public static void Usun(Sklep sklep, Produkt produkt)
        {
            Sklep_Produkt sp = new Sklep_Produkt
            {
                id1 = sklep.id,
                id2 = produkt.id
            };
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Konfigurator.ConnectionString("cnKASA")))
            {
                connection.Execute("DELETE FROM Sklep_Produkt WHERE idsklepu=@idsklepu AND idproduktu=@idproduktu", sp);
            }
        }
        public static void Wstaw(Sklep sklep, Produkt produkt)
        {
            if (BazaDanych.ZnajdzRekord<Sklep>(sklep.id) == null)
            {
                sklep = BazaDanych.Wstaw(sklep);
            }
            if (BazaDanych.ZnajdzRekord<Produkt>(produkt.id) == null)
            {
                produkt = BazaDanych.Wstaw(produkt);
            }
            Sklep_Produkt sp = new Sklep_Produkt
            {
                id1 = sklep.id,
                id2 = produkt.id
            };
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Konfigurator.ConnectionString("cnKASA")))
            {
                connection.Execute("INSERT INTO Sklep_Produkt(idsklepu, idproduktu) VALUES (@idsklepu,@idproduktu)", sp);
            }
        }
        public static List<Sklep_Produkt> Kolekcje()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Konfigurator.ConnectionString("cnKASA")))
            {
                try
                {
                    return connection.Query<Sklep_Produkt>("SELECT * FROM Sklep_Produkt").ToList();
                }
                catch (InvalidOperationException)
                {
                    return null;
                }
            }
        }
    }
}
