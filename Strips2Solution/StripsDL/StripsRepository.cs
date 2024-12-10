using StripsBL.Model;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StripsDL
{
    public class StripsRepository
    {
        private string connectionString;

        public StripsRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public void SchrijfStrip(Strip strip)
        {
            string SQL = "INSERT INTO Product( id, nednaam, wetnaam, beschrijving, prijs) VALUES( @Id, @Nednaam, " +
                "@Wetnaam, @Beschrijving, @Prijs)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Parameters.Add(new SqlParameter("@Nednaam", System.Data.SqlDbType.NVarChar));
                cmd.Parameters["@Nednaam"].Value = strip.Nednaam;
                cmd.Parameters.Add(new SqlParameter("@Wetnaam", System.Data.SqlDbType.NVarChar));
                cmd.Parameters["@Wetnaam"].Value = strip.Wetnaam;
                cmd.Parameters.Add(new SqlParameter("@Beschrijving", System.Data.SqlDbType.NVarChar));
                cmd.Parameters["@Beschrijving"].Value = strip.Beschrijving;
                cmd.Parameters.Add(new SqlParameter("@Prijs", System.Data.SqlDbType.Float));
                cmd.Parameters["@Prijs"].Value = strip.Prijs;
                cmd.ExecuteNonQuery();
            }
        }



    }
}
