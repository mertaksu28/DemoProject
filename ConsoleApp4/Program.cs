using System;

namespace ConsoleApp4
{   //Çıplak class kalmasın
    //Class ların erişim bildirgecinin default u internal dır.
    //Bir sınıf bağımlı olduğu başka bir sınıfı new' leyemez.
    class Program
    {
        static void Main(string[] args)
        {
            //Burada IOC Container : Aoutofac, Ninjecth gibi teknolojiler kullanılır. Bu teknolojiler ile yönetilir.
            CustomerManager customerManager = new CustomerManager(new NhCustomerDal(), new MainLoggerAdapter());
            customerManager.Save(new Customer());
        }
    }

    class CustomerDal : ICustomerDal
    {
        public void Save()
        {
            Console.WriteLine("Customer Added with Ef");
        }
    }

    class NhCustomerDal : ICustomerDal
    {
        public void Save()
        {
            Console.WriteLine("Customer Added with Nh");
        }
    }

    internal interface ICustomerDal
    {
        void Save();
    }

    class CustomerManager : ICustomerService
    {
        //Tasarım Deseni Dependency Injection
        ICustomerDal _customerDal;
        ILoggerService _loggerService;

        public CustomerManager(ICustomerDal customerDal, ILoggerService loggerService)
        {
            _customerDal = customerDal;
            _loggerService = loggerService;
        }

        public void Save(Customer customer)
        {
            _customerDal.Save();
            _loggerService.Log();
        }
    }

    internal interface ICustomerService
    {
        void Save(Customer customer);
    }

    public class Customer
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
    }

    class LogerDatabase : ILoggerService
    {
        public void Log()
        {
            Console.WriteLine("Log to db");
        }
    }
    class LogerEmail : ILoggerService
    {
        public void Log()
        {
            Console.WriteLine("Log to email");
        }
    }

    interface ILoggerService
    {
        void Log();
    }

    //Adapter Design Pattern : Bir microservisi uygulamaya dahil etmek için kullanılır

    class MainLoggerAdapter : ILoggerService
    {
        public void Log()
        {
            MainLogger mainLogger = new MainLogger();
            mainLogger.LogToMain();
        }
    }


    //Microservice
    class MainLogger
    {
        public void LogToMain()
        {
            Console.WriteLine("Logged to Main");
        }
    }

    //public enum LoggerType
    //{
    //    Database, File
    //}
    //Enum' ların amacı magic stringlerle çalışmaktır
}
