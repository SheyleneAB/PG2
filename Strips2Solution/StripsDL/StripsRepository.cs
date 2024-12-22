﻿using StripsBL.Model;
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
                                string adres;
                                if (!reader.IsDBNull(reader.GetOrdinal("Adres")))
                                {
                                    adres = reader.GetString(reader.GetOrdinal("Adres"));
                                }
                                else
                                {
                                    adres = string.Empty;
                                }

                                string reeksNaam = reader.GetString(reader.GetOrdinal("ReeksNaam"));
                                int reeksNummer;
                                if (!reader.IsDBNull(reader.GetOrdinal("ReeksNummer")))
                                {
                                    reeksNummer = reader.GetInt32(reader.GetOrdinal("ReeksNummer"));
                                }
                                else
                                {
                                    reeksNummer = 0;
                                }
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
        
        public void SchrijfStripReeksnr(Strip strip)
        {
            const string SQL = @"
               UPDATE strip
               SET reeksnummer = @Reeksnummer
               WHERE Titel = @Titel;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = SQL;
                    cmd.Parameters.AddWithValue("@Reeksnummer", strip.Reeks.Reeksnummer);
                    cmd.Parameters.AddWithValue("@Titel", strip.Titel);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void SchrijfStrip(Strip strip)
        {
            const string SQL = @"
                INSERT INTO Strip (Titel, ReeksId, UitgeverijId, ReeksNummer) 
                OUTPUT INSERTED.ID 
                VALUES (@Titel, @ReeksId, @UitgeverijId, @ReeksNummer);";

            const string SQLReeks = @"
                IF NOT EXISTS (SELECT 1 FROM Reeks WHERE Naam = @ReeksNaam)
                BEGIN
                    INSERT INTO Reeks (Naam) 
                    OUTPUT INSERTED.ID 
                    VALUES (@ReeksNaam);
                END
                ELSE
                BEGIN
                    SELECT Id FROM Reeks WHERE Naam = @ReeksNaam;
                END";

            const string SQLUitgeverij = @"
                IF NOT EXISTS (SELECT 1 FROM Uitgeverij WHERE Naam = @UitgeverijNaam)
                BEGIN
                    INSERT INTO Uitgeverij (Naam ) 
                    OUTPUT INSERTED.ID 
                    VALUES (@UitgeverijNaam);
                END
                ELSE
                BEGIN
                    SELECT Id FROM Uitgeverij WHERE Naam = @UitgeverijNaam;
                END";

            const string SQLAuteur = @"
                IF NOT EXISTS (SELECT 1 FROM Auteur WHERE Naam = @AuteurNaam)
                BEGIN
                    INSERT INTO Auteur (Naam, Email) 
                    OUTPUT INSERTED.ID 
                    VALUES (@AuteurNaam, @Email);
                END
                ELSE
                BEGIN
                    SELECT Id FROM Auteur WHERE Naam = @AuteurNaam;
                END";

            const string SQLStripAuteur = @"
                INSERT INTO StripAuteur (StripId, AuteurId) 
                VALUES (@StripId, @AuteurId);";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        int reeksId;
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = SQLReeks;
                            cmd.Parameters.AddWithValue("@ReeksNaam", strip.Reeks.Naam);
                            reeksId = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        int uitgeverijId;
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = SQLUitgeverij;
                            cmd.Parameters.AddWithValue("@UitgeverijNaam", strip.Uitgeverij.Naam);
                            uitgeverijId = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        int stripId;
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = SQL;
                            cmd.Parameters.AddWithValue("@Titel", strip.Titel);
                            cmd.Parameters.AddWithValue("@ReeksId", reeksId);
                            cmd.Parameters.AddWithValue("@ReeksNummer", strip.Reeks.Reeksnummer);
                            cmd.Parameters.AddWithValue("@UitgeverijId", uitgeverijId);
                            stripId = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        foreach (var auteur in strip.Auteurs)
                        {
                            int auteurId;
                            using (SqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.Transaction = transaction;
                                cmd.CommandText = SQLAuteur;
                                cmd.Parameters.AddWithValue("@AuteurNaam", auteur.Naam);
                                cmd.Parameters.AddWithValue("@Email", auteur.Email ?? (object)DBNull.Value);
                                auteurId = Convert.ToInt32(cmd.ExecuteScalar());
                            }

                            using (SqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.Transaction = transaction;
                                cmd.CommandText = SQLStripAuteur;
                                cmd.Parameters.AddWithValue("@StripId", stripId);
                                cmd.Parameters.AddWithValue("@AuteurId", auteurId);
                                cmd.ExecuteNonQuery();
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
        public void AddAuteurToStrip(int stripId, Auteur auteur)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "INSERT INTO StripAuteur (StripId, AuteurId) VALUES (@StripId, @AuteurId)";
                cmd.Parameters.AddWithValue("@StripId", stripId);
                cmd.Parameters.AddWithValue("@AuteurId", auteur.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public void RemoveAuteurFromStrip(int stripId, int auteurId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "DELETE FROM StripAuteur WHERE StripId = @StripId AND AuteurId = @AuteurId";
                cmd.Parameters.AddWithValue("@StripId", stripId);
                cmd.Parameters.AddWithValue("@AuteurId", auteurId);
                cmd.ExecuteNonQuery();
            }
        }



    }
}
