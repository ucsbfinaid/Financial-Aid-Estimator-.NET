using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation.Arguments;
using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation.Constants;

namespace Ucsb.Sa.FinAid.AidEstimation.EfcCalculation.Test
{
    [TestClass]
    public class EfcCalculatorTests
    {
        private EfcCalculator _efcCalculator;

        [TestInitialize]
        public void Init()
        {
            EfcCalculatorConstants constants = TestConstantsFactory.GetEfcCalculatorConstants();

            IncomeCalculator incomeCalculator
                = new IncomeCalculator(TestConstantsFactory.GetIncomeCalculatorConstants());
            AllowanceCalculator allowanceCalculator
                = new AllowanceCalculator(TestConstantsFactory.GetAllowanceCalculatorConstants());
            AssetContributionCalculator assetContributionCalculator
                = new AssetContributionCalculator(TestConstantsFactory.GetAssetContributionCalculatorConstants());
            AaiContributionCalculator aaiContributionCalculator 
                = new AaiContributionCalculator(TestConstantsFactory.GetAaiContributionCalculatorConstants());

            _efcCalculator = new EfcCalculator(constants, 
                incomeCalculator, allowanceCalculator,
                assetContributionCalculator, aaiContributionCalculator);
        }

        [TestMethod]
        public void GetDependentEfcProfile_ZeroNumberInCollege_ZeroEfc()
        {
            DependentEfcCalculatorArguments args = new DependentEfcCalculatorArguments
            {
                NumberInCollege = 0,
                Student = new HouseholdMember()
            };

            EfcProfile result = _efcCalculator.GetDependentEfcProfile(args);
            Assert.AreEqual(0, result.ExpectedFamilyContribution);
        }

        [TestMethod]
        public void GetDependentEfcProfile_NegativeNumberInCollege_ZeroEfc()
        {
            DependentEfcCalculatorArguments args = new DependentEfcCalculatorArguments
            {
                NumberInCollege = -1,
                Student = new HouseholdMember(),
                MonthsOfEnrollment = 9
            };

            EfcProfile result = _efcCalculator.GetDependentEfcProfile(args);
            Assert.AreEqual(0, result.ExpectedFamilyContribution);
        }

        [TestMethod]
        public void GetDependentEfcProfile_NoStudent_ZeroEfc()
        {
            DependentEfcCalculatorArguments args = new DependentEfcCalculatorArguments
            {
                NumberInCollege = 3,
                Student = null,
                MonthsOfEnrollment = 9
            };

            EfcProfile result = _efcCalculator.GetDependentEfcProfile(args);
            Assert.AreEqual(0, result.ExpectedFamilyContribution);
        }

        [TestMethod]
        public void GetDependentEfcProfile_LowValues_Calculated()
        {
            DependentEfcCalculatorArguments args = new DependentEfcCalculatorArguments
            {
                FirstParent = new HouseholdMember
                {
                    IsWorking = false,
                    WorkIncome = 0
                },
                SecondParent = new HouseholdMember
                {
                    IsWorking = false,
                    WorkIncome = 0
                },
                Student = new HouseholdMember
                {
                    IsWorking = false,
                    WorkIncome = 0
                },
                ParentAdjustedGrossIncome = 0,
                AreParentsTaxFilers = false,
                ParentIncomeTaxPaid = 0,
                ParentUntaxedIncomeAndBenefits = 10000,
                ParentAdditionalFinancialInfo = 0,
                StudentAdjustedGrossIncome = 0,
                IsStudentTaxFiler = false,
                StudentIncomeTaxPaid = 0,
                StudentUntaxedIncomeAndBenefits = 0,
                StudentAdditionalFinancialInfo = 0,
                ParentCashSavingsChecking = 0,
                ParentInvestmentNetWorth = 0,
                ParentBusinessFarmNetWorth = 0,
                StudentCashSavingsChecking = 0,
                StudentInvestmentNetWorth = 0,
                MaritalStatus = MaritalStatus.MarriedRemarried,
                StateOfResidency = UnitedStatesStateOrTerritory.California,
                NumberInHousehold = 3,
                NumberInCollege = 1,
                OldestParentAge = 30,
                MonthsOfEnrollment = 9
            };

            EfcProfile profile = _efcCalculator.GetDependentEfcProfile(args);
            Assert.AreEqual(0, profile.ExpectedFamilyContribution);
        }

