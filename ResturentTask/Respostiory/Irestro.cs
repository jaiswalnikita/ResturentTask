using ResturentTask.Models;
using static ResturentTask.Models.Playerfvtrestruent;

namespace ResturentTask.Respostiory
{
    public interface Irestro
    {
        List<RestaurantModel>Listrestro ();
        bool restrocreate(RestaurantModel emp);

        List<PlayerModel> ListPlayer();
        bool Playercreate(PlayerModel pl);

        List<PlayersFavRestro> Map();
        List<RestaurantModel> retervieResturantByName(string name);
        List<PlayerModel> retrivePlayerByName(string name);
        PlayersFavRestroList FvtplyRest(string name, bool status = true);
        PlayersFavRestroList GetbyAge(string Name, int age);

        List<string> fvtplyresatuarnt(string name);
        List<PlayerModel> playerdatabycity(string city);
    }
    public abstract class RestroAbs : Irestro
    {
        public abstract List<RestaurantModel> Listrestro();
        public abstract bool restrocreate(RestaurantModel emp);

        public abstract List<PlayerModel> ListPlayer();
        public abstract bool Playercreate(PlayerModel pl);
        public abstract List<PlayerModel> playerdatabycity(string city);

        public abstract List<PlayersFavRestro> Map();

        public abstract List<RestaurantModel> retervieResturantByName(string name);

        public abstract List<PlayerModel> retrivePlayerByName(string name);

        public abstract PlayersFavRestroList FvtplyRest(string name, bool status = true);

        public abstract PlayersFavRestroList GetbyAge(string Name, int age);
        public abstract List<string> fvtplyresatuarnt(string name);


    }
    public class ResRepository : RestroAbs
    {
        private readonly Classcontext dbcontext = null;
        private readonly object item;

        public ResRepository(Classcontext _dbContxet)
        {
            dbcontext = _dbContxet;
        }
        //api 1
        public override List<RestaurantModel> Listrestro()
        {
            return dbcontext.tblRestaurant.ToList();
        }
        public override bool restrocreate(RestaurantModel emp)
        {
            if (emp == null)
            {
                return false;
            }
            else
            {
                if (emp.RestaurantId == 0)
                {
                    dbcontext.Add(emp);
                    dbcontext.SaveChanges();
                    return true;
                }
                else
                {
                    dbcontext.Entry(emp).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbcontext.SaveChanges();
                    return true;
                }
            }
        }

        //api 2
        public override List<PlayerModel> ListPlayer()
        {
            return dbcontext.tblPlayer.ToList();
        }
        public override bool Playercreate(PlayerModel pl)
        {
            if (pl == null)
            {
                return false;
            }
            else
            {
                if (pl.PlayerId == 0)
                {
                    dbcontext.Add(pl);
                    dbcontext.SaveChanges();
                    return true;
                }
                else
                {
                    dbcontext.Entry(pl).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbcontext.SaveChanges();
                    return true;
                }
            }
        }

        // 3 api mapping
        public override List<PlayersFavRestro> Map()
        {
            List<PlayersFavRestro> allplayer = new List<PlayersFavRestro>();
            var res = (from player in dbcontext.tblPlayer
                       from Fav in dbcontext.tblReslinkPlayer
                       from restaurent in dbcontext.tblRestaurant
                       where player.PlayerId == Fav.PlayerId
                       && restaurent.RestaurantId == Fav.RestaurantId
                       where player.PlayerId > 0
                       select new
                       {
                           player = player,
                           restaurent = restaurent,

                       }).ToList();

            foreach (var item in res)
            {
                PlayersFavRestro obj = new PlayersFavRestro();

                obj.player = item.player;
                obj.restaurent = item.restaurent;
                allplayer.Add(obj);
            }
            return allplayer;
        }

        // retrive a
        public override List<RestaurantModel> retervieResturantByName(string name)
        {
            var obj = dbcontext.tblRestaurant.Where(Models => Models.Name == name).ToList();
            return obj;
        }

        //retrive b
        public override List<PlayerModel> retrivePlayerByName(string name)
        {
            var obj1 = dbcontext.tblPlayer.Where(Models => Models.Name == name).ToList();
            return obj1;
        }
        //retreive c and d
        public override PlayersFavRestroList FvtplyRest(string name, bool status = true)
        {
            PlayersFavRestroList lt = new PlayersFavRestroList();

            var playerDetail = dbcontext.tblPlayer.Where(x => x.Name == name).ToList();
            if (playerDetail != null)
            {
                var playerID = playerDetail.FirstOrDefault().PlayerId;

                var resDetail = (from map in dbcontext.tblReslinkPlayer
                                 join res in dbcontext.tblRestaurant
                                 on map.RestaurantId equals res.RestaurantId
                                 where map.PlayerId == playerID
                                 select new RestaurantModel
                                 {
                                     RestaurantId = map.RestaurantId,
                                     Name = res.Name
                                 }).ToList();

                lt.player = playerDetail;
                lt.restaurent = resDetail;
            }
            return lt;

        }

