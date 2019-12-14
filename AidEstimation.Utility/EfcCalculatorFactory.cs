using System;
using System.Xml;
using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation;
using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation.Constants;

namespace Ucsb.Sa.FinAid.AidEstimation.Utility
{
    /// <summary>
    /// Constructs Calculator and Constants objects from XML sources
    /// </summary>
    public class EfcCalculatorFactory
    {
        private readonly XmlConstantsSource _source;

        public EfcCalculatorFactory(string sourcePath)
        {
            _source = new XmlConstantsSource(sourcePath);
        }

        public EfcCalculatorFactory(XmlDocument sourceDoc)
        {
            _source = new XmlConstantsSource(sourceDoc);
        }

        public EfcCalculator GetEfcCalculator()
        {
            EfcCalculatorConstants constants = GetEfcCalculatorConstants();
            IncomeCalculator incomeCalc = GetIncomeCalculator();
            AllowanceCalculator allowanceCalc = GetAllowanceCalculator();
            AssetContributionCalculator assetContrCalc = GetAssetContributionCalculator();
            AaiContributionCalculator aaiContrCalc = GetAaiContributionCalculator();

            return new EfcCalculator(constants, incomeCalc, allowanceCalc, assetContrCalc, aaiContrCalc);
        }

        public EfcCalculatorConstants GetEfcCalculatorConstants()
        {
            try
            {
                return new EfcCalculatorConstants
                {
                    SimplifiedEfcMax = _source.GetValue<int>("SimplifiedEFCMax"),
                    AutoZeroEfcMax = _source.GetValue<int>("AutoZeroEFCMax"),
                    AltEnrollmentIncomeProtectionAllowance = _source.GetValue<int>("AltEnrollmentIncomeProtectionAllowance")
                };
            }
            catch (Exception)
            {
                throw new Exception("Unable to read EFC Calculator constants");
            }
        }

        public IncomeCalculatorConstants GetIncomeCalculatorConstants()
        {
            try
            {
                return new IncomeCalculatorConstants
                {
                    AiAssessmentPercent = _source.GetValue<double>("AIAssessmentPercent")
                };
            }
            catch (Exception)
            {
                throw new Exception("Unable to read Income Calculator constants");
            }
        }

        public IncomeCalculator GetIncomeCalculator()
        {
            return new IncomeCalculator(GetIncomeCalculatorConstants());
        }

        public AllowanceCalculatorConstants GetAllowanceCalculatorConstants()
        {
            try
            {
                return new AllowanceCalculatorConstants
                {
                    StateTaxAllowanceIncomeThreshold = _source.GetValue<int>("StateTaxAllowanceIncomeThreshold"),
                    ParentStateTaxAllowancePercents = _source.GetArray<int>("ParentStateTaxAllowancePercents"),
                    StudentStateTaxAllowancePercents = _source.GetArray<int>("StudentStateTaxAllowancePercents"),

                    SocialSecurityTaxIncomeThresholds = _source.GetArray<int>("SocialSecurityTaxIncomeThresholds"),
                    SocialSecurityTaxPercentages = _source.GetArray<double>("SocialSecurityTaxPercentages"),
                    SocialSecurityTaxBases = _source.GetArray<double>("SocialSecurityTaxBases"),

                    EmploymentExpensePercent = _source.GetValue<double>("EmploymentExpensePercent"),
                    EmploymentExpenseMaximum = _source.GetValue<int>("EmploymentExpenseMaximum"),

                    DependentParentIncomeProtectionAllowances = _source.GetMultiArray<int>("DepParentIncomeProtectionAllowances"),
                    IndependentWithDependentsIncomeProtectionAllowances = _source.GetMultiArray<int>("IndWithDepIncomeProtectionAllowances"),

                    DependentAdditionalStudentAllowance = _source.GetValue<int>("DepAdditionalStudentAllowance"),
                    DependentAdditionalFamilyAllowance = _source.GetValue<int>("DepAdditionalFamilyAllowance"),
                    IndependentAdditionalStudentAllowance = _source.GetValue<int>("IndAdditionalStudentAllowance"),
                    IndependentAdditionalFamilyAllowance = _source.GetValue<int>("IndAdditionalFamilyAllowance"),

                    DependentStudentIncomeProtectionAllowance = _source.GetValue<int>("DepStudentIncomeProtectionAllowance"),
                    SingleIndependentWithoutDependentsIncomeProtectionAllowance = _source.GetValue<int>("IndWithoutDepSingleIncomeProtectionAllowance"),
                    MarriedIndependentWithoutDependentsIncomeProtectionAllowance = _source.GetValue<int>("IndWithoutDepMarriedIncomeProtectionAllowance")
                };
            }
            catch (Exception)
            {
                throw new Exception("Unable to read Allowance Calculator constants");
            }
        }

        public AllowanceCalculator GetAllowanceCalculator()
        {
            return new AllowanceCalculator(GetAllowanceCalculatorConstants());
        }

        public AssetContributionCalculatorConstants GetAssetContributionCalculatorConstants()
        {
            try
            {
                return new AssetContributionCalculatorConstants
                {
                    DependentParentAssetRate = _source.GetValue<double>("DepParentAssetRate"),
                    DependentStudentAssetRate = _source.GetValue<double>("DepStudentAssetRate"),
                    IndependentWithDependentsAssetRate = _source.GetValue<double>("IndWithDepAssetRate"),
                    IndependentWithoutDependentsAssetRate = _source.GetValue<double>("IndWithoutDepAssetRate"),

                    AssetProtectionAllowanceLowestAge = _source.GetValue<int>("AssetProtectionAllowanceLowestAge"),
                    MarriedAssetProtectionAllowances = _source.GetArray<int>("MarriedAssetProtectionAllowances"),
                    SingleAssetProtectionAllowances = _source.GetArray<int>("SingleAssetProtectionAllowances"),

                    BusinessFarmNetWorthAdjustmentRanges = _source.GetArray<int>("BusinessFarmNetWorthAdjustmentRanges"),
                    BusinessFarmNetWorthAdjustmentBases = _source.GetArray<int>("BusinessFarmNetWorthAdjustmentBases"),
                    BusinessFarmNetWorthAdjustmentPercents = _source.GetArray<double>("BusinessFarmNetWorthAdjustmentPercents")
                };
            }
            catch (Exception)
            {
                throw new Exception("Unable to read Asset Contribution Calculator constants");
            }
        }

        public AssetContributionCalculator GetAssetContributionCalculator()
        {
            return new AssetContributionCalculator(GetAssetContributionCalculatorConstants());
        }

        public AaiContributionCalculatorConstants GetAaiContributionCalculatorConstants()
        {
            try
            {
                return new AaiContributionCalculatorConstants
                {
                    AaiContributionRanges = _source.GetArray<int>("AAIContributionRanges"),
                    AaiContributionBases = _source.GetArray<int>("AAIContributionBases"),
                    AaiContributionPercents = _source.GetArray<double>("AAIContributionPercents")
                };
            }
            catch (Exception)
            {
                throw new Exception("Unable to read AAI Contribution Calculator constants");
            }
        }

        public AaiContributionCalculator GetAaiContributionCalculator()
        {
            return new AaiContributionCalculator(GetAaiContributionCalculatorConstants());
        }
    }
}
