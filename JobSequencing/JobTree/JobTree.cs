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
        #region private properties

        private const string ROOT_JOB_IDENTIFIER = "ROOT";
        private readonly JobNode Root;

        #endregion

        #region Interface methods

        /// <summary>
        /// Add Job to the tree
        /// </summary>
        /// <param name="job"></param>
        public void Add(Job job)
        {
            Validate(job);
            AddToTree(job.JobId, job.ParentJobId);
        }



        /// <summary>
        /// Get the jobs in sequence
        /// </summary>
        /// <returns></returns>
        public List<string> GetJobs()
        {
            return DepthFirstTraversal(Root);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates an instance of Job tree
        /// </summary>
        public JobTree()
        {
            Root = new JobNode(ROOT_JOB_IDENTIFIER);
        }

        #endregion


        #region Private methods

        private void Validate(Job job)
        {
            if (string.IsNullOrEmpty(job.JobId))
                throw new ArgumentNullException("JobId");

            if (string.Equals(job.JobId, job.ParentJobId))
                throw new ArgumentException("Parent and Job cannot be same");

            if (CheckCircularReference(job))
                throw new Exception("Circular Reference Not Allowed");

        }

        private bool CheckCircularReference(Job job)
        {
            var jobToAdd = Find(Root, job.JobId);
            if (jobToAdd != null && job.ParentJobId != null)
            {
                var existingJobChildOfParent = Find(jobToAdd, job.ParentJobId);
                return existingJobChildOfParent != null;
            }

            return false;
        }

        private JobNode AddToTree(string jobId, string parentJobId = null)
        {
            var alreadyInTree = Find(Root, jobId);
            var isAlreadyInTree = alreadyInTree != null;

            var nodeToAppendJob = Root;
            if (parentJobId != null)
            {
                var parentJob = Find(Root, parentJobId);
                if (parentJob != null)
                {
                    nodeToAppendJob = parentJob;
                }
                else
                {
                    nodeToAppendJob = AddToTree(parentJobId);
                }
            }


            //IF already in parent
            //1.Remove the already in tree from parent            
            if(isAlreadyInTree) DettatchFromParent(jobId,alreadyInTree);

            var jobToRelate = isAlreadyInTree ? alreadyInTree : new JobNode(jobId);

            if (nodeToAppendJob.ChildJobs == null)
                nodeToAppendJob.ChildJobs = new List<JobNode>();

            nodeToAppendJob.ChildJobs.Add(jobToRelate);
            return jobToRelate;




        }

        private void DettatchFromParent(string jobId,JobNode alreadyPresent)
        {
            var parent = FindParent(Root,jobId);
            parent.ChildJobs.Remove(alreadyPresent);
        }

        private JobNode FindParent(JobNode node, string jobId)
        {            
            foreach (var child in node.ChildJobs)
            {
                if (child.JobId == jobId)
                    return node;
                else
                 return FindParent(child, jobId);                
            }

            return null;
           
        }

        private JobNode Find(JobNode node, string jobId)
        {
            if (node.JobId == jobId)
                return node;

            if (node.ChildJobs != null)
            {
                foreach (var child in node.ChildJobs)
                {
                    var findResult = Find(child, jobId);
                    if (findResult != null)
                        return findResult;
                }

            }

            return null;
        }

        private List<string> DepthFirstTraversal(JobNode node)
        {
            var result = new List<string>();

            if (node.ChildJobs != null)
            {
                foreach (var childJob in node.ChildJobs)
                {
                    result.AddRange(DepthFirstTraversal(childJob));
                }
            }

            result.Add(node.JobId);
            return result;
        }
        #endregion
    }
}
