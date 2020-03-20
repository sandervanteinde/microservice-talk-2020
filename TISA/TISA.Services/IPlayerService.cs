using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TISA.Models;

namespace TISA.Services
{
    public interface IPlayerService
    {
        Player Player { get; set; }
        bool IsPlayerDefined { get; }

        Task SetPlayerByName(string playerName);
    }
}
