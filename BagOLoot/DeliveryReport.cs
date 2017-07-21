using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace BagOLoot
{
    public class DeliveryReport
    {
        private string _connectionString = $"Data Source={Environment.GetEnvironmentVariable("BAGOLOOT_DB")}";
        private SqliteConnection _connection;
        public DeliveryReport()
        {
            _connection = new SqliteConnection(_connectionString);
        }

        public Dictionary <int, string> GetAllToysForChild()
        {
            Dictionary<int, string> childList = new Dictionary<int, string>();// Will hold list of all children
            using (_connection)
            {
                _connection.Open();
                SqliteCommand dbcmd = _connection.CreateCommand();

                // Insert the new child
                dbcmd.CommandText = $"select c.id, c.name from child c inner join toy t where c.id = t.childID";
                using (SqliteDataReader dr = dbcmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        if(childList.ContainsKey(dr.GetInt32(0)))
                        {

                        }
                        else
                        {
                            childList.Add(dr.GetInt32(0), dr[1].ToString());
                        }
                         
                    }
                }

                // clean up
                dbcmd.Dispose ();
                _connection.Close ();
            }
            return childList;
        }

        public bool Delivered(int childId)
        {
            bool delivered = false;
            using (_connection)
            {
                _connection.Open ();
                SqliteCommand dbcmd = _connection.CreateCommand ();

                dbcmd.CommandText = $"update child set delivered = 1 where id = {childId}";
                Console.WriteLine(dbcmd.CommandText);
                dbcmd.ExecuteNonQuery ();

                dbcmd.CommandText = $"select c.id from child c where c.id = {childId} and c.delivered = 1";
                using (SqliteDataReader dr = dbcmd.ExecuteReader()) 
                {
                    if (dr.Read()) {
                        delivered = true;
                    } else {
                        delivered = false;
                    }
                }

                // clean up
                dbcmd.Dispose ();
                _connection.Close ();
            }
            return delivered;
        }
    }
}