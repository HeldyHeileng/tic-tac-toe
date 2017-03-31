using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TicTacToeWebClient.Models;

interface IRepository : IDisposable
{
    List<GameInfo> GetGameInfo();
    void SaveGameInfo(GameInfo gameInfo);

}

public class GameInfoRepository : IRepository
{
    private GameInfoDBContext db;
    public GameInfoRepository()
    {
        this.db = new GameInfoDBContext();
    }
    public virtual List<GameInfo> GetGameInfo()
    {
        return db.GameInfo.ToList();
    }

    public virtual void SaveGameInfo(GameInfo gameInfo)
    {
        db.GameInfo.Add(gameInfo);
        db.SaveChanges();
    }


    private bool disposed = false;

    public virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}