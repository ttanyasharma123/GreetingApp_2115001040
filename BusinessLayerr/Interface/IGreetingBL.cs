using System.Collections.Generic;

namespace BusinessLayerr.Interface
{
    public interface IGreetingBL
    {
        string GetGreetingMessage(string? firstName, string? lastName);
        void SaveGreeting(string message);  
        List<string> GetAllGreetings();    
    }
}
