using ShoeShop.Dtos.Concrete;
using ShoeShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShop.Businness.Abstract
{
    public interface IProductService
    {
        ICollection<Product> GetAllProducts();
        ICollection<ProductDto> GetAllActiveProductsWithBrand();
        int CreateProduct(ProductDto productDto);
        ProductDto GetProductByIdWithDetails(int id);
        ProductDto GetProductById(int id);
        int UpdateProduct(ProductDto productDto);
        bool isExist(int id);

        void SoftDelete(ProductDto productDto);
    }
}
