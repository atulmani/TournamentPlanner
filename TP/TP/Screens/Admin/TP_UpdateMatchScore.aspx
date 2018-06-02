<%@ Page Title="" Language="C#" MasterPageFile="~/Screens/TP.Master" AutoEventWireup="true" CodeBehind="TP_UpdateMatchScore.aspx.cs" Inherits="TournamentPlanner.TP_UpdateMatchScore" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   	<asp:ScriptManager ID="scriptmanager1" runat="server">

	</asp:ScriptManager>

		
	<!-- Main Body -->
    <div class="form_Atul">		
        <table border="0" cellspacing="1" width="100%" style='padding-bottom:2px;'>
			<tr style='height:10px;'>
				<td style='color:rgb(251,85,58);border-right:0px; border-right:0px;background:rgb(230,230,230);text-align:center;' class="h3BoldText_Atul">Update Match Score</td>
			</tr>			
		</table>

        <div style="text-align:center;padding-top:18px;" class="h3BoldText_Atul">			
			<asp:LinkButton id="lbtnReturn2Dashboard" Text="RETURN TO HOME" runat="server" OnClick="lbtnReturn2Dashboard_Click"/>
		</div>

        <hr />


	    <asp:Panel runat="server" id="pnlErrorMsg" visible="false" height="40px" style='text-align:center;'>
			    <asp:Label id="lblErrorMsg" runat="server" Text=""  class="h4BoldText_Atul" style="color:Green;"></asp:Label>
	    </asp:Panel>



		<div class="h5Text_Atul">
			<asp:Panel runat="server" id="pnlMatchList" visible="true" class="panelRegistration_Atul">
                <asp:DataGrid ID="dgMatchList" width="100%" runat="server" PageSize="1" AllowPaging="False"
						AutoGenerateColumns="False" GridLines="None" OnItemDataBound="dgMatchList_RowDataBound">  
						<Columns>
							<asp:TemplateColumn HeaderText="Match ID" ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center">
								<ItemTemplate>						
									<asp:LinkButton ID="lbMatchID" style="text-decoration: underline;" runat="server"
									     Text='<%# Bind("ID") %>' 
									     OnClick="lbMatchID_Click" CommandArgument='<%# Eval("ID") %>'></asp:LinkButton>
						      </ItemTemplate>
			
							 </asp:TemplateColumn>
												
							<asp:BoundColumn HeaderText="Time" DataField="MatchSchedule" ItemStyle-HorizontalAlign="Center" ></asp:BoundColumn>
							<asp:TemplateColumn HeaderText="Event Name" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
								<ItemTemplate>						
									
									     <asp:Label ID="lbEventCode" runat="server" Text='<%#Bind("EventCode") %>' class="h4Text_Atul"></asp:Label>
						                <asp:HiddenField ID="hfMatchType" runat ="server" Value='<%#Bind("MatchType") %>' /> 
                              </ItemTemplate>
	                            		
							 </asp:TemplateColumn>
							
							
							<asp:BoundColumn HeaderText="Player1" DataField="FirstTeamPlayerName" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Bold="False"></asp:BoundColumn>																			
							<asp:BoundColumn HeaderText="Player2" DataField="SecondTeamPlayerName" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Bold="False"></asp:BoundColumn>
							<asp:BoundColumn HeaderText="Score" DataField="MatchScore" Visible=false></asp:BoundColumn>
							<asp:BoundColumn HeaderText="Duration" DataField="MatchDuration" Visible=false></asp:BoundColumn>
							<asp:BoundColumn HeaderText="Court" DataField="CourtDetails" Visible=false></asp:BoundColumn>
							<asp:BoundColumn HeaderText="Winner" DataField="WinnerTeamCode"  visible="false" ></asp:BoundColumn>
						</Columns>  
						
						<FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="Black" />  
						
						<PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" Mode="NumericPages" />  
						
						<AlternatingItemStyle BackColor="#F5F5F5" />  
						
						<ItemStyle BackColor="#FFFFFF" ForeColor="#666666" Height="35px" HorizontalAlign="Center" />  
						
						<HeaderStyle height="40px" BackColor="#F5F5F5" Font-Bold="True" ForeColor="#555555" HorizontalAlign="Center" />  
					
					</asp:DataGrid>
				
				</asp:Panel>
			</div>
			
		
			<asp:Panel runat="server" id="pnlUpdateScore" visible="false" style="background:rgb(245,245,245);" class="panelRegistration_Atul">
				<div style="text-align:center;padding:8px;" class="h3BoldText_Atul">			
					<asp:LinkButton id="lbtnMatchList" Text="Go back to Match List" runat="server" OnClick="lbtnMatchList_Click"/>
				</div>
				<hr />
                <table width="100%" style="padding-top:15px;">
                    <tr>
                        <td style="text-align:left;">
                            <div style="padding-left:10px;" class="h3BoldText_Atul">
					            Match ID: <asp:Label id="lblMatchID" runat="server" Text=""></asp:Label>				
				            </div>        
                        </td>
                        <td style="text-align:right;">
                            <div style="padding-right:10px;" class="h3BoldText_Atul">
					            Event Code: <asp:Label id="lblEventCode" runat="server" Text=""></asp:Label>				
				            </div>        
                        </td>
                    </tr>
                </table>
				
				<hr />
				
				    
				<div style="padding:6px; text-align:center" class="h2BoldText_Atul">
					<div style="text-align:center;">
                        Select Winner
					</div>
                    <div style="padding-top:20px;">
					<asp:RadioButton ID="rbFirstPlayer" runat="server" checked="true" Text="" GroupName="Player" AutoPostBack="false" class="h3BoldText_Atul" style="padding-right:40px;"/>
					<asp:RadioButton ID="rbSecondPlayer" runat="server" Text="" GroupName="Player" AutoPostBack="false" class="h3BoldText_Atul" style="padding-left:40px;"/>
                    <asp:RadioButton ID="rbNoMatch" runat="server"  Text="No Match" GroupName="Player" AutoPostBack="false" class="h3BoldText_Atul" Visible=false style="padding-left:40px;"/>
                    
                    </div>
				</div>
                <hr />
				<div style="padding:6px; text-align:center;" class="h3BoldText_Atul" >
					<table width="100%" cellpadding="10px" cellspacing="10px">
                        <tr>
                            <td width="120px">
                                Match Score:        
                            </td>
                            <td>
                                <asp:TextBox id="txtMatchScore" Text="" runat="server" width="90%" MaxLength="140" style="background:rgb(256,256,256);" class="textBox_Atul"/>        
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Match Duration:        
                            </td>
                            <td>
                                <asp:TextBox id="txtMatchDuration" Text="" runat="server" width="90%" MaxLength="140" style="background:rgb(256,256,256);" class="textBox_Atul"/>        
                            </td>                            
                        </tr>
                        <tr>
                            <td>
                                Court Name:        
                            </td>
                            <td>
                                <asp:TextBox id="txtCourtName" Text="" runat="server" width="90%" MaxLength="140" style="background:rgb(256,256,256);" class="textBox_Atul"/>        
                                <asp:HiddenField ID="hfMatchType" runat="server" />
                            </td>
                        </tr>
                    </table>
                    
					
				</div>
								
				<div style="padding-top:16px;text-align:center;">
                        <asp:Label ID="NoplayerMessame" runat="server" class="h3BoldText_Atul"></asp:Label>
    					<asp:Button id="btnUpdateScore" Text="Update Score" runat="server" OnClick="btnUpdateScore_Click" class="button_Atul" width="90%" />
                 </div>
                 <div style="padding-top:35px;text-align:center; visibility:hidden;">
                        <asp:Button id="btnRollbackStatus" Text="Reset Match status" runat="server" OnClick="btnRollbackScore_Click" class="button_Atul" width="90%" />				
				</div>
				
				
			</asp:Panel>	
		
			
	</div>
</asp:Content>
