using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepAssignmentFolder
{
    /// <summary>
    /// Class to hold data on submissions
    /// </summary>
    class Submission
    {
        // Property to hold original file name
        private string _originalFilePath;

        // Accessor for file name
        public string FileName
        {
            get { return Path.GetFileName(_originalFilePath); }
        }

        // Accessor for student name
        public string StudentName
        {
            get { return ExtractStudentName(this.FileName); }
        }

        // Accessor for submission date and time
        public DateTime SubmissionDateTime
        {
            get { return ExtractSubmissionDateTime(this.FileName); }
        }

        // Accessor for file extension
        public string FileExtension
        {
            get { return Path.GetExtension(this._originalFilePath); }
        }

        // Accessor for original file path
        public string FilePath
        {
            get { return this._originalFilePath; }
        }

        /// <summary>
        /// Constructor for submission object
        /// </summary>
        /// <param name="path">File path of submission</param>
        public Submission(string path)
        {
            this._originalFilePath = path;
        }

        /// <summary>
        /// Extracts student name from submission file name
        /// </summary>
        /// <param name="fileString">filename to be processed$</param>
        /// <returns>Student name as string</returns>
        public static string ExtractStudentName(string fileString)
        {
            return fileString.Split(new string[] { " - " }, StringSplitOptions.None)[1].Trim();
        }

        /// <summary>
        /// Extracts submission date and time from submission file name
        /// </summary>
        /// <param name="fileString">filename to be processed$</param>
        /// <returns>Submission date and time as DateTime</returns>
        public static DateTime ExtractSubmissionDateTime(string fileString)
        {
            string dateTimeString = fileString.Split(new string[] { " - " }, StringSplitOptions.None)[2].Trim();
            dateTimeString = dateTimeString.Insert(dateTimeString.Length - 5, ":");
            return DateTime.Parse(dateTimeString);
        }

    }
}
