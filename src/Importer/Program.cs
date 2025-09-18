
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore;
public class Program
{
    public static void Main(string[] args)
    {

        //configure console logging
        var serviceCollection = new ServiceCollection();

        // Add logging to the service collection and configure the console provider
        serviceCollection.AddLogging(configure =>
        {
            configure.AddConsole(); // Adds the console logger
                                    // You can also add other providers like AddDebug(), AddEventLog(), etc.
        });
        try
        {
            var connection = new SqliteConnection("Data Source=hello.db");
            serviceCollection.AddDbContext<MyDbContext>(options =>
            options.UseSqlite(connection));
            Console.WriteLine("Connected to the SQLite database!");
        }
        catch (SqliteException ex)
        {
            Console.WriteLine(ex.Message);
        }

        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Obtain an ILogger instance to start logging
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

        logger.LogInformation("This is an information message logged to the console.");
        logger.LogWarning("This is a warning message.");
        logger.LogError("This is an error message.");

        // See https://aka.ms/new-console-template for more information
        Console.WriteLine("Hello, World!");

        // Keep the console open in a console application to see logs
        // if the application exits quickly
        Console.Read();
    }

}
