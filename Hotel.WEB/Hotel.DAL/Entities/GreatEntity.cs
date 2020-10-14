using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Entities
{
    /// <summary>
    /// For the convenience in using in generic Repository
    /// </summary>
    abstract public class GreatEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
