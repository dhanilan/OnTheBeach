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
            AddToTree(job.JobId, job.DependantJobId);
        }



        /// <summary>
        /// Get the jobs in sequence
        /// </summary>
        /// <returns></returns>
        public List<string> GetJobs()
        {
            return BreadthFirstTraversal(Root);
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

            if (string.Equals(job.JobId, job.DependantJobId))
                throw new ArgumentException("Parent and Job cannot be same");

            if (CheckCircularReference(job))
                throw new Exception("Circular Reference Not Allowed");

        }

        private bool CheckCircularReference(Job job)
        {
            var jobToAdd = Find(Root, job.JobId);
            if (jobToAdd != null && job.DependantJobId != null)
            {
                var existingJobChildOfParent = Find(jobToAdd, job.DependantJobId);
                return existingJobChildOfParent != null;
            }

            return false;
        }

        private JobNode AddToTree(string jobId, string dependantJobId = null)
        {
            var alreadyInTree = Find(Root, jobId);
            var isAlreadyInTree = alreadyInTree != null;

            var nodeToAppendJob = Root;
            if (dependantJobId != null)
            {
                var dependantJob = Find(Root, dependantJobId);
                if (dependantJob != null)
                {
                    nodeToAppendJob = dependantJob;
                }
                else
                {
                    nodeToAppendJob = AddToTree(dependantJobId);
                }
            }


            //IF already in parent
            //1.Remove the already in tree from parent            
            if(isAlreadyInTree) DettatchFromParent(jobId,alreadyInTree);

            var jobToRelate = isAlreadyInTree ? alreadyInTree : new JobNode(jobId);

            if (nodeToAppendJob.DependantJobs == null)
                nodeToAppendJob.DependantJobs = new List<JobNode>();

            nodeToAppendJob.DependantJobs.Add(jobToRelate);
            return jobToRelate;
        }

        private void DettatchFromParent(string jobId,JobNode alreadyPresent)
        {
            var parent = FindParent(Root,jobId);
            parent.DependantJobs.Remove(alreadyPresent);
        }

        private JobNode FindParent(JobNode node, string jobId)
        {
            if (node.DependantJobs == null)
                return null;
            foreach (var child in node.DependantJobs)
            {
                if (child.JobId == jobId)
                    return node;
                else
                {
                    var findNextLevel = FindParent(child, jobId);
                    if (findNextLevel != null)
                        return findNextLevel;
                }
            }

            return null;
           
        }

        private JobNode Find(JobNode node, string jobId)
        {
            if (node.JobId == jobId)
                return node;

            if (node.DependantJobs != null)
            {
                foreach (var child in node.DependantJobs)
                {
                    var findResult = Find(child, jobId);
                    if (findResult != null)
                        return findResult;
                }

            }

            return null;
        }

        private List<string> BreadthFirstTraversal(JobNode node)
        {
            var result = new List<string>();

            if (node.JobId != ROOT_JOB_IDENTIFIER)
                result.Add(node.JobId);

            if (node.DependantJobs != null)
            {
                foreach (var childJob in node.DependantJobs)
                {
                    result.AddRange(BreadthFirstTraversal(childJob));
                }
            }

            

            return result;
        }
        #endregion
    }
}
