using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JobSequencing.Tests
{
    [TestClass]
    public class JobTreeTests
    {
        [TestMethod]
        public void JobTreeAdd_ValidArgs_Success()
        {
            IJobTree jobTree = new JobTree();
            jobTree.Add(new Job("A"));
            jobTree.Add(new Job("B"));

            var jobs =  jobTree.GetJobs();
            Assert.IsTrue(jobs.Contains("A"));
            Assert.IsTrue(jobs.Contains("B"));
        }

        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void JobTreeAdd_WithNoJobId_Throws()
        {
            IJobTree jobTree = new JobTree();
            jobTree.Add(new Job(""));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void JobTreeAdd_WithSameChildAndParent_Throws()
        {
            IJobTree jobTree = new JobTree();
            var job = new Job("A");
            job.ParentJobId = "A";
            jobTree.Add(job);
        }        

        [TestMethod]
        public void JobTreeGetJobs_WithParentAndChild_ReturnsChildThenParent()
        {
            IJobTree jobTree = new JobTree();
            jobTree.Add(new Job("A"));
            jobTree.Add(new Job("B"));
            jobTree.Add(new Job("C") { ParentJobId = "B"});

            var jobs = jobTree.GetJobs();
            Assert.IsTrue(jobs.Contains("A"));
            Assert.IsTrue(jobs.Contains("B"));
            Assert.IsTrue(jobs.Contains("C"));
            Assert.IsTrue(jobs.IndexOf("C") > jobs.IndexOf("B"));
        }

        [TestMethod]
        public void JobTreeGetJobs_WithParentAndChildNested_ReturnsChildThenParent()
        {
            IJobTree jobTree = new JobTree();
            jobTree.Add(new Job("A"));
            jobTree.Add(new Job("B"));
            jobTree.Add(new Job("C") { ParentJobId = "B" });
            jobTree.Add(new Job("D") { ParentJobId = "C" });

            var jobs = jobTree.GetJobs();
            Assert.IsTrue(jobs.Contains("A"));
            Assert.IsTrue(jobs.Contains("B"));
            Assert.IsTrue(jobs.Contains("C"));
            Assert.IsTrue(jobs.IndexOf("C") > jobs.IndexOf("B"));
            Assert.IsTrue(jobs.IndexOf("D") > jobs.IndexOf("C"));
        }

        [TestMethod]
        public void JobTreeGetJobs_WithParentAndChildNestedToLevels_ReturnsChildThenParent()
        {

            IJobTree jobTree = new JobTree();
            jobTree.Add(new Job("A"));

            jobTree.Add(new Job("B"));
            jobTree.Add(new Job("C") { ParentJobId = "B" });
            jobTree.Add(new Job("D") { ParentJobId = "C" });

            jobTree.Add(new Job("E") { ParentJobId = "A" });
            jobTree.Add(new Job("F") { ParentJobId = "E" });

            var jobs = jobTree.GetJobs();
            Assert.IsTrue(jobs.Contains("A"));
            Assert.IsTrue(jobs.Contains("B"));
            Assert.IsTrue(jobs.Contains("C"));
            Assert.IsTrue(jobs.IndexOf("C") > jobs.IndexOf("B"));
            Assert.IsTrue(jobs.IndexOf("D") > jobs.IndexOf("C"));

            Assert.IsTrue(jobs.IndexOf("E") > jobs.IndexOf("A"));
            Assert.IsTrue(jobs.IndexOf("F") > jobs.IndexOf("E"));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void JobTreeAdd_WithCircular_Throws()
        {
            IJobTree jobTree = new JobTree();
            //jobTree.Add(new Job("B"));
            jobTree.Add(new Job("B") { ParentJobId = "C" });
            jobTree.Add(new Job("C") { ParentJobId = "F" });
            jobTree.Add(new Job("F") { ParentJobId = "B" });


        }
    }
}
