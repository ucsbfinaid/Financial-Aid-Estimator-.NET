using System;
using System.Collections.Generic;
using System.Linq;
using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation;
using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation.Arguments;

namespace Ucsb.Sa.FinAid.AidEstimation.Utility
{
    /// <summary>
    /// Encapsulates the complex validation logic for the various arguments used within
    /// the Aid Estimation process
    /// </summary>
    public partial class AidEstimationValidator
    {
        public List<ValidationError> Errors
        {
            get
            {
                return _validator.Errors;
            }
        }

        private readonly ArgumentValidator _validator = new ArgumentValidator();

        /// <summary>
        /// Parses "raw" string values into a <see cref="DependentEfcCalculatorArguments"/> object that
        /// can be passed to the <see cref="EfcCalculator"/>. If validation errors occur while parsing the values,
        /// they are added to the <see cref="Errors"/> property of this validator
        /// </summary>
        /// <param name="args">Set of "raw" string arguments to parse</param>
        /// <returns>The parsed <see cref="DependentEfcCalculatorArguments"/> object or null if validation failed</returns>
        public DependentEfcCalculatorArguments ValidateDependentEfcCalculatorArguments(RawDependentEfcCalculatorArguments args)
        {
            if (args == null)
            {
                throw new ArgumentException("No raw arguments provided");
            }

            // Oldest Parent Age
            int oldestParentAge
                = _validator.ValidateNonZeroInteger(
                        args.OldestParentAge,
                        LabelOldestParentAge,
                        ParamOldestParentAge);

            // Marital Status
            MaritalStatus maritalStatus
                = _validator.ValidateMaritalStatus(
                        args.MaritalStatus,
                        LabelParentMaritalStatus,
                        ParamMaritalStatus);

            // Is First Parent Working?
            bool isFirstParentWorking
                = _validator.ValidateBoolean(
                        args.IsFirstParentWorking,
                        LabelIsFirstParentWorking,
                        ParamIsFirstParentWorking);

            // First Parent Work Income
            double firstParentWorkIncome
                = isFirstParentWorking
                      ? _validator.ValidatePositiveMoneyValue(
                            args.FirstParentWorkIncome,
                            LabelFirstParentWorkIncome,
                            ParamFirstParentWorkIncome)
                      : 0;

            HouseholdMember firstParent = new HouseholdMember
            {
                IsWorking = isFirstParentWorking,
                WorkIncome = firstParentWorkIncome
            };

            HouseholdMember secondParent = null;
            if (maritalStatus == MaritalStatus.MarriedRemarried)
            {
                // Is Second Parent Working?
                bool isSecondParentWorking
                    = _validator.ValidateBoolean(
                            args.IsSecondParentWorking,
                            LabelIsSecondParentWorking,
                            ParamIsSecondParentWorking);

                // Mother Work Income
                double secondParentWorkIncome
                    = isSecondParentWorking
                          ? _validator.ValidatePositiveMoneyValue(
                                args.SecondParentWorkIncome,
                                LabelSecondParentWorkIncome,
                                ParamSecondParentWorkIncome)
                          : 0;

                secondParent = new HouseholdMember
                {
                    IsWorking = isSecondParentWorking,
                    WorkIncome = secondParentWorkIncome
                };
            }

            // Is Student Working?
            bool isStudentWorking
                = _validator.ValidateBoolean(
                        args.IsStudentWorking,
                        LabelIsStudentWorking,
                        ParamIsStudentWorking);

            // Student Work Income
            double studentWorkIncome
                = isStudentWorking
                      ? _validator.ValidatePositiveMoneyValue(
                            args.StudentWorkIncome,
                            LabelStudentWorkIncome,
                            ParamStudentWorkIncome)
                      : 0;

            HouseholdMember student = new HouseholdMember
            {
                IsWorking = isStudentWorking,
                WorkIncome = studentWorkIncome
            };

            // Parent AGI
            double parentAgi
                = _validator.ValidateMoneyValue(
                        args.ParentAgi,
                        LabelParentAgi,
                        ParamParentAgi);

            // Are Parents Tax Filers?
            bool areParentsTaxFilers
                = _validator.ValidateBoolean(
                        args.AreParentsTaxFilers,
                        LabelAreParentsTaxFilers,
                        ParamAreParentsTaxFilers);

            // Parent Income Tax Paid
            double parentIncomeTaxPaid
                = _validator.ValidateMoneyValue(
                        args.ParentIncomeTax,
                        LabelParentIncomeTax,
                        ParamParentIncomeTax);

            // Parent Untaxed Income and Benefits
            double parentUntaxedIncomeAndBenefits
                = _validator.ValidatePositiveMoneyValue(
                        args.ParentUntaxedIncomeAndBenefits,
                        LabelParentUntaxedIncomeAndBenefits,
                        ParamParentUntaxedIncomeAndBenefits);

            // Parent Additional Financial Info
            double parentAdditionalFinancialInfo
                = _validator.ValidatePositiveMoneyValue(
                        args.ParentAdditionalFinancialInfo,
                        LabelParentAdditionalFinancialInfo,
                        ParamParentAdditionalFinancialInfo);

            // Student AGI
            double studentAgi
                = _validator.ValidateMoneyValue(
                        args.StudentAgi,
                        LabelStudentAgi,
                        ParamStudentAgi);

            // Is Student Tax Filer?
            bool isStudentTaxFiler
                = _validator.ValidateBoolean(
                        args.IsStudentTaxFiler,
                        LabelIsStudentTaxFiler,
                        ParamIsStudentTaxFiler);

            // Student Income Tax Paid
            double studentIncomeTaxPaid
                = _validator.ValidateMoneyValue(
                        args.StudentIncomeTax,
                        LabelStudentIncomeTax,
                        ParamStudentIncomeTax);

            // Student Untaxed Income and Benefits
            double studentUntaxedIncomeAndBenefits
                = _validator.ValidatePositiveMoneyValue(
                        args.StudentUntaxedIncomeAndBenefits,
                        LabelStudentUntaxedIncomeAndBenefits,
                        ParamStudentUntaxedIncomeAndBenefits);

            // Student Additional Financial Information
            double studentAdditionalFinancialInfo
                = _validator.ValidatePositiveMoneyValue(
                        args.StudentAdditionalFinancialInfo,
                        LabelStudentAdditionalFinancialInfo,
                        ParamStudentAdditionalFinancialInfo);

            // Parent Cash, Savings, Checking
            double parentCashSavingsChecking
                = _validator.ValidatePositiveMoneyValue(
                        args.ParentCashSavingsChecking,
                        LabelParentCashSavingsChecking,
                        ParamParentCashSavingsChecking);

            // Parent Investment Net Worth
            double parentInvestmentNetWorth
                = _validator.ValidateMoneyValue(
                        args.ParentInvestmentNetWorth,
                        LabelParentInvestmentNetWorth,
                        ParamParentInvestmentNetWorth);

            // Parent Business Farm Net Worth
            double parentBusinessFarmNetWorth
                = _validator.ValidateMoneyValue(
                        args.ParentBusinessFarmNetWorth,
                        LabelParentBusinessFarmNetWorth,
                        ParamParentBusinessFarmNetWorth);

            // Student Cash, Savings, Checkings
            double studentCashSavingsCheckings
                = _validator.ValidatePositiveMoneyValue(
                        args.StudentCashSavingsChecking,
                        LabelStudentCashSavingsChecking,
                        ParamStudentCashSavingsChecking);

            // Student Investment Net Worth
            double studentInvestmentNetWorth
                = _validator.ValidateMoneyValue(
                        args.StudentInvestmentNetWorth,
                        LabelStudentInvestmentNetWorth,
                        ParamStudentInvestmentNetWorth);

            // Student Business Farm Net Worth
            double studentBusinessFarmNetWorth
                = _validator.ValidateMoneyValue(
                    args.StudentBusinessFarmNetWorth,
                    LabelStudentBusinessFarmNetWorth,
                    ParamStudentBusinessFarmNetWorth);

            // State of Residency
            UnitedStatesStateOrTerritory stateOfResidency
                = _validator.ValidateUnitedStatesStateOrTerritory(
                    args.StateOfResidency,
                    LabelStateOfResidency,
                    ParamStateOfResidency);

            // Number in Household
            int numberInHousehold
                = _validator.ValidateNonZeroInteger(
                    args.NumberInHousehold,
                    LabelNumInHousehold,
                    ParamNumInHousehold);

            // Number in College
            int numberInCollege
                = _validator.ValidateNonZeroInteger(
                    args.NumberInCollege,
                    LabelNumInCollege,
                    ParamNumInCollege);

            // CHECK: If Married, Number in Household must be greater than 3
            if (numberInHousehold > 0
                && maritalStatus == MaritalStatus.MarriedRemarried
                && numberInHousehold < 3)
            {
                _validator.Errors.Add(new ValidationError(ParamNumInHousehold,
                    String.Format(@"{0} was ""Married/Remarried"", but {1} was less than three",
                    LabelParentMaritalStatus, LabelNumInHousehold)));
            }
            // CHECK: Number in household must be greater than two
            else if (numberInHousehold > 0 && numberInHousehold < 2)
            {
                _validator.Errors.Add(new ValidationError(ParamNumInHousehold,
                    String.Format(@"{0} must equal at least two",
                    LabelNumInHousehold)));
            }

            // CHECK: Number in Household must be greater than or equal to Number in College
            if (numberInCollege > numberInHousehold)
            {
                _validator.Errors.Add(new ValidationError(ParamNumInCollege,
                    String.Format(@"{0} must be less than or equal to {1}",
                    LabelNumInCollege, LabelNumInHousehold)));
            }

            // Is Qualified For Simplified?
            bool isQualifiedForSimplified
                = _validator.ValidateBoolean(
                    args.IsQualifiedForSimplified,
                    LabelIsQualifiedForSimplified,
                    ParamIsQualifiedForSimplified);

            // Months of Enrollment
            int monthsOfEnrollment
                = _validator.ValidateNonZeroInteger(
                    args.MonthsOfEnrollment,
                    LabelMonthsOfEnrollment,
                    ParamMonthsOfEnrollment);

            if (_validator.Errors.Any())
            {
                return null;
            }

            // Build calculation arguments
            DependentEfcCalculatorArguments parsedArgs = new DependentEfcCalculatorArguments
                {
                    FirstParent = firstParent,
                    SecondParent = secondParent,
                    Student = student,
                    ParentAdjustedGrossIncome = parentAgi,
                    AreParentsTaxFilers = areParentsTaxFilers,
                    ParentIncomeTaxPaid = parentIncomeTaxPaid,
                    ParentUntaxedIncomeAndBenefits = parentUntaxedIncomeAndBenefits,
                    ParentAdditionalFinancialInfo = parentAdditionalFinancialInfo,
                    StudentAdjustedGrossIncome = studentAgi,
                    IsStudentTaxFiler = isStudentTaxFiler,
                    StudentIncomeTaxPaid = studentIncomeTaxPaid,
                    StudentUntaxedIncomeAndBenefits = studentUntaxedIncomeAndBenefits,
                    StudentAdditionalFinancialInfo = studentAdditionalFinancialInfo,
                    ParentCashSavingsChecking = parentCashSavingsChecking,
                    ParentInvestmentNetWorth = parentInvestmentNetWorth,
                    ParentBusinessFarmNetWorth = parentBusinessFarmNetWorth,
                    StudentCashSavingsCheckings = studentCashSavingsCheckings,
                    StudentInvestmentNetWorth = studentInvestmentNetWorth,
                    StudentBusinessFarmNetWorth = studentBusinessFarmNetWorth,
                    MaritalStatus = maritalStatus,
                    StateOfResidency = stateOfResidency,
                    NumberInHousehold = numberInHousehold,
                    NumberInCollege = numberInCollege,
                    OldestParentAge = oldestParentAge,
                    IsQualifiedForSimplified = isQualifiedForSimplified,
                    MonthsOfEnrollment = monthsOfEnrollment
                };

            return parsedArgs;
        }

