using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadania
{
    internal class Person
    {
        Hub startHub;
        Hub endHub;
        public Person(Hub startHub, Hub endHub)
        {
            this.startHub = startHub;
            this.endHub = endHub;
        }
        public Person(Map map1,  Map map2)
        {
            //ciąg dalszy nastąpi
        }
    }
}
