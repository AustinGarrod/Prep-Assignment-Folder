using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepAssignmentFolder
{
    class Submission
    {
        private string _originalFilePath;

        public string FileName
        {
            get { return Path.GetFileName(_originalFilePath); }
        }

        public string StudentName
        {
            get { return ExtractStudentName(this.FileName); }
        }

        public DateTime SubmissionDateTime
        {
            get { return ExtractSubmissionDateTime(this.FileName); }
        }

        public string FileExtension
        {
            get { return Path.GetExtension(this._originalFilePath); }
        }

        public string FilePath
        {
            get { return this._originalFilePath; }
        }

        public Submission(string path)
        {
            this._originalFilePath = path;
        }

        public static string ExtractStudentName(string fileString)
        {
            return fileString.Split(new string[] { " - " }, StringSplitOptions.None)[1].Trim();
        }

        public static DateTime ExtractSubmissionDateTime(string fileString)
        {
            string dateTimeString = fileString.Split(new string[] { " - " }, StringSplitOptions.None)[2].Trim();
            dateTimeString = dateTimeString.Insert(dateTimeString.Length - 5, ":");
            return DateTime.Parse(dateTimeString);
        }

    }
}
