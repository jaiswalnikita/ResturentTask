using System.ComponentModel.DataAnnotations;

namespace ResturentTask.Models
{
    public class Mapping
    {
     
            [Key]
            public int Id { get; set; }

            public virtual RestaurantModel RestaurantId { get; set; }


            public virtual PlayerModel PlayerId { get; set; }


    }
}
