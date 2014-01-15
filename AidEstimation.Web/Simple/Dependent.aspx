<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Dependent.aspx.cs" Inherits="Ucsb.Sa.FinAid.AidEstimation.Web.Simple.Dependent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
  
    <p>
        This Financial Aid Estimator provides an estimation of financial aid for <strong>prospective full-time,
        dependent undergraduate students</strong>. The estimated values produced by this tool are
        <strong>not</strong> the actual amounts that will be offered by an institution. All estimated values
        are <strong>subject to the availability of funding</strong>. To begin the actual financial aid
        application process, complete a <a href="http://www.fafsa.ed.gov/">FAFSA</a>.
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
        
        <form method="post" action="Dependent.aspx" runat="server">
            <ul>
                <li>
                    <fieldset>
                        <legend>Parent(s)' Marital Status</legend>
                        <asp:RadioButtonList runat="server" ID="inputMaritalStatus" RepeatLayout="UnorderedList">
                            <asp:ListItem Value="single">Single/Separated/Divorced</asp:ListItem>
                            <asp:ListItem Value="married">Married/Remarried</asp:ListItem>
                        </asp:RadioButtonList>
                    </fieldset>
                </li>
                <li>
                    <asp:Label runat="server" AssociatedControlID="inputParentIncome">
                        Parent(s)' Income
                        <span class="description">Income earned by your Parent(s) during the last year</span>
                    </asp:Label>
                    <asp:TextBox runat="server" ID="inputParentIncome"></asp:TextBox>
                </li>
                <li>
                    <asp:Label runat="server" AssociatedControlID="inputParentOtherIncome">
                        Parent(s)' Other Income
                        <span class="description">Income earned by your Parent(s) during the last year that was <em>not</em> reported on their tax return</span>
                    </asp:Label>
                    <asp:TextBox runat="server" ID="inputParentOtherIncome"></asp:TextBox>
                </li>
                <li>
                    <fieldset>
                        <legend>Parent(s)' Income Earned By</legend>
                        <asp:RadioButtonList runat="server" ID="inputParentIncomeEarnedBy" RepeatLayout="UnorderedList">
                            <asp:ListItem Value="none">Neither Parents</asp:ListItem>
                            <asp:ListItem Value="one">One Parent</asp:ListItem>
                            <asp:ListItem Value="both">Both Parents</asp:ListItem>
                        </asp:RadioButtonList>
                    </fieldset>
                </li>
                <li>
                    <asp:Label runat="server" AssociatedControlID="inputParentIncomeTax">Parent(s)' Income Tax Paid</asp:Label>
                    <asp:TextBox runat="server" ID="inputParentIncomeTax"></asp:TextBox>
                </li>
                <li>
                    <asp:Label runat="server" AssociatedControlID="inputParentAssets">
                        Parent(s)' Assets
                        <span class="description">Total value of your Parent(s)' assets in the last year, including the total amount in cash, savings, and checking, and the net worth of all investments</span>
                    </asp:Label>
                    <asp:TextBox runat="server" ID="inputParentAssets"></asp:TextBox>
                </li>
                <li>
                    <asp:Label runat="server" AssociatedControlID="inputStudentIncome">
                        Student's Income
                        <span class="description">Income earned by you during the last year</span>
                    </asp:Label>
                    <asp:TextBox runat="server" ID="inputStudentIncome"></asp:TextBox>
                </li>
                <li>
                    <asp:Label runat="server" AssociatedControlID="inputStudentOtherIncome">
                        Student's Other Income
                        <span class="description">Income earned by you during the last year that was <em>not</em> reported on your tax return</span>
                    </asp:Label>
                    <asp:TextBox runat="server" ID="inputStudentOtherIncome"></asp:TextBox>
                </li>
                <li>
                    <asp:Label runat="server" AssociatedControlID="inputStudentIncomeTax">Student's Income Tax Paid</asp:Label>
                    <asp:TextBox runat="server" ID="inputStudentIncomeTax"></asp:TextBox>
                </li>
                <li>
                    <asp:Label runat="server" AssociatedControlID="inputStudentAssets">
                        Student's Assets
                        <span class="description">Total value of your assets in the last year, including the total amount in cash, savings, and checking, and the net worth of all investments</span>
                    </asp:Label>
                    <asp:TextBox runat="server" ID="inputStudentAssets"></asp:TextBox>
                </li>
                <li>
                    <asp:Label runat="server" AssociatedControlID="inputNumberInHousehold">
                        Number in Household
                        <span class="description">Total number in your household during the last academic year. Include you, your parents, and your parent(s)' dependents</span>
                    </asp:Label>
                    <asp:TextBox runat="server" ID="inputNumberInHousehold"></asp:TextBox>
                </li>
                <li>
                    <asp:Label runat="server" AssociatedControlID="inputNumberInCollege">
                        Number in College
                        <span class="description">Total number of people in your household that were in college during the last academic year, not including your parent(s)</span>
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

        <a href="Dependent.aspx">&laquo; Return to Calculator</a>
        
    </asp:Placeholder>

</asp:Content>