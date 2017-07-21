using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace BagOLoot
{
    public class SantaHelper
    {
        private string _connectionString = $"Data Source={Environment.GetEnvironmentVariable("BAGOLOOT_DB")}";
        private SqliteConnection _connection;

        private List<string> _toylist = new List<string>();
        public SantaHelper()
        {
            _connection = new SqliteConnection(_connectionString);
        }
        public int AddToyToBag(string toy, int childId)
        {
            int _lastId = 0; // Will store the id of the last inserted record
            using (_connection)
            {
                _connection.Open ();
                SqliteCommand dbcmd = _connection.CreateCommand ();

                // Insert the new toy to bag
                dbcmd.CommandText = $"insert into toy values (null, '{toy}', '{childId}')";
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

            return childId;
        }
        
        
        // public void RemoveToyFromBag(int toyId, int childId)
        public void RemoveToyFromBag(int toyId)
        {
            using (_connection)
            {
                _connection.Open ();
                SqliteCommand dbcmd = _connection.CreateCommand ();

                // Remove a toy from child's bag o' loot
                dbcmd.CommandText = $"delete from toy where id = '{toyId}'";
                dbcmd.ExecuteNonQuery ();

                // clean up
                dbcmd.Dispose ();
                _connection.Close ();
            }
        }

        public List<string> GetChildsToys(int childId)
        {
            using (_connection)
            {
                _connection.Open ();
                SqliteCommand dbcmd = _connection.CreateCommand();

                // Get the name of all the children
                dbcmd.CommandText = $"select name, childId from toy where childId = '{childId}';";
                dbcmd.ExecuteNonQuery ();
                // Get all the names
                using (SqliteDataReader dr = dbcmd.ExecuteReader()) 
                {
                    while(dr.Read()) {
                        _toylist.Add(dr[0].ToString()); //Add child name to the list
                    }
                }

                // clean up
                dbcmd.Dispose ();
                _connection.Close ();
            }
            return _toylist;
        }
        public Dictionary <int, string> GetAllToysForChild(int childID)
        {
            Dictionary<int,string> allToysForChild = new Dictionary<int, string>();
            using (_connection)
            {
                _connection.Open();
                SqliteCommand dbcmd = _connection.CreateCommand();
                dbcmd.CommandText = $"select toy.id, toy.name from toy where toy.childID = {childID}";
                using(SqliteDataReader dr = dbcmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        allToysForChild.Add(dr.GetInt32(0), dr[1].ToString());
                    }
                }

                _connection.Dispose();
                _connection.Close();
            }

            return allToysForChild;
        }
    }
}