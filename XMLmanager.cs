using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace monogame_assignment
{
    public class XMLmanager<T>
    {
        public Type type { get; set; }

        public XMLmanager()
        {
            type = typeof(T);
        }

        public T Load(string Path)
        {
            T instance;
            using(TextReader reader = new StreamReader(Path))
            {
                XmlSerializer xml = new XmlSerializer(type);
                instance = (T)xml.Deserialize(reader);
            }
            return instance;
        }

        public void Save(string Path, object obj)
        {
            using(TextWriter writer = new StreamWriter(Path))
            {
                XmlSerializer xml = new XmlSerializer(type);
                xml.Serialize(writer, obj);
            }
        }
    }
}
