using System;
using System.Collections.Generic;
using System.Text;

namespace CompositionApp
{
    internal class DbMigrator
    {
        private Logger _logger = null;

        public DbMigrator(Logger logger)
        {
            _logger = logger;
        }

        public void Migrate()
        {
            _logger.Log("We megrate something");
        }
    }
}
