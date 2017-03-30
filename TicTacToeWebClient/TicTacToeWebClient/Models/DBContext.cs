using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TicTacToeWebClient.Models
{
    public class GameInfoDBContext : DbContext
    {

        public GameInfoDBContext() : base("DBConnectionString")
        {
            Database.SetInitializer<GameInfoDBContext>(new CreateDatabaseIfNotExists<GameInfoDBContext>());

            Database.SetInitializer<GameInfoDBContext>(new DropCreateDatabaseIfModelChanges<GameInfoDBContext>());
        }
        public DbSet<GameInfo> GameInfo { get; set; }
    }
}