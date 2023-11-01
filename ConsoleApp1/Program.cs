using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.ConstrainedExecution;

internal class Program
{

    private static List<QuestionData> AllQuestions = new List<QuestionData>();
    private static string csvFilePath = "C:\\Users\\felix.tamakloe\\Desktop\\Spikes\\C#\\ConsoleApps\\question_data.csv";

    private static void AskQuestions()
    {
        int randomNumber = Random.Shared.Next();
        int numQuestions = AllQuestions.Count;
        Boolean isPractising = true;
        while (isPractising)
        {
            if (numQuestions == 0 )
            {
                Console.WriteLine("Out of questions. Exiting.....");
                break;
            }
            int MyQuestionIndex = randomNumber % numQuestions;
            QuestionData question = AllQuestions[MyQuestionIndex];
            Console.WriteLine(question.Question.ToString());
            question.showOptions();
            AllQuestions.Remove(question);
            //myQuestions.RemoveAt(MyQuestionIndex);
            Console.WriteLine("Press q to exit or anything else to continue");
            if (Console.ReadLine() == "q")
            {
                isPractising = false;
            }
            numQuestions = numQuestions-1;
        }
    }

    private static void LoadQuestions()
    {
        // Create a configuration for CsvHelper..
        var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ",",
        };
        // Read the CSV file.
        using (var reader = new StreamReader(csvFilePath))
        using (var csv = new CsvReader(reader, csvConfig))
        {
            IEnumerable<QuestionData> records = csv.GetRecords<QuestionData>();
            Console.WriteLine("Reading questions into app");
            foreach (var record in records)
            {
                AllQuestions.Add(record);
            }
        }
    }



    private static void Main(string[] args)
    {
        Console.WriteLine("This application helps you prepare for the drivers test by asking you a few practice questions!");
        LoadQuestions();
        AskQuestions();
    }

    public class QuestionData
    {
        public String Question { get; set; }
        public HashSet<string> Options { get; set; }
        private string Answer { get; set; }


        public QuestionData(string Question, string Options, string Answer)
        {
            this.Question = Question;
            this.Options = new HashSet<string>();
            foreach (var option in Options.Split(",")) 
                {
                this.Options.Add(option);
                    }
            this.Options.Add(Answer);
            this.Answer = Answer;
        }

        public string getAnswer()
        {
            return this.Answer;
        }
        public void showOptions()
        {
            foreach (var option in this.Options){
                Console.Write($"{option.ToString()} |");
            }
            Console.WriteLine() ;
        }
    }

}





