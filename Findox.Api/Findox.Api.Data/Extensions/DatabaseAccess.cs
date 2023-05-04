using Npgsql;

namespace Findox.Api.Data.Extensions
{
    public interface IDatabaseAccess
    {
        NpgsqlConnection Connection();
    }

    public class DatabaseAccess : IDatabaseAccess, IDisposable
    {
        private NpgsqlConnection _openConnection;

        public DatabaseAccess(PostgresDataSource postgresDataSource)
        {
            _openConnection = postgresDataSource.DataSource.OpenConnection();
            _openConnection.Open();
        }

        public NpgsqlConnection Connection()
        {
            return _openConnection;
        }

        public void Dispose()
        {
            if (_openConnection.State != System.Data.ConnectionState.Closed)
                _openConnection.Close();
            _openConnection.Dispose();
        }
    }
}
