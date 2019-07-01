using System;
using System.Collections.Generic;
using System.Text;

namespace JobSequencing.Parser
{
    public class ParserFactory
    {
        static IJobParser Create()
        {
            return new JobParser();

        }

    }
}
