using System.Net;

namespace QuizAPI.Exceptions
{
    public class ResourceNotFoundException : HttpResponseException
    {
        public ResourceNotFoundException(string resourceId) : base(
            (int)HttpStatusCode.NotFound,
            $"Resource with id {resourceId} not found")
        {
        }
    }
}