using ORM;
using System;

namespace Session.Models
{
    public class Exam : ModelBase
    {
        private int knowledgeControlId;
        private int studentId;
        private int mark;

        public int KnowledgeControlId 
        {
            get => knowledgeControlId;
            set
            {
                knowledgeControlId = value;
                OnPropertyChanged(nameof(KnowledgeControlId));
            }
        }
        public int StudentId 
        {
            get => studentId;
            set
            {
                studentId = value;
                OnPropertyChanged(nameof(StudentId));
            }
        }
        public int Mark
        {
            get => mark;
            set
            {
                if (value < 0 || value > 10)
                {
                    throw new ArgumentException("Mark value must be in range (0 - 10)");
                }

                mark = value;
                OnPropertyChanged(nameof(Mark));
            }
        }

        public Exam() { }

        public Exam(int knowledgeControlId, int studentId, int mark)
        {
            KnowledgeControlId = knowledgeControlId;
            StudentId = studentId;
            Mark = mark;
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is Exam)
            {
                Exam exam = obj as Exam;

                if (KnowledgeControlId == exam.KnowledgeControlId &&
                    StudentId == exam.StudentId)
                {
                    return true;
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            return (KnowledgeControlId << 3) ^ (StudentId << 2) ^ Mark;
        }

        public override string ToString()
        {
            return $"{nameof(Student)} " +
                $"{nameof(Id)}={Id} " +
                $"{nameof(KnowledgeControlId)}={KnowledgeControlId} " +
                $"{nameof(StudentId)}={StudentId} " +
                $"{nameof(Mark)}={Mark}";
        }
    }
}
