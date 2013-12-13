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
    public class AidEstimationValidator
    {
        public List<ValidationError> Errors
        {
            get
            {
                return _validator.Errors;
            }
        }

        // Is Working?

        private const string LabelIsFirstParentWorking = @"""Did the First Parent Work?""";
        private const string LabelIsSecondParentWorking = @"""Did the Second Parent Work?""";
        private const string LabelIsStudentWorking = @"""Did the Student Work?""";
        private const string LabelIsSpouseWorking = @"""Did the Spouse Work?""";
        private const string ParamIsFirstParentWorking = "isFirstParentWorking";
        private const string ParamIsSecondParentWorking = "isSecondParentWorking";
        private const string ParamIsStudentWorking = "isStudentWorking";
        private const string ParamIsSpouseWorking = "isSpouseWorking";

        // Work Income

        private const string LabelFirstParentWorkIncome = @"""First Parent's Income Earned From Work""";
        private const string LabelSecondParentWorkIncome = @"""Second Parent's Income Earned From Work""";
        private const string LabelStudentWorkIncome = @"""Student's Income Earned From Work""";
        private const string LabelSpouseWorkIncome = @"""Spouse's Income Earned From Work""";
        private const string ParamFirstParentWorkIncome = "firstParentWorkIncome";
        private const string ParamSecondParentWorkIncome = "secondParentWorkIncome";
        private const string ParamStudentWorkIncome = "studentWorkIncome";
        private const string ParamSpouseWorkIncome = "spouseWorkIncome";

        // AGI

        private const string LabelParentAgi = @"""Parent(s)' Adjusted Gross Income (AGI)""";
        private const string LabelStudentAgi = @"""Student's Adjusted Gross Income (AGI)""";
        private const string LabelIndStudentAgi = @"""Student and Spouse's Adjusted Gross Income (AGI)""";
        private const string ParamParentAgi = "parentAgi";
        private const string ParamStudentAgi = "studentAgi";
        private const string ParamIndStudentAgi = "studentAgi";

        // Are Tax Filers?

        private const string LabelAreParentsTaxFilers = @"""Did Parent(s) File Taxes?""";
        private const string LabelIsStudentTaxFiler = @"""Did the Student File Taxes?""";
        private const string LabelIsIndStudentTaxFiler = @"""Did the Student and Spouse File Taxes?""";
        private const string ParamAreParentsTaxFilers = "areParentsTaxFilers";
        private const string ParamIsStudentTaxFiler = "isStudentTaxFiler";
        private const string ParamIsIndStudentTaxFiler = "isStudentTaxFiler";

        // Income Tax Paid

        private const string LabelParentIncomeTax = @"""Parent(s)' Income Taxes Paid""";
        private const string LabelStudentIncomeTax = @"""Student's Income Taxes Paid""";
        private const string LabelIndStudentIncomeTax = @"""Student and Spouse's Total Income Taxes Paid""";
        private const string ParamParentIncomeTax = "parentIncomeTax";
        private const string ParamStudentIncomeTax = "studentIncomeTax";
        private const string ParamIndStudentIncomeTax = "studentIncomeTax";

        // Untaxed Income And Benefits

        private const string LabelParentUntaxedIncomeAndBenefits = @"""Parent(s)' Untaxed Income and Benefits""";
        private const string LabelStudentUntaxedIncomeAndBenefits = @"""Student's Untaxed Income and Benefits""";
        private const string LabelIndStudentUntaxedIncomeAndBenefits
            = @"""Student and Spouse's Untaxed Income and Benefits""";
        private const string ParamParentUntaxedIncomeAndBenefits = "parentUntaxedIncomeAndBenefits";
        private const string ParamStudentUntaxedIncomeAndBenefits = "studentUntaxedIncomeAndBenefits";
        private const string ParamIndStudentUntaxedIncomeAndBenefits = "studentUntaxedIncomeAndBenefits";

        // Additional Financial Information

        private const string LabelParentAdditionalFinancialInfo = @"""Parent(s)' Additional Financial Information""";
        private const string LabelStudentAdditionalFinancialInfo = @"""Student's Additional Financial Information""";
        private const string LabelIndStudentAdditionalFinancialInfo = @"""Student and Spouse's Additional Financial Information""";
        private const string ParamParentAdditionalFinancialInfo = "parentAdditionalFinancialInfo";
        private const string ParamStudentAdditionalFinancialInfo = "studentAdditionalFinancialInfo";
        private const string ParamIndStudentAdditionalFinancialInfo = "studentAdditionalFinancialInfo";

        // Cash, Savings, Checking

        private const string LabelParentCashSavingsChecking = @"""Parent(s)' Cash, Savings, and Checking""";
        private const string LabelStudentCashSavingsChecking = @"""Student's Cash, Savings, and Checking""";
        private const string LabelIndStudentCashSavingsChecking
            = @"""Student and Spouse's Cash, Savings, and Checking""";
        private const string ParamParentCashSavingsChecking = "parentCashSavingsChecking";
        private const string ParamStudentCashSavingsChecking = "studentCashSavingsChecking";
        private const string ParamIndStudentCashSavingsChecking = "studentCashSavingsChecking";

        // Investment Net Worth

        private const string LabelParentInvestmentNetWorth = @"""Net Worth of Parent(s)' Investments""";
        private const string LabelStudentInvestmentNetWorth = @"""Net Worth of Student's Investments""";
        private const string LabelIndStudentInvestmentNetWorth = @"""Net Worth of Student and Spouse's Investments""";
        private const string ParamParentInvestmentNetWorth = "parentInvestmentNetWorth";
        private const string ParamStudentInvestmentNetWorth = "studentInvestmentNetWorth";
        private const string ParamIndStudentInvestmentNetWorth = "studentInvestmentNetWorth";

        // Business Farm Net Worth

        private const string LabelParentBusinessFarmNetWorth = @"""Net Worth of Parent(s)' Business and/or Investment Farm""";
        private const string LabelStudentBusinessFarmNetWorth = @"""Net Worth of Student's Business and/or Investment Farm""";
        private const string LabelIndStudentBusinessFarmNetWorth = @"""Net Worth of Student and Spouse's Business and/or Investment Farm""";
        private const string ParamParentBusinessFarmNetWorth = "parentBusinessFarmNetWorth";
        private const string ParamStudentBusinessFarmNetWorth = "studentBusinessFarmNetWorth";
        private const string ParamIndStudentBusinessFarmNetWorth = "studentBusinessFarmNetWorth";

        // Has Dependents

        private const string LabelIndStudentHasDep = @"""Student has Dependents""";
        private const string ParamIndStudentHasDep = "hasDependents";

        // Marital Status

        private const string ParamMaritalStatus = "maritalStatus";
        private const string LabelParentMaritalStatus = @"""Parent(s)' Marital Status""";
        private const string LabelIndStudentMaritalStatus = @"""Student's Marital Status""";

        // State of Residency

        private const string LabelStateOfResidency = @"""State of Residency""";
        private const string ParamStateOfResidency = "stateOfResidency";

        // Number in Household

        private const string LabelNumInHousehold = @"""Number in Household""";
        private const string ParamNumInHousehold = "numberInHousehold";

        // Number in College

        private const string LabelNumInCollege = @"""Number in College""";
        private const string ParamNumInCollege = "numberInCollege";

        // Age

        private const string LabelOldestParentAge = @"""Age of Oldest Parent""";
        private const string LabelIndStudentAge = @"""Student's Age""";
        private const string ParamOldestParentAge = "oldestParentAge";
        private const string ParamIndStudentAge = "studentAge";

        // Months of Enrollment

        private const string LabelMonthsOfEnrollment = @"""Months of Enrollment""";
        private const string ParamMonthsOfEnrollment = "monthsOfEnrollment";

        // Is Qualified for Simplified?

        private const string LabelIsQualifiedForSimplified = @"""Is Qualified for Simplified?""";
        private const string ParamIsQualifiedForSimplified = "isQualifiedForSimplified";

        private readonly ArgumentValidator _validator = new ArgumentValidator();

        /// <summary>
        /// Parses "raw" string values into a <see cref="DependentEfcCalculatorArguments"/> object that
        /// can be passed to the <see cref="EfcCalculator"/>. If validation errors occur while parsing the values,
        /// they are added to the <see cref="Errors"/> property of this validator
        /// </summary>
        /// < name="args">Set of "raw" string arguments to parse</>
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
