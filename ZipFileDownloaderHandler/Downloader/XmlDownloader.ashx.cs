using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web;

namespace Downloader
{
    /// <summary>
    /// Summary description for XmlDownloader
    /// </summary>
    public class XmlDownloader : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            var files = new List<string>()
            {
                "<note><to>Tove</to><from>Jani</from><heading>Reminder</heading><body>Don't forget me this weekend!</body></note>",
                "<?xml version=\"1.0\"?><catalog><book><author>Gambardella, Matthew</author><title>XML Developer's Guide</title><genre>Computer</genre>"
            };
            var zipInMemory = new MemoryStream();
            int filename = 1;

            using (var updateArchive = new ZipArchive(zipInMemory, ZipArchiveMode.Update))
            {
                foreach (var file in files)
                {
                    var xmlFileEntry = "Xml_" + filename + ".xml";
                    var zipentry = updateArchive.CreateEntry(xmlFileEntry,CompressionLevel.Optimal);
                    var entryFile = updateArchive.Entries.First(x => x.Name.Equals(xmlFileEntry));
                    using (StreamWriter writer = new StreamWriter(entryFile.Open()))
                    {
                        writer.WriteLine(file);
                    }
                    filename ++;
                }
            }
            
            byte[] buffer = zipInMemory.GetBuffer();
            context.Response.AppendHeader("content-disposition",
                "attachment; filename=Zip_" + DateTime.Now.ToString() + ".zip");
            context.Response.AppendHeader("content-length", buffer.Length.ToString());
            context.Response.ContentType = "application/x-compressed";
            context.Response.OutputStream.Write(buffer, 0, buffer.Length);
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}