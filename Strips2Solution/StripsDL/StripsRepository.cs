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
            const string SQL = @"
                INSERT INTO Strip (Titel, ReeksId, UitgeverijId) 
                OUTPUT INSERTED.ID 
                VALUES (@Titel, @ReeksId, @UitgeverijId);";

            const string SQLReeks = @"
                insert into Reeks(naam) OUTPUT INSERTED.ID values (@ReeksNaam);";

            const string SQLUitgeverij = @"
                insert into Uitgeverij(naam, adres) OUTPUT INSERTED.ID values (@UitgeverijNaam, @Adres);";

            const string SQLAuteur = @"'
                insert into Auteur(naam, email) OUTPUT INSERTED.ID values (@AuteurNaam, @Email);";

            const string SQLStripAuteur = @"
                INSERT INTO StripAuteur () 
                VALUES (@StripId, @AuteurId);";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = SQLReeks;

                            cmd.Parameters.AddWithValue("@ReeksNaam", strip.Reeks.Naam);
                            
                        }
                        int stripId;
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = SQL;

                            cmd.Parameters.AddWithValue("@Titel", strip.Titel);
                            stripId = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = SQL;

                            cmd.Parameters.AddWithValue("@Titel", strip.Titel);
                            stripId = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        using (SqlCommand cmdAuteur = conn.CreateCommand())
                        {
                            cmdAuteur.Transaction = transaction;
                            cmdAuteur.CommandText = SQLStripAuteur;

                            cmdAuteur.Parameters.Add(new SqlParameter("@StripId", SqlDbType.Int));
                            cmdAuteur.Parameters.Add(new SqlParameter("@AuteurId", SqlDbType.Int));

                            foreach (var auteur in strip.Auteurs)
                            {
                                cmdAuteur.Parameters["@StripId"].Value = stripId;
                                cmdAuteur.Parameters["@AuteurId"].Value = auteur.Id;

                                cmdAuteur.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Error writing strip", ex);
                    }
                }
            }
        }


    }
}
