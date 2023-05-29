namespace Application.Common.Interfaces.Persistence;
public interface IUserRepository
{
    Task<bool> ValidateCredientals(string username, string password);
    Task<string> Register(string email, string username, string password);
    Task<bool> ValidateUsername(string username);
    Task<bool> ValidateEmail(string email);
}
