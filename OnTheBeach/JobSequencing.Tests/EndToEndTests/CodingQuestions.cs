using System;
using System.Collections.Generic;
using JobSequencing.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JobSequencing.Tests.EndToEndTests
{
    [TestClass]
    public class CodingQuestions
    {
       

        [TestMethod]
        public void TestMethod2()
        {
            var allLines = @"a =>
                            b => 
                            c =>";
            var parser = ParserFactory.Create();
            IJobTree tree = new JobTree();

            foreach (var item in allLines.Split('\n'))
            {
                tree.Add(parser.Parse(item));
            }

            var result = tree.GetJobs();

            Assert.IsTrue(result.Contains("a"));
            Assert.IsTrue(result.Contains("b"));
            Assert.IsTrue(result.Contains("c"));

        }

        [TestMethod]
        public void TestMethod3()
        {
            var allLines = @"a =>
                            b => c
                            c =>";
            var parser = ParserFactory.Create();
            IJobTree tree = new JobTree();

            foreach (var item in allLines.Split('\n'))
            {
                tree.Add(parser.Parse(item));
            }

            var result = tree.GetJobs();

            Assert.IsTrue(result.Contains("a"));
            Assert.IsTrue(result.Contains("b"));
            Assert.IsTrue(result.Contains("c"));
            Assert.IsTrue(result.IndexOf("c") < result.IndexOf("b"));

        }


        [TestMethod]
        public void TestMethod4()
        {
            var allLines = new List<string>();
            allLines.Add("a =>");
            allLines.Add("b => c");
            allLines.Add("c => f");
            allLines.Add("d => a");
            allLines.Add("e => b");
            allLines.Add("f =>");

            var parser = ParserFactory.Create();
            IJobTree tree = new JobTree();

            foreach (var item in allLines)
            {
                tree.Add(parser.Parse(item));
            }

            var result = tree.GetJobs();
            foreach (var item in result)
            {
                Console.Write(item);
            }

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMethod5()
        {
            var allLines = @"a =>
                            b => c
                            c => c";
            var parser = ParserFactory.Create();
            IJobTree tree = new JobTree();

            foreach (var item in allLines.Split('\n'))
            {
                tree.Add(parser.Parse(item));
            }           

        }


        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestMethod6()
        {
            var allLines = @"a =>
                            b => c
                            c => f
                            d => a
                            e =>
                            f => b";
            var parser = ParserFactory.Create();
            IJobTree tree = new JobTree();

            foreach (var item in allLines.Split('\n'))
            {
                tree.Add(parser.Parse(item));
            }

        }

    }
}
