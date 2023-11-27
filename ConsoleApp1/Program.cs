using MyApp;
using Microsoft.Extensions.Configuration;

internal class Program
{


    private static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        string ?csvFilePath = configuration.GetSection("Source").GetSection("CSV").Value;
        if (csvFilePath == null )
        {
            Console.WriteLine("Unable to load Questions. Exiting...");
            return;
        }
        Console.WriteLine("This application helps you prepare for the drivers test by asking you a few practice questions!");
        QuestionLoader questionLoader = new CSVQuestionLoader(csvFilePath);
        List<QuestionData> AllQuestions = questionLoader.LoadQuestions();
        QuestionApp.AskQuestions(AllQuestions);
    }
}