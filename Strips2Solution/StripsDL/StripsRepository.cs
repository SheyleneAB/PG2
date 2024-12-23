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
        public List<Strip> GeefReeksMStrip(int reeksnummer)
        {
            string SQL = "SELECT Strip.*, Reeks.Naam as ReeksNaam, Uitgeverij.Naam as UitgeverNaam, Auteur.Naam as Auteurnaam, Auteur.Id as AuteurId" +
                         " FROM Strip" +
                         " JOIN Reeks ON Reeks.Id = Strip.ReeksId" +
                         " JOIN Uitgeverij ON Uitgeverij.Id = Strip.UitgeverijId" +
                         " JOIN StripAuteur ON Strip.Id = StripAuteur.StripId" +
                         " JOIN Auteur ON StripAuteur.AuteurId = Auteur.Id" +
                         " WHERE Reeks.Reeksnummer = @Reeksnummer;";

            List<Strip> strips = new List<Strip>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = SQL;
                    cmd.Parameters.AddWithValue("@Reeksnummer", reeksnummer);

                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int stripId = reader.GetInt32(reader.GetOrdinal("Id"));
                            Strip strip = strips.FirstOrDefault(s => s.Id == stripId);
                            if (strip == null)
                            {
                                string titel = reader.GetString(reader.GetOrdinal("Titel"));
                                int uitgeverijId = reader.GetInt32(reader.GetOrdinal("UitgeverijId"));
                                string adres = reader.IsDBNull(reader.GetOrdinal("Adres")) ? string.Empty : reader.GetString(reader.GetOrdinal("Adres"));
                                string reeksNaam = reader.GetString(reader.GetOrdinal("ReeksNaam"));
                                int reeksId = reader.GetInt32(reader.GetOrdinal("ReeksId"));
                                string uitgeverijNaam = reader.GetString(reader.GetOrdinal("UitgeverNaam"));
                                int reeksNummer = reader.IsDBNull(reader.GetOrdinal("ReeksNummer")) ? 0 : reader.GetInt32(reader.GetOrdinal("ReeksNummer"));

                                Uitgeverij uitgeverij = new Uitgeverij(uitgeverijId, uitgeverijNaam, adres);
                                Reeks reeks = new Reeks(reeksNaam, reeksId, reeksNummer);
                                strip = new Strip(titel, reeks, uitgeverij) { Id = stripId };
                                strips.Add(strip);
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
                }
                catch (Exception ex)
                {
                    throw new Exception("Error retrieving strips by series number", ex);
                }
            }
            return strips;
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
        public bool HeeftAuteur(Auteur auteur)
        {
            string query = "SELECT count(*) FROM dbo.Auteur WHERE Naam=@Naam";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@Naam", auteur.Naam);
                    int n = (int)command.ExecuteScalar();
                    return n > 0;
                }
                catch (Exception ex)
                {
                    Exception dbex = new Exception("HeeftAuteur niet gelukt", ex);
                    dbex.Data.Add("Auteur", auteur);
                    throw dbex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public bool HeeftStrip (Strip strip)
        {
            string query = "SELECT count(*) FROM dbo.Strip WHERE Titel=@Titel";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@Titel", strip.Titel);
                    int n = (int)command.ExecuteScalar();
                    return n > 0;
                }
                catch (Exception ex)
                {
                    Exception dbex = new Exception("HeeftStrip niet gelukt", ex);
                    dbex.Data.Add("Strip", strip);
                    throw dbex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public bool HeeftReeks (Reeks reeks)
        {
            string query = "SELECT count(*) FROM dbo.Reeks WHERE Naam=@Naam";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@Naam", reeks.Naam);
                    int n = (int)command.ExecuteScalar();
                    return n > 0;
                }
                catch (Exception ex)
                {
                    Exception dbex = new Exception("HeeftReeks niet gelukt", ex);
                    dbex.Data.Add("Reeks", reeks);
                    throw dbex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public bool HeeftUitgeverij(Uitgeverij uitgeverij)
        {
            string query = "SELECT count(*) FROM dbo.Uitgeverij WHERE Naam=@Naam";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@Naam", uitgeverij.Naam);
                    int n = (int)command.ExecuteScalar();
                    return n > 0;
                }
                catch (Exception ex)
                {
                    Exception dbex = new Exception("HeeftUitgeverij niet gelukt", ex);
                    dbex.Data.Add("Uitgeverij", uitgeverij);
                    throw dbex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public void AddAuteurToStrip(int stripId, int auteurid)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "INSERT INTO StripAuteur (StripId, AuteurId) VALUES (@StripId, @AuteurId)";
                cmd.Parameters.AddWithValue("@StripId", stripId);
                cmd.Parameters.AddWithValue("@AuteurId", auteurid);
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
        public void UpdateStripTitel(Strip strip)
        {
            string query = "UPDATE dbo.strip SET Titel=@Titel WHERE Strip.id =@Stripid";
            SqlConnection conn = new SqlConnection(connectionString);
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.Add(new SqlParameter("@Stripid", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@Titel", SqlDbType.NVarChar));
                    command.Parameters["@Stripid"].Value = strip.Id;
                    command.Parameters["@Titel"].Value = strip.Titel;
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Exception dbex = new Exception("UpdateStripTitel not successful", ex);
                    dbex.Data.Add("Strip", strip);
                    throw dbex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public void UpdateUitgeverijgeg(Uitgeverij uitgeverij)
        {
            string query = "UPDATE dbo.Uitgeverij SET Naam=@Naam, Adres=@Adres WHERE Uitgeverij.id =@Uitgeverijid";
            SqlConnection conn = new SqlConnection(connectionString);
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.Add(new SqlParameter("@Uitgeverijid", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@Naam", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Adres", SqlDbType.NVarChar));
                    command.Parameters["@Uitgeverijid"].Value = uitgeverij.UitgeverijId;
                    command.Parameters["@Naam"].Value = uitgeverij.Naam;
                    command.Parameters["@Adres"].Value = uitgeverij.Adres;
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Exception dbex = new Exception("UpdateUitgeverijgeg not successful", ex);
                    dbex.Data.Add("Uitgeverij", uitgeverij);
                    throw dbex;
                }
                finally
                {
                    conn.Close();
                }
            }


        }
        public void UpdateAuteurgeg(Auteur auteur)
        {
            string query = "UPDATE dbo.Auteur SET Naam=@Naam, Email=@Email WHERE Auteur.id =@Auteurid";
            SqlConnection conn = new SqlConnection(connectionString);
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.Add(new SqlParameter("@Auteurid", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@Naam", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar));
                    command.Parameters["@Auteurid"].Value = auteur.Id;
                    command.Parameters["@Naam"].Value = auteur.Naam;
                    command.Parameters["@Email"].Value = auteur.Email;
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Exception dbex = new Exception("UpdateAuteurgeg not successful", ex);
                    dbex.Data.Add("Auteur", auteur);
                    throw dbex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public void VeranderReeks(Strip strip, Reeks reeks)
        {
            string query = "UPDATE dbo.strip SET ReeksId=@ReeksId WHERE Strip.id =@Stripid";
            SqlConnection conn = new SqlConnection(connectionString);
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.Add(new SqlParameter("@Stripid", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@ReeksId", SqlDbType.Int));
                    command.Parameters["@Stripid"].Value = strip.Id;
                    command.Parameters["@ReeksId"].Value = reeks.Id;
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Exception dbex = new Exception("VeranderReeks not successful", ex);
                    dbex.Data.Add("Strip", strip);
                    dbex.Data.Add("Reeks", reeks);
                    throw dbex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public void VeranderUitgever(Strip strip, Uitgeverij uitgeverij)
        {
            string query = "UPDATE dbo.strip SET UitgeverijId=@UitgeverijId WHERE Strip.id =@Stripid";
            SqlConnection conn = new SqlConnection(connectionString);
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    command.Parameters.Add(new SqlParameter("@Stripid", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@UitgeverijId", SqlDbType.Int));
                    command.Parameters["@Stripid"].Value = strip.Id;
                    command.Parameters["@UitgeverijId"].Value = uitgeverij.UitgeverijId;
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Exception dbex = new Exception("VeranderUitgever not successful", ex);
                    dbex.Data.Add("Strip", strip);
                    dbex.Data.Add("Uitgeverij", uitgeverij);
                    throw dbex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
