<%@ Page Title="" Language="C#" MasterPageFile="~/Screens/TP.Master" AutoEventWireup="true" CodeBehind="TP_RunMatch.aspx.cs" Inherits="TournamentPlanner.TP_RunMatch" %>
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
						<tr style='color:rgb(50,50,50); font: 9pt Calibri;'>
							<td style='border-right:0px;border-right:0px;'>								
								<asp:Label id="lblTournamentOrganisation" Text="" runat="server"></asp:Label>								
							</td>						
						</tr>						
						<tr style='color:rgb(50,50,50); font: 9pt Calibri;'>
							<td style='border-right:0px;border-right:0px;'>							 
								<asp:Label id="lblTournamentVenue" Text="" runat="server"></asp:Label>
							</td>						
						</tr>
						<tr style='color:rgb(50,50,50); font: 9pt Calibri;'>
							<td style='border-right:0px;border-right:0px;'>							 
								<asp:Label id="lblLocationAddress" Text="" runat="server"></asp:Label>
							</td>						
						</tr>
						<tr style='color:rgb(50,50,50); font: 9pt Calibri;'>
							<td style='border-right:0px;border-right:0px;'>							
								<asp:Label id="lblTournamentContacts" Text="" runat="server"></asp:Label>							
							</td>
						</tr>
					</table>
				</td>		
				<td style='min-width:5%;border-right:0px;'>
					<table border="0" cellspacing="0" style='width:100%; height:100px; background:rgb(235,235,235);'>
						<tr style='color:rgb(100,100,100); font: bold 14pt Calibri;'>
							<td style='width:40%; border-right:0px;'>Online Entry</td>
						</tr>
						<tr style='color:rgb(50,50,50); font: 9pt Calibri;'>
							<th>Entry Open:</th>
							<td style='border-right:0px;'>							
								<asp:Label id="lblEntryOpenDate" Text="" runat="server"></asp:Label>							
							</td>
						</tr>
						<tr style='color:rgb(50,50,50); font: 9pt Calibri;'>
							<th>Entry Closes:</th>
							<td style='border-right:0px;'>
								<asp:Label id="lblEntryClosesDate" Text="" runat="server"></asp:Label>
							</td>						
						</tr>
						<tr style='color:rgb(50,50,50); font: 9pt Calibri;'>
							<th>Withdrawal Deadline:</th>
							<td style='border-right:0px;border-right:0px;'>
								<asp:Label id="lblEntryWithdrawalDate" Text="" runat="server"></asp:Label>
							</td>
						</tr>
					</table>
				</td>
				<td style='font: bold 14pt Calibri;border-right:0px;min-width:50%;text-align:right;'>
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
		<!--<table border="0" cellspacing="0" style='font: 11pt Calibri; width:100%; height:25px; background:rgb(245,245,245);'>-->
		<table border="0" cellspacing="0" style='font:bold 11pt Calibri; width:100%; height:25px; background:rgb(146,215,10);'>
			<tr>
				<td style='text-align:center;'><a style='text-decoration:none;color:rgb(10,10,10);' href="./TP_BD_Home.aspx">Home</a></td>
				<td style='text-align:center;'><a style='text-decoration:none;color:rgb(10,10,10);' href="./TP_Events.aspx">Events</a></td>
				<!--<td style='text-align:center;'><a style='text-decoration:none;' href="./TP-SeededEntry.html">Seaded Entries</a></td>		-->
				<td style='text-align:center;'><a style='text-decoration:none;color:rgb(10,10,10);' href="./TP_Players.aspx">Players</a></td>
				<td style='text-align:center;'><a style='text-decoration:none;color:rgb(10,10,10);' href="./TP_Draws.aspx">Draws</a></td>
				<td style='text-align:center;'><a style='text-decoration:none;color:rgb(10,10,10);' href="./TP_Matches.aspx">Matches</a></td>
				
				<td style='text-align:center;'><a style='text-decoration:none;color:rgb(10,10,10);' href="./TP_Winners.aspx">Winners</a></td>
				<td style='text-align:center; border-right:0px;'><a style='text-decoration:none;color:rgb(10,10,10);' href="./TP_Gallery.aspx">Gallery</a></td>
			</tr>
		</table>

	</div>

	<!-- Main Body -->
	<div style=' padding-top:2px; min-height:400px; font: 10pt Calibri; width:100%; background:rgb(256,256,256);'>
		<table align="top" border="0" cellspacing="0" width="100%" style='font: 10pt Calibri;'>
			<tr style='height:10px;'>
				<td colspan="2" style='border-right:0px; font: bold 12pt Arial;border-right:0px;background:rgb(231,231,215);'>					
					<asp:Label id="lblMatchScoreHeading" runat="server" Text="Match - Score"  ></asp:Label>					
				</td>				
				<td style='text-align:right; border-right:0px; font: 10pt Arial;background:rgb(231,231,215);color:rgb(10,10,10);'>
					
					<asp:Label id="lblMatchStatus" runat="server" Text="  Match Status: Not Started"  ></asp:Label>					
				</td>
			</tr>			
		</table>
		
		<asp:Panel runat="server" id="pnlKickOffMatch">
			<table width="100%" border="0">
				<tr>
					<td valign="top" style="border-right:0px;">
						<table border="0" align="top" width="100%">									
							<tr>
								<th style="border-right:0px;">Umpire Code: *</th>
								<td style="border-right:0px;">
									<asp:TextBox id="txtUmpireCode"  runat="server" width="100%" height="24px" MaxLength="200" />
								</td> 
							</tr>
							<tr>
								<th style="border-right:0px;">Umpire Name: *</th>
								<td style="border-right:0px;">
									<asp:TextBox id="txtUmpireName" Text="" runat="server" width="100%" height="24px" MaxLength="200" />
								</td> 
							</tr>
							<tr>
								<th style="border-right:0px;">Court Details: *</th>
								<td style="border-right:0px;">
									<asp:TextBox id="txtMatchLocation" Text="" runat="server" width="100%" height="24px" MaxLength="200" />
								</td> 
							</tr>									
						</table>			
					</td>
					
				</tr>
			</table>
			<table width="100%">
				<tr>
					<td style="border-right:0px; padding-top:20px;text-align:center;">
						<asp:Button id="btnStartMatch" Text="KickOff Match" runat="server" OnClick="btnKickOffMatch_Click" width="150px" height="28px" />
					</td>
				</tr>
			</table>
		</asp:Panel>
		<asp:Panel runat="server" id="pnlMatchRound" visible="False" >
			<table border="0" width="80%" >
				<tr>
					<td style='font: bold 11pt Calibri;border-right:0px;'>
						<asp:Label id="lblMatchID" runat="server" Text="Match ID: 1234 "  ></asp:Label>
					</td>
					<td style='font: 11pt Calibri;border-right:0px;'>
						<asp:Label id="lblNoOfSetsPoints" runat="server" Text="  3 Sets of 21 Points"  ></asp:Label>
					</td>
				</tr>
				<tr>
					<td style='font: bold 11pt Calibri;border-right:0px;'>Umpire Name:</td>
					<td style='font: 11pt Calibri;border-right:0px;'>
						<asp:Label id="lblUmpireName" runat="server" Text=""  ></asp:Label>
					</td>
				
					<td style='font: bold 11pt Calibri;border-right:0px;'>Match Location:</td>
					<td style='font: 11pt Calibri;border-right:0px;'>
						<asp:Label id="lblMatchLocation" runat="server" Text=""  ></asp:Label>
					</td>
					
					<td style='font: bold 11pt Calibri;border-right:0px;'>Match Duration:</td>
					<td style='font: 11pt Calibri;border-right:0px;'>
						<asp:Label id="lblMatchDuration" runat="server" Text="-"  ></asp:Label>
					</td>					
				</tr>
			</table>			
			<table width="100%" border="0">
				<tr style="background:#F1F1F1;font:bold 12pt Calibri;">
					<td>Team / Player Name</td>
					<td style="text-align:center;">Round1</td>					
					<td style="text-align:center;">Round2</td>
					<td style="text-align:center;">Round3</td>
					<td width="50px" style='background:#FFFFFF; font: 12pt Calibri;border-right:0px;'></td>
					<td style='background:#FFFFFF; border-right:0px;'></td>
					<td width="50px" style='background:#FFFFFF; border-right:0px;'></td>
					
				</tr>
				<tr>
					<td></td>
					<td style="text-align:center;">
						<asp:Button id="btnFirstSetStart" Text="Start" runat="server" OnClick="btnFirstSetStartEnd_Click" style="font: bold 20pt Calibri;border:0;background:rgb(235,235,235);" />
					</td>
					<td style="text-align:center;">
						<asp:Button id="btnSecondSetStart" Text="Start" visible=false runat="server" OnClick="btnSecondSetStartEnd_Click" style="font: bold 20pt Calibri;border:0;background:rgb(235,235,235);" />
					</td>
					<td style="text-align:center;">
						<asp:Button id="btnThirdSetStart" Text="Start" visible=false runat="server" OnClick="btnThirdSetStartEnd_Click" style="font: bold 20pt Calibri;border:0;background:rgb(235,235,235);" />
					</td>
				</tr>
				<tr>
					<td valign="top" style="text-align:right;">
						<asp:Label id="Label123" runat="server" Text="Status: " style="font:bold 10pt Calibri;"  ></asp:Label>
					</td>
					<td style="text-align:center;">						
						<asp:Label id="lblRound1Status" runat="server" Text="Not Yet Started"  ></asp:Label>
						<hr/>
					</td>
					<td style="text-align:center;">						
						<asp:Label id="lblRound2Status" runat="server" Text="Not Yet Started"  ></asp:Label>
						<hr/>
					</td>
					<td style="text-align:center;">						
						<asp:Label id="lblRound3Status" runat="server" Text="Not Yet Started"  ></asp:Label>
						<hr/>
					</td>
				</tr>
				
				<tr>
					<td valign="top" style="text-align:right;">
						<asp:Label id="Label2" runat="server" Text="Start Time: " style="font:bold 10pt Calibri;"  ></asp:Label>
					</td>
					<td style="text-align:center;">
						<asp:Label id="lblFirstSetStartTime" runat="server" Text="-"  ></asp:Label>
					</td>
					<td style="text-align:center;">
						<asp:Label id="lblSecondSetStartTime" runat="server" Text="-"  ></asp:Label>
					</td>
					<td style="text-align:center;">
						<asp:Label id="lblThirdSetStartTime" runat="server" Text="-"  ></asp:Label>
					</td>
				</tr>
				<tr>
					<td valign="top" style="text-align:right;">
						<asp:Label id="Label1" runat="server" Text="End Time: " style="font:bold 10pt Calibri;"  ></asp:Label>
					</td>
					<td style="text-align:center;">
						<asp:Label id="lblFirstSetEndTime" runat="server" Text="-"  ></asp:Label>
						<hr/>
					</td>
					
					<td style="text-align:center;">
						<asp:Label id="lblSecondSetEndTime" runat="server" Text="-"  ></asp:Label>
						<hr/>
					</td>
					<td style="text-align:center;">
						<asp:Label id="lblThirdSetEndTime" runat="server" Text="-"  ></asp:Label>
						<hr/>
					</td>
				</tr>
				<tr>
					<td>
						<asp:Label id="lblFirstTeamPlayer" runat="server" Text=""  ></asp:Label>
					</td>					
					<td style="text-align:center;">
						<asp:Label id="lblFirstPlayerFirstSetScore" runat="server" Text="-"  ></asp:Label>
					</td>					
					<td style="text-align:center;">
						<asp:Label id="lblFirstPlayerSecondSetScore" runat="server" Text="-"  ></asp:Label>
					</td>
					<td style="text-align:center;">
						<asp:Label id="lblFirstPlayerThirdSetScore" runat="server" Text="-"  ></asp:Label>
					</td>
					<td style="text-align:center;font: bold 50pt Calibri;border-right:0px;">
						<asp:Button id="btnIncrementScoreFirstTeam" Text="+" runat="server" OnClick="btnIncrementScoreFirstTeam_Click" style="font: bold 50pt Calibri;border:0;background:rgb(235,235,235);height:80px;width:80px;" />
					</td>
					<td style="text-align:center;font: 30pt Calibri; color:rgb(216,216,216);border-right:0px;">
						<asp:Label id="lblFirstHypenBeteenIncrementDecrementButton" runat="server" Text="-"  >|</asp:Label>
					</td>
					<td style="text-align:center;font: bold 50pt Calibri;border-right:0px;">
						<asp:Button id="btnDecrementScoreFirstTeam" Text="-" runat="server" OnClick="btnDecrementScoreFirstTeam_Click" style="font: bold 50pt Calibri;border:0;background:rgb(235,235,235);height:80px;width:80px;" />
					</td>
				</tr>
				<tr>
					<td>
						<asp:Label id="lblSecondTeamPlayer" runat="server" Text=""  ></asp:Label>
					</td>					
					<td style="text-align:center;">
						<asp:Label id="lblSecondPlayerFirstSetScore" runat="server" Text="-"  ></asp:Label>
					</td>
					<td style="text-align:center;">
						<asp:Label id="lblSecondPlayerSecondSetScore" runat="server" Text="-"  ></asp:Label>
					</td>
					<td style="text-align:center;">
						<asp:Label id="lblSecondPlayerThirdSetScore" runat="server" Text="-"  ></asp:Label>
					</td>
					<td style="text-align:center;font: bold 50pt Calibri;border-right:0px;">
						<asp:Button id="btnIncrementScoreSecondTeam" Text="+" runat="server" OnClick="btnIncrementScoreSecondTeam_Click" style="font: bold 50pt Calibri;border:0;background:rgb(235,235,235);height:80px;width:80px;" />
					</td>
					<td style="text-align:center;font: 30pt Calibri; color:rgb(216,216,216);border-right:0px;">
						<asp:Label id="lblSecondHypenBeteenIncrementDecrementButton" runat="server" Text="-"  >|</asp:Label>
					</td>
					<td style="text-align:center;font: bold 50pt Calibri;border-right:0px;">
						<asp:Button id="btnDecrementScoreSecondTeam" Text="-" runat="server" OnClick="btnDecrementScoreSecondTeam_Click" style="font: bold 50pt Calibri;border:0;background:rgb(235,235,235);height:80px;width:80px;" />
					</td>
				</tr>
			</table>
		</asp:Panel>
		
		<table border="0" width="100%" style="padding-top:10px;">
			<tr>
				<td  style="text-align:center;border-right:0px;">
					<asp:Panel runat="server" id="pnlFinishMatch" visible="false">
						<asp:Button id="btnFinishMatch" Text="Finish Match" runat="server" OnClick="btnFinishMatch_Click" width="150px" height="38px" />
					</asp:Panel>
				</td>
			</tr>
			<tr>
				<td  style="text-align:center;border-right:0px;">
					<asp:Panel runat="server" id="pnlWinnerName" visible="false">
						<asp:Label id="lblWinnerName" Text="Winner: " style='font: bold 11pt Calibri;' runat="server" height="38px" />
					</asp:Panel>
				</td>
			</tr>			
		</table>
		
	</div>

<div>
	<hr/>
</div>
<div style='width:100%; text-align:center;padding-top:10px; padding-bottom:20px; font: 10pt calibri; color:gray; background:rgb(235,235,235);'>
	----------------------------------------------      Advertisement      --------------------------------------------
</div>

</asp:Content>
