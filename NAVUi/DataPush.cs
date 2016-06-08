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
    public partial class DataPush : Form
    {

        public DataPush()
        {
            InitializeComponent();
        }

        public void refresh()
        {
            if (!string.IsNullOrEmpty(path.Text))
            {
                xmlDisplayer.ForeColor = Color.Black;
                xmlDisplayer.Visible = true;
                xmlDisplayer.Text = XmlDoc(path.Text).ToString();
            }
            else
            {
                xmlDisplayer.ForeColor = Color.Red;
                xmlDisplayer.Visible = true;
                xmlDisplayer.Text = "Please Select the XML file.";
            }
        }

        public string selectPath()
        {       
            OpenFileDialog dialogue=new OpenFileDialog();
            dialogue.Filter = "XML File(*.xml)|*.xml";
            if (dialogue.ShowDialog() == DialogResult.OK)
            {
                path.Text = dialogue.FileName;
                xmlDisplayer.Visible = true;
                xmlDisplayer.Text = XmlDoc(path.Text).ToString();
            }
            return xmlDisplayer.Text;
        }

        public string processSelect(string process)
        {
            var action = string.Empty;
            if(process=="Customer")
            {
                action = "WebCustomerImport";
                return action;
            }
            if (process== "Item")
            {
                action = "WebSimpleProductImport";
                return action;
            }
            if (process== "Sales Order")
            {
                action = "WebOrderImport";
                return action;
            }
            if (process== "Sales Invoice")
            {
                action = "WebOrderInvoicePost";
                return action;
            }
            return string.Empty;
        }

        public XElement XmlDoc(string path)
        {
            var displayDoc = XElement.Load(path);
            return displayDoc;
        }

        public bool saveXml()
        {
            try
            {
                var editedDoc = XDocument.Parse(xmlDisplayer.Text);
                editedDoc.Save(path.Text);
                return true;
            }
            catch(Exception ex)
            {
                var error = ex.Message;
                xmlDisplayer.Clear();
                xmlDisplayer.ForeColor = Color.Red;
                xmlDisplayer.Text = ex.Message;
                return false;
            }
        }

        public void navImport()
        {
            var credential = XElement.Load("credentials.xml");
            string url = credential.Element("URL").ToString();
            var stopwatch = new Stopwatch();
            stopwatch.Reset();
            stopwatch.Start();
            string process = processSelect(processes.Text);
            if (saveXml())
            {
                try
                {
                    string soaprequest = (@"<soapenv:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/'>" +
                                            "<soapenv:Body>" +
                                            "<" + process + " " + "xmlns='urn:microsoft-dynamics-schemas/codeunit/insync_import'>" +
                                                "<dataFilePath>" + path.Text + "</dataFilePath>" +
                                            "</WebCustomerImport>" +
                                            "</soapenv:Body>" +
                                            "</soapenv:Envelope>");
                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url+"/Codeunit/insync_import");
                    ASCIIEncoding encoding = new ASCIIEncoding();
                    byte[] bytesToWrite = encoding.GetBytes(soaprequest);

                    request.Method = "POST";
                    request.ContentLength = bytesToWrite.Length;
                    request.Headers.Add("SOAPAction:\"urn:microsoft-dynamics-schemas/codeunit/insync_import\"");
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
                    xmlDisplayer.Clear();
                    if(output.Value.Contains("error"))
                    {
                        xmlDisplayer.ForeColor = Color.Red;
                    }
                    xmlDisplayer.Text = output.Value;
                }
                catch (WebException ex)
                {
                    string actualError;
                    var response = ex.Response;
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (var responseReader = new StreamReader(responseStream))
                        {
                            actualError = responseReader.ReadToEnd();
                            var errordata = XElement.Parse(actualError);
                            actualError = errordata.Value;
                            xmlDisplayer.Clear();
                            xmlDisplayer.ForeColor = Color.Red;
                            xmlDisplayer.Text = actualError;
                        }
                    }
                }
                stopwatch.Stop();
                label3.Visible = true;
                label3.Text = "Time Taken: " + stopwatch.Elapsed;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            selectPath();
        }

        private void pushNav_Click(object sender, EventArgs e)
        {
            navImport();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.Hide();
            //var form1 = new Form1(e);
            //form1.ShowDialog();
            //this.Close();
        }
    }
}
