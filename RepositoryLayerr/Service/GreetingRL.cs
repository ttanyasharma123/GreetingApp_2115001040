using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayerr.Interface;

namespace RepositoryLayerr.Service
{
    public class GreetingRL : IGreetingRL
    {
        public GreetingRL()
        {

        }
        public string GetGreetingMessage()
        {
            return "Hello World";
        }
    }
}
