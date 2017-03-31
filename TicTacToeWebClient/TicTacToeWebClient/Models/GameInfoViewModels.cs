using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicTacToeWebClient.Models
{

    public class GameInfoViewModels
    {
        public List<GameInfo> GameInfoList { get; set; }

        public GameInfoViewModels(GameInfoRepository repo)
        {
            GameInfoList = repo.GetGameInfo(); //Берем информацию 
        }
    }

    public class GameInfo
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }

        public string FirstMove { get; set; }

        public int MoveCount { get; set; }

        public string Winner { get; set; }

        public DateTime CreateDate { get; set; }
    }


}