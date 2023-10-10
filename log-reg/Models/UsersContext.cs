using Microsoft.EntityFrameworkCore;


namespace log_reg.Models
{
    public class UsersContext : DbContext
    {
        public DbSet<Users> UsersObjects { get; set; }
        public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        {

        }
    }
}
