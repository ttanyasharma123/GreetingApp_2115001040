using System;
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
    }
}
