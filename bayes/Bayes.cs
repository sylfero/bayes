using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bayes
{
    //Dla przykładu 2 i 3
    class Bayes
    {
        private List<Case> Cases = new List<Case>();
        private List<int> NumberOfOptions = new List<int>();

        public Bayes(string path)
        {
            string[] getData = File.ReadAllLines(path);
            string[][] options = new string[getData[0].Split(';').Length - 1][];
            for (int i = 0; i < options.Length; i++)
            {
                options[i] = new string[getData.Length];
            }

            for (int i = 0; i < getData.Length; i++)
            {
                var tmp = getData[i].Split(';');
                Cases.Add(new Case(tmp));

                for (int j = 1; j < tmp.Length; j++)
                {
                    options[j - 1][i] = tmp[j];
                }
            }
            foreach (var item in options)
            {
                NumberOfOptions.Add(item.ToList().Distinct().Count());
            }
        }

        public void Clasify(params string[] properties)
        {
            double[] yesProb = new double[properties.Length == Cases[0].Properties.Length ? properties.Length + 1: throw new Exception("Wrong number of properties")];
            double[] noProb = new double[properties.Length + 1];

            foreach (Case line in Cases)
            {
                if (line.Decision == true)
                {
                    yesProb[0]++;
                    for (int i = 0; i < properties.Length; i++)
                    {
                        if (properties[i].ToUpper().Equals(line.Properties[i]))
                            yesProb[i + 1]++;
                    }
                }
                else
                {
                    noProb[0]++;
                    for (int i = 0; i < properties.Length; i++)
                    {
                        if (properties[i].ToUpper().Equals(line.Properties[i]))
                            noProb[i + 1]++;
                    }
                }
            }

            for (int i = 1; i < yesProb.Length; i++)
            {
                yesProb[i] = yesProb[i] != 0 ? yesProb[i] / yesProb[0] : 1 / (yesProb[0] + NumberOfOptions[i - 1]);
                noProb[i] = noProb[i] != 0 ? noProb[i] / noProb[0] : 1 / (noProb[0] + NumberOfOptions[i - 1]);
            }

            yesProb[0] /= Cases.Count();
            noProb[0] /= Cases.Count();
            double finalYes = 1, finalNo = 1;

            for (int i = 0; i < yesProb.Length; i++)
            {
                finalYes *= yesProb[i];
                finalNo *= noProb[i];
            }

            if (finalYes > finalNo)
                Console.WriteLine($"Decyzja pozytywna. Prawdopodobienstwo pozytywne: {finalYes} Prawdopodobienstwo negatywne: {finalNo}");
            else
                Console.WriteLine($"Decyzja negatywna. Prawdopodobienstwo pozytywne: {finalYes} Prawdopodobienstwo negatywne: {finalNo}");
        }

        private class Case
        {
            public string[] Properties { get; }
            public bool Decision { get; }

            public Case(string[] data)
            {
                Decision = data[0].ToUpper().Equals("TAK") ? true : (data[0].ToUpper().Equals("NIE") ? false : throw new Exception("Decision can't be set to " + data[0])); 
                Properties = new string[data.Length - 1];
                for (int i =1; i < data.Length; i++)
                {
                    Properties[i - 1] = data[i].ToUpper();
                }
            }
        }
    }
}
