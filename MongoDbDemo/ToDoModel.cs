using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MongoDbDemo
{
    partial class Program
    {
        public class ToDoModel
        {
            public string Table = "ToDoTable";
            [BsonId]
            public Guid Id { get; set; }
            public string Date { get; set; }
            public string Label { get; set; }
            public string Description { get; set; }

        }
    }
}
