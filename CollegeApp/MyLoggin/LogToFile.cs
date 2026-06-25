namespace CollegeApp.MyLoggin
{
    public class LogToFile : IMyLogger
    {
        public void Log(string message)
        {
            // Code to log the message to a file
            Console.WriteLine($"Logging to file: {message}");
            Console.WriteLine("LogtoFile");
        }
    }
}
