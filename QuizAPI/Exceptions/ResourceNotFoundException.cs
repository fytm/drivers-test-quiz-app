namespace QuizAPI.Exceptions
{
    public class ResourceNotFoundException : ApplicationException
    {
        public ResourceNotFoundException(string resourceId) : base($"Resource with id {resourceId} not found"){
        }
    }
}
