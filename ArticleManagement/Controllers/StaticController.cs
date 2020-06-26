using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ArticleManagement.Controllers
{
    public class StaticController : Controller
    {
        public IActionResult HomeStatic()
        {
            return View();
        }
        public IActionResult GuideLinesMenuScripts()
        {
            return View();
        }
        public IActionResult SubmitReview()
        {
            return View();
        }
        public IActionResult JSDReviewerInstruction()
        {
            return View();
        }
        public IActionResult GuideLinesForAcceptedArticles()
        {
            return View();
        }
        public IActionResult ReplicationFilesandArticleVerificationProcess()
        {
            return View();
        }
        public IActionResult SectionsoftheManuscript()
        {
            return View();
        }
        public IActionResult SubmissionProcessforFinalDraft()
        {
            return View();
        }
        public IActionResult EditorialBoard()
        {
            return View();
        }
    }

}