using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using System.Net;
using ZBS.Infrastructure.Models;
using ZBS.Infrastructure.Repositories.EBook;
using ZBS.Infrastructure.Repositories.EBook.CRUDmodels;
using ZBS.Shared.Helpers.EBooks;

namespace ZBS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EBookController : ControllerBase
    {
        private readonly EBookRepository ebookRepository;

        public EBookController(EBookRepository ebookRepository)
        {
            this.ebookRepository = ebookRepository;
        }

        [HttpPost("UploadFiles")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UploadFiles([FromForm] List<IFormFile> files)
        {
            var uploadResponse = await ebookRepository.UploadFiles(files);
            if (uploadResponse.ErrorMessage != "")
                return BadRequest(new { error = uploadResponse.ErrorMessage });
            return Ok(uploadResponse);
        }

        [Route("DownloadFile")]
        [HttpGet]
        public async Task<IActionResult> DownloadFile(int id)
        {
            var stream = await ebookRepository.DownloadFile(id);
            if (stream == null)
                return NotFound();
            return new FileContentResult(stream, "application/octet-stream");
        }

        [Route("DownloadFiles")]
        [HttpGet]
        public async Task<List<FileDownloadView>> DownloadFiles()
        {
            var files = await ebookRepository.DownloadFiles();
            return files.ToList();
        }

        [HttpGet]
        public async Task<IEnumerable<GetEBooks>> GetAllEBooks(int currentPageNumber, int pageSize)
        {
            return await ebookRepository.GetAllEBooks(currentPageNumber, pageSize);
        }

        [HttpGet("{id}")]
        public async Task<GetEBooks> GetEBookById(int id)
        {
            return await ebookRepository.GetEBookById(id);
        }

    }
}
