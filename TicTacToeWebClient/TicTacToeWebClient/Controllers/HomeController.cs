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
        // GET: Home
        public ActionResult Index()
        {
            return View(new GameInfoViewModels());
        }

        [HttpPost]
        public bool SaveGameInfo(GameInfo gameInfo) {
            try
            {
                var filePath = @"c:\gameInfo.json";

                // Читаем что есть в файле
                var jsonData = System.IO.File.ReadAllText(filePath);
                // Десериализуем информацию в лист 
                var gameInfoList = JsonConvert.DeserializeObject<List<GameInfo>>(jsonData)
                                      ?? new List<GameInfo>();

                // Добавляем в список нашу информацию
                gameInfoList.Add(gameInfo);

                // Переписываем файл
                jsonData = JsonConvert.SerializeObject(gameInfoList);
                System.IO.File.WriteAllText(filePath, jsonData);
            }
            catch {
                return false;
            }
            return true;
        }

        [HttpGet]
        public string GetGameInfo()
        {
            return JsonConvert.SerializeObject(new GameInfoViewModels().GameInfoList);
        }
    }
}