        public override PlayersFavRestroList GetbyAge(string Name, int age)
        {
            PlayersFavRestroList lst = new PlayersFavRestroList();
            var RestaurantCollection = dbcontext.tblRestaurant.FirstOrDefault(x => x.Name == Name);
            List<PlayerModel> pares = new List<PlayerModel>();
            List<PlayerModel> set = new List<PlayerModel>();

            if (RestaurantCollection != null)
            {
                var restaurentId = RestaurantCollection.RestaurantId;

                var LinkDetail = dbcontext.tblReslinkPlayer.Where(x => x.RestaurantId == restaurentId).ToList();
                foreach (var item in LinkDetail)
                {

                    var abc = dbcontext.tblPlayer.Where(x => x.PlayerId == item.PlayerId).FirstOrDefault();
                    set.Add(abc);

                }
                if (set.Count > 0)
                {
                    foreach (var item in set)
                    {

                        var years = DateTime.Now.Year - Convert.ToDateTime(item.dob).Year;

                        if (years >= age)
                        {
                            pares.Add(item);
                        }

                        years = 0;
                    }
                }
                List<PlayersFavRestro> res = new List<PlayersFavRestro>();
                var areas = (from player in dbcontext.tblPlayer
                             from restaurent in dbcontext.tblRestaurant
                             from Fav in dbcontext.tblReslinkPlayer
                             where player.PlayerId == Fav.PlayerId
                             && restaurent.RestaurantId == Fav.RestaurantId
                             select new
                             {
                                 player = player,
                                 restaurent = restaurent,
                                 Fav = Fav
                             }).ToList();
                foreach (var item in areas)
                {
                    PlayersFavRestro obj1 = new PlayersFavRestro();

                    obj1.player = item.player;
                    obj1.restaurent = item.restaurent;
                    obj1.Fav = item.Fav;
                    res.Add(obj1);
                }


                lst.rest = RestaurantCollection;
                lst.player = pares;




            }

            return lst;
        }

        public override List<string> fvtplyresatuarnt(string name)
        {
            var player = dbcontext.tblPlayer.Where(x => x.Name == name).FirstOrDefault();
            var playerid = player.PlayerId;
            var res = (from a in dbcontext.tblReslinkPlayer
                       where a.PlayerId == playerid
                       select new RestaurantModel
                       {
                           RestaurantId = a.RestaurantId,
                       }
                       ).ToList();
            List<string> Listrest = new List<string>();
            foreach (RestaurantModel item in res)
            {
                var adder = dbcontext.tblRestaurant.Where(x => x.RestaurantId == item.RestaurantId).FirstOrDefault().Name;
                Listrest.Add(adder);
            }
            return Listrest;
        }
        //public bool tosavefvtplayersrestro(PlayersFavRestroList playersFavRestroList)
        //{
        //    var playerId = dbcontext.tblPlayer.Where(x => x.Name == playersFavRestroList.pls.Name).FirstOrDefault().PlayerId;
        //    List<int> restroIdList = new List<int>();

        //    foreach (var item in playersFavRestroList.restaurent)
        //    {
        //        var a = dbcontext.tblRestaurant.Where(y => y.Name == item.Name).FirstOrDefault().RestaurantId;
        //        if (a != 0)
        //        {
        //            restroIdList.Add(a);

        //        }
        //    }
        //    foreach (var item in restroIdList)
        //    {
        //        ReslinkplayerModel a = new ReslinkplayerModel()
        //        {
        //            PlayerId = playerId,
        //            RestaurantId = item
        //        };
        //        //a.PlayerId = playerId;
        //        //a.RestaurantId = item;
        //        if (a != null)
        //        {
        //            dbcontext.tblMapping.Add(a);
        //        }
        //    }
        //    dbcontext.SaveChanges();
        //    return true;
        //}
        public override List<PlayerModel> playerdatabycity(string city)
        {
            var obj = dbcontext.tblPlayer.Where(Models => Models.pCity == city).ToList();
            return obj;
        }
    }
}
