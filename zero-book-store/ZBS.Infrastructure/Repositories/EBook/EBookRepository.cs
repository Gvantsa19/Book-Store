using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ZBS.Infrastructure.DBContexts;
using ZBS.Infrastructure.Models;
using ZBS.Infrastructure.Repositories.EBook.CRUDmodels;
using ZBS.Shared.Helpers;
using ZBS.Shared.Helpers.EBooks;

namespace ZBS.Infrastructure.Repositories.EBook
{
    public class EBookRepository
    {
        private readonly DataBaseContext context;
        private readonly DbcontextDapper dbcontextDapper;

        public EBookRepository(DataBaseContext context, DbcontextDapper dbcontextDapper)
        {
            this.context = context;
            this.dbcontextDapper = dbcontextDapper;
        }
        public async Task<FileUploadResponse> UploadFiles(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            List<FileUploadResponseData> uploadedFiles = new List<FileUploadResponseData>();

            try
            {
                foreach (var f in files)
                {
                    string name = f.FileName.Replace(@"\\\\", @"\\");

                    if (f.Length > 0)
                    {
                        var memoryStream = new MemoryStream();

                        try
                        {
                            await f.CopyToAsync(memoryStream);

                            //Upload check if less than 2mb!
                            if (memoryStream.Length < 2097152)
                            {
                                var file = new EBooks()
                                {
                                    FileName = Path.GetFileName(name),
                                    FileSize = (int)memoryStream.Length,
                                    UploadDate = DateTime.Now,
                                    UploadedBy = "Admin",
                                    FileContent = memoryStream.ToArray(),
                                };

                                context.EBooks.Add(file);

                                await context.SaveChangesAsync();

                                uploadedFiles.Add(new FileUploadResponseData()
                                {
                                    Id = file.Id,
                                    Status = "OK",
                                    FileName = Path.GetFileName(name),
                                    ErrorMessage = "",
                                });
                            }
                            else
                            {
                                uploadedFiles.Add(new FileUploadResponseData()
                                {
                                    Id = 0,
                                    Status = "ERROR",
                                    FileName = Path.GetFileName(name),
                                    ErrorMessage = "File " + f + " failed to upload"
                                });
                            }
                        }
                        finally
                        {
                            memoryStream.Close();
                            memoryStream.Dispose();
                        }
                    }
                }
                return new FileUploadResponse() { Data = uploadedFiles, ErrorMessage = "" };
            }
            catch (Exception ex)
            {
                return new FileUploadResponse() { ErrorMessage = ex.Message.ToString() };
            }
        }

        public async Task<IEnumerable<FileDownloadView>> DownloadFiles()
        {
            IEnumerable<FileDownloadView> downloadFiles =
            context.EBooks.ToList().Select(f =>
                new FileDownloadView
                {
                    Id = f.Id,
                    FileName = f.FileName,
                    FileSize = f.FileContent.Length
                }
            );
            return downloadFiles;
        }

        public async Task<byte[]> DownloadFile(int id)
        {
            try
            {
                var selectedFile = context.EBooks
                    .Where(f => f.Id == id)
                    .SingleOrDefault();

                if (selectedFile == null)
                    return null;
                return selectedFile.FileContent;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<GetEBooks>> GetAllEBooks(int currentPageNumber, int pageSize)
        {
            using var connection = dbcontextDapper.OpenConnection();
            int maxPagSize = 50;
            pageSize = (pageSize > 0 && pageSize <= maxPagSize) ? pageSize : maxPagSize;

            int skip = (currentPageNumber - 1) * pageSize;
            int take = pageSize;

            string query = @"SELECT 
                            COUNT(*)
                            FROM [EBooks]
 
                            SELECT  * FROM [EBooks]
                            ORDER BY Id
                            OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

            var reader = connection.QueryMultiple(query, new { Skip = skip, Take = take });
            int count = reader.Read<int>().FirstOrDefault();
            List<GetEBooks> allTodos = reader.Read<GetEBooks>().ToList();

            var result = new PagingResponseModel<List<GetEBooks>>(allTodos, count, currentPageNumber, pageSize);
            return result.Data;
        }

        public async Task<GetEBooks> GetEBookById(int id)
        {
            using var connection = dbcontextDapper.OpenConnection();

            string query = @"SELECT [Id],
                              [FileName] FROM [EBooks] WHERE Id = @Id";

            var book = await connection.QueryFirstOrDefaultAsync<GetEBooks>(query, new { Id = id });
            return book;
        }
    }
}
