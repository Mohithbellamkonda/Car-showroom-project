using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Testing_Login.Areas.Identity.Data;

namespace Testing_Login.Data;

public class Testing_LoginContext : IdentityDbContext<Testing_LoginUser>
{
    public Testing_LoginContext(DbContextOptions<Testing_LoginContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
