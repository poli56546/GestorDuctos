using System.Linq;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

namespace GestorDuctos.Services
{
    public class SupabaseUploadResult
    {
        public bool Ok { get; set; }
        public string? StoragePath { get; set; }
        public string? PublicUrl { get; set; }
        public string? Error { get; set; }
    }

    public class SupabaseStorageService
    {
        private readonly HttpClient _httpClient;
        private readonly string _supabaseUrl;
        private readonly string _serviceKey;
        private readonly string _bucketName;

        public SupabaseStorageService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;

            _supabaseUrl = (configuration["SUPABASE_URL"]
                ?? Environment.GetEnvironmentVariable("SUPABASE_URL")
                ?? "https://hhtfudrlsxhweuhpodkf.supabase.co").TrimEnd('/');

            _serviceKey = configuration["SUPABASE_SERVICE_KEY"]
                ?? Environment.GetEnvironmentVariable("SUPABASE_SERVICE_KEY")
                ?? string.Empty;

            _bucketName = configuration["SUPABASE_BUCKET"]
                ?? Environment.GetEnvironmentVariable("SUPABASE_BUCKET")
                ?? "documentos-ductos";
        }

        public async Task<SupabaseUploadResult> SubirArchivoAsync(
            Stream archivoStream,
            string nombreOriginal,
            string contentType,
            string subcarpeta = "planos")
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_supabaseUrl) || string.IsNullOrWhiteSpace(_serviceKey))
                {
                    return new SupabaseUploadResult
                    {
                        Ok = false,
                        Error = "Faltan SUPABASE_URL o SUPABASE_SERVICE_KEY en la configuración."
                    };
                }

                var nombreLimpio = string.Concat(nombreOriginal.Where(c =>
                    char.IsLetterOrDigit(c) || c is '-' or '_' or '.'));

                if (string.IsNullOrWhiteSpace(nombreLimpio))
                {
                    nombreLimpio = "archivo";
                }

                var carpetaLimpia = subcarpeta.Trim('/');
                var storagePath = string.IsNullOrWhiteSpace(carpetaLimpia)
                    ? $"{Guid.NewGuid()}_{nombreLimpio}"
                    : $"{carpetaLimpia}/{Guid.NewGuid()}_{nombreLimpio}";

                var uploadUrl = $"{_supabaseUrl}/storage/v1/object/{Uri.EscapeDataString(_bucketName)}/{storagePath}";

                using var ms = new MemoryStream();
                await archivoStream.CopyToAsync(ms);
                var bytes = ms.ToArray();

                using var request = new HttpRequestMessage(HttpMethod.Post, uploadUrl);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _serviceKey);
                request.Headers.Add("apikey", _serviceKey);
                request.Headers.Add("x-upsert", "true");

                var content = new ByteArrayContent(bytes);
                content.Headers.ContentType = new MediaTypeHeaderValue(
                    string.IsNullOrWhiteSpace(contentType) ? "application/octet-stream" : contentType);
                request.Content = content;

                using var response = await _httpClient.SendAsync(request);
                var responseBody = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new SupabaseUploadResult
                    {
                        Ok = false,
                        Error = $"HTTP {(int)response.StatusCode}: {responseBody}"
                    };
                }

                var publicUrl = $"{_supabaseUrl}/storage/v1/object/public/{Uri.EscapeDataString(_bucketName)}/{storagePath}";

                return new SupabaseUploadResult
                {
                    Ok = true,
                    StoragePath = storagePath,
                    PublicUrl = publicUrl
                };
            }
            catch (Exception ex)
            {
                return new SupabaseUploadResult
                {
                    Ok = false,
                    Error = ex.Message
                };
            }
        }
    }
}