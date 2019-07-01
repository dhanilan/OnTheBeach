using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JobSequencing.Tests
{
    [TestClass]
    public class JobParser_Tests
    {
        [TestMethod]
        public void JobParser_ValidJobWithParent_ShouldParse()
        {
            IJobParser parser = new JobParser();
            var result = parser.Parse("A => B");
            Assert.AreEqual(result.JobId,"A");
            Assert.AreEqual(result.ParentJobId,"B");
        }


        [TestMethod]
        public void JobParser_ValidJobWithoutParent_ShouldParse()
        {
            IJobParser parser = new JobParser();
            var result = parser.Parse("A =>");
            Assert.AreEqual(result.JobId, "A");
            Assert.IsNull(result.ParentJobId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void JobParser_ValidJobWithEmpty_ShouldError()
        {
            IJobParser parser = new JobParser();
            var result = parser.Parse("");           
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void JobParser_ValidJobWithMoreThanThreeParameters_ShouldError()
        {
            IJobParser parser = new JobParser();
            var result = parser.Parse("A B C B");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void JobParser_ValidJobWithoutImplies_ShouldError()
        {
            IJobParser parser = new JobParser();
            var result = parser.Parse("A = B");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void JobParser_ValidJobWithWrongSecondArgument_ShouldError()
        {
            IJobParser parser = new JobParser();
            var result = parser.Parse("A B");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void JobParser_ValidJobWithWrongNoImpliesArgument_ShouldError()
        {
            IJobParser parser = new JobParser();
            var result = parser.Parse("A B C");
        }

    }
}
