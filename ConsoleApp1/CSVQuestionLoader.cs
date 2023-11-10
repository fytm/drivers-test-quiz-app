using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;


namespace MyApp
{
    class CSVQuestionLoader : QuestionLoader
    {
        private CsvConfiguration csvConfig;
        private string csvFilePath;

        public CSVQuestionLoader(string csvFilePath)
        {
            // Create a configuration for CsvHelper..
            this.csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ",",
            };
            this.csvFilePath = csvFilePath;
        }

        public List<QuestionData> LoadQuestions()
        {
            List<QuestionData> AllQuestions = new List<QuestionData>();
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
