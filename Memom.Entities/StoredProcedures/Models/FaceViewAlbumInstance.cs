using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memom.Entities.Models
{
    public partial class FaceViewAlbumInstance : UserAlbumInstanceDetail
    {
        public string PhotoFile { get; set; }
        public string OriginalFile { get; set; }
        public string AiFolderPath { get; set; }
        public string AiAbsolutePath { get; set; }
        public string ThumbnailFile { get; set; }
        public string SmallPhotoFile { get; set; }
        public string MediumPhotoFile { get; set; }
        public string LargePhotoFile { get; set; }

    }
}
