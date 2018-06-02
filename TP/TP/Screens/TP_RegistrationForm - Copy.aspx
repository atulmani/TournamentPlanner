<%@ Page Title="" Language="C#" MasterPageFile="~/Screens/TournamentPlanner.Master" AutoEventWireup="true" CodeBehind="TP_RegistrationForm.aspx.cs" Inherits="TournamentPlanner.TP_RegistrationForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<style type="text/css">
			
			.hide {
				width:100%;
				border:0px solid #000;				
				max-height:99em;
				opacity:1;
				height:auto;
				overflow:hidden;
				transition:opacity 0.4s linear, max-height 0.4s linear;
			}
			.hide p {
				padding:10px;
				margin:0
			}
			input[type=checkbox]:checked + div {
				opacity:0;
				max-height:0;
				border:none;
			}		
		</style>



<script type="text/javascript" language="javascript">

function Validate(sender, args) {

        var gridView = document.getElementById("dgEventParticipation"); 

        var checkBoxes = gridView.getElementsByTagName("input");

        for (var i = 0; i < checkBoxes.length; i++) {

            if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked) {

          //      args.IsValid = true;
			alert("test 1")
          //      return;

            }

        }
        alert("test 2");

        //args.IsValid = false;

    }

function myFunction() {
		    var x = document.getElementById('test');
		    if (x.style.display === 'none') {
		        x.style.display = 'block';
		    } else {
		        x.style.display = 'none';
		    }
		}
		
		function show() {
	        if(document.getElementById('benefits').style.display=='none') {
	          document.getElementById('benefits').style.display='block';
	         else
	         	document.getElementById('benefits').style.display='none';
	        }
    	}
    	
    	function close() {
	        if(document.getElementById('benefits').style.display=='block') {
	          document.getElementById('benefits').style.display='none';
	        }
    	} 
    	
    	 $(".form_datetime").datetimepicker({format: 'yyyy-mm-dd hh:ii'});
    	

	</script>
	

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:ScriptManager ID="scriptmanager1" runat="server">

