using AM.DM.Article;
using System;
using System.Collections.Generic;
using System.Text;

namespace AM.BLL.Articles.Core
{
    public interface IArticleService
    {
        public void SaveArticle(ArticleModel pArticle);
        public List<ArticleModel> GetArticles();
        public ArticleModel GetArticle(long pId);
        public void EditArticle(ArticleModel article);
        public void VerifyArticle(long pId);
        public void RejectArticle(long pId);
        public List<ArticleModel> GetArticlesByAuthor();
        public List<ArticleModel> GetAllApprovedArticles();

    }
}
