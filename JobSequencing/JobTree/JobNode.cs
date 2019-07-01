using System.Collections.Generic;

namespace JobSequencing
{
    /// <summary>
    /// A node in tree of jobs
    /// </summary>
    public class JobNode
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public readonly string JobId;

        /// <summary>
        /// Child/Dependent Jobs
        /// </summary>
        public List<JobNode> DependantJobs { get; set; }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="jobId">Job Identfier</param>
        public JobNode(string jobId)
        {
            JobId = jobId;
        }
    }
}
