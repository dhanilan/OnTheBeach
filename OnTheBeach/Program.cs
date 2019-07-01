using JobSequencing;
using JobSequencing.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheBeach
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("START INPUT on the format A => B. type 'exit' to end inputs");

            IJobParser parser = ParserFactory.Create();
            IJobTree tree = new JobTree();
            do
            {
                var line = Console.ReadLine();
                if (line == "exit")
                    break;
                tree.Add(parser.Parse(line));

            } while (true);

            var result = tree.GetJobs();
            foreach (var item in result)
            {
                Console.Write(item+" ");
            }

            Console.WriteLine("");
            Console.WriteLine("Press any key to exit");
            Console.ReadLine();

        }
    }
}
