﻿using Microsoft.AspNetCore.Mvc;
using ShoeShop.Businness.Abstract;
using System.Linq;

namespace ShopShop.ViewComponents
{
    public class BrandFilterViewComponent : ViewComponent
    {
        private readonly IBrandService _brandService;

        public BrandFilterViewComponent(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public IViewComponentResult Invoke()
        {
            var brands = _brandService.GetAllBrands().OrderBy(b => b.Name);
            return View(brands);
        }
    }
}
