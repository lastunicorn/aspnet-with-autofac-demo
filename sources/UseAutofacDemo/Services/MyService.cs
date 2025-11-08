namespace DustInTheWind.AspNetWithAutofacDemo.Services
{

    public class MyService : IMyService
    {
        public string GetMessage()
        {
            return "Hello from Autofac!";
        }
    }
}