using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayerr.Interface
{
    public interface IGreetingBL
    {
        string GetGreetingMessage(string? firstName, string? lastName);
    }
}
