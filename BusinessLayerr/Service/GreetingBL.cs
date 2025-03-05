using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public string GetGreetingMessage()
        {
            return _greetingRL.GetGreetingMessage();
        }
    }
}
