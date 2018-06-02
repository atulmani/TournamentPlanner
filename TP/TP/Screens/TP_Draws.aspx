<%@ Page Title="" Language="C#" MasterPageFile="~/Screens/TP.Master" AutoEventWireup="true"
    CodeBehind="TP_Draws.aspx.cs" Inherits="TournamentPlanner.TP_Draws" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*Wait Panel - Start*/
        .pleaseWait
        {
            color: rgb(4,163,233);
            font-family: Arial;
            font-size: 28px;
            font-weight: bold; /*border: 5px solid #67CFF5;*/
            text-align: center;
            width: 100%;
            height: 40px;
            background-color: #0a0a0a;
            display: none;
            z-index: 999; /*position:absolute; 
        left:40%; 
        top:60%;*/
            position: fixed;
            left: 0;
            top: 50%;
        }
        
        /*Wait Panel - End*/
    </style>
    <script type="text/javascript" language="javascript">

        //Wait Panel - Start
        function showLoading() {
            $('.pleaseWait').show();

        }

        function pageLoad() {
            $('.pleaseWait').fadeOut(200);
        }
        //Wait Panel - End
                
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="scriptmanager1" runat="server">
    </asp:ScriptManager>
    <!-- Wait Panel - Start -->
    <div class="pleaseWait">
        Loading..... please wait!
    </div>
    <!-- Wait Panel - End -->
    <div>
        <table width="100%">
            <tr>
                <td>
                    <div class="h3BoldText_Atul">
                        <asp:Label ID="lblTournamentName" Text="" runat="server"></asp:Label>
                    </div>
                    <div class="h4Text_Atul">
                        <asp:Label ID="lblTournamentOrganisation" Text="" runat="server"></asp:Label>
                    </div>
                    <div class="h4Text_Atul">
                        <asp:Label ID="lbl22" Text="Venue: " runat="server" class="h4BoldText_Atul"></asp:Label>
                        <asp:Label ID="lblTournamentVenue" Text="" runat="server"></asp:Label>
                    </div>
                    <!--<div class="h4Text_Atul">
                        <asp:Label ID="lblLocationAddress" Text="" runat="server"></asp:Label>
                    </div>
                    <div class="h4Text_Atul">
                        <asp:Label ID="lbl24" Text="Contact: " runat="server" class="h4BoldText_Atul"></asp:Label>
                        <asp:Label ID="lblTournamentContacts" Text="" runat="server"></asp:Label>
                    </div>
                    <div class="h4Text_Atul">
                        <asp:Label ID="lbl25" Text="Entry Open: " runat="server" class="h4BoldText_Atul"></asp:Label>
                        <asp:Label ID="lblEntryOpenDate" Text="" runat="server"></asp:Label>
                    </div>
                    <div class="h4Text_Atul">
                        <asp:Label ID="lbl26" Text="Entry Closes: " runat="server" class="h4BoldText_Atul"></asp:Label>
                        <asp:Label ID="lblEntryClosesDate" Text="" runat="server"></asp:Label>
                    </div>
                    <div class="h4Text_Atul">
                        <asp:Label ID="lbl27" Text="Withdrawal: " runat="server" class="h4BoldText_Atul"></asp:Label>
                        <asp:Label ID="lblEntryWithdrawalDate" Text="" runat="server"></asp:Label>
                    </div>-->
                    <div class="h4Text_Atul">
                        <asp:Label ID="lbl28" Text="Duration: " runat="server" class="h4BoldText_Atul"></asp:Label>
                        <asp:Label ID="lblTournamentDuration" Text="" runat="server"></asp:Label>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <!-- TP Menu -->
    <div style="text-align: center;">
        <table border="0" cellspacing="0" style='width: 100%; height: 25px;'>
            <tr>
                <td style='text-align: center;'>
                    <asp:LinkButton ID="lbtnHome" Text="HOME" runat="server" OnClick="lbtnTPMenu_Click"
                        Width="100%" class="tabMenuInactive_Atul" Style="padding-top: 10px;" />
                </td>
                <td style='text-align: center;'>
                    <asp:LinkButton ID="lbtnEvents" Text="EVENTS" runat="server" OnClick="lbtnTPMenu_Click"
                        Width="100%" class="tabMenuInactive_Atul" Style="padding-top: 10px;" />
                </td>
                <td style='text-align: center;'>
                    <asp:LinkButton ID="lbtnPlayers" Text="PLAYERS" runat="server" OnClick="lbtnTPMenu_Click"
                        Width="100%" class="tabMenuInactive_Atul" Style="padding-top: 10px;" />
                </td>
                <td style='text-align: center;'>
                    <asp:LinkButton ID="lbtnDraws" Text="DRAWS" runat="server" OnClick="lbtnTPMenu_Click"
                        Width="100%" class="tabMenuActive_Atul" Style="padding-top: 10px;" />
                </td>
                <td style='text-align: center;'>
                    <asp:LinkButton ID="lbtnMatches" Text="MATCHES" runat="server" OnClick="lbtnTPMenu_Click"
                        Width="100%" class="tabMenuInactive_Atul" Style="padding-top: 10px;" />
                </td>
            </tr>
        </table>
    </div>
    <!-- Main Body -->
    <div class="form_Atul">
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <div style='padding-top: 2px; width: 100%; background: rgb(256,256,256);'
                class="h4Text_Atul">
                <div class="pageTopic">
                    DRAWS
                </div>
                <asp:Panel runat="server" ID="pnlDrawsNotPublished" Width="100%" class="panelRegistration_Atul">
                    <div style="padding: 20px; text-align: center;" class="h4Text_Atul">
                        DRAWS not yet published, will be published soon!!!
                    </div>
                </asp:Panel>
                
                <div style=" padding-top:10px; padding-left:10px;width:100%;">
                    <asp:Panel runat="server" ID="pnlDraws" Width="100%" class="panelRegistration_Atul">
                            <asp:PlaceHolder ID="PlaceHolderEventName" runat="server"></asp:PlaceHolder>
                    
                    <hr />
                        <div style="padding-top: 0px;" class="h4Text_Atul">
                            <asp:Label ID="Label123" runat="server" Text="DRAWS for the Event : " Visible="false"></asp:Label>
                            <asp:Label ID="Label1" runat="server" Text="" class="h4BoldText_Atul"></asp:Label>
                        </div>
                        <hr />
                        
                        <div  runat="server" id="divKnocOut" visible="false" style="padding-top: 4px; width:100%;" class="h4BoldText_Atul">
                            <asp:Label ID="Label12" runat="server" Text="Events:  " Visible="true"></asp:Label>
                            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                            <asp:Literal ID="lit1" runat="server">
                            </asp:Literal>
                        </div>
                        <div runat="server" id="divLeague" visible="false" style='background:rgb(256,256,256);width:100%;' class="h5Text_Atul">
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
		
        

                        <asp:Button ID="btnGenerateDraws" Text="Generate Draws" runat="server" OnClick="btnGenerateDraws_Click"
                            Width="150px" Height="36px" Visible="false" Style="background: rgb(146,215,10);
                            color: rgb(10,10,10); border: none; font: bold 12pt Calibri" />
                    </asp:Panel>
                </div>
                
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    <div>
        <hr />
    </div>
    <div style='width: 100%; text-align: center; padding-top: 16px; padding-bottom: 20px;
        font: 14pt calibri; color: gray; background: rgb(235,235,235);'>
        Advertisement
    </div>
</asp:Content>
