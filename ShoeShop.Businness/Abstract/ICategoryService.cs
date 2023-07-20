using ShoeShop.Dtos.Concrete;
using ShoeShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShop.Businness.Abstract
{
    public interface ICategoryService
    {
        ICollection<Category> GetAllCategories();
        CategoryDto GetCategoryById(int id);
        void UpdateCategory(CategoryDto categoryDto);
        bool isExist(int id);
        int AddCategory(CategoryDto categoryDto);
        void DeleteCategoryById(int categoryDtoId);
    }
}
