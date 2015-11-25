using Antelope.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Antelope.Processors
{
    class CoreProcessor
    {
        private Model _context;
        private bool _isRunning = false;

        public CoreProcessor(Model context)
        {
            _context = context;
        }

        public void Start()
        {
            var mp = 3000;
            
            _isRunning = true;

            while(_isRunning)
            {
                var accounts = _context.Accounts.ToList();

                Thread.Sleep(mp);
            }
        }

        public void Stop()
        {
            _isRunning = false;
        }
    }
}
