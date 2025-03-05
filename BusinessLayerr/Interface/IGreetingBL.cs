using System.Collections.Generic;

namespace BusinessLayerr.Interface
{
    public interface IGreetingBL
    {
        string GetGreetingMessage(string? firstName, string? lastName);
        void SaveGreeting(int id, string message);  
        string? FindGreetingById(int id);  
        List<string> GetAllGreetings();
    }
}
