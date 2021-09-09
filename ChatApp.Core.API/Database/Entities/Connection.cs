using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Core.API.Database.Entities
{
    public class Connection
    {
        [Key]
        public string ConnectionId { get; set;  }
        [ForeignKey("User")]
        public string UserId { get; set;  }

        public virtual User User { get; }
    }
}
