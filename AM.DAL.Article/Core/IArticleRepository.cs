using AM.DAL.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AM.DAL.Articles.Core
{
    public interface IArticleRepository
    {
        public void SaveArticle(Article pArticle);
        public List<Article> GetArticles();
        public List<Article> GetArticlesByAuthor(long pCreatedBy);
        public List<Article> GetAllApprovedArticles();
        public Article GetArticle(long pId);
        public void EditArticle(Article article);
        public void VerifyArticle(Article article);
        public void RejectArticle(Article article);

    }
}
