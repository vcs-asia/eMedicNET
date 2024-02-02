using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon;
using Amazon.Runtime;
using Microsoft.AspNetCore.Http;

namespace eMedicNETv7.Extensions
{
    public static class AmazonS3
    {
        private const string bucketName = "cicems";
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.APSoutheast1;
        private static string AccessKey = "AKIASCJMMGY6PN6IQSGQ";
        private static string SecretKey = "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";
        private static BasicAWSCredentials basicAWSCredentials = new BasicAWSCredentials(AccessKey, SecretKey);

        private static IAmazonS3 client = new AmazonS3Client(basicAWSCredentials, bucketRegion);

        public static async Task WritingAnObjectAsync(IFormFile file, string name, string subfolder)
        {
            try
            {
                var putRequest = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = subfolder + name,
                    InputStream = file.OpenReadStream(),
                    ContentType = file.ContentType
                };

                PutObjectResponse response = await client.PutObjectAsync(putRequest);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
        public static async Task<GetObjectResponse> ReadingAnObjectAsync(string name, string subfolder)
        {
            BasicAWSCredentials basicAWSCredentials = new BasicAWSCredentials(AccessKey, SecretKey);
            client = new AmazonS3Client(basicAWSCredentials, bucketRegion);

            var request = new GetObjectRequest()
            {
                BucketName = bucketName,
                Key = subfolder + name
            };

            return await client.GetObjectAsync(request);
        }

        public static FileInfo GetFile(string fileName, string subfolder)
        {
            using (GetObjectResponse response = ReadingAnObjectAsync(fileName, subfolder).Result)
            {
                byte[] buffer = new byte[16 * 1024];
                using (MemoryStream stream = new MemoryStream())
                {
                    int read;
                    while ((read = response.ResponseStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        stream.Write(buffer, 0, read);
                    }
                    FileInfo fileInfo = new FileInfo();
                    fileInfo.ContentType = response.Headers["Content-Type"];
                    fileInfo.ContentData = stream.ToArray();

                    return fileInfo;
                }
            }
            /*
            Amazon.S3.Model.GetObjectResponse response = await AmazonObject.ReadingAnObjectAsync(fileName);
            using Stream responseStream = response.ResponseStream;
            var stream = new MemoryStream();
            await responseStream.CopyToAsync(stream);
            stream.Position = 0;

            return new FileStreamResult(stream, response.Headers["Content-Type"])
            {
                FileDownloadName = fileName
            };*/

        }
    }
    public class FileInfo
    {
        public string ContentType { get; set; } = null!;
        public byte[] ContentData { get; set; } = null!;
    }
}
