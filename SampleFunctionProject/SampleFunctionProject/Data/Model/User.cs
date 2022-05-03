using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleFunctionProject.Data.Model
{
    internal class User
    {

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public GenderType Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        internal bool Any()
        {
            throw new NotImplementedException();
        }
    }

    internal enum GenderType
    {
        male,
        female,
        na
    }
}
