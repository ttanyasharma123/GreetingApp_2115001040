using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayerr.Interface;

namespace BusinessLayerr.Service
{
    
        public class GreetingBL : IGreetingBL
        {
            public string GetGreeting()
            {
                return "Hello, Tanya!";
            }
        }
    }

