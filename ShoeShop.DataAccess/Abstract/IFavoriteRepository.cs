﻿using ShoeShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShop.DataAccess.Abstract
{
    public interface IFavoriteRepository : IRepository<Favorite>
    {
        bool IsFavorite(int userID, int productID);
        void RemoveFav(int userId, int productId);
        bool IsExists(int id);
        List<int> GetFavoriteProductsId(int userId);
    }
}
