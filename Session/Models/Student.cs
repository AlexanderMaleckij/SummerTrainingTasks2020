using ORM;
using System;

namespace Session.Models
{
    public class Student : ModelBase
    {
        private int studentGroupId;
        private string fullName;
        private char gender;
        private DateTime dateOfBirth;

        public int StudentGroupId 
        {
            get => studentGroupId;
            set
            {
                studentGroupId = value;
                OnPropertyChanged(nameof(StudentGroupId));
            }
        }
        public string FullName
        {
            get => fullName;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Full name value can't be null or empty");
                }
                if (!value.Trim().Contains(" "))
                {
                    throw new ArgumentException("Full name must contain at least first name and last name");
                }

                fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }
        public char Gender
        {
            get => gender;
            set
            {
                char lowerValue = char.ToLower(value);

                if (lowerValue != 'm' || lowerValue != 'f')
                {
                    throw new ArgumentException("Gender value can contains only 'm' or 'f' value");
                }

                gender = lowerValue;
                OnPropertyChanged(nameof(Gender));
            }
        }
        public DateTime DateOfBirth 
        {
            get => dateOfBirth;
            set
            {
                dateOfBirth = value;
                OnPropertyChanged(nameof(DateOfBirth));
            }
        }

        public Student() { }

        public Student(string fullName, int studentGroupId, char gender, DateTime dateOfBirth)
        {
            FullName = fullName;
            StudentGroupId = studentGroupId;
            Gender = gender;
            DateOfBirth = dateOfBirth;
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is Student)
            {
                Student student = obj as Student;

                if (StudentGroupId == student.StudentGroupId &&
                    FullName == student.FullName &&
                    Gender == student.Gender &&
                    DateOfBirth == student.DateOfBirth)
                {
                    return true;
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            return (StudentGroupId.GetHashCode() << 4) ^
                   (FullName.GetHashCode() << 3) ^
                   (Gender.GetHashCode() << 2) ^
                   DateOfBirth.GetHashCode();
        }

        public override string ToString()
        {
            return $"{nameof(Student)} " +
                $"{nameof(Id)}={Id} " +
                $"{nameof(StudentGroupId)}={StudentGroupId} " +
                $"{nameof(FullName)}={FullName} " +
                $"{nameof(Gender)}={Gender} " +
                $"{nameof(DateOfBirth)}={DateOfBirth}";
        }
    }
}
