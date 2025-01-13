using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ElementarySchool.Models;

namespace ElementarySchool.Data
{
    public class ElementarySchoolContext : DbContext
    {
        public ElementarySchoolContext (DbContextOptions<ElementarySchoolContext> options)
            : base(options)
        {
        }

        public DbSet<ElementarySchool.Models.Teacher> Teacher { get; set; } = default!;
        public DbSet<ElementarySchool.Models.Student> Student { get; set; } = default!;
    }
}
