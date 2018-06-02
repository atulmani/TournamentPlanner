<%@ Page Title="" Language="C#" MasterPageFile="~/Screens/TP.Master" AutoEventWireup="true" CodeBehind="TP_TournamentDetails.aspx.cs" Inherits="TournamentPlanner.TP_TournamentDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div>
	<table width="100%">
		<tr>
			<td>		
				<div class="h3BoldText_Atul">
					<asp:Label id="lblTournamentName" Text="" runat="server"></asp:Label>
				</div>
				<div class="h4Text_Atul">
					<asp:Label id="lblTournamentOrganisation" Text="" runat="server"></asp:Label>
				</div>
				<div class="h4Text_Atul">
					<asp:Label id="lbl22" Text="Venue: " runat="server" class="h4BoldText_Atul"></asp:Label>				
					<asp:Label id="lblTournamentVenue" Text="" runat="server"></asp:Label>
				</div>
				<!--<div class="h4Text_Atul">					
					<asp:Label id="lblLocationAddress" Text="" runat="server"></asp:Label>
				</div>
				<div class="h4Text_Atul">
					<asp:Label id="lbl24" Text="Contact: " runat="server" class="h4BoldText_Atul"></asp:Label>
					<asp:Label id="lblTournamentContacts" Text="" runat="server"></asp:Label>				
				</div>
				<div class="h4Text_Atul">
					<asp:Label id="lbl25" Text="Entry Open: " runat="server" class="h4BoldText_Atul"></asp:Label>								
					<asp:Label id="lblEntryOpenDate" Text=""   runat="server"></asp:Label>
				</div>
				<div class="h4Text_Atul">				
					<asp:Label id="lbl26" Text="Entry Closes: " runat="server" class="h4BoldText_Atul"></asp:Label>
					<asp:Label id="lblEntryClosesDate" Text="" runat="server"></asp:Label>
				</div>
				<div class="h4Text_Atul">
					<asp:Label id="lbl27" Text="Withdrawal: " runat="server" class="h4BoldText_Atul"></asp:Label>
					<asp:Label id="lblEntryWithdrawalDate" Text="" runat="server"></asp:Label>					
				</div>-->
				<div class="h4Text_Atul">
					<asp:Label id="lbl28" Text="Duration: " runat="server" class="h4BoldText_Atul"></asp:Label>
					<asp:Label id="lblTournamentDuration" Text="" runat="server"></asp:Label>
				</div>
			</td>
		</tr>
	</table>
	</div>
	
	<!-- TP Menu -->
	<div style="text-align:center;">
		<table border="0" cellspacing="0" style='width:100%; height:25px;'>
			<tr>
				<td style='text-align:center;'>				
					<asp:LinkButton id="lbtnHome" Text="HOME" runat="server" OnClick="lbtnTPMenu_Click" width="100%" class="tabMenuInactive_Atul" style="padding-top:10px;" />
				</td>
				<td>
					<asp:LinkButton id="lbtnEvents" Text="EVENTS" runat="server" OnClick="lbtnTPMenu_Click" width="100%" class="tabMenuInactive_Atul" style="padding-top:10px;" />
				</td>				
				<td>
					<asp:LinkButton id="lbtnPlayers" Text="PLAYERS" runat="server" OnClick="lbtnTPMenu_Click" width="100%" class="tabMenuInactive_Atul" style="padding-top:10px;" />
				</td>
				<td>
					<asp:LinkButton id="lbtnDraws" Text="DRAWS" runat="server" OnClick="lbtnTPMenu_Click" width="100%" class="tabMenuInactive_Atul" style="padding-top:10px;" />
				</td>
				<td>					
					<asp:LinkButton id="lbtnMatches" Text="MATCHES" runat="server" OnClick="lbtnTPMenu_Click" width="100%" class="tabMenuInactive_Atul" style="padding-top:10px;" />
				</td>				
			</tr>
		</table>
	</div>

	<!-- Main Body -->
    <div class="form_Atul">
	<!--<div style='padding-top:2px; min-height:400px; width:100%; background:rgb(256,256,256);'>-->		
        <div class="pageTopic">
                    TOURNAMENT DETAILS
         </div>
		
        <asp:Panel runat="server" id="pnlRegistrationOPENUp" visible="false" class="panelRegistration_Atul">
        <div style="text-align:center; margin-top:16px;" class="h1BoldText_Atul">
            <asp:LinkButton ID="lbOnlineRegistrationFormBottomUp" runat="server" OnClick="lbtnOnlineRegistrationForm_Click">Online Registration Form</asp:LinkButton>
        </div>
        <!--<div style="text-align:center; margin-top:16px;">
            <asp:Button ID="LinkButton1" runat="server" Text="REGISTER NOW" class="button_Atul" Width="260px"/>
        </div>-->
        <div style="text-align:center; margin-top:0px;" class="h4Text_Atul">
            <asp:Label id="Label4" runat="server" Text="Registration Closes on "></asp:Label>
            <asp:Label id="lblEntryCloseDate" runat="server" Text="" Font-Bold=true></asp:Label>
        </div>
        
        <div style="text-align:center; margin-top:10px; display:none;" class="h4Text_Atul">
            <asp:LinkButton ID="LinkButton123" runat="server" OnClick="lbTermsandConditons_Click" class="h4Text_Atul">TERMS & CONDITIONS</asp:LinkButton>
        </div>
        </asp:Panel>

        <asp:Panel runat="server" id="pnlOtherThanRegistrationUp" visible="false" class="panelRegistration_Atul">
            <div style="text-align:center; margin-top:16px;" class="h1BoldText_Atul">
                <asp:Label id="lblTournamentStatusUp" runat="server" Text=""></asp:Label>
            </div>
        </asp:Panel>
        <hr />
            <div class="h4Text_Atul" style="text-align:center;">                
                <span class="h4BoldText_Atul">NOTE</span>
                <ul style="text-align:left;">
                    <li>Matches upto QF will be played in 15 points best of 3 with no extension. QF onwards, it will be 21 points, best of 3</li>
                    <li>Players to report 30 mins before the match timings</li>
                    <li>For any query, contact Mayank Gole: 880 627 1903</li>
                </ul>
            </div>
        <hr />
        <div style="text-align:left; margin-left:16px; margin-top:16px;" class="h2BoldText_Atul">
            <asp:Label id="lblTPName" runat="server" Text=""></asp:Label>
        </div>
        <div style="text-align:left; margin-left:16px; margin-top:2px;" class="h3Text_Atul">
            <asp:Label id="Label8" runat="server" Text="Organisation: " Font-Bold=true></asp:Label>
            <asp:Label id="lblOrganisation" runat="server" Text=""></asp:Label>
        </div>
        <div style="text-align:left; margin-left:16px; margin-top:2px;" class="h3Text_Atul">
            <asp:Label id="Label3" runat="server" Text="Venue: " Font-Bold=true></asp:Label>
            <asp:Label id="lblVenue" runat="server" Text=""></asp:Label>
        </div>
        <div style="text-align:left; margin-left:16px; margin-top:2px;" class="h3Text_Atul">
            <asp:Label id="Label6" runat="server" Text="Duration: " Font-Bold=true class="h4BoldText_Atul"></asp:Label>
            <asp:Label id="lblDuration" runat="server" Text="" Font-Bold=true class="h4BoldText_Atul"></asp:Label>
        </div>
        
        <div style="text-align:left; margin-left:16px; margin-top:16px;" class="h2BoldText_Atul">
            <asp:Label id="Label10" runat="server" Text="Tournament Contacts"></asp:Label>
        </div>
        <div style="text-align:left; margin-left:16px; margin-top:2px;" class="h3Text_Atul">
            <asp:Label id="Label11" runat="server" Text="Contacts:" Font-Bold=true></asp:Label>
            <asp:Label id="lblContacts" runat="server" Text=""></asp:Label>
        </div>
        <div style="text-align:left; margin-left:16px; margin-top:2px;" class="h3Text_Atul">
            <asp:Label id="Label1" runat="server" Text="Email:" Font-Bold=true></asp:Label>
            <asp:Label id="Label2" runat="server" Text=""></asp:Label>
        </div>
        <div style="text-align:left; margin-left:16px; margin-top:16px;" class="h2BoldText_Atul">
            <asp:Label id="Label5" runat="server" Text="Tournament Events"></asp:Label>
        </div>
        <div style="text-align:left; margin-left:16px; margin-top:2px;" class="h3Text_Atul">
            <asp:Label id="Label7" runat="server" Text="Events:" Font-Bold=true></asp:Label>
            <asp:LinkButton ID="lbEvents" runat="server" OnClick="lbEvents_Click" class="h4Text_Atul">Click to view tournament EVENTS</asp:LinkButton>
        </div>
        <div style="text-align:left; margin-left:16px; margin-top:16px;" class="h2BoldText_Atul">
            <asp:Label id="Label12" runat="server" Text="Tournament Players"></asp:Label>
        </div>
        <div style="text-align:left; margin-left:16px; margin-top:2px;" class="h3Text_Atul">
            <asp:Label id="Label18" runat="server" Text="Players: " Font-Bold=true></asp:Label>
            <asp:LinkButton ID="lbPlayers" runat="server" OnClick="lbPlayers_Click" class="h4Text_Atul">Click to view tournament REGISTERED PLAYERS</asp:LinkButton>
        </div>
        
        <!--<div style="text-align:left; margin-left:16px; margin-top:16px;" class="h2BoldText_Atul">
            <asp:Label id="Label29" runat="server" Text="Seeded Entries"></asp:Label>
        </div>
        
        <div style="text-align:left; margin-left:16px; margin-top:2px;" class="h3Text_Atul">
            <asp:Label id="Label30" runat="server" Text="Seeded Entries: " Font-Bold=true></asp:Label>
            <asp:Label id="Label31" runat="server" Text=""></asp:Label>
        </div>-->
        <div style="text-align:left; margin-left:16px; margin-top:16px;" class="h2BoldText_Atul">
            <asp:Label id="Label23" runat="server" Text="Tournament Draws"></asp:Label>
        </div>
        <div style="text-align:left; margin-left:16px; margin-top:2px;" class="h3Text_Atul">
            <asp:Label id="Label24" runat="server" Text="Draws: " Font-Bold=true></asp:Label>
            <asp:LinkButton ID="lbDraws" runat="server" OnClick="lbDraws_Click" class="h4Text_Atul">Click to view MATCH DRAWS</asp:LinkButton>
        </div>
        <div style="text-align:left; margin-left:16px; margin-top:16px;" class="h2BoldText_Atul">
            <asp:Label id="Label20" runat="server" Text="Tournament Matches"></asp:Label>
        </div>
        <div style="text-align:left; margin-left:16px; margin-top:2px;" class="h3Text_Atul">
            <asp:Label id="Label21" runat="server" Text="Matches: " Font-Bold=true></asp:Label>
            <asp:LinkButton ID="lbMatches" runat="server" OnClick="lbMatches_Click" class="h4Text_Atul">Click to view tournament MATCH SCHEDULE</asp:LinkButton>
        </div>
        <div style="text-align:left; margin-left:16px; margin-top:16px;" class="h2BoldText_Atul">
            <asp:Label id="Label26" runat="server" Text="Tournament Results"></asp:Label>
        </div>

        <div style="text-align:left; margin-left:16px; margin-top:2px;" class="h3Text_Atul">
            <asp:Label id="Label27" runat="server" Text="Results: " Font-Bold=true></asp:Label>
            <asp:Label id="Label28" runat="server" Text="Result not yet published" class="h4Text_Atul"></asp:Label>
        </div>

        <hr />
        <asp:Panel runat="server" id="pnlRegistrationOPENBottom" visible="false" class="panelRegistration_Atul">
        <div style="text-align:center; margin-top:16px;" class="h1BoldText_Atul">
            <asp:LinkButton ID="lbOnlineRegistrationFormBottom" runat="server" OnClick="lbtnOnlineRegistrationForm_Click">Online Registration Form</asp:LinkButton>
        </div>
        <!--<div style="text-align:center; margin-top:16px;">
            <asp:Button ID="LinkButton3" runat="server" Text="REGISTER NOW" class="button_Atul" Width="260px"/>
        </div>-->
        <div style="text-align:center; margin-top:0px;" class="h4Text_Atul">
            <asp:Label id="Label13" runat="server" Text="Registration Closes on "></asp:Label>
            <asp:Label id="Label14" runat="server" Text="" Font-Bold=true></asp:Label>
        </div>
        <div style="text-align:center; margin-top:10px; display:none;" class="h4Text_Atul">
            <asp:LinkButton ID="lbTermsandConditons" runat="server" OnClick="lbTermsandConditons_Click" class="h4Text_Atul">TERMS & CONDITIONS</asp:LinkButton>
        </div>
        </asp:Panel>

        
        <asp:Panel runat="server" id="pnlOtherThanRegistrationBottom" visible="false" class="panelRegistration_Atul">
            <div style="text-align:center; margin-top:16px;" class="h1BoldText_Atul">
                <asp:Label id="lblTournamentStatusBottom" runat="server" Text=""></asp:Label>
            </div>
        </asp:Panel>
	</div>
   
<div>
	<hr/>
</div>

<div style='width:100%; text-align:center;padding-top:16px; padding-bottom:20px; font: 14pt calibri; color:gray;background:rgb(235,235,235);'>
Advertisement
</div>
</asp:Content>
