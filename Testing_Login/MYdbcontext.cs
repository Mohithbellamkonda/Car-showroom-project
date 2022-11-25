using Microsoft.EntityFrameworkCore;
using Testing_Login.Data;
using Testing_Login.Models;

namespace Testing_Login
{
    public class MYDbcontext : DbContext
    {
        public MYDbcontext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Testing_Login.Models.Buy>Buy { get; set; }
        public DbSet<Testing_Login.Models.Cart>Cart { get; set; }
        public DbSet<Testing_Login.Models.Sold> Sold { get; set; }
        public DbSet<Testing_Login.Models.Brought> Brought { get; set; }

    }
}
