using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToeWebClient.Models
{
    public class GameInfoViewModels
    {
        public List<GameInfo> GameInfoList { get; set; }

        public GameInfoViewModels()
        {
            string info = System.IO.File.ReadAllText(@"c:\gameInfo.json");
            GameInfoList = JsonConvert.DeserializeObject<List<GameInfo>>(info); //Берем информацию 
        }
    }

    public class GameInfo
    {
        public string UserName { get; set; }

        public string FirstMove { get; set; }

        public int MoveCount { get; set; }

        public string Winner { get; set; }

        public DateTime CreateDate { get; set; }
    }


}