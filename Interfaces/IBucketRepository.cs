using LifeBackup.Core.Communication.Bucket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LifeBackup.Core.Interfaces
{
    public interface IBucketRepository
    {
        Task<CreateS3BucketResponse> CreateBucketAsync(string bucketName);
        Task<bool> DoesS3BucketExistAsync(string bucketName);
        Task<IEnumerable<ListS3BucketResponse>> ListBucketsAsync();
        Task DeleteBucketAsync(string bucketName);
    }
}
