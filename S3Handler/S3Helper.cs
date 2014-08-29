using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System.Net;
using System.IO;
using Amazon.S3.IO;

namespace S3Handler
{
    public class S3Helper : IS3Helper
    {
        static IAmazonS3 client;
        public bool CreateBucket(string userID)
        {
            bool retval = false;
            using (client = Amazon.AWSClientFactory.CreateAmazonS3Client(RegionEndpoint.APSoutheast1))
            {
                try
                {
                    //create bucket
                    PutBucketRequest request = new PutBucketRequest();
                    request.BucketName = userID;
                    request.BucketRegion = S3Region.APS1;
                    request.CannedACL = S3CannedACL.PublicRead;
                    PutBucketResponse res;
                    res = client.PutBucket(request);
                    retval = true;
                }
                catch (AmazonS3Exception amazonS3Exception)
                {
                    throw amazonS3Exception;
                }
            }
            return retval;
        }

        public bool CheckBucketIsExist(string userid)
        {
            bool retval = false;
            using (client = Amazon.AWSClientFactory.CreateAmazonS3Client(RegionEndpoint.APSoutheast1))
            {
                try
                {
                    S3DirectoryInfo bucket = new S3DirectoryInfo(client, userid);
                    retval = bucket.Exists;
                }
                catch (AmazonS3Exception amazonS3Exception)
                {
                    throw amazonS3Exception;
                }
            }
            return retval;
        }

        public bool Upload(string userID, string fileName, Stream file)
        {
            bool retval = false;

            try
            {
                TransferUtility fileTransferUtility = new
                    TransferUtility(new AmazonS3Client(Amazon.RegionEndpoint.APSoutheast1));
                TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();
                request.InputStream = file;
                request.Key = fileName;
                request.BucketName = userID;
                request.CannedACL = S3CannedACL.PublicRead;
                fileTransferUtility.Upload(request);//file, userID, fileName);

            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                throw amazonS3Exception;
            }
            return retval;
        }
        //public void test()
        //{

        //    //create bucket
        //    //using (client = Amazon.AWSClientFactory.CreateAmazonS3Client(RegionEndpoint.APSoutheast1))
        //    //{
        //    //    try
        //    //    {
        //    //        PutBucketRequest request = new PutBucketRequest();
        //    //        request.BucketName = "pin.2234567890";
        //    //        request.BucketRegion = S3Region.APS1;
        //    //        PutBucketResponse res;
        //    //        res = client.PutBucket(request);
        //    //    }
        //    //    catch (AmazonS3Exception amazonS3Exception)
        //    //    {
        //    //        if (amazonS3Exception.ErrorCode != null && (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") || amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
        //    //        {
        //    //            Console.WriteLine("Please check the provided AWS Credentials.");
        //    //            Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");
        //    //        }
        //    //        else
        //    //        {
        //    //            Console.WriteLine("An Error, number {0}, occurred when creating a bucket with the message '{1}", amazonS3Exception.ErrorCode, amazonS3Exception.Message);
        //    //        }
        //    //    }
        //    //}




        //    //read
        //    //string responseBody = "";
        //    //string searchBucket="pin.2234567890";
        //    //try
        //    //{
        //    //    //IAmazonS3 clientid = Amazon.AWSClientFactory.CreateAmazonS3Client(RegionEndpoint.APSoutheast1);

        //    //    using (client = new AmazonS3Client(Amazon.RegionEndpoint.APSoutheast1))
        //    //    {

