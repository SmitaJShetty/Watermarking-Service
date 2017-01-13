using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileOperations
{
    public class FileUtility
    {
        private Action<string> _log;

        public FileUtility(Action<string> Log)
        {
            _log = Log;
        }

        public void WriteToLocalFile(byte[] FileContent,string DestinationFolder)
        {
            if (!string.IsNullOrEmpty(DestinationFolder))
            {
                if ((FileContent != null) && (FileContent.Length > 0))
                {
                    string _localFolder = DestinationFolder;
                    Random _random = new Random();
                    string _localFilePath = _localFolder + "\\output_" + _random.Next().ToString() + ".pdf";
                    FileStream _fs = null;

                    try
                    {
                        _fs = new FileStream(_localFilePath, FileMode.CreateNew);
                        _fs.Write(FileContent, 0, FileContent.Length);
                    }
                    catch
                    {
                        //Log
                        _log("Error/Exception while writing filestream.");
                        throw;
                    }
                    finally
                    {
                        _fs.Flush();
                        _fs.Close();
                    }
                }
                else
                { 
                    //log :invalid file content 
                    _log("Invalid File format");
                    throw new InvalidDataException("File is in invalid format.");
                }
            }
            else { 
                //log empty folder
                _log("Destination Folder name empty.");
                throw new FileNotFoundException("Destination folder name was provided empty. ");
            }
        }

        public byte[] GetFileByteArray(string FilePath)
        {
            byte[] _outputBytes = null;

            if (File.Exists(FilePath))
            {
                _outputBytes = File.ReadAllBytes(FilePath);
            }
            else { 
            
                //log that file does not exist
                _log("Input File does not exist.");
                throw new FileNotFoundException("Input File does not exist.");
            }
            return _outputBytes;
        }

        public byte[] GetFileByteArray(Uri UriPath)
        {
            throw new NotImplementedException();
        }
    }
}
