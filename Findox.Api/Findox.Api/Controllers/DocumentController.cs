using Findox.Api.Domain.Helpers;
using Findox.Api.Domain.Interfaces.Services.Document;
using Findox.Api.Domain.Requests;
using Findox.Api.Domain.Responses;
using Findox.Api.Domain.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using System.Net;

namespace Findox.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : AuthenticatedUserControllerBase
    {
        private readonly IDocumentUploadService uploadService;
        private readonly UploadConfigurations uploadConfigurations;

        public DocumentController(IDocumentUploadService uploadService, UploadConfigurations uploadConfigurations)
        {
            this.uploadService = uploadService;
            this.uploadConfigurations = uploadConfigurations;
        }

        [Authorize("RegularUser")]
        [HttpGet]
        public async Task<IActionResult> GetAllPaginated([FromQuery] DocumentGetAllPaginatedRequest request, [FromServices] IDocumentGetAllService service)
        {
            IEnumerable<DocumentResponse> result = await service.RunAsync(request, RequestedBy(), UserRoleId());
            return Ok(result);
        }

        [Authorize("RegularUser")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, [FromServices] IDocumentGetService service)
        {
            DocumentResponse? result = await service.RunAsync(id, RequestedBy(), UserRoleId());

            if (result == null || result?.DocumentId == Guid.Empty)
                return NotFound();

            return Ok(result);
        }

        [Authorize("RegularUser")]
        [HttpGet("{id}/download")]
        public async Task<IActionResult> Download(Guid id, [FromServices] IDocumentDownloadService service)
        {
            var result = await service.RunAsync(id);

            if (result == null || result.FileStream == null)
            {
                return NotFound();
            }

            return File(result.FileStream, "application/octet-stream", result.FileName);
        }

        [Authorize("Admin")]
        [HttpPost]
        [DisableFormValueModelBinding]
        public async Task<IActionResult> Upload()
        {
            if (!MultipartRequestManager.IsMultipartContentType(Request.ContentType))
            {
                throw new FormatException("Form without multipart content.");
            }

            var count = 0;
            var totalSize = 0L;

            var boundary = MultipartRequestManager.GetBoundary(MediaTypeHeaderValue.Parse(Request.ContentType));
            var reader = new MultipartReader(boundary, HttpContext.Request.Body);
            var section = await reader.ReadNextSectionAsync();

            do
            {
                ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition);
                if (!MultipartRequestManager.HasFileContentDisposition(contentDisposition))
                {
                    section = await reader.ReadNextSectionAsync();
                    continue;
                }

                var fileSection = section.AsFileSection();
                if (uploadConfigurations.FileTypeAllowed(fileSection.FileName) == false)
                {
                    return ValidationProblem($"File type from file {fileSection.FileName} is not allowed. These are the file types allowed: ${uploadConfigurations.AcceptedFileTypes}");
                }

                totalSize = totalSize + await uploadService.RunAsync(fileSection, fileSection.FileName, RequestedBy());

                count++;
                section = await reader.ReadNextSectionAsync();
            } while (section != null);

            return Ok(new { Count = count, Size = SizeConverter(totalSize) });

        }

        private string SizeConverter(long bytes)
        {
            var fileSize = new decimal(bytes);
            var kilobyte = new decimal(1024);
            var megabyte = new decimal(1024 * 1024);
            var gigabyte = new decimal(1024 * 1024 * 1024);

            switch (fileSize)
            {
                case var _ when fileSize < kilobyte:
                    return $"Less then 1KB";
                case var _ when fileSize < megabyte:
                    return $"{Math.Round(fileSize / kilobyte, 0, MidpointRounding.AwayFromZero):##,###.##}KB";
                case var _ when fileSize < gigabyte:
                    return $"{Math.Round(fileSize / megabyte, 2, MidpointRounding.AwayFromZero):##,###.##}MB";
                case var _ when fileSize >= gigabyte:
                    return $"{Math.Round(fileSize / gigabyte, 2, MidpointRounding.AwayFromZero):##,###.##}GB";
                default:
                    return "n/a";
            }
        }

        [Authorize("Admin")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] DocumentRequest request, [FromServices] IDocumentUpdateService service)
        {
            (Guid documentId, string message) = await service.RunAsync(request, RequestedBy(), UserRoleId());

            if (documentId == Guid.Empty)
                return ValidationProblem(message);

            return Ok(new { documentId, message });
        }

        [Authorize("Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, [FromServices] IDocumentDeleteService service)
        {
            (bool deleted, string message) = await service.RunAsync(id, RequestedBy());

            if (deleted == false)
                return ValidationProblem(message);

            return Ok();

        }
    }
}
