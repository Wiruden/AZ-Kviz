// Jméno a příjmení: David Mihók
// Třída: 4.C
// Předmět: Programování a vývoj aplikací
// Program: AZ Kvíz
using System;
using System.Collections.Generic;
using System.Linq;

namespace az_kviz.Services.Game
{
    public class WinChecker
    {
        // Define the target "Sides" of the triangle to connect
        // Left Side: A down to S
        private readonly string[] _leftSide = { "A", "B", "Č", "F", "I", "N", "S" };
        // Right Side: A down to Ž
        private readonly string[] _rightSide = { "A", "C", "E", "CH", "M", "Ř", "Ž" };
        // Bottom Side: S across to Ž
        private readonly string[] _bottomSide = { "S", "Š", "T", "U", "V", "Z", "Ž" };

        /// <summary>
        /// Checks if the player has connected all three sides of the triangle.
        /// </summary>
        /// <param>List of letters currently held by the player (Tag "1" or "2")</param>
        /// <returns>True if a winning path exists</returns>
        public bool CheckWin(IEnumerable<string> ownedLetters)
        {
            var owned = ownedLetters.ToHashSet();

            // AZ-Kvíz Victory Condition: A path must exist that touches all three sides.
            // We check if a path exists between each pair of sides using the owned tiles.
            bool connectsLeftToRight = DoesPathExist(owned, _leftSide, _rightSide);
            bool connectsLeftToBottom = DoesPathExist(owned, _leftSide, _bottomSide);
            bool connectsRightToBottom = DoesPathExist(owned, _rightSide, _bottomSide);

            return connectsLeftToRight && connectsLeftToBottom && connectsRightToBottom;
        }

        private bool DoesPathExist(HashSet<string> owned, string[] startGroup, string[] endGroup)
        {
            var startNodes = startGroup.Where(owned.Contains).ToList();
            if (!startNodes.Any()) return false;

            var queue = new Queue<string>(startNodes);
            var visited = new HashSet<string>(startNodes);

            while (queue.Any())
            {
                var current = queue.Dequeue();

                if (endGroup.Contains(current))
                    return true;

                foreach (var neighbor in GetNeighbors(current))
                {
                    if (owned.Contains(neighbor) && !visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// The Hexagonal Grid Adjacency Map for the 28-tile AZ-Kvíz board.
        /// </summary>
        private string[] GetNeighbors(string letter)
        {
            var adjacency = new Dictionary<string, string[]>
            {
                { "A", new[] { "B", "C" } },
                { "B", new[] { "A", "C", "Č", "D" } },
                { "C", new[] { "A", "B", "D", "E" } },
                { "Č", new[] { "B", "D", "F", "G" } },
                { "D", new[] { "B", "C", "Č", "E", "G", "H" } },
                { "E", new[] { "C", "D", "H", "CH" } },
                { "F", new[] { "Č", "G", "I", "J" } },
                { "G", new[] { "Č", "D", "F", "H", "J", "K" } },
                { "H", new[] { "D", "E", "G", "CH", "K", "L" } },
                { "CH", new[] { "E", "H", "L", "M" } },
                { "I", new[] { "F", "J", "N", "O" } },
                { "J", new[] { "F", "G", "I", "K", "O", "P" } },
                { "K", new[] { "G", "H", "J", "L", "P", "Q" } },
                { "L", new[] { "H", "CH", "K", "M", "Q", "R" } },
                { "M", new[] { "CH", "L", "R", "Ř" } },
                { "N", new[] { "I", "O", "S", "Š" } },
                { "O", new[] { "I", "J", "N", "P", "Š", "T" } },
                { "P", new[] { "J", "K", "O", "Q", "T", "U" } },
                { "Q", new[] { "K", "L", "P", "R", "U", "V" } },
                { "R", new[] { "L", "M", "Q", "Ř", "V", "Z" } },
                { "Ř", new[] { "M", "R", "Z", "Ž" } },
                { "S", new[] { "N", "Š" } },
                { "Š", new[] { "N", "O", "S", "T" } },
                { "T", new[] { "O", "P", "Š", "U" } },
                { "U", new[] { "P", "Q", "T", "V" } },
                { "V", new[] { "Q", "R", "U", "Z" } },
                { "Z", new[] { "R", "Ř", "V", "Ž" } },
                { "Ž", new[] { "Ř", "Z" } }
            };

            return adjacency.TryGetValue(letter, out var neighbors) ? neighbors : Array.Empty<string>();
        }
    }
}