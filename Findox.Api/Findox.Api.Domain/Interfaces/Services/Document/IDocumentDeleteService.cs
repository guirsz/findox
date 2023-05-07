namespace Findox.Api.Domain.Interfaces.Services.Document
{
    public interface IDocumentDeleteService
    {
        Task<(bool deleted, string message)> RunAsync(Guid id, int requestedBy);
    }
}
