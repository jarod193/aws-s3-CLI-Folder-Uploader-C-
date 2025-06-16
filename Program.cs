using System;
using System.IO;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon;
using System.Runtime.CompilerServices;

class Program
{
    private const string bucketName = "gallery.submission.1"; //Declares bucketname for rest of code
    private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USEast2; //Declares Region to utilize

    //Declare log file path for logging
    private static readonly string logFilePath = "upload_log.txt";


    static async Task Main(string[] args)
    {

        //Ensures the folder path is correct and exists

        Console.WriteLine("Enter folder path: ");
        string folderPath = Console.ReadLine().Trim('"');

        if (string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath))
        {
            Console.WriteLine("Invalid folder path.");
            return;
        }
        //Create an Array of files the folder for the loop to process one by one
        string[] files = Directory.GetFiles(folderPath);
        string folderName = Path.GetFileName(folderPath);

        //Log start of upload session

        LogToFile("=== Upload Session Started ===");
        LogToFile($"Folder: {folderPath}");
        //Create a loop to iterate through each file and update the name using the s3 Key

        foreach (string file in files)
        {
            try
            {
                string fileName = Path.GetFileName(file);
                string s3Key = $"{folderName}/{fileName}";

                Console.WriteLine($"Uploading {fileName} to S3 with key {s3Key}...");
                LogToFile($"Uploading {fileName} to S3 with key {s3Key}...");

                //Call the method using await to ensure the loop doesn't continue until the method is complete
                await UploadFileToS3(file, s3Key);

                LogToFile($"Successfully uploaded {fileName} to S3 with key {s3Key}");
            }
            catch (AmazonS3Exception s3Ex)
            {
                LogToFile($"S3 error for {file}: {s3Ex.Message}");
                Console.WriteLine($"S3 error: {s3Ex.Message}");
            }
            catch (Exception ex)
            {
                LogToFile($"General error for {file}: {ex.Message}");
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }




    }
    //Create method to upload files to S3
    static async Task UploadFileToS3(string filePath, string s3Key)
    {
        var s3Client = new AmazonS3Client();
        var transferUtility = new TransferUtility(s3Client);

        var uploadRequest = new TransferUtilityUploadRequest
        {
            BucketName = bucketName,
            FilePath = filePath,
            Key = s3Key
        };

        await transferUtility.UploadAsync(uploadRequest);
        Console.WriteLine($"File {filePath} uploaded to S3 with key {s3Key}");
    }

    //Create a method to log the upload details to a file
    static void LogToFile(string message)
    {
        using (StreamWriter writer = File.AppendText(logFilePath))
        {
            writer.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}");
        }
    }
}
