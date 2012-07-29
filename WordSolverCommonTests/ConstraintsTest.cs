using WordSolver;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace WordSolverCommonTests
{
    
    
    /// <summary>
    ///This is a test class for ConstraintsTest and is intended
    ///to contain all ConstraintsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ConstraintsTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Constraints Constructor
        ///</summary>
        [TestMethod()]
        public void ConstraintsConstructorTest()
        {
            string tiles = string.Empty; // TODO: Initialize to an appropriate value
            string template = string.Empty; // TODO: Initialize to an appropriate value
            Constraints target = new Constraints(tiles, template);

            Assert.AreEqual(tiles, target.Tiles);
            Assert.AreEqual(template, target.Template);
        }

        /// <summary>
        ///A test for TryCandidateWord
        ///</summary>
        [TestMethod()]
        public void TryCandidateWordTest()
        {
            var constraints = new Constraints("dog", "o");
            
            Word w;
            var match = constraints.TryCandidateWord("DOG", out w);
            Assert.IsFalse(match);
            Assert.IsNull(w);
        }
    }
}
