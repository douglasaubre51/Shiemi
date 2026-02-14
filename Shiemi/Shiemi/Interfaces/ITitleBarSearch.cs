namespace Shiemi.Interfaces;

public interface ITitleBarSearch
{
    Task<object?> GetSearchedProjects(string title);
}
