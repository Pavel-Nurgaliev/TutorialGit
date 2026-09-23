using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseConnectionProject
{
    internal class OracleConnection : DbConnection
    {
        public OracleConnection(string? connectionString) : base(connectionString)
        {
        }

        public override void CloseConnection()
        {
            Console.WriteLine("Close OracleConnection");
        }

        public override void OpenConnection()
        {
            if (Timeout == TimeSpan.Zero)
                throw new InvalidOperationException("Timeout must be set before opening a connection.");

            Console.WriteLine("Open OracleConnection");
        }
    }
}
