using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SampleFunctionProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleFunctionProject.Services
{
    internal class DbService : IDisposable
    {
        private readonly SqliteConnection _dbConnection;
        internal DbService()
        {
            _dbConnection = new SqliteConnection("Filename=:memory:");
            if(_dbConnection.State == System.Data.ConnectionState.Closed) { 
            _dbConnection.Open();
            }
        }

        internal ApplicationDBContext GetDbContext()
        {
            var applicationDbContextOptions = new DbContextOptionsBuilder<ApplicationDBContext>().UseSqlite(_dbConnection).Options;
            return new ApplicationDBContext(applicationDbContextOptions);

        }

        public void Dispose() => _dbConnection.Close();
    }
}
