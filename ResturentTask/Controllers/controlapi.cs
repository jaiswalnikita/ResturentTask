using Microsoft.AspNetCore.Mvc;
using ResturentTask.Models;
using ResturentTask.Respostiory;
using static ResturentTask.Models.Playerfvtrestruent;

namespace ResturentTask.Controllers
{
    [ApiController]
    [Route("Api/[Controller]/[action]")]
    public class controlapi : Controller
    {
        private readonly ResRepository repo = null;
        public controlapi(ResRepository _repo)
        {
            repo = _repo;
        }
        [HttpGet]
        public IActionResult Listrestro()
        {
            var a = repo.Listrestro();
            return Ok(a);
        }
        [HttpPost]
        public IActionResult restrocreate(RestaurantModel emp)
        {
            var a = repo.restrocreate(emp);
            return Ok(a);
        }
        [HttpGet]
        public IActionResult ListPlayer()
        {
            var a = repo.ListPlayer();
            return Ok(a);
        }

        [HttpPost]
        public IActionResult Playercreate(PlayerModel pl)
        {
            var a = repo.Playercreate(pl);
            return Ok(a);
        }
        [HttpGet]
        public IActionResult Map()
        {
            return Ok(repo.Map());
        }

        [HttpGet("{name}")]
        public List<RestaurantModel> retervieResturantByName(string name)
        {
            return (repo.retervieResturantByName(name));
        }

        [HttpGet("{name}")]
        public List<PlayerModel> retrivePlayerByName(string name)
        {
            return (repo.retrivePlayerByName(name));
        }
        [HttpGet("{name}")]
        public PlayersFavRestroList favplyRes(string name)
        {
            var statuss = true;
            return (repo.FvtplyRest(name, statuss));
        }

        //[HttpGet("{name}")]
        //public List<string> FvtplyResatuarnt(string name)
        //{
        //    return (repo.fvtplyresatuarnt(name));
        //}
        [HttpGet]
        public IActionResult GetbyAge(string Name, int Age)
        {
            var result = repo.GetbyAge(Name, Age);
            return Ok(result);
        }
        [HttpGet]
        public List<PlayerModel> playerdatabycity(string city)
        {
            var res = repo.playerdatabycity(city);
            return res;
        }


    }

}