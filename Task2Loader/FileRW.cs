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

        /// <summary>
        /// Read text from file
        /// </summary>
        /// <returns>read text</returns>
        public string Read()
        {
            string content;

            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    content = sr.ReadToEnd();
                }
            }

            return content;
        }

        /// <summary>
        /// Write text to file
        /// </summary>
        /// <param name="content">text for writing</param>
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
