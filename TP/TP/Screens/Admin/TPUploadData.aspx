<%@ Page Title="" Language="C#" MasterPageFile="~/Screens/TP.Master" AutoEventWireup="true" CodeBehind="TPUploadData.aspx.cs" Inherits="TP.Screens.Admin.TPUploadData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <p>
        <br />
    </p>
    <p>
        Select File to be uploaded</p>
    <p>
&nbsp;<asp:FileUpload ID="FileUpload1" runat="server" />
    </p>
    <p>

    <asp:Button ID="btnUploadData" runat="server" Text="Upload data" 
            onclick="btnUploadData_Click" />
        &nbsp;</p>


</asp:Content>
