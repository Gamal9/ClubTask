using ClubTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClubTask.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        ClubEF db = new ClubEF();
        public ActionResult Index()
        {
            List<CoatchModel> ModelList;
            try
            {
                List<UserTbl> list = db.UserTbls.ToList();
               
                ModelList = list.Select(x => new CoatchModel
                {
                    ID = x.ID,
                    CoachName = x.CoachName,
                    Age = x.Age,
                    PhoneNumber = x.PhoneNumber,
                    //TrainingName=x.TrainingTbl.TrainingName
                    
                }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
            return View(ModelList);
        }

        public ActionResult AddNew()
        {
            List<TrainingTbl> Dep = db.TrainingTbls.ToList();
            ViewBag.DepartmentList = new SelectList(Dep, "TrainingID", "TrainingName");

            return View();
        }
        [HttpPost]
        public ActionResult AddNew(CoatchModel _newCoach)
        {
            try
            {
                UserTbl user = new UserTbl
                {
                    ID = _newCoach.ID,
                    Age = _newCoach.Age,
                    CoachName = _newCoach.CoachName,
                    PhoneNumber = _newCoach.PhoneNumber,
                    TrainingID = _newCoach.TrainingID
                };
                db.UserTbls.Add(user);
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction("index");
        }

        public ActionResult SelectCoach(int _id)
        {
            
            UserTbl user = db.UserTbls.SingleOrDefault(x => x.ID == _id);
            CoatchModel NewCoach = new CoatchModel
            {
                TrainingName = user.TrainingTbl.TrainingName,
                ID = user.ID,
                CoachName = user.CoachName,
                PhoneNumber = user.PhoneNumber,
                Age = user.Age,
                TrainingID = user.TrainingID
            };

            return View(NewCoach);
        }


        [HttpPost]
        public JsonResult DeleteCoach(int _CoachID)
        {
            UserTbl user = db.UserTbls.SingleOrDefault(x => x.ID == _CoachID);
            bool result = false;
            if(user!=null)
            {
                db.UserTbls.Remove(user);
                db.SaveChanges();
                result = true;
            }

            return Json(result,JsonRequestBehavior.AllowGet);
        }

    }
}