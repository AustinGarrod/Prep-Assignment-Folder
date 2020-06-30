using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepAssignmentFolder
{
    class Submission
    {
        private string originalFileName;
        private string studentName;
        private DateTime submissionDateTime;

        public string OriginalFileName
        {
            get
            {
                return originalFileName;
            }
            set
            {
                originalFileName = value;
            }
        }

        public string StudentName
        {
            get
            {
                return studentName;
            }
            set
            {
                studentName = value;
            }
        }

        public DateTime SubmissionDateTime
        {
            get
            {
                return submissionDateTime;
            }
            set
            {
                submissionDateTime = value;
            }
        }

        public Submission(string originalFileName)
        {
            this.OriginalFileName = originalFileName;

        }

        private string ExtractStudentName(string fileString)
        {
            string studentName = "";


            return studentName;
        }

    }
}
