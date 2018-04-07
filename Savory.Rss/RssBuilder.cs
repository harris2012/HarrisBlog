using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Savory.Rss
{
    public class RssBuilder
    {
        class NodeNameConst
        {
            public const string rss = "rss";
            public const string channel = "channel";
            public const string Title = "title";
            public const string Link = "link";
            public const string Description = "description";
            public const string Language = "language";
            public const string Item = "item";
            public const string PubDate = "pubDate";
            public const string Author = "author";
            public const string Category = "category";
        }

        private readonly XmlDocument document = new XmlDocument();

        public string Build(RssDocument source)
        {
            document.AppendChild(document.CreateXmlDeclaration("1.0", "utf-8", null));

            var rss = document.CreateElement(NodeNameConst.rss);
            rss.SetAttribute("version", "2.0");
            document.AppendChild(rss);

            XmlElement channel = document.CreateElement(NodeNameConst.channel);
            rss.AppendChild(channel);

            AppendTextNode(channel, NodeNameConst.Title, source.Channel.Title);
            AppendTextNode(channel, NodeNameConst.Link, source.Channel.Link);
            AppendTextNode(channel, NodeNameConst.Description, source.Channel.Description);
            AppendTextNode(channel, NodeNameConst.Language, source.Channel.Language);

            if (source.Items != null && source.Items.Count > 0)
            {
                foreach (var item in source.Items)
                {
                    AppendItemNode(channel, item);
                }
            }

            XmlWriterSettings setting = new XmlWriterSettings();
            setting.Indent = true;
            setting.Encoding = Encoding.UTF8;

            using (StringWriter stringWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, setting))
                {
                    document.WriteContentTo(xmlWriter);
                }

                return stringWriter.ToString();
            }
        }

        private void AppendTextNode(XmlElement root, string nodeName, string nodeValue)
        {
            if (string.IsNullOrEmpty(nodeValue))
            {
                return;
            }

            var node = document.CreateElement(nodeName);

            node.AppendChild(document.CreateTextNode(nodeValue));

            root.AppendChild(node);
        }

        private void AppendItemNode(XmlElement root, Item item)
        {
            if (item == null)
            {
                return;
            }

            var node = document.CreateElement(NodeNameConst.Item);

            AppendTextNode(node, NodeNameConst.Title, item.Title);
            AppendTextNode(node, NodeNameConst.Link, item.Link);
            AppendTextNode(node, NodeNameConst.Author, item.Author);
            AppendTextNode(node, NodeNameConst.PubDate, item.PubDate);
            AppendTextNode(node, NodeNameConst.Description, item.Description);

            root.AppendChild(node);
        }
    }
}
