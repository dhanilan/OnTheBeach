namespace JobSequencing
{
    /// <summary>
    /// Encapsulates a Job
    /// </summary>
    public class Job
    {
        /// <summary>
        /// Job Identifier
        /// </summary>
        public readonly string JobId;


        /// <summary>
        /// Identifier of parent Job
        /// </summary>
        public string ParentJobId { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="jobId">Identifier</param>
        public Job(string jobId)
        {
            JobId = jobId;
        }
    }
}
