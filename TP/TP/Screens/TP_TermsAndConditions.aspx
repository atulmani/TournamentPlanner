<%@ Page Title="" Language="C#" MasterPageFile="~/Screens/TP.Master" AutoEventWireup="true" CodeBehind="TP_PlayersParticipation.aspx.cs" Inherits="TournamentPlanner.TP_TermsAndConditions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   	<div>
		<table border="0" cellspacing="10px" cellpadding="50px" align="top" text-align="top" style='width:100%; height:100px; background:rgb(235,235,235);'>
			<tr>
				<td style='min-width:35%; border-right:0px;'>
					<table border="0" cellspacing="0" style='width:100%; height:100px; background:rgb(235,235,235);'>
						<tr style='color:rgb(100,100,100); font: bold 14pt Calibri;'>
							<td style='width:40%; border-right:0px;'>							
								<asp:Label id="lblTournamentName" Text="" runat="server"></asp:Label>							
							</td>		
						</tr>						
						<tr style='color:rgb(50,50,50); font: 16pt Calibri;'>
							<td style='border-right:0px;border-right:0px;'>								
								<asp:Label id="lblTournamentOrganisation" Text="" runat="server"></asp:Label>								
							</td>						
						</tr>						
						<tr style='color:rgb(50,50,50); font: 16pt Calibri;'>
							<td style='border-right:0px;border-right:0px;'>
								<asp:Label id="lbl22" Text="Venue: " runat="server" style='font: bold 16pt Calibri;'></asp:Label>																
								<asp:Label id="lblTournamentVenue" Text="" runat="server"></asp:Label>
								<asp:Label id="lbl23" Text="| Address: " runat="server" style='font: bold 16pt Calibri;'></asp:Label>
								<asp:Label id="lblLocationAddress" Text="" runat="server"></asp:Label>
								<asp:Label id="lbl24" Text="| Contacts: " runat="server" style='font: bold 16pt Calibri;'></asp:Label>
								<asp:Label id="lblTournamentContacts" Text="" runat="server"></asp:Label>							
							</td>						
						</tr>
						<tr style='color:rgb(50,50,50); font: 16pt Calibri;'>
								<td style='border-right:0px;'>		
								<asp:Label id="lbl25" Text="Entry Open: " runat="server" style='font: bold 16pt Calibri;'></asp:Label>								
								<asp:Label id="lblEntryOpenDate" Text=""   runat="server"></asp:Label>							
								<asp:Label id="lbl26" Text="Entry Closes: " runat="server" style='font: bold 16pt Calibri;'></asp:Label>
								<asp:Label id="lblEntryClosesDate" Text="" runat="server"></asp:Label>
								<asp:Label id="lbl27" Text="Withdrawal Deadline: " runat="server" style='font: bold 16pt Calibri;'></asp:Label>
								<asp:Label id="lblEntryWithdrawalDate" Text="" runat="server"></asp:Label>
							</td>						
						</tr>
						
						<tr style='color:rgb(50,50,50); font: bold 24pt Calibri; padding-top:50px;'>
							<td style='border-right:0px;'>
								<asp:LinkButton id="lbtnRegistrationForm" Text="Registration Form" PostBackURL="./TP_RegistrationForm.aspx" runat="server" style="text-align:center;valign:middle; border:none;font: bold 24pt Calibri" />								
							</td>						
						</tr>
					</table>
				</td>		
				
				<td class="tblcol3hide" style='font: bold 14pt Calibri;border-right:0px;min-width:50%;text-align:right;'>
					<table border="0" cellspacing="0" style='width:100%; height:100px; background:rgb(235,235,235);'>
						<tr style='color:rgb(100,100,100); font: bold 14pt Calibri;'>
							<td style='width:40%; border-right:0px;'>Tournament Duration</td>
						</tr>
						<tr style='color:rgb(50,50,50); font:bold 9pt Calibri;'>
							<td style='border-right:0px;'>
								<asp:Label id="lblTournamentDuration" Text="" runat="server"></asp:Label>
							</td>
						</tr>
																		
					</table>
				</td>
			</tr>
		</table>
	</div>
	
	<!-- Sub Menu -->
	<div>
		<table border="0" cellspacing="0" style='font:bold 11pt Calibri; width:100%; height:25px; background:rgb(146,215,10);'>
			<tr>
				<td class="submenufont" style='text-align:center;'><a style='text-decoration:none;color:rgb(10,10,10);' href="./TP_BD_Home.aspx">Home</a></td>
				<td class="submenufont" style='text-align:center;'><a style='text-decoration:none;color:rgb(10,10,10);' href="./TP_Events.aspx">Events</a></td>				
				<td class="submenufont" style='text-align:center;'><a style='text-decoration:none;color:rgb(10,10,10);' href="./TP_Players.aspx">Players</a></td>
				<td class="submenufont" style='text-align:center;'><a style='text-decoration:none;color:rgb(10,10,10);' href="./TP_Draws.aspx">Draws</a></td>
				<td class="submenufont" style='text-align:center;'><a style='text-decoration:none;color:rgb(10,10,10);' href="./TP_Matches.aspx">Matches</a></td>				
				<td class="submenufont" style='text-align:center;'><a style='text-decoration:none;color:rgb(10,10,10);' href="./TP_Winners.aspx">Winners</a></td>
				<td class="submenufont" style='text-align:center; border-right:0px;'><a style='text-decoration:none;color:rgb(10,10,10);' href="./TP_Gallery.aspx">Gallery</a></td>
			</tr>
		</table>

	</div>

	<!-- Main Body -->
	<div style=' padding-top:2px; min-height:400px; width:100%; background:rgb(256,256,256);'>
		
		<div style="font:16px Calibri;">
					
		</div>
		
		
		
	</div>

<div>
	<hr/>
</div>
<div style='width:100%; text-align:center;padding-top:16px; padding-bottom:20px; font: 14pt calibri; color:gray;background:rgb(235,235,235);'>
	----------------------------------------------      Advertisement      --------------------------------------------
</div>

</asp:Content>
