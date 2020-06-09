using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AM.BLL.User.Core;
using AM.BLL.Users.Core;
using AM.DM.User;
using ArticleManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace ArticleManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _IUserService;
        private readonly IProfessionService _IProfessionService;
        private readonly IOrganizationService _IOrganizationService;
        public UserController(IUserService userService, IProfessionService professionService, IOrganizationService organizationService)
        {
            _IUserService = userService;
            _IProfessionService = professionService;
            _IOrganizationService = organizationService;
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
        public IActionResult Register(UserInformationModel pUserModel)
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
        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpGet]
        public IActionResult Profile()
        {
            UserInformationModel userInformationModel = _IUserService.GetUserProfile();
            List<ProfessionModel> professions = _IProfessionService.GetAllProfessions();
            List<OrganizationModel> organizations = _IOrganizationService.GetAllOrganizations();
            ViewBag.Professions = professions;
            ViewBag.Organizations = organizations;
            return View("Profile", userInformationModel);
        }
        [HttpPost]
        public IActionResult Profile(UserInformationModel pUserModel)
        {
            string serverResponse = string.Empty;
            try
            {
                _IUserService.UpdateProfile(pUserModel);
                serverResponse = "<p class='alert alert-success'>Update Successfully!</p>";
            }
            catch (Exception ex)
            {
                serverResponse = "<p class='alert alert-danger'>" + ((ex.InnerException != null) ? ex.GetBaseException().Message : ex.Message) + "!</p>";
            }
            TempData["SeverResponse"] = serverResponse;
            return RedirectToAction("Profile", "User");
        }
    }
}