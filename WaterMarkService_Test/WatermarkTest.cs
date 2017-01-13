using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;
using WatermarkingLib;

namespace WaterMarkService_Test
{
    [TestClass]
    public class WatermarkTest
    {
        [TestMethod]
        public void Watermark_Check_if_svc_Is_Alive()
        {
          
        }

        [TestMethod]
        public void Watermark_Check_if_Svc_returns_A_ValidType()
        {
            string _inputFilePath = @"C:\TestFile\Test1.pdf";
            byte[] _inputFileArray = GetFileByteArray(_inputFilePath);
            
            string _waterMarkString = "Sample water mark";

            Mock<IWaterMark> _utility = new Mock<IWaterMark>();
           
            WatermarkingService.IWaterMarkService _wmService = new WatermarkingService.WaterMarkService(_utility.Object);
            byte[] _actualOutputContent= _wmService.WaterMarkFile(_inputFileArray,_waterMarkString);

            Assert.IsNotNull(_actualOutputContent);
            Assert.IsInstanceOfType(_actualOutputContent, typeof(byte[]));
        }

        [TestMethod]
        public void Watermark_Check_if_Svc_Returns_A_Valid_file()
        {
            string _inputFilePath = @"C:\TestFile\Test1.pdf";
            byte[] _inputFileArray = GetFileByteArray(_inputFilePath);

            IWaterMark _utility = new WatermarkingUtility();
            WatermarkingService.IWaterMarkService _svc=new WatermarkingService.WaterMarkService(_utility);
           
            string _waterMarkString = "Sample water mark";

             byte[] _outputFileArray= _svc.WaterMarkFile(_inputFileArray, _waterMarkString);

            Assert.IsNotNull(_outputFileArray);
            Assert.IsInstanceOfType(_outputFileArray,typeof(byte[]));
            Assert.AreNotEqual(_outputFileArray.Length,0);
        }

        private byte[] GetFileByteArray(string FilePath)
        {
            byte[] _outputBytes = null;

            if (File.Exists(FilePath))
            {
                _outputBytes = File.ReadAllBytes(FilePath);
            }
            return _outputBytes;
        }
    }
}
