using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Ucsb.Sa.FinAid.AidEstimation.Utility
{
    /// <summary>
    /// Reads constants used within Aid Estimation calculations from XML files
    /// </summary>
    public class XmlConstantsSource
    {
        private enum ConstantMultiplicity
        {
            Single,
            Multiple,
            Multidimensional
        }

        private readonly XmlDocument _source;

        /// <summary>
        /// Creates a new constants source with values harvested from the XML file
        /// at the specified path
        /// </summary>
        /// <param name="sourcePath">Full path to the source file</param>
        public XmlConstantsSource(string sourcePath)
        {
            if (String.IsNullOrEmpty(sourcePath))
            {
                throw new ArgumentException("Path for XML source file was not provided");
            }

            if (!File.Exists(sourcePath))
            {
                throw new ArgumentException("XML source file does not exist");
            }

            _source = new XmlDocument();
            _source.Load(sourcePath);
        }

        /// <summary>
        /// Creates a new constants source with values harvested from the <see cref="XmlDocument"/>
        /// </summary>
        /// <param name="source">Source XML</param>
        public XmlConstantsSource(XmlDocument source)
        {
            if (source == null)
            {
                throw new ArgumentException();
            }

            _source = source;
        }

        /// <summary>
        /// Returns the value of a constant
        /// </summary>
        /// <typeparam name="T">Type of the constant</typeparam>
        /// <param name="constantName">Name of the constant</param>
        /// <returns>The value of the constant</returns>
        public T GetValue<T>(string constantName)
        {
            XmlNode node = GetConstantNode(constantName, ConstantMultiplicity.Single);
            return ConvertValue<T>(node.InnerText);
        }

        /// <summary>
        /// Returns the values of a constant, contained within an array
        /// </summary>
        /// <typeparam name="T">Type of the constant</typeparam>
        /// <param name="constantName">Name of the constant</param>
        /// <returns>The values of the constant, as an array</returns>
        public T[] GetArray<T>(string constantName)
        {
            XmlNode node = GetConstantNode(constantName, ConstantMultiplicity.Multiple);

            T[] constants = new T[node.ChildNodes.Count];
            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                constants[i] = ConvertValue<T>(node.ChildNodes[i].InnerText);
            }

            return constants;
        }

        /// <summary>
        /// Returns the values of a constant, as a multidimensional array
        /// </summary>
        /// <typeparam name="T">Type of the constant</typeparam>
        /// <param name="constantName">Name of the constant</param>
        /// <returns>The values of the constant, as a multidimensional array</returns>
        public T[,] GetMultiArray<T>(string constantName)
        {
            XmlNode node = GetConstantNode(constantName, ConstantMultiplicity.Multidimensional);

            T[,] constants = new T[node.ChildNodes.Count, node.ChildNodes[0].ChildNodes.Count];
            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                for (int j = 0; j < node.ChildNodes[i].ChildNodes.Count; j++)
                {
                    constants[i, j] = ConvertValue<T>(node.ChildNodes[i].ChildNodes[j].InnerText);
                }
            }

            return constants;
        }

        /// <summary>
        /// Returns the value of a constant, as a key value pair
        /// </summary>
        /// <typeparam name="T">Type of the constant</typeparam>
        /// <param name="constantName">Name of the constant</param>
        /// <returns>The values of the constant, as a key value pair</returns>
        public KeyValuePair<string, T>[] GetKeyValuePairArray<T>(string constantName)
        {
            XmlNode node = GetConstantNode(constantName, ConstantMultiplicity.Multidimensional);
            KeyValuePair<string, T>[] constants = new KeyValuePair<string, T>[node.ChildNodes.Count];

            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                if (node.ChildNodes[i].Attributes == null
                    || node.ChildNodes[i].Attributes["key"] == null)
                {
                    throw new ArgumentException("No key provided");
                }

                string key = node.ChildNodes[i].Attributes["key"].Value;
                T value = ConvertValue<T>(node.ChildNodes[i].InnerText);
                constants[i] = new KeyValuePair<string, T>(key, value);
            }

            return constants;
        }

        public CostOfAttendanceItem[] GetCostOfAttendanceItemArray(string constantName)
        {
            XmlNode node = GetConstantNode(constantName, ConstantMultiplicity.Multidimensional);

            CostOfAttendanceItem[] constants = new CostOfAttendanceItem[node.ChildNodes.Count];
            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                CostOfAttendanceItem item = new CostOfAttendanceItem();
                XmlNode itemNode = node.ChildNodes[i];

                if (itemNode.Attributes == null)
                {
                    throw new ArgumentException("No cost of attendance item attributes provided");
                }

                // Name
                XmlAttribute nameAttr = itemNode.Attributes["name"];
                if (nameAttr == null || String.IsNullOrEmpty(nameAttr.Value))
                {
                    throw new ArgumentException("No cost of attendance item name provided");
                }

                item.Name = nameAttr.Value;

                // Description
                XmlAttribute descAttr = itemNode.Attributes["description"];
                if (descAttr == null || String.IsNullOrEmpty(descAttr.Value))
                {
                    throw new ArgumentException("No cost of attendance item description provided");
                }

                item.Description = descAttr.Value;

                // Value
                item.Value = ConvertValue<double>(itemNode.InnerText);

                constants[i] = item;
            }

            return constants;
        }

        private XmlNode GetConstantNode(string constantName, ConstantMultiplicity multiplicity)
        {
            if (String.IsNullOrEmpty(constantName))
            {
                throw new ArgumentException("No constant name provided");
            }

            string valueSelector = null;

            switch (multiplicity)
            {
                case ConstantMultiplicity.Single:
                    valueSelector = "/value";
                    break;

                case ConstantMultiplicity.Multiple:
                    valueSelector = "/values";
                    break;

                case ConstantMultiplicity.Multidimensional:
                    valueSelector = String.Empty;
                    break;
            }

            string selectionString = GetSelectionString(constantName, valueSelector);
            XmlNode node = _source.SelectSingleNode(selectionString);

            if (node == null)
            {
                throw new ArgumentException("Constant could not be found");
            }

            return node;
        }

        private static T ConvertValue<T>(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentException("No value provided");
            }

            Type constantType = typeof(T);

            if (constantType.BaseType == typeof(Enum))
            {
                return (T)Enum.Parse(constantType, value);
            }

            return (T)Convert.ChangeType(value, constantType);
        }

        private static string GetSelectionString(string constantName, string valueSelector)
        {
            return String.Format("constants/constant[@name='{0}']{1}", constantName, valueSelector);
        }
    }
}