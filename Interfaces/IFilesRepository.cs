using LifeBackup.Core.Communication.Files;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LifeBackup.Core.Interfaces
{
    public interface IFilesRepository
    {
        Task<AddFileResponse> UploadFiles(string bucketName, IList<IFormFile> formFiles);
        Task<IEnumerable<ListFilesResponse>> ListFiles(string bucketName);
        Task DownloadFile(string bucketName, string fileName);
        Task<DeleteFileResponse> DeleteFile(string bucketName, string fileName);
        Task AddJsonObject(string bucketName, AddJsonObjectRequest request);

        Task<GetJsonObjectResponse> GetJsonObject(string bucketName, string fileName);
    }
}
