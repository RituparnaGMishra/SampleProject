using SampleFunctionProject.Data;
using SampleFunctionProject.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleFunctionProject.Services
{
    internal class DemandManagementService : DbService
    {
        private ApplicationDBContext _context => base.GetDbContext();

        public IList<Demand> GetAll()
        {

            return _context.Demands.ToList();
        }

        public Demand GetById(Guid Id)
        {

            return _context.Demands.Find(Id);
        }

        public Demand Edit(Demand demand)
        {

            _context.Demands.Update(demand);
            _context.SaveChanges();
            return demand;
        }

        public Demand Add(Demand demand)
        {

            _context.Demands.Add(demand);
            _context.SaveChanges();
            return demand;
        }

        public void Delete(Guid id)
        {
            var demand = GetById(id);
            _context.Demands.Remove(demand);
            _context.SaveChanges();
        }
        public IList<Demand> GetByUserId(Guid id)
        {

            return _context.Demands
                .Where(d=>d.User.Id==id).ToList();
        }

    }
}
