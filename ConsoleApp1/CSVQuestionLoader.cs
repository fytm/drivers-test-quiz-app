using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;


namespace MyApp
{
    class CSVQuestionLoader : QuestionLoader
    {
        private CsvConfiguration csvConfig;
        private string csvFilePath;
        private List<QuestionData> AllQuestions;

        public CSVQuestionLoader(string csvFilePath)
        {
            // Create a configuration for CsvHelper..
            this.csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ",",
            };
            this.csvFilePath = csvFilePath;
            this.AllQuestions = new List<QuestionData>();
        }

        public List<QuestionData> LoadQuestions()
        {
            // Read the CSV file.
            Console.WriteLine("Reading questions into app");
            using (var reader = new StreamReader(csvFilePath))
            using (var csv = new CsvReader(reader, csvConfig))
            {
                IEnumerable<QuestionData> records = csv.GetRecords<QuestionData>();
                foreach (var record in records)
                {
                    AllQuestions.Add(record);
                }
            }
            return AllQuestions;
        }
    }

}
