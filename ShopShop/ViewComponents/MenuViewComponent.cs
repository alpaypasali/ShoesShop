using Microsoft.AspNetCore.Mvc;
using ShoeShop.Businness.Abstract;
using System.Linq;

namespace ShopShop.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public MenuViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IViewComponentResult Invoke()
        {
            var categories = _categoryService.GetAllCategories().OrderBy(p => p.Name);
            return View(categories);
        }
    }
}
