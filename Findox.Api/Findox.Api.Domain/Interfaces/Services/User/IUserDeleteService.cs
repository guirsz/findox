namespace Findox.Api.Domain.Interfaces.Services.User
{
    public interface IUserDeleteService
    {
        Task<(bool deleted, string message)> RunAsync(int id, int requestedBy);
    }
}