        [TestMethod]
        public void GetDependentEfcProfile_ParentTotalIncome_Calculated()
        {
            DependentEfcCalculatorArguments args = new DependentEfcCalculatorArguments
            {
                FirstParent = new HouseholdMember
                {
                    IsWorking = false,
                    WorkIncome = 0
                },
                SecondParent = new HouseholdMember
                {
                    IsWorking = false,
                    WorkIncome = 0
                },
                Student = new HouseholdMember
                {
                    IsWorking = false,
                    WorkIncome = 0
                },
                ParentAdjustedGrossIncome = 0,
                AreParentsTaxFilers = false,
                ParentIncomeTaxPaid = 0,
                ParentUntaxedIncomeAndBenefits = 10000,
                ParentAdditionalFinancialInfo = 0,
                StudentAdjustedGrossIncome = 0,
                IsStudentTaxFiler = false,
                StudentIncomeTaxPaid = 0,
                StudentUntaxedIncomeAndBenefits = 0,
                StudentAdditionalFinancialInfo = 0,
                ParentCashSavingsChecking = 0,
                ParentInvestmentNetWorth = 0,
                ParentBusinessFarmNetWorth = 0,
                StudentCashSavingsChecking = 0,
                StudentInvestmentNetWorth = 0,
                MaritalStatus = MaritalStatus.MarriedRemarried,
                StateOfResidency = UnitedStatesStateOrTerritory.California,
                NumberInHousehold = 3,
                NumberInCollege = 1,
                OldestParentAge = 30,
                MonthsOfEnrollment = 9
            };

            EfcProfile profile = _efcCalculator.GetDependentEfcProfile(args);
            Assert.AreEqual(10000, profile.ParentTotalIncome);
        }

        [TestMethod]
        public void GetDependentEfcProfile_Values_Calculated()
        {
            DependentEfcCalculatorArguments args = new DependentEfcCalculatorArguments
            {
                FirstParent = null,
                SecondParent = new HouseholdMember
                {
                    IsWorking = true,
                    WorkIncome = 60000
                },
                Student = new HouseholdMember
                {
                    IsWorking = true,
                    WorkIncome = 10000
                },
                ParentAdjustedGrossIncome = 60000,
                AreParentsTaxFilers = true,
                ParentIncomeTaxPaid = 6000,
                ParentUntaxedIncomeAndBenefits = 1000,
                ParentAdditionalFinancialInfo = 200,
                StudentAdjustedGrossIncome = 10000,
                IsStudentTaxFiler = true,
                StudentIncomeTaxPaid = 1000,
                StudentUntaxedIncomeAndBenefits = 0,
                StudentAdditionalFinancialInfo = 0,
                ParentCashSavingsChecking = 80000,
                ParentInvestmentNetWorth = 5000,
                ParentBusinessFarmNetWorth = 0,
                StudentCashSavingsChecking = 3000,
                StudentInvestmentNetWorth = 0,
                MaritalStatus = MaritalStatus.SingleSeparatedDivorced,
                StateOfResidency = UnitedStatesStateOrTerritory.California,
                NumberInHousehold = 3,
                NumberInCollege = 1,
                OldestParentAge = 45,
                MonthsOfEnrollment = 9
            };

            EfcProfile profile = _efcCalculator.GetDependentEfcProfile(args);
            Assert.AreEqual(7923, profile.ExpectedFamilyContribution);
        }

