using System.ComponentModel.DataAnnotations;

namespace ResturentTask.Models
{
    public class RestaurantModel
    {
        [Key]
        public int RestaurantId { get; set; }
        public string Name { get; set; }

        public string Address { get; set; }

        public string ContactNumber { get; set; }

        public int hoursofoperation { get; set; }
    }
}
