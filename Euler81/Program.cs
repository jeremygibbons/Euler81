using System;
using System.IO;

namespace Euler81
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] data = new int[80, 80];
            int[,] paths = new int[80, 80];

            //read in source data
            string line;
            int linenum = 0;
            StreamReader file = new StreamReader("p081_matrix.txt");
            while ((line = file.ReadLine()) != null && linenum < 80)
            {
                string[] lineValues = line.Split(',');
                for(int i = 0; i < 80; i++)
                {
                    data[linenum, i] = Convert.ToInt32(lineValues[i], 10);
                }
                linenum++;
            }

            //compute minimal path to lower right corner starting at lower right and
            //moving by "generation"
            paths[79, 79] = data[79, 79];

            int gen = 78;
            while(gen >= 0)
            {
                //boundary cases, only one direction possible
                paths[79, gen] = paths[79, gen + 1] + data[79, gen];
                paths[gen, 79] = paths[gen + 1, 79] + data[gen, 79];
                //general case, shortest path is sum of current cell contents + min of path down or right
                for (int i = 78; i > gen; i--)
                {
                    paths[i, gen] = data[i, gen] + Math.Min(paths[i, gen + 1], paths[i + 1, gen]);
                    paths[gen, i] = data[gen, i] + Math.Min(paths[gen + 1, i], paths[gen, i + 1]);
                }
                //finally handle upper left cell of the current generation
                paths[gen, gen] = data[gen, gen] + Math.Min(paths[gen, gen + 1], paths[gen + 1, gen]);

                gen--;
            }
            Console.WriteLine(paths[0, 0]);
            Console.ReadLine();
        }
    }
}
