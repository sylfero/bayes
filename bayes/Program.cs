using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace bayes
{
    class Program
    {
        static void Main(string[] args)
        {
            Bayes ba = new Bayes("data.txt");
            ba.Clasify("deszczowo", "goraco", "slaby");
            ba.Clasify("pochmurno", "chlodno", "mocny");
            
            Filter.MyFilter("before.jpg");
            Console.ReadKey();
        }
    }
}
