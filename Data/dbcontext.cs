using Khanakhajana.Models;
using Microsoft.EntityFrameworkCore;

namespace Khanakhajana.Data
{
    public class dbcontext : DbContext
    {
        public dbcontext()
        {
        }

        public dbcontext(DbContextOptions<dbcontext> s) : base(s)
        {

        }

        public DbSet<RegisterModel> Users { get; set; }
        public
            DbSet<ProfileModel> Profile
        { get; set; }


    }
}
