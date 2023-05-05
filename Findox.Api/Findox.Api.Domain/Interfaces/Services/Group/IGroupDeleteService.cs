namespace Findox.Api.Domain.Interfaces.Services.Group
{
    public interface IGroupDeleteService
    {
        Task<(bool deleted, string message)> RunAsync(int id, int requestedBy);
    }
}
