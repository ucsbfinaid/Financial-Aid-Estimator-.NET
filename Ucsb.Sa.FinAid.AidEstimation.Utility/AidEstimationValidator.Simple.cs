using System;
using System.Linq;
using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation;
using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation.Arguments;

namespace Ucsb.Sa.FinAid.AidEstimation.Utility
{
    public partial class AidEstimationValidator
    {
        public DependentEfcCalculatorArguments ValidateSimpleDependentEfcCalculatorArguments(RawSimpleDependentEfcCalculatorArguments args)
        {
            if (args == null)
            {
                throw new ArgumentException("No raw arguments provided");
            }

            // Marital Status
            MaritalStatus maritalStatus = 
                _validator.ValidateMaritalStatus(
                    args.MaritalStatus,
                    LabelParentMaritalStatus,
                    ParamMaritalStatus);

            // Oldest Parent Age
            int oldestParentAge
                = _validator.ValidateNonZeroInteger(
                        args.OldestParentAge,
                        LabelOldestParentAge,
                        ParamOldestParentAge);

            // Parent Income
            double parentIncome =
                _validator.ValidatePositiveMoneyValue(
                        args.ParentIncome,
                        LabelParentIncome,
                        ParamParentIncome);

            // Parent Other Income
            double parentOtherIncome =
                _validator.ValidatePositiveMoneyValue(
                        args.ParentOtherIncome,
                        LabelParentOtherIncome,
                        ParamParentOtherIncome);

            // Parent Income Earned By
            IncomeEarnedBy incomeEarnedBy =
                _validator.ValidateIncomeEarnedBy(
                    args.ParentIncomeEarnedBy,
                    LabelParentIncomeEarnedBy,
                    ParamParentIncomeEarnedBy);

            // CHECK: If Single/Separated/Divorced, "Parent Income Earned By" can not be "Both"
            if (maritalStatus == MaritalStatus.SingleSeparatedDivorced && incomeEarnedBy == IncomeEarnedBy.Both)
            {
                _validator.Errors.Add(new ValidationError(ParamParentIncomeEarnedBy,
                    String.Format(@"{0} was ""Single/Separated/Divorced"", but {1} was marked as earned by both parents",
                    LabelParentMaritalStatus, LabelParentIncomeEarnedBy)));
            }

            // CHECK: If "Parent Income Earned By" is "None", then "Parent Income" must be 0
            if (incomeEarnedBy == IncomeEarnedBy.None && parentIncome > 0)
            {
                _validator.Errors.Add(new ValidationError(ParamParentIncome,
                    String.Format(@"{0} was marked as earned by neither parents, but {1} was greater than 0",
                        LabelParentIncomeEarnedBy, LabelParentIncome)));
            }

            // Parent Income Tax Paid
            double parentIncomeTaxPaid =
                _validator.ValidatePositiveMoneyValue(
                    args.ParentIncomeTax,
                    LabelParentIncomeTax,
                    ParamParentIncomeTax);

            // Parent Assets
            double parentAssets =
                _validator.ValidatePositiveMoneyValue(
                    args.ParentAssets,
                    LabelParentAssets,
                    ParamParentAssets);

            // Student Income
            double studentIncome =
                _validator.ValidatePositiveMoneyValue(
                    args.StudentIncome,
                    LabelStudentIncome,
                    ParamStudentIncome);

            // Student Other Income
            double studentOtherIncome =
                _validator.ValidatePositiveMoneyValue(
                    args.StudentOtherIncome,
                    LabelStudentOtherIncome,
                    ParamStudentOtherIncome);

            // Student Income Tax Paid
            double studentIncomeTaxPaid =
                _validator.ValidatePositiveMoneyValue(
                    args.StudentIncomeTax,
                    LabelStudentIncomeTax,
                    ParamStudentIncomeTax);

            // Student Assets
            double studentAssets =
                _validator.ValidatePositiveMoneyValue(
                    args.StudentAssets,
                    LabelStudentAssets,
                    ParamStudentAssets);

            // Number in Household
            int numberInHousehold =
                _validator.ValidateNonZeroInteger(
                    args.NumberInHousehold,
                    LabelNumInHousehold,
                    ParamNumInHousehold);

            // Number in College
            int numberInCollege =
                _validator.ValidateNonZeroInteger(
                    args.NumberInCollege,
                    LabelNumInCollege,
                    ParamNumInCollege);

            // CHECK: Number in Household must be greater than or equal to Number in College
            if (numberInCollege > numberInHousehold)
            {
                _validator.Errors.Add(new ValidationError(ParamNumInCollege,
                    String.Format(@"{0} must be less than or equal to {1}",
                    LabelNumInCollege, LabelNumInHousehold)));
            }

            // State of Residency
            UnitedStatesStateOrTerritory stateOfResidency =
                _validator.ValidateUnitedStatesStateOrTerritory(
                    args.StateOfResidency,
                    LabelStateOfResidency,
                    ParamStateOfResidency);

            if (_validator.Errors.Any())
            {
                return null;
            }

            // Build a list of arguments for the full EFC calculation using assumed
            // values gleaned from the "simplified" values provided

            bool isFirstParentWorking = false;
            bool isSecondParentWorking = false;

            double firstParentWorkIncome = 0;
            double secondParentWorkIncome = 0;

            if (incomeEarnedBy == IncomeEarnedBy.One)
            {
                isFirstParentWorking = true;
                firstParentWorkIncome = parentIncome;
            }

            if (incomeEarnedBy == IncomeEarnedBy.Both)
            {
                isFirstParentWorking = isSecondParentWorking = true;
                firstParentWorkIncome = secondParentWorkIncome = (parentIncome / 2);
            }

            HouseholdMember firstParent = new HouseholdMember
            {
                IsWorking = isFirstParentWorking,
                WorkIncome = firstParentWorkIncome
            };

            HouseholdMember secondParent = new HouseholdMember
            {
                IsWorking = isSecondParentWorking,
                WorkIncome = secondParentWorkIncome
            };

            // ASSUME: Student is working
            HouseholdMember student = new HouseholdMember
            {
                IsWorking = true,
                WorkIncome = studentIncome
            };

            // Build calculation arguments
            DependentEfcCalculatorArguments parsedArgs = new DependentEfcCalculatorArguments
            {
                FirstParent = firstParent,
                SecondParent = secondParent,
                Student = student,

                // ASSUME: "Parent's AGI" == "Parent's Income"
                ParentAdjustedGrossIncome = parentIncome,

                // ASSUME: Parents are tax filers
                AreParentsTaxFilers = true,

                ParentIncomeTaxPaid = parentIncomeTaxPaid,

                // ASSUME: "Parent's Untaxed Income and Benefits" == "Parent's Other Income"
                ParentUntaxedIncomeAndBenefits = parentOtherIncome,

                // ASSUME: "Parent's Additional Financial Information" is zero
                ParentAdditionalFinancialInfo = 0,

                // ASSUME: "Student's AGI" == "Student's Income"
                StudentAdjustedGrossIncome = studentIncome,

                // ASSUME: Student is a tax filer
                IsStudentTaxFiler = true,

                StudentIncomeTaxPaid = studentIncomeTaxPaid,

                // ASSUME: "Student's Untaxed Income and Benefits" == "Student's Other Income"
                StudentUntaxedIncomeAndBenefits = studentOtherIncome,

                // ASSUME: "Student's Additional Financial Information" is zero
                StudentAdditionalFinancialInfo = 0,

                // ASSUME: "Parent's Cash, Savings, and Checking" == "Parent's Assets"
                ParentCashSavingsChecking = parentAssets,

                // ASSUME: "Parent's Net Worth of Investments" is zero
                ParentInvestmentNetWorth = 0,

                // ASSUME: "Parent's Net Worth of Business and/or Investment Farm" is zero
                ParentBusinessFarmNetWorth = 0,

                // ASSUME: "Student's Cash, Savings, and Checking" == "Student's Assets"
                StudentCashSavingsChecking = studentAssets,

                // ASSUME: "Student's Net Worth of Investments" is zero
                StudentInvestmentNetWorth = 0,

                // ASSUME: "Student's Net Worth of Business and/or Investment Farm" is zero
                StudentBusinessFarmNetWorth = 0,

                MaritalStatus = maritalStatus,
                StateOfResidency = stateOfResidency,
                NumberInHousehold = numberInHousehold,
                NumberInCollege = numberInCollege,
                OldestParentAge = oldestParentAge,

                // ASSUME: Student is NOT qualified for simplified formula
                IsQualifiedForSimplified = false,

                // ASSUME: Nine months of enrollment
                MonthsOfEnrollment = 9
            };

            return parsedArgs;
        }
    }
}
