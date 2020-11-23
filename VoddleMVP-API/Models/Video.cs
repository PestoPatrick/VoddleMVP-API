using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoddleMVP_API.Models
{
    public class Video
    {
        public string videoId { get; set; }
        public string videoUrl { get; set; }
        public string userId { get; set; }
        public string vidTitle { get; set; }
        public string userPageUrl { get; set; }
        public DateTime published { get; set; }
        public string description { get; set; }
        public string vidThumbnail { get; set; }
    }
}
