using ShoeShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShop.DataAccess.Abstract
{
    public interface ICategoryRepository : IRepository<Category>
    {
        bool IsExists(int id);
    }
}
