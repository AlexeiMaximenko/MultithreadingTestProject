using System.IO;
using System.Text;

namespace JobProject
    {
    public class Log
        {
        private string path;

        public Log(string path)
            {
            this.path = path;
            }

        public void Logging(string logSave)
            {
            var streamwriter = new StreamWriter(path, true, Encoding.UTF8);
            streamwriter.WriteLine(logSave);
            streamwriter.Close();
            }
        }
    }