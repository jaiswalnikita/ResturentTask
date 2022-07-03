using Microsoft.EntityFrameworkCore;

namespace ResturentTask.Models
{
    public class Classcontext:DbContext
    {
        public Classcontext(DbContextOptions<Classcontext> options) : base(options)
        {

        }
        public DbSet<RestaurantModel> tblRestaurant { get; set; }

        public DbSet<PlayerModel> tblPlayer { get; set; }

        public DbSet<ReslinkplayerModel> tblReslinkPlayer { get; set; }

        //public DbSet<Mapping> tblMapping { get; set; }

    }
}
