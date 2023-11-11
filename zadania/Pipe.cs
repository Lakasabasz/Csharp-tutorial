using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadania
{
    internal class Pipe
    {
        Hub startHub;
        Hub endHub;
        public Pipe(Hub startHub, Hub endHub)
        {
            this.startHub = startHub;
            this.endHub = endHub;
            addPipesToHubs();
        }
        private void addPipesToHubs()
        {
            startHub.addPipe(this);
            endHub.addPipe(this);
        }
    }
}
