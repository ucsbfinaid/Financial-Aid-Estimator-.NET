using System;
using System.Collections.Generic;
using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation.Arguments;
using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation.Constants;

namespace Ucsb.Sa.FinAid.AidEstimation.EfcCalculation
{
    /// <summary>
    /// Expected Family Contribution (EFC) calculator
    /// </summary>
    public class EfcCalculator
    {
        private const int DefaultMonthsOfEnrollment = 9;
        private const int AnnualMonthsOfEnrollment = 12;

        private readonly EfcCalculatorConstants _constants;
        private readonly IncomeCalculator _incomeCalculator;
        private readonly AllowanceCalculator _allowanceCalculator;
        private readonly AssetContributionCalculator _assetContributionCalculator;
        private readonly AaiContributionCalculator _aaiContributionCalculator;

        /// <summary>
        /// Constructs a new Expected Family Contribution (EFC) calculator
        /// </summary>
        /// <param name="constants"><see cref="EfcCalculatorConstants"/> used in the calculation of
        /// Expected Family Contribution (EFC)</param>
        /// <param name="incomeCalculator">Calculator used in income calculations</param>
        /// <param name="allowanceCalculator">Calculator used in allowance calculations</param>
        /// <param name="assetContributionCalculator">Calculator used in asset contribution calculations</param>
        /// <param name="aaiContributionCalculator">Calculator used in Adjusted Available Income (AAI) contribution
        /// calculations</param>
        public EfcCalculator(EfcCalculatorConstants constants,
                                    IncomeCalculator incomeCalculator,
                                    AllowanceCalculator allowanceCalculator,
                                    AssetContributionCalculator assetContributionCalculator,
                                    AaiContributionCalculator aaiContributionCalculator)
        {
            _constants = constants;
            _incomeCalculator = incomeCalculator;
            _allowanceCalculator = allowanceCalculator;
            _assetContributionCalculator = assetContributionCalculator;
            _aaiContributionCalculator = aaiContributionCalculator;
        }

