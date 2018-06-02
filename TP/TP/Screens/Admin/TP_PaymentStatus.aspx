<%@ Page Title="" Language="C#" MasterPageFile="~/Screens/TP.Master" AutoEventWireup="true" CodeBehind="TP_PaymentStatus.aspx.cs" Inherits="TournamentPlanner.TP_PaymentStatus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   	<asp:ScriptManager ID="scriptmanager1" runat="server">

	</asp:ScriptManager>
	
	
	
	<!-- Main Body -->
	<div style=' padding-top:2px; width:100%; background:rgb(256,256,256);'>
		<table align="top" border="0" cellspacing="1" width="100%" style='padding-bottom:2px;'>
			<tr style='height:10px;'>
				<td style='color:rgb(251,85,58);border-right:0px; border-right:0px;background:rgb(230,230,230);text-align:center;' class="h3BoldText_Atul">Payment Status</td>
			</tr>			
		</table>
		
		<div style="text-align:center;padding:8px;" class="h3BoldText_Atul">			
			<asp:LinkButton id="lbtnReturn2Dashboard" Text="RETURN TO HOME" runat="server" OnClick="lbtnReturn2Dashboard_Click"/>
		</div>
		
	</div>
	
    <div class="form_Atul">
<!-- Payment Section-->
	<div style='min-height:400px; padding-top:2px; width:100%; background:rgb(256,256,256);'>		
		<!-- Playment View -->
	<asp:Panel runat="server" id="pnlPayment" width="100%" style='background:rgb(250,250,250);text-align:center;min-height:400px;' class="h3Text_Atul">
		<div style='width:100%;'>
				<div style="padding:6px;">
					<asp:UpdatePanel ID="updatepnl" runat="server">
						<ContentTemplate>
							<asp:RadioButtonList ID="rbPaymentReportOptions" runat="server"
								repeatdirection="horizontal" width="100%"
								OnCheckedChanged="rbPaymentReportOptions_CheckedChanged" autoPostBack="true">
		            	        <asp:ListItem>Pending</asp:ListItem>
		            	        <asp:ListItem>Completed</asp:ListItem>
		            	        <asp:ListItem>Status Update - In Progress</asp:ListItem>
		            	    </asp:RadioButtonList>									
						</ContentTemplate>
					</asp:UpdatePanel>
			</div>
			
			<div style="padding-top:5px;text-align:center;">
				<asp:Button id="btnGetPaymentList" runat="server" text="Get Payment List" OnClick="btnGetPaymentList_Click" width="250px" class="button_Atul" ></asp:Button>
			</div>			
			
			<hr/>
			
			<table width="100%" style="text-align:center; cellpadding="16px" cellspacing="16px">
				<tr>
					<td>						
						<asp:Button id="btnSelectAndTakeAction" runat="server" text="Select and Act" visible="false" OnClick="btnSelectAndTakeAction_Click" width="80%" class="button_Atul"></asp:Button>
					</td>
				</tr>
				<tr>
					<td>
						<asp:Button id="btnSelectChangeStatus" runat="server" text="Select and Act" visible="false" OnClick="btnSelectChangeStatus_Click" width="80%" class="button_Atul" ></asp:Button>
					</td>
				</tr>
				<tr>
					<td>
						<asp:Button id="btnUpdateOfflineStatus" runat="server" text="Confirm Offline(Cash) Payment" visible="false" OnClick="btnUpdateOfflineStatus_Click" width="80%" class="button_Atul" ></asp:Button>
					</td>
				</tr>
				<tr>
					<td>
						<asp:Button id="btnExportPayment" runat="server" text="Click to Export" OnClick="ExportPaymentList_Click" width="80%" class="button_Atul" ></asp:Button>
					</td>
				</tr>
			</table>
		</div>
		<hr/>
		<div style="background:rgb(231,231,215);color:rgb(10,10,10);height:25px;" class="h4BoldText_Atul">
			<table width="100%" border="0">
				<tr>
					<td style="text-align:left;">	Total Entry :<asp:Label id="lblTotalRow" runat="server"></asp:Label></td>
					<td style="text-align:right;"> Total Amount :
						<asp:Label id="lblTotalAmount" runat="server"></asp:Label>
					</td>
				</tr>
			</table>
		</div>
		
		<div style='background:rgb(256,256,256);text-align:top;width:100%;' class="h5Text_Atul">
			<asp:DataGrid ID="dgPayment" width="98%" runat="server" PageSize="1" AllowPaging="False"
				AutoGenerateColumns="False" GridLines="None" >  
				<Columns>
					<asp:BoundColumn HeaderText="Player Code" DataField="PlayerCode"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Player/Team Name" DataField="PlayerFullName"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Contact#" DataField="PlayerContact" ItemStyle-Width="2%"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="EmailID" DataField="PlayerEmailID" ItemStyle-Width="5%" visible="false"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Date Of Birth" DataField="PlayerDOB"  ItemStyle-Width="5%" visible="false"></asp:BoundColumn>					
					<asp:BoundColumn HeaderText="Category" DataField="EventCode"  ItemStyle-Width="5%"></asp:BoundColumn>
					
					<asp:BoundColumn HeaderText="Parter Name" DataField="PartnerName" visible="false" ></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Parter Date Of Birth" DataField="PartnerDOB" visible="false" ></asp:BoundColumn>
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
				
				<HeaderStyle height="15px" BackColor="#F5F5F5" Font-Bold="True" ForeColor="#555555" />  
			
			</asp:DataGrid>
		</div>
		
	</asp:Panel>
	</div>
	</div>
	
</asp:Content>
