using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AM.BLL.Users.Core;
using AM.DM.User;
using ArticleManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace ArticleManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _IUserService;
        public UserController(IUserService userService)
        {
            _IUserService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public IActionResult Register(UserModel pUserModel)
        {
            string serverResponse = string.Empty;
            try
            {
                _IUserService.Create(pUserModel);
                ViewBag.ServerResponseMessage = "<p class='alert alert-success'>Registered Successfully!</p>";
            }
            catch (Exception ex)
            {
                ViewBag.ServerResponseMessage = "<p class='alert alert-danger'>"+ ((ex.InnerException != null) ? ex.GetBaseException().Message : ex.Message) + "!</p>";
            }

            return View("Register");
        }
    }
}