<%@ Page Title="" Language="C#" MasterPageFile="~/Screens/TP.Master" AutoEventWireup="true" CodeBehind="TP_Match_Upcoming.aspx.cs" Inherits="TournamentPlanner.TP_Match_Upcoming" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="display:none;">
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
                    <div class="h4Text_Atul">
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
                    </div>
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
                <td>
                    <asp:LinkButton ID="lbtnEvents" Text="EVENTS" runat="server" OnClick="lbtnTPMenu_Click"
                        Width="100%" class="tabMenuInactive_Atul" Style="padding-top: 10px;" />
                </td>
                <td>
                    <asp:LinkButton ID="lbtnPlayers" Text="PLAYERS" runat="server" OnClick="lbtnTPMenu_Click"
                        Width="100%" class="tabMenuInactive_Atul" Style="padding-top: 10px;" />
                </td>
                <td>
                    <asp:LinkButton ID="lbtnDraws" Text="DRAWS" runat="server" OnClick="lbtnTPMenu_Click"
                        Width="100%" class="tabMenuInactive_Atul" Style="padding-top: 10px;" />
                </td>
                <td>
                    <asp:LinkButton ID="lbtnMatches" Text="MATCHES" runat="server" OnClick="lbtnTPMenu_Click"
                        Width="100%" class="tabMenuInactive_Atul" Style="padding-top: 10px;" />
                </td>
            </tr>
        </table>
    </div>
    <!-- Main Body -->
    <div style='padding-top: 2px; min-height: 400px; width: 100%; background: rgb(256,256,256);'
        class="h4BoldText_Atul">
        <div class="pageTopic">
            UPCOMING MATCHES - LIVE !!! 
        </div>
        <br />
        
        <asp:Panel runat="server" ID="pnlMatchNotPublished" Width="100%" class="panelRegistration_Atul">
            <div style="padding: 20px; text-align: center;" class="h2BoldText_Atul">
                Match Schedule not yet published, will be published soon!!!
            </div>
        </asp:Panel>

        <asp:Panel runat="server" ID="pnlMatchPublished" Width="100%" class="panelRegistration_Atul">
            <div style="display:none;">
                <table width="60%">
                    <tr>
                        <td width="50%">
                            <asp:LinkButton ID="lbt3Feb" Text="3 Feb" runat="server" OnClick="lbt3Feb_Click"
                                Width="100%" />
                        </td>
                        <td width="50%">
                            <asp:LinkButton ID="lbt4Feb" Text="4 Feb" runat="server" OnClick="lbt4Feb_Click"
                                Width="100%" />
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div class="h4Text_Atul">
                <asp:DataGrid ID="dgMatchList" Width="99%" runat="server" PageSize="1" AllowPaging="False"
                    AutoGenerateColumns="False" GridLines="None" OnItemDataBound="dgMatchList_RowDataBound">
                    <Columns>
                        <asp:TemplateColumn HeaderText="Match ID" ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center" Visible="false">
                            <ItemTemplate>
                                <!--<asp:LinkButton ID="lbMatchID1" style="text-decoration: underline;" runat="server" Text='<%# Bind("ID") %>' 
									PostBackUrl='<%# string.Format("./TP_MatchScore.aspx?MatchID={0}&EventCode={1}", Eval("ID"), Eval("EventCode")) %>'></asp:LinkButton>-->
                                <asp:LinkButton ID="lbMatchID" Style="text-decoration: underline;" runat="server"
                                    Text='<%# Bind("ID") %>' PostBackUrl=''></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn HeaderText="Time" DataField="MatchSchedule" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="15%"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Event" DataField="EventCode" ItemStyle-Width="8%"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Round" DataField="MatchRound"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Player1" DataField="FirstTeamPlayerName" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Font-Bold="False"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Player2" DataField="SecondTeamPlayerName" ItemStyle-HorizontalAlign="Center">
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="  Score  " DataField="MatchScore" ItemStyle-HorizontalAlign="Center" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText=" Duration " DataField="MatchDuration" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Court" DataField="CourtDetails" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Winner" DataField="WinnerTeamCode" Visible="false">
                        </asp:BoundColumn>
                    </Columns>
                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="Black" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="#F5F5F5" />
                    <ItemStyle BackColor="#FFFFFF" ForeColor="#666666" Height="45px" HorizontalAlign="Center" />
                    <HeaderStyle Height="50px" BackColor="#F5F5F5" Font-Bold="True" ForeColor="#555555"
                        HorizontalAlign="Center" />
                </asp:DataGrid>
            </div>
        </asp:Panel>
    </div>
    <div>
        <hr />
    </div>
    <div style='width: 100%; text-align: center; padding-top: 16px; padding-bottom: 20px;
        font: 14pt calibri; color: gray; background: rgb(235,235,235);'>
        Advertisement
    </div>
</asp:Content>
