using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartHall.DAL.Sql.Entities;

namespace SmartHall.DAL.Sql
{
    public sealed class SmartHallDbContext : IdentityDbContext<IdentityUser>
    {
        public SmartHallDbContext(DbContextOptions<SmartHallDbContext> options) : base(options)
        {
        }

        public DbSet<HallEntity> Halls { get; set; }
    }
}
