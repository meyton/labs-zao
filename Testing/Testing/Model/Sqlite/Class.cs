using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Testing.Data;

namespace Testing.Model.Sqlite
{
    public class Class : ISqliteModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
