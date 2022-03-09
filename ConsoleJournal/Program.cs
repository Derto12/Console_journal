using Figgle;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace ConsoleJournal
{
    class Program
    {
        static Settings settings = new Settings();
        static UI menu = new UI();
        static Random random = new Random();
        static Screen currentScreen = Screen.Main;

        static string Greeting()
        {
            string greeting = "";

            DateTime dateTime = DateTime.Now;
            if (dateTime.Hour >= 5 && dateTime.Hour <= 12) greeting = "Good morning";
            else if (dateTime.Hour > 12 && dateTime.Hour < 18) greeting = "Good afternoon";
            else greeting = "Good evening";

            if(Settings.TodaysQuestion == "" || (dateTime - Settings.LastRefresh).TotalDays >= 1)
            {
                Settings.TodaysQuestion = Question();
                Settings.LastRefresh = dateTime;
                SaveConfig();
            }

            greeting += string.Format(", {0}!\n{1}", Settings.Username != "" ?
                Settings.Username : "diarist", Settings.TodaysQuestion);

            return greeting;
        }

        static string Question()
        {
            var questions = File.ReadAllLines("./System/Data/questions.txt");

            return questions[random.Next(0, questions.Length)];
        }

        static void Initialize()
        {
            if (File.Exists("./System/Config/config.ini")) LoadConfig();
        }

        static void LoadConfig()
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load("./System/Config/config.xml");
            var nodes = xdoc.SelectSingleNode("Settings").ChildNodes;

            foreach (XmlNode node in nodes)
            {
                settings = (Settings)AddParsedProp(settings, node.Name, node.InnerText);
            }
        }

        static void SaveConfig()
        {
            XDocument doc = new XDocument();
            doc.Declaration = new XDeclaration("1.0", Encoding.UTF8.HeaderName, String.Empty);

            var elements = new List<XElement>();
            var props = typeof(Settings).GetProperties();

            foreach (var prop in props) elements.Add(new XElement(prop.Name, prop.GetValue(settings)));

            doc.Add(new XElement("Settings", elements));
            doc.Save("./System/Config/config.xml");
        }

        static object AddParsedProp(object obj, string propname, string value)
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

        static void Main(string[] args)
        {
            Initialize();

            bool exit = false;
            while (!exit)
            {
                currentScreen = currentScreen switch
                {
                    Screen.Main => UI.MainMenu(Greeting()),
                    Screen.New => UI.NewMenu(),
                    Screen.Docs => UI.DocsMenu(),
                    Screen.Settings => UI.SettingsMenu(),
                    _ => Screen.None
                };
                exit = currentScreen == Screen.None;
            }
        }
    }
}
