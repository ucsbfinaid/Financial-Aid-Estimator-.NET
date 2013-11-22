using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation.Constants;

namespace Ucsb.Sa.FinAid.AidEstimation.EfcCalculation.Test
{
    [TestClass]
    public class AllowanceCalculatorTests
    {
        private AllowanceCalculator _allowanceCalculator;

        [TestInitialize]
        public void Init()
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

            _allowanceCalculator = new AllowanceCalculator(constants);
        }

        [TestMethod]
        public void CalculateIncomeTaxAllowance_Value_Calculated()
        {
            double result = _allowanceCalculator.CalculateIncomeTaxAllowance(1000);
            Assert.AreEqual(1000, result);
        }

        [TestMethod]
        public void CalculateIncomeTaxAllowance_NegativeValue_EqualsZero()
        {
            double result = _allowanceCalculator.CalculateIncomeTaxAllowance(-1000);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void CalculateIncomeTaxAllowance_DecimalValue_Rounded()
        {
            double result = _allowanceCalculator.CalculateIncomeTaxAllowance(2000.55);
            Assert.AreEqual(2001, result);
        }

        [TestMethod]
        public void CalculateStateTaxAllowance_HighStateTaxPercent_Calculated()
        {
            double result = _allowanceCalculator.CalculateStateTaxAllowance(EfcCalculationRole.Parent, UnitedStatesStateOrTerritory.California, 14999);
            Assert.AreEqual(1200, result);
        }

        [TestMethod]
        public void CalculateStateTaxAllowance_LowStateTaxPercent_Calculated()
        {
            double result = _allowanceCalculator.CalculateStateTaxAllowance(EfcCalculationRole.Parent, UnitedStatesStateOrTerritory.California, 15000);
            Assert.AreEqual(1050, result);
        }

        [TestMethod]
        public void CalculateStateTaxAllowance_NegativeTotalIncome_EqualsZero()
        {
            double result = _allowanceCalculator.CalculateStateTaxAllowance(EfcCalculationRole.Parent, UnitedStatesStateOrTerritory.California, -15000);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void CalculateStateTaxAllowance_DependentStudent_Calculated()
        {
            double result = _allowanceCalculator.CalculateStateTaxAllowance(EfcCalculationRole.DependentStudent, UnitedStatesStateOrTerritory.California, 3000);
            Assert.AreEqual(150, result);
        }

        [TestMethod]
        public void CalculateStateTaxAllowance_IndependentStudentWithDep_Calculated()
        {
            double result = _allowanceCalculator.CalculateStateTaxAllowance(EfcCalculationRole.IndependentStudentWithDependents, UnitedStatesStateOrTerritory.California, 3000);
            Assert.AreEqual(240, result);
        }

        [TestMethod]
        public void CalculateStateTaxAllowance_IndependentStudentWithoutDep_Calculated()
        {
            double result = _allowanceCalculator.CalculateStateTaxAllowance(EfcCalculationRole.IndependentStudentWithoutDependents, UnitedStatesStateOrTerritory.California, 3000);
            Assert.AreEqual(150, result);
        }

        [TestMethod]
        public void CalculateStateTaxAllowance_DecimalValue_Rounded()
        {
            double result = _allowanceCalculator.CalculateStateTaxAllowance(EfcCalculationRole.Parent, UnitedStatesStateOrTerritory.California, 3000.55);
            Assert.AreEqual(240, result);
        }

        [TestMethod]
        public void CalculateSocialSecurityTaxAllowance_LowRange_Calculated()
        {
            double result = _allowanceCalculator.CalculateSocialSecurityTaxAllowance(106800);
            Assert.AreEqual(8170, result);
        }

        [TestMethod]
        public void CalculateSocialSecurityTaxAllowance_HighRange_Calculated()
        {
            double result = _allowanceCalculator.CalculateSocialSecurityTaxAllowance(206800);
            Assert.AreEqual(9825, result);
        }

        [TestMethod]
        public void CalculateSocialSecurityTaxAllowance_NegativeValue_EqualsZero()
        {
            double result = _allowanceCalculator.CalculateSocialSecurityTaxAllowance(-206800);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void CalculateSocialSecurityTaxAllowance_DecimalValue_Rounded()
        {
            double result = _allowanceCalculator.CalculateSocialSecurityTaxAllowance(206800.56);
            Assert.AreEqual(9825, result);
        }

        [TestMethod]
        public void CalculateIncomeProtectionAllowance_ZeroValue_EqualsZero()
        {
            double result = _allowanceCalculator
                .CalculateIncomeProtectionAllowance(EfcCalculationRole.Parent, MaritalStatus.MarriedRemarried, 0, 0);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void CalculateIncomeProtectionAllowance_Value_Calculated()
        {
            double result = _allowanceCalculator
                .CalculateIncomeProtectionAllowance(EfcCalculationRole.Parent, MaritalStatus.MarriedRemarried, 2, 3);
            Assert.AreEqual(18380, result);
        }

        [TestMethod]
        public void CalculateIncomeProtectionAllowance_TooManyInCollege_EqualsZero()
        {
            double result = _allowanceCalculator
                .CalculateIncomeProtectionAllowance(EfcCalculationRole.Parent, MaritalStatus.MarriedRemarried, 6, 3);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void CalculateIncomeProtectionAllowance_AddtlHousehold_Calculated()
        {
            double result = _allowanceCalculator
                .CalculateIncomeProtectionAllowance(EfcCalculationRole.Parent, MaritalStatus.MarriedRemarried, 5, 20);
            Assert.AreEqual(82020, result);
        }

        [TestMethod]
        public void CalculateIncomeProtectionAllowance_AddtlCollege_Calculated()
        {
            double result = _allowanceCalculator
                .CalculateIncomeProtectionAllowance(EfcCalculationRole.Parent, MaritalStatus.MarriedRemarried, 10, 20);
            Assert.AreEqual(67470, result);
        }

        [TestMethod]
        public void CalculateIncomeProtectionAllowance_IndependentStudentWithDep_Calculated()
        {
            double result = _allowanceCalculator
                .CalculateIncomeProtectionAllowance(EfcCalculationRole.IndependentStudentWithDependents, MaritalStatus.MarriedRemarried, 10, 20);
            Assert.AreEqual(95140, result);
        }

        [TestMethod]
        public void CalculateIncomeProtectionAllowance_IndependentStudentWithoutDep_Calculated()
        {
            double result = _allowanceCalculator
                .CalculateIncomeProtectionAllowance(EfcCalculationRole.IndependentStudentWithoutDependents, MaritalStatus.MarriedRemarried, 1, 1);
            Assert.AreEqual(15290, result);
        }

        [TestMethod]
        public void CalculateIncomeProtectionAllowance_SingleIndependentStudentWithoutDep_Calculated()
        {
            double result = _allowanceCalculator
                .CalculateIncomeProtectionAllowance(EfcCalculationRole.IndependentStudentWithoutDependents, MaritalStatus.SingleSeparatedDivorced, 1, 1);
            Assert.AreEqual(9540, result);
        }

        [TestMethod]
        public void CalculateIncomeProtectionAllowance_SingleIndependentStudentWithoutDepWithSpouseInCollege_Calculated()
        {
            double result = _allowanceCalculator
                .CalculateIncomeProtectionAllowance(EfcCalculationRole.IndependentStudentWithoutDependents, MaritalStatus.MarriedRemarried, 2, 2);
            Assert.AreEqual(9540, result);
        }

        [TestMethod]
        public void CalculateIncomeProtectionAllowance_DependentStudent_Calculated()
        {
            double result = _allowanceCalculator.CalculateIncomeProtectionAllowance(EfcCalculationRole.DependentStudent, MaritalStatus.MarriedRemarried, 10, 20);
            Assert.AreEqual(6130, result);
        }

        [TestMethod]
        public void CalculateEmploymentExpenseAllowance_TwoWorkingIncomeOverThreshold_Calculated()
        {
            List<HouseholdMember> parents = new List<HouseholdMember>
                                                 {
                                                     new HouseholdMember { IsWorking = true, WorkIncome = 20000},
                                                     new HouseholdMember { IsWorking = true, WorkIncome = 40000}
                                                 };

            double result = _allowanceCalculator
                .CalculateEmploymentExpenseAllowance(EfcCalculationRole.Parent, MaritalStatus.MarriedRemarried, parents);
            Assert.AreEqual(3900, result);
        }

        [TestMethod]
        public void CalculateEmploymentExpenseAllowance_TwoWorkingIncomeUnderThreshold_Calculated()
        {
            List<HouseholdMember> parents = new List<HouseholdMember>
                                                 {
                                                     new HouseholdMember { IsWorking = true, WorkIncome = 2000},
                                                     new HouseholdMember { IsWorking = true, WorkIncome = 4000}
                                                 };

            double result = _allowanceCalculator
                .CalculateEmploymentExpenseAllowance(EfcCalculationRole.Parent, MaritalStatus.MarriedRemarried, parents);
            Assert.AreEqual(700, result);
        }

        [TestMethod]
        public void CalculateEmploymentExpenseAllowance_OneParent_Calculated()
        {
            List<HouseholdMember> parents = new List<HouseholdMember>
                                                 {
                                                     new HouseholdMember { IsWorking = true, WorkIncome = 20000}
                                                 };

            double result = _allowanceCalculator
                .CalculateEmploymentExpenseAllowance(EfcCalculationRole.Parent, MaritalStatus.SingleSeparatedDivorced, parents);
            Assert.AreEqual(3900, result);
        }

        [TestMethod]
        public void CalculateEmploymentExpenseAllowance_OneWorking_EqualsZero()
        {
            List<HouseholdMember> parents = new List<HouseholdMember>
                                                 {
                                                     new HouseholdMember { IsWorking = false, WorkIncome = 2000},
                                                     new HouseholdMember { IsWorking = true, WorkIncome = 4000}
                                                 };

            double result = _allowanceCalculator
                .CalculateEmploymentExpenseAllowance(EfcCalculationRole.Parent, MaritalStatus.MarriedRemarried, parents);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void CalculateEmploymentExpenseAllowance_NoParents_EqualsZero()
        {
            List<HouseholdMember> parents = new List<HouseholdMember>();
            double result = _allowanceCalculator
                .CalculateEmploymentExpenseAllowance(EfcCalculationRole.Parent, MaritalStatus.MarriedRemarried, parents);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void CalculateEmploymentExpenseAllowance_NullParents_EqualsZero()
        {
            double result = _allowanceCalculator
                .CalculateEmploymentExpenseAllowance(EfcCalculationRole.Parent, MaritalStatus.MarriedRemarried, null);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void CalculateEmploymentExpenseAllowance_SingleIndependentStudentWithoutDep_Calculated()
        {
            List<HouseholdMember> adults = new List<HouseholdMember>
                                                 {
                                                     new HouseholdMember { IsWorking = true, WorkIncome = 2000},
                                                 };

            double result = _allowanceCalculator
                .CalculateEmploymentExpenseAllowance(EfcCalculationRole.IndependentStudentWithoutDependents, MaritalStatus.SingleSeparatedDivorced, adults);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void CalculateEmploymentExpenseAllowance_DependentStudent_EqualsZero()
        {
            List<HouseholdMember> adults = new List<HouseholdMember>
                                                 {
                                                     new HouseholdMember { IsWorking = true, WorkIncome = 2000},
                                                 };

            double result = _allowanceCalculator
                .CalculateEmploymentExpenseAllowance(EfcCalculationRole.DependentStudent, MaritalStatus.SingleSeparatedDivorced, adults);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void CalculateEmploymentExpenseAllowance_TooManyPeople_Calculated()
        {
            List<HouseholdMember> adults = new List<HouseholdMember>
                                                 {
                                                     new HouseholdMember { IsWorking = true, WorkIncome = 6000},
                                                     new HouseholdMember { IsWorking = true, WorkIncome = 5000},
                                                     new HouseholdMember { IsWorking = true, WorkIncome = 4000},
                                                     new HouseholdMember { IsWorking = true, WorkIncome = 3000},
                                                 };

            double result = _allowanceCalculator
                .CalculateEmploymentExpenseAllowance(EfcCalculationRole.Parent, MaritalStatus.MarriedRemarried, adults);
            Assert.AreEqual(1050, result);
        }

        [TestMethod]
        public void CalculateEmploymentExpenseAllowance_NegativeValue_EqualsZero()
        {
            List<HouseholdMember> parents = new List<HouseholdMember>
                                                 {
                                                     new HouseholdMember { IsWorking = true, WorkIncome = -2000},
                                                     new HouseholdMember { IsWorking = true, WorkIncome = -4000}
                                                 };

            double result = _allowanceCalculator
                .CalculateEmploymentExpenseAllowance(EfcCalculationRole.Parent, MaritalStatus.MarriedRemarried, parents);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void CalculateEmploymentExpenseAllowance_DecimalValue_Rounded()
        {
            List<HouseholdMember> parents = new List<HouseholdMember>
                                                 {
                                                     new HouseholdMember { IsWorking = true, WorkIncome = 2000.55},
                                                     new HouseholdMember { IsWorking = true, WorkIncome = 4000.86}
                                                 };

            double result = _allowanceCalculator
                .CalculateEmploymentExpenseAllowance(EfcCalculationRole.Parent, MaritalStatus.MarriedRemarried, parents);
            Assert.AreEqual(700, result);
        }

        [TestMethod]
        public void CalculateEmploymentExpenseAllowance_BelowThreshold_Rounded()
        {
            List<HouseholdMember> parents = new List<HouseholdMember>
                                                 {
                                                     new HouseholdMember { IsWorking = true, WorkIncome = 10000}
                                                 };

            double result = _allowanceCalculator
                .CalculateEmploymentExpenseAllowance(EfcCalculationRole.Parent, MaritalStatus.MarriedRemarried, parents);
            Assert.AreEqual(3500, result);
        }

        [TestMethod]
        public void CalculateTotalAllowances_Parent_Calculated()
        {
            List<HouseholdMember> parents = new List<HouseholdMember>
                                                 {
                                                     new HouseholdMember { IsWorking = true, WorkIncome = 15000},
                                                     new HouseholdMember { IsWorking = true, WorkIncome = 20000}
                                                 };

            double result = _allowanceCalculator.CalculateTotalAllowances(
                EfcCalculationRole.Parent,
                MaritalStatus.MarriedRemarried,
                UnitedStatesStateOrTerritory.California,
                2,
                3,
                parents,
                45000,
                3000);

            Assert.AreEqual(31108, result);
        }

        [TestMethod]
        public void CalculateTotalAllowances_IndependentStudentWithoutDep_Calculated()
        {
            List<HouseholdMember> persons = new List<HouseholdMember>
                                                 {
                                                     new HouseholdMember { IsWorking = true, WorkIncome = 30000 }
                                                 };

            double result = _allowanceCalculator.CalculateTotalAllowances(
                EfcCalculationRole.IndependentStudentWithoutDependents,
                MaritalStatus.SingleSeparatedDivorced,
                UnitedStatesStateOrTerritory.California,
                1,
                1,
                persons,
                35000,
                4000);

            Assert.AreEqual(17585, result);
        }

        [TestMethod]
        public void CalculateTotalAllowances_IndependentStudentWithDep_Calculated()
        {
            List<HouseholdMember> persons = new List<HouseholdMember>
                                                 {
                                                     new HouseholdMember { IsWorking = false, WorkIncome = 0},
                                                     new HouseholdMember { IsWorking = true, WorkIncome = 20000}
                                                 };

            double result = _allowanceCalculator.CalculateTotalAllowances(
                EfcCalculationRole.IndependentStudentWithDependents,
                MaritalStatus.MarriedRemarried,
                UnitedStatesStateOrTerritory.California,
                1,
                2,
                persons,
                25000,
                3000);

            Assert.AreEqual(30430, result);
        }

        [TestMethod]
        public void CalculateTotalAllowances_DependentStudent_Calculated()
        {
            List<HouseholdMember> persons = new List<HouseholdMember>
                                                 {
                                                     new HouseholdMember { IsWorking = true, WorkIncome = 10000 }
                                                 };

            double result = _allowanceCalculator.CalculateTotalAllowances(
                EfcCalculationRole.DependentStudent,
                MaritalStatus.SingleSeparatedDivorced,
                UnitedStatesStateOrTerritory.California,
                1,
                1,
                persons,
                10000,
                1000);

            Assert.AreEqual(8395, result);
        }
    }
}