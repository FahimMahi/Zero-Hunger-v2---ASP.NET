using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zero_Hunger_v2.DTOs;
using Zero_Hunger_v2.EF;

namespace Zero_Hunger_v2.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult EmployeeDashboard()
        {
            if (Session["UserID"] == null || Session["Role"].ToString() != "Employee")
            {
                return RedirectToAction("Login", "Home");
            }

            return View();
        }
        [HttpGet]
        public ActionResult EditEmployeeDetails()
        {
            if (Session["UserID"] == null || Session["Role"].ToString() != "Employee")
            {
                return RedirectToAction("Login", "Home");
            }
            var db = new Zero_Hunger_v2Entities(); 
            int userId = Convert.ToInt32(Session["UserID"]);

            var employee = db.Employees.FirstOrDefault(e => e.UserID == userId);
            if (employee == null)
            {
                return HttpNotFound("Employee not found.");
            }

            var employeeDto = new EmployeeDTO
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

            return View(employeeDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEmployeeDetails(EmployeeDTO employeeDto)
        {
            if (Session["UserID"] == null || Session["Role"].ToString() != "Employee")
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                var db = new Zero_Hunger_v2Entities();
                var employee = db.Employees.Find(employeeDto.EmployeeID);

                if (employee != null)
                {
                    employee.Name = employeeDto.Name;
                    employee.Address = employeeDto.Address;
                    employee.PhoneNumber = employeeDto.PhoneNumber;
                    employee.Nationality = employeeDto.Nationality;
                    employee.DOB = employeeDto.DOB;
                    employee.Gender = employeeDto.Gender;
                    employee.Religion = employeeDto.Religion;
                    employee.BloodGroup = employeeDto.BloodGroup;
                    employee.UserID = employeeDto.UserID; 
                    employee.nid = employeeDto.nid;

                    db.SaveChanges();
                    TempData["Msg"] = "Succesfully Done";
                    return RedirectToAction("EmployeeDashboard");
                }
                else
                {
                    ModelState.AddModelError("", "Employee not found.");
                }
            }

            return View(employeeDto);
        }
        [HttpGet]
        public ActionResult ViewAllRequestsByAllRestaurants()
        {
            if (Session["UserID"] == null || Session["Role"].ToString() != "Employee")
            {
                return RedirectToAction("Login", "Home");
            }

            var db = new Zero_Hunger_v2Entities(); 
            var requests = db.FoodCollections
                             .Select(r => new FoodCollectionDTO
                             {
                                 RID = r.RID,
                                 FoodName = r.FoodName,
                                 amount = r.amount,
                                 MaxPreserve = r.MaxPreserve,
                                 Status = r.Status,
                                 Note = r.Note,
                                 RestID = r.RestID,
                                 AssignedEmpID = r.AssignedEmpID,
                                 CollectionTime = r.CollectionTime,
                                 CompletionTime = r.CompletionTime,
                                 RequestTime = r.RequestTime
                             })
                             .ToList();

            return View(requests);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateRequestStatus(int requestId)
        {
            if (Session["UserID"] == null || Session["Role"].ToString() != "Employee")
            {
                return RedirectToAction("Login", "Home");
            }

            var db = new Zero_Hunger_v2Entities();
            var request = db.FoodCollections.Find(requestId);

            if (request != null && request.Status == "Pending")
            {
                request.Status = "Collected";
                request.CollectionTime = DateTime.Now;
                request.AssignedEmpID = Convert.ToInt32(Session["UserID"]); 

                try
                {
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Request has been marked as 'Collected'.";
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "An error occurred while updating the record: " + ex.Message;
                }
            }
            else if (request != null && request.Status == "Collected")
            {
                request.Status = "Distributed";
                request.CompletionTime = DateTime.Now;
                request.AssignedEmpID = Convert.ToInt32(Session["UserID"]);

                try
                {
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Request has been marked as 'Collected'.";
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "An error occurred while updating the record: " + ex.Message;
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Request not found or already collected.";
            }

            return RedirectToAction("ViewAllRequestsByAllRestaurants");
        }
        /*[HttpGet]
        public ActionResult CreateFoodDistribution()
        {
            if (Session["UserID"] == null || Session["Role"].ToString() != "Employee")
            {
                return RedirectToAction("Login", "Home");
            }

            return View(new FoodDistributerDTO());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]*/
        /*public ActionResult CreateFoodDistribution(int requestId, FoodDistributerDTO distributorDto)
        {
            if (Session["UserID"] == null || Session["Role"].ToString() != "Employee")
            {
                return RedirectToAction("Login", "Home");
            }

            var db = new Zero_Hunger_v2Entities();
            var request = db.FoodCollections.Find(requestId);

            if (request != null && request.Status != "Distributed")
            {
                request.Status = "Distributed";
                request.CollectionTime = DateTime.Now;
                request.AssignedEmpID = Convert.ToInt32(Session["UserID"]);

                var distribution = new FoodDistributer
                {
                    Distributetime = DateTime.Now,
                    Location = distributorDto.Location,
                    Distributednumber = distributorDto.Distributednumber,
                    notes = distributorDto.notes,
                    RID = requestId,
                    EmpID = request.AssignedEmpID 
                };

                db.FoodDistributers.Add(distribution);

                try
                {
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "The food collection request has been marked as 'Distributed'.";
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "An error occurred while updating the record: " + ex.Message;
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Request not found or has already been distributed.";
            }

            return RedirectToAction("ViewAllRequestsByAllRestaurants");
        }*/



    }
}