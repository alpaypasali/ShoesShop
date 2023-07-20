    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
using ShoeShop.Business.Abstract;
using ShoeShop.Businness.Abstract;
    using ShoeShop.Entities.Concrete;
    using ShopShop.Extensions;
    using ShopShop.Models;
    using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using static ShoeShop.Entities.Concrete.Order;

namespace ShopShop.Controllers
    {
        public class CartController : Controller
        {
            private readonly IProductService _productService;
      private readonly IOrderService _orderService;
        private readonly IUserService _userService;

            public CartController(IProductService productService, IOrderService orderService
                , IUserService userService)
            {
                _productService = productService;
            _orderService=orderService;
            _userService = userService; 
            }
            public IActionResult Basket()
            {
                var cartCollection = getCollectionFromSession();
                return View(cartCollection);
            }

            public IActionResult Add(int id)
            {
                if (_productService.isExist(id))
                {
                    var product = _productService.GetProductById(id);
                    CartCollection cartCollection = getCollectionFromSession();
                    cartCollection.Add(new CartItem { Product = product, Quantity = 1 });
                    saveToSession(cartCollection);
                    var cartCollections = getCollectionFromSession();
                    return Json($"{product.Name} Add to Cart");
                }

                return NotFound("This Product Could Not Be Found!");
            }

            private void saveToSession(CartCollection cartCollection)
            {
                HttpContext.Session.SetJson("Cart", cartCollection);
            }

            private CartCollection getCollectionFromSession()
            {
                CartCollection cartCollection = null;
                cartCollection = HttpContext.Session.GetJson<CartCollection>("Cart") ?? new CartCollection();
                return cartCollection;
            }

            public IActionResult Delete(int id)
            {
                CartCollection cartCollection = getCollectionFromSession();
                cartCollection.Delete(id);
                saveToSession(cartCollection);
                return Json("The Product Has Been Removed From the Cart");
            }

            public IActionResult CheckOut()
            {

                return View(new ShippingDetails() );

            }
        [HttpPost]
        public IActionResult CheckOut(ShippingDetails entity)
        {
            var cart = getCollectionFromSession();
            if (cart.CartItems.Count == 0)
            {
                ModelState.AddModelError("Ürün Error", "sepette ürün yok");
            }
            if (ModelState.IsValid)
            {
                SaveOrder(cart, entity);
                cart.ClearAll();
                saveToSession(cart); // remember to clear the cart in the session as well
                return View("Completed"); // assuming you have a Completed.cshtml view
            }

            // If model state is not valid, return the same view to display the validation errors
            return View(entity);
        }


        private int SaveOrder(CartCollection cart, ShippingDetails entity)
        {
            var order = new Order();
            order.OrderNumber = "A" + (new Random().Next(1111, 9999).ToString());
            order.TotalPrice = cart.GetTotalPrice();
            order.OrderDate = DateTime.Now;

            var user = _userService.GetUserByName(User.Identity.Name);
            if (user != null)
            {
                order.UserID = user.ID;
                var checkUserPayment = _orderService.CheckpaymentByUser(user.ID, (decimal)order.TotalPrice);
                if (!checkUserPayment.Keys.FirstOrDefault())
                {

                    //burada hata donecek   return checkUserPayment.Values.FirstOrDefault();
                }
            }
            else
            {
                // Kullanıcı bulunamadı, gerekli hata işlemlerini gerçekleştirin
                // Örneğin, throw new Exception("Kullanıcı bulunamadı"); gibi bir hata fırlatabilirsiniz.
            }

            order.FullName = User.Identity.Name;
            order.Address = entity.Address;
            order.Sehir = entity.City;
            order.OrderState = Order.EnumOrderState.waiting;
            order.Semt = entity.District;
            order.PostaKodu = entity.PostCode;
            order.OrderItems = new List<OrderItem>();

            foreach (var cartItem in cart.CartItems)
            {
                var orderItem = new OrderItem
                {
                    ProductID = cartItem.Product.ID,
                    Quantity = cartItem.Quantity,
                    Price = cartItem.Product.Price,
                };
                order.OrderItems.Add(orderItem);
            }

            _orderService.Add(order);

            return order.ID;
        }

        public IActionResult ChangeOrderStatus(int orderId, int OrderStatus)
        {
            var userName = User.Identity.Name;
            var user = _userService.GetUserByName(userName);
            if(user.Role == "admin")
            {
                var status = OrderStatus;
                var result = _orderService.ChangeOrderStatus(orderId, (EnumOrderState)OrderStatus);
                return Json(result);
            }
            return BadRequest();
        }
    }
}
