using Alistirma.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alistirma.Business.Abstract
{
    public interface IUserService
    {
        public List<User> getUserList();
    }
}
