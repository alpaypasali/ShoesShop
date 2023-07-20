using ShoeShop.Dtos.Concrete;
using ShoeShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShop.Businness.Abstract
{
    public interface IBrandService
    {
        ICollection<Brand> GetAllBrands();
        Brand GetBrand(int id);
        bool IsExist(int id);
        void UpdateBrand(BrandDto brandDto);
        int AddBrand(Brand brand);
        void DeleteBrand(int brandId);
    }
}