        [TestMethod]
        public void GetDependentEfcProfile_HighValues_Calculated()
        {
            DependentEfcCalculatorArguments args = new DependentEfcCalculatorArguments
            {
                FirstParent = new HouseholdMember
                {
                    IsWorking = true,
                    WorkIncome = 600000
                },
                SecondParent = new HouseholdMember
                {
                    IsWorking = true,
                    WorkIncome = 600000
                },
                Student = new HouseholdMember
                {
                    IsWorking = true,
                    WorkIncome = 20000
                },
                ParentAdjustedGrossIncome = 1200000,
                AreParentsTaxFilers = true,
                ParentIncomeTaxPaid = 120000,
                ParentUntaxedIncomeAndBenefits = 10000,
                ParentAdditionalFinancialInfo = 2000,
                StudentAdjustedGrossIncome = 20000,
                IsStudentTaxFiler = true,
                StudentIncomeTaxPaid = 2000,
                StudentUntaxedIncomeAndBenefits = 0,
                StudentAdditionalFinancialInfo = 0,
                ParentCashSavingsChecking = 100000,
                ParentInvestmentNetWorth = 8000,
                ParentBusinessFarmNetWorth = 9000,
                StudentCashSavingsChecking = 6000,
                StudentInvestmentNetWorth = 1000,
                MaritalStatus = MaritalStatus.MarriedRemarried,
                StateOfResidency = UnitedStatesStateOrTerritory.California,
                NumberInHousehold = 20,
                NumberInCollege = 10,
                OldestParentAge = 45,
                MonthsOfEnrollment = 9
            };

            EfcProfile profile = _efcCalculator.GetDependentEfcProfile(args);
            Assert.AreEqual(46943, profile.ExpectedFamilyContribution);
        }

        [TestMethod]
        public void GetIndependentEfcProfile_ZeroNumberInCollege_ZeroEfc()
        {
            IndependentEfcCalculatorArguments args = new IndependentEfcCalculatorArguments
            {
                NumberInCollege = 0,
                Student = new HouseholdMember()
            };

            EfcProfile result = _efcCalculator.GetIndependentEfcProfile(args);
            Assert.AreEqual(0, result.ExpectedFamilyContribution);
        }

        [TestMethod]
        public void GetIndependentEfcProfile_NegativeNumberInCollege_ZeroEfc()
        {
            IndependentEfcCalculatorArguments args = new IndependentEfcCalculatorArguments
            {
                NumberInCollege = -1,
                Student = new HouseholdMember()
            };

            EfcProfile result = _efcCalculator.GetIndependentEfcProfile(args);
            Assert.AreEqual(0, result.ExpectedFamilyContribution);
        }

        [TestMethod]
        public void GetIndependentEfcProfile_NoStudent_ZeroEfc()
        {
            IndependentEfcCalculatorArguments args = new IndependentEfcCalculatorArguments
            {
                NumberInCollege = 3,
                Student = null
            };

            EfcProfile result = _efcCalculator.GetIndependentEfcProfile(args);
            Assert.AreEqual(0, result.ExpectedFamilyContribution);
        }

        [TestMethod]
        public void GetIndependentEfcProfile_LowValues_Calculated()
        {
            var args = new IndependentEfcCalculatorArguments
            {
                Student = new HouseholdMember
                {
                    IsWorking = false,
                    WorkIncome = 0
                },
                Spouse = new HouseholdMember
                {
                    IsWorking = false,
                    WorkIncome = 0
                },
                AdjustedGrossIncome = 0,
                AreTaxFilers = false,
                IncomeTaxPaid = 0,
                UntaxedIncomeAndBenefits = 10000,
                AdditionalFinancialInfo = 0,
                CashSavingsCheckings = 0,
                InvestmentNetWorth = 0,
                BusinessFarmNetWorth = 0,
                HasDependents = false,
                MaritalStatus = MaritalStatus.MarriedRemarried,
                StateOfResidency = UnitedStatesStateOrTerritory.AmericanSamoa,
                NumberInHousehold = 2,
                NumberInCollege = 1,
                Age = 25,
                MonthsOfEnrollment = 9
            };

            EfcProfile profile = _efcCalculator.GetIndependentEfcProfile(args);
            Assert.AreEqual(0, profile.ExpectedFamilyContribution);
        }

