using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zero_Hunger_v2.DTOs;
using Zero_Hunger_v2.EF;

namespace Zero_Hunger_v2.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult AdminDashboard()
        {
            if (Session["UserID"] == null || Session["Role"].ToString() != "Admin")
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        public ActionResult ViewUserList()
        {
            if (Session["UserID"] == null || Session["Role"].ToString() != "Admin")
            {
                return RedirectToAction("Login", "Home");
            }
            var db = new Zero_Hunger_v2Entities();
            var users = db.Users.ToList(); 
            var userDtos = users.Select(u => new UserDTO
            {
                UserID = u.UserID,
                Username = u.Username,
                Password = u.Password,
                Role = u.Role,
                Email = u.Email
            }).ToList();

            return View(userDtos);
        }
        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(UserDTO userDto)
        {
            if (Session["UserID"] == null || Session["Role"].ToString() != "Admin")
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                var db = new Zero_Hunger_v2Entities();
                var user = new User
                {
                    Username = userDto.Username,
                    Password = userDto.Password, 
                    Role = userDto.Role,
                    Email = userDto.Email
                };

                db.Users.Add(user);
                db.SaveChanges();

                return RedirectToAction("ViewUserList");
            }

            return View(userDto);
        }
        [HttpGet]
        public ActionResult EditUser(int id)
        {
            if (Session["UserID"] == null || Session["Role"].ToString() != "Admin")
            {
                return RedirectToAction("Login", "Home");
            }
            var db = new Zero_Hunger_v2Entities();
            var user = db.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            var userDto = new UserDTO
            {
                UserID = user.UserID,
                Username = user.Username,
                Password = user.Password,
                Role = user.Role,
                Email = user.Email
            };

            return View(userDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(UserDTO userDto)
        {
            if (Session["UserID"] == null || Session["Role"].ToString() != "Admin")
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                var db = new Zero_Hunger_v2Entities();
                var user = db.Users.Find(userDto.UserID);

                if (user == null)
                {
                    return HttpNotFound();
                }

                user.Username = userDto.Username;
                user.Password = userDto.Password; 
                user.Role = userDto.Role;
                user.Email = userDto.Email;

                db.SaveChanges();

                return RedirectToAction("ViewUserList");
            }

            return View(userDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUser(int id)
        {
            if (Session["UserID"] == null || Session["Role"].ToString() != "Admin")
            {
                return RedirectToAction("Login", "Home");
            }
            var db = new Zero_Hunger_v2Entities();
            var user = db.Users.Find(id);

            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction("ViewUserList");
            }

            db.Users.Remove(user);
            db.SaveChanges();

            TempData["SuccessMessage"] = "User deleted successfully.";
            return RedirectToAction("ViewUserList");
        }
        public ActionResult ViewEmployeeDetails()
        {
            if (Session["UserID"] == null || Session["Role"].ToString() != "Admin")
            {
                return RedirectToAction("Login", "Home");
            }
            var db = new Zero_Hunger_v2Entities(); 
            var employeeList = db.Employees.Select(emp => new EmployeeDTO
            {
                EmployeeID = emp.EmployeeID,
                Name = emp.Name,
                Address = emp.Address,
                PhoneNumber = emp.PhoneNumber,
                Nationality = emp.Nationality,
                DOB = emp.DOB,
                Gender = emp.Gender,
                Religion = emp.Religion,
                BloodGroup = emp.BloodGroup,
                UserID = emp.UserID,
                nid = emp.nid
            }).ToList();

            return View(employeeList);
        }
        [HttpGet]
        public ActionResult EditEmployee(int id)
        {
            if (Session["UserID"] == null || Session["Role"].ToString() != "Admin")
            {
                return RedirectToAction("Login", "Home");
            }
            var db = new Zero_Hunger_v2Entities(); 
            var employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            var model = new EmployeeDTO()
            {
                EmployeeID = employee.EmployeeID,
                Name = employee.Name,
                Address = employee.Address,
                PhoneNumber = employee.PhoneNumber,
                Nationality = employee.Nationality,
                DOB = employee.DOB,
                Gender = employee.Gender,
                Religion = employee.Religion,
                BloodGroup = employee.BloodGroup,
                UserID = employee.UserID,
                nid = employee.nid
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEmployee(EmployeeDTO model)
        {
            if (Session["UserID"] == null || Session["Role"].ToString() != "Admin")
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                var db = new Zero_Hunger_v2Entities();
                var employee = db.Employees.Find(model.EmployeeID);
                if (employee != null)
                {
                    employee.Name = model.Name;
                    employee.Address = model.Address;
                    employee.PhoneNumber = model.PhoneNumber;
                    employee.Nationality = model.Nationality;
                    employee.DOB = model.DOB;
                    employee.Gender = model.Gender;
                    employee.Religion = model.Religion;
                    employee.BloodGroup = model.BloodGroup;
                    employee.nid = model.nid;
                    // employee.UserID = model.UserID; 
                    employee.nid = model.nid;

                    db.SaveChanges();
                    return RedirectToAction("ViewEmployeeDetails");
                }
                else
                {
                    ModelState.AddModelError("", "Employee not found");
                }
            }

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteEmployee(int id)
        {
            if (Session["UserID"] == null || Session["Role"].ToString() != "Admin")
            {
                return RedirectToAction("Login", "Home");
            }
            var db = new Zero_Hunger_v2Entities(); 
            var employee = db.Employees.Find(id);
            if (employee == null)
            {
                TempData["Error"] = "The employee does not exist.";
                return RedirectToAction("ViewEmployeeDetails");
            }

            

            db.Employees.Remove(employee);

            db.SaveChanges();
            TempData["Success"] = "Employee deleted successfully.";
            

            return RedirectToAction("ViewEmployeeDetails");
        }

        [HttpGet]
        public ActionResult AddEmployee()
        {
            if (Session["UserID"] == null || Session["Role"].ToString() != "Admin")
            {
                return RedirectToAction("Login", "Home");
            }
            return View(new EmployeeDTO());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEmployee(EmployeeDTO employeeDto)
        {
            if (Session["UserID"] == null || Session["Role"].ToString() != "Admin")
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                var db = new Zero_Hunger_v2Entities();
                var employee = new Employee
                {
                    Name = employeeDto.Name,
                    Address = employeeDto.Address,
                    PhoneNumber = employeeDto.PhoneNumber,
                    Nationality = employeeDto.Nationality,
                    DOB = employeeDto.DOB,
                    Gender = employeeDto.Gender,
                    Religion = employeeDto.Religion,
                    BloodGroup = employeeDto.BloodGroup,
                    UserID = employeeDto.UserID, 
                    nid = employeeDto.nid
                };

                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("ViewEmployeeDetails");
            }

            
            return View(employeeDto);
        }
        public ActionResult ViewFoodCollection()
        {
            if (Session["UserID"] == null || Session["Role"].ToString() != "Admin")
            {
                return RedirectToAction("Login", "Home");
            }
            var db = new Zero_Hunger_v2Entities();
            var foodCollectionList = db.FoodCollections.Select(fc => new FoodCollectionDTO
            {
                RID = fc.RID,
                FoodName = fc.FoodName,
                amount = fc.amount,
                MaxPreserve = fc.MaxPreserve,
                Status = fc.Status,
                AssignedEmpID = fc.AssignedEmpID,
                CollectionTime = fc.CollectionTime,
                CompletionTime = fc.CompletionTime,
                Note = fc.Note,
                RestID = fc.RestID,
                RequestTime = fc.RequestTime
            }).ToList();

            return View(foodCollectionList);
        }
        public ActionResult ViewListRestaurantDetails()
        {
            if (Session["UserID"] == null || Session["Role"].ToString() != "Admin")
            {
                return RedirectToAction("Login", "Home");
            }
            var db = new Zero_Hunger_v2Entities(); 
            var restaurants = db.Restaurants.ToList();

            var restaurantDtos = restaurants.Select(r => new RestaurantDTO
            {
                RestaurantID = r.RestaurantID,
                Name = r.Name,
                Address = r.Address,
                ContactNumber = r.ContactNumber,
                UserID = r.UserID,
                TINID = r.TINID
            }).ToList();

            return View(restaurantDtos);
        }
        [HttpGet]
        public ActionResult AddRestaurant()
        {
            if (Session["UserID"] == null || Session["Role"].ToString() != "Admin")
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRestaurant(RestaurantDTO restaurantDto)
        {
            if (Session["UserID"] == null || Session["Role"].ToString() != "Admin")
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                var db = new Zero_Hunger_v2Entities();
                var restaurant = new Restaurant 
                {
                    Name = restaurantDto.Name,
                    Address = restaurantDto.Address,
                    ContactNumber = restaurantDto.ContactNumber,
                    UserID = restaurantDto.UserID,
                    TINID = restaurantDto.TINID
                };

                db.Restaurants.Add(restaurant);
                db.SaveChanges();

                return RedirectToAction("ViewListRestaurantDetails");
            }

            return View(restaurantDto);
        }
        [HttpGet]
        public ActionResult EditRestaurant(int id)
        {
            if (Session["UserID"] == null || Session["Role"].ToString() != "Admin")
            {
                return RedirectToAction("Login", "Home");
            }
            var db = new Zero_Hunger_v2Entities(); 
            var restaurantEntity = db.Restaurants.Find(id);
            if (restaurantEntity == null)
            {
                return HttpNotFound();
            }

            var restaurantDto = new RestaurantDTO
            {
                RestaurantID = restaurantEntity.RestaurantID,
                Name = restaurantEntity.Name,
                Address = restaurantEntity.Address,
                ContactNumber = restaurantEntity.ContactNumber,
                UserID = restaurantEntity.UserID,
                TINID = restaurantEntity.TINID
            };

            return View(restaurantDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRestaurant(RestaurantDTO restaurantDto)
        {
            if (Session["UserID"] == null || Session["Role"].ToString() != "Admin")
            {
                return RedirectToAction("Login", "Home");
            }
            if (!ModelState.IsValid)
            {
                return View(restaurantDto);
            }

            var db = new Zero_Hunger_v2Entities(); 
            var restaurantEntity = db.Restaurants.Find(restaurantDto.RestaurantID);
            if (restaurantEntity == null)
            {
                return HttpNotFound();
            }

            restaurantEntity.Name = restaurantDto.Name;
            restaurantEntity.Address = restaurantDto.Address;
            restaurantEntity.ContactNumber = restaurantDto.ContactNumber;
            //restaurantEntity.UserID = restaurantDto.UserID;
            restaurantEntity.TINID = restaurantDto.TINID;

            db.SaveChanges();

            return RedirectToAction("ViewListRestaurantDetails");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRestaurant(int id)
        {
            if (Session["UserID"] == null || Session["Role"].ToString() != "Admin")
            {
                return RedirectToAction("Login", "Home");
            }
            var db = new Zero_Hunger_v2Entities();
            var restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                TempData["Error"] = "The restaurant does not exist or has already been deleted.";
                return RedirectToAction("ViewListRestaurantDetails");
            }

            try
            {
                db.Restaurants.Remove(restaurant);
                db.SaveChanges();
                TempData["Success"] = "The restaurant has been successfully deleted.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "There was an error deleting the restaurant: " + ex.Message;
            }

            return RedirectToAction("ViewListRestaurantDetails");
        }




    }
}