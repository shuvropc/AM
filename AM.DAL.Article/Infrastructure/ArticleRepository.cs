using AM.DAL.Articles.Core;
using AM.DAL.Core;
using AM.DAL.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AM.DAL.Articles.Infrastructure
{
    public class ArticleRepository : IArticleRepository
    {
        public void EditArticle(Article article)
        {
            using AMDBContext aMDBContext = new AMDBContext();
            aMDBContext.Article.Update(article);
            aMDBContext.SaveChanges();
        }

        public Article GetArticle(long pId)
        {
            using AMDBContext context = new AMDBContext();
            var result = context.Article.FirstOrDefault(s => s.Id == pId);
            return result;
        }

        public List<Article> GetArticles()
        {
            using AMDBContext context = new AMDBContext();
            var result = context.Article.ToList();
            return result;
        }


        public void SaveArticle(Article pArticle)
        {
            using AMDBContext context = new AMDBContext();
            context.Article.Add(pArticle);
            context.SaveChanges();
        }

        public void VerifyArticle(Article article)
        {
            using AMDBContext aMDBContext = new AMDBContext();
            aMDBContext.Article.Update(article);
            aMDBContext.SaveChanges();
        }

        public void RejectArticle(Article article)
        {
            using AMDBContext aMDBContext = new AMDBContext();
            aMDBContext.Article.Update(article);
            aMDBContext.SaveChanges();
        }

        public List<Article> GetArticlesByAuthor(long pCreatedBy)
        {
            using AMDBContext context = new AMDBContext();
            var result = context.Article.Where(a=>a.CreatedBy==pCreatedBy).ToList();
            return result;
        }

        public List<Article> GetAllApprovedArticles()
        {
            using AMDBContext context = new AMDBContext();
            var result = context.Article.Where(a => a.IsActive == true && a.IsApproved==true && a.IsRejected==false).ToList();
            return result;
        }

        public List<Article> GetAllPendingArticles()
        {
            using AMDBContext context = new AMDBContext();
            var result = context.Article.Where(s=>s.IsApproved==false).ToList();
            return result;
        }

        public List<Article> GetAllApprovedArticlesByTitle(string pArticleTitle)
        {
            using AMDBContext context = new AMDBContext();
            List<Article> articles;
            if (string.IsNullOrEmpty(pArticleTitle))
            {
                articles = context.Article.Where(a => a.IsActive == true && a.IsApproved == true && a.IsRejected == false).ToList();
            }
            else
            {
                articles = context.Article.Where(a => a.IsActive == true && a.IsApproved == true && a.IsRejected == false && a.ArticleTitle.ToLower().Contains(pArticleTitle.ToLower())).ToList();
            }
            return articles;
        }

        public void ChangePassword(long pUserId, string pPassword)
        {
            throw new NotImplementedException();
        }
    }
}
