using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bayes
{
    class Program
    {
        static void Main(string[] args)
        {
            Bayes b = new Bayes("data.txt");
            b.Clasify("deszczowo", "goraco", "slaby");
            b.Clasify("pochmurno", "chlodno", "mocny");
            Console.ReadKey();
        }
    }
}