        [TestMethod]
        public void GetIndependentEfcProfile_Values_Calculated()
        {
            var args = new IndependentEfcCalculatorArguments
            {
                Student = new HouseholdMember
                {
                    IsWorking = true,
                    WorkIncome = 60000
                },
                Spouse = null,
                AdjustedGrossIncome = 60000,
                AreTaxFilers = true,
                IncomeTaxPaid = 6000,
                UntaxedIncomeAndBenefits = 1000,
                AdditionalFinancialInfo = 200,
                CashSavingsCheckings = 80000,
                InvestmentNetWorth = 5000,
                BusinessFarmNetWorth = 0,
                HasDependents = false,
                MaritalStatus = MaritalStatus.SingleSeparatedDivorced,
                StateOfResidency = UnitedStatesStateOrTerritory.Alabama,
                NumberInHousehold = 1,
                NumberInCollege = 1,
                Age = 25,
                MonthsOfEnrollment = 9
            };

            EfcProfile profile = _efcCalculator.GetIndependentEfcProfile(args);
            Assert.AreEqual(36177, profile.ExpectedFamilyContribution);
        }

        [TestMethod]
        public void GetIndependentEfcProfile_HighValues_Calculated()
        {
            var args = new IndependentEfcCalculatorArguments
            {
                Student = new HouseholdMember
                {
                    IsWorking = true,
                    WorkIncome = 600000
                },
                Spouse = new HouseholdMember
                {
                    IsWorking = true,
                    WorkIncome = 600000
                },
                AdjustedGrossIncome = 1200000,
                AreTaxFilers = true,
                IncomeTaxPaid = 6000,
                UntaxedIncomeAndBenefits = 10000,
                AdditionalFinancialInfo = 2000,
                CashSavingsCheckings = 100000,
                InvestmentNetWorth = 8000,
                BusinessFarmNetWorth = 9000,
                HasDependents = false,
                MaritalStatus = MaritalStatus.MarriedRemarried,
                StateOfResidency = UnitedStatesStateOrTerritory.Alaska,
                NumberInHousehold = 2,
                NumberInCollege = 2,
                Age = 30,
                MonthsOfEnrollment = 9
            };

            EfcProfile profile = _efcCalculator.GetIndependentEfcProfile(args);
            Assert.AreEqual(297710, profile.ExpectedFamilyContribution);
        }

        [TestMethod]
        public void GetIndependentEfcProfile_HasDependentsLowValues_Calculated()
        {
            var args = new IndependentEfcCalculatorArguments
            {
                Student = new HouseholdMember
                {
                    IsWorking = false,
                    WorkIncome = 0
                },
                Spouse = new HouseholdMember
                {
                    IsWorking = false,
                    WorkIncome = 0
                },
                AdjustedGrossIncome = 0,
                AreTaxFilers = false,
                IncomeTaxPaid = 0,
                UntaxedIncomeAndBenefits = 10000,
                AdditionalFinancialInfo = 0,
                CashSavingsCheckings = 0,
                InvestmentNetWorth = 0,
                BusinessFarmNetWorth = 0,
                HasDependents = true,
                MaritalStatus = MaritalStatus.MarriedRemarried,
                StateOfResidency = UnitedStatesStateOrTerritory.AmericanSamoa,
                NumberInHousehold = 3,
                NumberInCollege = 1,
                Age = 25,
                MonthsOfEnrollment = 9
            };

            EfcProfile profile = _efcCalculator.GetIndependentEfcProfile(args);
            Assert.AreEqual(0, profile.ExpectedFamilyContribution);
        }

        [TestMethod]
        public void GetIndependentEfcProfile_HasDependentsValues_Calculated()
        {
            var args = new IndependentEfcCalculatorArguments
            {
                Student = new HouseholdMember
                {
                    IsWorking = true,
                    WorkIncome = 60000
                },
                Spouse = null,
                AdjustedGrossIncome = 60000,
                AreTaxFilers = true,
                IncomeTaxPaid = 6000,
                UntaxedIncomeAndBenefits = 1000,
                AdditionalFinancialInfo = 200,
                CashSavingsCheckings = 80000,
                InvestmentNetWorth = 5000,
                BusinessFarmNetWorth = 0,
                HasDependents = true,
                MaritalStatus = MaritalStatus.SingleSeparatedDivorced,
                StateOfResidency = UnitedStatesStateOrTerritory.Alabama,
                NumberInHousehold = 2,
                NumberInCollege = 1,
                Age = 25,
                MonthsOfEnrollment = 9
            };

            EfcProfile profile = _efcCalculator.GetIndependentEfcProfile(args);
            Assert.AreEqual(5595, profile.ExpectedFamilyContribution);
        }

