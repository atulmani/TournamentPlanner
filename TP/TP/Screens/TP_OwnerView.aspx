<%@ Page Title="" Language="C#" MasterPageFile="~/Screens/TP.Master" AutoEventWireup="true" CodeBehind="TP_OwnerView.aspx.cs" Inherits="TournamentPlanner.TP_OwnerView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   	<asp:ScriptManager ID="scriptmanager1" runat="server">

	</asp:ScriptManager>
	
	
	
	<!-- Main Body -->
	<div style=' padding-top:2px; width:100%; background:rgb(256,256,256);'>
	
		
		<table align="top" border="0" cellspacing="1" width="100%" style='padding-bottom:2px;'>
			<tr style='height:10px;'>
				<td style='color:rgb(251,85,58);border-right:0px; border-right:0px;background:rgb(230,230,230);text-align:center;' class="h3BoldText_Atul">Owner's Login</td>
			</tr>			
		</table>
		
		<!-- Owner's Login -->			
		<div class="form_Atul">		
			<asp:Panel runat="server" id="pnlOwnerLogin" visible="true" class="panelRegistration_Atul">				
				<asp:Panel runat="server" id="pnlLoginErrorMsg" visible="false" height="35px" style='text-align:center; background:rgb(250,250,250);color:red;padding-top:10px;' class="h4BoldText_Atul">
					<asp:Label id="lblLoginErrorMsg" runat="server" Text=""  ></asp:Label>
				</asp:Panel>
				<table border="0" align="top" width="100%" cellpadding="0px" cellspacing="0px">
					<tr>
						<th style="border-right:0px;color:rgb(4,163,233);padding-left:20px;padding-top:20px;">Tournament Code:</th>
					</tr>
					<tr>
						<td style="border-right:0px;">
							<asp:TextBox id="txtTournamentCode"  runat="server" width="90%" MaxLength="10" class="textBox_Atul" />
						</td> 
					</tr>
					<tr>
						<th style="border-right:0px;color:rgb(4,163,233);padding-left:20px;padding-top:20px;">User Code:</th>
					</tr>
					<tr>
						<td style="border-right:0px;">
							<asp:TextBox id="txtUserCode"  runat="server" width="90%" MaxLength="20" class="textBox_Atul" />
						</td> 
					</tr>
					<tr>
							<th style="border-right:0px;color:rgb(4,163,233);padding-left:20px;padding-top:20px;">Password:</th>
					</tr>
					<tr>
							<td style="border-right:0px;">
								<asp:TextBox id="txtOwnerPassword" TextMode="Password"  runat="server" width="90%" MaxLength="20" class="textBox_Atul"/>
							</td> 
					</tr>					
				</table>
				<div style="padding-top:15px;text-align:center;">
					<asp:Button id="btnOwnerLogin" Text="GO" runat="server" OnClick="btnOwnerLogin_Click" class="button_Atul" />
				</div>				
			</asp:Panel>
		</div>
		
		<asp:Panel runat="server" id="pnlAfterLogin" visible="false" class="panelRegistration_Atul">
		
			<!-- Section 1: Logged in Tournament Header -->
			<div style=' padding-top:6px;' class="h5Text_Atul">
				<table border="1" align="top" width="100%" style="text-align:center">
					<tr>
						<td style="border-right:0px;width:auto;">Tournament Name:</td>
						<td>
							<asp:Label id="lblTournamentName" Text="Tournament Name"  runat="server"/>
						</td>
					</tr>
					<tr>
						<td style="border-right:0px;">Tournament Organisation:</td>
						<td>
							<asp:Label id="lblTournamentOrganisation" Text="Tournament Organisation"  runat="server"/>
						</td>												
					</tr>
					<tr>
						<td style="border-right:0px;">Tournament Venue:</td>
						<td style="border-right:0px;">
							<asp:Label id="lblTournamentVenue"  Text="Tournament Venue" runat="server"/>
						</td>
					</tr>
					<tr>					
						<td style="border-right:0px;">Owner Name:</td>
						<td>
							<asp:Label id="lblOwnerName" Text="Owner Name" runat="server"/>
						</td>
					</tr>
					<tr>
						<td style="border-right:0px;">Owner ID:</td>
						<td>
							<asp:Label id="lblOwnerID"  Text="Owner ID" runat="server"/>
						</td>
					</tr>
					<tr>
						<td style="border-right:0px;">Owner Address:</td>
						<td style="border-right:0px;">
							<asp:Label id="lblOwnerAddress" Text="Owner Address" runat="server"/>
						</td>
					</tr>
				</table>				
			</div>
						
			<!-- Show Error Messages -->
			<asp:Panel runat="server" id="pnlErrorMsg" visible="false" height="20px" style='text-align:center; background:rgb(250,250,250);color:red;font:bold 18pt Calibri;'>
				<asp:Label id="lblErrorMsg" runat="server" Text=""  ></asp:Label>
			</asp:Panel>
		
			
			<hr/>
			<!-- Dashboard -->
			<div>			
				<asp:LinkButton id="btnToggleQuickAccess" visible="false" Text="Quick Access" runat="server" OnClick="btnToggleQuickAccess_Click" class="button_Atul" />
			
				<asp:Button id="btnRefershDashboard" runat="server" text="Refersh Dashboard" OnClick="btnRefreshDashboard_Click" class="button_Atul" ></asp:Button>
			</div>
			
			<div class="h5Text_Atul">
				
				<table border="1" width="100%" >
					<tr>
						<td>
							Total Players:
							<asp:Label id="lblTotalPlayer" Text="296" runat="server"/>
						</td>
						<td>
							Total entries: 
							<asp:Label id="lblTotalEntries" Text="383" runat="server"/>
						</td>
					</tr>
					<tr>
						<td>
							Singles: 
							<asp:Label id="lblSinglesEntries" Text="345" runat="server"/>
						</td>
						<td>
							Doubles: 
							<asp:Label id="lblDoublesEntries" Text="38" runat="server"/>
						</td>
					</tr>
					<tr>
						<td>
							Total fees receivable: 
							<asp:Label id="lblTotalFeesReceivable" Text="Rs. 1,60,800.00" runat="server"/>
						</td>
						<td>
							Total fees received online:
							<asp:Label id="lblTotalPaymentReceived" Text="Rs. 51,569.00" runat="server"/>
						</td>
					</tr>
					<tr>
						<td>
							Singles amount:
							<asp:Label id="lblSinglesAmount" Text="Rs. 1,38,000.00" runat="server"/>
						</td>
						<td>
							Doubles amount:
							<asp:Label id="lblDoublesAmount" Text="Rs. 22800.00" runat="server"/>
						</td>
					</tr>
					<tr>
						<td>
							Total amount received:
							<asp:Label id="lblTotalAmountReceived" Text="Rs. 1,38,000.00" runat="server"/>
						</td>
						<td>
							Total amount pending:
							<asp:Label id="lblTotalAmountPending" Text="Rs. 1,38,000.00" runat="server"/>
						</td>
					</tr>
					<tr>
						<td>
							Total District Reg Fees receivable:
							<asp:Label id="lblTotalRegAmountReceivable" Text="Rs. 1,38,000.00" runat="server"/>
						</td>
						<td>
							Total District Reg Fees received:
							<asp:Label id="lblTotalRegAmountReceived" Text="Rs. 1,38,000.00" runat="server"/>
						</td>
					</tr>
					
					
				</table>
			</div>

			<div class="form_Atul">
			<asp:Panel runat="server" id="pnlQuickAccess" visible="true" class="panelRegistration_Atul" >
				<table border="0" cellspacing="20px" cellpadding="20px" width="100%" >
					<tr>
						<td  style="text-align:center;border-right:0px;width:50%;">
							<asp:Button ID="btnSetupTournamentView" Text="Setup Tournament" runat="server" OnClick="btnSetupTournamentView_Click" width="100%" height="150px" class="button_Atul"/>
						</td>
						<td  style="text-align:center;border-right:0px;width:50%">
							<asp:Button ID="btnPlayerListView" Text="Player List" runat="server" OnClick="btnPlayerListView_Click"  width="100%" height="150px" class="button_Atul" />
						</td>				
					</tr>
					<tr>
						<td  style="text-align:center;border-right:0px;">
							<asp:Button ID="btnSetupDrawsView" Text="Setup Draws" runat="server" OnClick="btnSetupDrawsView_Click" width="100%" height="150px" class="button_Atul"/>
						</td>
						<td  style="text-align:center;border-right:0px;">
							<asp:Button ID="btnSetupMatchScheduleView" Text="Match Schedule" runat="server" OnClick="btnSetupMatchScheduleView_Click" width="100%" height="150px" class="button_Atul" />
						</td>				
					</tr>									
					<tr>
						<td  style="text-align:center;border-right:0px;">
							<asp:Button ID="btnPaymentView" Text="Payment View" runat="server" OnClick="btnPaymentView_Click" width="100%" height="150px" class="button_Atul"/>
						</td>
						<td  style="text-align:center;border-right:0px;">
							<asp:Button ID="btnOfflineRegistration" Text="Offline Registration" runat="server" OnClick="btnOfflineRegistration_Click" width="100%" height="150px" class="button_Atul"/>
						</td>				
					</tr>
				</table>
			</asp:Panel>			
			</div>
			
			
			<!-- Section 2: Setup Tournament  -->
			<div style=' padding-top:16px;'  class="form_Atul" >
				<asp:LinkButton id="btnToggleSetupTournament" visible="false" Text="+ Setup Tournament" runat="server" OnClick="btnToggleSetupTournament_Click" width="100%" height="56px" style="text-align:center; background:rgb(240,240,240);color:rgb(10,10,10);border:none;font: bold 28pt Calibri" />
				
				<asp:Panel runat="server" id="pnlSetupTournament" visible="false" class="panelRegistration_Atul">
					<div class="h5BoldText_Atul">						
						<div>
							<div style="text-align:center;">			
								<asp:Label id="lblTournamentSetup" runat="server" style="padding-left:10px;" class="h3BoldText_Atul">Setup Tournament</asp:Label>
							</div>
							<hr/>
							<div style="text-align:center;">
								Tournament Code:
								<asp:Label id="lblTournamentID"  runat="server" class="h4BoldText_Atul" />
							</div>
							<hr/>
							<div style="text-align:center;">
								Tournament Status:								
							</div>
							<div style="text-align:center;">
								<asp:Button ID="btnStatusINACTIVE" Text="INACTIVE" runat="server" width="100px" class="button_Atul"/>
								<asp:Button ID="btnStatusOPEN" Text="OPEN" runat="server" Enable=false width="100px"  class="button_Atul"/>
								<asp:Button ID="btnStatusRUNNING" Text="RUNNING" runat="server" Enable=false width="100px" class="button_Atul"/>
								<asp:Button ID="btnStatusCLOSED" Text="CLOSED" runat="server" Enable=false width="100px" class="button_Atul"/>
							<div>
							<hr/>
						
					
					<!-- Tournament Details -->
					<div style="width:100%;">					
						<asp:Button id="btnToggleTournamentDetails" Text="Step 1: Tournament Details" runat="server" OnClick="btnToggleTournamentDetails_Click" style="background:rgb(220,220,220);color:rgb(4,163,233);text-align:left; width:80%;height:58px;border:none;font: bold 16pt Calibri;"/>
						<asp:Button id="btnToggleTournamentDetailsPlus" Text="+" runat="server"  style="background:rgb(220,220,220);color:rgb(251,85,58);float:right; width:20%;height:58px;border:none;font: bold 30pt Calibri;" />
					</div>
					
					<asp:Panel runat="server" id="pnlTournamentDetails" visible="false" class="panelRegistration_Atul">
						<table border="0" align="top" width="100%" cellspacing="0px" cellpadding="0px">																			
							<tr>
								<td style="border-right:0px;color:rgb(4,163,233);padding-left:20px;">Tournament Name: *</td>
							</tr>
							<tr>
								<td style="border-right:0px;">
									<asp:TextBox id="txtTournamentName"  runat="server" width="100%" MaxLength="200" class="textBox_Atul" />
								</td> 
							</tr>
							<tr>
								<td style="border-right:0px;color:rgb(4,163,233);padding-left:20px;">Tournament Venue: *</td>
							</tr>
							<tr>
								<td style="border-right:0px;">
									<asp:TextBox id="txtTournamentVenue" Text="" runat="server" width="100%" MaxLength="200" class="textBox_Atul" />
								</td> 
							</tr>
							<tr>
								<td style="border-right:0px;color:rgb(4,163,233);padding-left:20px;">Tournament Entry Open Date: (dd/mm/yyyy) </td>
							</tr>
							<tr>
								<td style="border-right:0px;">
									<asp:TextBox id="txtTournamentEntryOpenDate" Text="e.g. 23/09/1998" runat="server" width="100%" MaxLength="21" class="textBox_Atul" />
								</td> 
							</tr>
							<tr>
								<td style="border-right:0px;color:rgb(4,163,233);padding-left:20px;">Tournament Entry End Date: (dd/mm/yyyy) </td>
							</tr>
							<tr>
								<td style="border-right:0px;">
									<asp:TextBox id="txtTournamentEntryEndDate" Text="e.g. 23/09/1998" runat="server" width="100%" MaxLength="21" class="textBox_Atul"/>
								</td> 
							</tr>
							<tr>
								<td style="border-right:0px;color:rgb(4,163,233);padding-left:20px;">Tournament Entry Withdrawal Date: (dd/mm/yyyy) </td>
							</tr>
							<tr>
								<td style="border-right:0px;">
									<asp:TextBox id="txtTournamentWithdrawaldate" Text="e.g. 23/09/1998" runat="server" width="100%" MaxLength="21" class="textBox_Atul"/>
								</td> 
							</tr>
							<tr>
								<td style="border-right:0px;color:rgb(4,163,233);padding-left:20px;">Tournament Start Date: (dd/mm/yyyy) *</td>
							</tr>
							<tr>
								<td style="border-right:0px;">
									<asp:TextBox id="txtTournamentStartDate" Text="e.g. 23/09/1998" runat="server" width="100%" MaxLength="21" class="textBox_Atul" />
								</td> 
							</tr>
							<tr>
								<td style="border-right:0px;color:rgb(4,163,233);padding-left:20px;">Tournament End Date: (dd/mm/yyyy) *</td>
							</tr>
							<tr>
								<td style="border-right:0px;">
									<asp:TextBox id="txtTournamentEndDate" Text="e.g. 23/09/1998" runat="server" width="100%" MaxLength="21" class="textBox_Atul" />
								</td> 
							</tr>
							<tr>
								<td style="border-right:0px;color:rgb(4,163,233);padding-left:20px;">Tournament Point Of Contact Names: *</td>
							</tr>
							<tr>
								<td style="border-right:0px;">
									<asp:TextBox id="txtTournamentPOCNames" Text="" runat="server" width="100%" MaxLength="200" class="textBox_Atul" />
								</td> 
							</tr>
							<tr>
								<td style="border-right:0px;color:rgb(4,163,233);padding-left:20px;">Tournament Point Of Contact Nos: *</td>
							</tr>
							<tr>
								<td style="border-right:0px;">
									<asp:TextBox id="txtTournamentPOCContacts" Text="" runat="server" width="100%" MaxLength="200" class="textBox_Atul" />
								</td> 
							</tr>
							<tr>
								<td style="border-right:0px;color:rgb(4,163,233);padding-left:20px;">Tournament Events: *</td>								 
							</tr>
							<tr>							
								<td colspan="2" style="border-right:0px;padding-left:20px;">
									<asp:CheckBoxList ID="cblEvents" repeatDirection="Horizontal" RepeatColumns="8" runat="server" width="100%" style="border-right:0px;" Autopostback="false">       
	
	        						</asp:CheckBoxList> 
								</td>
							</tr>
							<tr>
								<td style="border-right:0px;color:rgb(4,163,233);padding-left:20px;">Tournament Organisation: </td>
							</tr>	
						</table>
					</asp:Panel>
					
					<!-- Additional Details (Optional) -->					
					<div class="topPaddingBetweenObjects_Atul">					
						<asp:Button id="btnToggleTournamentAdditionalDetails" Text="Step 2: Additional Details(Optional)" runat="server" style="background:rgb(220,220,220);color:rgb(4,163,233);text-align:left; width:80%;height:58px;border:none;font: bold 16pt Calibri;"/>
						<asp:Button id="btnToggleTournamentAdditionalDetailsPlus" Text="+" runat="server" style="background:rgb(220,220,220);color:rgb(251,85,58);float:right; width:20%;height:58px;border:none;font: bold 30pt Calibri;" />
					</div>
					
					<asp:Panel runat="server" id="pnlTournamentAdditionalDetails" visible="false" class="panelRegistration_Atul">
						<table border="0" align="top" width="100%" cellspacing="0px" cellpadding="0px">	
							<tr>
								<td style="border-right:0px;">
									<asp:TextBox id="txtTournamentOrganisation" runat="server" width="100%" MaxLength="200" class="textBox_Atul" />
								</td> 
							</tr>
							<tr>
								<td style="border-right:0px;color:rgb(4,163,233);padding-left:20px;">Organisation Logo: </td>
							</tr>
							<tr>
								<td style="border-right:0px;">
									<asp:TextBox id="txtOranisationLogo" Text="" runat="server" width="100%" MaxLength="200" class="textBox_Atul" />
								</td> 
							</tr>							
							<tr>
								<td style="border-right:0px;color:rgb(4,163,233);padding-left:20px;">Tournament Location Address: </td>
							</tr>
							<tr>
								<td style="border-right:0px;">
									<asp:TextBox id="txtTournamentLocationAddress" Text="" runat="server" width="100%" MaxLength="200" class="textBox_Atul" />
								</td> 
							</tr>
							<tr>
								<td style="border-right:0px;color:rgb(4,163,233);padding-left:20px;">Tournament Location Contact Nos: </td>
							</tr>
							<tr>
								<td style="border-right:0px;">
									<asp:TextBox id="txtTournamentLocationContactNos" Text="" runat="server" width="100%" MaxLength="200" class="textBox_Atul" />
								</td> 
							</tr>						
							<tr>
								<td style="border-right:0px;color:rgb(4,163,233);padding-left:20px;">Tournament Duration: </td>
							</tr>
							<tr>
								<td style="border-right:0px;">
									<asp:TextBox id="txtTournamentDuration" Text="" runat="server" width="100%" MaxLength="16" class="textBox_Atul" />
								</td> 
							</tr>
							<tr>
								<td style="border-right:0px;color:rgb(4,163,233);padding-left:20px;">Tournament Sponsers: </td>
							</tr>
							<tr>
								<td style="border-right:0px;">
									<asp:TextBox id="txtTournamentSponsers" Text="" runat="server" width="100%" MaxLength="200" class="textBox_Atul"/>
								</td> 
							</tr>
						</table>
					</asp:Panel>
					
					<!-- Event Category Setup -->
					<div class="topPaddingBetweenObjects_Atul">					
						<asp:Button id="btnToggleTournamentEventSetup" Text="Step 3: Event Setup" runat="server" style="background:rgb(220,220,220);color:rgb(4,163,233);text-align:left; width:80%;height:58px;border:none;font: bold 16pt Calibri;"/>
						<asp:Button id="btnToggleTournamentEventSetupPlus" Text="+" runat="server" style="background:rgb(220,220,220);color:rgb(251,85,58);float:right; width:20%;height:58px;border:none;font: bold 30pt Calibri;" />
					</div>
					
					<asp:Panel runat="server" id="pnlTournamentEventSetup" visible="false" class="panelRegistration_Atul">
						
					</asp:Panel>
					
						<table border="0" align="top" width="100%" cellspacing="0px" cellpadding="0px">	
							<tr>
								<td style="border-right:0px;color:rgb(4,163,233);padding-left:20px;">Tournament Status: </td>
							</tr>
							<tr>
								<td style="border-right:0px;">									
									<asp:DropDownList ID="ddlTournamentStatus" runat="server" width="100%" class="dropdown_Atul">						                
						                <asp:ListItem Text="INACTIVE" Value="INACTIVE"></asp:ListItem>
						                <asp:ListItem Text="OPEN" Value="OPEN"></asp:ListItem>
						                <asp:ListItem Text="RUNNING" Value="RUNNING"></asp:ListItem>
						                <asp:ListItem Text="CLOSED" Value="CLOSED"></asp:ListItem>
				            		</asp:DropDownList>
								</td> 
							</tr>							
						</table>
					</div>
					<div style="text-align:center;padding-top:20px;">
						<asp:Button id="btnSetupTournament" Text="Setup Tournament" runat="server" OnClick="btnSetupTournament_Click" class="button_Atul" />
					</div>
					
				</asp:Panel>
			</div>
			
			<!-- Section 4: Generate Draws  -->
			<div style='padding-top:6px;'>
				
				
				<asp:LinkButton id="btnToggleGenerateDraws" Text="+ Generate Draws" visible="false" runat="server" OnClick="btnToggleGenerateDraws_Click" width="100%" height="36px" style="text-align:center;valign:middle; background:rgb(240,240,240);color:rgb(10,10,10);border:none;font: bold 12pt Calibri" />
				<asp:Panel runat="server" id="pnlGenerateDraws" visible="false" style='background:rgb(250,250,250);font: 10pt Calibri;padding-bottom:10px;'>
					<div style='font: 16pt Calibri;color:red;'>Note: Generate Draws only possible when the tournament status is "OPEN"</div>
					<div>
						<div style='font: bold 22pt Calibri;background:rgb(231,231,215);'>			
						<asp:Label id="lblGenerateDraws" runat="server" style="padding-left:10px;">Generate Draws</asp:Label>
					</div>
						
					<table width="100%" border=0 cellspacing="15px" cellpadding="0px">						
						<tr>	
							<!-- Dropdown for Participated Events to Generate Draws for the same -->
							<td style="border-right:0px;">
								<asp:DropDownList ID="ddlTournamentEvents" runat="server" width="100%" height="66px" style='font: 20pt Calibri;'>
					                <asp:ListItem Text="Select Event" Value="0"></asp:ListItem>					                
			            		</asp:DropDownList>
	            			</td>
	            		</tr>
	            		
	            		<tr>	
							<!-- Dropdown for Event Type to Generate Draws for the same -->
							<td style="border-right:0px;display:none;">
								<asp:DropDownList ID="ddlTournamentEventType" runat="server" width="100%" height="66px" style='font: 16pt Calibri;'>
					                <asp:ListItem Text="Select Event Type" Value="0"></asp:ListItem>
					                <asp:ListItem Text="Singles" Value="S"></asp:ListItem>
					                <asp:ListItem Text="Doubles" Value="D"></asp:ListItem>
			            		</asp:DropDownList>
	            			</td>
	            		</tr>
	            		
						<tr>            		
							<!-- Button to click to generate Draws -->		
							<td style="text-align:center;border-right:0px;">
								<asp:Button id="btnGenerateDraws" Text="Generate Draws" runat="server" OnClick="btnGenerateDraws_Click" width="350px" height="68px" style="background:rgb(146,215,10);color:rgb(10,10,10);border:none;font: bold 26pt Calibri;align:center; text-align:center;" />		
							</td>
						</tr>
						
						
					
				</asp:Panel>
			</div>
		
		</asp:Panel>


	
	<!-- Player List -->
	<asp:Panel runat="server" id="pnlPlayerList" visible="false"  style='background:rgb(250,250,250);font: 10pt Calibri;padding-bottom:10px;'>

		<div style='font: bold 20pt Calibri;'>
			Category Wise:
			<asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>			
		</div>
				
		<div style='font: bold 22pt Calibri;background:rgb(231,231,215);margin-top:20px;'>			
			<asp:Label id="lblPlayerList" runat="server" style="padding-left:10px;">Player List of Category: </asp:Label>
			<asp:Label ID="Label1" runat="server" Text=""  ></asp:Label>
			<asp:Button id="btnExportPlayerList" runat="server" Text="Export" OnClick="ExportPlayerList_Click" height="40px" width="200px" style="background:rgb(146,215,10);color:rgb(10,10,10);border:none;font: bold 20pt Calibri;align:center; text-align:center;margin-left:50%;"></asp:Button>			
		</div>
		<div style='font: 16pt Calibri; background:rgb(256,256,256);text-align:top; '>
			<asp:DataGrid ID="dgPlayerList" width="100%" runat="server" PageSize="1" AllowPaging="False"
				AutoGenerateColumns="False" GridLines="None" >  
				<Columns>
					<asp:BoundColumn HeaderText="Player Code" DataField="PlayerCode" ItemStyle-Width="5%"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Player Name" DataField="PlayerFullName" ItemStyle-Width="10%"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Contact#" DataField="PlayerContact" ItemStyle-Width="5%"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="EmailID" DataField="PlayerEmailID" ItemStyle-Width="10%"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Date Of Birth" DataField="PlayerDOB"  ItemStyle-Width="5%"></asp:BoundColumn>					
					<asp:BoundColumn HeaderText="Category" DataField="EventCode"  ItemStyle-Width="5%"></asp:BoundColumn>
					
					<asp:BoundColumn HeaderText="Parter Name" DataField="PartnerName" ItemStyle-Width="10%"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Parter Date Of Birth" DataField="PartnerDOB" ItemStyle-Width="5%"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Amount" DataField="Amount"  ItemStyle-Width="5%"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Payment Status" DataField="PaymentStatus"  ItemStyle-Width="10%"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Timestamp" DataField="Timestamp"  ItemStyle-Width="10%"></asp:BoundColumn>
					
				</Columns>  
				
				<FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="Black" />  
				
				<PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" Mode="NumericPages" />  
				
				<AlternatingItemStyle BackColor="#F5F5F5" />  
				
				<ItemStyle BackColor="#FFFFFF" ForeColor="#666666" Height="35px" />  
				
				<HeaderStyle height="15px" BackColor="#F5F5F5" Font-Bold="True" Font-Size="14pt" ForeColor="#555555" />  
			
			</asp:DataGrid>
		</div>
	</asp:Panel>
		
		
	
	<!-- Playment View -->
	<asp:Panel runat="server" id="pnlPayment" visible="false" width="100%" style='background:rgb(250,250,250);font: 10pt Calibri;padding-bottom:10px;'>
		<div style='font: bold 20pt Calibri;display : none'>
			<table >
				<tr>
					<td>
						Pending Amount
					</td>
					<td>
						Cofirmed Amount
					</td>
					<td>
						Status In Progress Amount
					</td>
				</tr>
				<tr>
					<td>
						<asp:Label id="lblTotalPendingAmount" runat="server"></asp:Label>
					</td>
					<td>
						<asp:Label id="lblTotalConfirmedAmount" runat="server"></asp:Label>
					</td>
					<td>
						<asp:Label id="lblTotalInProgressAmount" runat="server"></asp:Label>
					</td>
				</tr>
			</table>
		</div>
		
		
		<div style='font: bold 20pt Calibri;width:100%;'>
				<div style="width:100%;">
					
							<asp:RadioButtonList ID="rbPaymentReportOptions" runat="server"
								repeatdirection="horizontal" width="100%"
								OnCheckedChanged="rbPaymentReportOptions_CheckedChanged" autoPostBack="true">
		            	        <asp:ListItem>Pending</asp:ListItem>
		            	        <asp:ListItem>Completed</asp:ListItem>
		            	        <asp:ListItem>Status Update - In Progress</asp:ListItem>
		            	    </asp:RadioButtonList>									
						
			</div>
			
			<div style="padding-top:50px;text-align:center;">
				<asp:Button id="btnGetPaymentList" runat="server" text="Get Payment List" OnClick="btnGetPaymentList_Click" width="250px" height="66px" style="background:rgb(146,215,10);color:rgb(10,10,10);border:none;font: bold 20pt Calibri;align:center; text-align:center;" ></asp:Button>
			</div>			
			
			
			<table width="100%">
				<tr>
					<td>						
						<asp:Button id="btnSelectAndTakeAction" runat="server" text="Select and Act" visible="false" OnClick="btnSelectAndTakeAction_Click" width="300px" height="66px" style="background:rgb(146,215,10);color:rgb(10,10,10);border:none;font: bold 16pt Calibri;align:center; text-align:center;" ></asp:Button>
					</td>
					<td>
						<asp:Button id="btnSelectChangeStatus" runat="server" text="Select and Act" visible="false" OnClick="btnSelectChangeStatus_Click" width="300px" height="66px" style="background:rgb(146,215,10);color:rgb(10,10,10);border:none;font: bold 16pt Calibri;align:center; text-align:center;" ></asp:Button>
					</td>
				</tr>
				<tr>
					<td>
						<asp:Button id="btnUpdateOfflineStatus" runat="server" text="Confirm Offline(Cash) Payment" visible="false" OnClick="btnUpdateOfflineStatus_Click" width="300px" height="66px" style="background:rgb(146,215,10);color:rgb(10,10,10);border:none;font: bold 16pt Calibri;align:center; text-align:center;" ></asp:Button>
					</td>
					<td>
						<asp:Button id="btnExportPayment" runat="server" text="Click to Export" OnClick="ExportPaymentList_Click" width="300px" height="66px" style="background:rgb(146,215,10);color:rgb(10,10,10);border:none;font: bold 16pt Calibri;align:center; text-align:center;" ></asp:Button>
					</td>
				</tr>
				<tr>
					<td>
						<asp:Button id="btnDrawsANDSchedulePublish" runat="server" text="Dwaws and Schedule Published" OnClick="btnDrawsANDSchedulePublish_Click" width="400px" height="66px" style="background:rgb(146,215,10);color:rgb(10,10,10);border:none;font: bold 20pt Calibri;align:center; text-align:center;" ></asp:Button>
					</td>
					<td>
					</td>
				</tr>
		</div>
		
				
				
		<br/>
		
		<hr/>
		<div style='font: bold 20pt Calibri;width:100%;'>
		<table width="100%">
		<tr>
			<td>	Total Entry : </td>
			<td> <asp:Label id="lblTotalRow" runat="server"></asp:Label></td>
			<td> Total Amount : </td>
			<td> 
			<asp:Label id="lblTotalAmount" runat="server"></asp:Label>
			</td>
		</tr>
		</table>
		</div>
		<br/>
		<div style='font: 16pt Calibri; background:rgb(256,256,256);text-align:top;width:100%;'>
			<asp:DataGrid ID="dgPayment" width="100%" runat="server" PageSize="1" AllowPaging="False"
				AutoGenerateColumns="False" GridLines="None" >  
				<Columns>
					<asp:BoundColumn HeaderText="Player Code" DataField="PlayerCode"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Player Name" DataField="PlayerFullName"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Contact#" DataField="PlayerContact" ItemStyle-Width="2%"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="EmailID" DataField="PlayerEmailID" ItemStyle-Width="5%"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Date Of Birth" DataField="PlayerDOB"  ItemStyle-Width="5%"></asp:BoundColumn>					
					<asp:BoundColumn HeaderText="Category" DataField="EventCode"  ItemStyle-Width="5%"></asp:BoundColumn>
					
					<asp:BoundColumn HeaderText="Parter Name" DataField="PartnerName" ></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Parter Date Of Birth" DataField="PartnerDOB" ></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Amount" DataField="Amount"  ItemStyle-Width="5%"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Payment Status" DataField="PaymentStatus"  ItemStyle-Width="10%"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Timestamp" DataField="Timestamp" ></asp:BoundColumn>
					 <asp:TemplateColumn ItemStyle-HorizontalAlign="Center">
					 <HeaderTemplate>
					
					<asp:CheckBox AutoPostBack="True" ID="chkCheckAll" Runat="server" OnCheckedChanged="chkCheckAll_CheckedChanged"/>
					 </HeaderTemplate>
					 <ItemTemplate>
					 <asp:CheckBox ID="chkSelection" Runat="server" />
						<asp:HiddenField id="hfPaymentID" runat="server" Value='<%# Bind("PaymentID") %>' Visible="false" ></asp:HiddenField>
					</ItemTemplate>
					 </asp:TemplateColumn> 
					
				</Columns>  
				
				<FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="Black" />  
				
				<PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" Mode="NumericPages" />  
				
				<AlternatingItemStyle BackColor="#F5F5F5" />  
				
				<ItemStyle BackColor="#FFFFFF" ForeColor="#666666" Height="35px" />  
				
				<HeaderStyle height="15px" BackColor="#F5F5F5" Font-Bold="True" Font-Size="14pt" ForeColor="#555555" />  
			
			</asp:DataGrid>
		</div>
	</asp:Panel>
	
	<asp:Panel runat="server" id="pnlOfflineRegistration" visible="false" width="100%" style='background:rgb(250,250,250);font: 10pt Calibri;padding-bottom:10px;'>
		<div style="padding-top:40px;">
			<asp:LinkButton id="lbtnOfflineRegistrationForm" Text="Offline Registration Form" runat="server" OnClick="lbtnOfflineRegistrationForm_Click" style="text-align:center;valign:middle; border:none;font: bold 24pt Calibri" />
		</div>		
	</asp:Panel>
		
		
		
	</div>
	
	<div style=' padding-top:6px;'>
			<hr/>
	</div>
	
	
		

</asp:Content>
