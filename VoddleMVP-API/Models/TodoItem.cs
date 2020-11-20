using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoddleMVP_API.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
    }
}
