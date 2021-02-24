/*
 * Main program functionality to handle proceessing of student file submissions
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SevenZipExtractor;

namespace PrepAssignmentFolder
{
    class Program
    {
        static void Main(string[] args)
        {
            bool waitForKey = true;     // If we need to wait for a keypress, used to end program
            bool processInput = true;   // If we have a valid directory to process input
            string path = "";           // Path of directory to process, where zipped student submissions are

            // Ask user for directory to process, keep asking until value provided
            while (processInput)
            {
                string input;

                Console.WriteLine("Please enter a directory to process:");
                input = Console.ReadLine().Trim();

                if (input != "") // Path provided
                {
                    path = input;   // update out path
                    processInput = false;  // Exit the loop
                }
            }
            
            // If the user entered a valid path
            if (Directory.Exists(path))
            {
                string[] files; // Array to hold submission file paths from user provided directory
                List<Submission> submissions = new List<Submission>(); // List to hold submission objects that contain info on student submissions

                if (File.Exists(path + @"\index.html")) // Check to see if D2L gave us an index file
                {
                    File.Delete(path + @"\index.html"); // Delete the D2L index file
                }

                files = Directory.GetFiles(path); // Get all the files from the provided submission directory

                for (int fileCount = 0; fileCount < files.Length; fileCount++) // Loop through all the files found in the folder
                {
                    Submission newSubmission = new Submission(files[fileCount]); // Create a new submission object for the submission zip

                    if (submissions.Count > 0 && submissions[submissions.Count - 1].StudentName == newSubmission.StudentName) // Check to see if the user has submitted a file before this one
                    {
                        if (submissions[submissions.Count - 1].SubmissionDateTime < newSubmission.SubmissionDateTime) // Confirm that this file is newer than the previous submission
                        {
                            File.Delete(submissions[submissions.Count - 1].FilePath); // Delete the older submission
                            submissions[submissions.Count - 1] = newSubmission; // Replace that submission in the list with the latest submission found
                        }
                        else // The original file was newer
                        {
                            File.Delete(newSubmission.FilePath); // delete the older of the two submissions
                        }
                    }
                    else // No multiple submissions found
                    {
                        submissions.Add(newSubmission); // add the submission to the list
                    }
                }

                

                foreach (Submission submission in submissions) // Loop through the cleaned list of submissions
                {
                    using (ArchiveFile archiveFile = new ArchiveFile(submission.FilePath)) // Load the zip of the submission for handling
                    {
                        Console.WriteLine("Extracting " + submission.FileName);
                        archiveFile.Extract(path + @"\" + submission.StudentName); // extract files to folder named after student
                    }
                    File.Delete(submission.FilePath); // Delete the original zip file
                }

               Console.WriteLine("Done");
            }
            else // User entered an invalid path
            {
                Console.WriteLine("Directory: " + path + " does not exist");
                waitForKey = true; // Wait for keypress so user can see error
            }

            if (waitForKey) Console.ReadKey(); // Used to keep console open so user can see errors

        }
    }
}
