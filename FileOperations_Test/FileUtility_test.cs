using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using FileOperations;
using Moq;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;

namespace FileOperations_Test
{
    [TestClass]
    public class FileUtility_test
    {
        [TestMethod]
        public void WriteToLocalFile_Should_Write_Out_To_LocalFile()
        {
            //Take in flecontent to write in byte array, destination folder and write ouot to destination path
            //check by verifyin if the file exists.
            string _inputFilePath = @"C:\TestFile\test1.pdf";
            byte[] _inputFileContents=  File.ReadAllBytes(_inputFilePath);
            Assert.IsNotNull(_inputFilePath);
            Assert.IsNotInstanceOfType(_inputFileContents,typeof(byte[]));
            string _destinationFolder = @"C:\TestFileOutput";
            Mock<Action<string>> _mockAction=new Mock<Action<string>>();
            FileUtility _fileUtility = new FileUtility(_mockAction.Object);
            _fileUtility.WriteToLocalFile(_inputFileContents, _destinationFolder);

            Assert.IsTrue( Directory.Exists(_destinationFolder));

            IEnumerable<string> _filesInDirectory = Directory.EnumerateFiles(_destinationFolder,"*.pdf");
            Assert.IsNotNull(_filesInDirectory);
            var _filteredList = _filesInDirectory.Where(_f => _f.Contains("_output"));
            Assert.IsTrue(_filteredList.Count() > 0);            
        }

        [ExpectedException(typeof(FileNotFoundException))]
        [TestMethod]
        public void WriteToLocalFile_Should_Throw_All_Valid_Exceptions()
        { 
             //destination folder string empty exception
            string _invalidPath = @"C:\InvalidPath";
            string _blankPath = string.Empty;
            byte[] _invalidFileContent = null;

            Mock<Action<string>> _mockAction = new Mock<Action<string>>();
            FileUtility _fileUtility = new FileUtility(_mockAction.Object);

            _fileUtility.WriteToLocalFile(_invalidFileContent, _invalidPath);
            _fileUtility.WriteToLocalFile(_invalidFileContent,_blankPath);
        }

        [ExpectedException(typeof(InvalidDataException))]
        [TestMethod]
        public void WriteToLocalFile_Should_Throw_FileCcontent_Empty_Excception()
        {
            //filecontent empty excepton
             string _validPath = @"C:\TestFile";
            string _blankPath = string.Empty;
            byte[] _invalidFileContent = null;

            Mock<Action<string>> _mockAction = new Mock<Action<string>>();
            FileUtility _fileUtility = new FileUtility(_mockAction.Object);

            _fileUtility.WriteToLocalFile(_invalidFileContent, _validPath);
        }

        [ExpectedException(typeof(IOException))]
        [TestMethod]
        public void WriteToLocalFile_Should_Throw_FileStrm_Read_Exception()
        {
            //filestream read exception
             string _invalidFolder = @"C:\TestFileOutput1";// invalid path -- this should throw a ioexception
            string _validFilePath = @"C:\TestFile\test1.pdf";
            byte[] _validFileContent = File.ReadAllBytes(_validFilePath);

            Mock<Action<string>> _mockAction = new Mock<Action<string>>();
            FileUtility _fileUtility = new FileUtility(_mockAction.Object);

            _fileUtility.WriteToLocalFile(_validFileContent, _invalidFolder);
        }

        [TestMethod]
        public void GetFileByteArray_Check_If_Output_Is_Non_0_Length()
        {
            //check if output array is non 0           
            string _path = @"C:\TestFile\test1.pdf";
          
             Mock<Action<string>> _mockAction = new Mock<Action<string>>();

            FileUtility _testUutility = new FileUtility(_mockAction.Object);
            byte[] _testContent= _testUutility.GetFileByteArray(_path);

            Assert.IsNotNull(_testContent);
            Assert.IsInstanceOfType(_testContent, typeof(byte[]));
            Assert.AreNotEqual(_testContent.Length,0);
        }

       [ExpectedException(typeof(FileNotFoundException))]
       public void GetFileByteArray_Check_If_File_DoesNot_Exist()
        { 
            //check if exception is thrown if the input file path is not valid  
           Mock<Action<string>> _mockAction=new Mock<Action<string>>();
           FileUtility _fileUtility = new FileUtility(_mockAction.Object);
           string _invalidPath = @"C:\InvalidFolder\abc.pdf";
           _fileUtility.GetFileByteArray(_invalidPath);
        }
    }
}
