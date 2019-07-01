using System;

namespace JobSequencing
{
    /// <summary>
    /// Parser class which parses Job from input string
    /// </summary>
    public class JobParser : IJobParser
    {
        /// <summary>
        /// Parses the Job from input
        /// </summary>
        /// <param name="input">input string</param>
        /// <returns>Job instance</returns>
        public Job Parse(string input)
        {
            input = input.Replace('\r', ' ');
            input = input.Trim();
            string[] args = input.Split(Constants.JobSeperator);

            if (!(args.Length == 2 || args.Length == 3))
                throw new ArgumentException(Constants.IncorrectFormatMessage);

            if (args[1] != Constants.JobDependencyOperator)
                throw new ArgumentException(Constants.ImpliesOperatorExpectedMessage);

            var result = new Job(args[0]);

            if (args.Length == 3)
                result.ParentJobId = args[2];

            return result;
        }
    }
}
