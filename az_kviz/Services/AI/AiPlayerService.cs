using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace az_kviz.Services.AI
{
    public class AiPlayerService
    {
        private Random _random = new Random();

        public string GetMove(List<string> availableLetters)
        {
            if (availableLetters == null || availableLetters.Count == 0) return "A";
            return availableLetters[_random.Next(availableLetters.Count)];
        }

        public bool WillAnswerCorrectly()
        {
            // Simple AI: 70% chance to be right
            return _random.Next(1, 101) <= 70;
        }
    }
}