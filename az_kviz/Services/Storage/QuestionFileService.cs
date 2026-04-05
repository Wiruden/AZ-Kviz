// Jméno a příjmení: David Mihók
// Třída: 4.C
// Předmět: Programování a vývoj aplikací
// Program: AZ Kvíz
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using az_kviz.Models.Quiz;

namespace az_kviz.Services.Storage
{
    public class QuestionFileService
    {
        private List<Question> _questions = new List<Question>();

        public QuestionFileService()
        {
            LoadQuestions();
        }

        private void LoadQuestions()
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "questions.sample.json");

                if (File.Exists(path))
                {
                    // Use UTF8 encoding to handle Czech characters properly
                    string jsonString = File.ReadAllText(path, System.Text.Encoding.UTF8);

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    _questions = JsonSerializer.Deserialize<List<Question>>(jsonString, options) ?? new List<Question>();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"JSON Load Error: {ex.Message}");
                _questions = new List<Question>();
            }
        }

        public Question GetRandomQuestionByLetter(string letter)
        {
            if (_questions == null || !_questions.Any()) return null;

            var filtered = _questions
                .Where(q => q.Letter != null && q.Letter.Equals(letter, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (filtered.Count == 0) return null;

            Random rnd = new Random();
            return filtered[rnd.Next(filtered.Count)];
        }
    }
}