        [TestMethod]
        public void GetIndependentEfcProfile_HasDependentsHighValues_Calculated()
        {
            var args = new IndependentEfcCalculatorArguments
            {
                Student = new HouseholdMember
                {
                    IsWorking = true,
                    WorkIncome = 600000
                },
                Spouse = new HouseholdMember
                {
                    IsWorking = true,
                    WorkIncome = 600000
                },
                AdjustedGrossIncome = 1200000,
                AreTaxFilers = true,
                IncomeTaxPaid = 6000,
                UntaxedIncomeAndBenefits = 10000,
                AdditionalFinancialInfo = 2000,
                CashSavingsCheckings = 100000,
                InvestmentNetWorth = 8000,
                BusinessFarmNetWorth = 350000,
                HasDependents = true,
                MaritalStatus = MaritalStatus.MarriedRemarried,
                StateOfResidency = UnitedStatesStateOrTerritory.Alaska,
                NumberInHousehold = 20,
                NumberInCollege = 10,
                Age = 30,
                MonthsOfEnrollment = 9
            };

            EfcProfile profile = _efcCalculator.GetIndependentEfcProfile(args);
            Assert.AreEqual(49037, profile.ExpectedFamilyContribution);
        }

        [TestMethod]
        public void GetIndependentEfcProfile_DifferentSpouseIncome_Calculated()
        {
            var args = new IndependentEfcCalculatorArguments
            {
                Student = new HouseholdMember
                {
                    IsWorking = true,
                    WorkIncome = 600000
                },
                Spouse = new HouseholdMember
                {
                    IsWorking = true,
                    WorkIncome = 900000
                },
                AdjustedGrossIncome = 1200000,
                AreTaxFilers = true,
                IncomeTaxPaid = 6000,
                UntaxedIncomeAndBenefits = 10000,
                AdditionalFinancialInfo = 2000,
                CashSavingsCheckings = 100000,
                InvestmentNetWorth = 8000,
                BusinessFarmNetWorth = 350000,
                HasDependents = true,
                MaritalStatus = MaritalStatus.MarriedRemarried,
                StateOfResidency = UnitedStatesStateOrTerritory.Alaska,
                NumberInHousehold = 20,
                NumberInCollege = 10,
                Age = 30,
                MonthsOfEnrollment = 9
            };

            EfcProfile profile = _efcCalculator.GetIndependentEfcProfile(args);
            Assert.AreEqual(48706, profile.ExpectedFamilyContribution);
        }

        [TestMethod]
        public void GetDependentEfcProfile_SimplifiedHighAssets_Calculated()
        {
            DependentEfcCalculatorArguments args = new DependentEfcCalculatorArguments
            {
                FirstParent = new HouseholdMember
                {
                    IsWorking = true,
                    WorkIncome = 25000
                },
                SecondParent = new HouseholdMember
                {
                    IsWorking = true,
                    WorkIncome = 24999
                },
                Student = new HouseholdMember
                {
                    IsWorking = false,
                    WorkIncome = 0
                },
                ParentAdjustedGrossIncome = 49999,
                AreParentsTaxFilers = false,
                ParentIncomeTaxPaid = 0,
                ParentUntaxedIncomeAndBenefits = 0,
                ParentAdditionalFinancialInfo = 0,
                StudentAdjustedGrossIncome = 0,
                IsStudentTaxFiler = false,
                StudentIncomeTaxPaid = 0,
                StudentUntaxedIncomeAndBenefits = 0,
                StudentAdditionalFinancialInfo = 0,
                ParentCashSavingsChecking = 123456789,
                ParentInvestmentNetWorth = 123456789,
                ParentBusinessFarmNetWorth = 123456789,
                StudentCashSavingsChecking = 123456789,
                StudentInvestmentNetWorth = 123456789,
                MaritalStatus = MaritalStatus.MarriedRemarried,
                StateOfResidency = UnitedStatesStateOrTerritory.California,
                NumberInHousehold = 3,
                NumberInCollege = 1,
                OldestParentAge = 30,
                IsQualifiedForSimplified = true,
                MonthsOfEnrollment = 9
            };

            EfcProfile profile = _efcCalculator.GetDependentEfcProfile(args);
            Assert.AreEqual(3281, profile.ExpectedFamilyContribution);
        }

