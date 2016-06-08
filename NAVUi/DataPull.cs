using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace NAVUi
{
    public partial class DataPull : Form
    {
        
        public DataPull()
        {
            InitializeComponent();
        }

        public string action()
        {
            string pageServece = string.Empty;
            if (comboBox1.Text == "Customer")
            {
                pageServece = "/Page/insync_customer";
            }
            if (comboBox1.Text == "Item")
            {
                pageServece = "/Page/insync_item";
            }
            if (comboBox1.Text == "Sales Order")
            {
                pageServece = "/Page/insync_salesorder";
            }
            if (comboBox1.Text == "Sales Invoice")
            {
                pageServece = "/Page/insync_postedinvoice";
            }
            if (comboBox1.Text == "Sales Shipment")
            {
                pageServece = "/Page/insync_postedshipment";
            }
            return pageServece;
        }

        public string requestPacket(string pageService)
        {
            string requestXML = (@"<soapenv:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/'>" +
                                            "<soapenv:Body>" +
                                            "<ReadMultiple xmlns='urn:microsoft-dynamics-schemas"+pageService.ToLower()+"'>"+
                                                "<filter>" +
                                                    "<Field></Field>" +
                                                    "<Criteria></Criteria>"+
                                                "</filter>" +
                                            "</ReadMultiple>" +
                                            "</soapenv:Body>" +
                                            "</soapenv:Envelope>");
            return requestXML;
        }

        public void NavPull(string XML)
        {
            var credential = XElement.Load("credentials.xml");
            string url = credential.Element("URL").ToString();
            var stopwatch = new Stopwatch();
            stopwatch.Reset();
            stopwatch.Start();
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url+action());
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] bytesToWrite = encoding.GetBytes(XML);

                request.Method = "POST";
                request.ContentLength = bytesToWrite.Length;
                request.Headers.Add("SOAPAction:\"urn:microsoft-dynamics-schemas"+action().ToLower()+":ReadMultiple\"");
                request.UseDefaultCredentials = true;
                request.ContentType = "text/xml; charset=utf-8";

                Stream newStream = request.GetRequestStream();
                newStream.Write(bytesToWrite, 0, bytesToWrite.Length);
                newStream.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);

                string responseFromServer = reader.ReadToEnd();
                var output = XElement.Parse(responseFromServer);
                xmlOutputDisplay.Clear();
                xmlOutputDisplay.Text = output.ToString();
            }
            catch(Exception e)
            {

            }
            stopwatch.Stop();
            label4.Visible = true;
            label4.Text = "Time Taken: " + stopwatch.Elapsed;
        }

        private void button1_Click(object sender, EventArgs e)
        {     
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NavPull(xmlDisplayer.Text);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string actionName=action();
            xmlDisplayer.Clear();
            var output = XElement.Parse(requestPacket(actionName));
            xmlDisplayer.Text = output.ToString();
        }

    }
}
