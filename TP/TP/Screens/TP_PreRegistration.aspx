<%@ Page Title="" Language="C#" MasterPageFile="~/Screens/TP.Master" AutoEventWireup="true"
    CodeBehind="TP_PreRegistration.aspx.cs" Inherits="TournamentPlanner.TP_PreRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Header -->
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
    <div style='padding-top: 2px; min-height: 400px; width: 100%; background: rgb(256,256,256);'
        class="h4BoldText_Atul">
        <div class="pageTopic">
            <asp:Label ID="lblPlayerRegistration" runat="server" Text="Player Registration"></asp:Label>
        </div>

        <asp:Panel runat="server" ID="pnlAlreadyRegistered" Visible="true" class="panelRegistration_Atul" style="background-color:White;">
            <div class="h1BoldText_Atul" style="text-align: center; margin-top: 40px; width: 100%;">
                ALREADY REGISTERED?
            </div>
            <div class="h2Text_Atul" style="text-align: center; margin-top: 40px; width: 100%;">
                Already Registered for the Tournament? and would like to Register for more Events/Categories?
            </div>
            <div style="text-align: center;">
                <table border="0" cellpadding="20px" cellspacing="20px" style="text-align: center;
                    margin-top: 40px; width: 100%;">
                    <tr>
                        <td style="text-align: right;">
                            <asp:Button ID="btnYES" Text="YES" runat="server" class="button_Atul" Width="120px"
                                OnClick="btnYES_Click" />
                        </td>
                        <td style="text-align: left;">
                            <asp:Button ID="btnNO" Text="NO" runat="server" class="button_Atul" Width="120px"
                                OnClick="btnNO_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>

        <!-- If already registered, then show the text box to submit mobile no, accordingly show all the registration details against that mobile no -->
        <asp:Panel runat="server" ID="pnlValidateContact" Visible="false" class="panelRegistration_Atul" style="background-color:White;">
            <div class="h3BoldText_Atul" style="margin-top:50px; text-align:center; padding-left:20px;">
                Enter Your Registered Mobile No:
            </div>
            <div class="h3BoldText_Atul" style="text-align:center;">                                                     
                <asp:TextBox ID="txtContact" Text="" runat="server" Width="70%" MaxLength="10" TextMode="Number" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);"
                    class="textBox_Atul" Style="text-align: center;" />
            </div>
            <div style="text-align: center; margin-top: 40px;">
                <asp:Button ID="btnSubmit" Text="SUBMIT" runat="server" class="button_Atul" Width="160px" />
            </div>
        </asp:Panel>
    </div>
    <div>
        <hr />
    </div>
    <div style='width: 100%; text-align: center; padding-top: 16px; padding-bottom: 20px;
        font: 14pt calibri; color: gray; background: rgb(235,235,235);'>
        Advertisement
    </div>
</asp:Content>
