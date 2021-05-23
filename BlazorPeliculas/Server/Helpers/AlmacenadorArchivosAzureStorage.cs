using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BlazorPeliculas.Server.Helpers
{
    public class AlmacenadorArchivosAzureStorage : IAlmacenadorArchivos
    {
        private string connectionString;
        public AlmacenadorArchivosAzureStorage(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("AzureStorage");
        }
        public async Task<string> EditarArchivo(byte[] contenido, string extension, string nombreContenedor, string ruta)
        {
            if (!string.IsNullOrWhiteSpace(ruta))
            {
                await EliminarArchivo(ruta, nombreContenedor);
            }
            return await GuardarArchivo(contenido, extension, nombreContenedor);
        }

        public async Task EliminarArchivo(string ruta, string nombreContenedor)
        {
            if (string.IsNullOrWhiteSpace(ruta)) return;
            var cliente = new BlobContainerClient(connectionString, nombreContenedor);
            await cliente.CreateIfNotExistsAsync();
            var nombreArchivo = Path.GetFileName(ruta);
            var blob = cliente.GetBlobClient(nombreArchivo);
            await blob.DeleteIfExistsAsync();
        }

        public async Task<string> GuardarArchivo(byte[] contenido, string extension, string nombreContenedor)
        {
            var cliente = new BlobContainerClient(connectionString, nombreContenedor);
            await cliente.CreateIfNotExistsAsync();
            cliente.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

            var archivoNombre = $"{Guid.NewGuid()}{extension}";
            var blob = cliente.GetBlobClient(archivoNombre);
            using (var ms = new MemoryStream(contenido))
            {
                await blob.UploadAsync(ms);
            }
            return blob.Uri.ToString();
        }
    }
}
