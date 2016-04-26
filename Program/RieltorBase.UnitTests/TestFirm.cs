using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RieltorBase.Domain;
using NUnit.Framework;
using System.Collections.Generic;


namespace RieltorBase.UnitTests
{
    [TestClass]
    public class TestFirm
    {
        //VolgaInfoDBEntities _context = new VolgaInfoDBEntities();
        //readonly Firm _testFirm = new Firm() { Name = "TestFirm" };

        //[SetUp]
        //public void Init()
        //{
        //    _context = new VolgaInfoDBEntities();
        //}

        //[TestMethod]
        //public void AddNewFirm()
        //{
        //    FirmCard.Add(_testFirm, _context);

        //    var checkingFirm = _context.Firms
        //        .Select(firm => firm.Name).ToList();

        //    NUnit.Framework.Assert.Contains(_testFirm.Name, checkingFirm);
        //}

        //[TestMethod]
        //public void DeleteFirm()
        //{
        //    FirmCard.Delete(_testFirm, _context);

        //    var checkingFirm = _context.Firms
        //        .Where(firm => firm.Name == _testFirm.Name)
        //        .Select(firm => firm.Name).ToList();

        //    NUnit.Framework.Assert.AreEqual(checkingFirm.Count(), 0);
        //}

        //[TearDown]
        //public void Cleanup()
        //{
        //    _context.Dispose();
        //    _context = null;
        //}
    }
}
