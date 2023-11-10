using MyApp;

internal class Program
{
    private static string csvFilePath = "C:\\Users\\felix.tamakloe\\Desktop\\Spikes\\C#\\ConsoleApps\\question_data.csv";

    private static void Main(string[] args)
    {
        Console.WriteLine("This application helps you prepare for the drivers test by asking you a few practice questions!");
        QuestionLoader questionLoader = new CSVQuestionLoader(csvFilePath);
        List<QuestionData> AllQuestions = questionLoader.();
        QuestionApp.AskQuestions(AllQuestions);
    }

}





