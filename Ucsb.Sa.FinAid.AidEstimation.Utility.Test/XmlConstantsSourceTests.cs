using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation;

namespace Ucsb.Sa.FinAid.AidEstimation.Utility.Test
{
    [TestClass]
    public class XmlConstantsSourceTests
    {
        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void StringConstructor_NullValue_ThrowsException()
        {
            string sourcePath = null;
            XmlConstantsSource source = new XmlConstantsSource(sourcePath);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void StringConstructor_EmptyString_ThrowsException()
        {
            XmlConstantsSource source = new XmlConstantsSource(String.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void StringConstructor_FileDoesNotExist_ThrowsException()
        {
            XmlConstantsSource source = new XmlConstantsSource("this_file_does_not_exist.xml");
        }

        [TestMethod]
        public void StringConstructor_FileExists_Succeeds()
        {
            XmlConstantsSource source = new XmlConstantsSource("TestFile.xml");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void XmlDocConstructor_NullValue_ThrowsException()
        {
            XmlDocument xmlDoc = null;
            XmlConstantsSource source = new XmlConstantsSource(xmlDoc);
        }

        [TestMethod]
        public void XmlDocConstructor_XmlDoc_Succeeds()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlConstantsSource source = new XmlConstantsSource(xmlDoc);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void GetValue_NullValue_ThrowsException()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlConstantsSource source = new XmlConstantsSource(xmlDoc);
            source.GetValue<string>(null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void GetValue_EmptyValue_ThrowsException()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlConstantsSource source = new XmlConstantsSource(xmlDoc);
            source.GetValue<string>(String.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void GetValue_NoNode_ThrowsException()
        {
            XmlDocument xmlDoc = new XmlDocument
            {
                InnerXml = "<constants></constants>"
            };

            XmlConstantsSource source = new XmlConstantsSource(xmlDoc);
            source.GetValue<string>("doesntexist");
        }

        [TestMethod]
        public void GetValue_IntValue_ReturnsInt()
        {
            XmlDocument xmlDoc = new XmlDocument
            {
                InnerXml = @"<constants><constant name=""test""><value>3</value></constant></constants>"
            };

            XmlConstantsSource source = new XmlConstantsSource(xmlDoc);
            int value = source.GetValue<int>("test");

            Assert.AreEqual(3, value);
        }

        [TestMethod]
        public void GetValue_EnumValue_ReturnsEnum()
        {
            XmlDocument xmlDoc = new XmlDocument
            {
                InnerXml = @"<constants><constant name=""test""><value>California</value></constant></constants>"
            };

            XmlConstantsSource source = new XmlConstantsSource(xmlDoc);
            UnitedStatesStateOrTerritory value = source.GetValue<UnitedStatesStateOrTerritory>("test");

            Assert.AreEqual(UnitedStatesStateOrTerritory.California, value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetValue_NoValue_ThrowsException()
        {
            XmlDocument xmlDoc = new XmlDocument
            {
                InnerXml = @"<constants><constant name=""test""></constant></constants>"
            };

            XmlConstantsSource source = new XmlConstantsSource(xmlDoc);
            source.GetValue<int>("test");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void GetArray_NullValue_ThrowsException()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlConstantsSource source = new XmlConstantsSource(xmlDoc);
            source.GetArray<string>(null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void GetArray_EmptyValue_ThrowsException()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlConstantsSource source = new XmlConstantsSource(xmlDoc);
            source.GetArray<string>(String.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void GetArray_NoNode_ThrowsException()
        {
            XmlDocument xmlDoc = new XmlDocument
            {
                InnerXml = "<constants></constants>"
            };

            XmlConstantsSource source = new XmlConstantsSource(xmlDoc);
            source.GetArray<string>("doesntexist");
        }

        [TestMethod]
        public void GetArray_IntValue_ReturnsInt()
        {
            XmlDocument xmlDoc = new XmlDocument
            {
                InnerXml =
                    @"<constants><constant name=""test""><values><value>1</value><value>2</value><value>3</value></values></constant></constants>"
            };

            XmlConstantsSource source = new XmlConstantsSource(xmlDoc);
            int[] values = source.GetArray<int>("test");

            Assert.AreEqual(2, values[1]);
            Assert.AreEqual(3, values.Length);
        }

        [TestMethod]
        public void GetArray_EnumValue_ReturnsEnum()
        {
            XmlDocument xmlDoc = new XmlDocument
            {
                InnerXml =
                    @"<constants><constant name=""test""><values><value>California</value><value>Texas</value></values></constant></constants>"
            };

            XmlConstantsSource source = new XmlConstantsSource(xmlDoc);
            UnitedStatesStateOrTerritory[] values = source.GetArray<UnitedStatesStateOrTerritory>("test");

            Assert.AreEqual(UnitedStatesStateOrTerritory.Texas, values[1]);
            Assert.AreEqual(2, values.Length);
        }

        [TestMethod]
        public void GetArray_EmptyValues_EmptyArray()
        {
            XmlDocument xmlDoc = new XmlDocument
            {
                InnerXml = @"<constants><constant name=""test""><values /></constant></constants>"
            };

            XmlConstantsSource source = new XmlConstantsSource(xmlDoc);
            int[] values = source.GetArray<int>("test");

            Assert.AreEqual(0, values.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetArray_NoValues_ThrowsException()
        {
            XmlDocument xmlDoc = new XmlDocument
            {
                InnerXml = @"<constants><constant name=""test""></constant></constants>"
            };

            XmlConstantsSource source = new XmlConstantsSource(xmlDoc);
            source.GetArray<int>("test");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void GetMultiArray_NullValue_ThrowsException()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlConstantsSource source = new XmlConstantsSource(xmlDoc);
            source.GetMultiArray<string>(null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void GetMultiArray_EmptyValue_ThrowsException()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlConstantsSource source = new XmlConstantsSource(xmlDoc);
            source.GetMultiArray<string>(String.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void GetMultiArray_NoNode_ThrowsException()
        {
            XmlDocument xmlDoc = new XmlDocument
            {
                InnerXml = "<constants></constants>"
            };

            XmlConstantsSource source = new XmlConstantsSource(xmlDoc);
            source.GetMultiArray<string>("doesntexist");
        }

        [TestMethod]
        public void GetMultiArray_IntValue_ReturnsInt()
        {
            XmlDocument xmlDoc = new XmlDocument
            {
                InnerXml =
                    @"<constants><constant name=""test""><values><value>1</value><value>2</value></values><values><value>3</value></values></constant></constants>"
            };

            XmlConstantsSource source = new XmlConstantsSource(xmlDoc);
            int[,] values = source.GetMultiArray<int>("test");

            Assert.AreEqual(2, values.Rank);
            Assert.AreEqual(2, values[0, 1]);
        }

        [TestMethod]
        public void GetMultiArray_EnumValue_ReturnsEnum()
        {
            XmlDocument xmlDoc = new XmlDocument
            {
                InnerXml =
                    @"<constants><constant name=""test""><values><value>California</value></values><values><value>Texas</value></values></constant></constants>"
            };

            XmlConstantsSource source = new XmlConstantsSource(xmlDoc);
            UnitedStatesStateOrTerritory[,] values = source.GetMultiArray<UnitedStatesStateOrTerritory>("test");

            Assert.AreEqual(UnitedStatesStateOrTerritory.Texas, values[1, 0]);
        }

        [TestMethod]
        public void GetMultiArray_EmptyValues_EmptyArray()
        {
            XmlDocument xmlDoc = new XmlDocument
            {
                InnerXml = @"<constants><constant name=""test""><values /></constant></constants>"
            };

            XmlConstantsSource source = new XmlConstantsSource(xmlDoc);
            int[,] values = source.GetMultiArray<int>("test");

            Assert.AreEqual(values.Length, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetMultiArray_NoValues_ThrowsException()
        {
            XmlDocument xmlDoc = new XmlDocument
            {
                InnerXml = @"<constants><constant name=""test""></constant></constants>"
            };

            XmlConstantsSource source = new XmlConstantsSource(xmlDoc);
            source.GetArray<int>("test");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void GetKeyValuePairArray_NullValue_ThrowsException()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlConstantsSource source = new XmlConstantsSource(xmlDoc);
            source.GetKeyValuePairArray<string>(null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void GetKeyValuePairArray_EmptyValue_ThrowsException()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlConstantsSource source = new XmlConstantsSource(xmlDoc);
            source.GetKeyValuePairArray<string>(String.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void GetKeyValuePairArray_NoNode_ThrowsException()
        {
            XmlDocument xmlDoc = new XmlDocument
            {
                InnerXml = "<constants></constants>"
            };

            XmlConstantsSource source = new XmlConstantsSource(xmlDoc);
            source.GetMultiArray<string>("doesntexist");
        }

        [TestMethod]
        public void GetKeyValuePairArray_StringValue_ReturnsString()
        {
            XmlDocument xmlDoc = new XmlDocument
            {
                InnerXml =
                    @"<constants><constant name=""test""><value key=""testkey"">testvalue</value></constant></constants>"
            };

            XmlConstantsSource source = new XmlConstantsSource(xmlDoc);
            KeyValuePair<string, string>[] values = source.GetKeyValuePairArray<string>("test");

            Assert.AreEqual(1, values.Length);
            Assert.AreEqual("testvalue", values.Where(v => v.Key == "testkey").FirstOrDefault().Value);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void GetKeyValuePairArray_NoKey_ThrowsException()
        {
            XmlDocument xmlDoc = new XmlDocument
            {
                InnerXml = @"<constants><constant name=""test""><value>testvalue</value></constant></constants>"
            };

            XmlConstantsSource source = new XmlConstantsSource(xmlDoc);
            KeyValuePair<string, string>[] values = source.GetKeyValuePairArray<string>("test");
        }

        [TestMethod]
        public void GetKeyValuePairArray_EnumValue_ReturnsEnum()
        {
            XmlDocument xmlDoc = new XmlDocument
            {
                InnerXml =
                    @"<constants><constant name=""test""><value key=""state"">Minnesota</value></constant></constants>"
            };

            XmlConstantsSource source = new XmlConstantsSource(xmlDoc);
            KeyValuePair<string, UnitedStatesStateOrTerritory>[] values
                = source.GetKeyValuePairArray<UnitedStatesStateOrTerritory>("test");

            Assert.AreEqual(UnitedStatesStateOrTerritory.Minnesota, values[0].Value);
        }

        [TestMethod]
        public void GetKeyValuePairArray_NoValues_EmptyArray()
        {
            XmlDocument xmlDoc = new XmlDocument
            {
                InnerXml = @"<constants><constant name=""test""></constant></constants>"
            };

            XmlConstantsSource source = new XmlConstantsSource(xmlDoc);
            KeyValuePair<string, int>[] values = source.GetKeyValuePairArray<int>("test");

            Assert.AreEqual(values.Length, 0);
        }


















        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetCostOfAttendanceItemArray_NullValue_ThrowsException()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlConstantsSource source = new XmlConstantsSource(xmlDoc);
            source.GetKeyValuePairArray<string>(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetCostOfAttendanceItemArray_EmptyValue_ThrowsException()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlConstantsSource source = new XmlConstantsSource(xmlDoc);
            source.GetKeyValuePairArray<string>(String.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetCostOfAttendanceItemArray_NoNode_ThrowsException()
        {
            XmlDocument xmlDoc = new XmlDocument
            {
                InnerXml = "<constants></constants>"
            };

            XmlConstantsSource source = new XmlConstantsSource(xmlDoc);
            source.GetMultiArray<string>("doesntexist");
        }

        [TestMethod]
        public void GetCostOfAttendanceItemArray_HasItem_ReturnsValue()
        {
            XmlDocument xmlDoc = new XmlDocument
            {
                InnerXml =
                    @"<constants><constant name=""test""><value name=""testname"" description=""testdescription"">6000</value></constant></constants>"
            };

            XmlConstantsSource source = new XmlConstantsSource(xmlDoc);
            CostOfAttendanceItem[] values = source.GetCostOfAttendanceItemArray("test");

            Assert.AreEqual(1, values.Length);
            Assert.AreEqual("testname", values[0].Name);
            Assert.AreEqual("testdescription", values[0].Description);
            Assert.AreEqual(6000, values[0].Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetCostOfAttendanceItemArray_NoName_ThrowsException()
        {
            XmlDocument xmlDoc = new XmlDocument
            {
                InnerXml =
                    @"<constants><constant name=""test""><value description=""testdescription"">6000</value></constant></constants>"
            };

            XmlConstantsSource source = new XmlConstantsSource(xmlDoc);
            source.GetCostOfAttendanceItemArray("test");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetCostOfAttendanceItemArray_NoDescription_ThrowsException()
        {
            XmlDocument xmlDoc = new XmlDocument
            {
                InnerXml =
                    @"<constants><constant name=""test""><value name=""testname"">6000</value></constant></constants>"
            };

            XmlConstantsSource source = new XmlConstantsSource(xmlDoc);
            source.GetCostOfAttendanceItemArray("test");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetCostOfAttendanceItemArray_NoValue_ThrowsException()
        {
            XmlDocument xmlDoc = new XmlDocument
            {
                InnerXml =
                    @"<constants><constant name=""test""><value name=""testname"" description=""testdescription""></value></constant></constants>"
            };

            XmlConstantsSource source = new XmlConstantsSource(xmlDoc);
            source.GetCostOfAttendanceItemArray("test");
        }

        [TestMethod]
        public void GetCostOfAttendanceItemArray_NoValues_EmptyArray()
        {
            XmlDocument xmlDoc = new XmlDocument
            {
                InnerXml = @"<constants><constant name=""test""></constant></constants>"
            };

            XmlConstantsSource source = new XmlConstantsSource(xmlDoc);
            CostOfAttendanceItem[] values = source.GetCostOfAttendanceItemArray("test");

            Assert.AreEqual(values.Length, 0);
        }
    }
}