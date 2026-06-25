using System;
using System.Collections.Generic;
using System.Text;

namespace CompositionApp
{
    internal class Installer
    {
        private Logger _logger = null;

        public Installer(Logger logger)
        {
            _logger = logger;
        }

        public void Install()
        {
            _logger.Log("We install something");
        }
    }
}
