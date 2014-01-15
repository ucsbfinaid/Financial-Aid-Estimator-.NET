using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation.Constants;

namespace Ucsb.Sa.FinAid.AidEstimation.EfcCalculation.Test
{
    public static class TestConstantsFactory
    {
        public static AaiContributionCalculatorConstants GetAaiContributionCalculatorConstants()
        {
            AaiContributionCalculatorConstants constants = new AaiContributionCalculatorConstants();

            constants.AaiContributionBases = new[] { 0, 3366, 4341, 5472, 6798, 8358 };
            constants.AaiContributionPercents = new double[] { 22, 25, 29, 34, 40, 47 };
            constants.AaiContributionRanges = new[] { -3409, 15300, 19200, 23100, 27000, 30900 };

            return constants;
        }

        public static AllowanceCalculatorConstants GetAllowanceCalculatorConstants()
        {
            AllowanceCalculatorConstants constants = new AllowanceCalculatorConstants();

            constants.StateTaxAllowanceIncomeThreshold = 15000;

            constants.ParentStateTaxAllowancePercents = new[]
            {
                2, 3, 2, 2, 4, 4, 8, 2, 5, 8, 5, 7, 2, 3, 6, 2, 4, 5, 5, 4, 5, 5, 5, 3, 6, 2, 8,
                7, 2, 5, 6, 3, 5, 5, 5, 3, 5, 9, 3, 9, 6, 3, 2, 6, 4, 7, 2, 5, 2, 7, 5, 2, 2, 3,
                5, 6, 2, 6, 4, 3, 7, 2
            };

            constants.StudentStateTaxAllowancePercents = new[]
            {
                2, 2, 0, 2, 2, 3, 5, 2, 3, 5, 3, 5, 2, 1, 3, 2, 3, 3, 2, 3, 3, 3, 4, 2, 4, 2, 6,
                4, 2, 3, 4, 3, 3, 3, 3, 1, 1, 4, 2, 6, 4, 1, 2, 3, 3, 5, 2, 3, 2, 4, 3, 1, 1, 1,
                3, 3, 2, 4, 1, 2, 4, 1
            };

            constants.SocialSecurityTaxIncomeThreshold = 110100;
            constants.SocialSecurityLowPercent = 0.0765;
            constants.SocialSecurityHighPercent = 0.0145;
            constants.SocialSecurityHighBase = 8422.65;

            constants.EmploymentExpensePercent = 0.35;
            constants.EmploymentExpenseMaximum = 3900;

            constants.DependentParentIncomeProtectionAllowances = new[,]
            {
                { 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0 },
                { 0, 17100, 14170, 0, 0, 0 },
                { 0, 21290, 18380, 15450, 0, 0 },
                { 0, 26290, 23370, 20460, 17530, 0 },
                { 0, 31020, 28100, 25190, 22260, 19350 },
                { 0, 36290, 33360, 30450, 27530, 24620 }
            };

            constants.IndependentWithDependentsIncomeProtectionAllowances = new[,]
            {
                { 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0 },
                { 0, 24150, 20020, 0, 0, 0 },
                { 0, 30070, 25960, 21830, 0, 0 },
                { 0, 37130, 33010, 28900, 24760, 0 },
                { 0, 43810, 39670, 35570, 31450, 27340 },
                { 0, 51230, 47110, 43020, 38870, 34770 }
            };

            constants.DependentAdditionalStudentAllowance = 2910;
            constants.DependentAdditionalFamilyAllowance = 4100;

            constants.IndependentAdditionalStudentAllowance = 4110;
            constants.IndependentAdditionalFamilyAllowance = 5780;

            constants.DependentStudentIncomeProtectionAllowance = 6130;
            constants.SingleIndependentWithoutDependentsIncomeProtectionAllowance = 9540;
            constants.MarriedIndependentWithoutDependentsIncomeProtectionAllowance = 15290;

            return constants;
        }

        public static AssetContributionCalculatorConstants GetAssetContributionCalculatorConstants()
        {
            AssetContributionCalculatorConstants constants = new AssetContributionCalculatorConstants();

            constants.DependentParentAssetRate = 0.12;
            constants.DependentStudentAssetRate = 0.2;
            constants.IndependentWithDependentsAssetRate = 0.07;
            constants.IndependentWithoutDependentsAssetRate = 0.2;

            constants.AssetProtectionAllowanceLowestAge = 25;
            constants.MarriedAssetProtectionAllowances = new[]
            {
                0, 2100, 4300, 6400, 8600, 10700, 12800, 15000, 17100, 19300,
                21400, 23500, 25700, 27800, 30000, 32100, 32900, 33700, 34500,
                35400, 36200, 37100, 38000, 39000, 39900, 40900, 42100, 43100,
                44200, 45500, 46800, 47900, 49300, 50800, 52200, 53500, 55000,
                56900, 58500, 60100, 61800,
            };
            constants.SingleAssetProtectionAllowances = new[]
            {
                0, 600, 1300, 1900, 2500, 3200, 3800, 4400, 5100, 5700, 6300,
                7000, 7600, 8200, 8900, 9500, 9700, 9900, 10100, 10300, 10600,
                10800, 11100, 11300, 11600, 11900, 12200, 12500, 12800, 13100,
                13400, 13700, 14100, 14400, 14800, 15100, 15600, 16000, 16400,
                16900, 17400
            };

            constants.BusinessFarmNetWorthAdjustmentRanges = new[] { 1, 120000, 365000, 610000 };
            constants.BusinessFarmNetWorthAdjustmentBases = new[] { 0, 48000, 170500, 317500 };
            constants.BusinessFarmNetWorthAdjustmentPercents = new double[] { 40, 50, 60, 100 };

            return constants;
        }

        public static IncomeCalculatorConstants GetIncomeCalculatorConstants()
        {
            IncomeCalculatorConstants constants = new IncomeCalculatorConstants
            {
                AiAssessmentPercent = 0.5
            };

            return constants;
        }

        public static EfcCalculatorConstants GetEfcCalculatorConstants()
        {
            EfcCalculatorConstants constants = new EfcCalculatorConstants();

            constants.AltEnrollmentIncomeProtectionAllowance = 4730;
            constants.SimplifiedEfcMax = 49999;
            constants.AutoZeroEfcMax = 24000;

            return constants;
        }
    }
}
