using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenRoad.DataAccess
{
    public class BlobClienteHelper
    {
        public CloudBlobClient _blobClient;
        public CloudBlobContainer _blobCointainer;

        private const string _blobContainerName = "igor-imperiali";

        public async Task SetupCloudBlob(string connectionString)
        {            
            var storageAccount = CloudStorageAccount.Parse(connectionString);

            _blobClient = storageAccount.CreateCloudBlobClient();
            _blobCointainer = _blobClient.GetContainerReference(_blobContainerName);

            await _blobCointainer.CreateIfNotExistsAsync();

            var permission = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            };

            await _blobCointainer.SetPermissionsAsync(permission);
        }

        public string GetRandomBlobName(string filename)
        {
            string ext = Path.GetExtension(filename);
            return string.Format("{0:10}_{1}{2}", DateTime.Now.Ticks, Guid.NewGuid(), ext);
        }
    }
}
