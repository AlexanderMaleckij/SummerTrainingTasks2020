using System;
using System.IO;

namespace Task2Loader
{
    class FileRW : IDataRW
    {
        private string fileName;

        public FileRW(string fileName)
        {
            this.fileName = fileName;
        }

        public string Read()
        {
            string content;

            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    content = sr.ReadToEnd();
                }
            }

            return content;
        }

        public void Write(string content)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(content);
                    sw.Flush();
                }
            }
        }
    }
}
