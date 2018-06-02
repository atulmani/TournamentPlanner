<%@ Page Title="" Language="C#" MasterPageFile="~/Screens/TP.Master" AutoEventWireup="true" CodeBehind="TP_CreateUmpire.aspx.cs" Inherits="TournamentPlanner.TP_CreateUmpire" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style=' padding-top:2px; width:100%; background:rgb(256,256,256);'>
        <table border="0" cellspacing="1" width="100%" style='padding-bottom:2px;'>
			<tr style='height:10px;'>
				<td style='color:rgb(251,85,58);border-right:0px; border-right:0px;background:rgb(230,230,230);text-align:center;' class="h3BoldText_Atul">Create Login for Umpire</td>
			</tr>			
		</table>
		
		<hr />  
		<div style="text-align:center;padding:8px;" class="h3BoldText_Atul">			
			<asp:LinkButton id="lbtnReturn2Dashboard" Text="RETURN TO HOME" runat="server" OnClick="lbtnReturn2Dashboard_Click"/>
           <!-- <img src="./TP_Dashboard.aspx"      icoHome.png -->
		</div>        
    </div>

    <hr />

    <div class="form_Atul">
        
        <div style='height:35px; padding:auto; width:100%;color:White; background-color:rgb(4,163,233);text-align:center; vertical-align:middle; padding-top:3px;' class="h1BoldText_Atul">
            LOGIN
        </div>

        <div style="padding-top:10px; padding-bottom:10px; text-align:center;">
            <asp:Label ID="lblMsg" runat="server" class="h4BoldText_Atul" Text="";></asp:Label>
        </div>
        
        <div class="h3Text_Atul">
            <table border="0" width="100%" cellpadding="0px" cellspacing="0px">
				<tr>
					<th style="border-right:0px;color:#010028;padding-left:16px;padding-top:20px;">Umpire Name:</th>
				</tr>
				<tr>
					<td style="border-right:0px;">
						<asp:TextBox id="txtUmpireName"  runat="server" width="90%" MaxLength="30" class="textBox_Atul" />
					</td> 
				</tr>
				<tr>
					<th style="border-right:0px;color:#010028;padding-left:16px;padding-top:20px;">Umpire Mobile No:</th>
				</tr>
				<tr>
					<td style="border-right:0px;">
						<asp:TextBox id="txtUmpireMobileNo"  runat="server" width="90%" MaxLength="10" TextMode="Number" class="textBox_Atul" />
					</td> 
				</tr>
				<tr>
					<th style="border-right:0px;color:#010028;padding-left:16px;padding-top:20px;">Email ID:</th>
				</tr>
				<tr>
					<td style="border-right:0px;">
						<asp:TextBox id="txtEmailIDasUserCode"  runat="server" width="90%" MaxLength="50"  class="textBox_Atul" />
					</td> 
				</tr>
                <tr>
						<th style="border-right:0px;color:#010028;padding-left:16px;padding-top:20px;">Password:</th>
				</tr>
				<tr>
						<td style="border-right:0px;">
							<asp:TextBox id="txtPWD" TextMode="Password"  runat="server" width="90%" MaxLength="20" class="textBox_Atul"/>
						</td> 
				</tr>					
			</table>
			<div style="padding-top:25px;padding-right:24px; padding-bottom:20px; text-align:center;">
				<asp:Button id="btnCreateUmpire" Text="CREATE" runat="server" OnClick="btnCreateUmpire_Click" class="button_Atul" />
			</div>
        </div>
    </div>
</asp:Content>
