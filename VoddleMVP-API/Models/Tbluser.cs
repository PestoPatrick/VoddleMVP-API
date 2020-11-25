using System;
using System.Collections.Generic;

#nullable disable

namespace VoddleMVP_API
{
    public partial class Tbluser
    {
        public Tbluser()
        {
            Tblvideos = new HashSet<Tblvideo>();
        }

        public Guid Userid { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Passwordhash { get; set; }

        public virtual ICollection<Tblvideo> Tblvideos { get; set; }
    }
}
