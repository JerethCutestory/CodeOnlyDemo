using System;
using System.Linq;
using CodeOnlyDemo.Controllers;
using CoreGraphics;
using Foundation;
using UIKit;

namespace CodeOnlyDemo
{
    public class UrlSessionDelegate : NSObject, INSUrlSessionDownloadDelegate
	{
        public GuitarListViewController controller;

		public UrlSessionDelegate(GuitarListViewController controller)
		{
			this.controller = controller;
		}

		public void DidWriteData(NSUrlSession session, NSUrlSessionDownloadTask downloadTask, long bytesWritten, long totalBytesWritten, long totalBytesExpectedToWrite)
		{
			Console.WriteLine("Set Progress");
			//if (downloadTask == controller.downloadTask)
			//{
			//	float progress = totalBytesWritten / (float)totalBytesExpectedToWrite;
			//	Console.WriteLine(string.Format("DownloadTask: {0}  progress: {1}", downloadTask, progress));
			//	InvokeOnMainThread(() => {
			//		controller.ProgressView.Progress = progress;
			//	});
			//}
		}

		public void DidFinishDownloading(NSUrlSession session, NSUrlSessionDownloadTask downloadTask, NSUrl location)
		{
			Console.WriteLine("Finished");
			Console.WriteLine("File downloaded in : {0}", location);
			NSFileManager fileManager = NSFileManager.DefaultManager;

			var URLs = fileManager.GetUrls(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User);
			NSUrl documentsDictionry = URLs[0];

            NSUrl originalURL = downloadTask.OriginalRequest.Url;
			NSUrl destinationURL = documentsDictionry.Append("image1.png", false);
			NSError removeCopy;
			NSError errorCopy;

			fileManager.Remove(destinationURL, out removeCopy);
			bool success = fileManager.Copy(location, destinationURL, out errorCopy);

			if (success)
			{
				// we do not need to be on the main/UI thread to load the UIImage
				UIImage image = UIImage.FromFile(destinationURL.Path);
				InvokeOnMainThread(() => {
                    Console.WriteLine("Image downloaded successfully.");

					var screen = UIScreen.MainScreen.Bounds;
					
                    UIImageView imageView = new UIImageView();
                    imageView.Image = image;
					imageView.Frame = new CGRect(0, 0, screen.Size.Width, screen.Size.Height);

					controller.Add(imageView);

					Console.WriteLine("Added image to ImageView");
				});
			}
			else
			{
				Console.WriteLine("Error during the copy: {0}", errorCopy.LocalizedDescription);
			}
		}

		public void DidCompleteWithError(NSUrlSession session, NSUrlSessionTask task, NSError error)
		{
			Console.WriteLine("DidComplete");
			if (error == null)
				Console.WriteLine("Task: {0} completed successfully", task);
			else
				Console.WriteLine("Task: {0} completed with error: {1}", task, error.LocalizedDescription);

			//float progress = task.BytesReceived / (float)task.BytesExpectedToReceive;
			//InvokeOnMainThread(() => {
			//	controller.ProgressView.Progress = progress;
			//});

			//controller.downloadTask = null;
		}

		public void DidResume(NSUrlSession session, NSUrlSessionDownloadTask downloadTask, long resumeFileOffset, long expectedTotalBytes)
		{
			Console.WriteLine("DidResume");
		}

		public void DidFinishEventsForBackgroundSession(NSUrlSession session)
		{
			using (AppDelegate appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate)
			{
				var handler = appDelegate.BackgroundSessionCompletionHandler;
				if (handler != null)
				{
					appDelegate.BackgroundSessionCompletionHandler = null;
					handler();
				}
			}

			Console.WriteLine("All tasks are finished");
		}
	}
}
