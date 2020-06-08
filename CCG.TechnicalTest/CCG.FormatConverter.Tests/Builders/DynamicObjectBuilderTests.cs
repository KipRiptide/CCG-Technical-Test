using CCG.FormatConverter.Builders;
using CCG.FormatConverter.Readers;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCG.FormatConverter.Tests.Builders
{
    [TestClass]
    public class DynamicObjectBuilderTests
    {
        [TestMethod]
        public void Build_WithNullFields_ThrowsArgumentNullException()
        {
            var builder = new DynamicObjectBuilder();
            string[] fields = null;
            string[] values = { "Dave" };
            Assert.ThrowsException<ArgumentNullException>(() => builder.Build(fields, values));
        }

        [TestMethod]
        public void Build_WithNullValues_ThrowsArgumentNullException()
        {
            var builder = new DynamicObjectBuilder();
            string[] fields = { "Name" };
            string[] values = null;
            Assert.ThrowsException<ArgumentNullException>(() => builder.Build(fields, values));
        }

        [TestMethod]
        public void Build_WithEmptyFields_ThrowsArgumentOutOfRangeException()
        {
            var builder = new DynamicObjectBuilder();
            string[] fields = { };
            string[] values = new[] { "Dave" };
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => builder.Build(fields, values));
        }

        [TestMethod]
        public void Build_WithEmptyValie_ThrowsArgumentOutOfRangeException()
        {
            var builder = new DynamicObjectBuilder();
            string[] fields = new[] { "Name" };
            string[] values = { };
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => builder.Build(fields, values));
        }

        [TestMethod]
        public void Build_WithFieldsAndValuesOfDifferentLength_ThrowsArgumentException()
        {
            var builder = new DynamicObjectBuilder();
            string[] fields = new[] { "Name" };
            string[] values = new[] { "Dave", "Smith" };
            Assert.ThrowsException<ArgumentException>(() => builder.Build(fields, values));
        }

        [TestMethod]
        public void Build_WithValidFieldsAndValues_ReturnsDynamicObject()
        {
            var builder = new DynamicObjectBuilder();
            string[] fields = new[] { "GivenName", "FamilyName" };
            string[] values = new[] { "Dave", "Smith" };
            var result = builder.Build(fields, values);
            Assert.AreEqual("Dave", result.GivenName);
            Assert.AreEqual("Smith", result.FamilyName);
        }

        [TestMethod]
        public void Build_WithNullValue_ReturnsDynamicObject()
        {
            var builder = new DynamicObjectBuilder();
            string[] fields = new[] { "GivenName", "MiddleNames", "FamilyName" };
            string[] values = new[] { "Dave", null, "Smith" };
            var result = builder.Build(fields, values);
            Assert.AreEqual("Dave", result.GivenName);
            Assert.IsNull(result.MiddleNames);
            Assert.AreEqual("Smith", result.FamilyName);
        }

        [TestMethod]
        public void Build_WithNestedFieldsAndValues_ReturnsNestedDynamicObject()
        {
            var builder = new DynamicObjectBuilder();
            string[] fields = new[] { "Contact_Phone_Home", "GivenName", "FamilyName", "Contact_Email", "Contact_Phone_Mobile" };
            string[] values = new[] { "01234568", "Dave", "Smith", "dave.smith@test.com", "0712345689" };
            var result = builder.Build(fields, values);
            Assert.AreEqual("Dave", result.GivenName);
            Assert.AreEqual("Smith", result.FamilyName);
            Assert.AreEqual("dave.smith@test.com", result.Contact.Email);
            Assert.AreEqual("01234568", result.Contact.Phone.Home);
            Assert.AreEqual("0712345689", result.Contact.Phone.Mobile);
        }

        
    }
}
