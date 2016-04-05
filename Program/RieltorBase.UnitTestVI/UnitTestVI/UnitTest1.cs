using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VI_EF;

namespace UnitTestVI
{
    [TestClass]
    public class FirsrTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (VolgaInfoDBEntities context = new VolgaInfoDBEntities())
            {
                var newAgent = new Firm { Name = "xxx1" };

                context.Firms.Add(newAgent);
                context.SaveChanges();

                Console.WriteLine("Done");
            }
        }
    }
}
