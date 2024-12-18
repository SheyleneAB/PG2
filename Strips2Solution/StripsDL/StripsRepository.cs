using StripsBL.Model;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using StripsBL.Interfaces;
using System.Data;

namespace StripsDL
{
    public class StripsRepository : IStripsRepository
    {
        private string connectionString;

        public StripsRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Strip GeefStrip(int id)
        {
            string SQL = "SELECT *, auteur.Naam as Auteurnaam, Uitgeverij.Naam as Uitgevernaam, Reeks.Naam as Reeksnaam" +
                         " FROM Strip" +
                         " JOIN Reeks ON Reeks.Id = Strip.ReeksId" +
                         " JOIN Uitgeverij ON Uitgeverij.Id = Strip.UitgeverijId" +
                         " JOIN StripAuteur ON Strip.Id = StripAuteur.StripId" +
                         " JOIN Auteur ON StripAuteur.AuteurId = Auteur.Id" +
                         " WHERE Strip.Id = @Id;";

            Strip strip = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = SQL;
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (strip == null)
                            {
                                string titel = reader.GetString(reader.GetOrdinal("Titel"));
                                int uitgeverijId = reader.GetInt32(reader.GetOrdinal("UitgeverijId"));
                                string adres = reader.GetString(reader.GetOrdinal("Adres"));
                                string reeksNaam = reader.GetString(reader.GetOrdinal("ReeksNaam"));
                                int reeksNummer = reader.GetInt32(reader.GetOrdinal("ReeksNummer"));
                                int reeksId = reader.GetInt32(reader.GetOrdinal("ReeksId"));
                                string uitgeverijNaam = reader.GetString(reader.GetOrdinal("UitgeverNaam"));

                                Uitgeverij uitgeverij = new Uitgeverij(uitgeverijId, uitgeverijNaam, adres);
                                Reeks reeks = new Reeks(reeksNaam, reeksId, reeksNummer);
                                strip = new Strip(titel, reeks, uitgeverij);
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("AuteurId")))
                            {
                                int auteurId = reader.GetInt32(reader.GetOrdinal("AuteurId"));
                                string auteurNaam = reader.GetString(reader.GetOrdinal("Auteurnaam"));
                                Auteur auteur = new Auteur(auteurId, auteurNaam, null);
                                strip.VoegAuteurToe(auteur);
                            }
                        }
                    }

                    return strip;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error strip", ex);
                }
            }
        }

        public void SchrijfStrip(Strip strip)
        {
            string SQL = "INSERT INTO offerte(datum, klantid, afhalenbool, plaatsenbool) output INSERTED.ID VALUES( @Datum, @Klantid, @Afhalenbool, @Plaatsenbool)";
            string SQLAU = "INSERT INTO offerteklantaantal(offerteid, productid, aantal) VALUES(@OfferteId, @ProductId, @Aantal)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    int offerteId;
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = SQL;

                            cmd.Parameters.Add(new SqlParameter("@Datum", System.Data.SqlDbType.DateTime)).Value = offerte.Datum;
                            cmd.Parameters.Add(new SqlParameter("@Klantid", System.Data.SqlDbType.Int)).Value = offerte.Klant.Id;
                            cmd.Parameters.Add(new SqlParameter("@Afhalenbool", System.Data.SqlDbType.Bit)).Value = offerte.AfhalenBool;
                            cmd.Parameters.Add(new SqlParameter("@Plaatsenbool", System.Data.SqlDbType.Bit)).Value = offerte.PlaatsenBool;

                            offerteId = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        using (SqlCommand cmdProd = conn.CreateCommand())
                        {
                            cmdProd.Transaction = transaction;
                            cmdProd.CommandText = SQLprod;

                            cmdProd.Parameters.Add(new SqlParameter("@OfferteId", System.Data.SqlDbType.Int));
                            cmdProd.Parameters.Add(new SqlParameter("@ProductId", System.Data.SqlDbType.Int));
                            cmdProd.Parameters.Add(new SqlParameter("@Aantal", System.Data.SqlDbType.Int));

                            foreach (var producten in offerte.Producten)
                            {
                                cmdProd.Parameters["@OfferteId"].Value = offerteId;
                                cmdProd.Parameters["@ProductId"].Value = producten.Key.Id;
                                cmdProd.Parameters["@Aantal"].Value = producten.Value;

                                cmdProd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        
    }
}
