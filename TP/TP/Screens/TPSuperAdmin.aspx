<%@ Page Title="" Language="C#" MasterPageFile="~/Screens/TournamentPlanner.Master" AutoEventWireup="true" CodeBehind="TPSuperAdmin.aspx.cs" Inherits="TournamentPlanner.TPSuperAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   	
	<!-- Main Body -->
	<div style=' padding-top:8px; min-height:400px; font: 10pt Calibri; width:100%; background:rgb(256,256,256);'>
	
	<!-- Show Error Messages -->
	<asp:Panel runat="server" id="pnlErrorMsg" visible="false" height="20px" style='text-align:center; background:rgb(250,250,250);color:red;font:bold 10pt Calibri;'>
		<asp:Label id="lblErrorMsg" runat="server" Text=""  ></asp:Label>
	</asp:Panel>
	
		<!-- Login Block -->
		<div>
			<asp:Panel runat="server" id="pnlLogin" visible="true" style='background:rgb(250,250,250);font: 20pt Calibri;height:auto; padding-bottom:100px;'>
				<table border="0" width="100%" cellpadding="0" cellspacing="0" >					
					<tr>
						<th style="border-right:0px;">User Name</th>						 
					</tr>
					<tr>
						<td style="border-right:0px;">
							<asp:TextBox id="txtUserCode"  runat="server" width="100%" MaxLength="50" CssClass="underlined"/>
						</td>
					</tr>
					<tr>
							<th style="border-right:0px;padding-top:35px;">Password:</th>							 
					</tr>
					<tr>
						<td style="border-right:0px;">
								<asp:TextBox id="txtOTPCode" TextMode="Password"  runat="server" width="100%" MaxLength="50" CssClass="underlined"/>
						</td>
					</tr>
					<!--<tr>
						<th style="border-right:0px;padding-top:35px;">Captcha: </th>						 
					</tr>
					<tr>
						<td style="border-right:0px;">
							<asp:TextBox id="txtCaptchaCode"  runat="server" width="100%" MaxLength="10" CssClass="underlined" />
						</td>
					</tr>-->
				</table>
				
				<table width="100%">
					<tr>
						<td style="border-right:0px; padding-top:25px;text-align:center;">
							<asp:Button id="btnLogin" Text="Login" runat="server" OnClick="btnLogin_Click" width="250px" height="66px" style="background:rgb(146,215,10);color:rgb(10,10,10);border:none;font: bold 20pt Calibri;align:center; text-align:center;" />							
						</td>
					</tr>
				</table>
			</asp:Panel>
		</div>
		
		<asp:Panel runat="server" id="pnlToggleCreateTournament" visible="false" style='background:rgb(250,250,250);font: 10pt Calibri;height:auto;'>		
			<asp:LinkButton id="btnToggleCreateTournament" Text="- Create Tournament" runat="server" OnClick="btnToggleCreateTournament_Click" width="100%" height="36px" style="text-align:center; background:rgb(240,240,240);color:rgb(10,10,10);border:none;font: bold 12pt Calibri" />
		</asp:Panel>
		
		<asp:Panel runat="server" id="pnlCreateTournament" visible="false" style='background:rgb(250,250,250);height:auto;'>
			<div style=' padding-top:20px; min-height:400px; font: 16pt Calibri; width:100%; background:rgb(256,256,256);'>
				<table align="top" border="0" cellspacing="1">
					<tr style='height:10px;'>
						<td colspan="2" style='border-right:0px; font: bold 20pt Calibri;border-right:0px;'>Create Tournament</td>
					</tr>			
				</table>
							
				<table width="100%" border="0" cellpadding="0px" cellspacing="0px">
					<tr>
						<td valign="top" style="border-right:0px;">
							<table border="0" align="top" width="100%">									
								<tr>
									<th style="border-right:0px;">Sports:</th>									 
								</tr>
								<tr>
									<td style="border-right:0px;">								
										<asp:DropDownList ID="ddlSports" runat="server" width="100%" height="66px" style='font: 16pt Calibri;'>
							                <asp:ListItem Text="Select Sport" Value="0"></asp:ListItem>
							                <asp:ListItem Text="Badminton" Value="BD"></asp:ListItem>					                
					            		</asp:DropDownList>
									</td>
								</tr>
								<tr>
									<th style="border-right:0px;padding-top:50px;">Tournament Name:</th>									
								</tr>
								<tr>
									<td style="border-right:0px;">
										<asp:TextBox id="txtTournamentName"  runat="server" width="100%" MaxLength="100" CssClass="underlined" />
									</td> 
								</tr>
								<tr>
									<th style="border-right:0px;padding-top:50px;">Tournament Organisation:</th>									 
								</tr>
								<tr>
									<td style="border-right:0px;">
										<asp:TextBox id="txtTournamentOrganisation" runat="server" width="100%" MaxLength="200" CssClass="underlined" />
									</td>
								</tr>
								<tr>
									<th style="border-right:0px;padding-top:50px;">Tournament Venue:</th>
								</tr>
								<tr>
									<td style="border-right:0px;">
										<asp:TextBox id="txtTournamentVenue" Text="" runat="server" width="100%" MaxLength="100" CssClass="underlined" />
									</td> 
								</tr>
								<tr>
									<th style="border-right:0px;padding-top:50px;">Tournament Owner Name:</th>
								</tr>
								<tr>
									<td style="border-right:0px;">
										<asp:TextBox id="txtTournamentOwnerName" Text="" runat="server" width="100%" MaxLength="100" CssClass="underlined" />
									</td> 
								</tr>
								<tr>
									<th style="border-right:0px;padding-top:50px;">Tournament Owner Contact No:</th>
								</tr>
								<tr>
									<td style="border-right:0px;">
										<asp:TextBox id="txtTournamentOwnerContactNo" Text="" runat="server" width="100%" MaxLength="50" CssClass="underlined" />
									</td> 
								</tr>
								<tr>
									<th style="border-right:0px;padding-top:50px;">Tournament Owner ID Type:</th>
								</tr>
								<tr>
									<td style="border-right:0px;">								
										<asp:DropDownList ID="ddlTournamentOwnerIDType" runat="server" width="100%" height="66px" style='font: 16pt Calibri;'>
							                <asp:ListItem Text="Select ID Type" Value="0"></asp:ListItem>
							                <asp:ListItem Text="PAN" Value="1"></asp:ListItem>
							                <asp:ListItem Text="AADHAAR" Value="2"></asp:ListItem>
							                <asp:ListItem Text="Driving License" Value="2"></asp:ListItem>
					            		</asp:DropDownList>
									</td> 
								</tr>
								<tr>
									<th style="border-right:0px;padding-top:50px;">Tournament Owner ID No:</th>
								</tr>
								<tr>
									<td style="border-right:0px;">
										<asp:TextBox id="txtTournamentOwnerIDNo" Text="" runat="server" width="100%" MaxLength="50" CssClass="underlined" />
									</td> 
								</tr>
								<tr>
									<th style="border-right:0px;padding-top:50px;">Tournament Owner Address:</th>
								</tr>
								<tr>
									<td style="border-right:0px;">
										<asp:TextBox id="txtTournamentOwnerAddress" Text="" runat="server" width="100%" MaxLength="200" CssClass="underlined" />
									</td> 
								</tr>
							</table>			
						</td>
						
					</tr>
				</table>
		
				<table width="100%">
					<tr>
						<td style="border-right:0px; padding-top:20px;text-align:center;">
							<asp:Button id="btnCreateTournament" Text="Create Tournament" runat="server" OnClick="btnCreateTournament_Click" width="250px" height="66px" style="background:rgb(146,215,10);color:rgb(10,10,10);border:none;font: bold 20pt Calibri;align:center; text-align:center;" />
						</td>
					</tr>
				</table>		
			</div>
		</asp:Panel>
		
		
		<div style="visibility: hidden;">
			<table>
				<tr>
					<th style="border-right:0px;">Tournament Events: *</th>					 
				</tr>
				<tr>
					
					<td colspan="2" style="border-right:0px;">
						<asp:CheckBoxList ID="cblEvents" repeatDirection="Horizontal" RepeatColumns="8" runat="server" width="100%" style="border-right:0px;" Autopostback="false">       

						</asp:CheckBoxList> 
					</td>
				</tr>
			</table>
		</div>

	</div>

<div>
	<hr/>
</div>
<div style='width:100%; text-align:center;padding-top:10px; padding-bottom:20px; font: 10pt calibri; color:gray;background:rgb(235,235,235);'>
	----------------------------------------------      Advertisement      --------------------------------------------
</div>

</asp:Content>
