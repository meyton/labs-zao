using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Testing.Model.Sqlite;

namespace Testing.Data
{
    public class LocalDB
    {
        readonly SQLiteAsyncConnection database;

        public LocalDB(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Student>().Wait();
            database.CreateTableAsync<Class>().Wait();
        }

        public async Task<List<T>> GetItems<T>() where T : class, new()
        {
            return await database.Table<T>().ToListAsync();
        }

        public async Task<T> GetItemByID<T>(int id) where T : class, ISqliteModel, new()
        {
            return await database.Table<T>().Where(x => x.ID == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItem<T>(T item)
        {
            return await database.InsertOrReplaceAsync(item);
        }

    }
}
