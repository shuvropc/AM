using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AM.BLL.User.Core;
using AM.BLL.Users.Core;
using AM.DM.User;
using ArticleManagement.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        [Route("Account/Login")]
        public IActionResult Login()
        {
            return View("Login");
        }

        private List<string> GetUserRoles(UserInformationModel user)
        {
            List<string> roles = new List<string>();

            if (user.IsSuperAdmin)
            {
                roles.Add("SuperAdmin");
            }

            if (user.IsArticleUser)
            {
                roles.Add("ArticleUser");
            }

            return roles;
        }

        [HttpPost]
        [Route("Account/Login")]
        public IActionResult Login(UserInformationModel model, string returnUrl)
        {

            try
            {
                bool isUservalid = false;
                model.Password = Convert.ToBase64String(Encoding.Unicode.GetBytes(model.Password));
                UserInformationModel user = _IUserService.GetUserForAuth(model.Email,model.Password);

                if (user != null)
                {
                    isUservalid = true;
                }

                if (isUservalid)
                {
                    var claims = new List<Claim>();

                    string userAccessClaim = JsonConvert.SerializeObject(user).ToString();

                    claims.Add(new Claim("userAccessClaim", userAccessClaim));

                    foreach (var role in GetUserRoles(user))
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var props = new AuthenticationProperties();
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["SeverResponse"] = "Invalid UserName Or Password!";
                }
            }
            catch (Exception ex)
            {
                TempData["SeverResponse"] = "<p class='alert alert-danger'>" + ((ex.InnerException != null) ? ex.GetBaseException().Message : ex.Message) + "!</p>";
            }

            return View("Login");
        }
        [Authorize]
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
        [HttpGet]
        public IActionResult CreateProfessionalProfile()
        {
            ProfessionalProfileModel professionalProfileModel = _IUserService.GetProfessionalProfileByUserId();
            return View("CreateProfessionalProfile", professionalProfileModel);
        }
        [HttpPost]
        public IActionResult CreateProfessionalProfile(ProfessionalProfileModel professionalProfileModel)
        {

            string serverResponse = string.Empty;
            try
            {
                _IUserService.CreateProfessionalProfile(professionalProfileModel);
                serverResponse = "<p class='alert alert-success'>Saved Successfully!</p>";
            }
            catch (Exception ex)
            {
                serverResponse = "<p class='alert alert-danger'>" + ((ex.InnerException != null) ? ex.GetBaseException().Message : ex.Message) + "!</p>";
            }
            TempData["SeverResponse"] = serverResponse;
            return RedirectToAction("CreateProfessionalProfile", "User");
        }
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync( CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View("ChangePassword");
        }
        [HttpPost]
        public IActionResult ChangePassword(string OldPassword, string NewPassword)
        {
            string serverResponse = string.Empty;
            try
            {
                _IUserService.ChangePassword(OldPassword, NewPassword);
                serverResponse = "<p class='alert alert-success'>Password Changed Successfully!</p>";
            }
            catch (Exception ex)
            {
                serverResponse = "<p class='alert alert-danger'>" + ((ex.InnerException != null) ? ex.GetBaseException().Message : ex.Message) + "!</p>";
            }
            TempData["SeverResponse"] = serverResponse;

            return View("ChangePassword");
        }
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View("ForgetPassword");
        }
        [HttpPost]
        public IActionResult ForgetPassword(string Email)
        {
            string serverResponse = string.Empty;
            try
            {
                _IUserService.SendResetPasswordCode(Email);
                TempData["UserEmail"] = Email;
                return RedirectToAction("ResetPassword", "User");
            }
            catch (Exception ex)
            {
                serverResponse = "<p class='alert alert-danger'>" + ((ex.InnerException != null) ? ex.GetBaseException().Message : ex.Message) + "!</p>";
            }
            TempData["SeverResponse"] = serverResponse;

            return View("ForgetPassword");
        }
        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View("ResetPassword");
        }
        [HttpPost]
        public IActionResult ResetPassword(string VerificationCode, string NewPassword)
        {
            string serverResponse = string.Empty;
            try
            {
                string UserEmail = TempData["UserEmail"].ToString();

                _IUserService.ResetPassword(UserEmail, VerificationCode, NewPassword);
                serverResponse = "<p class='alert alert-success'>Password Reset Successfully!</p>";
            }
            catch (Exception ex)
            {
                serverResponse = "<p class='alert alert-danger'>" + ((ex.InnerException != null) ? ex.GetBaseException().Message : ex.Message) + "!</p>";
            }
            TempData["SeverResponse"] = serverResponse;

            return View("ResetPassword");
        }

    }
}