using System;

public class QuestionApp
{
    public static void AskQuestions(List<QuestionData> AllQuestions)
    {
        void showFormattedOptions(HashSet<string> Options)
        {
            foreach (var option in Options)
            {
                Console.Write($"{option.ToString()} |");
            }
            Console.WriteLine();
        }

        int randomNumber = Random.Shared.Next();
        int numQuestions = AllQuestions.Count;
        Boolean isPractising = true;
        while (isPractising)
        {
            if (numQuestions == 0)
            {
                Console.WriteLine("Out of questions. Exiting.....");
                break;
            }
            int MyQuestionIndex = randomNumber % numQuestions;
            QuestionData question = AllQuestions[MyQuestionIndex];
            Console.WriteLine(question.Question.ToString());
            showFormattedOptions(question.getOptions());
            AllQuestions.Remove(question);
            //myQuestions.RemoveAt(MyQuestionIndex);
            Console.WriteLine("Press q to exit or anything else to continue");
            if (Console.ReadLine() == "q")
            {
                isPractising = false;
            }
            numQuestions = numQuestions - 1;
        }
    }
}