// Jméno a příjmení: David Mihók
// Třída: 4.C
// Předmět: Programování a vývoj aplikací
// Program: AZ Kvíz
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace az_kviz.Models.Game
{
    public enum GameMode { LocalVsLocal, LocalVsAI }

    public class GameState
    {
        public bool IsGameOver { get; set; }
        public int CurrentPlayerId { get; set; } = 1;
        public GameMode Mode { get; set; }
        public string WinnerName { get; set; }
    }
}
