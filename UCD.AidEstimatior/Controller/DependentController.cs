using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using Ucsb.Sa.FinAid.AidEstimation;
using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation;
using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation.Arguments;
using Ucsb.Sa.FinAid.AidEstimation.Utility;

namespace Web.Controllers
{
    public class DependentController : Controller
    {
        private AppSettings AppSettings { get; set; }

        public DependentController(IOptions<AppSettings> settings)
        {
            AppSettings = settings.Value;
        }

        /// <summary>
        /// Home page for dependents
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ViewData["Title"] = "Dependent ";
            ViewData["HeaderText"] = "PLACEHOLDER Header Text";
            ViewData["Errors"] = "";
            // set any default values desired
            Dictionary<string, string> FormValues = GetModelDictionary(null, null, "California", "OnCampus", null, null, null, 0, null, 0, null, 0, 0, null, null, null, null, null, null, 0, null, 0, 0, null, null, null, null, null);
            return View("Index", FormValues);
        }

        /// <summary>
        /// Runs the dependent estimation
        /// </summary>
        /// <param name="NumberInHousehold"></param>
        /// <param name="NumberInCollege"></param>
        /// <param name="StateOfResidency"></param>
        /// <param name="Housing"></param>
        /// <param name="OldestParentAge"></param>
        /// <param name="MaritalStatus"></param>
        /// <param name="FirstParentWorking"></param>
        /// <param name="FirstParentWorkIncome"></param>
        /// <param name="SecondParentWorking"></param>
        /// <param name="SecondParentWorkIncome"></param>
        /// <param name="ParentsTaxFilers"></param>
        /// <param name="ParentAgi"></param>
        /// <param name="ParentIncomeTax"></param>
        /// <param name="ParentUntaxedIncomeAndBenefits"></param>
        /// <param name="ParentAdditionalFinancialInfo"></param>
        /// <param name="ParentCashSavingsChecking"></param>
        /// <param name="ParentInvestmentNetWorth"></param>
        /// <param name="ParentBusinessFarmNetWorth"></param>
        /// <param name="StudentWorking"></param>
        /// <param name="StudentWorkIncome"></param>
        /// <param name="StudentTaxFiler"></param>
        /// <param name="StudentAgi"></param>
        /// <param name="StudentIncomeTax"></param>
        /// <param name="StudentUntaxedIncomeAndBenefits"></param>
        /// <param name="StudentAdditionalFinancialInfo"></param>
        /// <param name="StudentCashSavingsChecking"></param>
        /// <param name="StudentInvestmentNetWorth"></param>
        /// <param name="StudentBusinessFarmNetWorth"></param>
        /// <returns></returns>
        [HttpPost("[controller]/[action]")]
        public IActionResult Estimate(int NumberInHousehold, int NumberInCollege, string StateOfResidency, string Housing, int OldestParentAge, string MaritalStatus, 
            bool FirstParentWorking, decimal FirstParentWorkIncome, bool SecondParentWorking, decimal SecondParentWorkIncome, bool ParentsTaxFilers, decimal ParentAgi,
            decimal ParentIncomeTax, decimal ParentUntaxedIncomeAndBenefits, decimal ParentAdditionalFinancialInfo, decimal ParentCashSavingsChecking, 
            decimal ParentInvestmentNetWorth, decimal ParentBusinessFarmNetWorth, bool StudentWorking, decimal StudentWorkIncome, bool StudentTaxFiler, decimal StudentAgi, 
            decimal StudentIncomeTax, decimal StudentUntaxedIncomeAndBenefits, decimal StudentAdditionalFinancialInfo, decimal StudentCashSavingsChecking, 
            decimal StudentInvestmentNetWorth, decimal StudentBusinessFarmNetWorth)
        {
            ViewData["Title"] = "Dependent Estimate ";
            ViewData["EstimateHeaderText"] = "PLACEHOLDER Estimate Text";

            // Collect user input
            RawDependentEfcCalculatorArguments rawArgs = new RawDependentEfcCalculatorArguments();

            rawArgs.OldestParentAge = OldestParentAge.ToString();
            rawArgs.MaritalStatus = MaritalStatus;
            rawArgs.StateOfResidency = StateOfResidency;

            rawArgs.IsFirstParentWorking = FirstParentWorking.ToString();
            rawArgs.FirstParentWorkIncome = FirstParentWorkIncome.ToString();
            rawArgs.IsSecondParentWorking = SecondParentWorking.ToString();
            rawArgs.SecondParentWorkIncome = SecondParentWorkIncome.ToString();
            rawArgs.IsStudentWorking = StudentWorking.ToString();
            rawArgs.StudentWorkIncome = StudentWorkIncome.ToString();

            rawArgs.ParentAgi = ParentAgi.ToString();
            rawArgs.AreParentsTaxFilers = ParentsTaxFilers.ToString();
            rawArgs.ParentIncomeTax = ParentIncomeTax.ToString();
            rawArgs.ParentUntaxedIncomeAndBenefits = ParentUntaxedIncomeAndBenefits.ToString();
            rawArgs.ParentAdditionalFinancialInfo = ParentAdditionalFinancialInfo.ToString();

            rawArgs.StudentAgi = StudentAgi.ToString();
            rawArgs.IsStudentTaxFiler = StudentTaxFiler.ToString();
            rawArgs.StudentIncomeTax = StudentIncomeTax.ToString();
            rawArgs.StudentUntaxedIncomeAndBenefits = StudentUntaxedIncomeAndBenefits.ToString();
            rawArgs.StudentAdditionalFinancialInfo = StudentAdditionalFinancialInfo.ToString();

            rawArgs.ParentCashSavingsChecking = ParentCashSavingsChecking.ToString();
            rawArgs.ParentInvestmentNetWorth = ParentInvestmentNetWorth.ToString();
            rawArgs.ParentBusinessFarmNetWorth = ParentBusinessFarmNetWorth.ToString();

            rawArgs.StudentCashSavingsChecking = StudentCashSavingsChecking.ToString();
            rawArgs.StudentInvestmentNetWorth = StudentInvestmentNetWorth.ToString();
            rawArgs.StudentBusinessFarmNetWorth = StudentBusinessFarmNetWorth.ToString();

            rawArgs.NumberInHousehold = NumberInHousehold.ToString();
            rawArgs.NumberInCollege = NumberInCollege.ToString();

            rawArgs.MonthsOfEnrollment = "9";
            rawArgs.IsQualifiedForSimplified = "false";

            // Validate user input
            AidEstimationValidator validator = new AidEstimationValidator();
            DependentEfcCalculatorArguments args = validator.ValidateDependentEfcCalculatorArguments(rawArgs);

            // If validation fails, display errors
            if (validator.Errors.Any())
            {
                string errors = "Errors found: <ul>";

                foreach (ValidationError err in validator.Errors)
                {
                    errors += "<li>" + err.Message + "</li>";
                }
                errors += "</ul>";
                ViewData["Errors"] = errors;
                ViewData["HeaderText"] = "PLACEHOLDER Header Text";

                Dictionary<string, string> FormValues = GetModelDictionary(NumberInHousehold, NumberInCollege, StateOfResidency, Housing, OldestParentAge, MaritalStatus,
                        FirstParentWorking, FirstParentWorkIncome, SecondParentWorking, SecondParentWorkIncome, ParentsTaxFilers, ParentAgi,
                        ParentIncomeTax, ParentUntaxedIncomeAndBenefits, ParentAdditionalFinancialInfo, ParentCashSavingsChecking,
                        ParentInvestmentNetWorth, ParentBusinessFarmNetWorth, StudentWorking, StudentWorkIncome, StudentTaxFiler, StudentAgi,
                        StudentIncomeTax, StudentUntaxedIncomeAndBenefits, StudentAdditionalFinancialInfo, StudentCashSavingsChecking,
                        StudentInvestmentNetWorth, StudentBusinessFarmNetWorth);
                return View("Index", FormValues);
            }
            else
            {
                ViewData["Errors"] = "";
                EfcCalculator calculator = EfcCalculatorConfigurationManager.GetEfcCalculator("1920", AppSettings.EfcCalculationConstants);
                EfcProfile profile = calculator.GetDependentEfcProfile(args);
                CostOfAttendanceEstimator coaEstimator = CostOfAttendanceEstimatorConfigurationManager.GetCostOfAttendanceEstimator("1920", AppSettings.AidEstimationConstants);
                HousingOption ho = (HousingOption)Enum.Parse(typeof(HousingOption), Housing.ToString());
                CostOfAttendance coa = coaEstimator.GetCostOfAttendance(EducationLevel.Undergraduate, ho);

                double grantAward = 0;
                double selfHelp = Math.Max(0, AppSettings.SelfHelpConstant - profile.ExpectedFamilyContribution);
                double maxCosts = profile.ExpectedFamilyContribution + selfHelp + AppSettings.MaxLoanAmount;
                double subCosts = Math.Min(maxCosts, coa.Total);
                string AY = AppSettings.AidYear;

                if (StateOfResidency == "California")
                {
                    grantAward = coa.Total - subCosts;
                    ViewData["ShowOutOfState"] = false;
                }
                else
                {
                    grantAward = GetGrantAmount(AY, profile.ExpectedFamilyContribution, coa.Total + coa.OutOfStateFees);
                    ViewData["ShowOutOfState"] = true;
                }
                
                double parentLoan = Math.Max(0, coa.Total - grantAward - selfHelp);
                double netCosts = coa.Total - grantAward;
                double totalCOA = coa.Total;

                if (StateOfResidency != "California")
                {
                    parentLoan += coa.OutOfStateFees;
                    netCosts += coa.OutOfStateFees;
                    totalCOA += coa.OutOfStateFees;
                }

                Dictionary<string, string> EstimatedAwards = new Dictionary<string, string>();
                EstimatedAwards.Add("GrantAwards", grantAward.ToString("C0"));
                EstimatedAwards.Add("SelfHelp", selfHelp.ToString("C0"));
                EstimatedAwards.Add("ParentLoans", parentLoan.ToString("C0"));
                EstimatedAwards.Add("GrantAwardsPct", AppSettings.PercentageGrantDependant);
                EstimatedAwards.Add("NetCost", Math.Max(0, netCosts).ToString("C0"));
                EstimatedAwards.Add("OutOfStateFee", coa.OutOfStateFees.ToString("C0"));
                EstimatedAwards.Add("TotalCOA", totalCOA.ToString("C0"));
                
                ViewData["EstimatedAwards"] = EstimatedAwards;

                var tuple = (EfcProfile: profile, CostOfAttendance: coa);
                return View(tuple);

            }

        }

