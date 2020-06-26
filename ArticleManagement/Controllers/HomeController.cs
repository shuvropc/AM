using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ArticleManagement.Models;
using AM.BLL.Articles.Core;

namespace ArticleManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArticleService _IArticleService;


        public HomeController(ILogger<HomeController> logger, IArticleService articleService)
        {
            _logger = logger;
            _IArticleService = articleService;
        }

        public IActionResult Index(string particleTitle=null)
        {
            var result = _IArticleService.GetAllApprovedArticlesByTitle(particleTitle);
            TempData["ApprovedArticle"] = result;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