        [TestMethod]
        public void GetIndependentEfcProfile_SimplifiedHighAssets_Calculated()
        {
            var args = new IndependentEfcCalculatorArguments
            {
                Student = new HouseholdMember
                {
                    IsWorking = true,
                    WorkIncome = 25000
                },
                Spouse = new HouseholdMember
                {
                    IsWorking = true,
                    WorkIncome = 24999
                },
                AdjustedGrossIncome = 49999,
                AreTaxFilers = false,
                IncomeTaxPaid = 6000,
                UntaxedIncomeAndBenefits = 0,
                AdditionalFinancialInfo = 0,
                CashSavingsCheckings = 123456789,
                InvestmentNetWorth = 123456789,
                BusinessFarmNetWorth = 350000,
                HasDependents = true,
                MaritalStatus = MaritalStatus.MarriedRemarried,
                StateOfResidency = UnitedStatesStateOrTerritory.Alaska,
                NumberInHousehold = 3,
                NumberInCollege = 1,
                Age = 30,
                IsQualifiedForSimplified = true,
                MonthsOfEnrollment = 9
            };

            EfcProfile profile = _efcCalculator.GetIndependentEfcProfile(args);
            Assert.AreEqual(467, profile.ExpectedFamilyContribution);
        }

        [TestMethod]
        public void GetDependentEfcProfile_AutoZero_Calculated()
        {
            DependentEfcCalculatorArguments args = new DependentEfcCalculatorArguments
            {
                FirstParent = new HouseholdMember
                {
                    IsWorking = true,
                    WorkIncome = 11500
                },
                SecondParent = new HouseholdMember
                {
                    IsWorking = true,
                    WorkIncome = 11500
                },
                Student = new HouseholdMember
                {
                    IsWorking = false,
                    WorkIncome = 0
                },
                ParentAdjustedGrossIncome = 32000,
                AreParentsTaxFilers = false,
                ParentIncomeTaxPaid = 0,
                ParentUntaxedIncomeAndBenefits = 0,
                ParentAdditionalFinancialInfo = 0,
                StudentAdjustedGrossIncome = 0,
                IsStudentTaxFiler = false,
                StudentIncomeTaxPaid = 0,
                StudentUntaxedIncomeAndBenefits = 0,
                StudentAdditionalFinancialInfo = 0,
                ParentCashSavingsChecking = 123456789,
                ParentInvestmentNetWorth = 123456789,
                ParentBusinessFarmNetWorth = 123456789,
                StudentCashSavingsChecking = 123456789,
                StudentInvestmentNetWorth = 123456789,
                MaritalStatus = MaritalStatus.MarriedRemarried,
                StateOfResidency = UnitedStatesStateOrTerritory.California,
                NumberInHousehold = 3,
                NumberInCollege = 1,
                OldestParentAge = 30,
                IsQualifiedForSimplified = true,
                MonthsOfEnrollment = 9
            };

            EfcProfile profile = _efcCalculator.GetDependentEfcProfile(args);
            Assert.AreEqual(0, profile.ExpectedFamilyContribution);
        }

