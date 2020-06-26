using AM.BLL.Articles.Core;
using AM.DAL.Articles.Core;
using AM.DAL.Core.Entities;
using AM.DM.Article;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using UserAccessTokenClaim.Core;

namespace AM.BLL.Articles.Infrastructure
{
    public class ArticleService : IArticleService
    {
        private readonly IMapper _IMapper;
        private readonly IArticleRepository _IArticleRepository;
        private readonly IUserAccessTokenClaimsService _IUserAccessTokenClaims;
        public ArticleService(IMapper mapper, IArticleRepository articleRepository, IUserAccessTokenClaimsService userAccessTokenClaims)
        {
            _IMapper = mapper;
            _IArticleRepository = articleRepository;
            _IUserAccessTokenClaims = userAccessTokenClaims;
        }
        public void EditArticle(ArticleModel article)
        {
            var loggedUser = _IUserAccessTokenClaims.GetCurrentLoggedUserInfo();
            var fetchedArticle = _IArticleRepository.GetArticle(article.Id);

            fetchedArticle.ArticleTitle = article.ArticleTitle;
            fetchedArticle.ArticleDescription = article.ArticleDescription;

            if (fetchedArticle.File != article.File && article.File!=null)
            {
                fetchedArticle.File = article.File;
                fetchedArticle.FileName = article.FileName;
            }

            fetchedArticle.ModifiedBy = loggedUser.Id;
            fetchedArticle.DateModified = DateTime.Now;
            fetchedArticle.DateModified = DateTime.Now;

            _IArticleRepository.EditArticle(fetchedArticle);
        }

        public List<ArticleModel> GetAllApprovedArticles()
        {
            var articles = _IArticleRepository.GetAllApprovedArticles();
            var articlesModel = _IMapper.Map<List<Article>, List<ArticleModel>>(articles);
            return articlesModel;
        }

        public ArticleModel GetArticle(long pId)
        {
            var articles = _IArticleRepository.GetArticle(pId);
            var articlesModel = _IMapper.Map<Article, ArticleModel>(articles);
            return articlesModel;
        }

        public List<ArticleModel> GetArticles()
        {
            var articles = _IArticleRepository.GetArticles();
            var articlesModel = _IMapper.Map<List<Article>, List<ArticleModel>>(articles);
            return articlesModel;
        }

        public List<ArticleModel> GetArticlesByAuthor()
        {
            var loggedUser = _IUserAccessTokenClaims.GetCurrentLoggedUserInfo();
            var articles = _IArticleRepository.GetArticlesByAuthor(loggedUser.Id);
            var articlesModel = _IMapper.Map<List<Article>, List<ArticleModel>>(articles);
            return articlesModel;
        }

        public void RejectArticle(long pId)
        {
            var loggedUser = _IUserAccessTokenClaims.GetCurrentLoggedUserInfo();
            var fetchedArticle = _IArticleRepository.GetArticle(pId);
            fetchedArticle.IsRejected = true;
            fetchedArticle.IsApproved = false;
            fetchedArticle.RejectedBy = loggedUser.Id;
            fetchedArticle.DateModified = DateTime.Now;
            _IArticleRepository.RejectArticle(fetchedArticle);
        }

        public void SaveArticle(ArticleModel pArticle)
        {
            var loggedUser = _IUserAccessTokenClaims.GetCurrentLoggedUserInfo();
            pArticle.CreatedBy = loggedUser.Id;
            pArticle.DateCreated = DateTime.Now;
            pArticle.IsActive = true;
            pArticle.IsApproved = false;
            pArticle.IsRejected = false;
            pArticle.Version = 1;
            var articleToSave = _IMapper.Map<ArticleModel, Article>(pArticle);
            _IArticleRepository.SaveArticle(articleToSave);
        }

        public void VerifyArticle(long pId)
        {
            var loggedUser = _IUserAccessTokenClaims.GetCurrentLoggedUserInfo();
            var fetchedArticle = _IArticleRepository.GetArticle(pId);
            fetchedArticle.IsRejected = false;
            fetchedArticle.IsApproved = true;
            fetchedArticle.ApprovedBy = loggedUser.Id;
            fetchedArticle.DateModified = DateTime.Now;
            _IArticleRepository.VerifyArticle(fetchedArticle);
        }
    }
}
