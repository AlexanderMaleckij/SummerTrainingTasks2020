using System;

namespace Student
{
    [Serializable]
    public class TestMark
    {
        Test test;
        int mark;

        public Test Test
        {
            get => test;
            set
            {
                if(value == null)
                {
                    throw new ArgumentException("Test can't be a null");
                }

                test = value;
            }
        }

        public int Mark 
        { 
            get => mark;
            set
            {
                if(value < 0 || value > 10)
                {
                    throw new ArgumentException("mark should be in range 0 - 10");
                }

                mark = value;
            }
        }

        public TestMark(Test test, int mark)
        {
            Test = test;
            Mark = mark;
        }

        public TestMark() { }    //parameterless constructor for deserialization

        public override string ToString()
        {
            return $"{Test} Mark: {Mark}";
        }

        public override bool Equals(object obj)
        {
            if(obj != null && obj is TestMark)
            {
                TestMark testMark = obj as TestMark;

                if(test.Equals(testMark.Test) && mark == testMark.mark)
                {
                    return true;
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Test.GetHashCode() ^ Mark.GetHashCode();
        }
    }
}
