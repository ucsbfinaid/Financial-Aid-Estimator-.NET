<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Independent.aspx.cs" Inherits="Ucsb.Sa.FinAid.AidEstimation.Web.Full.Independent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">

    <p>
        This Financial Aid Estimator provides an estimation of financial aid for <strong>prospective full-time,
        independent undergraduate students</strong>. The estimated values produced by this tool are <strong>not</strong>
        the actual amounts that will be offered by an institution. All estimated values are <strong>subject to the
        availability of funding</strong>. To begin the actual financial aid application process, complete a
        <a href="http://www.fafsa.ed.gov/">FAFSA</a>.
    </p>

    <asp:Placeholder ID="formPlaceholder" runat="server" Visible="true">

        <asp:Repeater ID="errorList" runat="server">
            <HeaderTemplate>
                <p>There was an error with the values for the following fields:</p>
                <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li><%# Eval("Message") %></li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>

        <form method="post" action="Independent.aspx" runat="server">
            <ul>
                <li>
                    <asp:Label runat="server" AssociatedControlID="inputStudentAge">Age of Student</asp:Label>
                    <asp:TextBox runat="server" ID="inputStudentAge"></asp:TextBox>
                </li>
                <li>
                    <fieldset>
                        <legend>Marital Status</legend>
                        <asp:RadioButtonList runat="server" ID="inputMaritalStatus" RepeatLayout="UnorderedList">
                            <asp:ListItem Value="single">Single/Separated/Divorced</asp:ListItem>
                            <asp:ListItem Value="married">Married/Remarried</asp:ListItem>
                        </asp:RadioButtonList>
                    </fieldset>
                </li>
                <li>
                    <fieldset>
                        <legend>Did the Student Work?</legend>
                        <asp:RadioButtonList runat="server" ID="inputStudentWorking" RepeatLayout="UnorderedList">
                            <asp:ListItem Value="true">Yes, I worked</asp:ListItem>
                            <asp:ListItem Value="false">No, I did <em>not</em> work</asp:ListItem>
                        </asp:RadioButtonList>
                    </fieldset>
                </li>
                <li>
                    <asp:Label runat="server" AssociatedControlID="inputStudentWorkIncome">Student's Income Earned From Work</asp:Label>
                    <asp:TextBox runat="server" ID="inputStudentWorkIncome"></asp:TextBox>
                </li>
                <li>
                    <fieldset>
                        <legend>Did the Spouse Work?</legend>
                        
                        <asp:RadioButtonList runat="server" ID="inputSpouseWorking">
                            <asp:ListItem Value="true">Yes, my Spouse worked</asp:ListItem>
                            <asp:ListItem Value="false">No, my Spouse did <em>not</em> work</asp:ListItem>
                        </asp:RadioButtonList>
                    </fieldset>
                </li>
                <li>
                    <asp:Label runat="server" AssociatedControlID="inputSpouseWorkIncome">Spouse's Income Earned From Work</asp:Label>
                    <asp:TextBox runat="server" ID="inputSpouseWorkIncome"></asp:TextBox>
                </li>
                <li>
                    <asp:Label runat="server" AssociatedControlID="inputStudentAgi">Student and Spouse's Adjusted Gross Income (AGI)</asp:Label>
                    <asp:TextBox runat="server" ID="inputStudentAgi"></asp:TextBox>
                </li>
                <li>
                    <fieldset>
                        <legend>Did the Student and Spouse File Taxes?</legend>
                        
                        <asp:RadioButtonList runat="server" ID="inputStudentTaxFiler">
                            <asp:ListItem Value="true">Yes, we filed taxes</asp:ListItem>
                            <asp:ListItem Value="false">No, we did <em>not</em> files taxes</asp:ListItem>
                        </asp:RadioButtonList>
                    </fieldset>
                </li>
                <li>
                    <asp:Label runat="server" AssociatedControlID="inputStudentIncomeTax">Student and Spouse's Total Income Taxes Paid</asp:Label>
                    <asp:TextBox runat="server" ID="inputStudentIncomeTax"></asp:TextBox>
                </li>
                <li>
                    <asp:Label runat="server" AssociatedControlID="inputStudentUntaxedIncomeAndBenefits">
                        Student and Spouse's Untaxed Income and Benefits
                        <span class="description">Including child support and money received from friends or relatives</span>
                    </asp:Label>
                    <asp:TextBox runat="server" ID="inputStudentUntaxedIncomeAndBenefits"></asp:TextBox>
                </li>
                <li>
                    <asp:Label runat="server" AssociatedControlID="inputStudentAdditionalFinancialInfo">Student and Spouse's Additional Financial Information</asp:Label>
                    <asp:TextBox runat="server" ID="inputStudentAdditionalFinancialInfo"></asp:TextBox>
                </li>
                <li>
                    <asp:Label runat="server" AssociatedControlID="inputStudentCashSavingsChecking">Student and Spouse's Cash, Savings, and Checking</asp:Label>
                    <asp:TextBox runat="server" ID="inputStudentCashSavingsChecking"></asp:TextBox>
                </li>
                <li>
                    <asp:Label runat="server" AssociatedControlID="inputStudentInvestmentNetWorth">Net Worth of Student and Spouse's Investments</asp:Label>
                    <asp:TextBox runat="server" ID="inputStudentInvestmentNetWorth"></asp:TextBox>
                </li>
                <li>
                    <asp:Label runat="server" AssociatedControlID="inputStudentBusinessFarmNetWorth">Net Worth of Student and Spouse's Business and/or Investment Farm</asp:Label>
                    <asp:TextBox runat="server" ID="inputStudentBusinessFarmNetWorth"></asp:TextBox>
                </li>
                <li>
                    <fieldset>
                        <legend>Student has Dependents</legend>
                        
                        <asp:RadioButtonList runat="server" ID="inputHasDependents">
                            <asp:ListItem Value="true">Yes, I have dependents</asp:ListItem>
                            <asp:ListItem Value="false">No, I do <em>not</em> have dependents</asp:ListItem>
                        </asp:RadioButtonList>
                    </fieldset>
                </li>
                <li>
                    <asp:Label runat="server" AssociatedControlID="inputNumberInHousehold">
                        Number in Household
                        <span class="description">Total number in your household during the last academic year. Include you, your spouse, your children, and any other dependents</span>
                    </asp:Label>
                    <asp:TextBox runat="server" ID="inputNumberInHousehold"></asp:TextBox>
                </li>
                <li>
                    <asp:Label runat="server" AssociatedControlID="inputNumberInCollege">
                        Number in College
                        <span class="description">Total number of people in your household that were in college during the last academic year</span>
                    </asp:Label>
                    <asp:TextBox runat="server" ID="inputNumberInCollege"></asp:TextBox>
                </li>
                <li>
                    <asp:Label runat="server" AssociatedControlID="inputStateOfResidency">State of Residency</asp:Label>
                    <asp:DropDownList runat="server" ID="inputStateOfResidency">
                        <asp:ListItem Value="California">California</asp:ListItem>
                        <asp:ListItem Value="CanadaAndCanadianProvinces">Canada And Canadian Provinces</asp:ListItem>
                        <asp:ListItem Value="Colorado">Colorado</asp:ListItem>
                        <asp:ListItem Value="Connecticut">Connecticut</asp:ListItem>
                        <asp:ListItem Value="Delaware">Delaware</asp:ListItem>
                        <asp:ListItem Value="DistrictOfColumbia">District Of Columbia</asp:ListItem>
                        <asp:ListItem Value="FederatedStatesOfMicronesia">Federated States Of Micronesia</asp:ListItem>
                        <asp:ListItem Value="Florida">Florida</asp:ListItem>
                        <asp:ListItem Value="Georgia">Georgia</asp:ListItem>
                        <asp:ListItem Value="Guam">Guam</asp:ListItem>
                        <asp:ListItem Value="Hawaii">Hawaii</asp:ListItem>
                        <asp:ListItem Value="Idaho">Idaho</asp:ListItem>
                        <asp:ListItem Value="Illinois">Illinois</asp:ListItem>
                        <asp:ListItem Value="Indiana">Indiana</asp:ListItem>
                        <asp:ListItem Value="Iowa">Iowa</asp:ListItem>
                        <asp:ListItem Value="Kansas">Kansas</asp:ListItem>
                        <asp:ListItem Value="Kentucky">Kentucky</asp:ListItem>
                        <asp:ListItem Value="Louisiana">Louisiana</asp:ListItem>
                        <asp:ListItem Value="Maine">Maine</asp:ListItem>
                        <asp:ListItem Value="MarshallIslands">Marshall Islands</asp:ListItem>
                        <asp:ListItem Value="Maryland">Maryland</asp:ListItem>
                        <asp:ListItem Value="Massachusetts">Massachusetts</asp:ListItem>
                        <asp:ListItem Value="Mexico">Mexico</asp:ListItem>
                        <asp:ListItem Value="Michigan">Michigan</asp:ListItem>
                        <asp:ListItem Value="Minnesota">Minnesota</asp:ListItem>
                        <asp:ListItem Value="Mississippi">Mississippi</asp:ListItem>
                        <asp:ListItem Value="Missouri">Missouri</asp:ListItem>
                        <asp:ListItem Value="Montana">Montana</asp:ListItem>
                        <asp:ListItem Value="Nebraska">Nebraska</asp:ListItem>
                        <asp:ListItem Value="Nevada">Nevada</asp:ListItem>
                        <asp:ListItem Value="New Hampshire">New Hampshire</asp:ListItem>
                        <asp:ListItem Value="NewJersey">New Jersey</asp:ListItem>
                        <asp:ListItem Value="NewMexico">New Mexico</asp:ListItem>
                        <asp:ListItem Value="NewYork">New York</asp:ListItem>
                        <asp:ListItem Value="NorthCarolina">North Carolina</asp:ListItem>
                        <asp:ListItem Value="NorthDakota">North Dakota</asp:ListItem>
                        <asp:ListItem Value="NorthernMarianaIslands">Northern Mariana Islands</asp:ListItem>
                        <asp:ListItem Value="Ohio">Ohio</asp:ListItem>
                        <asp:ListItem Value="Oklahoma">Oklahoma</asp:ListItem>
                        <asp:ListItem Value="Oregon">Oregon</asp:ListItem>
                        <asp:ListItem Value="Palau">Palau</asp:ListItem>
                        <asp:ListItem Value="Pennsylvania">Pennsylvania</asp:ListItem>
                        <asp:ListItem Value="PuertoRico">Puerto Rico</asp:ListItem>
                        <asp:ListItem Value="RhodeIsland">Rhode Island</asp:ListItem>
                        <asp:ListItem Value="SouthCarolina">South Carolina</asp:ListItem>
                        <asp:ListItem Value="SouthDakota">South Dakota</asp:ListItem>
                        <asp:ListItem Value="Tennessee">Tennessee</asp:ListItem>
                        <asp:ListItem Value="Texas">Texas</asp:ListItem>
                        <asp:ListItem Value="Utah">Utah</asp:ListItem>
                        <asp:ListItem Value="Vermont">Vermont</asp:ListItem>
                        <asp:ListItem Value="VirginIslands">Virgin Islands</asp:ListItem>
                        <asp:ListItem Value="Virginia">Virginia</asp:ListItem>
                        <asp:ListItem Value="Washington">Washington</asp:ListItem>
                        <asp:ListItem Value="WestVirginia">West Virginia</asp:ListItem>
                        <asp:ListItem Value="Wisconsin">Wisconsin</asp:ListItem>
                        <asp:ListItem Value="Wyoming">Wyoming</asp:ListItem>
                        <asp:ListItem Value="Other">Other</asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li>
                    <asp:Button runat="server" UseSubmitBehavior="true" Text="Calculate EFC" />
                </li>
            </ul>
        </form>

    </asp:Placeholder>

    <asp:Placeholder ID="resultsPlaceholder" runat="server" Visible="false">
        
        <h3>Expected Family Contribution</h3>
        
        <ul>
            <li>Student Contribution (SC): <asp:Literal ID="studentContributionOutput" runat="server" /></li>
            <li>Parent Contribution (PC): <asp:Literal ID="parentContributionOutput" runat="server" /></li>
            <li>Expected Family Contribution (EFC): <asp:Literal ID="expectedFamilyContributionOutput" runat="server" /></li>
        </ul>

        <a href="Independent.aspx">&laquo; Return to Calculator</a>

    </asp:Placeholder>

</asp:Content>