using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using FileOperations;

namespace WCFTest
{
    public class Program
    {
        static Action<string> Log = (_s) => { Console.WriteLine(_s); }; //plug in log4net later on.
           
        static void Main(string[] args)
        {
            string _inputFilePath = ReadLineFromConsole("Please enter file path of pdf file to be watermarked."); 
            string _watermarkText = ReadLineFromConsole("Please enter watermark text.");
            string _destinationFolder = ReadLineFromConsole("Please enter destination folder.");

            if (File.Exists(_inputFilePath))
            {
                CallUpService(_inputFilePath, _watermarkText,_destinationFolder);
            }

            Console.ReadKey();
        }

        public static string ReadLineFromConsole(string UserPrompt)
        {
            Console.WriteLine(UserPrompt);
            return Console.ReadLine();
        }

        public static void CallUpService(string FilePath,string WaterMarkText, string DestinationPath)
        {
            WatermarkingService.IWaterMarkService _wmark=new WatermarkingService.WaterMarkServiceClient();
            byte[] _watermarkedFileContent = null;
            FileUtility _fileUtil = new FileUtility(Log);

            try
            {
               _watermarkedFileContent= _wmark.WaterMarkFile(_fileUtil.GetFileByteArray(FilePath), WaterMarkText);
               _fileUtil.WriteToLocalFile(_watermarkedFileContent,DestinationPath);
            }
            catch (Exception _ex)
            { 
                //Log this
                Log("Watermark : Callupservice threw an exception. "+ _ex.ToString());
                throw _ex;
            }
        }
    }
}
