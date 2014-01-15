using System;
using System.Collections.Generic;
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

        public CostOfAttendanceEstimator GetCostOfAttendanceEstimator()
        {
            Dictionary<CostOfAttendanceKey, CostOfAttendance> coaList = new Dictionary<CostOfAttendanceKey, CostOfAttendance>();

            // It's possible that some of these budgets will not be specified. Instead of displaying an error in these cases,
            // skip past the particular budget

            // Undergrad Off-Campus
            try
            {
                CostOfAttendanceKey undergradOffCampusCoaKey = new CostOfAttendanceKey(EducationLevel.Undergraduate, HousingOption.OffCampus);
                CostOfAttendanceItem[] undergradOffCampusCoaItems = _source.GetCostOfAttendanceItemArray("CoaUndergraduateOffCampus");
                CostOfAttendance undergradOffCampusCoa = new CostOfAttendance(undergradOffCampusCoaItems);
                coaList.Add(undergradOffCampusCoaKey, undergradOffCampusCoa);
            }
            catch (Exception)
            {
            }

            // Undergrad On-Campus
            try
            {
                CostOfAttendanceKey undergradOnCampusCoaKey = new CostOfAttendanceKey(EducationLevel.Undergraduate, HousingOption.OnCampus);
                CostOfAttendanceItem[] undergradOnCampusCoaItems = _source.GetCostOfAttendanceItemArray("CoaUndergraduateOnCampus");
                CostOfAttendance undergradOnCampusCoa = new CostOfAttendance(undergradOnCampusCoaItems);
                coaList.Add(undergradOnCampusCoaKey, undergradOnCampusCoa);
            }
            catch (Exception)
            {
            }

            // Undergrad Commuter
            try
            {
                CostOfAttendanceKey undergradCommuterCoaKey = new CostOfAttendanceKey(EducationLevel.Undergraduate, HousingOption.Commuter);
                CostOfAttendanceItem[] undergradCommuterCoaItems = _source.GetCostOfAttendanceItemArray("CoaUndergraduateCommuter");
                CostOfAttendance undergradCommuterCoa = new CostOfAttendance(undergradCommuterCoaItems);
                coaList.Add(undergradCommuterCoaKey, undergradCommuterCoa);
            }
            catch (Exception)
            {
            }

            // Grad Off-Campus
            try
            {
                CostOfAttendanceKey gradOffCampusCoaKey = new CostOfAttendanceKey(EducationLevel.Graduate, HousingOption.OffCampus);
                CostOfAttendanceItem[] gradOffCampusCoaItems = _source.GetCostOfAttendanceItemArray("CoaGraduateOffCampus");
                CostOfAttendance gradOffCampusCoa = new CostOfAttendance(gradOffCampusCoaItems);
                coaList.Add(gradOffCampusCoaKey, gradOffCampusCoa);
            }
            catch (Exception)
            {
            }

            // Grad On-Campus
            try
            {
                CostOfAttendanceKey gradOnCampusCoaKey = new CostOfAttendanceKey(EducationLevel.Graduate, HousingOption.OnCampus);
                CostOfAttendanceItem[] gradOnCampusCoaItems = _source.GetCostOfAttendanceItemArray("CoaGraduateOnCampus");
                CostOfAttendance gradOnCampusCoa = new CostOfAttendance(gradOnCampusCoaItems);
                coaList.Add(gradOnCampusCoaKey, gradOnCampusCoa);
            }
            catch (Exception)
            {
            }

            // Grad Commuter
            try
            {
                CostOfAttendanceKey gradCommuterCoaKey = new CostOfAttendanceKey(EducationLevel.Graduate, HousingOption.Commuter);
                CostOfAttendanceItem[] gradCommuterCoaItems = _source.GetCostOfAttendanceItemArray("CoaGraduateCommuter");
                CostOfAttendance gradCommuterCoa = new CostOfAttendance(gradCommuterCoaItems);
                coaList.Add(gradCommuterCoaKey, gradCommuterCoa);
            }
            catch (Exception)
            {
            }

            // Out of State Fees
            try
            {
                double outOfStateFees = _source.GetValue<double>("OutOfStateFees");
                foreach (CostOfAttendance coa in coaList.Values)
                {
                    coa.OutOfStateFees = outOfStateFees;
                }
            }
            catch (Exception)
            {
            }

            return new CostOfAttendanceEstimator(coaList);
        }

    }
}
