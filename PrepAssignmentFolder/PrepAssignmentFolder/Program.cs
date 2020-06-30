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
            bool waitForKey = true;
            bool processInput = true;
            string path = "";

            while (processInput)
            {
                string input;

                Console.WriteLine("Please enter a directory to process:");
                input = Console.ReadLine().Trim();

                if (input != "")
                {
                    path = input;
                    processInput = false;
                }
            }
            

            if (Directory.Exists(path))
            {
                string[] files;
                List<Submission> submissions = new List<Submission>();

                if (File.Exists(path + @"\index.html"))
                {
                    File.Delete(path + @"\index.html");
                }

                files = Directory.GetFiles(path);

                for (int fileCount = 0; fileCount < files.Length; fileCount++)
                {
                    Submission newSubmission = new Submission(files[fileCount]);

                    if (submissions.Count > 0 && submissions[submissions.Count - 1].StudentName == newSubmission.StudentName)
                    {
                        if (submissions[submissions.Count - 1].SubmissionDateTime < newSubmission.SubmissionDateTime)
                        {
                            File.Delete(submissions[submissions.Count - 1].FilePath);
                            submissions[submissions.Count - 1] = newSubmission;
                        }
                        else
                        {
                            File.Delete(newSubmission.FilePath);
                        }
                    }
                    else
                    {
                        submissions.Add(newSubmission);
                    }
                }

                

                foreach (Submission submission in submissions)
                {
                    using (ArchiveFile archiveFile = new ArchiveFile(submission.FilePath))
                    {
                        Console.WriteLine("Extracting " + submission.FileName);
                        archiveFile.Extract(path + @"\" + submission.StudentName); // extract all
                    }
                    File.Delete(submission.FilePath);
                }

               Console.WriteLine("Done");
            }
            else
            {
                Console.WriteLine("Directory: " + path + " does not exist");
                waitForKey = true;
            }

            if (waitForKey) Console.ReadKey();

        }
    }
}
