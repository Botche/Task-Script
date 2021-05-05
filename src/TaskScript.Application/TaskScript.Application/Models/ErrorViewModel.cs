namespace TaskScript.Application.Models
{
    public class ErrorViewModel
    {
        public int ErrorCode;

        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
