using System;

namespace Student
{
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
    }
}
