using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryShop.Data.Service.Impl
{
    public class FileService : IFileService
    {
        public readonly IConfiguration _configuration;
        public readonly BlobServiceClient _blobServiceClient;
        public FileService(IConfiguration configuration, BlobServiceClient blobServiceClient)
        {
            _configuration = configuration;
            _blobServiceClient = blobServiceClient;
        }

        public string GetFileUri(string fileName)
        {
            var containerName = _configuration.GetSection("FileStorage:ContainerName").Value;
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blockBlobClient = containerClient.GetBlockBlobClient(fileName);
            return Uri.UnescapeDataString(blockBlobClient.Uri.ToString());
        }

        public bool Upload(IFormFile formFile)
        {
            var containerName = _configuration.GetSection("FileStorage:ContainerName").Value;
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(formFile.FileName);
            using var stream = formFile.OpenReadStream();
            var response = blobClient.Upload(stream, true);
            return response != null;
        }
    }
}
