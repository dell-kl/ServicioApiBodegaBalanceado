namespace Utility.Exceptions
{
    public class OperationAbortExceptions : OperationCanceledException
    {
        public string message = "";

        public OperationAbortExceptions()
        {
        }

        public OperationAbortExceptions(string message)
        {
            this.message = message;
        }
    }
}
