using SampleFunctionProject.Data;
using SampleFunctionProject.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleFunctionProject.Services
{
    internal class CarManagementService : DbService
    {
        private ApplicationDBContext _context => base.GetDbContext();

        public IList<Car> GetAll()
        {

            return _context.Cars.ToList();
        }

        public Car GetById(Guid Id)
        {

            return _context.Cars.Find(Id);
        }

        public Car Edit(Car car)
        {

            _context.Cars.Update(car);
            _context.SaveChanges();
            return car;
        }

        public Car Add(Car car)
        {

            _context.Cars.Add(car);
            _context.SaveChanges();
            return car;
        }

        public void Delete(Guid id)
        {
            var car = GetById(id);
            _context.Cars.Remove(car);
            _context.SaveChanges();
        }
    }
}