        /// <summary>
        /// Calculates parent contribution (PC), student contribution (SC), and expected family contribution (EFC) for
        /// a dependent student
        /// </summary>
        /// <param name="args">Parameters for the calculation</param>
        /// <returns>Parent contribution (PC), student contribution (SC), and expected family contribution (EFC) for
        /// a dependent student</returns>
        public EfcProfile GetDependentEfcProfile(DependentEfcCalculatorArguments args)
        {
            if (args.NumberInCollege <= 0
                || args.MonthsOfEnrollment <= 0
                || args.Student == null)
            {
                return new EfcProfile(0, 0, 0, 0);
            }

            double workIncome = 0;
            List<HouseholdMember> parents = new List<HouseholdMember>();

            if (args.FirstParent != null)
            {
                if (args.FirstParent.IsWorking)
                {
                    workIncome += args.FirstParent.WorkIncome;
                }

                parents.Add(args.FirstParent);
            }

            if (args.SecondParent != null)
            {
                if (args.SecondParent.IsWorking)
                {
                    workIncome += args.SecondParent.WorkIncome;
                }

                parents.Add(args.SecondParent);
            }

            double simpleIncome = (args.AreParentsTaxFilers) ? args.ParentAdjustedGrossIncome : workIncome;

            // Determine Auto Zero EFC eligibility
            if (args.IsQualifiedForSimplified && simpleIncome <= _constants.AutoZeroEfcMax)
            {
                return new EfcProfile(0, 0, 0, 0);
            }

            // Parent's Total Income
            double parentTotalIncome = _incomeCalculator.CalculateTotalIncome(
                                            args.ParentAdjustedGrossIncome,
                                            workIncome,
                                            args.AreParentsTaxFilers,
                                            args.ParentUntaxedIncomeAndBenefits,
                                            args.ParentAdditionalFinancialInfo);

            // Parent's Total Allowances
            double parentTotalAllowances = _allowanceCalculator.CalculateTotalAllowances(
                                            EfcCalculationRole.Parent,
                                            args.MaritalStatus,
                                            args.StateOfResidency,
                                            args.NumberInCollege,
                                            args.NumberInHousehold,
                                            parents,
                                            parentTotalIncome,
                                            args.ParentIncomeTaxPaid);

            // Parent's Available Income
            double parentAvailableIncome
                = _incomeCalculator.CalculateAvailableIncome(EfcCalculationRole.Parent, parentTotalIncome,
                                                                parentTotalAllowances);

            // Determine Simplified EFC Equation Eligibility
            bool useSimplified = (args.IsQualifiedForSimplified && simpleIncome <= _constants.SimplifiedEfcMax);

            // Parent's Contribution From Assets
            double parentAssetContribution = 0;

            if (!useSimplified)
            {
                parentAssetContribution = _assetContributionCalculator.CalculateContributionFromAssets(
                    EfcCalculationRole.Parent,
                    args.MaritalStatus,
                    args.OldestParentAge,
                    args.ParentCashSavingsChecking,
                    args.ParentInvestmentNetWorth,
                    args.ParentBusinessFarmNetWorth);
            }

            // Parent's Adjusted Available Income
            double parentAdjustedAvailableIncome = parentAvailableIncome + parentAssetContribution;

            // Parent's Contribution from AAI
            double parentContributionFromAai =
                _aaiContributionCalculator.CalculateContributionFromAai(EfcCalculationRole.Parent,
                                                                            parentAdjustedAvailableIncome);

            // Parent Contribution
            double parentContribution = Math.Round(parentContributionFromAai / args.NumberInCollege,
                MidpointRounding.AwayFromZero);

            // Modify Parent Contribution based on months of enrollment
            if (args.MonthsOfEnrollment < DefaultMonthsOfEnrollment)
            {
                // LESS than default months of enrollment
                double parentMonthlyContribution
                    = Math.Round(parentContribution / DefaultMonthsOfEnrollment, MidpointRounding.AwayFromZero);

                parentContribution
                    = Math.Round(parentMonthlyContribution * args.MonthsOfEnrollment, MidpointRounding.AwayFromZero);
            }
            else if (args.MonthsOfEnrollment > DefaultMonthsOfEnrollment)
            {
                // MORE than default months of enrollment
                double parentAltAai
                    = parentAdjustedAvailableIncome + _constants.AltEnrollmentIncomeProtectionAllowance;

                double parentAltContributionFromAai
                    = _aaiContributionCalculator.CalculateContributionFromAai(EfcCalculationRole.Parent, parentAltAai);

                double parentAltContribution
                    = Math.Round(parentAltContributionFromAai / args.NumberInCollege, MidpointRounding.AwayFromZero);

                double parentContributionDiff
                    = (parentAltContribution - parentContribution);

                double parentMonthlyContribution
                    = Math.Round(parentContributionDiff / AnnualMonthsOfEnrollment, MidpointRounding.AwayFromZero);

                double parentContributionAdjustment
                    = Math.Round(parentMonthlyContribution * (args.MonthsOfEnrollment - DefaultMonthsOfEnrollment),
                                    MidpointRounding.AwayFromZero);

                parentContribution += parentContributionAdjustment;
            }

            // Student's Total Income
            double studentTotalIncome = _incomeCalculator.CalculateTotalIncome(
                                            args.StudentAdjustedGrossIncome,
                                            args.Student.WorkIncome,
                                            args.IsStudentTaxFiler,
                                            args.StudentUntaxedIncomeAndBenefits,
                                            args.StudentAdditionalFinancialInfo);

            // Student's Total Allowances
            double studentTotalAllowances = _allowanceCalculator.CalculateTotalAllowances(
                                            EfcCalculationRole.DependentStudent,
                                            MaritalStatus.SingleSeparatedDivorced,
                                            args.StateOfResidency,
                                            args.NumberInCollege,
                                            args.NumberInHousehold,
                                            new List<HouseholdMember> { args.Student },
                                            studentTotalIncome,
                                            args.StudentIncomeTaxPaid);

            // If parent has a negative AAI, add it to the student's Total Allowances
            if (parentAdjustedAvailableIncome < 0)
            {
                studentTotalAllowances -= parentAdjustedAvailableIncome;
            }

            // Student's Available Income (Contribution From Available Income)
            double studentAvailableIncome =
                _incomeCalculator.CalculateAvailableIncome(EfcCalculationRole.DependentStudent, studentTotalIncome,
                                                           studentTotalAllowances);

            // Modify Student's Available Income based on months of enrollment
            if (args.MonthsOfEnrollment < DefaultMonthsOfEnrollment)
            {
                // LESS than default months of enrollment
                double studentMonthlyContribution = Math.Round(studentAvailableIncome / DefaultMonthsOfEnrollment,
                                                               MidpointRounding.AwayFromZero);
                studentAvailableIncome = Math.Round(studentMonthlyContribution * args.MonthsOfEnrollment,
                                                    MidpointRounding.AwayFromZero);
            }

            // For MORE than default months of enrollment, the standard Available Income is used

            // Student's Contribution From Assets
            double studentAssetContribution = 0;

            if (!useSimplified)
            {
                studentAssetContribution = _assetContributionCalculator.CalculateContributionFromAssets(
                    EfcCalculationRole.DependentStudent,
                    MaritalStatus.SingleSeparatedDivorced,
                    0,
                    args.StudentCashSavingsChecking,
                    args.StudentInvestmentNetWorth,
                    args.StudentBusinessFarmNetWorth);
            }

            // Student Contribution
            double studentContribution = studentAvailableIncome + studentAssetContribution;

            EfcProfile profile = new EfcProfile(
                parentContribution + studentContribution,
                parentContribution,
                studentContribution,
                parentTotalIncome);

            return profile;
        }

