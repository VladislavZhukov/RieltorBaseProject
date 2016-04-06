using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VI_EF;
using RieltorBase.Domain;
using NUnit.Framework;
using System.Collections.Generic;

namespace RieltorBase.UnitTests
{
    [TestClass]
    public class AddDataInDB
    {
        static VolgaInfoDBEntities context = new VolgaInfoDBEntities();

        [SetUp]
        public static void SetUpTestsMethod()
        {
            context = new VolgaInfoDBEntities();
        }

        [TestMethod]
        public void AddNewFirm()
        {
            SharedOperations.AddFirm("Жожа и рулет");

            var myFirmsName = context.Firms
                .Select(firm => firm.Name).ToList();

            NUnit.Framework.Assert.Contains("Жожа и рулет", myFirmsName);

            var lastFirm = context.Firms.Select(firm => firm).ToList().Last();
            context.Firms.Remove(lastFirm);
            context.SaveChanges();
        }

        [TearDown]
        public static void TearDownTestMethod()
        {
            context.Dispose();
            context = null;
        }
    }
}
