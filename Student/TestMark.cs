using System;

namespace Student
{
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

        public override string ToString()
        {
            return $"{test} Mark: {Mark}";
        }
    }
}
