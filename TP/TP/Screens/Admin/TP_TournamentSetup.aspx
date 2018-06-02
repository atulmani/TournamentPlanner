<%@ Page Title="" Language="C#" MasterPageFile="~/Screens/TP.Master" AutoEventWireup="true" CodeBehind="TP_TournamentSetup.aspx.cs" Inherits="TournamentPlanner.TP_TournamentSetup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   	<asp:ScriptManager ID="scriptmanager1" runat="server">

	</asp:ScriptManager>
	
	
	
	<!-- Main Body -->
	<div style=' padding-top:2px; width:100%; background:rgb(256,256,256);'>
	
		
		<table align="top" border="0" cellspacing="1" width="100%" style='padding-bottom:2px;'>
			<tr style='height:10px;'>
				<td style='color:rgb(251,85,58);border-right:0px; border-right:0px;background:rgb(230,230,230);text-align:center;' class="h3BoldText_Atul">Tournament Setup</td>
			</tr>			
		</table>
		
		
		<div style="text-align:center;padding:8px;" class="h3BoldText_Atul">			
			<asp:LinkButton id="lbtnReturn2Dashboard" Text="RETURN TO HOME" runat="server" OnClick="lbtnReturn2Dashboard_Click"/>
           <!-- <img src="./TP_Dashboard.aspx"      icoHome.png -->
		</div>
		
		
		<asp:Panel runat="server" id="pnlAfterLogin" visible="true" class="panelRegistration_Atul">
					
			<!-- Show Error Messages -->
			<asp:Panel runat="server" id="pnlErrorMsg" visible="false" height="20px" style='text-align:center; padding-top:8px; background:rgb(250,250,250);color:red;font:bold 12pt Calibri;'>
				<asp:Label id="lblErrorMsg" runat="server" Text=""  ></asp:Label>
			</asp:Panel>
		
			<!-- Section 2: Setup Tournament  -->
			<div style=' padding-top:16px;'  class="form_Atul" >
				<asp:LinkButton id="btnToggleSetupTournament" visible="false" Text="+ Setup Tournament" runat="server" OnClick="btnToggleSetupTournament_Click" width="100%" height="56px" style="text-align:center; background:rgb(240,240,240);color:rgb(10,10,10);border:none;font: bold 28pt Calibri" />
				
				
				<asp:Panel runat="server" id="pnlSetupTournament" visible="true" class="panelRegistration_Atul">
					<div class="h4BoldText_Atul">	
							<div style="text-align:center;">
								Tournament Code:
								<asp:Label id="lblTournamentID"  runat="server" Text="" class="h3BoldText_Atul" />
							</div>
							<br/>
							<div style="text-align:center;">
								Registration Form Type:															
								<asp:Label id="lblRegistrationFormType" runat="server" Text="" class="h3BoldText_Atul"  ></asp:Label>
							</div>
							<hr/>
							
							<div style="text-align:center;">
								Tournament Status:								
							</div>
							<div style="text-align:center;width:100%;">
								<asp:Button ID="btnStatusINACTIVE" Text="INACTIVE" runat="server" OnClick="btnStatusINACTIVE_Click" width="20%"/>
								<asp:Button ID="btnStatusUPCOMING" Text="UPCOMING" runat="server" OnClick="btnStatusUPCOMING_Click" width="22%" />
								<asp:Button ID="btnStatusOPEN" Text="OPEN" runat="server" OnClick="btnStatusOPEN_Click" width="18%" />
								<asp:Button ID="btnStatusRUNNING" Text="RUNNING" runat="server" OnClick="btnStatusRUNNING_Click" width="20%" />
								<asp:Button ID="btnStatusCLOSED" Text="CLOSED" runat="server" OnClick="btnStatusCLOSED_Click" visible="false" width="20%"/>
							</div>
							<div style="text-align:center;padding-top:5px;color:red;">
								<asp:Label id="lblStatusTooltip" runat="server" Text="" class="h4BoldText_Atul"  ></asp:Label>
							</div>
							<hr/>
													
					
					<!-- Tournament Details -->
					<div style="width:100%;">					
						<asp:Button id="btnToggleTournamentDetails" Text="Step 1: Tournament Details" runat="server" OnClick="btnToggleTournamentDetails_Click" style="background:rgb(220,220,220);color:rgb(4,163,233);text-align:left; width:80%;" class="toggleButton_Atul" />
						<asp:Button id="btnToggleTournamentDetailsPlus" Text="+" runat="server" OnClick="btnToggleTournamentDetails_Click"  style="background:rgb(220,220,220);color:rgb(251,85,58);float:right; width:20%;height:58px;border:none;font: bold 30pt Calibri;" />
					</div>
					
					<asp:Panel runat="server" id="pnlTournamentDetails" visible="false" width="100%" class="panelRegistration_Atul">
						<table border="0" align="top" width="90%" cellspacing="0px" cellpadding="0px">																			
							<tr>
								<td style="border-right:0px;color:rgb(4,163,233);padding-left:20px;">Tournament Organisation: </td>
							</tr>	
							<tr>
								<td style="border-right:0px;">
									<asp:TextBox id="txtTournamentOrganisation" runat="server" width="100%" MaxLength="200" class="textBox_Atul" />
								</td> 
							</tr>
							<tr>
								<td style="border-right:0px;color:rgb(4,163,233);padding-left:20px;">Tournament Name:</td>
							</tr>
							<tr>
								<td style="border-right:0px;">
									<asp:TextBox id="txtTournamentName"  runat="server" width="100%" MaxLength="200" class="textBox_Atul" />
								</td> 
							</tr>
							<tr>
								<td style="border-right:0px;color:rgb(4,163,233);padding-left:20px;">Tournament Venue:</td>
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
								<td style="border-right:0px;color:rgb(4,163,233);padding-left:20px;">Tournament Start Date: (dd/mm/yyyy)</td>
							</tr>
							<tr>
								<td style="border-right:0px;">
									<asp:TextBox id="txtTournamentStartDate" Text="e.g. 23/09/1998" runat="server" width="100%" MaxLength="21" class="textBox_Atul" />
								</td> 
							</tr>
							<tr>
								<td style="border-right:0px;color:rgb(4,163,233);padding-left:20px;">Tournament End Date: (dd/mm/yyyy)</td>
							</tr>
							<tr>
								<td style="border-right:0px;">
									<asp:TextBox id="txtTournamentEndDate" Text="e.g. 23/09/1998" runat="server" width="100%" MaxLength="21" class="textBox_Atul" />
								</td> 
							</tr>
							<tr>
								<td style="border-right:0px;color:rgb(4,163,233);padding-left:20px;">Tournament Point Of Contact Names:</td>
							</tr>
							<tr>
								<td style="border-right:0px;">
									<asp:TextBox id="txtTournamentPOCNames" Text="" runat="server" width="100%" MaxLength="200" class="textBox_Atul" />
								</td> 
							</tr>
							<tr>
								<td style="border-right:0px;color:rgb(4,163,233);padding-left:20px;">Tournament Point Of Contact Nos:</td>
							</tr>
							<tr>
								<td style="border-right:0px;">
									<asp:TextBox id="txtTournamentPOCContacts" Text="" runat="server" width="100%" MaxLength="200" class="textBox_Atul" />
								</td> 
							</tr>
							<tr>
								<td style="border-right:0px;color:rgb(4,163,233);padding-left:20px;">Tournament Events:</td>								 
							</tr>
							<tr>							
								<td style="border-right:0px;padding-left:20px;">
									<asp:CheckBoxList ID="cblEvents" repeatDirection="Horizontal" RepeatColumns="4" runat="server" width="100%" Autopostback="false">       
	
	        						</asp:CheckBoxList> 
								</td>
							</tr>
						</table>
						
						<div style="text-align:center;padding-top:20px;">
							<asp:Button id="btnTournamentDetailSave" runat="server" text="Save" OnClick="btnTournamentDetailSave_Click" width="50%" class="button_Atul" />
						</div>
					</asp:Panel>
					
					<!-- Additional Details (Optional) -->					
					<div class="topPaddingBetweenObjects_Atul">					
						<asp:Button id="btnToggleTournamentAdditionalDetails" Text="Step 2: Additional Details" runat="server" OnClick="btnToggleTournamentAdditionalDetails_Click" style="background:rgb(220,220,220);color:rgb(4,163,233);text-align:left; width:80%;" class="toggleButton_Atul"/>
						<asp:Button id="btnToggleTournamentAdditionalDetailsPlus" Text="+" runat="server" OnClick="btnToggleTournamentAdditionalDetails_Click" style="background:rgb(220,220,220);color:rgb(251,85,58);float:right; width:20%;height:58px;border:none;font: bold 30pt Calibri;" />
					</div>
					
					<asp:Panel runat="server" id="pnlTournamentAdditionalDetails" width="100%" visible="false" class="panelRegistration_Atul">
						<table border="0" align="top" width="90%" cellspacing="0px" cellpadding="0px">								
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
						<asp:Button id="btnToggleTournamentEventSetup" Text="Step 3: Event Setup" runat="server" OnClick="btnToggleTournamentEventSetup_Click" style="background:rgb(220,220,220);color:rgb(4,163,233);text-align:left; width:80%;" class="toggleButton_Atul"/>
						<asp:Button id="btnToggleTournamentEventSetupPlus" Text="+" runat="server" OnClick="btnToggleTournamentEventSetup_Click" style="background:rgb(220,220,220);color:rgb(251,85,58);float:right; width:20%;height:58px;border:none;font: bold 30pt Calibri;" />
					</div>
					
					<asp:Panel runat="server" id="pnlTournamentEventSetup" width="100%" visible="false" class="panelRegistration_Atul">
						<asp:DataGrid ID="dgTournamentEvent" width="95%" runat="server" PageSize="1" AllowPaging="False"
								AutoGenerateColumns="False" CellPadding="0" cellspacing="0"  GridLines="None">  
								<Columns> 
									<asp:TemplateColumn HeaderText="Event" ItemStyle-Width="15%" >
										<ItemTemplate>						
											<asp:Label ID="dgtbEventCode" runat="server" Text='<%#Bind("strEventCode") %>' class="h4Text_Atul"></asp:Label>
		      							</ItemTemplate>	
			 						</asp:TemplateColumn>
									
									<asp:TemplateColumn HeaderText="Gender" ItemStyle-Width="5%">
										<ItemTemplate>						
											<asp:Label ID="dgtbGender" runat="server" Text='<%#Bind("strGender") %>' class="h4Text_Atul"></asp:Label>
		      							</ItemTemplate>	
			 						
			 						</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Date Reference" ItemStyle-Width="25%">
										<ItemTemplate>															
											<asp:TextBox ID="dgtbDOBRefernce" runat="server" width="100%" Text='<%#Bind("dlReferenceDate") %>' style="margin-left:0px;" class="textBox_Atul" ></asp:TextBox>		      								
		      							</ItemTemplate>	
			 						</asp:TemplateColumn>
			 						<asp:TemplateColumn HeaderText="Born(Before/After)" ItemStyle-Width="30%">
										<ItemTemplate>															
											<asp:RadioButtonList id="rblBeforeAfter"
										     AutoPostBack="False"
										     RepeatDirection="Horizontal"
										     RepeatLayout="Flow"
										     TextAlign="Right"
										     
										     runat="server">
										
										   <asp:ListItem Text="After" 
										        Value="After" />
											<asp:ListItem Text="Before" 
										        Value="Before" />
										</asp:RadioButtonList>
												      								
		      							</ItemTemplate>	
			 						</asp:TemplateColumn>
			 						<asp:TemplateColumn HeaderText="Fees" ItemStyle-Width="15%">
										<ItemTemplate>															
											<asp:TextBox ID="dgtbFee" runat="server" width="100%" Text='<%#Bind("iEventRateCard") %>' style="margin-left:0px;" class="textBox_Atul" ></asp:TextBox>		      								
		      							</ItemTemplate>	
			 						</asp:TemplateColumn>
			 						
								</Columns>  
								
								<FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="Black" />   
								
								<SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />  
								
								<PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" Mode="NumericPages" />  
								
								<AlternatingItemStyle BackColor="#F5F5F5" />  
								
								<ItemStyle BackColor="#FFFFFF" ForeColor="#666666" Height="35px" />  
				
								<HeaderStyle height="15px" BackColor="#F5F5F5" Font-Bold="True" ForeColor="#555555" />  
								
							</asp:DataGrid>
							
							<div style="text-align:center;padding-top:20px;">							
								<asp:Button id="btnUpdateEvent" runat="server" text="Save" OnClick="btnUpdateEvent_Click" width="50%" class="button_Atul" />
							</div> 
					</asp:Panel>
					
					</div>
					
					
					<div style="text-align:center;padding-top:20px;">
						<asp:Button id="btnSetupTournament" Text="SUBMIT" runat="server" OnClick="btnSetupTournament_Click" class="button_Atul" />
					</div>
					
				</asp:Panel>
			</div>
			
		</asp:Panel>	
		
	</div>
	
	<div style=' padding-top:6px;'>
			<hr/>
	</div>
	
	
		

</asp:Content>
