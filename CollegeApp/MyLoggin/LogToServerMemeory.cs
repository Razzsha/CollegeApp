namespace CollegeApp.MyLoggin
{
    public class LogToServerMemeory : IMyLogger
    {
        public void Log(string message)
        {
            // Code to log the message to server memory
            Console.WriteLine($"Logging to server memory: {message}");
            Console.WriteLine("LogtoServerMemeory");
        }
    }
}
