using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
using Findox.Api.Domain.Interfaces.Services.FileManagement;
using Findox.Api.Domain.Requests;
using Findox.Api.Domain.Responses;
using Findox.Api.Domain.Security;

namespace Findox.Api.Service.Services.FileManagement
{
    public class AwsStorageService : IAwsStorageService
    {
        private readonly AwsConfigurations awsConfigurations;

        public AwsStorageService(AwsConfigurations awsConfigurations)
        {
            this.awsConfigurations = awsConfigurations;
        }

        public async Task<S3UploadResponse> UploadFileAsync(S3UploadRequest request)
        {
            var credentials = new BasicAWSCredentials(awsConfigurations.AWSAccessKey, awsConfigurations.AWSSecretKey);

            var config = new AmazonS3Config()
            {
                RegionEndpoint = Amazon.RegionEndpoint.EUWest2
            };

            var response = new S3UploadResponse();
            try
            {
                var uploadRequest = new TransferUtilityUploadRequest()
                {
                    InputStream = request.InputStream,
                    Key = request.Name,
                    BucketName = awsConfigurations.AWSBucketName,
                    CannedACL = S3CannedACL.NoACL
                };

                // initialise client
                using var client = new AmazonS3Client(credentials, config);

                // initialise the transfer/upload tools
                var transferUtility = new TransferUtility(client);

                // initiate the file upload
                await transferUtility.UploadAsync(uploadRequest);

                response.StatusCode = 201;
                response.Message = $"{request.Name} has been uploaded sucessfully";
            }
            catch (AmazonS3Exception s3Ex)
            {
                response.StatusCode = (int)s3Ex.StatusCode;
                response.Message = s3Ex.Message;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<S3DownloadResponse> DownloadFileAsync(string fileName)
        {
            var credentials = new BasicAWSCredentials(awsConfigurations.AWSAccessKey, awsConfigurations.AWSSecretKey);

            var config = new AmazonS3Config()
            {
                RegionEndpoint = Amazon.RegionEndpoint.EUWest2
            };

            var response = new S3DownloadResponse();
            try
            {
                var downloadRequest = new TransferUtilityDownloadRequest()
                {
                    Key = fileName,
                    BucketName = awsConfigurations.AWSBucketName,
                };

                // initialise client
                using var client = new AmazonS3Client(credentials, config);

                // initialise the transfer/upload tools
                var transferUtility = new TransferUtility(client);

                // initiate the file upload
                response.OpenStream = await transferUtility.OpenStreamAsync(awsConfigurations.AWSBucketName, fileName);
                response.StatusCode = 201;
                response.Message = $"{fileName} has been uploaded sucessfully";
            }
            catch (AmazonS3Exception s3Ex)
            {
                response.StatusCode = (int)s3Ex.StatusCode;
                response.Message = s3Ex.Message;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
