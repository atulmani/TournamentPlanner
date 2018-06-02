<%@ Page Title="" Language="C#" MasterPageFile="~/Screens/TP.Master" AutoEventWireup="true" CodeBehind="TP_Dashboard.aspx.cs" Inherits="TournamentPlanner.TP_Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   	<asp:ScriptManager ID="scriptmanager1" runat="server">

	</asp:ScriptManager>
	
	<!-- Main Body -->
	<div style=' padding-top:2px; width:100%; background:rgb(256,256,256);'>
		<table border="0" cellspacing="1" width="100%" style='padding-bottom:2px;'>
			<tr style='height:10px;'>
				<td style='color:rgb(251,85,58);border-right:0px; border-right:0px;background:rgb(230,230,230);text-align:center;' class="h3BoldText_Atul">Dashboard</td>
			</tr>			
		</table>
				
		<asp:Panel runat="server" id="pnlAfterLogin" visible="true" class="panelRegistration_Atul">
			<!-- Section 1: Logged in Tournament Header -->
            <div class="form_Atul">
			<div style=' padding-top:6px; text-align:center;' class="h3Text_Atul">
				<table border="1" width="100%" style="text-align:left" cellpadding="4px">
					<tr>
						<td>
                            Tournament ID/Code:
							<asp:Label id="lblTournamentID" Text="Tournament Name"  runat="server" class="h1Text_Atul" Font-Bold=true/>
						</td>
					</tr>
                    <tr>
						<td>
                            Tournament Name:
							<asp:Label id="lblTournamentName" Text="Tournament Name"  runat="server" class="h2Text_Atul"/>
						</td>
					</tr>
					<tr>						
						<td>
                        Tournament Organisation:
							<asp:Label id="lblTournamentOrganisation" Text="Tournament Organisation"  runat="server" class="h2Text_Atul"/>
						</td>												
					</tr>
					<tr>						
						<td style="border-right:0px;">
                        Tournament Venue:
							<asp:Label id="lblTournamentVenue"  Text="Tournament Venue" runat="server" class="h2Text_Atul"/>
						</td>
					</tr>
					<tr>
						<td>					
						Owner Name:
							<asp:Label id="lblOwnerName" Text="Owner Name" runat="server" class="h2Text_Atul"/>
						</td>
					</tr>
					<tr>
						<td>
						Owner ID:
							<asp:Label id="lblOwnerID"  Text="Owner ID" runat="server" class="h2Text_Atul"/>
						</td>
					</tr>
					<tr>
						<td style="border-right:0px;">                        
						Owner Address:
							<asp:Label id="lblOwnerAddress" Text="Owner Address" runat="server" class="h2Text_Atul"/>
						</td>
					</tr>
				</table>				
			</div>
						
			<!-- Show Error Messages -->
			<asp:Panel runat="server" id="pnlErrorMsg" visible="false" height="20px" style='text-align:center; background:rgb(250,250,250);color:red;' class="h3BoldText_Atul">
				<asp:Label id="lblErrorMsg" runat="server" Text=""  ></asp:Label>
			</asp:Panel>
		
			<hr/>
			

			<asp:Panel runat="server" id="pnlTournamentSummary" style='text-align:center;'>
				<table border="0" width="100%" cellpadding="4px" class="h3Text_Atul" >
					<tr>
						<td style="border-right:0px;width:50%;">							
							<asp:Label id="lblTotalPlayer" Text="296" runat="server" style=" font-size:28px;"/>
                            Total Players
						</td>
						<td>							 
							<asp:Label id="lblTotalEntries" Text="383" runat="server" style=" font-size:28px;"/>
                            Total Entries
						</td>
					</tr>
					<tr>
						<td>							 
							<asp:Label id="lblSinglesEntries" Text="345" runat="server" style=" font-size:28px;"/>
                            Singles
						</td>
						<td> 
							<asp:Label id="lblDoublesEntries" Text="38" runat="server" style=" font-size:28px;"/>
                            Doubles
						</td>
					</tr>
					<tr>
						<td>	
                            Rs.						 
							<asp:Label id="lblTotalFeesReceivable" Text="Rs. 1,60,800.00" runat="server" style=" font-size:28px;"/>
                            Fees Receivable
						</td>
						<td>
                            Rs.
							<asp:Label id="lblTotalPaymentReceived" Text="Rs. 51,569.00" runat="server" style=" font-size:28px;"/>
                            Fees Received Online
						</td>
					</tr>
					<tr style="display:none;">
						<td>
                            Rs.
							<asp:Label id="lblSinglesAmount" Text="Rs. 1,38,000.00" runat="server" style=" font-size:28px;"/>
                            Singles Amount
						</td>
						<td>
                            Rs.
							<asp:Label id="lblDoublesAmount" Text="Rs. 22800.00" runat="server" style=" font-size:28px;"/>
                            Doubles Amount
						</td>
					</tr>
					<tr>
						<td>
                            Rs.
							<asp:Label id="lblTotalAmountReceived" Text="Rs. 1,38,000.00" runat="server" style=" font-size:28px;"/>
                            Amount Received
						</td>
						<td>
                            Rs.
							<asp:Label id="lblTotalAmountPending" Text="Rs. 1,38,000.00" runat="server" style=" font-size:28px;"/>
                            Amount Pending

						</td>
					</tr>
					<tr>
						<td>
                            Rs.
							<asp:Label id="lblTotalRegAmountReceivable" Text="Rs. 1,38,000.00" runat="server" style=" font-size:28px;"/>
                            District Reg Fees Receivable
						</td>
						<td>
                            Rs.
							<asp:Label id="lblTotalRegAmountReceived" Text="Rs. 1,38,000.00" runat="server" style=" font-size:28px;"/>
                            District Reg Fees Received
						</td>
					</tr>
					
					
				</table>
			</asp:Panel>

            <hr />

			
			<asp:Panel runat="server" id="pnlQuickAccess" visible="true" class="panelRegistration_Atul" >
				<table border="0" cellspacing="8px" cellpadding="8px" width="100%" >
					<tr>
						<td  style="text-align:center;border-right:0px;width:50%;">
							<asp:Button ID="btnSetupTournamentView" Text="Setup Tournament" runat="server" OnClick="btnSetupTournamentView_Click" width="100%" height="150px" class="button_Atul"/>
						</td>
						<td  style="text-align:center;border-right:0px;">
							<asp:Button ID="btnPaymentView" Text="Payment View" runat="server" OnClick="btnPaymentView_Click" width="100%" height="150px" class="button_Atul"/>
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
						<td  style="text-align:center;border-right:0px;width:50%">
							<asp:Button ID="btnPlayerListView" Text="Player List" runat="server" OnClick="btnPlayerListView_Click"  width="100%" height="150px" class="button_Atul" />
						</td>
						<td  style="text-align:center;border-right:0px;">
							<asp:Button ID="btnOfflineRegistration" Text="Offline Registration" runat="server" OnClick="btnOfflineRegistration_Click" width="100%" height="150px" class="button_Atul"/>
						</td>				
					</tr>
					<tr>
						<td  style="text-align:center;border-right:0px;width:50%">
							<asp:Button ID="btnUpdateMatchScore" Text="Match Score" runat="server" OnClick="btnUpdateMatchScore_Click"  width="100%" height="150px" class="button_Atul" />
						</td>
						<td  style="text-align:center;border-right:0px;">
                            <asp:Button ID="btnCreateUmpire" Text="Create Umpire" runat="server" OnClick="btnCreateUmpire_Click"  width="100%" height="150px" class="button_Atul" />
							<asp:Button ID="btnSMSSetup" Text="SMS Setup" runat="server" OnClick="btnSMSSetup_Click"  width="100%" height="150px" class="button_Atul" Visible=false />
						</td>				
					</tr>
				</table>
			</asp:Panel>			
			</div>
			
		</asp:Panel>

		
	</div>
	

	
	<div style=' padding-top:6px;'>
			<hr/>
	</div>
	
	
</asp:Content>
