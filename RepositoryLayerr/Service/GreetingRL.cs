using System;
using System.Collections.Generic;
using RepositoryLayerr.Interface;

namespace RepositoryLayerr.Service
{
    public class GreetingRL : IGreetingRL
    {
        private static Dictionary<int, string> greetings = new Dictionary<int, string>();

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

            int newId = greetings.Count + 1;
            SaveGreeting(newId, message);
            return message;
        }

        public void SaveGreeting(int id, string message)
        {
            if (!greetings.ContainsKey(id))
            {
                greetings[id] = message;
            }
        }

        public string? FindGreetingById(int id)
        {
            return greetings.ContainsKey(id) ? greetings[id] : null;
        }

        public List<string> GetAllGreetings()
        {
            if (greetings.Count == 0)
            {
                return new List<string> { "No greetings found." };
            }
            return new List<string>(greetings.Values);
        }

        /// <summary>
        /// Update an existing greeting message
        /// </summary>
        public void UpdateGreeting(int id, string message)
        {
            if (greetings.ContainsKey(id))
            {
                greetings[id] = message;
            }
        }

        /// <summary>
        /// Delete a greeting message by ID
        /// </summary>
        public bool DeleteGreetingById(int id)
        {
            if (greetings.ContainsKey(id))
            {
                greetings.Remove(id);
                return true;
            }
            return false;
        }
    }
}
