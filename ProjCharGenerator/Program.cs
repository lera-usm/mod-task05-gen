using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace generator
{

    class Generatorslov
    {

        private List<String> readLine(String line)
        {
            List<String> result = new List<String>();
            String word = "";
            foreach (char c in line)
            {
                if (c == ' ' || c == '\t')
                {
                    result.Add(word);
                    word = "";
                }
                else
                {
                    word += c;
                }
            }
            if (word.Length != 0)
            {
                result.Add(word);
            }
            return result;
        }
        public Generatorslov(String filePath)
        {
            tabl = new Dictionary<string, int>();
            StreamReader sr = File.OpenText(filePath);
            String str;
            List<List<String>> lines = new List<List<string>>();
            while ((str = sr.ReadLine()) != null)
            {
                lines.Add(new List<string>());
                lines[lines.Count - 1] = readLine(str);
            }
            str = "";
            foreach (var line in lines)
            {
                str += line[0];
                for (int i = 1; i < line.Count() - 1; i++)
                {
                    str += " " + line[i];
                }
                tabl[str] = Int32.Parse(line[line.Count() - 1]);
                str = "";
            }
        }

        public string Gentext(int size)
        {
            String rezult = "";
            int Sum = 0;
            int[] mass;
            long chance;
            for (int i = 0; i < size; i++)
            {
                mass = new int[tabl.Values.Count];
                tabl.Values.CopyTo(mass, 0);
                chance = rand.Next(0, mass.Sum());
                foreach (var pair in tabl)
                {
                    if (pair.Value + Sum >= chance)
                    {
                        rezult += pair.Key + " ";
                        Sum = 0;
                        break;
                    }
                    else
                    {
                        Sum += pair.Value;
                    }
                }
            }
            return rezult;
        }
        private Random rand = new Random();
        private Dictionary<String, int> tabl;
    }
    class Generatbukv
    {
        private List<String> readLine(String line)
        {
            List<String> result = new List<String>();
            String word = "";
            foreach (char c in line)
            {
                if (c == ' ' || c == '\t')
                {
                    result.Add(word);
                    word = "";
                }
                else
                {
                    word += c;
                }
            }
            if (word.Length != 0)
            {
                result.Add(word);
            }
            return result;
        }
        public Generatbukv(String filePath)
        {
            tabl = new Dictionary<char, Dictionary<char, int>>();
            StreamReader sr = File.OpenText(filePath);
            String str;
            List<List<String>> lines = new List<List<string>>();
            while ((str = sr.ReadLine()) != null)
            {
                lines.Add(new List<string>());
                lines[lines.Count - 1] = readLine(str);
            }
            for (int i = 0; i < syms.Length; i++)
            {
                tabl[syms[i]] = new Dictionary<char, int>();
                for (int j = 1; j < lines[i].Count; j++)
                {
                    tabl[syms[i]][syms[j - 1]] = Int32.Parse(lines[i][j]);
                }
            }
        }

        public String Gentext(int size)
        {
            String rezult = "";

            int Pos = rand.Next(0, syms.Length);
            char Sym = syms[Pos];
            int Sum = 0;
            int[] mass;
            int chance;

            rezult += Sym;
            for (int i = 0; i < size - 1; i++)
            {
                mass = new int[tabl[Sym].Values.Count];
                tabl[Sym].Values.CopyTo(mass, 0);
                chance = rand.Next(0, mass.Sum());
                foreach (var pair in tabl[Sym])
                {
                    if (pair.Value + Sum >= chance)
                    {
                        Sym = pair.Key;
                        rezult += Sym;
                        Sum = 0;
                        break;
                    }
                    else
                    {
                        Sum += pair.Value;
                    }
                }
            }
            return rezult;
        }
        private string syms = "АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЫЬЭЮЯ";
        private Random rand = new Random();
        private Dictionary<char, Dictionary<char, int>> tabl;
    }
    class CharGenerator 
    {
        private string syms = "абвгдеёжзийклмнопрстуфхцчшщьыъэюя"; 
        private char[] data;
        private int size;
        private Random random = new Random();
        public CharGenerator() 
        {
           size = syms.Length;
           data = syms.ToCharArray(); 
        }
        public char getSym() 
        {
           return data[random.Next(0, size)]; 
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            CharGenerator gen = new CharGenerator();
            SortedDictionary<char, int> stat = new SortedDictionary<char, int>();
            for(int i = 0; i < 1000; i++) 
            {
               char ch = gen.getSym(); 
               if (stat.ContainsKey(ch))
                  stat[ch]++;
               else
                  stat.Add(ch, 1); Console.Write(ch);
            }
            Console.Write('\n');
            foreach (KeyValuePair<char, int> entry in stat) 
            {
                 Console.WriteLine("{0} - {1}",entry.Key,entry.Value/1000.0); 
            }


            Generatbukv bgen = new Generatbukv(@"..\..\..\bukv.txt");
            Generatorslov fgen1 = new Generatorslov(@"..\..\..\slov1.txt");
            Generatorslov fgen2 = new Generatorslov(@"..\..\..\slov2.txt");
            File.WriteAllText(@"..\..\..\task1.txt", bgen.Gentext(1000));
            File.WriteAllText(@"..\..\..\task2.txt", fgen1.Gentext(1000));
            File.WriteAllText(@"..\..\..\task3.txt", fgen2.Gentext(1000));
        }
    }
}

