using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace NAVUi
{
    public partial class Main : Form
    {
        string soapAction = string.Empty;
        string URL = string.Empty;
        public Main()
        {
            InitializeComponent();
            LoadCombo();
        }

        public XElement sopaBody(XNamespace ins,XElement Body)
        {
            XNamespace soapenv = "http://schemas.xmlsoap.org/soap/envelope/";
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            XNamespace xsd = "http://www.w3.org/2001/XMLSchema";
            XNamespace Ins = ins;
            var root = new XElement(soapenv + "Envelope",
                new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
                new XAttribute(XNamespace.Xmlns + "soapenv", "http://schemas.xmlsoap.org/soap/envelope/"),
                new XAttribute(XNamespace.Xmlns+"ins",Ins),
                new XElement(soapenv+"Header"),
                new XElement(soapenv + "Body",Body));
            return root;
        }

        public void LoadCombo()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("credentials.xml");
            var credentials=XElement.Parse(doc.InnerXml);
            var serviceUrl = string.Format("{0}/{1}", credentials.Element("URL").Value, "Services");
            WebInterface WEB = new WebInterface();
            NAVWS navWs = new NAVWS();
            var Urls = XElement.Parse(WEB.Resopnse(serviceUrl));
            foreach (XElement url in Urls.Elements())
            {
                var webserviceURL = url.Attribute("ref").Value;
                if (!webserviceURL.Contains("/SystemService"))
                {
                    int pos = webserviceURL.LastIndexOf("/") + 1;
                    var FileName = webserviceURL.Substring(pos, webserviceURL.Length - pos);
                    WebServiceCombo.Items.Add(FileName);
                }
            }
        }

        public void LoadOperationFile()
        {
            XmlDocument requestDoc = new XmlDocument();
            requestDoc.Load(WebServiceCombo.Text+".xml");
            LoadOperations(requestDoc);
        }

        public void LoadOperations(XmlDocument requestDoc)
        {
            operationBox.Items.Clear();
            var operations = XElement.Parse(requestDoc.InnerXml);
            foreach(XElement operation in operations.Elements())
            {
                operationBox.Items.Add(operation.Name.LocalName);
            }
        }

        public void LoadResopnse()
        {
            requestBox.Clear();
            XmlDocument doc = new XmlDocument();
            doc.Load(WebServiceCombo.Text + ".xml");
            var Operation = XElement.Parse(doc.InnerXml);
            XNamespace ns = Operation.GetNamespaceOfPrefix("ins").NamespaceName;
            soapAction = Operation.Element(ns+operationBox.Text).Element("soapAction").Value;
            Operation.Element(ns+operationBox.Text).Element("soapAction").Remove();
            URL = Operation.Element("URL").Value;
            XElement soapBody = Operation.Element(ns+operationBox.Text);
            var body = sopaBody(ns,soapBody);
            requestBox.Text = body.ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            operationBox.Items.Clear();
            operationBox.Text = "";
            LoadOperationFile();
        }

        private void operationBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadResopnse();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebInterface web = new WebInterface();
            try
            {
                responseBox.Text = web.Request(requestBox.Text, soapAction, URL).ToString();
            }
            catch(Exception ex)
            {
                responseBox.Text = ex.Message;
            }
        }
    }
}
