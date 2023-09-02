using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogServices.Data;
using BlogServices.Models;
using BlogServices.Services;

namespace BlogServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticlesService articlesService;

        public ArticlesController(IArticlesService articlesService)
        {
            if (articlesService == null)
            {
                throw new ArgumentNullException("articlesService");
            }
            this.articlesService = articlesService;
        }

        // GET: api/Articles
        [HttpGet]
        public ActionResult<IList<Articles>> GetArticles()
        {
            var articles = articlesService.GetAllArticles().ToList();

            if (articles == null)
            {
                return NotFound();
            }

            return articles;
        }

        // GET: api/Articles/5
        [HttpGet("{id}")]
        public ActionResult<Articles> GetArticles(int id)
        {
            var articles = articlesService.GetArticleByID(id);

            if (articles == null)
            {
                return NotFound();
            }

            return articles;
        }

        // PUT: api/Articles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Articles>> PutArticles(int id, Articles articles)
        {
            if (id != articles.Id)
            {
                return BadRequest();
            }

            var article = articlesService.UpdateArticle(id, articles);
            if (article == null)
            {
                return NotFound();
            }
            return article;
        }

        // POST: api/Articles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Articles>> PostArticles(Articles articles)
        {
          if (articles == null)
          {
                return BadRequest();
          }

            var article = articlesService.AddArticle(articles);
            return article;
        }

        // DELETE: api/Articles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteArticles(int id)
        {
            var article = articlesService.RemoveArticle(id);
            return article;
        }
    }
}
