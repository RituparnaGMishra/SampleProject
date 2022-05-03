using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SampleFunctionProject.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.InMemory;
using System;
using SampleFunctionProject.Data.Model;

namespace SampleFunctionProject.Services
{
    internal class UserManagementService : DbService
    {
    
        private ApplicationDBContext _context => base.GetDbContext();

        public IList<User> GetAll()
        {
           
            return _context.Users.ToList();
        }

        public User GetById(Guid Id)
        {
           
            return _context.Users.Find(Id);
        }

        public User Edit(User user)
        {
           
            _context.Users.Update(user);
            _context.SaveChanges();
            return user;
        }

        public User Add(User user)
        {
           
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public void Delete(Guid id)
        {
            var user = GetById(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
       }
      
    }
}
