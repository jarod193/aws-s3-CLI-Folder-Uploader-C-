S3 Folder Uploader CLI (C# + AWS)

A command-line utility that uploads all files from a local folder to an Amazon S3 bucket, organizing them by folder name and logging the process.

---

Features

- Uploads every file in a specified local folder to S3
- Retains folder structure in S3 via "folderName/filename.ext" keys
- Appends all upload activity to "upload_log.txt"
- Catches and logs AWS-specific and general errors per file
- Simple CLI interface — no AWS console interaction required

---

Tech Stack

- C#
- .NET Core
- AWSSDK.S3
- Visual Studio

---

How to Use

Configure AWS credentials 
   Run aws configure:
   
   aws_access_key_id = YOUR_KEY
   aws_secret_access_key = YOUR_SECRET
   region = us-east-2

Set your bucket name
Inside Program.cs, update:

private const string bucketName = "your-s3-bucket-name";
Run the program
Use dotnet run, then:

Enter folder path:
C:\Users\YourName\Documents\UploadThis

Result

Files are uploaded to S3 at UploadThis/filename.ext

A log is generated: upload_log.txt

Future Improvements
 Add visual progress bar

 Add drag-and-drop UI (WinForms or WPF)

 Add dry-run mode or delete-after-upload option

Author
	Jarod Wood
	AWS Certified Cloud Practitioner | Microsoft C# Certified


