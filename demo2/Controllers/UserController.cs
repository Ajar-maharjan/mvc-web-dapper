using demo2.Logic;
using demo2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace demo2.Controllers
{
    public class UserController : Controller
    {

        public ActionResult GetUserList()
        {
            UserCRUD userCRUD = new UserCRUD();
            return View(userCRUD.GetUserList());
        }

        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(UserModel userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserCRUD userCRUD = new UserCRUD();
                    userCRUD.AddUser(userModel);
                    ViewBag.Message = "User added successfully";
                    return RedirectToAction("GetUserList");
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        public ActionResult EditUser(int Id)
        {
            UserCRUD userCRUD = new UserCRUD();
            return View(userCRUD.GetUserList().Find(userModel => userModel.UserID == Id));
        }

        [HttpPost]
        public ActionResult EditUser(int Id, UserModel userModel)
        {
            try
            {
                UserCRUD userCRUD = new UserCRUD();
                userCRUD.UpdateUser(Id, userModel);
                return RedirectToAction("GetUserList");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteUser(int Id)
        {
            try
            {
                UserCRUD userCRUD = new UserCRUD();
                if (userCRUD.DeleteUser(Id))
                {
                    ViewBag.AlertMsg = "User Deleted";
                }
                return RedirectToAction("GetUserList");

            }
            catch
            {
                return RedirectToAction("GetUserList");
            }
        }
    }
}