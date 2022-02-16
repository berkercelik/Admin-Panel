using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Models
{
    public class PanelContext : DbContext
    {
        public PanelContext(DbContextOptions<PanelContext> options) : base(options)
        {

        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Sort> Sorts { get; set; }
    }
}
