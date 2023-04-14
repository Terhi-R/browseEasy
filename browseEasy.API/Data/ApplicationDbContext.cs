using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using browseEasy.API.Models;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<browseEasy.API.Models.User> User { get; set; }

        public DbSet<browseEasy.API.Models.Group> Group { get; set; }
    }
