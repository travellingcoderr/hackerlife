using Amazon.S3;
using Amazon.S3.Model;
using LifeBackup.Core.Communication.Bucket;
using LifeBackup.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace LifeBackup.Infrastructure.Repositories
{
    public class BucketRepository : IBucketRepository
    {
        // Access AmazonS3Client 
        IAmazonS3 _s3Client;

        public BucketRepository(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        public async Task<CreateS3BucketResponse> CreateBucketAsync(string bucketName)
        {
            var putBucketRequest = new PutBucketRequest {
                BucketName = bucketName,
                UseClientRegion = true
            };

            var response = await _s3Client.PutBucketAsync(putBucketRequest);
            return new CreateS3BucketResponse
            {
                RequestId = response.ResponseMetadata.RequestId,
                BucketName = bucketName
            };
        }

        public async Task DeleteBucketAsync(string bucketName)
        {
            var deleteBucketRequest = new DeleteBucketRequest { BucketName = bucketName, UseClientRegion = true };
            var response = await _s3Client.DeleteBucketAsync(deleteBucketRequest);            
        }

        public async Task<bool> DoesS3BucketExistAsync(string bucketName)
        {
            return await _s3Client.DoesS3BucketExistAsync(bucketName);
        }

        public async Task<IEnumerable<ListS3BucketResponse>> ListBucketsAsync()
        {
            var listBucketRequest = new ListBucketsRequest();
            var response = await _s3Client.ListBucketsAsync(listBucketRequest);
            var list = new List<ListS3BucketResponse>();
            response.Buckets.ForEach(b =>
            {
                list.Add(new ListS3BucketResponse { BucketName = b.BucketName, CreationDate = b.CreationDate });
            });
            return list;
        }
    }
}
