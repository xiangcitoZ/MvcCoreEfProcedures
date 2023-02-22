using Microsoft.EntityFrameworkCore;
using MvcCoreEfProcedures.Models;

namespace MvcCoreEfProcedures.Data
{
    public class DoctoresContext : DbContext
    {
        public DoctoresContext(DbContextOptions<DoctoresContext> options)
       : base(options) { }
        public DbSet<Doctor> Doctores { get; set; }

    }

}
