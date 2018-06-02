<%@ Page Title="" Language="C#" MasterPageFile="~/Screens/TP.Master" AutoEventWireup="true" CodeBehind="TP_TournamentController.aspx.cs" Inherits="TournamentPlanner.TP_TournamentController" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<!-- Main Body -->
	<div style=' padding-top:2px; width:100%; height:600px;vertical-align: middle; background:rgb(256,256,256);'>
		<table align="top" border="0" cellspacing="1" width="100%" style='padding-bottom:2px;'>
			<tr style='height:10px;'>
				<td style='color:rgb(251,85,58);border-right:0px; border-right:0px;background:rgb(230,230,230);text-align:center;' class="h3BoldText_Atul">Tournament Controller</td>
			</tr>			
		</table>

		<!-- Panel for Owner & Umpire View -->
		<div style="padding-top:50px;">
		<table class="tblHorizontal" border="0" cellspacing="10" cellpadding="10" width="100%" style='border-right:0px;padding-top:150px;' >
			<tr>
				<td  style="text-align:center;border-right:0px;">
					<asp:Button ID="btnOwnerViewHorizontal" Text="Owner's View" runat="server" PostBackURL="./TP_OwnerView.aspx"  width="300px" height="300px" style='font: bold 28pt Calibri;' class="button_Atul" />
				</td>
				<td  style="text-align:center;border-right:0px;">
					<asp:Button ID="btnUmpireViewHorizontal" Text="Umpire's View" runat="server" PostBackURL="./TP_UmpireView.aspx" width="300px" height="300px" style='font: bold 28pt Calibri;' class="button_Atul" />
				</td>				
			</tr>
		</table>

		<table class="tblVertical" border="0" cellspacing="10" cellpadding="10" width="100%">
			<tr>
				<td  style="text-align:center;border-right:0px;">
					<asp:Button ID="btnOwnerViewVertical" Text="Owner's View" runat="server" PostBackURL="./TP_OwnerView.aspx" width="160px" height="160px" style='font: bold 16pt Calibri;' class="button_Atul"/>
				</td>
			</tr>
			<tr>
				<td  style="text-align:center;border-right:0px;padding-top:50px;">
					<asp:Button ID="btnUmpireViewVertical" Text="Umpire's View" runat="server" PostBackURL="./TP_UmpireView.aspx" width="160px" height="160px" style='font: bold 16pt Calibri;'  class="button_Atul"/>
				</td>				
			</tr>
		</table>
		
		</div>
		
	</div>
	
	<div style=' padding-top:6px;'>
			<hr/>
	</div>
	
	
		

</asp:Content>
