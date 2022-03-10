using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ConsoleJournal
{
    public class InOut
    {
        public static object LoadFromXMLFile(object obj, string path)
        {
            if (File.Exists(path) && new FileInfo(path).Extension == ".xml")
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(path);
                var nodes = xdoc.FirstChild.ChildNodes;

                foreach (XmlNode node in nodes)
                {
                    obj = AddParsedProp(obj, node.Name, node.InnerText);
                }

                return obj;
            }
            else return null;
        }

        public static void SavetoXMLFile(object obj, string path)
        {
            XDocument doc = new XDocument();
            doc.Declaration = new XDeclaration("1.0", Encoding.UTF8.HeaderName, String.Empty);

            var elements = new List<XElement>();
            var props = obj.GetType().GetProperties();

            foreach (var prop in props) elements.Add(new XElement(prop.Name, prop.GetValue(obj)));

            string objName = obj.GetType().Name;
            doc.Add(new XElement(objName, elements));
            doc.Save(path);
        }

        private static object AddParsedProp(object obj, string propname, string value)
        {
            var prop = obj.GetType().GetProperty(propname);
            object newvalue = ConvertType(value, prop.PropertyType);
            prop.SetValue(obj, newvalue);

            return obj;
        }

        public static object ConvertType(string value, Type type)
        {
            if (type.IsEnum) return Enum.Parse(type, value);
            else return Convert.ChangeType(value, type);
        }

        public static string[] GetQuestions()
        {
            return File.ReadAllLines("./System/Data/questions.txt");
        }
    }
}
