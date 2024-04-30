using System;
using System.Linq;
using Starcounter.Database;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace StarcounterConsoleSample
{
    [Database]
    public abstract class Worker
    {
        public abstract string Name { get; set; }
        public abstract string Surname { get; set; }
        public abstract int Age { get; set; }
        public abstract string Position { get; set; }
    }

    public class Program
    {
        public static void Main()
        {
            string connectionString =
                "Database=./.database/StarcounterConsoleSample;"
                + "OpenMode=CreateIfNotExists;"
                + "StartMode=StartIfNotRunning;"
                + "StopMode=IfWeStarted";

            using var services = new ServiceCollection()
                .AddStarcounter(connectionString)
                .BuildServiceProvider();

            var transactor = services.GetRequiredService<ITransactor>();

            // transactor.Transact(db =>
            // {
            //     j = db.Insert<Worker>();
            //     j.Name = "Jane";
            //     j.Surname = "Smith";
            //     j.Age = 24;
            //     j.Position = "Manager";

            //     var p = db.Insert<Worker>();
            //     p.Name = "Paul";
            //     p.Surname = "Grey";
            //     p.Age = 27;
            //     p.Position = "Programmer";

            //     var m = db.Insert<Worker>();
            //     m.Name = "Mary";
            //     m.Surname = "Peterson";
            //     m.Age = 25;
            //     m.Position = "Operator";

            //     var b = db.Insert<Worker>();
            //     b.Name = "Bob";
            //     b.Surname = "Smith";
            //     b.Age = 31;
            //     b.Position = "Programmer";
            // });

            // transactor.Transact(db =>
            // {
            //     var list = db.Sql<Worker>
            //     (
            //         "SELECT w FROM Worker w WHERE Position = ?",
            //         "Programmer"
            //     );

            //     foreach (var w in list)
            //     {
            //         Console.WriteLine(w.Name);
            //     }
            // });

            // transactor.Transact(db =>
            // {
            //     var bob = db.Sql<Worker>
            //     (
            //         "SELECT w FROM Worker w WHERE Name = ? AND Position = ?",
            //         "Bob",
            //         "Programmer"
            //     ).FirstOrDefault();

            //     if (bob != null) {
            //         bob.Position = "Lead";
            //     }

            //     var new_bob = db.Sql<Worker>
            //     (
            //         "SELECT w FROM Worker w WHERE Name = ? AND Position = ?",
            //         "Bob",
            //         "Lead"
            //     ).FirstOrDefault();

            //     if (new_bob != null) {
            //         Console.WriteLine(new_bob.Position);
            //     }
            // });

            transactor.Transact(db =>
            {
                var manager = db.Sql<Worker>
                (
                    "SELECT w FROM Worker w WHERE Position = ?",
                    "Manager"
                ).FirstOrDefault();

                Console.WriteLine(manager.Name);

                db.Sql<Worker>("DELETE FROM Worker WHERE Position = ?", "Manager");

                var new_manager = db.Sql<Worker>
                (
                    "SELECT w FROM Worker w WHERE Position = ?",
                    "Manager"
                ).FirstOrDefault();

                if (new_manager == null) {
                    Console.WriteLine("There is no managers");
                }
            });
        }
    }
}