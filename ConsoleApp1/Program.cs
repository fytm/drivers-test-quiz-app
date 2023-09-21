using static System.Net.Mime.MediaTypeNames;

internal class Program
{
    record class Question
    {
        public String text{ get; }
        private List<string> options;
        private string answer;


        public Question(string text, List<string> options, int ans_pos)
        {
            this.text = text;
            this.options = options;
            this.answer = options[ans_pos];
        }
    }


    //public static void play()
    //{
    //    console.writeline("hello")

    //}
    private static void AskQuestions(List<Question> myQuestions)
    {
        int randomNumber = Random.Shared.Next();
        int numQuestions = myQuestions.Count;
        Boolean isPractising = true;
        while (isPractising)
        {
            Question question = myQuestions[randomNumber % numQuestions];
            question.ToString();
            Console.WriteLine(question.ToString());
            Console.WriteLine("Press q to exit or anything else to continue");
            if (Console.ReadLine() == "q")
            {
                isPractising = false;
            }
        }
    }

    private static void Main(string[] args)
    {
        List<Question> myQuestions = new List<Question>();
        List<string> options = new List<string>();
        options.Add("Stop");
        options.Add("Go");
        myQuestions.Add(new Question("What does a red light mean?", options, 0));
        Console.WriteLine("This application helps you prepare for the drivers test by asking you a few practice questions!");
        AskQuestions(myQuestions);
    }
   
}





