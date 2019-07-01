using System;

namespace JobSequencing
{
    public class JobParser : IJobParser
    {
        public Job Parse(string input)
        {
            string[] args = input.Split(' ');

            if (!(args.Length == 2 || args.Length == 3))
                throw new ArgumentException("Input string in incorrect format");

            if (args[1] != "=>")
                throw new ArgumentException("implies operator expected");

            var result = new Job(args[0]);

            if (args.Length == 3)
                result.ParentJobId = args[2];

            return result;
        }
    }
}
