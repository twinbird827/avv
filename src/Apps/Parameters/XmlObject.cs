using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Avv.Apps.Parameters
{
    public class XmlObject<T>
    {
        private string FilePath { get; set; }

        public T ManagedObject { get; private set; }

        public XmlObject(string filePath)
        {
            FilePath = filePath;
        }

        public void Save()
        {
            var se = new XmlSerializer(typeof(T));
            using (var sw = new StreamWriter(FilePath, false, new System.Text.UTF8Encoding(false)))
            {
                se.Serialize(sw, ManagedObject);
            }
        }

        public void Load()
        {
            if (!File.Exists(FilePath))
            {
                ManagedObject = (T)Activator.CreateInstance(typeof(T));
                return;
            }

            var se = new XmlSerializer(typeof(T));
            using (var sr = new StreamReader(FilePath, new System.Text.UTF8Encoding(false)))
            {
                ManagedObject = (T)se.Deserialize(sr);
            }
        }
    }
}
