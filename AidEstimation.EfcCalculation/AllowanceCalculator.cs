using System;
using System.Collections.Generic;
using System.Linq;
using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation.Constants;

namespace Ucsb.Sa.FinAid.AidEstimation.EfcCalculation
{
    /// <summary>
    /// Total Allowances calculator
    /// </summary>
    public class AllowanceCalculator
    {
        private readonly AllowanceCalculatorConstants _constants;

        /// <summary>
        /// Constructs a new Total Allowances calculator
        /// </summary>
        /// <param name="constants">Constants used in the calculation of Total Allowances</param>
        public AllowanceCalculator(AllowanceCalculatorConstants constants)
        {
            _constants = constants;
        }

        /// <summary>
        /// Calculates Total Allowances
        /// </summary>
        /// <param name="role">Subject's role within the calculation</param>
        /// <param name="maritalStatus">Marital status</param>
        /// <param name="stateOfResidency">State of residency</param>
        /// <param name="numInCollege">Number in college</param>
        /// <param name="numInHousehold">Number in household</param>
        /// <param name="employablePersons">People capable of employment. Exact definition varies depending on
        /// role. If the role is "Parent", for example, <see cref="employablePersons"/> refers to the parents.
        /// If the role is "IndependentStudent", <see cref="employablePersons"/> refers to the student and
        /// spouse</param>
        /// <param name="totalIncome">Total income</param>
        /// <param name="incomeTaxPaid">U.S. income tax paid</param>
        /// <returns>"Total Allowances"</returns>
        public double CalculateTotalAllowances(
            EfcCalculationRole role,
            MaritalStatus maritalStatus,
            UnitedStatesStateOrTerritory stateOfResidency,
            int numInCollege,
            int numInHousehold,
            List<HouseholdMember> employablePersons,
            double totalIncome,
            double incomeTaxPaid)
        {
            double totalAllowances = 0;

            // Income Tax Paid
            totalAllowances += CalculateIncomeTaxAllowance(incomeTaxPaid);

            // State Tax
            totalAllowances += CalculateStateTaxAllowance(role, stateOfResidency, totalIncome);

            // Social Security Tax
            totalAllowances += employablePersons
                                    .Where(person => person.IsWorking)
                                    .Sum(person => CalculateSocialSecurityTaxAllowance(person.WorkIncome));

            // Employment Expense Allowance
            if (role != EfcCalculationRole.DependentStudent)
            {
                totalAllowances += CalculateEmploymentExpenseAllowance(role, maritalStatus, employablePersons);
            }

            // Income Protection Allowance
            totalAllowances += CalculateIncomeProtectionAllowance(role, maritalStatus, numInCollege, numInHousehold);

            return Math.Round(totalAllowances < 0 ? 0 : totalAllowances, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Calculates Income Tax Paid allowance
        /// </summary>
        /// <param name="incomeTaxPaid">U.S. income tax paid</param>
        /// <returns>Income Tax Paid allowance</returns>
        public double CalculateIncomeTaxAllowance(double incomeTaxPaid)
        {
            return Math.Round(incomeTaxPaid < 0 ? 0 : incomeTaxPaid, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Calculates State and Other Tax allowance
        /// </summary>
        /// <param name="role">Subject's role within the calculation</param>
        /// <param name="stateOfResidency">State of residency</param>
        /// <param name="totalIncome">Total income</param>
        /// <returns>State and Other Tax allowance</returns>
        public double CalculateStateTaxAllowance(
            EfcCalculationRole role,
            UnitedStatesStateOrTerritory stateOfResidency,
            double totalIncome)
        {
            double stateTaxAllowance = 0;

            switch (role)
            {
                // For Parents and Independent Students With Dependents, use the "Parent" State Tax Allowance chart
                case EfcCalculationRole.Parent:
                case EfcCalculationRole.IndependentStudentWithDependents:
                    if (totalIncome < _constants.StateTaxAllowanceIncomeThreshold)
                    {
                        stateTaxAllowance =
                            (_constants.ParentStateTaxAllowancePercents[(int)stateOfResidency] * 0.01) * totalIncome;
                    }
                    else
                    {
                        stateTaxAllowance =
                            ((_constants.ParentStateTaxAllowancePercents[(int)stateOfResidency] - 1) * 0.01)
                                * totalIncome;
                    }
                    break;

                // For Dependent Students and Independent Students Without Dependents, use the "Student" State Tax Allowance chart
                case EfcCalculationRole.DependentStudent:
                case EfcCalculationRole.IndependentStudentWithoutDependents:
                    stateTaxAllowance =
                        (_constants.StudentStateTaxAllowancePercents[(int)stateOfResidency] * 0.01) * totalIncome;
                    break;
            }

            return Math.Round(stateTaxAllowance < 0 ? 0 : stateTaxAllowance, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Calculates Social Security Tax allowance
        /// </summary>
        /// <param name="workIncome">Income earned from work</param>
        /// <returns>Social Security Tax allowance</returns>
        public double CalculateSocialSecurityTaxAllowance(double workIncome)
        {
            double socialSecurityTaxAllowance;

            double socialSecurityTaxBase = 0;
            double socialSecurityTaxPercentage = 0;
            double socialSecurityTaxThreshold = 0;

            for(int i = 0; i < _constants.SocialSecurityTaxIncomeThresholds.Length; i++)
            {
                if (workIncome > _constants.SocialSecurityTaxIncomeThresholds[i])
                {
                    socialSecurityTaxThreshold = _constants.SocialSecurityTaxIncomeThresholds[i];
                    socialSecurityTaxBase = _constants.SocialSecurityTaxBases[i];
                    socialSecurityTaxPercentage = _constants.SocialSecurityTaxPercentages[i];
                }
            }

            socialSecurityTaxAllowance = ((workIncome - socialSecurityTaxThreshold) * socialSecurityTaxPercentage) + socialSecurityTaxBase;

            return Math.Round(socialSecurityTaxAllowance < 0 ? 0 : socialSecurityTaxAllowance,
                MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Calculates Income Protection Allowance
        /// </summary>
        /// <param name="role">Subject's role within the calculation</param>
        /// <param name="maritalStatus">Marital status</param>
        /// <param name="numInCollege">Number in college</param>
        /// <param name="numInHousehold">Number in household</param>
        /// <returns>"Income Protection Allowance"</returns>
        public double CalculateIncomeProtectionAllowance(
            EfcCalculationRole role,
            MaritalStatus maritalStatus,
            int numInCollege,
            int numInHousehold)
        {
            if (numInCollege > numInHousehold || numInCollege <= 0 || numInHousehold <= 0)
            {
                return 0;
            }

            double incomeProtectionAllowance = 0;

            switch (role)
            {
                case EfcCalculationRole.IndependentStudentWithDependents:
                case EfcCalculationRole.Parent:

                    // Determine the appropriate charts to use for Income Protection Allowance values
                    int[,] incomeProtectionAllowances = (role == EfcCalculationRole.Parent)
                        ? _constants.DependentParentIncomeProtectionAllowances
                        : _constants.IndependentWithDependentsIncomeProtectionAllowances;

                    int additionalStudentAllowance = (role == EfcCalculationRole.Parent)
                        ? _constants.DependentAdditionalStudentAllowance
                        : _constants.IndependentAdditionalStudentAllowance;

                    int additionalFamilyAllowance = (role == EfcCalculationRole.Parent)
                        ? _constants.DependentAdditionalFamilyAllowance
                        : _constants.IndependentAdditionalFamilyAllowance;

                    // If number of children in the household exceeds table range, add additionalFamilyAllowance
                    // for each additional child
                    int maxHouseholdCount = incomeProtectionAllowances.GetLength(0) - 1;
                    int householdCount = numInHousehold;

                    if (numInHousehold > maxHouseholdCount)
                    {
                        // Set number in household to maximum table range
                        householdCount = maxHouseholdCount;
                        incomeProtectionAllowance += (numInHousehold - maxHouseholdCount) * additionalFamilyAllowance;
                    }

                    // If number of children in college exceeds table range, subtract additionalStudentAllowance
                    // for each additional child
                    int maxCollegeCount = incomeProtectionAllowances.GetLength(1) - 1;
                    int collegeCount = numInCollege;

                    if (numInCollege > maxCollegeCount)
                    {
                        // Set number in college to maximum table range
                        collegeCount = maxCollegeCount;
                        incomeProtectionAllowance -= (numInCollege - maxCollegeCount) * additionalStudentAllowance;
                    }

                    // Add standard incomeProtectionAllowance value
                    incomeProtectionAllowance += incomeProtectionAllowances[householdCount, collegeCount];

                    break;

                case EfcCalculationRole.IndependentStudentWithoutDependents:

                    if (maritalStatus == MaritalStatus.SingleSeparatedDivorced)
                    {
                        incomeProtectionAllowance
                            = _constants.SingleIndependentWithoutDependentsIncomeProtectionAllowance;
                    }
                    else
                    {
                        // If spouse is enrolled at least 1/2 time, then use the Income Protection Allowance value
                        // for Single/Separated/Divorced
                        incomeProtectionAllowance = (numInCollege > 1) ?
                            _constants.SingleIndependentWithoutDependentsIncomeProtectionAllowance
                                : _constants.MarriedIndependentWithoutDependentsIncomeProtectionAllowance;
                    }

                    break;

                case EfcCalculationRole.DependentStudent:

                    incomeProtectionAllowance = _constants.DependentStudentIncomeProtectionAllowance;

                    break;
            }

            return Math.Round(incomeProtectionAllowance < 0 ? 0 : incomeProtectionAllowance,
                MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Calculates "Employmement Expense Allowance"
        /// </summary>
        /// <param name="role">Subject's role within the calculation</param>
        /// <param name="maritalStatus">Marital status</param>
        /// <param name="employablePersons">People capable of employment. Exact definition varies depending on
        /// role. If the role is "Parent", for example, <see cref="employablePersons"/> refers to the parents.
        /// If the role is "IndependentStudent", <see cref="employablePersons"/> refers to the student and
        /// spouse</param>
        /// <returns>"Employment Expense Allowance"</returns>
        public double CalculateEmploymentExpenseAllowance(
            EfcCalculationRole role,
            MaritalStatus maritalStatus,
            List<HouseholdMember> employablePersons)
        {
            if (employablePersons == null
                || !employablePersons.Any()
                || (role == EfcCalculationRole.DependentStudent)
                || (role == EfcCalculationRole.IndependentStudentWithoutDependents
                        && maritalStatus == MaritalStatus.SingleSeparatedDivorced))
            {
                return 0;
            }

            IEnumerable<HouseholdMember> incomeEarners
                = employablePersons
                    .Where(ep => ep.IsWorking)
                    .OrderBy(ie => ie.WorkIncome);

            // Not all of the employable persons are working
            if (incomeEarners.Count() != employablePersons.Count())
            {
                return 0;
            }

            // Use the lesser of the incomes for the calculation
            double lowestIncome = incomeEarners.First().WorkIncome;
            double adjustedLowestIncome = (lowestIncome * _constants.EmploymentExpensePercent);

            double employmentExpenseAllowance = adjustedLowestIncome > _constants.EmploymentExpenseMaximum
                ? _constants.EmploymentExpenseMaximum : adjustedLowestIncome;

            return Math.Round(employmentExpenseAllowance < 0 ? 0 : employmentExpenseAllowance,
                MidpointRounding.AwayFromZero);
        }
    }
}