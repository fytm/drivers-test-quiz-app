namespace MyApp
{
    public class QuestionApp
    {
        static void showFormattedOptions(HashSet<string> Options)
        {
            foreach (var option in Options)
            {
                Console.Write($"{option.ToString()} |");
            }
            Console.WriteLine();
        }
        public static void AskQuestions(List<QuestionData> AllQuestions)
        {
            int randomNumber = Random.Shared.Next();
            int numQuestions = AllQuestions.Count;
            int MyQuestionIndex;
            while (true)
            {
                if (numQuestions <= 0)
                {
                    Console.WriteLine("Out of questions. Exiting.....");
                    break;
                }
                MyQuestionIndex = randomNumber % numQuestions;
                QuestionData question = AllQuestions[MyQuestionIndex];
                Console.WriteLine(question.Question.ToString());
                showFormattedOptions(question.getOptions());
                Console.WriteLine("Press q to exit or anything else to continue");
                if (Console.ReadLine() == "q")
                {
                    break;
                }
                AllQuestions.Remove(question);
                numQuestions -= 1;
            }
        }
    }

}
