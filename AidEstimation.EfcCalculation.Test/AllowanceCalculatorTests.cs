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
            AllowanceCalculatorConstants constants = TestConstantsFactory.GetAllowanceCalculatorConstants();
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
            Assert.AreEqual(180, result);
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
            Assert.AreEqual(180, result);
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
        public void CalculateSocialSecurityTaxAllowance_MidRange_Calculated()
        {
            double result = _allowanceCalculator.CalculateSocialSecurityTaxAllowance(136000);
            Assert.AreEqual(9933, result);
        }

        [TestMethod]
        public void CalculateSocialSecurityTaxAllowance_HighRange_Calculated()
        {
            double result = _allowanceCalculator.CalculateSocialSecurityTaxAllowance(206800);
            Assert.AreEqual(11021, result);
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
            Assert.AreEqual(11021, result);
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
            Assert.AreEqual(20510, result);
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
            Assert.AreEqual(93970, result);
        }

        [TestMethod]
        public void CalculateIncomeProtectionAllowance_AddtlCollege_Calculated()
        {
            double result = _allowanceCalculator
                .CalculateIncomeProtectionAllowance(EfcCalculationRole.Parent, MaritalStatus.MarriedRemarried, 10, 20);
            Assert.AreEqual(77720, result);
        }

        [TestMethod]
        public void CalculateIncomeProtectionAllowance_IndependentStudentWithDep_Calculated()
        {
            double result = _allowanceCalculator
                .CalculateIncomeProtectionAllowance(EfcCalculationRole.IndependentStudentWithDependents, MaritalStatus.MarriedRemarried, 10, 20);
            Assert.AreEqual(106190, result);
        }

        [TestMethod]
        public void CalculateIncomeProtectionAllowance_IndependentStudentWithoutDep_Calculated()
        {
            double result = _allowanceCalculator
                .CalculateIncomeProtectionAllowance(EfcCalculationRole.IndependentStudentWithoutDependents, MaritalStatus.MarriedRemarried, 1, 1);
            Assert.AreEqual(17060, result);
        }

        [TestMethod]
        public void CalculateIncomeProtectionAllowance_SingleIndependentStudentWithoutDep_Calculated()
        {
            double result = _allowanceCalculator
                .CalculateIncomeProtectionAllowance(EfcCalculationRole.IndependentStudentWithoutDependents, MaritalStatus.SingleSeparatedDivorced, 1, 1);
            Assert.AreEqual(10640, result);
        }

        [TestMethod]
        public void CalculateIncomeProtectionAllowance_SingleIndependentStudentWithoutDepWithSpouseInCollege_Calculated()
        {
            double result = _allowanceCalculator
                .CalculateIncomeProtectionAllowance(EfcCalculationRole.IndependentStudentWithoutDependents, MaritalStatus.MarriedRemarried, 2, 2);
            Assert.AreEqual(10640, result);
        }

        [TestMethod]
        public void CalculateIncomeProtectionAllowance_DependentStudent_Calculated()
        {
            double result = _allowanceCalculator.CalculateIncomeProtectionAllowance(EfcCalculationRole.DependentStudent, MaritalStatus.MarriedRemarried, 10, 20);
            Assert.AreEqual(6840, result);
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
            Assert.AreEqual(4000, result);
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
            Assert.AreEqual(4000, result);
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

            Assert.AreEqual(33338, result);
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

            Assert.AreEqual(19035, result);
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

            Assert.AreEqual(33220, result);
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

            Assert.AreEqual(9205, result);
        }
    }
}