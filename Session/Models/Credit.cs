using ORM;

namespace Session.Models
{
    public class Credit : ModelBase
    {
        private int knowledgeControlId;
        private int studentId;
        private bool isPassed;

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
        public bool IsPassed 
        {
            get => isPassed;
            set
            {
                isPassed = value;
                OnPropertyChanged(nameof(IsPassed));
            }
        }

        public Credit() { }

        public Credit(int knowledgeControlId, int studentId, bool isPassed)
        {
            KnowledgeControlId = knowledgeControlId;
            StudentId = studentId;
            IsPassed = isPassed;
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is Credit)
            {
                Credit credit = obj as Credit;

                if (KnowledgeControlId == credit.KnowledgeControlId &&
                    StudentId == credit.StudentId)
                {
                    return true;
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            return (KnowledgeControlId.GetHashCode() << 2) ^ StudentId.GetHashCode();
        }

        public override string ToString()
        {
            return $"{nameof(Credit)} " +
                $"{nameof(Id)}={Id} " +
                $"{nameof(KnowledgeControlId)}={KnowledgeControlId} " +
                $"{nameof(StudentId)}={StudentId} " +
                $"{nameof(IsPassed)}={IsPassed}";
        }
    }
}