        [TestMethod]
        public void GetIndependentEfcProfile_AutoZero_Calculated()
        {
            var args = new IndependentEfcCalculatorArguments
            {
                Student = new HouseholdMember
                {
                    IsWorking = true,
                    WorkIncome = 11500
                },
                Spouse = new HouseholdMember
                {
                    IsWorking = true,
                    WorkIncome = 11500
                },
                AdjustedGrossIncome = 32000,
                AreTaxFilers = false,
                IncomeTaxPaid = 6000,
                UntaxedIncomeAndBenefits = 999999999999,
                AdditionalFinancialInfo = 0,
                CashSavingsCheckings = 123456789,
                InvestmentNetWorth = 123456789,
                BusinessFarmNetWorth = 350000,
                HasDependents = true,
                MaritalStatus = MaritalStatus.MarriedRemarried,
                StateOfResidency = UnitedStatesStateOrTerritory.Alaska,
                NumberInHousehold = 3,
                NumberInCollege = 1,
                Age = 30,
                IsQualifiedForSimplified = true,
                MonthsOfEnrollment = 9
            };

            EfcProfile profile = _efcCalculator.GetIndependentEfcProfile(args);
            Assert.AreEqual(0, profile.ExpectedFamilyContribution);
        }

        [TestMethod]
        public void GetDependentEfcProfile_NoMonthsOfEnrollment_Calculated()
        {
            DependentEfcCalculatorArguments args = new DependentEfcCalculatorArguments
            {
                FirstParent = null,
                SecondParent = new HouseholdMember
                {
                    IsWorking = true,
                    WorkIncome = 60000
                },
                Student = new HouseholdMember
                {
                    IsWorking = true,
                    WorkIncome = 10000
                },
                ParentAdjustedGrossIncome = 60000,
                AreParentsTaxFilers = true,
                ParentIncomeTaxPaid = 6000,
                ParentUntaxedIncomeAndBenefits = 1000,
                ParentAdditionalFinancialInfo = 200,
                StudentAdjustedGrossIncome = 10000,
                IsStudentTaxFiler = true,
                StudentIncomeTaxPaid = 1000,
                StudentUntaxedIncomeAndBenefits = 0,
                StudentAdditionalFinancialInfo = 0,
                ParentCashSavingsChecking = 80000,
                ParentInvestmentNetWorth = 5000,
                ParentBusinessFarmNetWorth = 0,
                StudentCashSavingsChecking = 3000,
                StudentInvestmentNetWorth = 0,
                MaritalStatus = MaritalStatus.SingleSeparatedDivorced,
                StateOfResidency = UnitedStatesStateOrTerritory.California,
                NumberInHousehold = 3,
                NumberInCollege = 1,
                OldestParentAge = 45,
                MonthsOfEnrollment = 0
            };

            EfcProfile profile = _efcCalculator.GetDependentEfcProfile(args);
            Assert.AreEqual(0, profile.ExpectedFamilyContribution);
        }

        [TestMethod]
        public void GetDependentEfcProfile_ThreeMonthsEnrollment_Calculated()
        {
            DependentEfcCalculatorArguments args = new DependentEfcCalculatorArguments
            {
                FirstParent = null,
                SecondParent = new HouseholdMember
                {
                    IsWorking = true,
                    WorkIncome = 60000
                },
                Student = new HouseholdMember
                {
                    IsWorking = true,
                    WorkIncome = 10000
                },
                ParentAdjustedGrossIncome = 60000,
                AreParentsTaxFilers = true,
                ParentIncomeTaxPaid = 6000,
                ParentUntaxedIncomeAndBenefits = 1000,
                ParentAdditionalFinancialInfo = 200,
                StudentAdjustedGrossIncome = 10000,
                IsStudentTaxFiler = true,
                StudentIncomeTaxPaid = 1000,
                StudentUntaxedIncomeAndBenefits = 0,
                StudentAdditionalFinancialInfo = 0,
                ParentCashSavingsChecking = 80000,
                ParentInvestmentNetWorth = 5000,
                ParentBusinessFarmNetWorth = 0,
                StudentCashSavingsChecking = 3000,
                StudentInvestmentNetWorth = 0,
                MaritalStatus = MaritalStatus.SingleSeparatedDivorced,
                StateOfResidency = UnitedStatesStateOrTerritory.California,
                NumberInHousehold = 3,
                NumberInCollege = 1,
                OldestParentAge = 45,
                MonthsOfEnrollment = 3
            };

            EfcProfile profile = _efcCalculator.GetDependentEfcProfile(args);
            Assert.AreEqual(2307, profile.ParentContribution);
            Assert.AreEqual(732, profile.StudentContribution);
        }

