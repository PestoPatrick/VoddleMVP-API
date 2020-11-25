using System;
using System.Collections.Generic;

#nullable disable

namespace VoddleMVP_API
{
    public partial class Tblvideo
    {
        public Guid Videoid { get; set; }
        public string Videourl { get; set; }
        public Guid Userid { get; set; }
        public string Vidtitle { get; set; }
        public string Userpageurl { get; set; }
        public DateTime Published { get; set; }
        public string Description { get; set; }
        public string Vidthumbnailurl { get; set; }

        public virtual Tbluser User { get; set; }
    }
}
