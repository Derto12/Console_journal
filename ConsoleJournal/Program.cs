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
        static Screen currentScreen = Screen.Main;
        static Journal journal = new Journal();

        public static void Initialize()
        {
            object infosObj = InOut.LoadFromXMLFile(journal, "./System/Journal.xml");
            if (infosObj != null) journal = (Journal) infosObj;
        }
        static void Main(string[] args)
        {
            Initialize();

            bool exit = false;
            while (!exit)
            {
                currentScreen = currentScreen switch
                {
                    Screen.Main => UI.MainMenu(journal.Greeting()),
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
