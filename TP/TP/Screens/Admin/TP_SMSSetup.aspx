<%@ Page Title="" Language="C#" MasterPageFile="~/Screens/TP.Master" AutoEventWireup="true" CodeBehind="TP_SMSSetup.aspx.cs" Inherits="TournamentPlanner.TP_SMSSetup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<script type="text/javascript">
    function CountChar(idTxtBox) {
        document.getElementById('lblSMSMsgLength').innerHTML = document.getElementById(txtSMSMsg).value.length;
    }
    
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<!-- Main Body -->
	<div style=' padding-top:2px; width:100%; background:rgb(256,256,256);'>		
		<table border="0" cellspacing="1" width="100%" style='padding-bottom:2px;'>
			<tr style='height:10px;'>
				<td style='color:rgb(251,85,58);border-right:0px; border-right:0px;background:rgb(230,230,230);text-align:center;' class="h3BoldText_Atul">SMS Setup</td>
			</tr>			
		</table>
		
		<hr />
		<div style="text-align:center;padding:8px;" class="h3BoldText_Atul">			
			<asp:LinkButton id="lbtnReturn2Dashboard" Text="RETURN TO HOME" runat="server" OnClick="lbtnReturn2Dashboard_Click"/>
           <!-- <img src="./TP_Dashboard.aspx"      icoHome.png -->
		</div>
        <hr />

        <div style="text-align:center;padding:8px;" class="h3BoldText_Atul">	
            SMS Details
        </div>
        <div style="text-align:center;padding:8px;" class="h4Text_Atul">
            Tournament SMS Limit: <asp:Label ID="lblSMSLimit" runat="server" Text="0"></asp:Label>
        </div>
        <div style="text-align:center;padding:8px;" class="h4Text_Atul">
            Used SMS Count: <asp:Label ID="lblUsedSMSCount" runat="server" Text="0"></asp:Label>
        </div>
        <div style="text-align:center;padding:8px;" class="h4BoldText_Atul">
            Remaining SMS Count: <asp:Label ID="lblRemainingSMSCount" runat="server" Text="0"></asp:Label>
        </div>
        <hr />


        <div style="text-align:center;padding:8px;" class="h3BoldText_Atul">	
            Customized SMS
        </div>
        <div class="form_Atul">            
            <table border="0" cellspacing="5" width="100%" style='padding-bottom:2px;'>
			    <tr>
				    <td class="h3BoldText_Atul" style="padding-left:12px">Mobile No: </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtMobileNo" runat="server" class="textBox_Atul" Width="90%" TextMode="Number" MaxLength="10" ></asp:TextBox>
                    </td>
			    </tr>			
                <tr>
                    <td class="h3BoldText_Atul" style="padding-left:12px; padding-top:14px;">SMS Message: 
                        <asp:Label ID="lblSMSMsgLength" runat="server" Text="0"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="h1Text_Atul">
                        <asp:TextBox ID="txtSMSMsg" runat="server" class="textBox_Atul" 
                        Width="90%" Height="200px" TextMode="MultiLine" MaxLength="135"
                         Font-Size="Larger" onkeyup="CountChar(this.id);" OnTextChanged="txtSMSMsg_TextChanged"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="h3BoldText_Atul" style="padding-left:12px; padding-top:24px; text-align:center;">
                        <asp:Button ID="btnSMSSend" runat="server" Text="SEND" class="button_Atul" Width="50%" OnClick="btnSMSSend_Click" />
                    </td>
                </tr>
		    </table> 
            <hr />            
            
            <!-- Notifications for DRAWS Published -->
            <div style="text-align:center;padding:8px;" class="h3BoldText_Atul">	
                DRAWS Publish Notification
            </div>
            <div style="color:Green; text-align:center">
                <asp:Label ID="lblDrawsStatus" runat="server" Text="" class="h4BoldText_Atul" style="color:Green; text-align:center" ></asp:Label>
            </div>
            <table border="0" cellspacing="5" width="100%" style='padding-bottom:2px; text-align:center;' class="h4Text_Atul">
                <tr>
                    <td>
                        Total Player Count:  <asp:Label ID="lblPlayerCount" runat="server" Text="0"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>SMS will send to all the players and SMS count will be deducted from SMS Limit</td>
                </tr>
                <tr>
                    <td class="h3BoldText_Atul" style="padding-left:12px; padding-top:24px; text-align:center;">
                        <asp:Button ID="btnDRAWSPublishedNotification" runat="server" Text="DRAWS Published Notification" 
                        class="button_Atul" Width="300px" OnClick="btnDRAWSPublishedNotification_Click" />
                    </td>
                </tr>
            </table>
                 
             <hr />
            <!-- Match Schedule Notifications -->
            <div style="text-align:center;padding:8px;" class="h3BoldText_Atul">	
                Match Schedule Notification
            </div>
            <div style="color:Green; text-align:center">
                <asp:Label ID="lblMatchScheduleStatus" runat="server" Text="" class="h4BoldText_Atul" style="color:Green; text-align:center" ></asp:Label>
            </div>
            <div style="padding-top:8px; text-align:center;">
                <asp:DropDownList ID="ddlEventCode" runat="server" Width="90%" class="dropdown_Atul">
                    <asp:ListItem Text="BULK" Value="BULK"></asp:ListItem>
                    <asp:ListItem Text="MS" Value="MS"></asp:ListItem>
                    <asp:ListItem Text="WS" Value="WS"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <table border="0" cellspacing="5" width="100%" style='padding-bottom:2px; padding-top:8px; text-align:center;' class="h4Text_Atul">                
                <tr>
                    <td>SMS will send to all the players and SMS count will be deducted from SMS Limit</td>
                </tr>
                <tr>
                    <td class="h3BoldText_Atul" style="padding-left:12px; padding-top:24px; text-align:center;">
                        <asp:Button ID="btnMatchScheduleNotification" runat="server" Text="Match Schedule Notification" 
                        class="button_Atul" Width="300px" OnClick="btnMatchScheduleNotification_Click" />
                    </td>
                </tr>
            </table>
                  
        </div>


    </div>


</asp:Content>
