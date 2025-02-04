using MySql.Data.MySqlClient;

namespace MySqlDatabase {
    public class Database {
        private readonly string _connectionString;

        public Database(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Method to establish and return a connection to the database
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        // Generic method to execute a query and return the results
        public List<Dictionary<string, object>> ExecuteQuery(string query)
        {
            List<Dictionary<string, object>> results = new List<Dictionary<string, object>>();
            
            using (var connection = GetConnection()) {
                try {
                    connection.Open();

                    using (var command = new MySqlCommand(query, connection))
                    using (var reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            var row = new Dictionary<string, object>();

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[reader.GetName(i)] = reader.GetValue(i);
                            }

                            results.Add(row);
                        }
                    }
                }
                catch (Exception ex) {
                    Console.WriteLine($"Error executing query: {ex.Message}");
                }
            }

            return results;
        }
    }
}