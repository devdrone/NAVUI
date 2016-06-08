using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Xml;

namespace NAVUi
{
    class NAVWS
    {
        public void WebServiceReader(string webservice,string fileName)
        {
            XmlDocument doc = new XmlDocument();
            var DOC = XDocument.Parse(webservice);
            XNamespace ins = DOC.Root.FirstAttribute.Value;
            XElement operation = new XElement("Root",
                new XAttribute(XNamespace.Xmlns+"ins",DOC.Root.FirstAttribute.Value));
            XElement element = DOC.Root.Elements(XName.Get(string.Format("{{{0}}}binding", DOC.Root.Name.Namespace))).FirstOrDefault();
            XElement types = DOC.Root.Elements(XName.Get(string.Format("{{{0}}}types", DOC.Root.Name.Namespace))).FirstOrDefault();
            XElement serviceUrl = DOC.Root.Elements(XName.Get(string.Format("{{{0}}}service", DOC.Root.Name.Namespace))).FirstOrDefault();
            var complexType = types.Elements(XName.Get(string.Format("{{{0}}}schema", "http://www.w3.org/2001/XMLSchema"))).FirstOrDefault();
            foreach (XElement operations in element.Descendants(element.Name.Namespace + "operation"))
            {
                if (!operations.Attribute("name").Value.Contains("_Result"))
                {
                    var soapAction = operations.Elements().Attributes("soapAction").ElementAtOrDefault(0).Value;
                    var action1 = new XElement(ins+operations.Attribute("name").Value,
                                    new XElement("soapAction", soapAction));
                    foreach (XElement type in types.Descendants(types.Elements().ElementAt(0).Name.Namespace + "element"))
                    {
                        if (type.Attribute("name").Value == operations.Attribute("name").Value)
                        {
                            foreach (XElement singleType in type.Descendants(types.Elements().ElementAt(0).Name.Namespace + "element"))
                            {
                                var action2 = new XElement(ins+singleType.Attribute("name").Value);
                                action1.Add(action2);
                                if (singleType.Attribute("type").Value.Contains("tns"))
                                {
                                    foreach (XElement service in complexType.Elements())
                                    {
                                        if (service.Attribute("name").Value == singleType.Attribute("type").Value.Remove(0, 4))
                                        {
                                            var action3 = new XElement(ins+singleType.Attribute("type").Value.Remove(0, 4));
                                            foreach (XElement field in service.Elements().Elements())
                                            {
                                                action3 = new XElement(ins+field.Attribute("name").Value, "");
                                                action2.Add(action3);
                                                if (field.Attribute("type").Value.Contains("tns") && field.Attribute("minOccurs").Value.Contains("0"))
                                                {
                                                    foreach (XElement Complexes in complexType.Elements(complexType.Name.Namespace + "complexType"))
                                                    {
                                                        if (Complexes.Attribute("name").Value == field.Attribute("type").Value.Remove(0, 4))
                                                        {
                                                            foreach (XElement complexes2 in complexType.Elements(complexType.Name.Namespace + "complexType"))
                                                            {
                                                                if (complexes2.Attribute("name").Value == Complexes.Attribute("name").Value)
                                                                {
                                                                    foreach (XElement complexex3 in complexes2.Elements().Elements())
                                                                    {
                                                                        var action3_1 = new XElement(ins+complexex3.Attribute("name").Value);
                                                                        foreach (XElement complexes4 in complexType.Elements(complexType.Name.Namespace + "complexType"))
                                                                        {
                                                                            if (complexes4.Attribute("name").Value == complexex3.Attribute("name").Value)
                                                                            {
                                                                                foreach (XElement complexes5 in complexes4.Elements().Elements())
                                                                                {
                                                                                    var action3_2 = new XElement(ins+complexes5.Attribute("name").Value, "");
                                                                                    action3_1.Add(action3_2);
                                                                                }
                                                                            }
                                                                        }
                                                                        action3.Add(action3_1);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                if (field.Attribute("maxOccurs").Value.Contains("unbounded"))
                                                {
                                                    foreach (XElement simpleFields in complexType.Elements())
                                                    {
                                                        if (simpleFields.Attribute("name").Value == field.Attribute("type").Value.Remove(0, 4))
                                                        {
                                                            foreach (XElement field2 in simpleFields.Elements().Elements())
                                                            {
                                                                var action4 = new XElement(ins+field2.Attribute("name").Value, "");
                                                                action3.Add(action4);
                                                                if (field2.Attribute("type").Value.Contains("tns"))
                                                                {
                                                                    foreach (XElement simpleFields2 in complexType.Elements())
                                                                    {
                                                                        if (simpleFields2.Attribute("name").Value == field2.Attribute("type").Value.Remove(0, 4))
                                                                        {
                                                                            foreach (XElement field3 in simpleFields2.Elements().Elements())
                                                                            {
                                                                                if (field3.FirstAttribute.Name.LocalName != "value")
                                                                                {
                                                                                    var action5 = new XElement(ins+field3.Attribute("name").Value, "");
                                                                                    action4.Add(action5);
                                                                                    if (field3.Attribute("maxOccurs").Value.Contains("unbounded"))
                                                                                    {
                                                                                        foreach (XElement simpleFields3 in complexType.Elements())
                                                                                        {
                                                                                            if (simpleFields3.Attribute("name").Value == field3.Attribute("type").Value.Remove(0, 4))
                                                                                            {
                                                                                                foreach (XElement field4 in simpleFields3.Elements().Elements())
                                                                                                {
                                                                                                    var action6 = new XElement(ins+field4.Attribute("name").Value, "");
                                                                                                    action5.Add(action6);
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    operation.Add(action1);
                }
            }
            var address = serviceUrl.Elements().Elements().ElementAt(0).Attribute("location").Value;
            var URL = new XElement("URL", address);
            var ns = new XElement("ns", DOC.Root.LastAttribute.Value);
            operation.Add(URL);
            operation.Add(ns);
            doc.LoadXml(operation.ToString());
            doc.Save(fileName+".xml");
        }
    }
}
