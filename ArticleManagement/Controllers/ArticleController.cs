using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AM.BLL.Articles.Core;
using AM.DM.Article;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ArticleManagement.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _IArticleService;
        public ArticleController(IArticleService articleService)
        {
            _IArticleService = articleService;
        }

        [HttpGet]
        public IActionResult PendingArticles()
        {
            return View("PendingArticle");
        }

        [HttpGet]
        public string GetAllPendingArticles()
        {
            var result = _IArticleService.GetAllPendingArticles();
            var jsonRes = JsonConvert.SerializeObject(result).ToString();
            return jsonRes;
        }


        [HttpGet]
        public IActionResult GetAllApprovedArticle()
        {
            return View("ApprovedArticle");
        }

        [HttpGet]
        public string GetAllApprovedArticles()
        {
            var result = _IArticleService.GetAllApprovedArticles();
            var jsonRes = JsonConvert.SerializeObject(result).ToString();
            return jsonRes;
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public IActionResult Create(ArticleModel article, IFormFile File)
        {
            BinaryReader reader = new BinaryReader(File.OpenReadStream());
            byte[] CoverImageBytes = reader.ReadBytes((int)File.Length);

            article.FileName = File.FileName;
            article.File = CoverImageBytes;

            string serverResponse = string.Empty;
            try
            {
                _IArticleService.SaveArticle(article);
                ViewBag.ServerResponseMessage = "<p class='alert alert-success'>Article Uploaded Successfully!</p>";
            }
            catch (Exception ex)
            {
                ViewBag.ServerResponseMessage = "<p class='alert alert-danger'>" + ((ex.InnerException != null) ? ex.GetBaseException().Message : ex.Message) + "!</p>";
            }
            return View("Create");
        }

        [HttpGet]
        public IActionResult VerifyArticle(long pArticleId)
        {
            ArticleModel result = _IArticleService.GetArticle(pArticleId);
            return View("Verify", result);
        }

        [HttpGet]
        public IActionResult Edit(long pArticleId)
        {
            var article = _IArticleService.GetArticle(pArticleId);
            return View("Edit", article);
        }
        [HttpPost]
        public IActionResult Edit(ArticleModel pArticleId, IFormFile File)
        {
            try
            {
                if (File != null)
                {
                    BinaryReader reader = new BinaryReader(File.OpenReadStream());
                    byte[] CoverImageBytes = reader.ReadBytes((int)File.Length);
                    pArticleId.File = CoverImageBytes;
                    pArticleId.FileName = File.FileName;
                }

                _IArticleService.EditArticle(pArticleId);
                TempData["SeverResponse"] = "<p class='alert alert-success'>Article Updated Successfully!</p>";
            }
            catch (Exception ex)
            {
                TempData["SeverResponse"] = "<p class='alert alert-danger'>" + ((ex.InnerException != null) ? ex.GetBaseException().Message : ex.Message) + "!</p>";
            }

            return RedirectToAction("Edit","Article", new { pArticleId = pArticleId.Id });
        }

        [HttpGet]
        public IActionResult Details(long pArticleId)
        {
            var article = _IArticleService.GetArticle(pArticleId);
            return View("Details", article);
        }
        public IActionResult DownloadDocument(long pArticleId)
        {
            var article = _IArticleService.GetArticle(pArticleId);
            string fileName = article.FileName;
            byte[] fileBytes = article.File;
            return File(fileBytes, "application/force-download", fileName);
        }

        [HttpGet]
        public IActionResult ConfirmVerifyArticle(long pArticleId)
        {
            _IArticleService.VerifyArticle(pArticleId);
            return RedirectToAction("PendingArticles", "Article");
        }
        public IActionResult RejectArticle(long pArticleId)
        {
            _IArticleService.RejectArticle(pArticleId);
            return RedirectToAction("PendingArticles", "Article");
        }

        [HttpGet]
        public IActionResult GetMyArticles()
        {
            return View("AuthorArticle");
        }

        [HttpGet]
        public string GetAuthorArticles()
        {
            var result = _IArticleService.GetArticlesByAuthor();
            var jsonRes = JsonConvert.SerializeObject(result).ToString();
            return jsonRes;
        }
    }
}