using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Message { get; set; }

        public bool MySelf { get; set; }
    }
}
