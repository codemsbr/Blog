using Blog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Contexts
{
    public class BlogDbContexts:IdentityDbContext
    {
        public BlogDbContexts(DbContextOptions db):base(db) { }

        public DbSet<Header> Headers { get; set; }
        public DbSet<AppUser> AppUser { get; set; }
    }
}
