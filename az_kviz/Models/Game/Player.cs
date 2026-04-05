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
    public enum PlayerType { Human, AI }

    public class Player
    {
        public string Name { get; set; }
        public PlayerType Type { get; set; }
        public string ColorHex { get; set; } // e.g., "#FFD62222"
        public int Id { get; set; } // 1 or 2
        public int Score { get; set; }
    }
}
