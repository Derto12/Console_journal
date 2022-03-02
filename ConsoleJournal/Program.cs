using Figgle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleJournal
{
    class Program
    {
        static Screen currentScreen = Screen.Main;

        static void MainMenu()
        {
            Console.Clear();

            var banner = $"{FiggleFonts.Standard.Render("Journal")}{FiggleFonts.Standard.Render("is Life!")}";
            InOut.PrintLinesAt(banner, new Location(0, 5), InOut.TextAlign.Center);

            Console.SetCursorPosition(3, 21);

            string choice = Menu.SelectItem(new string[] { "New journal", "View documents", "Settings", "Exit" });
            switch(choice) 
            {
                case "New journal":
                    //currentScreen = Screen.New;
                    break;
                case "View documents":
                    //currentScreen = Screen.Docs;
                    break;
                case "Settings":
                    //currentScreen = Screen.Settings;
                    break;
                case "Exit":
                    currentScreen = Screen.None;
                    break;
            }
        }

        static void NewMenu()
        {

        }

        static void DocsMenu()
        {

        }

        static void SettingsMenu()
        {

        }

        static void Initialize()
        {
            
        }


        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                switch (currentScreen)
                {
                    case Screen.Main:
                        MainMenu();
                        break;
                    case Screen.New:
                        NewMenu();
                        break;
                    case Screen.Docs:
                        DocsMenu();
                        break;
                    case Screen.Settings:
                        SettingsMenu();
                        break;
                    case Screen.None:
                        exit = true;
                        break;
                }
            }
        }
    }
}
