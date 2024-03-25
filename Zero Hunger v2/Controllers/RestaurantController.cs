using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zero_Hunger_v2.DTOs;
using Zero_Hunger_v2.EF;

namespace Zero_Hunger_v2.Controllers
{
    public class RestaurantController : Controller
    {
        // GET: Restaurant
        public ActionResult RestaurantDashboard()
        {
            var restaurantUserID = Session["UserID"] as int?; 

            if (!restaurantUserID.HasValue)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        [HttpGet]
        public ActionResult CreateRequest()
        {
            if (Session["Role"] == null || Session["Role"].ToString() != "Restaurant")
            {
                return RedirectToAction("Login", "Home");
            }

            var foodCollectionDto = new FoodCollectionDTO
            {
                Status = "Pending",
                RequestTime = DateTime.Now, 
                RestID = Convert.ToInt32(Session["UserID"]) 
            };

            return View(foodCollectionDto);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRequest(FoodCollectionDTO foodCollectionDto)
        {
            if (Session["Role"] == null || Session["Role"].ToString() != "Restaurant")
            {
                ModelState.AddModelError("", "Unauthorized access. Please log in as a restaurant.");
                return View(foodCollectionDto);
            }

            foodCollectionDto.RestID = Convert.ToInt32(Session["UserID"]);

            if (ModelState.IsValid)
            {
                var db = new Zero_Hunger_v2Entities(); 

                var newRequest = new FoodCollection 
                {
                    FoodName = foodCollectionDto.FoodName,
                    amount = foodCollectionDto.amount,
                    MaxPreserve = foodCollectionDto.MaxPreserve,
                    Status = foodCollectionDto.Status,
                    Note = foodCollectionDto.Note,
                    RestID = foodCollectionDto.RestID, 
                    RequestTime = DateTime.Now 
                                               
                };

                db.FoodCollections.Add(newRequest);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while saving the request: " + ex.Message);
                    return View(foodCollectionDto);
                }

                TempData["Msg"] = "Requested Succesfully";
                return RedirectToAction("RestaurantDashboard"); 
            }


            return View(foodCollectionDto);
        }



        [HttpGet]
        public ActionResult EditRestaurantInfo()
        {
            var db = new Zero_Hunger_v2Entities();

            int userId = Convert.ToInt32(Session["UserID"]);

            var restaurantQuery = from r in db.Restaurants
                                  where r.UserID == userId
                                  select r;
            var restaurant = restaurantQuery.FirstOrDefault();

            if (restaurant == null)
            {
                return HttpNotFound();
            }

            var restaurantDto = new RestaurantDTO
            {
                RestaurantID = restaurant.RestaurantID,
                Name = restaurant.Name,
                Address = restaurant.Address,
                ContactNumber = restaurant.ContactNumber, 
                UserID = restaurant.UserID,
                TINID = restaurant.TINID
            };

            return View(restaurantDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRestaurantInfo(RestaurantDTO restaurantDto)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (Session["Role"] == null || Session["Role"].ToString() != "Restaurant")
            {
                ModelState.AddModelError("", "Unauthorized access. Please log in as a restaurant.");
                return View(restaurantDto);
            }
            if (ModelState.IsValid)
            {
                var db = new Zero_Hunger_v2Entities();
                var restaurant = db.Restaurants.Find(restaurantDto.RestaurantID);

                if (restaurant != null)
                {
                    restaurant.Name = restaurantDto.Name;
                    restaurant.Address = restaurantDto.Address;
                    restaurant.ContactNumber = restaurantDto.ContactNumber; 
                    restaurant.TINID = restaurantDto.TINID;

                    db.SaveChanges();
                    TempData["Msg"] = "Restaurant Info Updated Succesfully";

                    return RedirectToAction("RestaurantDashboard");
                }
                else
                {
                    ModelState.AddModelError("", "Restaurant not found.");
                }
            }

            return View(restaurantDto);
        }

        [HttpGet]
        public ActionResult ViewListRequest()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (Session["Role"] == null || Session["Role"].ToString() != "Restaurant")
            {
                return RedirectToAction("Login", "Home");
            }

            var db = new Zero_Hunger_v2Entities();
            int userId = Convert.ToInt32(Session["UserID"]);

            var requests = db.FoodCollections
                             .Where(r => r.RestID == userId)
                             .Select(r => new FoodCollectionDTO
                             {
                                 RID = r.RID,
                                 FoodName = r.FoodName,
                                 amount = r.amount,
                                 MaxPreserve = r.MaxPreserve,
                                 Status = r.Status,
                                 Note = r.Note,
                                 RestID = r.RestID,
                                 RequestTime = r.RequestTime
                             }).ToList();

            return View(requests);
        }
        [HttpGet]
        public ActionResult UpdateRequest(int id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (Session["Role"] == null || Session["Role"].ToString() != "Restaurant")
            {
                return RedirectToAction("Login", "Home");
            }

            var db = new Zero_Hunger_v2Entities();
            var request = db.FoodCollections.Find(id);

            if (request == null || request.RestID != Convert.ToInt32(Session["UserID"]))
            {
                return HttpNotFound();
            }

            var requestDto = new FoodCollectionDTO
            {
                RID = request.RID,
                FoodName = request.FoodName,
                amount = request.amount,
                MaxPreserve = request.MaxPreserve,
                Status = request.Status,
                Note = request.Note,
                RestID = request.RestID,
                RequestTime = request.RequestTime
            };

            return View(requestDto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateRequest(FoodCollectionDTO requestDto)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (Session["Role"] == null || Session["Role"].ToString() != "Restaurant")
            {
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                var db = new Zero_Hunger_v2Entities();
                var request = db.FoodCollections.Find(requestDto.RID);

                if (request != null && request.RestID == Convert.ToInt32(Session["UserID"]))
                {
                    request.FoodName = requestDto.FoodName;
                    request.amount = requestDto.amount;
                    request.MaxPreserve = requestDto.MaxPreserve;
                    request.Status = requestDto.Status;
                    request.Note = requestDto.Note;

                    db.Entry(request).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Msg"] = "Request Updated Succesfully";
                    return RedirectToAction("ViewListRequest"); 
                }
                else
                {
                    ModelState.AddModelError("", "Request not found or unauthorized access.");
                }
            }

            return View(requestDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRequest(int id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (Session["Role"] == null || Session["Role"].ToString() != "Restaurant")
            {
                return RedirectToAction("Login", "Home");
            }
            var db = new Zero_Hunger_v2Entities(); 
            var requestToDelete = db.FoodCollections.Find(id);
            if (requestToDelete == null)
            {
                TempData["ErrorMessage"] = "Request not found.";
                return RedirectToAction("ViewListRequest");
            }

            if (requestToDelete.RestID != Convert.ToInt32(Session["UserID"]))
            {
                TempData["ErrorMessage"] = "Unauthorized to delete this request.";
                return RedirectToAction("ViewListRequest");
            }

            db.FoodCollections.Remove(requestToDelete);
            db.SaveChanges();

            TempData["SuccessMessage"] = "Request deleted successfully.";
            return RedirectToAction("ViewListRequest");
        }
    }
}