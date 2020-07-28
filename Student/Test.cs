using System;

namespace Student
{
    [Serializable]
    public class Test
    {
        string name;
        string Name 
        { 
            get => name;
            set
            {
                if(string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Name of test should contains at least one letter");
                }

                name = value;
            }
        }
        DateTime Date { get; set; }

        public Test(string name, DateTime dateTime)
        {
            Name = name;
            Date = dateTime;
        }

        public override string ToString()
        {
            return $"Test: \"{Name}\" held on {Date}";
        }

        public override bool Equals(object obj)
        {
            if(obj != null && obj is Test)
            {
                Test test = obj as Test;

                if(name == test.name && Date == test.Date)
                {
                    return true;
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            return name.GetHashCode() ^ Date.GetHashCode();
        }
    }
}
