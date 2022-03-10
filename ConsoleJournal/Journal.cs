using System;

namespace ConsoleJournal {
    public class Journal 
    {
        public string Username { get; set; }
        public DateTime QuestionRefresh { get; set; } = DateTime.MinValue;

        private string _todaysQuestion = "";
        public string TodaysQuestion
        {
            get {
                return _todaysQuestion;
            }
            set {
                if (value == "" || (DateTime.Now - QuestionRefresh).TotalDays >= 1)
                {
                    _todaysQuestion = GetRandomQuestion();
                    QuestionRefresh = DateTime.Now;

                    InOut.SavetoXMLFile(this, "./System/Config/Journal.xml");
                }
                else _todaysQuestion = value;
            }
        }

        public Journal()
        {
            Username = "";
            TodaysQuestion = "";
        }

        private string GetRandomQuestion()
        {
            Random random = new Random();
            var questions = InOut.GetQuestions();

            return questions[random.Next(0, questions.Length)];
        }
        public string Greeting()
        {
            string greeting = "";

            DateTime dateTime = DateTime.Now;
            if (dateTime.Hour >= 5 && dateTime.Hour <= 12) greeting = "Good morning";
            else if (dateTime.Hour > 12 && dateTime.Hour < 18) greeting = "Good afternoon";
            else greeting = "Good evening";

            greeting += string.Format(", {0}!\n{1}", Username != "" ?
                Username : "diarist", TodaysQuestion);

            return greeting;
        }


    }
}
