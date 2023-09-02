using BlogServices.Data;
using BlogServices.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogServices.Repositories
{
    public class ArticlesRepository : IArticlesRepository
    {
        private readonly BlogServicesContext context;
        public ArticlesRepository(BlogServicesContext context)
        {
            this.context = context;
        }
        public List<Articles> GetAllArticles()
        {
            var articles = new List<Articles>();
            articles = context.Articles.ToList();

            // to get only published articles
            // articles = context.Articles.Where(s => s.Status == Status.Published).ToList();

            return articles;
        }
        public Articles GetArticleByID(int id)
        {
            Articles article;
            article = context.Articles.Where(s => s.Id == id).FirstOrDefault<Articles>();
            return article;
        }

        public Articles UpdateArticle(int id, Articles article)
        {
            Articles existingArticle = GetArticleByID(id);
            context.Entry(existingArticle).CurrentValues.SetValues(article);
            context.SaveChanges();
            return existingArticle;
        }
        public Articles AddArticle(Articles article)
        {
            context.Articles.Add(article);
            context.SaveChanges();
            return context.Articles.Where(s => s.Id == article.Id).FirstOrDefault<Articles>();
        }

        public bool RemoveArticle(int id)
        {
            Articles article = GetArticleByID(id);

            if(article != null)
            {
                context.Remove(article);
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
