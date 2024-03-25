using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zero_Hunger_v2.DTOs;
using Zero_Hunger_v2.EF;
using Zero_Hunger_v2.Models;

namespace Zero_Hunger_v2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
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
                Session["Username"] = user.Username;
                Session["Role"] = user.Role;
                Session["UserID"] = user.UserID; 
                switch (user.Role)
                {
                    case "Admin":
                        return RedirectToAction("AdminDashboard", "Admin");
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
        public ActionResult Register(RestaurantRegistrationViewModel viewModel)
        {
            var db = new Zero_Hunger_v2Entities();
            
            var userExists = db.Users.Any(u => u.Username == viewModel.User.Username || u.Email == viewModel.User.Email);
            if (userExists)
            {
                ModelState.AddModelError("", "Username or Email already exists.");
                return View(viewModel);
            }

            var newUser = new User
            {
                Username = viewModel.User.Username,
                Password = viewModel.User.Password,
                Email = viewModel.User.Email,
                Role = "Restaurant"
            };
            db.Users.Add(newUser);
            db.SaveChanges();

            var newRestaurant = new Restaurant
            {
                Name = viewModel.Restaurant.Name,
                Address = viewModel.Restaurant.Address,
                ContactNumber = viewModel.Restaurant.ContactNumber, 
                UserID = newUser.UserID,
                TINID = viewModel.Restaurant.TINID
            };
            db.Restaurants.Add(newRestaurant);
            db.SaveChanges();

            TempData["Msg"] = "Created Account Succesfully";
            return RedirectToAction("Login"); 
        }
        [HttpGet]
        public ActionResult EditUserProfile()
        {
            var db = new Zero_Hunger_v2Entities();

            int userId = Convert.ToInt32(Session["UserID"]);
            var userQuery = from r in db.Users
                                  where r.UserID == userId
                                  select r;
            var user = userQuery.FirstOrDefault();

            if (user == null)
            {
                return HttpNotFound();
            }

            var userDto = new UserDTO
            {
                UserID = user.UserID,
                Username = user.Username,
                Email = user.Email
            };

            return View(userDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUserProfile(UserDTO userDto)
        {
            if (ModelState.IsValid)
            {
                var db = new Zero_Hunger_v2Entities();
                var user = db.Users.Find(userDto.UserID);

                if (user != null)
                {
                    user.Username = userDto.Username;
                    user.Email = userDto.Email;
                    db.SaveChanges();
                    TempData["Msg"] = "User Info Updated Succesfully";

                    Session["Username"] = user.Username;
                    Session["Role"] = user.Role;
                    Session["UserID"] = user.UserID;
                    switch (user.Role)
                    {
                        case "Admin":
                            return RedirectToAction("AdminDashboard", "Admin");
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
                    ModelState.AddModelError("", "User not found.");
                }
            }

            return View(userDto);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Home");
        }
    }
}