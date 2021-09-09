using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Core.API.Database.Entities
{
    public class User
    {
        [Key]
        public string UserId { get; set; }
        public string Username { get; set;  }

        public virtual ICollection<Connection> Connections { get; }
    }
}
