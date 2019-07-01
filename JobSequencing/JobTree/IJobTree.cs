using System.Collections.Generic;

namespace JobSequencing
{
    /// <summary>
    /// A tree structure to save hierarychy of jobs
    /// </summary>
    public interface IJobTree
    {
        /// <summary>
        /// Add Job to the tree
        /// </summary>
        /// <param name="job"></param>
        void Add(Job job);

        /// <summary>
        /// Get the jobs in sequence
        /// </summary>
        /// <returns></returns>
        List<string> GetJobs();
    }
}
