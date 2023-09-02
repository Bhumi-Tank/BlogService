using BlogServices.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogServices.Repositories
{
    public interface IArticlesRepository
    {
        public List<Articles> GetAllArticles();
        public Articles GetArticleByID(int id);
        public Articles UpdateArticle(int id, Articles article);
        public Articles AddArticle(Articles article);
        public bool RemoveArticle(int id);
    }
}
