using ChatApp.Core.API.Database.Entities;
using Microsoft.EntityFrameworkCore;
using ChatApp.Core.API.Database.Repositories.Interfaces;

namespace ChatApp.Core.API.Database.Repositories
{
    public class ConnectionRepository : EFRepository<Connection>, IConnectionRepository
    {
        public ConnectionRepository(DbContext context) : base(context)
        {
        }
    }
}
