using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
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
        private readonly CloudBlobClient _client;

        public BlobRepository(IOptions<BlobRepositoryOptions> options)
        {
            _client = CloudStorageAccount.Parse(options.Value.ConnectionString).CreateCloudBlobClient();
        }

        public string Get(long id)
        {
            var container = _client.GetContainerReference("posters");
            var blob = container.GetBlockBlobReference(id.ToString());

            var readOnly = new SharedAccessBlobPolicy
            {
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(24),
                Permissions = SharedAccessBlobPermissions.Read
            };

            var signature = container.GetSharedAccessSignature(readOnly);

            return blob + signature;
        }

        public async Task Upload(long id, IFormFile file)
        {
            var container = _client.GetContainerReference("posters");
            var blob = container.GetBlockBlobReference(id.ToString());
            await blob.UploadFromStreamAsync(file.OpenReadStream());
        }
    }
}
