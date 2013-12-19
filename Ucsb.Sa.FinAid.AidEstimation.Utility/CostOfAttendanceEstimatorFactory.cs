using System.Xml;

namespace Ucsb.Sa.FinAid.AidEstimation.Utility
{
    public class CostOfAttendanceEstimatorFactory
    {
        private readonly XmlConstantsSource _source;

        public CostOfAttendanceEstimatorFactory(string sourcePath)
        {
            _source = new XmlConstantsSource(sourcePath);
        }

        public CostOfAttendanceEstimatorFactory(XmlDocument sourceDoc)
        {
            _source = new XmlConstantsSource(sourceDoc);
        }
    }
}
