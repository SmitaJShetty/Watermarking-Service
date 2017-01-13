using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace WatermarkingLib
{
    public interface IWaterMark
    {
        byte[] WaterMark(
            byte[] FileContents, string WatermarkText
            );
    }

    public class WatermarkingUtility:IWaterMark
    {
        public byte[] WaterMark(byte[] FileContents, string WatermarkText)
        {
            byte[] _outputFileContents = null;

            PdfReader _pdfReader = new PdfReader(FileContents);

            if (_pdfReader != null)
            {
                using (MemoryStream _memStrm = new MemoryStream())
                {
                    PdfStamper _pdfStamper = new PdfStamper(_pdfReader, _memStrm);

                    for (int i = 1; i <= _pdfReader.NumberOfPages; i++)
                    {
                        Rectangle _pageSize = _pdfReader.GetPageSizeWithRotation(i);
                        PdfContentByte _pdfContent = _pdfStamper.GetUnderContent(i);
                   
                        _pdfContent.BeginText();

                        BaseFont _font = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD,Encoding.ASCII.EncodingName,false);
                        _pdfContent.SetFontAndSize(_font, 40);
                        _pdfContent.SetRGBColorFill(244, 244, 244);
                        _pdfContent.ShowTextAligned(PdfContentByte.ALIGN_CENTER, WatermarkText, _pageSize.Width / 2, _pageSize.Height / 2, 45);
                        _pdfContent.EndText();
                    }

                    _pdfStamper.FormFlattening=true;
                    _pdfStamper.Close();

                    _outputFileContents= _memStrm.ToArray();
               }
            }
                    return _outputFileContents;
        }
    }
}
