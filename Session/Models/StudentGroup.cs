using ORM;
using System;

namespace Session.Models
{
    public class StudentGroup : ModelBase
    {
        private string groupName;

        public string GroupName
        {
            get => groupName;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Name value can't be null or empty");
                }

                groupName = value;
                OnPropertyChanged(nameof(GroupName));
            }
        }

        public int TransitionYear { get; set; }

        public StudentGroup() { }

        public StudentGroup(string name)
        {
            GroupName = name;
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is StudentGroup)
            {
                StudentGroup studentGroup = obj as StudentGroup;

                if (GroupName == studentGroup.GroupName)
                {
                    return true;
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            return groupName.GetHashCode();
        }

        public override string ToString()
        {
            return $"{nameof(Credit)} " +
                $"{nameof(Id)}={Id} " +
                $"{nameof(GroupName)}={GroupName}";
        }
    }
}
