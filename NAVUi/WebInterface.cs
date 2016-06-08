using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Xml.Linq;

namespace NAVUi
{
    class WebInterface
    {
        
        public string Resopnse(string URL)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
            ASCIIEncoding encoding = new ASCIIEncoding();

            request.Method = "GET";
            request.UseDefaultCredentials = true;
            request.ContentType = "text/xml; charset=utf-8";


            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);

            string responseFromServer = reader.ReadToEnd();

            return responseFromServer;
        }

        public XElement Request(string soaprequest,string soapAction,string URL)
        {
            var header=string.Format("SOAPAction:\"{0}\"",soapAction);
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] bytesToWrite = encoding.GetBytes(soaprequest);

            request.Method = "POST";
            request.ContentLength = bytesToWrite.Length;
            request.Headers.Add(header);
            request.UseDefaultCredentials = true;
            //request.Credentials = new NetworkCredential("devdrone", "devdrone");
            request.ContentType = "text/xml; charset=utf-8";

            Stream newStream = request.GetRequestStream();
            newStream.Write(bytesToWrite, 0, bytesToWrite.Length);
            newStream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);

            string responseFromServer = reader.ReadToEnd();

            var parsedResopnse = XElement.Parse(responseFromServer);
            return parsedResopnse;
        }
    }
}
