﻿using ShoeShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShop.DataAccess.Abstract
{
    public interface IBrandRepository : IRepository<Brand>
    {
        bool IsExists(int id);
    }
}
