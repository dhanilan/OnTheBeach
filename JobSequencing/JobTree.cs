using System;
using System.Collections.Generic;
using System.Text;

namespace JobSequencing
{
    /// <summary>
    /// A tree structure to save hierarychy of jobs
    /// </summary>
    public class JobTree : IJobTree
    {
        /// <summary>
        /// Add Job to the tree
        /// </summary>
        /// <param name="job"></param>
        public void Add(Job job)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the jobs in sequence
        /// </summary>
        /// <returns></returns>
        public List<string> GetJobs()
        {
            throw new NotImplementedException();
        }
    }
}
