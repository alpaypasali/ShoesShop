using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShoeShop.Business.Abstract;
using ShoeShop.Businness.Abstract;
using ShoeShop.Dtos;
using ShoeShop.Dtos.Concrete;
using ShoeShop.Entities.Concrete;
using ShopShop.Models;

namespace ShopShop.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userManager;
        private readonly IOrderService _orderService;

        public UsersController(IUserService userService,IOrderService orderService)
        {
            _userManager = userService;
            _orderService = orderService;
        }

        public IActionResult Login(string returnURL)
        {
            ViewBag.ReturnURL = returnURL;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserDto userDto, string returnURL)
        {
            if (returnURL == null)
            {
                returnURL = "/";
            }
            if (ModelState.IsValid)
            {
                var user = _userManager.ValidateUser(userDto.Email, userDto.Password);
                if (user != null)
                {
                    List<Claim> claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.Name,user.FullName),
                            new Claim(ClaimTypes.Role,user.Role),
                            new Claim(ClaimTypes.StreetAddress, user.Address),
                            new Claim(ClaimTypes.MobilePhone,user.PhoneNumber),
                        };
                    ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    if (Url.IsLocalUrl(returnURL))
                    {
                        return Redirect(returnURL);
                    }
                }
                ModelState.AddModelError("Login", "kullanıcı adı veya şifre hatalı!");
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        public IActionResult Details(string username)
        {
            var userDto = _userManager.GetUserByName(username);
            ViewBag.State = null;
            return View(userDto);
        }

        [HttpPost]
        public IActionResult Details(UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.GetUser(userDto.Email);
                if (BCrypt.Net.BCrypt.Verify(userDto.oldPassword, user.Password))
                {
                    userDto.Role = user.Role;
                    _userManager.UpdateUser(userDto);
                    ViewBag.State = true;
                    return View();
                }
            }
            ViewBag.State = false;
            return View();
        }

        public IActionResult AccessDenied(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                _userManager.AddUser(userDto);
                return RedirectToAction(nameof(Login));
            }
            return View();
        }

        public IActionResult Index()
        {
            List<UserOrderModel> result = new ();
            var username = User.Identity.Name;
            var orders = _orderService;
            var user = _userManager.GetUserByName(username);
            var Orders = 
                    user.Role!="admin"?
                            _orderService.GetOrders(user.ID)
                            :_orderService.GetAll();
            foreach (var item in Orders)
            {
                var userOrder = user.Role == "admin" ?
                                _userManager.GetUserById(item.UserID)
                                : user;
                result.Add(new UserOrderModel
                {
                    Id = item.ID,
                    OrderDate = item.OrderDate,
                    OrderNumber = item.OrderNumber,
                    orderState = item.OrderState,
                    Total = (int)item.TotalPrice,
                    User = userOrder
                });
            }

             return View(result);
        }

        public IActionResult OrderDetails(string orderNumber)
        {


            var order = _orderService.GetOrderWithProductDetails(orderNumber);
            return View(order);
        }

        [HttpGet]
        public IActionResult GetAllUsersSortedByName()
        {
            var users = _userManager.GetAllUsersSortedByName();
            return View(users);
        }

        [HttpPost]
        public IActionResult ChangeUserRole(string username, string newRole)
        {
            try
            {
                _userManager.ChangeUserRole(username, newRole);
                return RedirectToAction("GetAllUsersSortedByName");
            }
            catch (ArgumentException ex)
            {
                // Обработка ошибок: можно добавить код для записи ошибки в журнал или отображения сообщения об ошибке пользователю
                return View("Error");
            }
        }
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddUser(UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                _userManager.AddUser(userDto);
                return RedirectToAction(nameof(GetAllUsersSortedByName));
            }
            return View();
        }

        public IActionResult DeleteUser(string username)
        {
            try
            {
                _userManager.DeleteUserByName(username);
                return RedirectToAction(nameof(GetAllUsersSortedByName)); // Silme işlemi başarılıysa, ana sayfaya yönlendirilir
            }
            catch (ArgumentException ex)
            {
                // Silme işlemi sırasında hata oluşursa, hata mesajını göstermek için kullanıcıyı aynı sayfaya yönlendirir
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

    }
}
