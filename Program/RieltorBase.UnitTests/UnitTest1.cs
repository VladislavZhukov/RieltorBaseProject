using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VI_EF;

namespace RieltorBase.UnitTests
{
    [TestClass]
    public class UnitTest1
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
