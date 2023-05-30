using Azure.Storage;
using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Extensions.Configuration;
using MKT.Application.Infrastructure.Storage.Models;
using MKT.Application.Interfaces;


namespace MKT.Infrastructure.AzureBlobStorage
{
    public class AzureBlobStorageService : IStorageService
    {
        private string storageAccountName;
        private string accessKey;
        private StorageSharedKeyCredential _credential;
        private BlobServiceClient _blobServiceClient ;
        private BlobContainerClient _blobContainerClient;
        private BlobClient _blobClient;

        private IDictionary<ResourceType, string> _containersNames = new Dictionary<ResourceType, string>(){
            {ResourceType.MenuItem, "menuitems"},
            {ResourceType.Offer, "offers"},
            {ResourceType.Article, "articles"},
            {ResourceType.Invoice, "invoices"},
             {ResourceType.BuildAppGuid, "buildapp"}
        };

        public AzureBlobStorageService(IConfiguration config)
        {

            // Read the storage account name and access key from the appsettings.json file
            string storageAccountName = config["AzureStorage:AccountName"];
            string accessKey = config["AzureStorage:AccessKey"];

            _SetupBlobServiceClient();
        }

 


        private void _SetupBlobServiceClient()
        {


            // 1.Create a StorageSharedKeyCredential object using the storage account name and access key
            _credential = new StorageSharedKeyCredential(storageAccountName, accessKey);

            // 2.Create a BlobServiceClient object using the StorageSharedKeyCredential object
            _blobServiceClient  = new BlobServiceClient(new Uri($"https://{storageAccountName}.blob.core.windows.net"), _credential);
        }

        private async Task<BlobContainerClient> _SetupBlobContainerClient(ResourceType resourceType = ResourceType.BuildAppGuid)
        {

            // 3.Retrieve the desired container using the BlobServiceClient object and the container name
            _blobContainerClient  = _blobServiceClient .GetBlobContainerClient(_containersNames[resourceType]);

            // Check if the container exists
            if (!_blobContainerClient .Exists())
            {
                // Create the container if it does not exist
                await _blobContainerClient .CreateIfNotExistsAsync();
            }

            return _blobContainerClient ;
        }

        private string _GenerateFileName(string fileName)
            => string.Concat(Guid.NewGuid().ToString(), Path.GetExtension(fileName));


        public async Task downloadResourceAsync()
        {
            var blobName = "BuildAppGuid.pdf";
            _blobClient = _blobContainerClient.GetBlobClient(blobName);

            using (var stream = new MemoryStream())
            {
                await _blobClient.DownloadToAsync(stream);
                File.WriteAllBytes("<localPath>/" + blobName, stream.ToArray());
                Console.WriteLine("Blob downloaded successfully!");
            }
        }

        public Task<string> UploadResourceAsync(Resource resource)
        {
            throw new NotImplementedException();
        }

        public Task<string> UploadResourceAsync(InvoiceResource invoiceResource)
        {
            throw new NotImplementedException();
        }
    }
}