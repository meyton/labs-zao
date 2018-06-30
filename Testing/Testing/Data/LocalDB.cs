using SQLite;
using System;
using System.Collections;
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

        public async Task<List<Student>> GetStudentsByClassID(int id)
        {
            return await database.Table<Student>().Where(x => x.ClassID == id).ToListAsync();
        }

        public async Task<T> GetItemByID<T>(int id) where T : class, ISqliteModel, new()
        {
            return await database.Table<T>().Where(x => x.ID == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItem<T>(T item)
        {
            var result = await database.UpdateAsync(item);

            if (result == 0)
                result = await database.InsertAsync(item);

            return result;
        }

        public async Task<int> DeleteItem<T>(T item)
        {
            return await database.DeleteAsync(item);
        }
    }
}
