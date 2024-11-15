using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sentral
{
    internal class Tables
    {
        static void brukerlogg(string connString)
        {
            // Oppretter en forbindelse
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // SQL-spørring for å hente data
                string query = "SELECT * FROM Bruker";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    // Utfører spørringen og får tilbake en NpgsqlDataReader
                    using (var reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        //Printer kolonnenavn
                        foreach (DataColumn kolonne in dt.Columns)
                        {
                            Console.Write("{0,-10} ", kolonne.ColumnName);
                        }
                        Console.WriteLine();

                        // Printer rader
                        foreach (DataRow row in dt.Rows)
                        {
                            foreach (var item in row.ItemArray)
                            {
                                Console.Write("{0,-10} ", item);
                            }
                            Console.WriteLine();
                        }

                        Console.ReadKey();
                    }
                }
                conn.Close();
            }
            static void adgangslogg(string connString)
            {


            }
            static void innpasseringslogg(string connString)
            {


            }
        }
    }
}
