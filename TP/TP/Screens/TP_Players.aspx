<%@ Page Title="" Language="C#" MasterPageFile="~/Screens/TP.Master" AutoEventWireup="true" CodeBehind="TP_Players.aspx.cs" Inherits="TournamentPlanner.TP_Players" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

	<!-- Header -->
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
					<asp:LinkButton id="lbtnPlayers" Text="PLAYERS" runat="server" OnClick="lbtnTPMenu_Click" width="100%" class="tabMenuActive_Atul" style="padding-top:10px;" />
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
		    <div class="pageTopic">
                PLAYERS			    
		    </div>

            <div class="h3Text_Atul" style="padding-left:10px;padding-right:10px; padding-top:10px;">
                
			    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>			
		    </div>
		    <hr />
            <div style="text-align:center" class="h2BoldText_Atul">
                <asp:Label id="Label1" runat="server" Text="ALL Player List" ></asp:Label>
            </div>
            <div class="h4BoldText_Atul" style="text-align:center;">
                <asp:Label id="lblPlayerCount" runat="server" Text=" Count: "></asp:Label>
            </div>
            <hr />
			<div class="h5Text_Atul" style='width:100%;'>
                <asp:PlaceHolder ID="phPlayers" runat="server"></asp:PlaceHolder>	
			</div>
            <div class="h4Text_Atul" style="text-align:center; padding-top:10px;">
                <asp:Label id="lblMsg" runat="server" Text=""></asp:Label>
            </div>	   
    </div>

    <div>
        <hr />
    </div>
    <div style='width: 100%; text-align: center; padding-top: 16px; padding-bottom: 20px;
        font: 14pt calibri; color: gray; background: rgb(235,235,235);'>
        Advertisement
    </div>

</asp:Content>


