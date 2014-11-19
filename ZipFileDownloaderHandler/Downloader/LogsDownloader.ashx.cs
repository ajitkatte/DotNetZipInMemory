using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Ionic.Zip;

namespace Downloader
{
    /// <summary>
    /// Summary description for XmlLogsDownloader
    /// </summary>
    public class LogsDownloader : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "application/zip";
            //context.Response.ClearContent();
            //context.Response.ClearHeaders();
            //context.Response.AppendHeader("content-disposition", "attachment; filename=Report.zip");
            //int id = -1;
            //int.TryParse(context.Request.QueryString["id"], out id);
            //ZipOutputStream zipStream = new ZipOutputStream(context.Response.OutputStream);
            //MemoryStream stream = new MemoryStream();
            //UnicodeEncoding uniEncoding = new UnicodeEncoding();
            //var logs = new Dictionary<string, List<string>>();
            //logs.Add("10230",new List<string>(){"request","World"});
            
            //foreach (KeyValuePair<string, List<string>> keyValuePair in logs)
            //{
            //    foreach (var log in keyValuePair.Value)
            //    {
            //        if (log.Equals("request", StringComparison.OrdinalIgnoreCase))
            //            zipStream.PutNextEntry("request" + "_" + keyValuePair.Key+".xml");
            //        else
            //            zipStream.PutNextEntry("response" + "_" + keyValuePair.Key+".xml");

            //        MemoryStream inStream = new MemoryStream();

            //        inStream.Write(System.Text.Encoding.UTF8.GetBytes(log), 0, System.Text.Encoding.UTF8.GetBytes(log).Length-1);
            //        inStream.Seek(0, SeekOrigin.Begin);
                    
            //        //zipStream.    // False stops the Close also Closing the underlying stream.
                    
            //    }
                           
            //}
            ////zipStream.Seek(0, SeekOrigin.Begin);
            //zipStream.Close();
            MemoryStream stream = new MemoryStream();
            UnicodeEncoding uniEncoding = new UnicodeEncoding();
            var sw = new StreamWriter(stream, uniEncoding);
            try
            {
                string request = null;
                string response = null;
               
                    request = "Hello";
                    response = "World";
               
                sw.Write(request);
                sw.Flush();//otherwise you are risking empty stream
                stream.Seek(0, SeekOrigin.Begin);
                using (var memory = new MemoryStream())
                using (var zipFile = new ZipFile())
                {
                    zipFile.AddEntry("Report.txt", stream);
                    context.Response.ClearContent();
                    context.Response.ClearHeaders();
                    context.Response.AppendHeader("content-disposition", "attachment; filename=Report.zip");

                    zipFile.Save(memory);
                    memory.Seek(0, SeekOrigin.Begin);
                    memory.CopyTo(context.Response.OutputStream);

                    //--Needed to add the following 2 lines to make it work----

                    //  ms.WriteTo(context.Response.OutputStream);
                    //zipFile.Save(context.Response.OutputStream);

                }
                // Test and work with the stream here. 
                // If you need to start back at the beginning, be sure to Seek again.
            }
            finally
            {
                sw.Dispose();
            }
            stream.Seek(0, SeekOrigin.Begin);   // <-- must do this after writing the stream!

        }

        private static Dictionary<byte[],int> GetBytes(string file)
        {
            var dictionary = new Dictionary<byte[], int>();
            dictionary.Add(System.Text.Encoding.UTF8.GetBytes(file), System.Text.Encoding.UTF8.GetBytes(file).Length);
            return dictionary;
        }




        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}