// Jméno a příjmení: David Mihók
// Třída: 4.C
// Předmět: Programování a vývoj aplikací
// Program: AZ Kvíz
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace az_kviz.Services.Storage
{
    public static class JsonStorageService
    {
        // Cesta směřuje do složky Data vedle .exe souboru
        private static readonly string HistoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "history.json");

        /// <summary>
        /// Uloží výsledek hry do JSON souboru v adresáři Data.
        /// </summary>
        public static void SaveGameResult(string player1, string player2, string winner, bool isVsAi)
        {
            try
            {
                // Zajistíme, že složka Data existuje
                string directory = Path.GetDirectoryName(HistoryPath);
                if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

                var history = LoadHistory();

                // Používáme Dictionary pro kompatibilitu s deserializací
                var newEntry = new Dictionary<string, string>
                {
                    { "Datum", DateTime.Now.ToString("yyyy-MM-dd HH:mm") },
                    { "Hrac1", player1 },
                    { "Hrac2", player2 },
                    { "Vitez", winner },
                    { "Rezim", isVsAi ? "Vs AI" : "PvP" }
                };

                history.Add(newEntry);

                var options = new JsonSerializerOptions { WriteIndented = true };
                File.WriteAllText(HistoryPath, JsonSerializer.Serialize(history, options));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při ukládání: {ex.Message}");
            }
        }

        /// <summary>
        /// Načte historii her ze souboru.
        /// </summary>
        public static List<Dictionary<string, string>> LoadHistory()
        {
            try
            {
                if (!File.Exists(HistoryPath)) return new List<Dictionary<string, string>>();

                string json = File.ReadAllText(HistoryPath);
                // Klíčové: Specifikujeme přesný typ pro List
                return JsonSerializer.Deserialize<List<Dictionary<string, string>>>(json)
                       ?? new List<Dictionary<string, string>>();
            }
            catch
            {
                return new List<Dictionary<string, string>>();
            }
        }
    }
}