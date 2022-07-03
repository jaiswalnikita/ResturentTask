namespace ResturentTask.Models
{
    public class Playerfvtrestruent
    {
        public class PlayersFavRestro

        {

            public PlayersFavRestro()
            {
                player = new PlayerModel();
                restaurent = new RestaurantModel();
                Fav = new ReslinkplayerModel();
            }
            public PlayerModel player { get; set; }
            public RestaurantModel restaurent { get; set; }

            public ReslinkplayerModel Fav { get; set; }


        }
        public class PlayersFavRestroList

        {
            public List<PlayerModel> player { get; set; }
            public PlayerModel pls { get; set; }

            public List<RestaurantModel> restaurent { get; set; }

            public RestaurantModel rest { get; set; }

        }
    }
}
