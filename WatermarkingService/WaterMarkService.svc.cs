using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WatermarkingLib;

namespace WatermarkingService
{    
    public class WaterMarkService : IWaterMarkService
    {
        private IWaterMark _utility;

        public WaterMarkService(IWaterMark Utility)
        {
            _utility = Utility;
        }

        public byte[] WaterMarkFile(byte[] FileContents, string WaterMarkContent)
        {
            byte[] _fileOutput=null;

            try
            {
                _fileOutput  = _utility.WaterMark(FileContents, WaterMarkContent);
            }
            catch (Exception _ex)
            { 
                //Log this
            }
            
            return _fileOutput;
        }
    }
}
    