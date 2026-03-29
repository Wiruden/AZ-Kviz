using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace az_kviz.Services.Game
{
    public class TurnManager
    {
        public int CurrentPlayerId { get; private set; } = 1;
        private bool _isExtraTurn = false;

        public void SwitchTurn(bool wasCorrect)
        {
            if (wasCorrect)
            {
                // Standard switch after a successful claim
                CurrentPlayerId = (CurrentPlayerId == 1) ? 2 : 1;
                _isExtraTurn = false;
            }
            else
            {
                // If Player 1 fails, Player 2 gets a chance at the SAME tile.
                CurrentPlayerId = (CurrentPlayerId == 1) ? 2 : 1;
                _isExtraTurn = true;
            }
        }
    }
}