        [TestMethod]
        public void GetDependentEfcProfile_TwelveMonthsEnrollment_Calculated()
        {
            DependentEfcCalculatorArguments args = new DependentEfcCalculatorArguments
            {
                FirstParent = null,
                SecondParent = new HouseholdMember
                {
                    IsWorking = true,
                    WorkIncome = 60000
                },
                Student = new HouseholdMember
                {
                    IsWorking = true,
                    WorkIncome = 10000
                },
                ParentAdjustedGrossIncome = 60000,
                AreParentsTaxFilers = true,
                ParentIncomeTaxPaid = 6000,
                ParentUntaxedIncomeAndBenefits = 1000,
                ParentAdditionalFinancialInfo = 200,
                StudentAdjustedGrossIncome = 10000,
                IsStudentTaxFiler = true,
                StudentIncomeTaxPaid = 1000,
                StudentUntaxedIncomeAndBenefits = 0,
                StudentAdditionalFinancialInfo = 0,
                ParentCashSavingsChecking = 80000,
                ParentInvestmentNetWorth = 5000,
                ParentBusinessFarmNetWorth = 0,
                StudentCashSavingsChecking = 3000,
                StudentInvestmentNetWorth = 0,
                MaritalStatus = MaritalStatus.SingleSeparatedDivorced,
                StateOfResidency = UnitedStatesStateOrTerritory.California,
                NumberInHousehold = 3,
                NumberInCollege = 1,
                OldestParentAge = 45,
                MonthsOfEnrollment = 12
            };

            EfcProfile profile = _efcCalculator.GetDependentEfcProfile(args);
            Assert.AreEqual(7423, profile.ParentContribution);
            Assert.AreEqual(998, profile.StudentContribution);
        }

        [TestMethod]
        public void GetIndependentEfcProfile_NoMonthsOfEnrollment_Calculated()
        {
            var args = new IndependentEfcCalculatorArguments
            {
                Student = new HouseholdMember
                {
                    IsWorking = true,
                    WorkIncome = 60000
                },
                Spouse = null,
                AdjustedGrossIncome = 60000,
                AreTaxFilers = true,
                IncomeTaxPaid = 6000,
                UntaxedIncomeAndBenefits = 1000,
                AdditionalFinancialInfo = 200,
                CashSavingsCheckings = 80000,
                InvestmentNetWorth = 5000,
                BusinessFarmNetWorth = 0,
                HasDependents = false,
                MaritalStatus = MaritalStatus.SingleSeparatedDivorced,
                StateOfResidency = UnitedStatesStateOrTerritory.Alabama,
                NumberInHousehold = 1,
                NumberInCollege = 1,
                Age = 25,
                MonthsOfEnrollment = 0
            };

            EfcProfile profile = _efcCalculator.GetIndependentEfcProfile(args);
            Assert.AreEqual(0, profile.ExpectedFamilyContribution);
        }

        [TestMethod]
        public void GetIndependentEfcProfile_ThreeMonthsEnrollment_Calculated()
        {
            var args = new IndependentEfcCalculatorArguments
            {
                Student = new HouseholdMember
                {
                    IsWorking = true,
                    WorkIncome = 60000
                },
                Spouse = null,
                AdjustedGrossIncome = 60000,
                AreTaxFilers = true,
                IncomeTaxPaid = 6000,
                UntaxedIncomeAndBenefits = 1000,
                AdditionalFinancialInfo = 200,
                CashSavingsCheckings = 80000,
                InvestmentNetWorth = 5000,
                BusinessFarmNetWorth = 0,
                HasDependents = false,
                MaritalStatus = MaritalStatus.SingleSeparatedDivorced,
                StateOfResidency = UnitedStatesStateOrTerritory.Alabama,
                NumberInHousehold = 1,
                NumberInCollege = 1,
                Age = 25,
                MonthsOfEnrollment = 3
            };

            EfcProfile profile = _efcCalculator.GetIndependentEfcProfile(args);
            Assert.AreEqual(12060, profile.ExpectedFamilyContribution);
        }
    }
}