        /// <summary>
        /// Calculates the estimated grant amounts
        /// </summary>
        /// <param name="AY"></param>
        /// <param name="EFC"></param>
        /// <param name="COA"></param>
        /// <returns></returns>
        public double GetGrantAmount(string AY, double EFC, double COA)
        {
            // REPLACE WITH FUNCTION TO CALCULATE Grant Award
            return 0.00;
        }

        /// <summary>
        /// Creates a dictionary object from form parameters
        /// </summary>
        /// <param name="NumberInHousehold"></param>
        /// <param name="NumberInCollege"></param>
        /// <param name="StateOfResidency"></param>
        /// <param name="Housing"></param>
        /// <param name="OldestParentAge"></param>
        /// <param name="MaritalStatus"></param>
        /// <param name="FirstParentWorking"></param>
        /// <param name="FirstParentWorkIncome"></param>
        /// <param name="SecondParentWorking"></param>
        /// <param name="SecondParentWorkIncome"></param>
        /// <param name="ParentsTaxFilers"></param>
        /// <param name="ParentAgi"></param>
        /// <param name="ParentIncomeTax"></param>
        /// <param name="ParentUntaxedIncomeAndBenefits"></param>
        /// <param name="ParentAdditionalFinancialInfo"></param>
        /// <param name="ParentCashSavingsChecking"></param>
        /// <param name="ParentInvestmentNetWorth"></param>
        /// <param name="ParentBusinessFarmNetWorth"></param>
        /// <param name="StudentWorking"></param>
        /// <param name="StudentWorkIncome"></param>
        /// <param name="StudentTaxFiler"></param>
        /// <param name="StudentAgi"></param>
        /// <param name="StudentIncomeTax"></param>
        /// <param name="StudentUntaxedIncomeAndBenefits"></param>
        /// <param name="StudentAdditionalFinancialInfo"></param>
        /// <param name="StudentCashSavingsChecking"></param>
        /// <param name="StudentInvestmentNetWorth"></param>
        /// <param name="StudentBusinessFarmNetWorth"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetModelDictionary(int? NumberInHousehold, int? NumberInCollege, string StateOfResidency, string Housing, int? OldestParentAge, string MaritalStatus,
            bool? FirstParentWorking, decimal? FirstParentWorkIncome, bool? SecondParentWorking, decimal? SecondParentWorkIncome, bool? ParentsTaxFilers, decimal? ParentAgi,
            decimal? ParentIncomeTax, decimal? ParentUntaxedIncomeAndBenefits, decimal? ParentAdditionalFinancialInfo, decimal? ParentCashSavingsChecking,
            decimal? ParentInvestmentNetWorth, decimal? ParentBusinessFarmNetWorth, bool? StudentWorking, decimal? StudentWorkIncome, bool? StudentTaxFiler, decimal? StudentAgi,
            decimal? StudentIncomeTax, decimal? StudentUntaxedIncomeAndBenefits, decimal? StudentAdditionalFinancialInfo, decimal? StudentCashSavingsChecking,
            decimal? StudentInvestmentNetWorth, decimal? StudentBusinessFarmNetWorth)
        {
            var FormValues = new Dictionary<string, string>();
            FormValues.Add("NumberInHousehold", NumberInHousehold.ToString());
            FormValues.Add("NumberInCollege", NumberInCollege.ToString());
            FormValues.Add("StateOfResidency", StateOfResidency);
            FormValues.Add("Housing", Housing);
            FormValues.Add("OldestParentAge", OldestParentAge.ToString());
            FormValues.Add("MaritalStatus", MaritalStatus);
            FormValues.Add("FirstParentWorking", FirstParentWorking.ToString().ToLower());
            FormValues.Add("FirstParentWorkIncome", FirstParentWorkIncome.ToString());
            FormValues.Add("SecondParentWorking", SecondParentWorking.ToString().ToLower());
            FormValues.Add("SecondParentWorkIncome", SecondParentWorkIncome.ToString());
            FormValues.Add("ParentsTaxFilers", ParentsTaxFilers.ToString().ToLower());
            FormValues.Add("ParentAgi", ParentAgi.ToString());
            FormValues.Add("ParentIncomeTax", ParentIncomeTax.ToString());
            FormValues.Add("ParentUntaxedIncomeAndBenefits", ParentUntaxedIncomeAndBenefits.ToString());
            FormValues.Add("ParentAdditionalFinancialInfo", ParentAdditionalFinancialInfo.ToString());
            FormValues.Add("ParentCashSavingsChecking", ParentCashSavingsChecking.ToString());
            FormValues.Add("ParentInvestmentNetWorth", ParentInvestmentNetWorth.ToString());
            FormValues.Add("ParentBusinessFarmNetWorth", ParentBusinessFarmNetWorth.ToString());
            FormValues.Add("StudentWorking", StudentWorking.ToString().ToLower());
            FormValues.Add("StudentWorkIncome", StudentWorkIncome.ToString());
            FormValues.Add("StudentTaxFiler", StudentTaxFiler.ToString().ToLower());
            FormValues.Add("StudentAgi", StudentAgi.ToString());
            FormValues.Add("StudentIncomeTax", StudentIncomeTax.ToString());
            FormValues.Add("StudentUntaxedIncomeAndBenefits", StudentUntaxedIncomeAndBenefits.ToString());
            FormValues.Add("StudentAdditionalFinancialInfo", StudentAdditionalFinancialInfo.ToString());
            FormValues.Add("StudentCashSavingsChecking", StudentCashSavingsChecking.ToString());
            FormValues.Add("StudentInvestmentNetWorth", StudentInvestmentNetWorth.ToString());
            FormValues.Add("StudentBusinessFarmNetWorth", StudentBusinessFarmNetWorth.ToString());
            return FormValues;
        }

    }
}
