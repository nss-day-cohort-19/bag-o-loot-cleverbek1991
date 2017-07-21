using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;

namespace BagOLoot
{
    public class ChildRegister
    {
        private List<string> _children = new List<string>();
        private Dictionary<int, string> _kids = new Dictionary<int, string>(); 
        private string _connectionString = $"Data Source={Environment.GetEnvironmentVariable("BAGOLOOT_DB")}";
        private SqliteConnection _connection;

        public ChildRegister()
        {
            _connection = new SqliteConnection(_connectionString);
        }

        public bool AddChild (string child) 
        {
            int _lastId = 0; // Will store the id of the last inserted record
            using (_connection)
            {
                _connection.Open ();
                SqliteCommand dbcmd = _connection.CreateCommand ();

                // Insert the new child
                dbcmd.CommandText = $"insert into child values (null, '{child}', 0)";
                Console.WriteLine(dbcmd.CommandText);
                dbcmd.ExecuteNonQuery ();

                // Get the id of the new row
                dbcmd.CommandText = $"select last_insert_rowid()";
                using (SqliteDataReader dr = dbcmd.ExecuteReader()) 
                {
                    if (dr.Read()) {
                        _lastId = dr.GetInt32(0);
                    } else {
                        throw new Exception("Unable to insert value");
                    }
                }

                // clean up
                dbcmd.Dispose ();
                _connection.Close ();
            }

            return _lastId != 0;
        }

        public Dictionary<int, string> GetChildren ()
        {
            using (_connection)
            {
                _connection.Open ();
                SqliteCommand dbcmd = _connection.CreateCommand();

                // Get the name of all the children
                dbcmd.CommandText = $"select id, name from Child";
                dbcmd.ExecuteNonQuery ();
                // Get all the names
                using (SqliteDataReader dr = dbcmd.ExecuteReader()) 
                {
                    while(dr.Read()) {
                        _kids.Add(Convert.ToInt32(dr[0]), dr[1].ToString()); //Add child NAME and ID to the list
                    }
                }

                // clean up
                dbcmd.Dispose ();
                _connection.Close ();
            }
            return _kids;
        }

        public string GetChild (string name)
        {
            var child = _children.SingleOrDefault(c => c == name);

            // Inevitably, two children will have the same name. Then what?

            return child;
        }
    }
}