using System;
using RepositoryLayerr.Interface;

namespace RepositoryLayerr.Service
{
    public class GreetingRL : IGreetingRL
    {
        public GreetingRL()
        {
        }

        public string GetGreetingMessage(string? firstName, string? lastName)
        {
            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                return $"Hello {firstName} {lastName}!";
            }
            else if (!string.IsNullOrEmpty(firstName))
            {
                return $"Hello {firstName}!";
            }
            else if (!string.IsNullOrEmpty(lastName))
            {
                return $"Hello {lastName}!";
            }
            else
            {
                return "Hello World!";
            }
        }
    }
}