        //    //        GetObjectRequest request = new GetObjectRequest
        //    //        {
        //    //            BucketName = searchBucket,
        //    //            Key = "123.png"
        //    //        };
        //    //        using (GetObjectResponse response = client.GetObject(request))
        //    //        using (Stream responseStream = response.ResponseStream)
        //    //        using (StreamReader reader = new StreamReader(responseStream))
        //    //        {
        //    //            string title = response.Metadata["x-amz-meta-title"];
        //    //            Console.WriteLine("The object's title is {0}", title);
        //    //            //string dest = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "1234567890.png");
        //    //            //if (!File.Exists(dest))
        //    //            //{
        //    //            //    response.WriteResponseStreamToFile(dest);
        //    //            //}
        //    //            //responseBody = reader.ReadToEnd();
        //    //        }
        //    //    }
        //    //}
        //    //catch (AmazonS3Exception amazonS3Exception)
        //    //{
        //    //    if (amazonS3Exception.ErrorCode != null &&
        //    //        (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
        //    //        ||
        //    //        amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
        //    //    {
        //    //        Console.WriteLine("Check the provided AWS Credentials.");
        //    //        Console.WriteLine(
        //    //        "To sign up for service, go to http://aws.amazon.com/s3");
        //    //    }
        //    //    else
        //    //    {
        //    //        Console.WriteLine(
        //    //         "Error occurred. Message:'{0}' when listing objects",
        //    //         amazonS3Exception.Message);
        //    //    }
        //    //}

        //    //temp
        //    //string result = null;
        //    //int status = 0;
        //    //HttpWebResponse resp = null;
        //    //HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("https://www.dropbox.com/s/4klob6numdne7ti/%E7%9B%B8%E7%89%87%202013-9-25%20%E4%B8%8B%E5%8D%886%2006%2030.png");
        //    //// augment the request here: headers (Referer, User-Agent, etc)
        //    ////     CookieContainer, Accept, etc.
        //    //resp = (HttpWebResponse)req.GetResponse();
        //    //Encoding responseEncoding = Encoding.GetEncoding(resp.CharacterSet);
        //    //using (StreamReader sr = new StreamReader(resp.GetResponseStream(), responseEncoding))
        //    //{
        //    //    result = sr.ReadToEnd();

        //    //}
        //    //status = (int)resp.StatusCode;

        //    //ent temp
        //    //upload
        //    string uploadBucket = "pin.1234567890";
        //    //TransferUtility utility = new TransferUtility(ConfigurationManager.AppSettings["AWSAccessKey"], ConfigurationManager.AppSettings["AWSSecretKey"], RegionEndpoint.APSoutheast1);
        //    //utility.Upload(resp.GetResponseStream(), uploadBucket, "123.png");
        //    try
        //    {
        //        TransferUtility fileTransferUtility = new
        //            TransferUtility(new AmazonS3Client(Amazon.RegionEndpoint.APSoutheast1));

        //        //// 1. Upload a file, file name is used as the object key name.
        //        //fileTransferUtility.Upload(filePath, existingBucketName);
        //        //Console.WriteLine("Upload 1 completed");

        //        //// 2. Specify object key name explicitly.
        //        //fileTransferUtility.Upload(filePath,
        //        //                          existingBucketName, keyName);
        //        //Console.WriteLine("Upload 2 completed");

        //        // 3. Upload data from a type of System.IO.Stream.
        //        //using (FileStream fileToUpload =
        //        //    new FileStream("D:\\123.png", FileMode.Open, FileAccess.Read))
        //        //{

        //        //string uploadBucket = "pin.1234567890";
        //        //fileTransferUtility.Upload(Request.Files[0].InputStream,
        //        //               uploadBucket, Request.Files[0].FileName);

        //        //}
        //        Console.WriteLine("Upload 3 completed");

        //        // 4.Specify advanced settings/options.
        //        //TransferUtilityUploadRequest fileTransferUtilityRequest = new TransferUtilityUploadRequest
        //        //{
        //        //    BucketName = uploadBucket,
        //        //    FilePath = "D:\\123.png",
        //        //    StorageClass = S3StorageClass.ReducedRedundancy,
        //        //    PartSize = 6291456, // 6 MB.
        //        //    Key = keyName,
        //        //    CannedACL = S3CannedACL.PublicRead
        //        //};
        //        //fileTransferUtilityRequest.Metadata.Add("param1", "Value1");
        //        //fileTransferUtilityRequest.Metadata.Add("param2", "Value2");
        //        //fileTransferUtility.Upload(fileTransferUtilityRequest);
        //        //Console.WriteLine("Upload 4 completed");
        //    }
        //    catch (AmazonS3Exception s3Exception)
        //    {
        //        Console.WriteLine(s3Exception.Message,
        //                          s3Exception.InnerException);
        //    }
        //}
    }
}
