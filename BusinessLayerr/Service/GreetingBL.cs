using System;
using System.Collections.Generic;
using BusinessLayerr.Interface;
using RepositoryLayerr.Interface;

namespace BusinessLayerr.Service
{
    public class GreetingBL : IGreetingBL
    {
        private readonly IGreetingRL _greetingRL;

        public GreetingBL(IGreetingRL greetingRL)
        {
            _greetingRL = greetingRL;
        }

        public string GetGreetingMessage(string? firstName, string? lastName)
        {
            return _greetingRL.GetGreetingMessage(firstName, lastName);
        }

        public void SaveGreeting(int id, string message)
        {
            _greetingRL.SaveGreeting(id, message);
        }

        public string? FindGreetingById(int id)
        {
            return _greetingRL.FindGreetingById(id);
        }

        public List<string> GetAllGreetings()
        {
            return _greetingRL.GetAllGreetings();
        }

        /// <summary>
        /// Update an existing greeting message
        /// </summary>
        public void UpdateGreeting(int id, string message)
        {
            var existingGreeting = _greetingRL.FindGreetingById(id);
            if (existingGreeting == null)
            {
                throw new KeyNotFoundException("Greeting not found.");
            }

            _greetingRL.UpdateGreeting(id, message);
        }

        /// <summary>
        /// Delete a greeting message by ID
        /// </summary>
        public bool DeleteGreeting(int id)
        {
            var existingGreeting = _greetingRL.FindGreetingById(id);
            if (existingGreeting == null)
            {
                return false;
            }

            return _greetingRL.DeleteGreetingById(id);
        }
    }
}
