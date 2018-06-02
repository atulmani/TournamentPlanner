<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="TP_MatchScore.aspx.cs" Inherits="TournamentPlanner.TP_MatchScore" %>

<link rel="stylesheet" href="~/TournamentPlanner.css" type="text/css"/>

<form id="Form_WebForm1" method="post" runat="server">   		
		<div style="font: 26pt Arial Rounded MT Bold;
				color: rgb(4,163,233);	
		width:100%;
		height:50px;
		text-align:center;
		padding-left:0px;		
		padding-top:8px;">
			TOURNAMENT PLANNER
        </div>
	
	<div>
		<table border="0" cellspacing="0" style='font:bold 20pt Calibri; width:100%; height:80px; background:rgb(10,10,10);'>
			<tr>
				<td style='text-align:center;'><a style='text-decoration:none;color:rgb(146,215,10);' href="./TP_Matches.aspx?tcode=bd_tp3">Click to Return</a></td>				
			</tr>
		</table>
	</div>

	<!-- Main Body -->
	<div style=' padding-top:4px; min-height:400px; font: 10pt Calibri; width:100%; background:rgb(256,256,256);'>
		<table align="top" border="0" cellpadding="8" width="100%">
			<tr>
				<td colspan="2" style='border-right:0px; font: bold 22pt Calibri;border-right:0px;background:rgb(231,231,215);'>					
					<asp:Label id="lblMatchScoreHeading" runat="server" Text="Match Score - Live!"  ></asp:Label>					
				</td>				
				<td style='text-align:right; border-right:0px; font: bold 22pt Calibri;background:rgb(231,231,215);color:rgb(10,10,10);'>
					
					<asp:Label id="lblMatchStatus" runat="server" Text="  Match Status: Not Started"  ></asp:Label>					
				</td>
			</tr>			
		</table>
		
		<!-- Show Error Messages -->
		<asp:Panel runat="server" id="pnlErrorMsg" visible="false" height="20px" style='text-align:center; background:rgb(250,250,250);color:red;font:bold 10pt Calibri;'>
			<asp:Label id="lblErrorMsg" runat="server" Text=""  ></asp:Label>
		</asp:Panel>
		
		<asp:Panel runat="server" id="pnlKickOffMatch" visible="false">
			<table width="100%" border="0">
				<tr>
					<td valign="top" style="border-right:0px;">
						<table border="0" align="top" width="100%" cellpadding="15px" >									
							<tr>
								<th style="border-right:0px;">Umpire Code: *</th>
								<td style="border-right:0px;">
									<asp:TextBox id="txtUmpireCode"  runat="server" width="100%" height="36px" MaxLength="200" />
								</td> 
							</tr>
							<tr>
								<th style="border-right:0px;">Umpire Name: *</th>
								<td style="border-right:0px;">
									<asp:TextBox id="txtUmpireName" Text="" runat="server" width="100%" height="36px" MaxLength="200" />
								</td> 
							</tr>
							<tr>
								<th style="border-right:0px;">Court Details: *</th>
								<td style="border-right:0px;">
									<asp:TextBox id="txtMatchLocation" Text="" runat="server" width="100%" height="36px" MaxLength="200" />
								</td> 
							</tr>	
							
							<tr>
								<th style="border-right:0px;">Match Points: *</th>
								<td style="border-right:0px;">
									<asp:DropDownList ID="ddlMatchPoints" runat="server" Width="100%" height="36px">
					                <asp:ListItem Text="Select Match Points" Value="0"></asp:ListItem>
					                <asp:ListItem Text="21" Value="1"></asp:ListItem>
					                <asp:ListItem Text="17" Value="2"></asp:ListItem>
					                <asp:ListItem Text="15" Value="3"></asp:ListItem>
					                <asp:ListItem Text="11" Value="4"></asp:ListItem>
			            		</asp:DropDownList>
								</td> 
							</tr>
						</table>			
					</td>
					
				</tr>
			</table>
			<table width="100%">
				<tr>
					<td style="border-right:0px; padding-top:20px;text-align:center;">
						<asp:Button id="btnStartMatch" Text="KickOff" runat="server" OnClick="btnKickOffMatch_Click" width="150px" height="36px" style="background:rgb(146,215,10);color:rgb(10,10,10);border:none;font: bold 12pt Calibri"  />
					</td>
				</tr>
			</table>
		</asp:Panel>
		
		<asp:Panel runat="server" id="pnlMatchRound" visible="False" style="padding-top:20px;" >
			<table border="0" width="100%" cellpadding="20px" >
				<tr>
					<td style='font: bold 18pt Calibri;border-right:0px;'>
						<asp:Label id="lblMatchID" runat="server" Text="Match ID: 1234 "  ></asp:Label>
					</td>
					<td style='font: 18pt Calibri;border-right:0px;'>
						<asp:Label id="lbltemp123" runat="server" Text="  3 Sets of "  ></asp:Label>
						<asp:Label id="lblNoOfSetsPoints" runat="server" Text=""  ></asp:Label>
					</td>
					<td style='font: bold 18pt Calibri;border-right:0px;'>Match Duration:</td>
					<td style='font: 18pt Calibri;border-right:0px;'>
						<asp:Label id="lblMatchDuration" runat="server" Text="-"  ></asp:Label>
					</td>
				</tr>
				<tr>
					<td style='font: bold 18pt Calibri;border-right:0px;'>Umpire Name:</td>
					<td style='font: 18pt Calibri;border-right:0px;'>
						<asp:Label id="lblUmpireName" runat="server" Text=""  ></asp:Label>
					</td>
				
					<td style='font: bold 18pt Calibri;border-right:0px;'>Match Location:</td>
					<td style='font: 18pt Calibri;border-right:0px;'>
						<asp:Label id="lblMatchLocation" runat="server" Text=""  ></asp:Label>
					</td>
					<td></td>					
				</tr>
			</table>			
			<table width="100%" border="0" cellspacing="25px" style="padding-top: 30px;">
				<tr style="background:#F1F1F1;font:bold 18pt Calibri;">
					<td style="width:40%;">Team / Player Name</td>
					<td style="text-align:center;width:20%;">Round1</td>					
					<td style="text-align:center;width:20%;">Round2</td>
					<td style="text-align:center;width:20%;">Round3</td>
					<td width="50px" style='background:#FFFFFF; font: 12pt Calibri;border-right:0px;'></td>
					<td style='background:#FFFFFF; border-right:0px;'></td>
					<td width="50px" style='background:#FFFFFF; border-right:0px;'></td>
					
				</tr>
				
				<tr>
					<td valign="top" style="text-align:right;">
						<asp:Label id="Label123" runat="server" Text="Status: " style="font:bold 16pt Calibri;"  ></asp:Label>
					</td>
					<td style="text-align:center;">						
						<asp:Label id="lblRound1Status" runat="server" Text="Not Yet Started" style="font:16pt Calibri;"  ></asp:Label>
						<hr/>
					</td>
					<td style="text-align:center;">						
						<asp:Label id="lblRound2Status" runat="server" Text="Not Yet Started" style="font:16pt Calibri;"  ></asp:Label>
						<hr/>
					</td>
					<td style="text-align:center;">						
						<asp:Label id="lblRound3Status" runat="server" Text="Not Yet Started" style="font:16pt Calibri;"  ></asp:Label>
						<hr/>
					</td>
				</tr>
				
				<tr>
					<td valign="top" style="text-align:right;">
						<asp:Label id="Label2" runat="server" Text="Start Time: " style="font:bold 16pt Calibri;"  ></asp:Label>
					</td>
					<td style="text-align:center;">
						<asp:Label id="lblFirstSetStartTime" runat="server" Text="-"  style="font:16pt Calibri;" ></asp:Label>
					</td>
					<td style="text-align:center;">
						<asp:Label id="lblSecondSetStartTime" runat="server" Text="-" style="font:16pt Calibri;" ></asp:Label>
					</td>
					<td style="text-align:center;">
						<asp:Label id="lblThirdSetStartTime" runat="server" Text="-" style="font:16pt Calibri;" ></asp:Label>
					</td>
				</tr>
				<tr>
					<td valign="top" style="text-align:right;">
						<asp:Label id="Label1" runat="server" Text="End Time: " style="font:bold 16pt Calibri;"  ></asp:Label>
					</td>
					<td style="text-align:center;">
						<asp:Label id="lblFirstSetEndTime" runat="server" Text="-" style="font:16pt Calibri;" ></asp:Label>
						<hr/>
					</td>
					
					<td style="text-align:center;">
						<asp:Label id="lblSecondSetEndTime" runat="server" Text="-" style="font:16pt Calibri;" ></asp:Label>
						<hr/>
					</td>
					<td style="text-align:center;">
						<asp:Label id="lblThirdSetEndTime" runat="server" Text="-" style="font:16pt Calibri;" ></asp:Label>
						<hr/>
					</td>
				</tr>
				<tr height="50px">
					<td>
						<asp:Label id="lblFirstTeamPlayer" runat="server" Text="" style="font:bold 22pt Calibri;" ></asp:Label>						
					</td>					
					<td style="text-align:center;">
						<asp:Label id="lblFirstPlayerFirstSetScore" runat="server" Text="-" style="font:bold 22pt Calibri;" ></asp:Label>
					</td>					
					<td style="text-align:center;">
						<asp:Label id="lblFirstPlayerSecondSetScore" runat="server" Text="-" style="font:bold 22pt Calibri;"  ></asp:Label>
					</td>
					<td style="text-align:center;">
						<asp:Label id="lblFirstPlayerThirdSetScore" runat="server" Text="-" style="font:bold 22pt Calibri;" ></asp:Label>
					</td>					
				</tr>
				<tr height="50px">
					<td>
						<asp:Label id="lblSecondTeamPlayer" runat="server" Text="" style="font:bold 22pt Calibri;" ></asp:Label>
					</td>					
					<td style="text-align:center;">
						<asp:Label id="lblSecondPlayerFirstSetScore" runat="server" Text="-" style="font:bold 22pt Calibri;"  ></asp:Label>
					</td>
					<td style="text-align:center;">
						<asp:Label id="lblSecondPlayerSecondSetScore" runat="server" Text="-" style="font:bold 22pt Calibri;"  ></asp:Label>
					</td>
					<td style="text-align:center;">
						<asp:Label id="lblSecondPlayerThirdSetScore" runat="server" Text="-" style="font:bold 22pt Calibri;" ></asp:Label>
					</td>					
				</tr>
			</table>
		</asp:Panel>
		
		<table border="0" width="100%" style="padding-top:30px;">
			
			<tr>
				<td  style="text-align:center;border-right:0px;">
					<asp:Panel runat="server" id="pnlWinnerName" visible="false">
						<asp:Label id="lblWinnerName" Text="Winner: " style='font: bold 24pt Calibri;' runat="server"/>
					</asp:Panel>
				</td>
			</tr>			
		</table>
	</div>
	
	<div style="padding-top:25px;">
		<table border="0" cellspacing="0" style='font:bold 24pt Calibri; width:100%; height:80px; background:rgb(10,10,10);'>
			<tr>
				<td style='text-align:center;'><a style='text-decoration:none;color:rgb(146,215,10);' href="./TP_Matches.aspx?tcode=bd_tp3">Click to Return</a></td>				
			</tr>
		</table>
	</div>
	
<div>
	<hr/>
</div>
</form>
