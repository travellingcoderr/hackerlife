using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LifeBackup.Core.Communication.Bucket;
using LifeBackup.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LifeBackup.Api.Controllers
{
    [Route("/api/bucket")]
    public class BucketController : ControllerBase
    {
        private readonly IBucketRepository _bucketRepository;

        public BucketController(IBucketRepository bucketRepository)
        {
            _bucketRepository = bucketRepository;
        }

        [HttpPost]
        [Route("create/{bucketName}")]
        public async Task<ActionResult<CreateS3BucketResponse>> CreateBucket([FromRoute] string bucketName)
        {
            // validate for bucketname 
            var bucketExists = await _bucketRepository.DoesS3BucketExistAsync(bucketName);
            if (bucketExists)
            {
                BadRequest("S3 Bucket already exists!");
            }

            var result = await _bucketRepository.CreateBucketAsync(bucketName);
            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);            
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<IEnumerable<ListS3BucketResponse>>> ListS3Buckets()
        {
            var result = await _bucketRepository.ListBucketsAsync();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{bucketName}")]
        public async Task<IActionResult> DeleteS3Bucket(string bucketName)
        {
            await _bucketRepository.DeleteBucketAsync(bucketName);

            return Ok();
        }

    }
}