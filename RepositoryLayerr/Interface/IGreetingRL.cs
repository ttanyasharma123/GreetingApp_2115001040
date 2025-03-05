using System.Collections.Generic;

namespace RepositoryLayerr.Interface
{
    public interface IGreetingRL
    {
        string GetGreetingMessage(string? firstName, string? lastName);
        void SaveGreeting(int id, string message);
        string? FindGreetingById(int id);
        List<string> GetAllGreetings();
        void UpdateGreeting(int id, string message);
        bool DeleteGreetingById(int id); 
    }
}
