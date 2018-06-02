<%@ Page Title="" Language="C#" MasterPageFile="~/Screens/TP.Master" AutoEventWireup="true" CodeBehind="TP_BD_Home.aspx.cs" Inherits="TournamentPlanner.TP_BD_Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<style type="text/css">
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   	
	<!-- Message Falsh using Marquee -->
	<div>
		<marquee style=' padding-top:5px; width:100%;' class="h4Text_Atul">
			<b>Young Shuttlers 2018  </b> Badminton Tournament date will be published soon
		</marquee>
	</div>
	<!-- Main Body -->
	<div class="form_Atul">
        <div style="min-height:600px;">	
		<div style="text-align:center;padding-top:0px;">
			<table border="0px" cellspacing="0px" cellpadding="0px" width="100%">
				<tr>
					<td style="border:0px;width:25%;padding:0px;">
						<asp:LinkButton id="btnRegistrationOpen" Text="REGISTRATION" runat="server" OnClick="btnRegistrationOpen_Click" width="100%" class="tabMenuInactive_Atul" style="padding-top:10px;"/>
					</td>
                    <td style="border:0px;width:25%;padding:0px;">
						<asp:LinkButton id="btnUpcomingMatches" Text="UPCOMING" runat="server" OnClick="btnUpcomingMatches_Click" width="100%" class="tabMenuActive_Atul" style="padding-top:10px;" />
					</td>
                    <td style="border:0px;width:25%;padding:0px;">
						<asp:LinkButton id="btnRunning" Text="RUNNING" runat="server" OnClick="btnRunning_Click" width="100%" class="tabMenuInactive_Atul" style="padding-top:10px;" />
					</td>
					<td style="border:0px;width:25%;padding:0px;">
						<asp:LinkButton id="btnHistory" Text="HISTORY" runat="server" OnClick="btnHistory_Click" width="100%" class="tabMenuInactive_Atul" style="padding-top:10px;"/>
					</td>
				</tr>
			</table>
		</div>
		
		<!--<div style="background:rgb(245,245,245);padding-bottom:10px;min-height:400px;">-->
		<asp:Panel runat="server" id="pnlRunning" visible="false" style="background:rgb(245,245,245);" class="panelRegistration_Atul">
			<div style="padding:8px;text-align:center;" class="h3BoldText_Atul">
				RUNNING TOURNAMENTS
			</div>		
			
            <div>
                <asp:PlaceHolder ID="phTPRunning" runat="server"></asp:PlaceHolder>
            </div>
            
            <div class="h4Text_Atul">			
			<asp:DataGrid ID="dgRunningTournamentList" width="100%" runat="server" PageSize="1" AllowPaging="False"
				AutoGenerateColumns="False" GridLines="None" >  
				<Columns> 					
	        		<asp:TemplateColumn HeaderText="Tournament Name" ItemStyle-Width="25%">
						<ItemTemplate>						
							<asp:LinkButton ID="lbTournamentName" runat="server" Text='<%# Bind("TournamentName") %>' PostBackUrl='<%# "./TP_TournamentDetails.aspx?TCode=" + Eval("TournamentCode") %>'></asp:LinkButton>
				        </ItemTemplate>
					 </asp:TemplateColumn>
					<asp:BoundColumn HeaderText="Organisation" DataField="TournamentOrganisation" ItemStyle-Width="20%"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Start Date" DataField="TournamentStartDate" DataFormatString="{0:d-MMM-yyy}" ItemStyle-Width="10%"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="End Date" DataField="TournamentEndDate" DataFormatString="{0:d-MMM-yyy}" ItemStyle-Width="10%"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Venue" DataField="TournamentVenue" ItemStyle-Width="20%"></asp:BoundColumn>
				</Columns>  
				
				<FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="Black" />  
				
				<PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" Mode="NumericPages" />  
				
				<AlternatingItemStyle BackColor="#F5F5F5" />  
				
				<ItemStyle BackColor="#FFFFFF" ForeColor="#666666" Height="35px"  />  
				
				<HeaderStyle height="25px" BackColor="#DDDDDD" Font-Bold="True" ForeColor="#555555" />  
			
			</asp:DataGrid>
			</div>
		</asp:Panel>
				
		<asp:Panel runat="server" id="pnlRegistrationOpen" visible="false" style="background:rgb(245,245,245);" class="panelRegistration_Atul">
			<div style="padding:8px;;text-align:center;" class="h3BoldText_Atul">
				REGISTRATION OPEN
                
			</div>

            <div>
                <asp:PlaceHolder ID="phTPRegistration" runat="server"></asp:PlaceHolder>
            </div>

            <div>
                <asp:Panel runat="server" id="Panel1" visible="true" Width="90%"  BorderColor="ActiveBorder" BorderStyle="Dashed" BorderWidth="2px" style="background:rgb(245,245,245);" class="panelRegistration_Atul">
                    <asp:Label ID="lblTournamentName" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="lblOrganization" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="lblSchedule" runat="server" Text=""></asp:Label>
                </asp:Panel>
            </div>


			<div class="h4Text_Atul">
			<asp:DataGrid ID="dgRegistrationOpenTournamentList" width="100%" runat="server" PageSize="1" AllowPaging="False"
				AutoGenerateColumns="False" GridLines="None" >  
				<Columns>
	        		<asp:TemplateColumn HeaderText="Tournament Name" ItemStyle-Width="25%">
						<ItemTemplate>													
							<asp:LinkButton ID="lbTournamentName" runat="server" Text='<%# Bind("TournamentName") %>' OnClick="btnRegistrationForm_Click"
                                                    CommandArgument='<%# Eval("TournamentCode") %>'></asp:LinkButton>
				        </ItemTemplate>
					 </asp:TemplateColumn>
					<asp:BoundColumn HeaderText="Organisation" DataField="TournamentOrganisation" ItemStyle-Width="20%"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Entry Open" DataField="TournamentEntryOpenDate" DataFormatString="{0:d-MMM-yyy}" ItemStyle-Width="10%"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Entry Closes" DataField="TournamentEntryCloseDate" DataFormatString="{0:d-MMM-yyy}" ItemStyle-Width="10%"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Venue" DataField="TournamentVenue" ItemStyle-Width="20%"></asp:BoundColumn>
				</Columns>  
				
				<FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="Black" />  
				
				<PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" Mode="NumericPages" />  
				
				<AlternatingItemStyle BackColor="#F5F5F5" />  
				
				<ItemStyle BackColor="#FFFFFF" ForeColor="#666666" Height="35px" />  
				
				<HeaderStyle height="15px" BackColor="#DDDDDD" Font-Bold="True" ForeColor="#555555" />  
			
			</asp:DataGrid>
			</div>
		</asp:Panel>
				
		<asp:Panel runat="server" id="pnlUpcoming" visible="false" style="background:rgb(245,245,245);" class="panelRegistration_Atul">
			<div style="padding:8px;;text-align:center;background:#1E5C8B;color:White;" class="h3BoldText_Atul">
				UPCOMING TOURNAMENTS				
			</div>
			
            <div>
                <asp:PlaceHolder ID="phTPUpComing" runat="server"></asp:PlaceHolder>
            </div>
            
            <div class="h4Text_Atul">
			<asp:DataGrid ID="dgUpcomingTournamentList" width="100%" runat="server" PageSize="1" AllowPaging="False"
				AutoGenerateColumns="False" GridLines="None" >  
				<Columns>
	        		<asp:TemplateColumn HeaderText="Tournament Name" ItemStyle-Width="25%">
						<ItemTemplate>						
							<asp:LinkButton ID="lbTournamentName" runat="server" Text='<%# Bind("TournamentName") %>' PostBackUrl='<%# "./TP_TournamentDetails.aspx?TCode=" + Eval("TournamentCode") %>'></asp:LinkButton>
				        </ItemTemplate>
					 </asp:TemplateColumn>
					<asp:BoundColumn HeaderText="Organisation" DataField="TournamentOrganisation" ItemStyle-Width="20%"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Scheduled" DataField="TournamentStartDate" DataFormatString="{0:MMM-yyyy}" ItemStyle-Width="10%"></asp:BoundColumn>					
					<asp:BoundColumn HeaderText="Venue" DataField="TournamentVenue" ItemStyle-Width="20%"></asp:BoundColumn>
				</Columns>  
				
				<FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="Black" />  
				
				<PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" Mode="NumericPages" />  
				
				<AlternatingItemStyle BackColor="#F5F5F5" />  
				
				<ItemStyle BackColor="#FFFFFF" ForeColor="#666666" Height="35px" />  
				
				<HeaderStyle height="15px" BackColor="#DDDDDD" Font-Bold="True" ForeColor="#555555" />  
			
			</asp:DataGrid>
			</div>
		</asp:Panel>
		
		<asp:Panel runat="server" id="pnlHistory" visible="false" style="background:rgb(245,245,245);" class="panelRegistration_Atul">
			<div style="padding:8px;text-align:center;background:#1E5C8B;color:White;" class="h3BoldText_Atul">
				TOURNAMENT HISTORY
			</div>

            <div>
                <asp:PlaceHolder ID="phTPHistory" runat="server"></asp:PlaceHolder>
            </div>

			<div class="h4Text_Atul" style="display:none;">
			<asp:DataGrid ID="dgClosedTournamentList" width="100%" runat="server" PageSize="1" AllowPaging="False"
				AutoGenerateColumns="False" GridLines="None" >  
				<Columns>
	        		<asp:TemplateColumn HeaderText="Tournament Name" ItemStyle-Width="25%">
						<ItemTemplate>						
							<asp:LinkButton ID="lbTournamentName" runat="server" Text='<%# Bind("TournamentName") %>' PostBackUrl='<%# "./TP_TournamentDetails.aspx?TCode=" + Eval("TournamentCode") %>'></asp:LinkButton>
				        </ItemTemplate>	
					</asp:TemplateColumn>
					<asp:BoundColumn HeaderText="Organisation" DataField="TournamentOrganisation" ItemStyle-Width="20%"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Start Date" DataField="TournamentStartDate" DataFormatString="{0:d-MMM-yyy}" ItemStyle-Width="10%"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="End Date" DataField="TournamentEndDate" DataFormatString="{0:d-MMM-yyy}" ItemStyle-Width="10%"></asp:BoundColumn>
				</Columns>  
				
				<FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="Black" />  
				
				<PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" Mode="NumericPages" />  
				
				<AlternatingItemStyle BackColor="#F5F5F5" />  
				
				<ItemStyle BackColor="#FFFFFF" ForeColor="#666666" Height="35px" />  
				
				<HeaderStyle height="15px" BackColor="#DDDDDD" Font-Bold="True" ForeColor="#555555" />  
			
			</asp:DataGrid>
			</div>
		</asp:Panel>
		</div>
	</div>

	<div>
		<hr/>
	</div>
	
	<div style='width:100%; text-align:center;padding-top:16px; padding-bottom:20px; font: 14pt calibri; color:gray;background:rgb(235,235,235);'>
	Advertisement
	</div>


</asp:Content>
