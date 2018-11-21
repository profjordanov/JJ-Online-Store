using JjOnlineStore.Services.Core;

using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

using System.IO;

namespace JjOnlineStore.Services.Business
{
    public class PdfGenerator : IPdfGenerator
    {
        public byte[] GeneratePdfFromHtml(string html)
        {
            var pdfDoc = new Document(PageSize.A4, 20f, 20f, 20f, 5f);
            var htmlParser = new HTMLWorker(pdfDoc);

            using (var memoryStream = new MemoryStream())
            {
                PdfWriter.GetInstance(pdfDoc, memoryStream);
                pdfDoc.Open();

                using (var reader = new StringReader(html))
                {
                    htmlParser.Parse(reader);
                }

                pdfDoc.Close();

                return memoryStream.ToArray();
            }
        }
    }
}