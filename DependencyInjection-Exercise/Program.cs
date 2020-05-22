using System;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection_Exercise
{
    public class Program
    {
        static void Main(string[] args)
        {
            var collection = new ServiceCollection();
            collection.AddScoped<IDataAccess, DataAccess>();
            collection.AddScoped<IBusiness, Business>();

            var provider = collection.BuildServiceProvider();
            IBusiness biz = provider.GetService<IBusiness>();

            var userInterface = new UserInterface(biz);

        }

    }
    public class UserInterface
    {
        private readonly IBusiness _business;
        public UserInterface(IBusiness business)
        {
            _business = business;
        }
        public void GetData()
        {
            Console.WriteLine("Enter your Username");
            var username = Console.ReadLine();

            Console.WriteLine("Enter your Password");
            var password = Console.ReadLine();

            _business.Signup(username, password);

        }
    }

    public class Business : IBusiness
    {
        private readonly IDataAccess _dataAccess;
        public Business(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public void Signup(string username, string password)
        {
            _dataAccess.Store(username, password);
        }
    }

    public class Business2 : IBusiness
    {
        public void Signup(string username, string password)
        {
            var dataAccess = new DataAccess();
            dataAccess.Store(username, password);
        }
    }

    public class DataAccess : IDataAccess
    {
        public void Store(string user, string password)
        {
            // DB
        }
    }

    public interface IBusiness
    {
        void Signup(string username, string password);
    }
    public interface IDataAccess
    {
        void Store(string user, string password);
    }
}
