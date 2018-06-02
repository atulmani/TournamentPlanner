<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="TP_UmpireView.aspx.cs" Inherits="TournamentPlanner.TP_UmpireView" %>


<form id="Form_WebForm1" method="post" runat="server">  

<asp:ScriptManager ID="scriptmanager1" runat="server">

</asp:ScriptManager>


	
		<div>
        	<table border="0" cellspacing="0" ba style='color:rgb(10,10,10); font: 36pt Arial Rounded MT Bold; width:100%; height:80px; background:rgb(146,215,10);' >
				<tr>
					<td style='border-right:0px;'>Tournament Planner</td>
				</tr>
			</table>
        </div>
	
	<div>
		<table border="0" cellspacing="0" style='font:bold 24pt Calibri; width:100%; height:55px; background:rgb(10,10,10);'>
			<tr>				
				<td style='text-align:center;'><a style='text-decoration:none;color:rgb(146,215,10);' href="./TP_TournamentController.aspx">Home</a></td>				
			</tr>
		</table>
	</div>

	<!-- Main Body -->
	<div style=' padding-top:2px; min-height:400px; font: 10pt Calibri; width:100%; background:rgb(256,256,256);'>
		<table align="top" border="0" cellspacing="0" width="100%" height="20px">
			<tr>
				<td colspan="2" style='border-right:0px; font: bold 22pt Calibri;border-right:0px;background:rgb(231,231,215);'>					
					<asp:Label id="lblMatchScoreHeading" runat="server" Text="Umpire's Login"  ></asp:Label>					
				</td>				
				<td style='text-align:right; border-right:0px; font: bold 22pt Calibri;background:rgb(231,231,215);color:rgb(10,10,10);'>
					
					<asp:Label id="lblMatchStatus" runat="server" Text=""  ></asp:Label>					
				</td>
			</tr>			
		</table>
		
		<!-- Show Error Messages -->
		<asp:Panel runat="server" id="pnlErrorMsg" visible="true" height="20px" style='text-align:center; background:rgb(250,250,250);color:red;font:bold 24pt Calibri;'>
			<asp:Label id="lblErrorMsg" runat="server" Text=""  ></asp:Label>
		</asp:Panel>
		
		<!-- Login -->
		<div  style="padding-top:30px;padding-bottom:40px;">
			<asp:Panel runat="server" id="pnlUmpireLogin" visible="true" style='background:rgb(250,250,250);height:auto;'>
				<table border="0" width="100%" cellspacing="40px" style="align:center;font: bold 26px calibri;" >
					<tr>
						<th style="border-right:0px;width:20%;">Tournament Code:</th>
						<td style="border-right:0px;">
							<asp:TextBox id="txtTournamentCode"  runat="server" width="50%" height="56px" MaxLength="50" style="font: bold 36px Calibri;"  />
						</td> 
					</tr>
					<tr>
						<th style="border-right:0px;">User Code:</th>
						<td style="border-right:0px;">
							<asp:TextBox id="txtUserCode"  runat="server" width="50%" height="56px" MaxLength="50" style="font: bold 36px Calibri;" />
						</td> 
					</tr>
					<tr>
							<th style="border-right:0px;">Password:</th>
							<td style="border-right:0px;">
								<asp:TextBox id="txtOTPCode" TextMode="Password"  runat="server" width="50%" height="56px" MaxLength="50" style="font: bold 36px Calibri;" />
							</td> 
					</tr>
					<tr>
						<th style="border-right:0px;">Captcha: *</th>
						<td style="border-right:0px;">
							<asp:TextBox id="txtCaptchaCode"  runat="server" width="50%" height="56px" MaxLength="50" style="font: bold 36px Calibri;" />
						</td> 
					</tr>
				</table>
				
				<table width="100%">
					<tr>
						<th style="width:23%;"></th>					
						<td style="border-right:0px; padding-top:30px;">						
							<asp:Button id="btnOwnerLogin" Text="GO" runat="server" OnClick="btnOwnerLogin_Click" width="200px" height="68px" style="background:rgb(146,215,10);color:rgb(10,10,10);border:none;font: bold 26pt Calibri;" />							
						</td>
					</tr>
				</table>
			</asp:Panel>
		</div>
		
		<!-- Match List Grid -->
		<div>
			<asp:Panel runat="server" id="pnlMatchList" visible="false">
				<asp:DataGrid ID="dgMatchList" width="100%" runat="server" PageSize="1" AllowPaging="False"
					AutoGenerateColumns="False" GridLines="None">  
					<Columns>
						<asp:TemplateColumn HeaderText="Match ID" ItemStyle-Width="5%">
							<ItemTemplate>						
								<asp:LinkButton ID="lbMatchID" runat="server" Text='<%# Bind("ID") %>' PostBackUrl='<%# string.Format("./TP_UmpireView.aspx?MatchID={0}&EventCode={1}", Eval("ID"), Eval("EventCode")) %>' OnClick="lbMatchID_Click"></asp:LinkButton>
					      </ItemTemplate>
		
						 </asp:TemplateColumn>
						
						
						<asp:BoundColumn HeaderText="Time" DataField="MatchSchedule" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%"></asp:BoundColumn>
						<asp:BoundColumn HeaderText="Draw" DataField="EventCode" ItemStyle-Width="10%"></asp:BoundColumn>
						<asp:BoundColumn HeaderText="Player1" DataField="FirstTeamPlayerName" ItemStyle-Font-Bold="False"></asp:BoundColumn>																			
						<asp:BoundColumn HeaderText="Player2" DataField="SecondTeamPlayerName"></asp:BoundColumn>
						<asp:BoundColumn HeaderText="Score" DataField="MatchScore" ItemStyle-Width="10%" visible="false"></asp:BoundColumn>
						<asp:BoundColumn HeaderText="Status" DataField="MatchStatus" ItemStyle-Width="12%" visible="true"></asp:BoundColumn>
						<asp:BoundColumn HeaderText="Umpire Name" DataField="UmpireName" visible="true" ItemStyle-Width="18%" ></asp:BoundColumn>
						<asp:BoundColumn HeaderText="Winner" DataField="WinnerTeamCode"  visible="false" ></asp:BoundColumn>
					</Columns>  
					
					<FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="Black" />  
					
					<PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" Mode="NumericPages" />  
					
					<AlternatingItemStyle BackColor="#F5F5F5" />  
					
					<ItemStyle BackColor="#FFFFFF" ForeColor="#666666" Height="35px" />  
					
					<HeaderStyle height="40px" BackColor="#F5F5F5" Font-Bold="True" Font-Size="12pt" ForeColor="#555555" />  
				
				</asp:DataGrid>
			</asp:Panel>
		</div>
		
		<!-- Match Kickoff -->
		<asp:Panel runat="server" id="pnlKickOffMatch" visible="false">
			<table width="100%" border="0">
				<tr>
					<td valign="top" style="border-right:0px;">
						<table border="0" align="top" width="100%" cellpadding="15px" >									
							<tr>
								<th style="border-right:0px;">Umpire Code: *</th>
								<td style="border-right:0px;">
									<asp:TextBox id="txtUmpireCode"  runat="server" width="100%" height="36px" MaxLength="20" />
								</td> 
							</tr>
							<tr>
								<th style="border-right:0px;">Umpire Name: *</th>
								<td style="border-right:0px;">
									<asp:TextBox id="txtUmpireName" Text="" runat="server" width="100%" height="36px" MaxLength="50" />
								</td> 
							</tr>
							<tr>
								<th style="border-right:0px;">Court Details: *</th>
								<td style="border-right:0px;">
									<asp:TextBox id="txtMatchLocation" Text="" runat="server" width="100%" height="36px" MaxLength="50" />
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
		
		<!-- Match Round -->
		<asp:Panel runat="server" id="pnlMatchRound" visible="False" style="padding-top:10px;" >
			<table border="0" width="100%" >
				<tr>
					<td style='font: bold 11pt Calibri;border-right:0px;'>
						<asp:Label id="lblMatchID" runat="server" Text="Match ID: 1234 "  ></asp:Label>
					</td>
					<td style='font: 11pt Calibri;border-right:0px;'>
						<asp:Label id="lbltemp123" runat="server" Text="  3 Sets of "  ></asp:Label>
						<asp:Label id="lblNoOfSetsPoints" runat="server" Text=""  ></asp:Label>
					</td>
					<td style='font: bold 11pt Calibri;border-right:0px;'>Match Duration:</td>
					<td style='font: 11pt Calibri;border-right:0px;'>
						<asp:Label id="lblMatchDuration" runat="server" Text="-"  ></asp:Label>
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
					<td></td>					
				</tr>
			</table>	
		</asp:Panel>
		
		<asp:UpdatePanel ID="updatepnl" runat="server">
			<ContentTemplate>
			<table width="100%" border="0" style="padding-top: 20px;">
				<tr style="background:#F1F1F1;font:bold 10pt Calibri;">
					<td style="width:40%;">Team / Player Name</td>
					<td style="text-align:center;width:20%;">Round1</td>					
					<td style="text-align:center;width:20%;">Round2</td>
					<td style="text-align:center;width:20%;">Round3</td>
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
						<asp:Label id="lblRound1Status" runat="server" Text="Not Yet Started" style="font:10pt Calibri;"  ></asp:Label>
						<hr/>
					</td>
					<td style="text-align:center;">						
						<asp:Label id="lblRound2Status" runat="server" Text="Not Yet Started" style="font:10pt Calibri;"  ></asp:Label>
						<hr/>
					</td>
					<td style="text-align:center;">						
						<asp:Label id="lblRound3Status" runat="server" Text="Not Yet Started" style="font:10pt Calibri;"  ></asp:Label>
						<hr/>
					</td>
				</tr>
				
				<tr>
					<td valign="top" style="text-align:right;">
						<asp:Label id="Label2" runat="server" Text="Start Time: " style="font:bold 10pt Calibri;"  ></asp:Label>
					</td>
					<td style="text-align:center;">
						<asp:Label id="lblFirstSetStartTime" runat="server" Text="-" style="font:9pt Calibri;" ></asp:Label>
					</td>
					<td style="text-align:center;">
						<asp:Label id="lblSecondSetStartTime" runat="server" Text="-" style="font:9pt Calibri;" ></asp:Label>
					</td>
					<td style="text-align:center;">
						<asp:Label id="lblThirdSetStartTime" runat="server" Text="-" style="font:9pt Calibri;" ></asp:Label>
					</td>
				</tr>
				<tr>
					<td valign="top" style="text-align:right;">
						<asp:Label id="Label1" runat="server" Text="End Time: " style="font:bold 10pt Calibri;"  ></asp:Label>
					</td>
					<td style="text-align:center;">
						<asp:Label id="lblFirstSetEndTime" runat="server" Text="-" style="font:9pt Calibri;" ></asp:Label>
						<hr/>
					</td>
					
					<td style="text-align:center;">
						<asp:Label id="lblSecondSetEndTime" runat="server" Text="-" style="font:9pt Calibri;" ></asp:Label>
						<hr/>
					</td>
					<td style="text-align:center;">
						<asp:Label id="lblThirdSetEndTime" runat="server" Text="-" style="font:9pt Calibri;" ></asp:Label>
						<hr/>
					</td>
				</tr>
				<tr height="50px">
					<td>
						<asp:Label id="lblFirstTeamPlayer" runat="server" Text="" style="font:10pt Calibri;" ></asp:Label>						
					</td>					
					<td style="text-align:center;">
						<asp:Label id="lblFirstPlayerFirstSetScore" runat="server" Text="-" style="font:11pt Calibri;" ></asp:Label>
					</td>					
					<td style="text-align:center;">
						<asp:Label id="lblFirstPlayerSecondSetScore" runat="server" Text="-" style="font:11pt Calibri;"  ></asp:Label>
					</td>
					<td style="text-align:center;">
						<asp:Label id="lblFirstPlayerThirdSetScore" runat="server" Text="-" style="font:11pt Calibri;" ></asp:Label>
					</td>
					<td style="text-align:center;font: bold 50pt Calibri;border-right:0px;">
						<asp:Button id="btnWalkoverFirstTeam" Text="Walkover" runat="server" OnClick="btnWalkoverFirstTeam_Click" visible="false" style="font: bold 10pt Calibri;border:0;background:rgb(235,235,235);height:20px;text-align:right;" />
						<asp:Button id="btnIncrementScoreFirstTeam" Text="+" runat="server" OnClick="btnIncrementScoreFirstTeam_Click" style="font: bold 50pt Calibri;border:0;background:rgb(235,235,235);height:80px;width:80px;" />
					</td>
					<td style="text-align:center;font: 30pt Calibri; color:rgb(216,216,216);border-right:0px;">
						<asp:Label id="lblFirstHypenBeteenIncrementDecrementButton" runat="server" Text="-"  >|</asp:Label>
					</td>
					<td style="text-align:center;font: bold 50pt Calibri;border-right:0px;">
						<asp:Button id="btnDecrementScoreFirstTeam" Text="-" runat="server" OnClick="btnDecrementScoreFirstTeam_Click" style="font: bold 50pt Calibri;border:0;background:rgb(235,235,235);height:80px;width:80px;" />
					</td>
				</tr>
				<tr height="50px">
					<td>
						<asp:Label id="lblSecondTeamPlayer" runat="server" Text="" style="font:10pt Calibri;" ></asp:Label>
					</td>					
					<td style="text-align:center;">
						<asp:Label id="lblSecondPlayerFirstSetScore" runat="server" Text="-" style="font:11pt Calibri;"  ></asp:Label>
					</td>
					<td style="text-align:center;">
						<asp:Label id="lblSecondPlayerSecondSetScore" runat="server" Text="-" style="font:11pt Calibri;"  ></asp:Label>
					</td>
					<td style="text-align:center;">
						<asp:Label id="lblSecondPlayerThirdSetScore" runat="server" Text="-" style="font:11pt Calibri;" ></asp:Label>
					</td>
					<td style="text-align:center;font: bold 50pt Calibri;border-right:0px;">
						<asp:Button id="btnWalkoverSecondTeam" Text="Walkover" runat="server" OnClick="btnWalkoverSecondTeam_Click" visible="false" style="font: bold 10pt Calibri;border:0;background:rgb(235,235,235);height:20px;" />	
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
			</ContentTemplate>
		</asp:UpdatePanel>
		
		
		
		<table border="0" width="100%" style="padding-top:10px;">
			<tr>
				<td  style="text-align:center;border-right:0px;">
					<asp:Panel runat="server" id="pnlFinishMatch" visible="true">
						<asp:Button id="btnFinishMatch" Text="Finish Match" runat="server" OnClick="btnFinishMatch_Click" width="150px" height="36px" style="background:rgb(146,215,10);color:rgb(10,10,10);border:none;font: bold 12pt Calibri" />
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
</form>
