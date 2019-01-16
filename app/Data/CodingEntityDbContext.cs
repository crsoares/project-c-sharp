using Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Project.Data
{
    public class CodingEntityDbContext : DbContext
    {
        public CodingEntityDbContext(DbContextOptions<CodingEntityDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}