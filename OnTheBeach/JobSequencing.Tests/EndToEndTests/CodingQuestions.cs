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
        public void TestMethod1()
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
        public void TestMethod2()
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
            foreach (var item in result)
            {
                Console.Write(item);
            }

        }
    }
}
