namespace Application.Common.Interfaces.Persistence;
public interface IUserRepository
{
    Task<bool> ValidateCredientals(string username, string password);
    Task<bool> Register(string email, string username, string password);
}
