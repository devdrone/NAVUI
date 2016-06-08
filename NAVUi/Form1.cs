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
using System.Net;
using System.Xml.Linq;
using System.IO;

namespace NAVUi
{
    public partial class Form1 : Form
    {
        string GeneralURL = string.Empty;
        private Func<XElement, string> e;
        public Form1(Func<XElement, string> e)
        {
            this.e = e;
            InitializeComponent();
        }

        public bool blankcheck()
        {
            if (string.IsNullOrEmpty(serverName.Text) || string.IsNullOrEmpty(userName.Text) ||
                string.IsNullOrEmpty(password.Text) || string.IsNullOrEmpty(instanceName.Text) ||
                string.IsNullOrEmpty(soapPort.Text))
            {
                label7.Visible = true;
                label7.ForeColor = System.Drawing.Color.Red;
                label7.Text = "Please Fill all the values";
                return true;
            }
            return false;
        }

        public void getCompany()
        {
            Uri serviceUrl = getserviceUrl();
            company.Items.Clear();
            company.Text = string.Empty;
            if (!blankcheck())
            {
                const string getNavCompany =
                        @"<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' " +
                                "xmlns:sys='urn:microsoft-dynamics-schemas/nav/system/'>" +
                                "<soapenv:Header/>" +
                                "<soapenv:Body>" +
                                    "<sys:Companies/>" +
                                "</soapenv:Body>" +
                         "</soapenv:Envelope>";

                string namespacestring = @"urn:microsoft-dynamics-schemas/nav/system/";
                XNamespace nsSys = namespacestring;
                string soapAction = string.Format("{0}:{1}", namespacestring, "Companies");
                var client = new WebClient
                {
                    Headers = new WebHeaderCollection
                        {
                            {"Content-Type", "text/xml; charset=utf-8"},
                            {"SOAPAction", soapAction}
                        },
                    Credentials = new NetworkCredential(userName.Text, password.Text)
                };

                var responsestring = client.UploadString(serviceUrl.ToString(), getNavCompany);
                XElement result = XElement.Parse(responsestring);
                if (result.Descendants(nsSys + "return_value").Any())
                {
                    foreach (var Company in result.Descendants(nsSys + "return_value"))
                    {
                        var count = 0;
                        company.Items.Add(Company.Value);
                        count++;
                        label7.Visible = true;
                        label7.ForeColor = System.Drawing.Color.Green;
                        label7.Text = count + " " + "companie(s) fetched";
                    }

                }
            }
        }

        public Uri getserviceUrl()
        {
            var uri = new UriBuilder();
            uri.Host = serverName.Text;
            uri.Port = Convert.ToInt32(soapPort.Text);
            uri.Scheme = Uri.UriSchemeHttp;
            string strPath = string.Format("/{0}/WS/{1}", instanceName.Text, "SystemService");
            uri.Path = strPath;
            return uri.Uri;
        }

        public void save()
        {
            string CompanyUrl = company.Text.Replace(" ", "%20");
            GeneralURL = string.Format("http://{0}:{1}/{2}/WS/{3}", serverName.Text, soapPort.Text, instanceName.Text, CompanyUrl);
            using (XmlWriter settingCreator = XmlWriter.Create("credentials.xml"))
            {
                settingCreator.WriteStartDocument();
                settingCreator.WriteStartElement("setting");
                settingCreator.WriteElementString("Server_Name", serverName.Text);
                settingCreator.WriteElementString("User_Name", userName.Text);
                settingCreator.WriteElementString("Password", password.Text);
                settingCreator.WriteElementString("Instance_Name", instanceName.Text);
                settingCreator.WriteElementString("SOAP_Port", soapPort.Text);
                settingCreator.WriteElementString("Domain", domain.Text);
                settingCreator.WriteElementString("Company", company.Text);
                settingCreator.WriteElementString("URL", GeneralURL);
                settingCreator.WriteEndElement();
                settingCreator.WriteEndDocument();
            }
            label7.ForeColor = Color.Red;
            label7.Text = "Please select the company";
        }

        public void load()
        {
            XElement doc = XElement.Load("credentials.xml");
            serverName.Text = e(doc.Element("Server_Name"));
            userName.Text = e(doc.Element("User_Name"));
            password.Text = e(doc.Element("Password"));
            instanceName.Text = e(doc.Element("Instance_Name"));
            soapPort.Text = e(doc.Element("SOAP_Port"));
            domain.Text = e(doc.Element("Domain"));
            company.Text = e(doc.Element("Company"));
            company.Items.Add(company.Text);
        }

        public void reset()
        {
            serverName.Text = null;
            userName.Text = null;
            password.Text = null;
            instanceName.Text = null;
            soapPort.Text = null;
            domain.Text = null;
            company.Text = null;
            label7.Visible = false;
            label9.Visible = false;
        }

        public void WebServiceUrl()
        {
            XmlDocument doc = new XmlDocument();
            string serviceUrl = string.Format("{0}/{1}", GeneralURL, "Services");
            XElement root = new XElement("Root",
                new XElement("operations"));
            WebInterface WEB = new WebInterface();
            NAVWS navWs = new NAVWS();
            var Urls = XElement.Parse(WEB.Resopnse(serviceUrl));
            foreach(XElement url in Urls.Elements())
            {
                var webserviceURL = url.Attribute("ref").Value;
                if(!webserviceURL.Contains("/WS/SystemService"))
                {
                    int pos = webserviceURL.LastIndexOf("/") + 1;
                    var FileName = webserviceURL.Substring(pos, webserviceURL.Length - pos);
                    var WebService = WEB.Resopnse(webserviceURL);
                    navWs.WebServiceReader(WebService,FileName);    
                }
            }
        }
       
        private void button1_Click_1(object sender, EventArgs e)
        {
            label9.Visible = false;
            try
            {
                getCompany();
            }
            catch (WebException ex)
            {
                label9.Visible = true;
                label9.ForeColor = System.Drawing.Color.Red;
                label9.Text = ex.Message;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label9.Visible = false;
            label7.Visible = false;
            save();
            label7.Visible = true;
            label7.ForeColor = Color.Green;
            label7.Text = "Saved Successfully";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label9.Visible = false;
            label7.Visible = false;
            load();
            label7.Visible = true;
            label7.ForeColor = Color.Green;
            label7.Text = "Loaded Successfully";
        }

        private void pushToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pullToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            WebServiceUrl();
            var main = new Main();
            main.ShowDialog();
        }
    }
}