</asp:ScriptManager>

   	<div>
		<table border="0" cellspacing="10px" cellpadding="50px" align="top" text-align="top" style='width:100%; height:100px; background:rgb(235,235,235);'>
			<tr>
				<td style='min-width:35%; border-right:0px;'>
					<table border="0" cellspacing="0" style='width:100%; height:100px; background:rgb(235,235,235);'>
						<tr style='color:rgb(100,100,100); font: bold 22pt Calibri;'>
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
								<asp:Label id="lbl23" Text=", Address: " runat="server" style='font: bold 16pt Calibri;'></asp:Label>
								<asp:Label id="lblLocationAddress" Text="" runat="server"></asp:Label>
								<asp:Label id="lbl24" Text=", Contacts: " runat="server" style='font: bold 16pt Calibri;'></asp:Label>
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
		<table border="0" cellspacing="0" style='font: 11pt Calibri; width:100%; height:25px; background:rgb(245,245,245);'>
			<tr>
				<td class="submenufont" style='text-align:center;'><a style='text-decoration:none;' href="../Screens/TP_BD_Home.aspx">Home</a></td>
				<td class="submenufont" style='text-align:center;'><a style='text-decoration:none;' href="../Screens/TP_Events.aspx">Events</a></td>				
				<td class="submenufont" style='text-align:center;'><a style='text-decoration:none;' href="../Screens/TP_Players.aspx">Players</a></td>
				<td class="submenufont" style='text-align:center;'><a style='text-decoration:none;' href="../Screens/TP_Draws.aspx">Draws</a></td>
				<td class="submenufont" style='text-align:center;'><a style='text-decoration:none;' href="../Screens/TP_Matches.aspx">Matches</a></td>				
				<td class="submenufont" style='text-align:center;'><a style='text-decoration:none;' href="../Screens/TP_Winners.aspx">Winners</a></td>
				<td class="submenufont" style='text-align:center; border-right:0px;'><a style='text-decoration:none;' href="../Screens/TP_Gallery.aspx">Gallery</a></td>
			</tr>
		</table>

	</div>

	<!-- Main Body -->
	<asp:UpdatePanel ID="updatepnl" runat="server">
			<ContentTemplate>
	<div style=' padding-top:2px; min-height:400px; width:100%; background:rgb(256,256,256);'>		
		<table align="top" border="0" cellspacing="1" width="100%" style='padding-bottom:2px;'>
			<tr style='height:10px;'>
				<td colspan="2" style='border-right:0px; font: bold 22pt Arial;border-right:0px;background:rgb(231,231,215);'>Registration Form</td>
			</tr>			
		</table>
		
		<!-- Show Error Messages -->
		<asp:Panel runat="server" id="pnlErrorMsg" visible="false" height="20px" style='text-align:center; background:rgb(250,250,250);color:red;font:bold 16pt Calibri;'>
			<asp:Label id="lblErrorMsg" runat="server" Text=""  ></asp:Label>
		</asp:Panel>
		
		<!-- Player Details -->		
			<div style="padding-top:8px;">
				<asp:Button id="btnTogglePlayerDetails" Text="+ Fill Player Details" runat="server" OnClick="btnTogglePlayerDetails_Click" width="100%" height="56px" style="background:rgb(225,225,225);color:rgb(10,10,10);border:none;font: bold 24pt Calibri" />
							
				<asp:Panel runat="server" id="pnlPlayerDetails" visible="false" style='background:rgb(250,250,250);font:bold 20pt Calibri;'>							
							<table border="0" align="top" width="100%" cellpadding="0px" cellspacing="0px" >
									<tr>
										<td style="border-right:0px;">First Name:</td>										 
									</tr>
									<tr>
										<td style="border-right:0px;">
											<asp:TextBox ID="txtFirstName" runat="server" width="100%" MaxLength="16" CssClass="underlined"></asp:TextBox>								
										</td>
									</tr>									
									<tr>
										<td style="border-right:0px;padding-top:40px;">Last Name:</td>										 
									</tr>
									<tr>
										<td style="border-right:0px;">
											<asp:TextBox id="txtLastName"  runat="server" width="100%" MaxLength="16" CssClass="underlined" />
										</td>
									</tr>
									<tr>
										<td style="border-right:0px;padding-top:40px;">Player Gender:</td>										 
									</tr>
									<tr>
										<td style="border-right:0px;">
											<asp:DropDownList ID="ddlGender" runat="server" width="100%" height="66px" style='font: 16pt Calibri;'>
								                <asp:ListItem Text="Select Gender" Value="0"></asp:ListItem>
								                <asp:ListItem Text="Male" Value="M"></asp:ListItem>
								                <asp:ListItem Text="Female" Value="F"></asp:ListItem>
						            		</asp:DropDownList>					
										</td>
									</tr>
									<tr>
										<td style="border-right:0px;padding-top:40px;">Player Birthdate: (dd/mm/yyyy)</td>										
									</tr>
									<tr>
										<td style="border-right:0px;">
											<asp:TextBox id="txtBirthdate" Text="" runat="server" width="100%"  MaxLength="16" CssClass="underlined"  />
											<asp:CustomValidator runat="server" ControlToValidate="txtBirthdate" ErrorMessage="Date format should be dd/mm/yyyy" OnServerValidate="CustomValidator1_ServerValidate" style="color:red" />
										</td>
									</tr>
									<tr>
										<td style="border-right:0px;padding-top:40px;">Player Contact:</td>										 
									</tr>
									<tr>
										<td style="border-right:0px;">
											<asp:TextBox id="txtContact" Text="" runat="server" width="100%" MaxLength="32" CssClass="underlined" />
										</td>
									</tr>									
									<tr>
										<td style="border-right:0px;padding-top:40px;">Player Email ID: </td>										 
									</tr>
									<tr>
										<td style="border-right:0px;">
											<asp:TextBox id="txtEmailID" Text="e.g. xyz@gmail.com" runat="server" width="100%" MaxLength="50" CssClass="underlined" />
										</td>
									</tr>
									
									<tr>
										<td style="border-right:0px;padding-top:40px;">Player State:</td>														
									</tr>
									<tr>
										<td style="border-right:0px;">
											<asp:DropDownList ID="ddlStates" runat="server" width="100%" height="66px" style='font: 20pt Calibri;'>
								                <asp:ListItem Text="Maharashtra" Value="1"></asp:ListItem>		                
						            		</asp:DropDownList>					
										</td>
									</tr>
									<tr>
										<td style="border-right:0px;padding-top:40px;">Player District:</td>														
									</tr>
									<tr>
										<td style="border-right:0px;">
											<asp:DropDownList ID="ddlDistricts" runat="server" width="100%" height="66px" style='font: 20pt Calibri;'>
								                <asp:ListItem Text="Pune" Value="1"></asp:ListItem>								                                
						            		</asp:DropDownList>					
										</td>
									</tr>
									<tr>
										<td style="border-right:0px;padding-top:40px;">Player City:</td>										 
									</tr>
									<tr>
										<td style="border-right:0px;">
											<asp:TextBox id="txtCity" Text="Pune" runat="server" width="100%" MaxLength="20" readonly=true CssClass="underlined"/>
										</td>
									</tr>
									<tr>
										<td style="border-right:0px;padding-top:40px;">Player Address: </td>										
									</tr>											
									<tr>
										<td style="border-right:0px;">
											<asp:TextBox id="txtAddress" Text="" runat="server" width="100%" MaxLength="140" CssClass="underlined"/>
										</td> 
									</tr>
								</table>	
						</asp:Panel>						
			</div>						
		
		<!-- Event Details -->
		<div style="padding-top:8px;">
			
			<asp:Button id="btnToggleAddEvent" Text="+ Select Events" runat="server" OnClick="btnToggleAddEvent_Click" width="100%" height="56px" style="background:rgb(225,225,225);color:rgb(10,10,10);border:none;font: bold 24pt Calibri" />
			<asp:Panel runat="server" id="pnlAddEvent" visible="false" style='background:rgb(250,250,250);font:bold 20pt Calibri;'>
				<table width="100%" border="0">
					<tr>
						<td valign="top" style="border-right:0px;">
							<table border="0" align="top" width="100%"  style="display:none">						
								<tr>
									<td style="border-right:0px;width:16%;">Tournament Event:</td>									 
								</tr>
								<tr>
									<td style="border-right:0px;">
										<asp:DropDownList ID="ddlEvents" runat="server" width="100%" height="56px" style='font: 16pt Calibri;'
											OnSelectedIndexChanged="ddlEvents_SelectedIndexChanged" AutoPostBack="true">							                
					            		</asp:DropDownList>					
									</td>
								</tr>
							</table>
							
							<asp:Panel id="pnlPartnerDetails" runat = "server" visible="false" >
								<table width="100%">				
									<tr>
										<td style="border-right:0px;width:30%;">Partner First Name: *</td>
										<td style="border-right:0px;">
											<asp:TextBox id="txtPartnerFName" Text="" runat="server" width="100%" height="36px" MaxLength="16"/>
										</td>
									</tr>
									<tr>
										<td style="border-right:0px;">Partner Last Name:</td>
										<td style="border-right:0px;">
											<asp:TextBox id="txtPartnerLName" Text="" runat="server" width="100%" height="36px" MaxLength="16"/>
										</td>
									</tr>
									<tr>
										<td style="border-right:0px;">Partner Date of Birth:</td>
										<td style="border-right:0px;">
											<asp:TextBox id="txtPartnerDOB" Text="" runat="server" width="100%" height="36px" MaxLength="16"/>
										</td>
									</tr>
								</table>
							</asp:Panel>
							<table border="0" align="top" width="100%" style="display:none">
								<tr>
									<th style="width:30%; border-right:0px;"></th>
									<td style="border-right:0px; padding-top:0px;">
										<asp:Button id="btnAddEvent" Text="Add Event" runat="server" OnClick="btnAddEvent_Click" width="150px" height="46px" style="background:rgb(146,215,10);color:rgb(10,10,10);border:none;font: bold 16pt Calibri" />
									</td> 
								</tr>
							</table>
							
							<table border="0" align="top" width="100%" style="padding-top:40px;">								
								<tr>
									<th style="border-right:0px;">Tournament Events</th>									
								</tr>
								<tr>
									<td style="border-right:0px;" colspan="2">
							            <asp:DataGrid ID="dgEventParticipation" width="100%" runat="server" PageSize="1" AllowPaging="False"
										AutoGenerateColumns="False" CellPadding="0" cellspacing="10"  ForeColor="#333333" GridLines="None"
										OnDeleteCommand="dgEventParticipation_DeleteCommand">  
										<Columns> 
											<asp:BoundColumn HeaderText="Sr#" DataField="ID"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="Event" ItemStyle-Width="25%" >
												<ItemTemplate>						
													<asp:Label ID="dgtbEventCode" runat="server" width="100%" Text='<%# Bind("Event") %>' ></asp:Label>
				      							</ItemTemplate>	
					 						</asp:TemplateColumn>
											
											<asp:TemplateColumn HeaderText="Partner Name" ItemStyle-Width="25%">
												<ItemTemplate>						
													<asp:TextBox ID="dgtbPartnerName" runat="server" Text="" width="100%" height="30px" CssClass="underlined"></asp:TextBox>
				      							</ItemTemplate>	
					 						</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Partner Birthdate" ItemStyle-Width="20%">
												<ItemTemplate>						
													<asp:TextBox ID="dgtbPartnerDOB" runat="server" Text="dd/mm/yyyy" width="100%" height="30px" CssClass="underlined"></asp:TextBox>													
				      								<asp:HiddenField id="hfFees" runat="server" Value='<%# Bind("Fees") %>' Visible="false" ></asp:HiddenField>
				      						</ItemTemplate>	
					 						</asp:TemplateColumn>
					 						
					 						
											<asp:BoundColumn HeaderText="Fees"  DataField="Fees"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="Select" ItemStyle-Width="5%">
												<ItemTemplate>						
													<asp:CheckBox ID="dgcbEventSelect" runat="server" OnCheckedChanged="chkview_CheckedChanged" AutoPostBack=true CssClass="mycheckbox"></asp:CheckBox>
				      							</ItemTemplate>	
					 						</asp:TemplateColumn>
											<asp:ButtonColumn HeaderText="Delete Entry" Text="[Remove]" CommandName="Delete" visible="false"></asp:ButtonColumn>											
										</Columns>  
										
										<FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />  
										
										<SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />  
										
										<PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" Mode="NumericPages" />  
										
										<AlternatingItemStyle BackColor="White" />  
										
										<ItemStyle BackColor="#F8F8F8" ForeColor="#333333" />  
										
										<HeaderStyle BackColor="#E0E0E0" Font-Bold="True" ForeColor="#0A0A0A" />  
										
										</asp:DataGrid>
									</td>
								</tr>
							</table>
							<hr/>
							<table border="0" align="top" width="100%" style="padding-top:20px">								
								<tr>
									<td style="border-right:0px;width:50%;">
										<asp:CheckBox id="chkbDistrictReg" runat="server" width="100%" Text=" District Registration Fees(30 Rs.)" OnCheckedChanged="chkbDistrictReg_CheckedChanged" autopostback="true" CssClass="mycheckbox"></asp:CheckBox>
									</td>
									<th style="border-right:0px;width:230px;">Total Amount: </th>
									<td style="border-right:0px;">
										Rs.
										<asp:Label id="lblTotalAmount" runat="server" Text="0" width="200px"></asp:Label>
									</td>
								</tr>				
							</table>
						</td>
					</tr>
				</table>
			</asp:Panel>
		
		</div>
		
		<table width="100%">
			<tr>
				<td style="border-right:0px; padding-top:20px;">
					<asp:Button id="btnRegisterPay" Text="Register and Pay" runat="server" OnClick="btnRegisterPay_Click" width="250px" height="66px" style="background:rgb(146,215,10);color:rgb(10,10,10);border:none;font: bold 20pt Calibri" />
				</td>
			</tr>
		</table>
						
	</div>
	
		</ContentTemplate>
	</asp:UpdatePanel>

<div>
	<hr/>
</div>
<div style='width:100%; text-align:center;padding-top:10px; padding-bottom:20px; font: 10pt calibri; color:gray;background:rgb(235,235,235);'>
	----------------------------------------------      Advertisement      --------------------------------------------
</div>



</asp:Content>
