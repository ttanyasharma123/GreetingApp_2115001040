using System;
using System.Collections.Generic;
using RepositoryLayerr.Interface;

namespace RepositoryLayerr.Service
{
    public class GreetingRL : IGreetingRL
    {
        private static List<string> greetings = new List<string>(); 

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

            SaveGreeting(message); // Save greeting message when generated
            return message;
        }

        public void SaveGreeting(string message)
        {
            greetings.Add(message); // Store the greeting message
        }

        public List<string> GetAllGreetings()
        {
            return greetings; // Return all stored greetings
        }
    }
}
