using BlogServices.Models;
using BlogServices.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;

namespace BlogServices.Services
{
    public class ArticlesService : IArticlesService
    {
        private readonly IArticlesRepository articlesRepoitory;

        public ArticlesService(IArticlesRepository articlesRepoitory)
        {
            this.articlesRepoitory = articlesRepoitory;
        }
        public List<Articles> GetAllArticles()
        {
            return articlesRepoitory.GetAllArticles().ToList();
        }
        public Articles GetArticleByID(int id)
        {
            var article = articlesRepoitory.GetArticleByID(id);
            return article;
        }
        public Articles UpdateArticle(int id, Articles article)
        {
            var updatedArticle = articlesRepoitory.UpdateArticle(id, article);
            return updatedArticle;
        }
        public Articles AddArticle(Articles article)
        {
            var newArticle = articlesRepoitory.AddArticle(article);
            return newArticle;
        }
        public bool RemoveArticle(int id)
        {
            var result = articlesRepoitory.RemoveArticle(id);
            return result;
        }
    }
}
