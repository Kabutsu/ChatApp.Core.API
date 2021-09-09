using Microsoft.EntityFrameworkCore;
using ChatApp.Core.API.Database.Entities;
using ChatApp.Core.API.Database.Repositories.Interfaces;

namespace ChatApp.Core.API.Database.Repositories
{
    public class UserRepository : EFRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
    }
}
