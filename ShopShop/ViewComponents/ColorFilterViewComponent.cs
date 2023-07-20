using Microsoft.AspNetCore.Mvc;
using ShoeShop.Businness.Abstract;
using System.Linq;

namespace ShopShop.ViewComponents
{
    public class ColorFilterViewComponent : ViewComponent
    {
        private readonly IColorService _colorService;

        public ColorFilterViewComponent(IColorService colorService)
        {
            _colorService = colorService;
        }

        public IViewComponentResult Invoke()
        {
            var colors = _colorService.GetAllColors().OrderBy(c => c.Name);
            return View(colors);
        }
    }
}
