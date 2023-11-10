namespace MyApp
{
    public class QuestionData
    {
        public String Question;
        public HashSet<string> Options;
        private string Answer;


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

        public HashSet<string> getOptions()
        {
            return this.Options;
        }

    }
}
