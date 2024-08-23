using Demo1.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo1.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserDept> UserDepartments { get; set; }
        public DbSet<InternalUser> AddInternal { get; set; }
        public DbSet<AddSubjectModel> StdSubject { get; set; }
        public DbSet<Addsbj> AddSubject { get; set; }
        public DbSet<Addttr> AddEnrollment { get; set; }
        public DbSet<SbjTable> TimeTable { get; set; }
        public DbSet<PaymentProcess> PaymentProcesses { get; set; }
        public DbSet<Pay> Pay { get; set; }

    }

}
