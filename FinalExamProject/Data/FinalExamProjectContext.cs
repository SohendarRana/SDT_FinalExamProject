using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FinalExamProject.Models;

namespace FinalExamProject.Data
{
    public class FinalExamProjectContext : DbContext
    {
        public FinalExamProjectContext (DbContextOptions<FinalExamProjectContext> options)
            : base(options)
        {
        }

        public DbSet<FinalExamProject.Models.Reviews> Reviews { get; set; } = default!;
    }
}
