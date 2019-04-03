using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingSystem.DataTranferObject
{
    public class ReviewTestResult
    {
        public int TestId { get; set; }
        public string TestName { get; set; }
        public int numRank { get; set; }
        public DateTime dateTest { get; set; }
    }
}
