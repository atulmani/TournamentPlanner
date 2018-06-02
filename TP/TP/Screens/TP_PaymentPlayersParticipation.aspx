<%@ Page Title="" Language="C#" MasterPageFile="~/screens/TP.Master" AutoEventWireup="true" CodeBehind="TP_PlayerPlayersParticipation.aspx.cs" Inherits="TournamentPlanner.TP_PaymentPlayersParticipation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   	
   	<asp:ScriptManager ID="scriptmanager1" runat="server">

	</asp:ScriptManager>

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
					<asp:Label id="lbl23" Text=", Address: " runat="server" class="h4BoldText_Atul"></asp:Label>
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
	<div style=' padding-top:2px; min-height:400px; width:100%; background:rgb(256,256,256);' >
		<div class="pageTopic">
			Player Participation
		</div>
		<!-- Player List -->
		<div>			
			
			
			<div style=' background:rgb(256,256,256);text-align:top;padding-top:10px; ' class="h4Text_Atul">
			<asp:DataGrid ID="dgPlayerParticilationList" width="100%" runat="server" PageSize="1" AllowPaging="False"
				AutoGenerateColumns="False" GridLines="None" >  
				<Columns>					
					<asp:BoundColumn HeaderText="Player Name" DataField="PlayerFullName" DataFormatString="{0:MMM-d-yyy}" ItemStyle-Width="10%"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Event" DataField="EventCode" DataFormatString="{0:MMM-d-yyy}" ItemStyle-Width="10%"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Parter Name" DataField="PartnerPlayerCode" ItemStyle-Width="20%"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Fees" DataField="Fees" ItemStyle-Width="20%"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Payment Status" DataField="PaymentStatus" ItemStyle-Width="20%"></asp:BoundColumn>
				</Columns>  
				
				<FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="Black" />  
				
				<PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" Mode="NumericPages" />  
				
				<AlternatingItemStyle BackColor="#F5F5F5" />  
				
				<ItemStyle BackColor="#FFFFFF" ForeColor="#666666" Height="35px" />  
				
				<HeaderStyle height="15px" BackColor="#F5F5F5" Font-Bold="True" ForeColor="#555555" /> 
			
			</asp:DataGrid>
			</div>			
		</div>
		<hr/>
		
		<div class="h3Text_Atul" style="text-align:center;">
				Please check your email: 
				<u><asp:Label id="lblPlayerEmail" Text="" runat="server" style='font-weight: bold;color:blue;'></asp:Label></u>
				for the details of your participation.
		</div>
		

        <div>
            <asp:Panel runat="server" id="pnlNONPaymentGatewayOption">
                <hr />
                <div class="h3Text_Atul" style="text-align:center;">
				
                    Please do your payment through <b>PayTM to Priyanshi Khare @ 9130509674 </b> and don't forget to get the acknowledgement from her.
                    <br />
                    <br />
                	Please complete the <b>PAYMENT</b> process to <b>CONFIRM</b> your registration for the 
					<b><asp:Label id="Label1" Text=" Tournament " runat="server"></asp:Label></b>
				. 
				</div>
            </asp:Panel>
        </div>
                
		<div style="display:none;">
			<asp:Panel runat="server" id="pnlPaymentInformation">
		 		<div class="h3Text_Atul">
					Please complete the <b>PAYMENT</b> process to <b>CONFIRM</b> your registration for the 
					<b><asp:Label id="lblTournamentName2" Text=" Tournament " runat="server"></asp:Label></b>
				.
				</div>						
		
				<!-- Upload Indentity Proof -->		
				<hr/>		
				<asp:Panel runat="server" id="pnlDocTypeMsg" visible="false" width="100%" class="panelRegistration_Atul">					
					<asp:Label id="lblDocTypeMsg" runat="server" text="Upload the Govt ID Proof" />		
				</asp:Panel>
		
				<asp:Panel runat="server" id="pnlDocType" visible="false" width="100%" class="panelRegistration_Atul">					
				<div style="text-align:center;" class="h3Text_Atul">		
					<asp:UpdatePanel ID="UpdatePanel1" runat="server">
	    				<ContentTemplate>
							<asp:Label id="lblDocType" runat="server" text="Upload the Govt ID Proof" />
						
							<br /><br />
						    <asp:Label id="lblSelectDocType" runat="server" text="Select Document Type" />
						
							<asp:DropDownList ID="ddlDocType" runat="server" width="40%" class="dropdown_Atul">			                
				                <asp:ListItem Text="PAN Card" Value="PAN Card"></asp:ListItem>
				                <asp:ListItem Text="Aadhar Card" Value="Aadhar Card"></asp:ListItem>
				                <asp:ListItem Text="Driving Licence" Value="Driving Licence"></asp:ListItem>
				                <asp:ListItem Text="Passport Copy" Value="Passport Copy"></asp:ListItem>
				                <asp:ListItem Text="Company ID" Value="Company ID"></asp:ListItem>
		        	   		</asp:DropDownList>					
		
						    <br /><br />
						    <div style="text-align:center;padding-left:50px;">
								<asp:FileUpload id="FileUploadControl" runat="server"/>
						    </div>
					    
						    <br /><br/>
						    <asp:Button runat="server" id="btnUploadButton" text="Upload Document" onclick="UploadButton_Click" class="button_Atul" width="250px" />
					    
					    <br /><br />
					    
						    <div style="height:auto;width:100%;">
						    	<asp:HiddenField id="hfContentType" runat="server" />
						    	<asp:Image id="imgProof" runat="server" style="width:30%;height:30%;"  />
						    </div>				    
			    			<asp:Label runat="server" id="lblStatusLabel" text="Upload status: " style="color:red;"   visible="false" />
					 	</ContentTemplate>
						<Triggers>
		    			    <asp:PostBackTrigger ControlID = "btnUploadButton" />
	    				</Triggers>			
					</asp:UpdatePanel>
				</div>
			</asp:Panel>
			
			</asp:Panel>
		</div>
		
		<!-- Agree Checkbox & Payment Button -->		
		<div style="text-align:center;display:none;">
			<hr/>
			<table border="0" width="100%" class="h3Text_Atul">
				<tr>
					<td width="90%" style="border-right:0px;text-align:center;">
						<asp:CheckBox id="chkbAgree" runat="server" Text="Agree & Pay" autopostback="true" OnCheckedChanged="chkbAgree_CheckedChanged" CssClass="mycheckbox"></asp:CheckBox>
					</td>
				</tr>
				<tr>
					<td>
						<asp:Label id="lblPleaseWaitMsg" runat="server" Text="Please Wait..." style="color:red;" visible="false" ></asp:Label>
					</td>
				</tr>
				<tr>
					<td style="border-right:0px;text-align:center;">						
						<asp:Button id="btnPayment" Text="Click Pay" runat="server" OnClick="btnPayment_Click" Enabled = "false" class="button_Atul" width="250px" />
					</td>
				</tr>
			</table>
		</div>
		
		<!-- <div style="font:bold 28px Calibri;color:red;">
			Please select "Agree & Pay" and then "Click to Pay" button to complete the Payment process.
		</div>
		 need to uncomment once payment is completed-->
				
		
		
			<asp:Panel runat="server" id="pnlErrorMsg" visible="false" height="20px" style='text-align:center;margin-top:20px;'  class="panelRegistration_Atul">
				<asp:Label id="lblErrorMsg" runat="server" Text="" style="color:red;"  ></asp:Label>
			</asp:Panel>
		
		<div class="h3Text_Atul">
			<asp:Label id="lblPartnerMessage" Text="Note: For doubles entry , player marked with '(*)' have done the registration, Payment to be done by player marked with '(*)'" runat="server" ></asp:Label>
		</div>
		
		<!-- Terms & Conditons -->
		
		<hr/>
		<div class="h3Text_Atul" style="text-align:center;">
			We request you to kindly go through the terms and conditions before participating in the tournament.
		</div>
		<br/>
		<div class="h3Text_Atul">
			<table width="100%">
				<tr>
					<td style="border-right:0px;" class="h2BoldText_Atul" >
						<u>Terms & Conditions</u>
					</td>
				</tr>
				<tr>
					<td style="border-right:0px;">
						1. The tournament will be conducted as per the rules and regulations, guidelines, laws as laid by BWF & BAI.
					</td>
				</tr>	
				<tr>
					<td style="border-right:0px;">
						2. Minimum 8 entries are required for conducting the event. In case of lesser number of entries, the event will be cancelled.
					</td>
				</tr>
				<tr>
					<td style="border-right:0px;">
						3. The decision of the tournament committee will be final and binding for all players.
					</td>
				</tr>
				<tr>
					<td style="border-right:0px;">
						4. Entries must be on the specified form only via online media.
					</td>
				</tr>
				<tr>
					<td style="border-right:0px;">
						5. Name of the partner must be disclosed for doubles otherwise the entry will not be accepted. 
					</td>
				</tr>
				<tr>
					<td style="border-right:0px;">
						6. The Tournament Committee reserves the right of refusing any entry without assigning any reason.
					</td>
				</tr>
				<tr>
					<td style="border-right:0px;">
						7.  Premium Quality Shuttlecocks will be used during the tournament.
					</td>
				</tr>				
				<tr>
					<td style="border-right:0px;">
						8. Outside shoes shall not be allowed on the playing surface.Players should carry their Badminton Shoes (PU Sole) in their bag.
					</td>
				</tr>
				<tr>
					<td style="border-right:0px;">
						9. Players are required to report at least 30 minutes prior to their scheduled match time.
					</td>
				</tr>
				<tr>
					<td style="border-right:0px;">
						10. In case of any issue, please contact Tournament Organiser.
					</td>
				</tr>				
			</table>			
		</div>
		
		
		
	</div>

<div>
	<hr/>
</div>
<div style='width:100%; text-align:center;padding-top:16px; padding-bottom:20px; font: 14pt calibri; color:gray;background:rgb(235,235,235);'>
Advertisement
</div>

</asp:Content>
