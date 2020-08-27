using ORM;
using System;

namespace Session.Models
{
    public class KnowledgeControl : ModelBase
    {
        private int semester;
        private string subjectName;
        private DateTime passDate;
        private int studentGroupId;

        public int Semester
        {
            get => semester;
            set
            {
                if (value != 1 || value != 2)
                {
                    throw new ArgumentException($"{Semester} value must be 1 or 2");
                }

                semester = value;
                OnPropertyChanged(nameof(Semester));
            }
        }
        public string SubjectName
        {
            get => subjectName;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Subject name can't be null or empty");
                }

                subjectName = value;
                OnPropertyChanged(nameof(SubjectName));
            }
        }
        public DateTime PassDate 
        {
            get => passDate;
            set
            {
                passDate = value;
                OnPropertyChanged(nameof(PassDate));
            }
        }
        public int StudentGroupId 
        {
            get => studentGroupId;
            set
            {
                studentGroupId = value;
                OnPropertyChanged(nameof(StudentGroupId));
            } 
        }

        public KnowledgeControl() { }

        public KnowledgeControl(int studentGroupId, string subjectName, DateTime passDate, int term)
        {
            StudentGroupId = studentGroupId;
            SubjectName = subjectName;
            PassDate = passDate;
            Semester = term;
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is KnowledgeControl)
            {
                KnowledgeControl knowledgeControl = obj as KnowledgeControl;

                if (Semester == knowledgeControl.Semester &&
                    SubjectName == knowledgeControl.SubjectName &&
                    PassDate == knowledgeControl.PassDate &&
                    StudentGroupId == knowledgeControl.StudentGroupId)
                {
                    return true;
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            return (Semester.GetHashCode() << 4) ^
                   (SubjectName.GetHashCode() << 3) ^
                   (PassDate.GetHashCode() << 2) ^
                   StudentGroupId.GetHashCode();
        }

        public override string ToString()
        {
            return $"{nameof(Student)} " +
                $"{nameof(Id)}={Id} " +
                $"{nameof(Semester)}={Semester} " +
                $"{nameof(SubjectName)}={SubjectName} " +
                $"{nameof(PassDate)}={PassDate} " +
                $"{nameof(StudentGroupId)}={StudentGroupId}";
        }
    }
}
