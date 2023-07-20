﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoeShop.Businness.Abstract;
using ShoeShop.Dtos;
using ShoeShop.Dtos.Concrete;

namespace ShopShop.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IColorService _colorService;
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;
        private readonly IGenderService _genderService;
        private readonly IStockService _stockService;
        private readonly IFavoriteService _favoriteManager;
        private readonly IUserService _userManager;

        public ProductsController(IProductService productService, ICategoryService categoryService,
            IBrandService brandService, IColorService colorService, IGenderService genderService,
            IStockService stockService, IFavoriteService favoriteService, IUserService userService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
            _colorService = colorService;
            _genderService = genderService;
            _stockService = stockService;
            _favoriteManager = favoriteService;
            _userManager = userService;
        }
        public IActionResult Show(int page)
        {
            var products = _productService.GetAllProducts();
            var productsPerPage = 5;
            var paginatedProducts = products.OrderBy(x => x.ID)
                .Skip((page - 1) * productsPerPage)
                .Take(productsPerPage);
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = Math.Ceiling((decimal)products.Count / productsPerPage);
            return View(paginatedProducts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = GetCategoriesForDropdown();
            ViewBag.Colors = GetColorsForDropdown();
            ViewBag.Brands = GetBrandsForDropdown();
            ViewBag.Genders = GetGendersForDropdown();
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                _productService.CreateProduct(productDto);
                return RedirectToAction(nameof(Show));
            }

            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Details(int productID)
        {
            var productWithDetails = _productService.GetProductByIdWithDetails(productID);
            ViewBag.Sizes = GetSizes(productID);
            ViewBag.Month1 = GetMonthToDelivery(3);
            ViewBag.Month2 = GetMonthToDelivery(7);
            var userID = User.Identity.Name != null ? _userManager.GetUserByName(User.Identity.Name).ID : 0;
            ViewBag.Favorites = _favoriteManager.GetFavoritesIdByUser(userID);
            return View(productWithDetails);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (_productService.isExist(id))
            {
                ProductDto product = _productService.GetProductById(id);
                ViewBag.Categories = GetCategoriesForDropdown();
                ViewBag.Colors = GetColorsForDropdown();
                ViewBag.Brands = GetBrandsForDropdown();
                ViewBag.Genders = GetGendersForDropdown();
                return View(product);
            }

            return NotFound();

        }

        [HttpPost]
        public IActionResult Edit(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                _productService.UpdateProduct(productDto);
                return RedirectToAction(nameof(Show));
            }
            return View();
        }

        private List<SelectListItem> GetCategoriesForDropdown()
        {
            var selectedItems = new List<SelectListItem>();
            _categoryService.GetAllCategories().ToList().ForEach(cat => selectedItems.Add(new
                SelectListItem
            { Text = cat.Name, Value = cat.ID.ToString() }
            ));
            return selectedItems;
        }
        private List<SelectListItem> GetBrandsForDropdown()
        {
            var selectedItems = new List<SelectListItem>();
            _brandService.GetAllBrands().ToList().ForEach(bnd => selectedItems.Add(new
                SelectListItem
            { Text = bnd.Name, Value = bnd.ID.ToString() }
            ));
            return selectedItems;
        }
        private List<SelectListItem> GetColorsForDropdown()
        {
            var selectedItems = new List<SelectListItem>();
            _colorService.GetAllColors().ToList().ForEach(col => selectedItems.Add(new
                SelectListItem
            { Text = col.Name, Value = col.ID.ToString() }
            ));
            return selectedItems;
        }
        private List<SelectListItem> GetGendersForDropdown()
        {
            var selectedItems = new List<SelectListItem>();
            _genderService.GetAllGenders().ToList().ForEach(gen => selectedItems.Add(new
                SelectListItem
            { Text = gen.Name, Value = gen.ID.ToString() }
            ));
            return selectedItems;
        }
        private string GetMonthToDelivery(int i)
        {
            var monthNumber = DateTime.Today.AddDays(i).Month;
            switch (monthNumber)
            {
                case 1:
                    return "January";
                case 2:
                    return "February";
                case 3:
                    return "March";
                case 4:
                    return "April";
                case 5:
                    return "May";
                case 6:
                    return "June";
                case 7:
                    return "July";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "October";
                case 11:
                    return "November";
                case 12:
                    return "December";
                default:
                    return "";
            }
        }
        private List<SelectListItem> GetSizes(int id)
        {
            var selectedItems = new List<SelectListItem>();
            _stockService.GetSizes(id).ToList().ForEach(stk => selectedItems.Add(new
                SelectListItem
            { Text = stk.SizeName.ToString(), Value = stk.SizeName.ToString() }
            ));
            return selectedItems;
        }


    }
}
