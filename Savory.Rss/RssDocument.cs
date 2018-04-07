using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Savory.Rss
{
    public class RssDocument
    {
        public Channel Channel { get; set; }

        public List<Item> Items { get; set; }
    }
}
