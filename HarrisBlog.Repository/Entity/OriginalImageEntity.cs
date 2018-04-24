using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarrisBlog.Repository.Entity
{
    class OriginalImageEntity
    {
        public int Id { get; set; }

        public string ImageId { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string FileName { get; set; }

        public string FileExtension { get; set; }

        public string LocalFilePath { get; set; }

        public string RemoteFilePath { get; set; }

        public string FakeFilePath { get; set; }
    }
}