        /// <summary>
        /// Calculates student contribution (PC) and expected family contribution (EFC) for an independent student
        /// </summary>
        /// <param name="args">Parameters for the calculation</param>
        /// <returns>Student contribution (PC) and expected family contribution (EFC) for an independent student</returns>
        public EfcProfile GetIndependentEfcProfile(IndependentEfcCalculatorArguments args)
        {
            if (args.NumberInCollege <= 0
                    || args.MonthsOfEnrollment <= 0
                    || args.Student == null)
            {
                return new EfcProfile(0, 0, 0, 0);
            }

            EfcCalculationRole role = (args.HasDependents)
                                          ? EfcCalculationRole.IndependentStudentWithDependents
                                          : EfcCalculationRole.IndependentStudentWithoutDependents;

            double workIncome = 0;

            List<HouseholdMember> householdMembers = new List<HouseholdMember> { args.Student };
            workIncome += args.Student.IsWorking ? args.Student.WorkIncome : 0;

            if (args.Spouse != null)
            {
                if (args.Spouse.IsWorking)
                {
                    workIncome += args.Spouse.WorkIncome;
                }

                householdMembers.Add(args.Spouse);
            }

            double simpleIncome = (args.AreTaxFilers) ? args.AdjustedGrossIncome : workIncome;

            // Determine Auto Zero EFC eligibility
            if (args.IsQualifiedForSimplified
                && role == EfcCalculationRole.IndependentStudentWithDependents
                && simpleIncome <= _constants.AutoZeroEfcMax)
            {
                return new EfcProfile(0, 0, 0, 0);
            }

            // Student's Total Income
            double totalIncome = _incomeCalculator.CalculateTotalIncome(
                                    args.AdjustedGrossIncome,
                                    workIncome,
                                    args.AreTaxFilers,
                                    args.UntaxedIncomeAndBenefits,
                                    args.AdditionalFinancialInfo);

            // Student's Total Allowances
            double totalAllowances = _allowanceCalculator.CalculateTotalAllowances(
                                        role,
                                        args.MaritalStatus,
                                        args.StateOfResidency,
                                        args.NumberInCollege,
                                        args.NumberInHousehold,
                                        householdMembers,
                                        totalIncome,
                                        args.IncomeTaxPaid);

            // Student's Available Income (Contribution from Available Income)
            double availableIncome = _incomeCalculator.CalculateAvailableIncome(role, totalIncome, totalAllowances);

            // Determine Simplified EFC Equation Eligibility
            bool useSimplified = (args.IsQualifiedForSimplified && simpleIncome <= _constants.SimplifiedEfcMax);

            // Student's Contribution From Assets
            double assetContribution = 0;

            if (!useSimplified)
            {
                assetContribution = _assetContributionCalculator.CalculateContributionFromAssets(
                    role,
                    args.MaritalStatus,
                    args.Age,
                    args.CashSavingsCheckings,
                    args.InvestmentNetWorth,
                    args.BusinessFarmNetWorth);
            }

            // Student's Adjusted Available Income
            double adjustedAvailableIncome = availableIncome + assetContribution;

            // Student Contribution From AAI
            double studentContributionFromAai
                = _aaiContributionCalculator.CalculateContributionFromAai(role, adjustedAvailableIncome);

            // Student's Contribution
            double studentContribution = Math.Round(studentContributionFromAai / args.NumberInCollege,
                MidpointRounding.AwayFromZero);

            // Modify Student's Available Income based on months of enrollment
            if (args.MonthsOfEnrollment < DefaultMonthsOfEnrollment)
            {
                // LESS than default months of enrollment
                double monthlyContribution = Math.Round(studentContribution / DefaultMonthsOfEnrollment);
                studentContribution = monthlyContribution * args.MonthsOfEnrollment;
            }

            // For MORE than default months of enrollment, the standard contribution is used

            EfcProfile profile = new EfcProfile(studentContribution, 0, studentContribution, 0);
            return profile;
        }
    }
}