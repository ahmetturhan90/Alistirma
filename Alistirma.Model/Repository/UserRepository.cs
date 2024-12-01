using Alistirma.Data;
using Alistirma.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alistirma.Infrastructure.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        //TODO: Write here custom methods that are required for specific requirements.
    }
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(TestDbContext empDBContext)
            : base(empDBContext)
        {
        }
        //TODO: Write here custom methods that are required for specific requirements.
    }
}
