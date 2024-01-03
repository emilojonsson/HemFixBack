using HemFixBack.Models;
using HemFixBack.Repositories;
using System.Reflection;

namespace HemFixBack.Services
{
    public class GardenTaskService : IGardenTaskService
    {
        public GardenTask Create(GardenTask task)
        {
            if (Database.CreateRecord(task) == false) 
            {
                task = null;
            } 
            return task;
        }

        public GardenTask Get(string id)
        {
            string tableName = "gardentask"; 
            object[] record = Database.ReadRecord(tableName, id);
            if (record == null) 
            {
                return null;
            }

            GardenTask newTask = new GardenTask();
            PropertyInfo[] properties = typeof(GardenTask).GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                if (i < record.Length)
                {
                    // Konvertera värdena om det behövs (till exempel från databastypen)
                    object value = Convert.ChangeType(record[i], properties[i].PropertyType);
                    // Sätt egenskapens värde
                    properties[i].SetValue(newTask, value);
                }
            }
            return newTask;
        }

        public List<GardenTask> List()
        {
            string tableName = "gardentask";
            var records = Database.ListRecords(tableName);
            if (records is null) 
            {
                return null;
            }

            List<GardenTask> gardenTasks = new List<GardenTask>();
            foreach (var record in records)
            {
                GardenTask newTask = new GardenTask();
                PropertyInfo[] properties = typeof(GardenTask).GetProperties();
                for (int i = 0; i < properties.Length; i++)
                {
                    if (i < record.Length)
                    {
                        // Konvertera värdena om det behövs (till exempel från databastypen)
                        object value = Convert.ChangeType(record[i], properties[i].PropertyType);
                        // Sätt egenskapens värde
                        properties[i].SetValue(newTask, value);
                    }
                }
                gardenTasks.Add(newTask);
            }
            return gardenTasks;
        }

        public GardenTask Update(GardenTask newTask)
        {
            if (Delete(newTask.Id))
            {
                Create(newTask);
            }
            else 
            {
                return null;
            }
            return newTask;
        }

        public bool Delete(string id)
        {
            string tableName = "gardentask";
            return Database.DeleteRecord(tableName, id);
        }
    }
}
