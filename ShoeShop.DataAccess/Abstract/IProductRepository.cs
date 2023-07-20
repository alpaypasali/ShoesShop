using ShoeShop.Dtos.Concrete;
using ShoeShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShop.DataAccess.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
        bool IsExists(int id);
        IQueryable<ProductDto> GetProductByIdWithDetails(int id);
        IQueryable<ProductDto> GetProductById(int id);
        ICollection<ProductDto> GetAllActiveProductsWithBrand();
        void SoftDelete(Product product);
    }
}
