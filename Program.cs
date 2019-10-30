using System;
using System.Xml;

namespace NopLanCompare
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Compare Begining....");

            XmlDocument xmlSourcedoc = new XmlDocument();
            xmlSourcedoc.Load("language_pack_source.xml");

            XmlNodeList allResources = xmlSourcedoc.GetElementsByTagName("LocaleResource");
            XmlNode lanNodeInSource = xmlSourcedoc.GetElementsByTagName("Language")[0];


            XmlDocument xmlTargetdoc = new XmlDocument();
            xmlTargetdoc.Load("language_pack_target.xml");

            XmlNodeList allTargetResources = xmlTargetdoc.GetElementsByTagName("LocaleResource");

            XmlNode lanNodeInTarget = xmlTargetdoc.GetElementsByTagName("Language")[0];


            XmlDocument newDoc = new XmlDocument();
            //给xml创建节点
            XmlElement rootNode = newDoc.CreateElement("Language");
            newDoc.AppendChild(rootNode);

            XmlNodeList newNodeList = newDoc.ChildNodes;

            foreach (XmlElement node in allResources) {
                var cureentNodeName = node.Attributes[0].Value;

                //print all the LocaleResource which not in the source doc
                XmlNode nodeInTarget = xmlTargetdoc.SelectSingleNode("//LocaleResource[@Name='"+ cureentNodeName+"']");
                if (nodeInTarget == null) {
                    Console.WriteLine(cureentNodeName);

                    var newElement = newDoc.CreateElement("LocaleResource");
                    newElement.SetAttribute("Name", cureentNodeName);

                    var innerElement = newDoc.CreateElement("Value");

                    //baidu translation
                    string transText = node.InnerText;

                    try
                    {
                        var tranText = BaiduTranslationService.Translate(node.InnerText);

                        var transResult = JsonHelper.DeserializeJSON<TransResult>(tranText);
                        transText = transResult.trans_result[0].dst;
                    }
                    catch { }

                    innerElement.InnerText = transText;

                    newElement.AppendChild(innerElement);

                    rootNode.AppendChild(newElement);
                }
            }

            newDoc.Save("newTarget.xml");
        }
    }
}
