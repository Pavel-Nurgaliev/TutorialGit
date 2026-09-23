using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseConnectionProject
{
    internal class SqlConnection : DbConnection
    {
        public SqlConnection(string? connectionString) : base(connectionString)
        {
        }

        public override void CloseConnection()
        {
            Console.WriteLine("Close SqlConnection");
        }

        public override void OpenConnection()
        {
            if (Timeout == TimeSpan.Zero)
                throw new InvalidOperationException("Timeout must be set before opening a connection.");

            Console.WriteLine("Open SqlConnection");
        }
    }
}
