<%@ Page Title="" Language="C#" MasterPageFile="~/Screens/TP.Master" AutoEventWireup="true" CodeBehind="TP_OfflineRegistration.aspx.cs" Inherits="TournamentPlanner.TP_OfflineRegistration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style=' padding-top:2px; width:100%;'>
		<div class="form_Atul">
            <div style='height:35px; padding-top:5px; width:100%;color:White; background-color:rgb(4,163,233);text-align:center;' class="h1BoldText_Atul"'>
                OFFLINE REGISTRATION
            </div>
            <div style="text-align:center;padding:8px;" class="h3BoldText_Atul">			
			    <asp:LinkButton id="lbtnReturn2Dashboard" Text="RETURN TO HOME" runat="server" OnClick="lbtnReturn2Dashboard_Click"/>
		    </div>

            <div style=' padding-top:16px; text-align:center; visibility:hidden;'  class="h3Text_Atul">
                <asp:LinkButton ID="lbOnlineRegistrationForm" runat="server" OnClick="lbtnOnlineRegistrationForm_Click">Offline Registration Form</asp:LinkButton>
            </div>

            <div class="h3Text_Atul" style="padding-top:55px;padding-left:10px;">Choose file to be uploaded (only excel file)</div>
            <div class="h3Text_Atul" style="padding-top:5px;padding-left:10px;">
                <asp:FileUpload ID="FileUpload1" runat="server" Width="92%" Height="50px" />
            </div>
            <div style="padding-top:70px;padding-left:10px;">
                <asp:Button ID="btnUploadData" runat="server" Text="UPLOAD" onclick="btnUploadData_Click"  class="button_Atul"/>
            </div>
        </div>
    </div>

</asp:Content>

