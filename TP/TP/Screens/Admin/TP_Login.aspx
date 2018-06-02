<%@ Page Title="" Language="C#" MasterPageFile="~/Screens/TP.Master" AutoEventWireup="true" CodeBehind="TP_Login.aspx.cs" Inherits="TournamentPlanner.TP_Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   	
	<!-- Main Body -->
    
    	
		<!-- Owner's Login -->			
		<div class="form_Atul">
            <div style='height:35px; padding:auto; width:100%;color:White; background-color:rgb(4,163,233);text-align:center; vertical-align:middle; padding-top:3px;' class="h1BoldText_Atul">
                LOGIN
            </div>	
            <div>	
                <asp:Panel runat="server" id="pnlLoginErrorMsg" visible="false" height="35px" style='text-align:center; background:rgb(250,250,250);color:red;padding-top:10px;' class="h4BoldText_Atul">
					    <asp:Label id="lblLoginErrorMsg" runat="server" Text=""  ></asp:Label>
				    </asp:Panel>
			    <asp:Panel runat="server" id="pnlOwnerLogin" visible="true" class="panelRegistration_Atul">				
				    
				    <table border="0" width="100%" cellpadding="0px" cellspacing="0px">
					    <tr>
						    <th style="border-right:0px;color:#010028;padding-left:16px;padding-top:20px;">Tournament Code:</th>
					    </tr>
					    <tr>
						    <td style="border-right:0px;">
							    <asp:TextBox id="txtTournamentCode"  runat="server" width="90%" MaxLength="10" class="textBox_Atul" />
						    </td> 
					    </tr>
					    <tr>
						    <th style="border-right:0px;color:#010028;padding-left:16px;padding-top:20px;">User Code:</th>
					    </tr>
					    <tr>
						    <td style="border-right:0px;">
							    <asp:TextBox id="txtUserCode"  runat="server" width="90%" MaxLength="50" class="textBox_Atul" />
						    </td> 
					    </tr>
					    <tr>
							    <th style="border-right:0px;color:#010028;padding-left:16px;padding-top:20px;">Password:</th>
					    </tr>
					    <tr>
							    <td style="border-right:0px;">
								    <asp:TextBox id="txtOwnerPassword" TextMode="Password"  runat="server" width="90%" MaxLength="20" class="textBox_Atul"/>
							    </td> 
					    </tr>					
				    </table>
				    <div style="padding-top:25px;padding-right:24px;text-align:center;">
					    <asp:Button id="btnLogin" Text="Login" runat="server" OnClick="btnAuthenticationANDGenerateOTP_Click" class="button_Atul" />
				    </div>				
			    </asp:Panel>
            </div>

            <div>
                <asp:Panel runat="server" id="pnlOTP" visible="false" class="panelRegistration_Atul">
                    <div class="h1Text_Atul" style=" text-align:center;padding:15px;">
                        Verify Your Identity
                    </div>
                    
                    <div class="h4Text_Atul" style=" text-align:center;padding-top:14px;">
                        You're trying to <b>Log In to TournamentPlanner</b>. To make sure your TournamentPlanner account is secure, 
                        we have to verify your identity.
                    </div>
                    <div class="h4Text_Atul" style=" text-align:center;padding-top:14px;">
                        Enter the verification code we texted to your registered <b>Email Id</b> and your registered <b>Mobile no</b>. 
                    </div>
                    <div class="h3BoldText_Atul" style="padding-top:24px;padding-left:14px;">
                        Verification Code
                    </div>
                    <div>
                        <asp:TextBox id="txtOTP"  runat="server" width="94%" TextMode="Number" MaxLength="4" class="textBox_Atul" />
                    </div>
                    <div style="padding-top:15px;text-align:center;">                        
					    <asp:Button id="btnGO" Text="VERIFY" runat="server" OnClick="btnLogin_Click" class="button_Atul" />				    
                    </div>
                </asp:Panel>
            </div>
		</div>
		
	<div style=' padding-top:6px;'>
			<hr/>
	</div>
	
</asp:Content>
