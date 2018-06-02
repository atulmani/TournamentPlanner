<%@ Page Title="" Language="C#" MasterPageFile="~/Screens/TP.Master" AutoEventWireup="true" CodeBehind="TP_PlayerList.aspx.cs" Inherits="TournamentPlanner.TP_PlayerList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   	<asp:ScriptManager ID="scriptmanager1" runat="server">

	</asp:ScriptManager>
	
	
	
	<!-- Main Body -->
	<div style=' padding-top:2px; width:100%; background:rgb(256,256,256);'>
		<table align="top" border="0" cellspacing="1" width="100%" style='padding-bottom:2px;'>
			<tr style='height:10px;'>
				<td style='color:rgb(251,85,58);border-right:0px; border-right:0px;background:rgb(230,230,230);text-align:center;' class="h3BoldText_Atul">Dashboard</td>
			</tr>			
		</table>
				
		
		<div style="text-align:center;padding:8px;" class="h3BoldText_Atul">			
			<asp:LinkButton id="lbtnReturn2Dashboard" Text="RETURN TO HOME" runat="server" OnClick="lbtnReturn2Dashboard_Click"/>
		</div>
		
	</div>
	

	
	<div style=' padding-top:6px;'>
			<hr/>
	</div>
	
    <div class="form_Atul">
	<!-- Player List -->
	<asp:Panel runat="server" id="pnlPlayerList" style='background:rgb(250,250,250);font: 10pt Calibri;padding-bottom:10px;'>

		<div class="h3Text_Atul" style="padding-top:10px;">
			<span style="padding-left:10px;"> Category Wise: </span>
			<asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>			
		</div>
				
		<div style='padding-top:20px;' class="h3Text_Atul">	
            <table width="100%">
                <tr>
                    <td>
                        <asp:Label id="lblPlayerList" runat="server" style="padding-left:10px;">Player List of Category: </asp:Label>
			            <asp:Label ID="Label1" runat="server" Text=""  ></asp:Label>        
                    </td>
                    <td style="text-align:right;padding-right:15px;">
                        <asp:Button id="btnExportPlayerList" runat="server" Text="Export" OnClick="ExportPlayerList_Click" height="40px" width="200px" class="button_Atul"></asp:Button>			        
                    </td>
                </tr>
            </table>		
			
			
		</div>
		<div style='background:rgb(256,256,256);' class="h5Text_Atul">
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
					
					
						
					<asp:BoundColumn HeaderText="Player TShirt Size" DataField="TShirtSize"  ItemStyle-Width="10%"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Orgnization Designation" DataField="OrgnizationDesignation" ItemStyle-Width="5%"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Orgnization HR Contact" DataField="OrgnizationHRContact" ItemStyle-Width="5%"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Orgnization HR Email ID" DataField="OrgnizationHREmailID" ItemStyle-Width="10%"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Orgnization HR Name" DataField="OrgnizationHRName" ItemStyle-Width="10%"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Orgnization Name" DataField="OrgnizationName" ItemStyle-Width="10%"></asp:BoundColumn>
					
					
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

</asp:Content>
