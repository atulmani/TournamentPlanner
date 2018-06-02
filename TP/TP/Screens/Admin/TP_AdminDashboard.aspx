<%@ Page Title="" Language="C#" MasterPageFile="~/Screens/TP.Master" AutoEventWireup="true" CodeBehind="TP_AdminDashboard.aspx.cs" Inherits="TournamentPlanner.TP_AdminDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   	<asp:ScriptManager ID="scriptmanager1" runat="server">

	</asp:ScriptManager>
	
	<!-- Main Body -->
	<div style=' padding-top:2px; width:100%;'>
		<div class="form_Atul">	
            <div style='height:35px; padding-top:5px; width:100%;color:White; background-color:rgb(4,163,233);text-align:center;' class="h1BoldText_Atul"'>
                ADMIN DASHBOARD
            </div>    
			<asp:Panel runat="server" id="pnlQuickAccess" visible="true" class="panelRegistration_Atul" >
				<table border="0" cellspacing="20px" cellpadding="20px" width="100%" >
					<tr>
						<td  style="text-align:center;border-right:0px;">
							<asp:Button ID="btnCreateTournamentView" Text="CREATE TOURNAMENT" runat="server" OnClick="btnCreateTournamentView_Click" width="100%" height="150px" class="button_Atul"/>
						</td>
                    </tr>
                    <tr>
						<td  style="text-align:center;border-right:0px;">
							<asp:Button ID="btnTPDashboardView" Text="TOURNAMENT DASHBOARD" runat="server" OnClick="btnTPDashboardView_Click"  width="100%" height="150px" class="button_Atul" />
						</td>				
					</tr>
				</table>
			</asp:Panel>			
		</div>
		
		<div class="form_Atul">
			<asp:Panel runat="server" id="pnlCreateTournament" visible="false" class="panelRegistration_Atul" >
					<div style='height:35px; padding-top:5px; width:100%;color:White; background-color:rgb(4,163,233);text-align:center;' class="h1BoldText_Atul"'>
                        TOURNAMENT FORM
                    </div>
                    <div style="color:red;padding-top:8px;text-align:center;" class="h4Text_Atul">
						<asp:Label id="lblErrorMsg" runat="server" Text=""  ></asp:Label>
					</div>
					<table width="100%" border="0" cellpadding="0px" cellspacing="0px">
						<tr>
							<td valign="top" style="border-right:0px;">
								<table border="0" width="100%">																		
									<tr>
										<td style="border-right:0px;padding-left:20px; padding-top:20px" class="h3BoldText_Atul">Sports</td> 
									</tr>
									<tr>
										<td style="border-right:0px;">								
											<asp:DropDownList ID="ddlSports" runat="server" width="90%" class="dropdown_Atul">
								                <asp:ListItem Text="Select Sport" Value="0"></asp:ListItem>
								                <asp:ListItem Text="Badminton" Value="BD"></asp:ListItem>					                
						            		</asp:DropDownList>
										</td>
									</tr>
									<tr>
										<td style="border-right:0px;padding-left:20px; padding-top:20px" class="h3BoldText_Atul">Tournament Name</td> 
									</tr>
									<tr>
										<td style="border-right:0px;">
											<asp:TextBox id="txtTournamentName"  runat="server" width="90%" MaxLength="100" class="textBox_Atul" />
										</td> 
									</tr>
									<tr>
										<td style="border-right:0px;padding-left:20px; padding-top:20px" class="h3BoldText_Atul">Organization</td>										 
									</tr>
									<tr>
										<td style="border-right:0px;">
											<asp:TextBox id="txtTournamentOrganisation" runat="server" width="90%" MaxLength="200" class="textBox_Atul" />
										</td>
									</tr>									
									<tr>
										<td style="border-right:0px;padding-left:20px; padding-top:20px" class="h3BoldText_Atul">Venue</td>										 
									</tr>
									<tr>
										<td style="border-right:0px;">
											<asp:TextBox id="txtTournamentVenue" Text="" runat="server" width="90%" MaxLength="100" class="textBox_Atul" />
										</td> 
									</tr>									
									<tr>
										<td style="border-right:0px;padding-left:20px; padding-top:20px" class="h3BoldText_Atul">Owner Name</td>										 
									</tr>
									<tr>
										<td style="border-right:0px;">
											<asp:TextBox id="txtTournamentOwnerName" Text="" runat="server" width="90%" MaxLength="30" class="textBox_Atul" />
										</td> 
									</tr>									
									<tr>
										<td style="border-right:0px;padding-left:20px; padding-top:20px" class="h3BoldText_Atul">Owner Contact No</td>										 
									</tr>
									<tr>
										<td style="border-right:0px;">
											<asp:TextBox id="txtTournamentOwnerContactNo" Text="" runat="server" width="90%" MaxLength="10" TextMode="Number" class="textBox_Atul" />
										</td> 
									</tr>									
									<tr>
										<td style="border-right:0px;padding-left:20px; padding-top:20px" class="h3BoldText_Atul">Email ID</td>										 
									</tr>
                                    <tr>
										<td style="border-right:0px;">
											<asp:TextBox id="txtTournamentOwnerEmailID" Text="" runat="server" width="90%" MaxLength="60" class="textBox_Atul" />
										</td> 
									</tr>
                                    <tr>
										<td style="border-right:0px;padding-left:20px; padding-top:20px" class="h3BoldText_Atul">Owner ID Type</td>										 
									</tr>
									<tr>
										<td style="border-right:0px;">								
											<asp:DropDownList ID="ddlTournamentOwnerIDType" runat="server" width="90%" class="dropdown_Atul">
								                <asp:ListItem Text="Select ID Type" Value="0"></asp:ListItem>
								                <asp:ListItem Text="PAN" Value="1"></asp:ListItem>
								                <asp:ListItem Text="AADHAAR" Value="2"></asp:ListItem>
								                <asp:ListItem Text="Driving License" Value="2"></asp:ListItem>
						            		</asp:DropDownList>
										</td> 
									</tr>
									<tr>
										<td style="border-right:0px;padding-left:20px; padding-top:20px" class="h3BoldText_Atul">Owner ID No</td>										 
									</tr>
									<tr>
										<td style="border-right:0px;">
											<asp:TextBox id="txtTournamentOwnerIDNo" Text="" runat="server" width="90%" MaxLength="50" class="textBox_Atul" />
										</td> 
									</tr>									
									<tr>
										<td style="border-right:0px;padding-left:20px; padding-top:20px" class="h3BoldText_Atul">Owner Address</td>										 
									</tr>
									<tr>
										<td style="border-right:0px;">
											<asp:TextBox id="txtTournamentOwnerAddress" Text="" runat="server" width="90%" MaxLength="200" class="textBox_Atul" />
										</td> 
									</tr>
								</table>			
							</td>
							
						</tr>
					</table>
			
					<table width="100%">
						<tr>
							<td style="border-right:0px; padding-top:20px;text-align:center;">
								<asp:Button id="btnCreateTournament" Text="Create Tournament" runat="server" OnClick="btnCreateTournament_Click" class="button_Atul" />
							</td>
						</tr>
					</table>		
				
			</asp:Panel>
		</div>
		
	</div>
	
	<div style=' padding-top:6px;'>
			<hr/>
	</div>
	
</asp:Content>
