using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicTacToeWebClient.Models;

namespace TicTacToeWebClient.Controllers
{
    public class HomeController : Controller
    {
        GameInfoRepository repo;

        public HomeController(GameInfoRepository r)
        {
            repo = r;
        }
        public HomeController()
        {
            repo = new GameInfoRepository();
        }

        // GET: Home
        public ActionResult Index()
        {
            return View(new GameInfoViewModels(repo));
        }

        [HttpPost]
        public bool SaveGameInfo(GameInfo gameInfo)
        {
            try
            {
                repo.SaveGameInfo(gameInfo);
            }
            catch
            {
                return false;
            }
            return true;
        }

        [HttpGet]
        public string GetGameInfo()
        {
            return JsonConvert.SerializeObject(repo.GetGameInfo());
        }
    }
}