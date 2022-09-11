using Microsoft.EntityFrameworkCore;

namespace Demo.Data
{

    public class UserDbContext : DbContext
    {
        #region Contructor
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {

        }
        #endregion

        #region Public properties
        public DbSet<User> Users { get; set; }
        #endregion

        #region Overidden methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(GetInitialUsers());
            base.OnModelCreating(modelBuilder);
        }
        #endregion


        #region Private methods
        private List<User> GetInitialUsers()
        {
            return new List<User>
            {
                new User { Id = 1001, Firstname = "Liyo", Lastname = "Jose",  Email = "liyojose@gmail.com"},
                new User { Id = 1002, Firstname = "Roger", Lastname = "Wan",  Email = "roger@gmail.com"},
                new User { Id = 1003, Firstname = "Pedro", Lastname = "hugo",  Email = "pedro@gmail.com"},
            };
        }
        #endregion
    }
}
