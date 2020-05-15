using System;
using System.Threading.Tasks;
using CinemaConstructor.Database.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace CinemaConstructor.Database.Repositories
{
    public class BlobRepository
    {
        private readonly CloudBlobContainer _container;
        private readonly string _signature;

        public BlobRepository(IOptions<BlobRepositoryOptions> options)
        {
            var client = CloudStorageAccount.Parse(options.Value.ConnectionString).CreateCloudBlobClient();

            _container = client.GetContainerReference("posters");
            var readOnly = new SharedAccessBlobPolicy
            {
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(24),
                Permissions = SharedAccessBlobPermissions.Read
            };
            _signature = _container.GetSharedAccessSignature(readOnly);
        }

        public string Get(long id)
        {
            var blob = _container.GetBlockBlobReference(id.ToString());
            return blob.Uri + _signature;
        }

        public async Task Upload(long id, IFormFile file)
        {
            var blob = _container.GetBlockBlobReference(id.ToString());
            await blob.UploadFromStreamAsync(file.OpenReadStream());
        }
    }
}
