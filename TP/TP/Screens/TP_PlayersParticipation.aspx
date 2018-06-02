<%@ Page Title="" Language="C#" MasterPageFile="~/Screens/TP.Master" AutoEventWireup="true" CodeBehind="TP_PlayersParticipation.aspx.cs" Inherits="TournamentPlanner.TP_PlayersParticipation" %>
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
	<div style=' padding-top:2px; min-height:400px; width:100%; background:rgb(256,256,256);'>		
		<!-- Player List -->
		<div>
			<div class="pageTopic">
				PLAYER PARTICIPATION
			</div>
			
			<div style=' background:rgb(256,256,256);text-align:top;padding-top:10px; ' class="h4Text_Atul">
			<asp:DataGrid ID="dgPlayerParticilationList" width="100%" runat="server" PageSize="1" AllowPaging="False"
				AutoGenerateColumns="False" GridLines="None" >  
				<Columns>					
					<asp:BoundColumn HeaderText="Player Name" DataField="PlayerFullName" DataFormatString="{0:MMM-d-yyy}" ItemStyle-Width="10%"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Event" DataField="EventCode" DataFormatString="{0:MMM-d-yyy}" ItemStyle-Width="10%"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Parter Name" DataField="PartnerPlayerCode" ItemStyle-Width="20%"></asp:BoundColumn>
				</Columns>  
				
				<FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="Black" />  
				
				<PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" Mode="NumericPages" />  
				
				<AlternatingItemStyle BackColor="#F5F5F5" />  
				
				<ItemStyle BackColor="#FFFFFF" ForeColor="#666666" Height="35px" />  
				
				<HeaderStyle height="15px" BackColor="#F5F5F5" Font-Bold="True" ForeColor="#555555" />  
			
			</asp:DataGrid>
		</div>
		</div>
	</div>

<div>
	<hr/>
</div>
<div style='width:100%; text-align:center;padding-top:16px; padding-bottom:20px; font: 14pt calibri; color:gray;background:rgb(235,235,235);'>
Advertisement
</div>

</asp:Content>
