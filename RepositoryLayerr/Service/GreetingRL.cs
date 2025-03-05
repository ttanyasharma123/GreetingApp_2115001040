using System;
using System.Collections.Generic;
using RepositoryLayerr.Interface;

namespace RepositoryLayerr.Service
{
    public class GreetingRL : IGreetingRL
    {
        private static Dictionary<int, string> greetings = new Dictionary<int, string>(); // Store greetings with an ID

        public GreetingRL()
        {
        }

        public string GetGreetingMessage(string? firstName, string? lastName)
        {
            string message;

            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                message = $"Hello {firstName} {lastName}!";
            }
            else if (!string.IsNullOrEmpty(firstName))
            {
                message = $"Hello {firstName}!";
            }
            else if (!string.IsNullOrEmpty(lastName))
            {
                message = $"Hello {lastName}!";
            }
            else
            {
                message = "Hello World!";
            }

            SaveGreeting(greetings.Count + 1, message); // Assigning a unique ID
            return message;
        }

        public void SaveGreeting(int id, string message)
        {
            greetings[id] = message; // Store the greeting with its ID
        }

        public string? FindGreetingById(int id)
        {
            return greetings.ContainsKey(id) ? greetings[id] : null;
        }

        public List<string> GetAllGreetings()
        {
            return new List<string>(greetings.Values); // Return all stored greetings
        }
    }
}
