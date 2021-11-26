using System.Data;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using Seventy.DomainClass;
namespace Seventy.Data
{
    public class DapperContext : IDapperContext
    {
        private IDbConnection _conn { get; set; }
        private readonly IOptions<PublicConfiguration> _settings;

        public DapperContext(IOptions<PublicConfiguration> settings)
        {
            _settings = settings;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_settings.Value.ConnectionString);
            }

        }
        public void Dispose()
        {
            if (_conn != null)
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                    _conn.Dispose();
                }
                _conn = null;
            }
        }
    }
}


