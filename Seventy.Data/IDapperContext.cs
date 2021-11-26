using System;
using System.Data;

namespace Seventy.Data
{
    public interface IDapperContext : IDisposable
    {
        IDbConnection Connection { get; }
    }
}
