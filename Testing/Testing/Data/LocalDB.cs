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
        private SQLiteAsyncConnection _database;

        public LocalDB(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Student>().Wait();
            _database.CreateTableAsync<Class>().Wait();
        }

        internal async Task<List<T>> GetItems<T>() where T : class, new()
        {
            return await _database.Table<T>().ToListAsync();
        }

        internal async Task<T> GetItemByID<T>(int id) where T : class, ISqliteModel, new()
        {
            return await _database.Table<T>().Where(x => x.ID == id).FirstOrDefaultAsync();
        }

        internal async Task<int> SaveItemAsync<T>(T item)
        {
            return await _database.InsertOrReplaceAsync(item);
        }
    }
}
