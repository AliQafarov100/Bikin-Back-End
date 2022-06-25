using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bikin_Back_End.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bikin_Back_End.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Setting> Settings { get; set; }
        public DbSet<ServiceCard> ServiceCards { get; set; }
        public DbSet<AboutCards> AboutCards { get; set; }
        public DbSet<MainPage> MainPages { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
    }
}
