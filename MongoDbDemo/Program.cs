using System;
using System.Collections.Generic;
using System.Linq;

namespace MongoDbDemo
{
    partial class Program
    {
        static void Main(string[] args)
        {
            ToDoModel todo = new ToDoModel();
            Helpper help = new Helpper();
            MongoCRUD mydb = new MongoCRUD("MyMongoDb");

            bool loop = true;
            while (loop)
            {
                help.PrintSelect();
                string numString = Console.ReadLine();
                if (
                    numString != "0" & 
                    numString != "1" & 
                    numString != "2" & 
                    numString != "3" & 
                    numString != "4" &
                    numString != "5"
                    )
                {
                    numString = "0";
                }
                int caseSwitch = Convert.ToInt32(numString);
                if (caseSwitch == 0)
                {
                    loop = false;
                }
                switch (caseSwitch)
                {
                    case 1:

                        Console.Write("     Add Todo: ");
                        todo.Label = Console.ReadLine();
                        Console.Write("     Add Description: ");
                        todo.Description = Console.ReadLine();
                        todo.Date = DateTime.Now.ToString("yyyy.MM.dd ddd HH:mm");
                        Console.WriteLine();
                        Console.WriteLine($"Tabel {todo.Table}");
                        Console.WriteLine($"    Added todo: {todo.Label} | Time: {todo.Date}");
                        Console.WriteLine($"    Description: {todo.Description}");
                        Console.Write("     This data will be recorded (y/n): ");
                        var i = Console.ReadLine();
                        if (i == "y")
                        {
                            mydb.InsertRecord(todo.Table,
                            new ToDoModel
                            {
                                Date = todo.Date,
                                Label = todo.Label,
                                Description = todo.Description
                            });
                        }
                        else
                        {
                            Console.WriteLine("No record");
                        }
                        help.DrawLine();
                        Console.WriteLine($"    Tabel: {todo.Table}");
                        Console.WriteLine($"    Time: {todo.Date}");
                        Console.WriteLine($"    Added todo: {todo.Label}");
                        Console.WriteLine($"    Description: {todo.Description}");
                        break;
                    case 2:
                        help.DrawLine();
                        Console.WriteLine($"     Table: {todo.Table} ");
                        
                        var recs = mydb.LoadRecords<ToDoModel>(todo.Table);
                        Console.Write("     Include ID and time?(y/n): ");
                        string inc = Console.ReadLine();
                        if (inc == "y")
                        {
                            foreach (var r in recs)
                            {
                                help.DrawLine();
                                Console.WriteLine($"    Id: {r.Id}");
                                Console.WriteLine($"    Date: {r.Date}");
                                Console.WriteLine($"    ToDo: {r.Label}");
                                Console.WriteLine($"    Description: {r.Description}");
                            }
                        }
                        else
                        {
                            foreach (var r in recs)
                            {
                                help.DrawLine();
                                Console.WriteLine($"    ToDo: {r.Label}");
                                Console.WriteLine($"    Description: {r.Description}");
                            }
                        }
                        help.DrawLine();
                        Console.WriteLine();
                        help.BacKToMenu();
                        break;
                    case 3:
                        Console.Write("     Search label: ");
                        todo.Label = Console.ReadLine();
                        var recfiltered = mydb.LoadRecordByLabel<ToDoModel>(todo.Table,"Label",todo.Label);
                        foreach (var r in recfiltered)
                        {
                            help.DrawLine();
                            Console.WriteLine($"    Id: {r.Id}");
                            Console.WriteLine($"    Date: {r.Date}");
                            Console.WriteLine($"    ToDo: {r.Label}");
                            Console.WriteLine($"    Description: {r.Description}");
                        }
                        help.DrawLine();
                        Console.WriteLine();
                        help.BacKToMenu();
                        break;
                    case 4:
                        Console.Write("     Search id: ");
                        var guid = Console.ReadLine();
                        todo.Id = new Guid(guid);
                        var loadid = mydb.LoadRecordById<ToDoModel>(todo.Table, todo.Id);

                        help.DrawLine();
                        Console.WriteLine($"    Id: {loadid.Id}");
                        Console.WriteLine($"    Date: {loadid.Date}");
                        Console.WriteLine($"    ToDo: {loadid.Label}");
                        Console.WriteLine($"    ToDo: {loadid.Description}");
                        help.DrawLine();
                        Console.WriteLine("     Select What to do...");
                        Console.WriteLine("     1. Upsert(update or insert)");
                        Console.WriteLine("     2. Delete Record");
                        Console.Write("     Your selection: ");
                        string select = Console.ReadLine();
                        if (select == "1")
                        {
                            Console.Write("     Edit label?(y/n): ");
                            string subselect = Console.ReadLine();
                            if (subselect == "y")
                            {
                                Console.Write("     Edit Todo: ");
                                loadid.Label = Console.ReadLine();
                            }
                            Console.Write("     Edit Description: ");
                            todo.Description = Console.ReadLine();
                            todo.Date = DateTime.Now.ToString("yyyy.MM.dd ddd HH:mm");
                            Console.Write("     Are you sure?(y/n): ");
                            string s = "";
                            s = Console.ReadLine();
                            if (s == "y")
                            {
                                mydb.UpsertRecord(todo.Table, todo.Id,
                                    new ToDoModel
                                    {
                                        Id = loadid.Id,
                                        Date = todo.Date,
                                        Label = loadid.Label,
                                        Description = todo.Description
                                    });
                            }
                        }
                        else if (select == "2")
                        {
                            Console.Write("     Are you sure?(y/n): ");
                            string t = Console.ReadLine();
                            if (t == "y")
                            {
                                mydb.DeleteRecord<ToDoModel>(todo.Table, todo.Id);
                            } 
                        }
                        else
                        {
                            Console.WriteLine("     No actions...");
                        }
                        help.BacKToMenu();
                        break;
                    case 5:
                        List<ToDoModel> RList;
                        List<ToDoModel> RListSort;
                        RList = mydb.LoadRecords<ToDoModel>(todo.Table);
                        Console.Write("     Search for content: ");
                        string searchterm = Console.ReadLine();
                        RListSort = RList.Where(
                            s => s.Description
                            .Contains(searchterm, 
                            StringComparison.OrdinalIgnoreCase
                            )).ToList();
                        foreach (var item in RListSort)
                        {
                            help.DrawLine();
                            Console.WriteLine($"    Id: {item.Id}");
                            Console.WriteLine($"    Date: {item.Date}");
                            Console.WriteLine($"    ToDo: {item.Label}");
                            Console.WriteLine($"    Description: {item.Description}");
                        }
                        help.DrawLine();
                        help.BacKToMenu();
                        break;
                    case 0:
                        break;

                }
            }
            Console.WriteLine("    Press any key...");
            Console.ReadKey();
        }
    }
}
