using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoeShop.Businness.Abstract;
using ShoeShop.Dtos.Concrete;
using ShoeShop.Entities.Concrete;
using System.Collections.Generic;

namespace ShopShop.Controllers
{
    [Authorize]
    public class FavoriteController : Controller
    {
        private readonly IFavoriteService _favoriteManager;
        private readonly IProductService _productManager;
        private readonly IUserService _userManager;

        public FavoriteController(IFavoriteService favoriteService, IProductService productService, IUserService userService)
        {
            _favoriteManager = favoriteService;
            _productManager = productService;
            _userManager = userService;
        }
        public IActionResult Show()
        {
            var userID = _userManager.GetUserByName(User.Identity.Name).ID;
            var favoriteProducts = GetFavoriteProducts(userID);
            return View(favoriteProducts);
        }

        private List<ProductDto> GetFavoriteProducts(int userID)
        {
            var productsId = _favoriteManager.GetFavoriteProductsId(userID);
            var favoriteProducts = new List<ProductDto>();
            foreach (var id in productsId)
            {
                favoriteProducts.Add(_productManager.GetProductById(id));
            }
            return favoriteProducts;
        }

        public IActionResult AddFavorite(int id)
        {
            if (_productManager.isExist(id))
            {
                int userId = User.Identity.Name != null ? _userManager.GetUserByName(User.Identity.Name).ID : 0;
                if (!_favoriteManager.IsExist(id))
                {
                    _favoriteManager.AddFavorite(id, userId);
                    return Json("Add to Favorite");
                }

                return Json("The product is already in the favorites!");
            }

            return NotFound("This Product Could Not Be Found!");
        }

        public IActionResult RemoveFavorite(int id)
        {
            if (_favoriteManager.IsExist(id))
            {
                int userId = User.Identity.Name != null ? _userManager.GetUserByName(User.Identity.Name).ID : 0;
                _favoriteManager.RemoveFavorite(userId, id);
                return Json("Removed from Favorites");
            }

            return Json("The Product Was Not Found!");
        }
    }
}
