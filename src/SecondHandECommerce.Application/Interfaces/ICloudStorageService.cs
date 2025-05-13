namespace SecondHandECommerce.Application.Interfaces;

public interface ICloudStorageService
{
    Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType);
    Task<string> GetPreSignedUrlAsync(string fileName, TimeSpan expiry);

}
