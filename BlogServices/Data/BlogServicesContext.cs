using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlogServices.Models;

namespace BlogServices.Data
{
    public class BlogServicesContext : DbContext
    {
        public BlogServicesContext (DbContextOptions<BlogServicesContext> options)
            : base(options)
        {
        }

        public DbSet<BlogServices.Models.Articles> Articles { get; set; } = default!;

        public DbSet<BlogServices.Models.Users>? Users { get; set; }
    }
}