        public IndependentEfcCalculatorArguments ValidateIndependentEfcCalculatorArguments(RawIndependentEfcCalculatorArguments args)
        {
            if (args == null)
            {
                throw new ArgumentException("No raw arguments provided");
            }

            // Age
            int age
                = _validator.ValidateNonZeroInteger(
                        args.StudentAge,
                        LabelIndStudentAge,
                        ParamIndStudentAge);

            // Marital Status
            MaritalStatus maritalStatus
                = _validator.ValidateMaritalStatus(
                        args.MaritalStatus,
                        LabelIndStudentMaritalStatus,
                        ParamMaritalStatus);

            // Is Student Working?
            bool isStudentWorking
                = _validator.ValidateBoolean(
                        args.IsStudentWorking,
                        LabelIsStudentWorking,
                        ParamIsStudentWorking);

            // Student Work Income
            double studentWorkIncome
                = isStudentWorking
                        ? _validator.ValidatePositiveMoneyValue(
                            args.StudentWorkIncome,
                            LabelStudentWorkIncome,
                            ParamStudentWorkIncome)
                        : 0;

            HouseholdMember student = new HouseholdMember
                                            {
                                                IsWorking = isStudentWorking,
                                                WorkIncome = studentWorkIncome
                                            };

            HouseholdMember spouse = null;
            
            if (maritalStatus == MaritalStatus.MarriedRemarried)
            {
                // Is Spouse Working?
                bool isSpouseWorking
                    = _validator.ValidateBoolean(
                            args.IsSpouseWorking,
                            LabelIsSpouseWorking,
                            ParamIsSpouseWorking);

                // Spouse Work Income
                double spouseWorkIncome
                    = isSpouseWorking
                            ? _validator.ValidatePositiveMoneyValue(
                                    args.SpouseWorkIncome,
                                    LabelSpouseWorkIncome,
                                    ParamSpouseWorkIncome)
                            : 0;

                spouse = new HouseholdMember
                                {
                                    IsWorking = isSpouseWorking,
                                    WorkIncome = spouseWorkIncome
                                };
            }

            // Student and Spouse's AGI
            double agi = _validator.ValidateMoneyValue(
                            args.StudentAgi,
                            LabelIndStudentAgi,
                            ParamIndStudentAgi);

            // Are Tax Filers?
            bool areTaxFilers
                = _validator.ValidateBoolean(
                        args.IsStudentTaxFiler,
                        LabelIsIndStudentTaxFiler,
                        ParamIsIndStudentTaxFiler);

            // Income Tax Paid
            double incomeTaxPaid
                = _validator.ValidateMoneyValue(
                        args.StudentIncomeTax,
                        LabelIndStudentIncomeTax,
                        ParamIndStudentIncomeTax);

            // Untaxed Income And Benefits
            double untaxedIncomeAndBenefits
                = _validator.ValidatePositiveMoneyValue(
                        args.StudentUntaxedIncomeAndBenefits,
                        LabelIndStudentUntaxedIncomeAndBenefits,
                        ParamIndStudentUntaxedIncomeAndBenefits);

            // Additional Financial Information
            double additionalFinancialInfo
                = _validator.ValidatePositiveMoneyValue(
                        args.StudentAdditionalFinancialInfo,
                        LabelIndStudentAdditionalFinancialInfo,
                        ParamIndStudentAdditionalFinancialInfo);

            // Cash, Savings, Checking
            double cashSavingsChecking
                = _validator.ValidatePositiveMoneyValue(
                        args.StudentCashSavingsChecking,
                        LabelIndStudentCashSavingsChecking,
                        ParamIndStudentCashSavingsChecking);

            // Investment Net Worth
            double investmentNetWorth
                = _validator.ValidateMoneyValue(
                        args.StudentInvestmentNetWorth,
                        LabelIndStudentInvestmentNetWorth,
                        ParamIndStudentInvestmentNetWorth);

            // Business Farm Net Worth
            double businessFarmNetWorth
                = _validator.ValidateMoneyValue(
                        args.StudentBusinessFarmNetWorth,
                        LabelIndStudentBusinessFarmNetWorth,
                        ParamIndStudentBusinessFarmNetWorth);

            // Has Dependents?
            bool hasDependents
                = _validator.ValidateBoolean(
                        args.HasDependents,
                        LabelIndStudentHasDep,
                        ParamIndStudentHasDep);

            // State of Residency
            UnitedStatesStateOrTerritory stateOfResidency
                = _validator.ValidateUnitedStatesStateOrTerritory(
                        args.StateOfResidency,
                        LabelStateOfResidency,
                        ParamStateOfResidency);

            // Number in Household
            int numberInHousehold
                = _validator.ValidateNonZeroInteger(
                        args.NumberInHousehold,
                        LabelNumInHousehold,
                        ParamNumInHousehold);

            // Number in College
            int numberInCollege
                = _validator.ValidateNonZeroInteger(
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

            // CHECK: If student has dependents, Number in Household can not be less than two
            if (hasDependents && numberInHousehold < 2)
            {
                _validator.Errors.Add(new ValidationError(ParamIndStudentHasDep,
                    String.Format(@"Student has dependents, but {0} was less than two.",
                    LabelNumInHousehold)));
            }

            // Is Qualified for Simplified
            bool isQualifiedForSimplified
                = _validator.ValidateBoolean(
                        args.IsQualifiedForSimplified,
                        LabelIsQualifiedForSimplified,
                        ParamIsQualifiedForSimplified);

            // Months of Enrollment
            int monthsOfEnrollment
                = _validator.ValidateNonZeroInteger(
                        args.MonthsOfEnrollment,
                        LabelMonthsOfEnrollment,
                        ParamMonthsOfEnrollment);

            if (_validator.Errors.Any())
            {
                return null;
            }

            // Build calculation arguments
            IndependentEfcCalculatorArguments parsedArgs =
                new IndependentEfcCalculatorArguments
                {
                    Student = student,
                    Spouse = spouse,
                    AdjustedGrossIncome = agi,
                    AreTaxFilers = areTaxFilers,
                    IncomeTaxPaid = incomeTaxPaid,
                    UntaxedIncomeAndBenefits = untaxedIncomeAndBenefits,
                    AdditionalFinancialInfo = additionalFinancialInfo,
                    CashSavingsCheckings = cashSavingsChecking,
                    InvestmentNetWorth = investmentNetWorth,
                    BusinessFarmNetWorth = businessFarmNetWorth,
                    HasDependents = hasDependents,
                    MaritalStatus = maritalStatus,
                    StateOfResidency = stateOfResidency,
                    NumberInHousehold = numberInHousehold,
                    NumberInCollege = numberInCollege,
                    Age = age,
                    IsQualifiedForSimplified = isQualifiedForSimplified,
                    MonthsOfEnrollment = monthsOfEnrollment
                };

            return parsedArgs;
        }
    }
}
