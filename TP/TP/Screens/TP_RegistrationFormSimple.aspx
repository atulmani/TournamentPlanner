<%@ Page Title="" Language="C#" MasterPageFile="~/Screens/TP.Master" AutoEventWireup="true"
    CodeBehind="TP_RegistrationFormSimple.aspx.cs" Inherits="TournamentPlanner.TP_RegistrationFormSimple" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
         
    /*Wait Panel - Start*/
    .pleaseWait 
    {
        color:rgb(4,163,233);
        font-family:Arial;
        font-size:28px;
        font-weight:bold;
        /*border: 5px solid #67CFF5;*/
        text-align:center;
        width: 100%; 
        height: 40px; 
        background-color:#0a0a0a; 
        display:none; 
        z-index:999; 
        /*position:absolute; 
        left:40%; 
        top:60%;*/
        position:fixed;
        left: 0;
        top:50%;
     }
     
     /*Wait Panel - End*/
    
    </style>

    <script type="text/javascript" language="javascript">

        //Wait Panel - Start
        function showLoading() {
            $('.pleaseWait').show();

        }

        function pageLoad() {
            $('.pleaseWait').fadeOut(200);
        }
        //Wait Panel - End


        function Validate(sender, args) {

            var gridView = document.getElementById("dgEventParticipation");

            var checkBoxes = gridView.getElementsByTagName("input");

            for (var i = 0; i < checkBoxes.length; i++) {

                if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked) {

                    //      args.IsValid = true;
                    alert("test 1")
                    //      return;

                }

            }
            alert("test 2");

            //args.IsValid = false;

        }

        function myFunction() {
            var x = document.getElementById('test');
            if (x.style.display === 'none') {
                x.style.display = 'block';
            } else {
                x.style.display = 'none';
            }
        }

        function show() {
            if (document.getElementById('benefits').style.display == 'none') {
                document.getElementById('benefits').style.display = 'block';
            }
            else {
                document.getElementById('benefits').style.display = 'none';
            }
        }

        function close() {
            if (document.getElementById('benefits').style.display == 'block') {
                document.getElementById('benefits').style.display = 'none';
            }
        }

        $(".form_datetime").datetimepicker({ format: 'yyyy-mm-dd hh:ii' });

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="scriptmanager1" runat="server">
    </asp:ScriptManager>

    <!-- Wait Panel - Start -->
    <div class="pleaseWait">
        Loading.....   please wait!
    </div>
    <!-- Wait Panel - End -->

    <!-- Tournament Details - Header -->
    <div>
        <table width="100%">
            <tr>
                <td>
                    <div class="h3BoldText_Atul">
                        <asp:Label ID="lblTournamentName" Text="" runat="server"></asp:Label>
                    </div>
                    <div class="h4Text_Atul">
                        <asp:Label ID="lblTournamentOrganisation" Text="" runat="server"></asp:Label>
                    </div>
                    <div class="h4Text_Atul">
                        <asp:Label ID="lbl22" Text="Venue: " runat="server" class="h4BoldText_Atul"></asp:Label>
                        <asp:Label ID="lblTournamentVenue" Text="" runat="server"></asp:Label>
                    </div>
                    <!--<div class="h4Text_Atul">
                        <asp:Label ID="lblLocationAddress" Text="" runat="server"></asp:Label>
                    </div>
                    <div class="h4Text_Atul">
                        <asp:Label ID="lbl24" Text="Contact: " runat="server" class="h4BoldText_Atul"></asp:Label>
                        <asp:Label ID="lblTournamentContacts" Text="" runat="server"></asp:Label>
                    </div>
                    <div class="h4Text_Atul">
                        <asp:Label ID="lbl25" Text="Entry Open: " runat="server" class="h4BoldText_Atul"></asp:Label>
                        <asp:Label ID="lblEntryOpenDate" Text="" runat="server"></asp:Label>
                    </div>
                    <div class="h4Text_Atul">
                        <asp:Label ID="lbl26" Text="Entry Closes: " runat="server" class="h4BoldText_Atul"></asp:Label>
                        <asp:Label ID="lblEntryClosesDate" Text="" runat="server"></asp:Label>
                    </div>
                    <div class="h4Text_Atul">
                        <asp:Label ID="lbl27" Text="Withdrawal: " runat="server" class="h4BoldText_Atul"></asp:Label>
                        <asp:Label ID="lblEntryWithdrawalDate" Text="" runat="server"></asp:Label>
                    </div>-->
                    <div class="h4Text_Atul">
                        <asp:Label ID="lbl28" Text="Duration: " runat="server" class="h4BoldText_Atul"></asp:Label>
                        <asp:Label ID="lblTournamentDuration" Text="" runat="server"></asp:Label>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <!-- TP Menu -->
    <div style="text-align: center;">
        <table border="0" cellspacing="0" style='width: 100%; height: 25px;'>
            <tr>
                <td style='text-align: center;'>
                    <asp:LinkButton ID="lbtnHome" Text="HOME" runat="server" OnClick="lbtnTPMenu_Click"
                        Width="100%" class="tabMenuInactive_Atul" Style="padding-top: 10px;" />
                </td>
                <td>
                    <asp:LinkButton ID="lbtnEvents" Text="EVENTS" runat="server" OnClick="lbtnTPMenu_Click"
                        Width="100%" class="tabMenuInactive_Atul" Style="padding-top: 10px;" />
                </td>
                <td>
                    <asp:LinkButton ID="lbtnPlayers" Text="PLAYERS" runat="server" OnClick="lbtnTPMenu_Click"
                        Width="100%" class="tabMenuInactive_Atul" Style="padding-top: 10px;" />
                </td>
                <td>
                    <asp:LinkButton ID="lbtnDraws" Text="DRAWS" runat="server" OnClick="lbtnTPMenu_Click"
                        Width="100%" class="tabMenuInactive_Atul" Style="padding-top: 10px;" />
                </td>
                <td>
                    <asp:LinkButton ID="lbtnMatches" Text="MATCHES" runat="server" OnClick="lbtnTPMenu_Click"
                        Width="100%" class="tabMenuInactive_Atul" Style="padding-top: 10px;" />
                </td>
            </tr>
        </table>
    </div>
    <!-- Main Body -->
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <div style='padding-top: 2px; min-height: 400px; width: 100%; background: rgb(256,256,256);'>
                <div class="pageTopic">
                    Registration Form
                </div>
                <!-- Show Error Messages -->
                <asp:Panel runat="server" ID="pnlErrorMsg" Visible="false" Height="20px" Style='text-align: center;
                    margin-top: 20px;' class="panelRegistration_Atul">
                    <asp:Label ID="lblErrorMsg" runat="server" Text="" Style="color: red;"></asp:Label>
                </asp:Panel>


                <div class="form_Atul">
                    <!-- Player Details -->
                    <div class="topPaddingBetweenObjects_Atul">
                        <div style="width: 100%;">
                            <asp:Button ID="btnTogglePlayerDetails" Text="Step 1: Player Details" runat="server"
                                OnClick="btnTogglePlayerDetails_Click" Style="background: rgb(220,220,220); color: rgb(4,163,233);
                                text-align: left; width: 80%; height: 58px; border: none; font: bold 16pt Calibri;" />
                            <asp:Button ID="btnTogglePlayerDetailsPlus" Text="+" runat="server" OnClick="btnTogglePlayerDetails_Click"
                                Style="background: rgb(220,220,220); color: rgb(251,85,58); float: right; width: 20%;
                                height: 58px; border: none; font: bold 30pt Calibri;" />
                        </div>
                        <asp:Panel runat="server" ID="pnlPlayerDetails" Visible="false" class="panelRegistration_Atul" style="background-color:White;">
                            <div style="text-align: center; padding: 5px;">
                                <asp:Label ID="lblErrMsgPlayerDetails" runat="server" Text="" Style="color: red;"></asp:Label>
                            </div>
                            
                            <div style="display:block;">
                            <div class="h2BoldText_Atul" style="border-right: 0px; color: rgb(4,163,233); text-align:center; margin-top:10px;">
                                ALREADY REGISTERED?
                            </div>
                            <div class="h3Text_Atul" style="border-right: 0px; color: rgb(4,163,233); text-align:center;">
                                If already Registered for the Tournament and would like to Register for more Events/Categories
                            </div>
                            <div class="h3BoldText_Atul" style="margin-top:25px; text-align:center;">
                                Enter Your Registered Mobile No:
                            </div>
                            <div class="h3BoldText_Atul" style="text-align:center;">                                                     
                                <asp:TextBox ID="txtAlreadyRegisteredMobile" Text="" runat="server" Width="70%" MaxLength="10" TextMode="Number" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);"
                                    class="textBox_Atul" Style="text-align: center;" />
                            </div>
                            <div style="text-align: center; margin-top: 15px;">
                                <asp:Button ID="btnSubmit" Text="SUBMIT" runat="server" class="button_Atul" Width="160px" OnClientClick="showLoading();" OnClick="btnCheckAlreadyRegPlayer_Click"  />
                            </div>
                            
                            <asp:Panel runat="server" ID="pnlAlreadyRegisteredPlayers" Visible="false" class="panelRegistration_Atul" style="background-color:White;">
                                <div class="h3Text_Atul" style="border-right: 0px; color: rgb(4,163,233); text-align:center; margin-top:25px;">
                                    Select Registered Player to Register for more events/categories
                                </div>
                                <div>
                                    <asp:DropDownList ID="ddlAlreadyRegisteredPlayers" runat="server" Width="90%" class="dropdown_Atul"  
                                    OnClientClick="showLoading();" 
                                    OnSelectedIndexChanged="ddlAlreadyRegisteredPlayers_SelectedIndexChanged"  AutoPostBack=true
                                    style="text-align:center;">
                                    
                                    </asp:DropDownList>                                                    
                                </div>
                            </asp:Panel>
                            <hr />
                            <div class="h2BoldText_Atul" style="border-right: 0px; color: rgb(4,163,233); text-align:center; margin-top:0px;">
                                ELSE
                            </div>

                            <div class="h3Text_Atul" style="border-right: 0px; color: rgb(4,163,233); text-align:center;">
                                Register Yourself Here
                            </div>
                            </div>
                            <table border="0" width="100%" cellpadding="0px" cellspacing="0px" style="margin-top:15px;">
                                <tr>
                                    <td style="border-right: 0px; color: rgb(4,163,233); padding-left: 20px;">
                                        Player First Name:
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-right: 0px;">
                                        <asp:TextBox ID="txtFirstName" runat="server" Width="90%" MaxLength="16" class="textBox_Atul"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-right: 0px; color: rgb(4,163,233); padding-left: 20px;" class="topPaddingBetweenObjects_Atul">
                                        Player Last Name:
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-right: 0px;">
                                        <asp:TextBox ID="txtLastName" runat="server" Width="90%" MaxLength="16" class="textBox_Atul" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-right: 0px; color: rgb(4,163,233); padding-left: 20px;" class="topPaddingBetweenObjects_Atul">
                                        Player Contact:
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-right: 0px;">
                                        <asp:TextBox ID="txtContact" Text="" runat="server" Width="90%" MaxLength="10" TextMode="Number" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);"
                                            class="textBox_Atul" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-right: 0px; color: rgb(4,163,233); padding-left: 20px;" class="topPaddingBetweenObjects_Atul">
                                        Player Email ID:
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-right: 0px;">
                                        <asp:TextBox ID="txtEmailID" Text="" runat="server" Width="90%" MaxLength="50" class="textBox_Atul" />
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td style="border-right: 0px; color: rgb(4,163,233); padding-left: 20px;" class="topPaddingBetweenObjects_Atul">
                                        Player State:
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td style="border-right: 0px;">
                                        <asp:DropDownList ID="ddlStates" runat="server" Width="90%" class="dropdown_Atul">
                                            <asp:ListItem Text="Maharashtra" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td style="border-right: 0px; color: rgb(4,163,233); padding-left: 20px;" class="topPaddingBetweenObjects_Atul">
                                        Player District:
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td style="border-right: 0px;">
                                        <asp:DropDownList ID="ddlDistricts" runat="server" Width="90%" class="dropdown_Atul">
                                            <asp:ListItem Text="Pune" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td style="border-right: 0px; color: rgb(4,163,233); padding-left: 20px;" class="topPaddingBetweenObjects_Atul">
                                        Player City:
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td style="border-right: 0px;">
                                        <asp:TextBox ID="txtCity" Text="Pune" runat="server" Width="90%" MaxLength="20" ReadOnly="true"
                                            class="textBox_Atul" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-right: 0px; color: rgb(4,163,233); padding-left: 20px;" class="topPaddingBetweenObjects_Atul">
                                        Player Address:
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-right: 0px;">
                                        <asp:TextBox ID="txtAddress" Text="" runat="server" Width="90%" MaxLength="140" class="textBox_Atul" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                    <!-- Event Details -->
                    <div class="topPaddingBetweenObjects_Atul">
                        <div style="width: 100%;">
                            <asp:Button ID="btnToggleAddEvent" Text="Step 2: Select Events" runat="server" OnClick="btnToggleAddEvent_Click"
                                Style="background: rgb(220,220,220); color: rgb(4,163,233); text-align: left;
                                width: 80%; height: 58px; border: none; font: bold 16pt Calibri;" />
                            <asp:Button ID="btnToggleAddEventPlus" Text="+" runat="server" OnClick="btnToggleAddEvent_Click"
                                Style="background: rgb(220,220,220); color: rgb(251,85,58); float: right; width: 20%;
                                height: 58px; border: none; font: bold 30pt Calibri;" />
                        </div>
                        <asp:Panel runat="server" ID="pnlAddEvent" Visible="false" Width="100%" class="panelRegistration_Atul">
                            <div style="text-align: center; padding: 4px;">
                                <asp:Label ID="lblErrMsgPAddEvents" runat="server" Text="" Style="color: red;"></asp:Label>
                            </div>
                            <div>
                                <table width="100%" cellpadding="0px" cellspacing="0px">
                                    <tr>
                                        <td style="text-align: center; padding-top: 28px;" class="h4Text_Atul">
                                            Event/Categories will be populated based on the selection of Gender & Date Of Birth
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td style="border-right: 0px; color: rgb(4,163,233); padding-left: 20px;" class="topPaddingBetweenObjects_Atul">
                                            Player Gender:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-right: 0px;">
                                            <asp:DropDownList ID="ddlGender" runat="server" Width="94%" class="dropdown_Atul">
                                                <asp:ListItem Text="Select Gender" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                                                <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-right: 0px; color: rgb(4,163,233); padding-left: 20px;" class="topPaddingBetweenObjects_Atul">
                                            Player Date of Birth
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-right: 0px; width: 100%;" >
                                            <asp:DropDownList ID="ddlDate" runat="server" Width="25%" class="dropdown_Atul">
                                                <asp:ListItem Text="Select Date" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="01" Value="01"></asp:ListItem>
                                                <asp:ListItem Text="02" Value="02"></asp:ListItem>
                                                <asp:ListItem Text="03" Value="03"></asp:ListItem>
                                                <asp:ListItem Text="04" Value="04"></asp:ListItem>
                                                <asp:ListItem Text="05" Value="05"></asp:ListItem>
                                                <asp:ListItem Text="06" Value="06"></asp:ListItem>
                                                <asp:ListItem Text="07" Value="07"></asp:ListItem>
                                                <asp:ListItem Text="08" Value="08"></asp:ListItem>
                                                <asp:ListItem Text="09" Value="09"></asp:ListItem>
                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlMonth" runat="server" Width="30%" class="dropdown_Atul">
                                                <asp:ListItem Text="Select Month" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Jan" Value="01"></asp:ListItem>
                                                <asp:ListItem Text="Feb" Value="02"></asp:ListItem>
                                                <asp:ListItem Text="Mar" Value="03"></asp:ListItem>
                                                <asp:ListItem Text="Apr" Value="04"></asp:ListItem>
                                                <asp:ListItem Text="May" Value="05"></asp:ListItem>
                                                <asp:ListItem Text="Jun" Value="06"></asp:ListItem>
                                                <asp:ListItem Text="Jul" Value="07"></asp:ListItem>
                                                <asp:ListItem Text="Aug" Value="08"></asp:ListItem>
                                                <asp:ListItem Text="Sep" Value="09"></asp:ListItem>
                                                <asp:ListItem Text="Oct" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="Nov" Value="11"></asp:ListItem>
                                                <asp:ListItem Text="Dec" Value="12"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtDOBYear" runat="server" MaxLength="4" Placeholder="YYYY" Width="30%"
                                                onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);" class="textBox_Atul" style="text-align: center;" />
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td style="text-align: center; padding-top: 38px;">
                                            <!-- Wait Panel - Method Calling - OnClientClick="showLoading();" -->
                                            <asp:Button ID="btnPopulateEventCategory" Text="Show Events" runat="server"
                                                OnClientClick="showLoading();" OnClick="btnPopulateEventCategory_Click" class="button_Atul" Width="250px" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <hr />
                            <table border="0" align="top" width="100%" style="padding-top: 1px;">
                                <tr>
                                    <th style="border-right: 0px; text-align: center;" class="h4BoldText_Atul">
                                        Tournament Events
                                        <br />
                                        <asp:Label ID="lblTournamentEvents" Text="Tournament Events will be shown once Gender and Date Of Birth will be filled and Click on Show Events Button" runat="server" class="h4Text_Atul" />
                                    </th>
                                </tr>
                                <tr>
                                    <td style="border-right: 0px;" class="h4Text_Atul">
                                        <asp:DataGrid ID="dgEventParticipation" Width="95%" runat="server" PageSize="1" AllowPaging="False"
                                            AutoGenerateColumns="False" CellPadding="0" CellSpacing="10" GridLines="None"
                                            OnDeleteCommand="dgEventParticipation_DeleteCommand">
                                            <Columns>
                                                <asp:BoundColumn HeaderText="Sr#" DataField="ID"></asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="Event" ItemStyle-Width="25%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="dgtbEventCode" runat="server" Text='<%# Bind("Event") %>'></asp:Label>
                                                        <asp:HiddenField ID="hfReferenceDOB" runat="server" Value='<%# Bind("ReferenceDate") %>' />
                                                        <asp:HiddenField ID="hfAfterBefore" runat="server" Value='<%# Bind("AfterBefore") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Partner Name" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="dgtbPartnerName" runat="server" Width="100%" Text="" class="textBoxUnderline_Atul"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Add Partner" ItemStyle-Width="35%">
                                                    <ItemTemplate>
                                                        <asp:Button ID="dgtbAddPartner" runat="server" Text="Add Partner" Width="100%" OnClick="btnAddPartner_Click"
                                                            enable="False" class="button_Atul"></asp:Button>
                                                        <asp:TextBox ID="dgtbPartnerDOB" runat="server" Width="100%" Text="dd/mm/yyyy" class="textBoxUnderline_Atul"
                                                            Visible="false"></asp:TextBox>
                                                        <asp:HiddenField ID="hfFees" runat="server" Value='<%# Bind("Fees") %>' Visible="false">
                                                        </asp:HiddenField>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn HeaderText="Fees" DataField="Fees"></asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="Select" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="dgcbEventSelect" runat="server" 
                                                            OnClientClick="showLoading();" OnCheckedChanged="chkview_CheckedChanged"
                                                            AutoPostBack="true" CssClass="mycheckbox"></asp:CheckBox>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="Black" />
                                            <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" Mode="NumericPages" />
                                            <AlternatingItemStyle BackColor="#F5F5F5" />
                                            <ItemStyle BackColor="#FFFFFF" ForeColor="#666666" Height="35px" />
                                            <HeaderStyle Height="15px" BackColor="#F5F5F5" Font-Bold="True" ForeColor="#555555" />
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                            </table>

                            <!-- Partner Form -->
                            <div class="form_Atul" style="width: 100%;">
                                <asp:Panel runat="server" ID="pnlPartnerDetails" Visible="false" class="panelRegistration_Atul"
                                    Style="background: rgb(235,247,255);">
                                    <div style="text-align: center; color: rgb(251,85,58); margin-top:8px;">
                                        Partner Details
                                    </div>

                                    <div style="display:none;">
                                        <table border="0" width="95%" cellpadding="0px" cellspacing="0px" style="padding-top: 20px;">
                                        <tr>
                                            <td style="border-right: 0px; color: rgb(4,163,233); padding-left: 20px;">
                                                Selected Event Name:
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-right: 0px;">
                                                <asp:Label ID="txtEventName" runat="server" Width="90%" class="textBox_Atul" style="padding-left:8px;"></asp:Label>
                                                <asp:HiddenField ID="hfReferencePartnerDOB" runat="server" />
                                                <asp:HiddenField ID="hfAfterBeforePartner" runat="server" />
                                            </td>
                                        </tr>
                                        </table>                                    
                                    </div>
                                                                        
                                    <div class="h3Text_Atul" style="border-right: 0px; color: rgb(4,163,233); text-align:center; margin-top:10px; margin-bottom:4px;">
                                        Register for Your Partner Details
                                    </div>
                                    
                                    <div>
                                        <table border="0" width="100%" cellpadding="0px" cellspacing="0px" style="text-align: center;">
                                        <tr>
                                            <td style="width:50%;">
                                                <asp:Button ID="btnAlreadyRegisteredPlayerAsPartner" runat="server" Text="Already Registered?" Width="170px" class="button_Atul"
                                                    OnClick="btnAlreadyRegisteredPlayerAsPartner_Click"></asp:Button>
                                            </td>
                                            <td>
                                            <asp:Button ID="btnNewPartnerDetails" runat="server" Text="New Partner Details" Width="170px" class="button_Atul"
                                                    OnClick="btnNewPartnerDetails_Click"></asp:Button>
                                            </td>
                                            </tr>
                                            </table>                                            
                                    </div>
                                    <hr />
                                
                                <asp:Panel runat="server" ID="pnlExistingPlayerAsPartner" Visible="false" class="panelRegistration_Atul"
                                    Style="background: rgb(235,247,255);">
                                        <div class="h3BoldText_Atul" style="border-right: 0px; color: rgb(4,163,233); text-align:center; margin-top:20px;">
                                        Select Already Registered Player as Partner
                                    </div>                                    
                                    <div>
                                        <asp:DropDownList ID="ddlRegisteredPartner" runat="server" Width="87%" class="dropdown_Atul" 
                                         OnClientClick="showLoading();" 
                                          AutoPostBack="true"
                                         OnSelectedIndexChanged="ddlRegisteredPartner_SelectedIndexChanged" />                                                    
                                    </div>
                                    <div style="text-align: center; margin-top: 15px;">
                                        <asp:Button ID="btnExistingPlayerAsPartner" runat="server" Text="Submit" Width="160px" class="button_Atul"
                                                    OnClick="btnExistingPlayerAsPartner_Click"></asp:Button>
                                    </div>
   
                                    </asp:Panel>
                                    

                                    <asp:Panel runat="server" ID="pnlNewPartnerDetails" Visible="false" class="panelRegistration_Atul"
                                    Style="background: rgb(235,247,255); margin-top:10px;">    
                                    <table border="0" width="95%" cellpadding="0px" cellspacing="0px" style="padding-top: 20px;">
                                        
                                        <tr>
                                            <td style="border-right: 0px; color: rgb(4,163,233); padding-left: 20px;">
                                                Partner First Name:
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-right: 0px;">
                                                <asp:TextBox ID="txtPartnerFirstName" runat="server" Width="90%" MaxLength="20" Style="background: rgb(256,256,256);"
                                                    class="textBox_Atul"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-right: 0px; color: rgb(4,163,233); padding-left: 20px;" class="topPaddingBetweenObjects_Atul">
                                                Partner Last Name:
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-right: 0px;">
                                                <asp:TextBox ID="txtPartnerLastName" runat="server" Width="90%" MaxLength="20" Style="background: rgb(256,256,256);"
                                                    class="textBox_Atul" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-right: 0px; color: rgb(4,163,233); padding-left: 20px;" class="topPaddingBetweenObjects_Atul">
                                                Partner Gender:
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-right: 0px; padding-left: 20px;">
                                                <asp:DropDownList ID="ddlPartnerGender" runat="server" Width="90%" class="dropdown_Atul">
                                                    <asp:ListItem Text="Select Gender" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                                                    <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-right: 0px; color: rgb(4,163,233); padding-left: 20px;" class="topPaddingBetweenObjects_Atul">
                                                Partner Date of Birth
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-right: 0px; width: 100%;">
                                                <asp:DropDownList ID="ddlPartnerDate" runat="server" Width="25%" Style="background: rgb(256,256,256);"
                                                    class="dropdown_Atul">
                                                    <asp:ListItem Text="Select Date" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="01" Value="01"></asp:ListItem>
                                                    <asp:ListItem Text="02" Value="02"></asp:ListItem>
                                                    <asp:ListItem Text="03" Value="03"></asp:ListItem>
                                                    <asp:ListItem Text="04" Value="04"></asp:ListItem>
                                                    <asp:ListItem Text="05" Value="05"></asp:ListItem>
                                                    <asp:ListItem Text="06" Value="06"></asp:ListItem>
                                                    <asp:ListItem Text="07" Value="07"></asp:ListItem>
                                                    <asp:ListItem Text="08" Value="08"></asp:ListItem>
                                                    <asp:ListItem Text="09" Value="09"></asp:ListItem>
                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                    <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                    <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                    <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                    <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                    <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                    <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                    <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                    <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                    <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                    <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                    <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                    <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                    <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                    <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                    <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                    <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                    <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlPartnerMonth" runat="server" Width="30%" Style="background: rgb(256,256,256);"
                                                    class="dropdown_Atul">
                                                    <asp:ListItem Text="Select Month" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Jan" Value="01"></asp:ListItem>
                                                    <asp:ListItem Text="Feb" Value="02"></asp:ListItem>
                                                    <asp:ListItem Text="Mar" Value="03"></asp:ListItem>
                                                    <asp:ListItem Text="Apr" Value="04"></asp:ListItem>
                                                    <asp:ListItem Text="May" Value="05"></asp:ListItem>
                                                    <asp:ListItem Text="Jun" Value="06"></asp:ListItem>
                                                    <asp:ListItem Text="Jul" Value="07"></asp:ListItem>
                                                    <asp:ListItem Text="Aug" Value="08"></asp:ListItem>
                                                    <asp:ListItem Text="Sep" Value="09"></asp:ListItem>
                                                    <asp:ListItem Text="Oct" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="Nov" Value="11"></asp:ListItem>
                                                    <asp:ListItem Text="Dec" Value="12"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtPartnerDOBYear" runat="server" MaxLength="4" Placeholder="YYYY"
                                                    Width="25%" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);" Style="background: rgb(256,256,256);"
                                                    class="textBox_Atul" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-right: 0px; color: rgb(4,163,233); padding-left: 20px;" class="topPaddingBetweenObjects_Atul">
                                                Partner Contact:
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-right: 0px;">
                                                <asp:TextBox ID="txtPartnerContact" Text="" runat="server" Width="90%" MaxLength="10" TextMode="Number"
                                                    onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);" Style="background: rgb(256,256,256);"
                                                    class="textBox_Atul" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-right: 0px; color: rgb(4,163,233); padding-left: 20px;" class="topPaddingBetweenObjects_Atul">
                                                Partner Email ID:
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-right: 0px;">
                                                <asp:TextBox ID="txtPartnerEmailID" Text="" runat="server" Width="90%" MaxLength="50"
                                                    Style="background: rgb(256,256,256);" class="textBox_Atul" />
                                            </td>
                                        </tr>
                                        <tr style="display: none;">
                                            <td style="border-right: 0px; color: rgb(4,163,233); padding-left: 20px;" class="topPaddingBetweenObjects_Atul">
                                                Partner State:
                                            </td>
                                        </tr>
                                        <tr style="display: none;">
                                            <td style="border-right: 0px;">
                                                <asp:DropDownList ID="ddlPartnerStates" runat="server" Width="90%" Style="background: rgb(256,256,256);"
                                                    class="dropdown_Atul">
                                                    <asp:ListItem Text="Maharashtra" Value="1"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr style="display: none;">
                                            <td style="border-right: 0px; color: rgb(4,163,233); padding-left: 20px;" class="topPaddingBetweenObjects_Atul">
                                                Partner District:
                                            </td>
                                        </tr>
                                        <tr style="display: none;">
                                            <td style="border-right: 0px;">
                                                <asp:DropDownList ID="ddlPartnerDistricts" runat="server" Width="90%" class="dropdown_Atul">
                                                    <asp:ListItem Text="Pune" Value="1"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr style="display: none;">
                                            <td style="border-right: 0px; color: rgb(4,163,233); padding-left: 20px;" class="topPaddingBetweenObjects_Atul">
                                                Partner City:
                                            </td>
                                        </tr>
                                        <tr style="display: none;">
                                            <td style="border-right: 0px;">
                                                <asp:TextBox ID="txtPartnerCity" Text="Pune" runat="server" Width="90%" MaxLength="30"
                                                    ReadOnly="true" class="textBox_Atul" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-right: 0px; color: rgb(4,163,233); padding-left: 20px;" class="topPaddingBetweenObjects_Atul">
                                                Partner Address:
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-right: 0px;">
                                                <asp:TextBox ID="txtPartnerAddress" Text="" runat="server" Width="90%" MaxLength="140"
                                                    Style="background: rgb(256,256,256);" class="textBox_Atul" />
                                            </td>
                                        </tr>
                                        <tr style="display: none;">
                                            <td style="border-right: 0px; color: rgb(4,163,233); padding-left: 20px;" class="topPaddingBetweenObjects_Atul">
                                                Partner T-Shirt Size:
                                            </td>
                                        </tr>
                                        <tr style="display: none;">
                                            <td style="border-right: 0px;">
                                                <asp:DropDownList ID="ddlPartnerTShirtSize" runat="server" Width="90%" class="dropdown_Atul">
                                                    <asp:ListItem Text="Select T-Shirt Size" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Small (S)" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Medium (M)" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Large (L)" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Extra Large (XL)" Value="4"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Panel runat="server" ID="pnlPartnerErrorMsg" Visible="false" Width="100%" Height="20px"
                                        Style='text-align: center; margin-top: 20px;' class="panelRegistration_Atul">
                                        <asp:Label ID="lblPartnerErrorMsg" runat="server" Text="" Style="color: red;"></asp:Label>
                                    </asp:Panel>
                                    <br />
                                    <table border="0" width="100%" cellpadding="0px" cellspacing="0px" style="text-align: center;">
                                        <tr>
                                            <td style="text-align: right; border-right: 0px;">
                                                <asp:Button ID="btnPartnerSaveDetails" runat="server" Text="Add" Width="70%" class="button_Atul"
                                                    OnClick="btnPartnerSave_Click"></asp:Button>
                                            </td>
                                            <td style="border-right: 0px;">
                                                <asp:Button ID="btnPartnerClose" runat="server" Text="Close" Width="60%" OnClick="btnPartnerClose_Click"
                                                    class="button_Atul"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                    </asp:Panel>
                                </asp:Panel>
                            </div>
                            <hr />
                            <table border="0" width="98%" style="margin: 8px; text-align: center;">
                                <tr style="display: none;">
                                    <td style="border-right: 0px; text-align: center;">
                                        <asp:CheckBox ID="chkbDistrictReg" runat="server" Width="100%" Text=" District Registration Fees(30 Rs.)"
                                            OnCheckedChanged="chkbDistrictReg_CheckedChanged" AutoPostBack="true" CssClass="mycheckbox"
                                            style="text-align:justify;"></asp:CheckBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-right: 0px; text-align: center;" class="h2BoldText_Atul">
                                        Total Amount: Rs.
                                        <asp:Label ID="lblTotalAmount" runat="server" Text="0" class="h2BoldText_Atul"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table width="80%" style="display: none">
                                <tr>
                                    <td style="border-right: 0px; padding-top: 20px;">
                                        <asp:Button ID="btnViewSummary" Text="View Summary" runat="server" OnClick="btnViewSummary_Click"
                                            class="button_Atul" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                    <!-- Summary -->
                    <div class="topPaddingBetweenObjects_Atul">
                        <div style="width: 100%">
                            <asp:Button ID="btnToggleSummary" Text="Step 3: Summary and Pay" runat="server" OnClick="btnToggleSummary_Click"
                                Style="background: rgb(220,220,220); color: rgb(4,163,233); text-align: left;
                                width: 80%; height: 58px; border: none; font: bold 16pt Calibri;" />
                            <asp:Button ID="btnToggleSummaryPlus" Text="+" runat="server" OnClick="btnToggleSummary_Click"
                                Style="background: rgb(220,220,220); color: rgb(251,85,58); float: right; width: 20%;
                                height: 58px; border: none; font: bold 30pt Calibri;" />
                        </div>
                        <asp:Panel runat="server" ID="pnlSummary" Visible="false" class="panelRegistration_Atul">
                            <table>
                                <tr>
                                    <td style="border-right: 0px;" class="h4BoldText_Atul">
                                        Player Name:
                                    </td>
                                    <td style="border-right: 0px;">
                                        <asp:Label ID="lblPlayerName" runat="server" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-right: 0px;" class="h4BoldText_Atul">
                                        Gender:
                                    </td>
                                    <td style="border-right: 0px;">
                                        <asp:Label ID="lblPlayerGender" runat="server" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-right: 0px;" class="h4BoldText_Atul">
                                        Player Birthdate:
                                    </td>
                                    <td style="border-right: 0px;">
                                        <asp:Label ID="lblPlayerDOB" runat="server" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-right: 0px;" class="h4BoldText_Atul">
                                        Player Contact:
                                    </td>
                                    <td style="border-right: 0px;">
                                        <asp:Label ID="lblPlayerContact" runat="server" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-right: 0px;" class="h4BoldText_Atul">
                                        Player Email ID:
                                    </td>
                                    <td style="border-right: 0px;">
                                        <asp:Label ID="lblPlayerEmailID" runat="server" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-right: 0px;" class="h4BoldText_Atul">
                                        District Registration:
                                    </td>
                                    <td style="border-right: 0px;">
                                        <asp:Label ID="lblDistricRegistration" runat="server" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <div style='background: rgb(256,256,256); text-align: top; padding-top: 10px;' class="h4Text_Atul">
                                <asp:DataGrid ID="dgPlayerParticilation" Width="100%" runat="server" PageSize="1"
                                    AllowPaging="False" AutoGenerateColumns="False" GridLines="None">
                                    <Columns>
                                        <asp:BoundColumn HeaderText="Category" DataField="EventCode" DataFormatString="{0:MMM-d-yyy}"
                                            ItemStyle-Width="10%"></asp:BoundColumn>
                                        <asp:BoundColumn HeaderText="Fees" DataField="EventRateCard" DataFormatString="{0:MMM-d-yyy}"
                                            ItemStyle-Width="10%"></asp:BoundColumn>
                                        <asp:BoundColumn HeaderText="Partner Name" DataField="PartnerFullName" ItemStyle-Width="20%">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn HeaderText="Partner DOB" DataField="ParterDOB" ItemStyle-Width="20%">
                                        </asp:BoundColumn>
                                    </Columns>
                                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="Black" />
                                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" Mode="NumericPages" />
                                    <AlternatingItemStyle BackColor="#F5F5F5" />
                                    <ItemStyle BackColor="#FFFFFF" ForeColor="#666666" Height="35px" />
                                    <HeaderStyle Height="15px" BackColor="#F5F5F5" Font-Bold="True" ForeColor="#555555" />
                                </asp:DataGrid>
                            </div>
                            <br />
                            <div style="padding-top: 15px; text-align: center;">
                                <asp:Label ID="lblParticipationAmount" runat="server" Text="" class="h3BoldText_Atul"></asp:Label>
                                <div>
                                    <br />
                                    <br />
                                    <div>
                                        <asp:Button ID="btnRegisterPay" Text="Register and Pay" runat="server" OnClick="btnRegisterPay_Click"
                                            class="button_Atul" Width="250px" />
                                    </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <div>
        <hr />
    </div>
    <div style='width: 100%; text-align: center; padding-top: 16px; padding-bottom: 20px;
        font: 14pt calibri; color: gray; background: rgb(235,235,235);'>
        Advertisement
    </div>
</asp:Content>
