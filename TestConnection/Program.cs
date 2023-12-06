// See https://aka.ms/new-console-template for more information
using TestConnection;

Console.WriteLine("Hello, World! " + System.DateTime.Now);
DataAccess dataAccess = new DataAccess();
dataAccess.TestConnection();