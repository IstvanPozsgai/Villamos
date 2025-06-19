using System.Data.OleDb;
using System.Threading.Tasks;

public class AccessDatabaseHelper
{
    /// <summary>
    /// async Access database helper class for executing queries.
    /// </summary>
    private readonly string _connectionString;

    public AccessDatabaseHelper(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<int> ExecuteNonQueryAsync(string query)
    {
        return await Task.Run(() =>
        {
            using (OleDbConnection connection = new OleDbConnection(_connectionString))
            {
                connection.Open();
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    return command.ExecuteNonQuery();
                }
            }
        });
    }

    public async Task<object> ExecuteScalarAsync(string query)
    {
        return await Task.Run(() =>
        {
            using (OleDbConnection connection = new OleDbConnection(_connectionString))
            {
                connection.Open();
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    return command.ExecuteScalar();
                }
            }
        });
    }
}
