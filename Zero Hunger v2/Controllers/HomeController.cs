using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zero_Hunger_v2.DTOs;
using Zero_Hunger_v2.EF;

namespace Zero_Hunger_v2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact";

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginDTO r)
        {
            var db = new Zero_Hunger_v2Entities();
            var user = (from d in db.Users
                        where d.Username == r.Username && d.Password == r.Password
                        select d).SingleOrDefault();

            if (user != null)
            {
                switch (user.Role)
                {
                    case "Admin":
                        return RedirectToAction("AdminDashboard","Admin");
                    case "Employee":
                        return RedirectToAction("EmployeeDashboard", "Employee");
                    case "Restaurant":
                        return RedirectToAction("RestaurantDashboard", "Restaurant");
                    default:
                        TempData["Msg"] = "Invalid Role";
                        break;
                }
            }
            else
            {
                TempData["Msg"] = "Username or Password Invalid";
            }
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserDTO userDto, RestaurantDTO restaurantDto)
        {
            var db = new Zero_Hunger_v2Entities();

            var userExists = db.Users.Any(u => u.Username == userDto.Username || u.Email == userDto.Email);
            if (userExists)
            {
                ModelState.AddModelError("", "Username or Email already exists.");
                return View(); 
            }

            userDto.Role = "Restaurant";

            var newUser = new User
            {
                Username = userDto.Username,
                Password = userDto.Password, 
                Email = userDto.Email,
                Role = userDto.Role
            };
            db.Users.Add(newUser);
            db.SaveChanges();

            var userId = newUser.UserID;

            var newRestaurant = new Restaurant
            {
                UserID = userId, 
                Name = restaurantDto.Name,
                Address = restaurantDto.Address,
                ContactNumber = restaurantDto.ContactNumber,
                TINID = restaurantDto.TINID
            };
            db.Restaurants.Add(newRestaurant);
            db.SaveChanges();

            return RedirectToAction("Login");
        }

    }
}