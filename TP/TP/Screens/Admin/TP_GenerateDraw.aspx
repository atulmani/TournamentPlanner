<%@ Page Title="" Language="C#" MasterPageFile="~/Screens/TP.Master" AutoEventWireup="true"
    CodeBehind="TP_GenerateDraw.aspx.cs" Inherits="TournamentPlanner.TP_GenerateDraw" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="scriptmanager1" runat="server">
    </asp:ScriptManager>
    <div style='padding-top: 0px; width: 100%; background: rgb(256,256,256);'>
        <table border="0" cellspacing="1" width="100%" style='padding-bottom: 2px;'>
            <tr style='height: 10px;'>
                <td style='color: rgb(251,85,58); border-right: 0px; border-right: 0px; background: rgb(230,230,230);
                    text-align: center;' class="h3BoldText_Atul">
                    Generate Draws
                </td>
            </tr>
        </table>
        <asp:Panel runat="server" ID="pnlErrorMsg" Visible="false" Height="20px" Style='text-align: center;
            background: rgb(250,250,250); color: red; font: bold 18pt Calibri;'>
            <asp:Label ID="lblErrorMsg" runat="server" Text=""></asp:Label>
        </asp:Panel>
        
        <hr />
        <div style="text-align: center; padding: 8px;" class="h3BoldText_Atul">
            <asp:LinkButton ID="lbtnReturn2Dashboard" Text="RETURN TO HOME" runat="server" OnClick="lbtnReturn2Dashboard_Click" />
            <!-- <img src="./TP_Dashboard.aspx"      icoHome.png -->
        </div>
        <hr />
        <!-- Section 4: Generate Draws  -->
        <div class="form_Atul">
            <div style="text-align:center;">
                <asp:Button ID="btnNotPublished" runat="server" Text="NotPublished" class="button_Atul" Width="130px" OnClick="btnNotPublished_Click" />
                <asp:Button ID="btnPublished" runat="server" Text="Published" class="buttonOFF_Atul" Width="130px" OnClick="btnPublished_Click"/>
            </div>
            <hr />
            
            <div style="padding-top:4px;text-align:center; width:90%;" class="h3Text_Atul">
                Events : <asp:DropDownList ID="ddlTournamentEvents" runat="server" Width="70%" class="dropdown_Atul">
                                </asp:DropDownList>
            </div>
            <div style="display: none;">
                <asp:DropDownList ID="ddlTournamentEventType" runat="server" Width="100%" Height="66px"
                                    Style='font: 16pt Calibri;'>
                    <asp:ListItem Text="Select Event Type" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Singles" Value="S"></asp:ListItem>
                    <asp:ListItem Text="Doubles" Value="D"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <hr />
            <table width="90%" border="0">
                <tr>
                    <td style="text-align:center;">
                        <div style="padding-top:8px; text-align:center;" class="h3Text_Atul">
                            <asp:RadioButtonList id="rdDraws" runat="server" autopostback="True" RepeatDirection="Horizontal" Width="90%" 
                                onselectedindexchanged="rdDraws_SelectedIndexChanged">
                                <asp:ListItem>KnockOut Draws</asp:ListItem>
                                <asp:ListItem>League</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>            
                    </td>
                </tr>
            </table>
            <hr />

            <asp:Panel runat="server" ID="pnlGenerateDraws" class="panelRegistration_Atul" Visible="false" style="padding-top:16px;">
                <div style="text-align:center;">                    
                    <table width="100%" border="0" cellspacing="15px" cellpadding="10px">                        
                        <tr>
                            <td>
                                Seed 1-PlayerCode: <asp:TextBox ID="txtSeed1" Text="" runat="server" Width="20%" MaxLength="10"
                                    Style="background: rgb(256,256,256);" class="textBox_Atul" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Seed 2-PlayerCode: <asp:TextBox ID="txtSeed2" Text="" runat="server" Width="20%" MaxLength="10"
                                    Style="background: rgb(256,256,256);" class="textBox_Atul" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Seed 3-PlayerCode: <asp:TextBox ID="txtSeed3" Text="" runat="server" Width="20%" MaxLength="10"
                                    Style="background: rgb(256,256,256);" class="textBox_Atul" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Seed 4-PlayerCode: <asp:TextBox ID="txtSeed4" Text="" runat="server" Width="20%" MaxLength="10"
                                    Style="background: rgb(256,256,256);" class="textBox_Atul" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Seed 5-PlayerCode: <asp:TextBox ID="txtSeed5" Text="" runat="server" Width="20%" MaxLength="10"
                                    Style="background: rgb(256,256,256);" class="textBox_Atul" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Seed 6-PlayerCode: <asp:TextBox ID="txtSeed6" Text="" runat="server" Width="20%" MaxLength="10"
                                    Style="background: rgb(256,256,256);" class="textBox_Atul" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Seed 7-PlayerCode: <asp:TextBox ID="txtSeed7" Text="" runat="server" Width="20%" MaxLength="10"
                                    Style="background: rgb(256,256,256);" class="textBox_Atul" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Seed 8-PlayerCode: <asp:TextBox ID="txtSeed8" Text="" runat="server" Width="20%" MaxLength="10"
                                    Style="background: rgb(256,256,256);" class="textBox_Atul" />
                            </td>
                        </tr>
                        
                        
                        <tr>
                            <td style="text-align: center; border-right: 0px;">
                                <asp:Button ID="btnUpdateSeed" Text="Update Seed" runat="server" OnClick="btnUpdateSeed_Click"
                                    class="button_Atul" Width="250px" />                            </td>
                        </tr>
                    </table>
                    <hr />
                    <div style="text-align: center; padding-top:20px;">
                        <asp:Button ID="btnGenerateDraws" Text="Generate Draws" runat="server" OnClick="btnGenerateDraws_Click"
                                    Width="70%" Height="40px" class="button_Atul" />
                    </div>
                </div>
            
                <hr />
                <div style="padding-top: 10px;">
                    <div style='padding-top: 2px; min-height: 400px; width: 100%; background: rgb(256,256,256);'
                    class="h4Text_Atul">
                
                    <div>
                </div>

            </asp:Panel>

            <asp:Panel runat="server" ID="pnlLeagueDraws" class="panelRegistration_Atul" Visible="false" style="padding-top:16px;">
                <div style="text-align:center;">
                    <asp:Button ID="btnLeagueDraws" Text="Generate League Draws" runat="server" OnClick="btnLeagueDraws_Click"
                                    class="button_Atul" Width="350px" />
                </div>
                <div>
                    
                </div>
            </asp:Panel>
                              <asp:Panel runat="server" ID="pnlDraws" Width="100%" class="panelRegistration_Atul">
                            <div style="padding-top: 4px;" class="h3BoldText_Atul">
                                <asp:Label ID="Label12" runat="server" Text="Events:  " Visible="true"></asp:Label>
                                <asp:PlaceHolder ID="PlaceHolderEventName" runat="server"></asp:PlaceHolder>
                    
                               
                           

                                 <div style="padding-top: 0px;">
                            <asp:Label ID="Label2" runat="server" Text="DRAWS for the Event : " Visible="false"
                                class="h3Text_Atul"></asp:Label>
                            <asp:Label ID="Label3" runat="server" Text="" class="h2BoldText_Atul"></asp:Label>
                        </div>
                        
                        <div  runat="server" id="divKnocOut" visible="false" style="padding-top: 4px;" class="h3BoldText_Atul">
                            <asp:Label ID="Label4" runat="server" Text="Events:  " Visible="true"></asp:Label>
                            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                    <asp:Literal ID="lit1" runat="server">
                    </asp:Literal>
                   
                                   </div>
                    </div>
             
                        
                        <div runat="server" id="divLeague" visible="false" style='background:rgb(256,256,256);text-align:top;width:100%;' class="h5Text_Atul">
			            <asp:DataGrid ID="dgLeague" widths="98%" runat="server" PageSize="1" AllowPaging="False"
				            AutoGenerateColumns="False" GridLines="None" >  
				            <Columns>
                                <asp:BoundColumn HeaderText="Event" DataField="EventCode" ItemStyle-Width="2%"></asp:BoundColumn>
					            <asp:BoundColumn HeaderText="Player Name" DataField="PlayerCode" ItemStyle-Width="12%"></asp:BoundColumn>
					            <asp:BoundColumn HeaderText="Won" DataField="MatchWon" ItemStyle-Width="2%" ></asp:BoundColumn>
					            <asp:BoundColumn HeaderText="Lost" DataField="MatchLost" ItemStyle-Width="2%"></asp:BoundColumn>
					            <asp:BoundColumn HeaderText="No Result" DataField="MatchNoResult" ItemStyle-Width="2%"></asp:BoundColumn>					
					            
                                <asp:BoundColumn HeaderText="Total Point" DataField="TotalPoint" ItemStyle-Width="2%"></asp:BoundColumn>
                                <asp:BoundColumn HeaderText="Total Point Scored" DataField="PointScored" Visible=false ItemStyle-Width="2%"></asp:BoundColumn>
                                <asp:BoundColumn HeaderText="Point Against" DataField="PointAgainst" Visible=false ItemStyle-Width="2%"></asp:BoundColumn>
					             
				            </Columns>  
				
				            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="Black" />  
				
				            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" Mode="NumericPages" />  
				
				            <AlternatingItemStyle BackColor="#F5F5F5" />  
				
				            <ItemStyle BackColor="#FFFFFF" ForeColor="#666666" Height="35px" />  
				
				            <HeaderStyle height="15px" BackColor="#F5F5F5" Font-Bold="True" ForeColor="#555555" />  
			
			            </asp:DataGrid>
		              </div>
		
                            
                            <hr />
                            <div style="padding-top: 8px;">
                                <asp:Label ID="Label123" runat="server" Text="Below are the Draws for: " Visible="true"
                                    class="h3Text_Atul"></asp:Label>
                                <asp:Label ID="Label1" runat="server" Text="" class="h3BoldText_Atul"></asp:Label>
                            </div>
                            <asp:Button ID="Button1" Text="Generate Draws" runat="server" OnClick="btnGenerateDraws_Click"
                                Width="150px" Height="36px" Visible="false" Style="background: rgb(146,215,10);
                                color: rgb(10,10,10); border: none; font: bold 12pt Calibri" />
                        </asp:Panel>
      
        </div>
    </div>
</asp:Content>
