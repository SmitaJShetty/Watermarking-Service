using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WCFTest;
using System.IO;

namespace WCFTestClient_test
{
    [TestClass]    
    public class WCFTestProgram_test
    {
        [TestMethod]
        public void CallUpService_DoesNot_Throw_Exceptions()
        {
            string _filePath=@"C:\Testpdf\Test1.pdf";
            string _waterMarkText ="Testing sample";
            string _destinationPath =@"C:\Output\Test1_output.pdf";
            try
            {
                Program.CallUpService(_filePath, _waterMarkText, _destinationPath);
            }
            catch (Exception _ex)
            {
                Assert.IsTrue(false, "Function threw error!");
                throw _ex;
            }

            Assert.IsTrue(true, "No exceptions throwwn");            
        }

        [TestMethod]
        public void CallUpService_Should_Put_File_Where_Expected()
        {
            string _filePath = @"C:\Testpdf\Test1.pdf";
            string _waterMarkText = "Testing sample";
            string _destinationPath = @"C:\Output\Test1_output.pdf";
            Program.CallUpService(_filePath, _waterMarkText, _destinationPath);

            bool _fileExists = File.Exists(_destinationPath);

            Assert.IsTrue(_fileExists);
        }
    }
}
