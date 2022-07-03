using System.ComponentModel.DataAnnotations;

namespace ResturentTask.Models
{
    public class ReslinkplayerModel
    {
        [Key]
        public int Id { get; set; }

        public int RestaurantId { get; set; }

        public int PlayerId { get; set; }

        public bool Fav { get; set; }

    }
}
