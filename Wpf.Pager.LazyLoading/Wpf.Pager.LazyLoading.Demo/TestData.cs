using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.Pager.LazyLoading.Demo
{
    public class TestData
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; } 

        public static IEnumerable<TestData> GetTestData()
        {
            
            for (int i = 0; i < 300; i++)
            {
                yield return new TestData
                {
                    Id = i,
                    Name = "Name " + i,
                    Address = "Address " + i
                };
            }
        }
    }
}
