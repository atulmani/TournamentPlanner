<%@ Page Title="" Language="C#" MasterPageFile="~/Screens/TP.Master" AutoEventWireup="true" CodeBehind="TP_PlayerUpdate.aspx.cs" Inherits="TournamentPlanner.TP_PlayerUpdate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<style type="text/css">
			
			.hide {
				width:100%;
				border:0px solid #000;				
				max-height:99em;
				opacity:1;
				height:auto;
				overflow:hidden;
				transition:opacity 0.4s linear, max-height 0.4s linear;
			}
			.hide p {
				padding:10px;
				margin:0
			}
			input[type=checkbox]:checked + div {
				opacity:0;
				max-height:0;
				border:none;
			}		
		</style>



<script type="text/javascript" language="javascript">

function Validate(sender, args) {

        var gridView = document.getElementById("dgEventParticipation"); 

        var checkBoxes = gridView.getElementsByTagName("input");

        for (var i = 0; i < checkBoxes.length; i++) {

            if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked) {

          //      args.IsValid = true;
			alert("test 1")
          //      return;

            }

        }
        alert("test 2");

        //args.IsValid = false;

    }

function myFunction() {
		    var x = document.getElementById('test');
		    if (x.style.display === 'none') {
		        x.style.display = 'block';
		    } else {
		        x.style.display = 'none';
		    }
		}
		
		function show() {
	        if(document.getElementById('benefits').style.display=='none') {
	          document.getElementById('benefits').style.display='block';
	         else
	         	document.getElementById('benefits').style.display='none';
	        }
    	}
    	
    	function close() {
	        if(document.getElementById('benefits').style.display=='block') {
	          document.getElementById('benefits').style.display='none';
	        }
    	} 
    	
    	 $(".form_datetime").datetimepicker({format: 'yyyy-mm-dd hh:ii'});
    	

	</script>
	

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:ScriptManager ID="scriptmanager1" runat="server">

</asp:ScriptManager>

	<!-- Main Body -->


	<div style=' padding-top:2px; width:100%; background:rgb(256,256,256);'>
		<table align="top" border="0" cellspacing="1" width="100%" style='padding-bottom:2px;'>
			<tr style='height:10px;'>
				<td style='color:rgb(251,85,58);border-right:0px; border-right:0px;background:rgb(230,230,230);text-align:center;' class="h3BoldText_Atul">Payment Status</td>
			</tr>			
		</table>
	</div>
		
		
	<div>
	<table>
		<tr>
			<td>
				<asp:RadioButtonList ID="rblSelection" runat="Server" autopostback="true">
		            <asp:ListItem Text="Player Code" Value="PlayerCode" selected="true"></asp:ListItem>
		            <asp:ListItem Text="Player Name" Value="PlayerName"></asp:ListItem>
		            
				</asp:RadioButtonList>
				<asp:HiddenField runat="server" id="hdType" />
			</td>
			<td>
				<asp:DropDownList ID="ddlPlayer" runat="server" class="dropdown_Atul">
				</asp:DropDownList>
			</td>
			
		</tr>
		<tr>
		<td colspan=2 >
			<asp:Button id="btnGetPlayerList" Text="Get Player Details for Update" runat="server" OnClick="btnGetPlayerList_Click" class="button_Atul" />
		
		</td>
		</tr>
	</table>
	</div>	
	
	
	<div class="form_Atul" style="width:100%;">
		
		<asp:Panel runat="server" id="pnlErrorMsg" visible="false" height="20px" style='text-align:center;margin-top:20px;'  class="panelRegistration_Atul">
			<asp:Label id="lblErrorMsg" runat="server" Text="" style="color:red;"  ></asp:Label>
		</asp:Panel>
		
		<asp:Panel runat="server" id="pnlPlayerDetails" visible="false" class="panelRegistration_Atul" style="background:rgb(235,247,255);">
		
			<table border="0" align="top" width="100%" cellpadding="0px" cellspacing="0px" >
				<tr>
					<td style="border-right:0px;color:rgb(4,163,233);padding-left:20px;">Player First Name:</td>										 
				</tr>
				<tr>
					<td style="border-right:0px;">
						<asp:TextBox ID="txtFirstName" runat="server" width="90%" MaxLength="16" class="textBox_Atul"></asp:TextBox>								
					</td>
				</tr>									
				<tr>
					<td style="border-right:0px;color:rgb(4,163,233);padding-left:20px;" class="topPaddingBetweenObjects_Atul">Player Last Name:</td>										 
				</tr>
				<tr>
					<td style="border-right:0px;">
						<asp:TextBox id="txtLastName"  runat="server" width="90%" MaxLength="16" class="textBox_Atul" />
					</td>
				</tr>
				<tr>
					<td style="border-right:0px;color:rgb(4,163,233);padding-left:20px;" class="topPaddingBetweenObjects_Atul">Player Gender:</td>										 
				</tr>
				<tr>
					<td style="border-right:0px;">
						<asp:DropDownList ID="ddlGender" runat="server" width="90%" class="dropdown_Atul">
			                <asp:ListItem Text="Select Gender" Value="0"></asp:ListItem>
			                <asp:ListItem Text="Male" Value="M"></asp:ListItem>
			                <asp:ListItem Text="Female" Value="F"></asp:ListItem>
	            		</asp:DropDownList>					
					</td>
				</tr>
				<tr>
					<td style="border-right:0px;color:rgb(4,163,233);padding-left:20px;" class="topPaddingBetweenObjects_Atul">Player Date of Birth</td>										
				</tr>
				<tr>
					<td style="border-right:0px;width:100%;">
						
						<asp:DropDownList ID="ddlDate" runat="server" width="25%" class="dropdown_Atul">
			                <asp:ListItem Text="Select Date" Value="0"></asp:ListItem>
			                <asp:ListItem Text="01" Value="01"></asp:ListItem>
			                <asp:ListItem Text="02" Value="02"></asp:ListItem>
			                <asp:ListItem Text="03" Value="03"></asp:ListItem>
			                <asp:ListItem Text="04" Value="04"></asp:ListItem>
			                <asp:ListItem Text="05" Value="05"></asp:ListItem>
			                <asp:ListItem Text="06" Value="06"></asp:ListItem>
			                <asp:ListItem Text="07" Value="07"></asp:ListItem>
			                <asp:ListItem Text="08" Value="08"></asp:ListItem>
			                <asp:ListItem Text="09" Value="09"></asp:ListItem>
			                <asp:ListItem Text="10" Value="10"></asp:ListItem>
			                <asp:ListItem Text="11" Value="11"></asp:ListItem>
			                <asp:ListItem Text="12" Value="12"></asp:ListItem>
			                <asp:ListItem Text="13" Value="13"></asp:ListItem>
			                <asp:ListItem Text="14" Value="14"></asp:ListItem>
			                <asp:ListItem Text="15" Value="15"></asp:ListItem>
			                <asp:ListItem Text="16" Value="16"></asp:ListItem>
			                <asp:ListItem Text="17" Value="17"></asp:ListItem>
			                <asp:ListItem Text="18" Value="18"></asp:ListItem>
			                <asp:ListItem Text="19" Value="19"></asp:ListItem>
			                <asp:ListItem Text="20" Value="20"></asp:ListItem>
			                <asp:ListItem Text="21" Value="21"></asp:ListItem>
			                <asp:ListItem Text="22" Value="22"></asp:ListItem>
			                <asp:ListItem Text="23" Value="23"></asp:ListItem>
			                <asp:ListItem Text="24" Value="24"></asp:ListItem>
			                <asp:ListItem Text="25" Value="25"></asp:ListItem>
			                <asp:ListItem Text="26" Value="26"></asp:ListItem>
			                <asp:ListItem Text="27" Value="27"></asp:ListItem>
			                <asp:ListItem Text="28" Value="28"></asp:ListItem>
			                <asp:ListItem Text="29" Value="29"></asp:ListItem>
			                <asp:ListItem Text="30" Value="30"></asp:ListItem>
			                <asp:ListItem Text="31" Value="31"></asp:ListItem>								                
	            		</asp:DropDownList>
																	
						<asp:DropDownList ID="ddlMonth" runat="server" width="30%" class="dropdown_Atul">
			                <asp:ListItem Text="Select Month" Value="0"></asp:ListItem>
			                <asp:ListItem Text="Jan" Value="01"></asp:ListItem>
			                <asp:ListItem Text="Feb" Value="02"></asp:ListItem>
			                <asp:ListItem Text="Mar" Value="03"></asp:ListItem>
			                <asp:ListItem Text="Apr" Value="04"></asp:ListItem>
			                <asp:ListItem Text="May" Value="05"></asp:ListItem>
			                <asp:ListItem Text="Jun" Value="06"></asp:ListItem>
			                <asp:ListItem Text="Jul" Value="07"></asp:ListItem>
			                <asp:ListItem Text="Aug" Value="08"></asp:ListItem>
			                <asp:ListItem Text="Sep" Value="09"></asp:ListItem>
			                <asp:ListItem Text="Oct" Value="10"></asp:ListItem>
			                <asp:ListItem Text="Nov" Value="11"></asp:ListItem>
			                <asp:ListItem Text="Dec" Value="12"></asp:ListItem>
	            		</asp:DropDownList>
	            		
	            		<asp:TextBox id="txtDOBYear"  runat="server" MaxLength="4" Placeholder="YYYY" width="25%" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" class="textBox_Atul" />
	            		
					</td>
				</tr>
				<tr>
					<td style="border-right:0px;color:rgb(4,163,233);padding-left:20px;" class="topPaddingBetweenObjects_Atul">Player Contact:</td>										 
				</tr>
				<tr>
					<td style="border-right:0px;">
						<asp:TextBox id="txtContact" Text="" runat="server" width="90%" MaxLength="10" TextMode="Number"  onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" class="textBox_Atul" />
					</td>
				</tr>									
				<tr>
					<td style="border-right:0px;color:rgb(4,163,233);padding-left:20px;" class="topPaddingBetweenObjects_Atul">Player Email ID: </td>										 
				</tr>
				<tr>
					<td style="border-right:0px;">
						<asp:TextBox id="txtEmailID" Text="" runat="server" width="90%" MaxLength="50" class="textBox_Atul" />
					</td>
				</tr>
				
				<tr style="display:none;">
					<td style="border-right:0px;color:rgb(4,163,233);padding-left:20px;" class="topPaddingBetweenObjects_Atul">Player State:</td>														
				</tr>
				<tr style="display:none;">
					<td style="border-right:0px;">
						<asp:DropDownList ID="ddlStates" runat="server" width="90%" class="dropdown_Atul">
			                <asp:ListItem Text="Maharashtra" Value="1"></asp:ListItem>		                
	            		</asp:DropDownList>					
					</td>
				</tr>
				<tr style="display:none;">
					<td style="border-right:0px;color:rgb(4,163,233);padding-left:20px;" class="topPaddingBetweenObjects_Atul">Player District:</td>														
				</tr>
				<tr style="display:none;">
					<td style="border-right:0px;">
						<asp:DropDownList ID="ddlDistricts" runat="server" width="90%" class="dropdown_Atul">
			                <asp:ListItem Text="Pune" Value="1"></asp:ListItem>								                                
	            		</asp:DropDownList>					
					</td>
				</tr>
				<tr style="display:none;">
					<td style="border-right:0px;color:rgb(4,163,233);padding-left:20px;" class="topPaddingBetweenObjects_Atul">Player City:</td>										 
				</tr>
				<tr style="display:none;">
					<td style="border-right:0px;">
						<asp:TextBox id="txtCity" Text="Pune" runat="server" width="90%" MaxLength="20" readonly=true class="textBox_Atul"/>
					</td>
				</tr>
				<tr>
					<td style="border-right:0px;color:rgb(4,163,233);padding-left:20px;" class="topPaddingBetweenObjects_Atul">Player Address: </td>										
				</tr>											
				<tr>
					<td style="border-right:0px;">
						<asp:TextBox id="txtAddress" Text="" runat="server" width="90%" MaxLength="140" class="textBox_Atul"/>
					</td> 
				</tr>
				<tr>
					<td style="border-right:0px;color:rgb(4,163,233);padding-left:20px;" class="topPaddingBetweenObjects_Atul">T-Shirt Size: </td>										
				</tr>
				<tr>
					<td style="border-right:0px;">
						<asp:DropDownList ID="ddlTShirtSize" runat="server" width="90%" class="dropdown_Atul">
			                <asp:ListItem Text="Select T-Shirt Size" Value="0"></asp:ListItem>								                
			                <asp:ListItem Text="Small (S)" Value="1"></asp:ListItem>
			                <asp:ListItem Text="Medium (M)" Value="2"></asp:ListItem>
			                <asp:ListItem Text="Large (L)" Value="3"></asp:ListItem>
			                <asp:ListItem Text="Extra Large (XL)" Value="4"></asp:ListItem>
	            		</asp:DropDownList>					
					</td>
				</tr>
		
				<tr>
					<td style="border-right:0px;">
						<asp:Button id="btnUpdatePlayer" Text="Update Player Details" runat="server" OnClick="btnUpdatePlayer_Click" class="button_Atul" />
		
					</td>
				</tr>
			</table>		

		</asp:Panel>
		
		<asp:Panel runat="server" id="pnlDocTypeMsg" visible="false" width="100%" class="panelRegistration_Atul">					
					<asp:Label id="lblDocTypeMsg" runat="server" text="Upload the Govt ID Proof" />
		</asp:Panel>
		
		<asp:Panel runat="server" id="pnlDocType" visible="false" width="100%" class="panelRegistration_Atul">					
			<div style="text-align:center;" class="h3Text_Atul">		
				<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    				<ContentTemplate>
						<asp:Label id="lblDocType" runat="server" text="Upload the Govt ID Proof" />
					
						<br /><br />
					    <asp:Label id="lblSelectDocType" runat="server" text="Select Document Type" />
					
						<asp:DropDownList ID="ddlDocType" runat="server" width="40%" class="dropdown_Atul">			                
			                <asp:ListItem Text="PAN Card" Value="PAN Card"></asp:ListItem>
			                <asp:ListItem Text="Aadhar Card" Value="Aadhar Card"></asp:ListItem>
			                <asp:ListItem Text="Driving Licence" Value="Driving Licence"></asp:ListItem>
			                <asp:ListItem Text="Passport Copy" Value="Passport Copy"></asp:ListItem>
			                <asp:ListItem Text="Company ID" Value="Company ID"></asp:ListItem>
	        	   		</asp:DropDownList>					
	
					    <br /><br />
					    <div style="text-align:center;padding-left:50px;">
							<asp:FileUpload id="FileUploadControl" runat="server"/>
					    </div>
				    
					    <br /><br/>
					    <asp:Button runat="server" id="btnUploadButton" text="Show Image" onclick="UploadButton_Click" class="button_Atul" width="250px" />
				    
				    <br /><br />
				    
					    <div style="height:auto;width:100%;">
					    	<asp:HiddenField id="hfContentType" runat="server" />
					    	<asp:Image id="imgProof" runat="server" style="width:30%;height:30%;"  />
					    </div>				    
		    			 <asp:Button runat="server" id="btnSaveImage" text="Save Image in DB" onclick="btnSaveImage_Click" class="button_Atul" width="250px" />
				    
				    	<asp:Button runat="server" id="btnSaveImage" text="Download Image" onclick="btnDownloadImage_Click" class="button_Atul" width="250px" />
				    
		    			<asp:Label runat="server" id="lblStatusLabel" text="Upload status: " style="color:red;"   visible="false" />
				 	</ContentTemplate>
					<Triggers>
	    			    <asp:PostBackTrigger ControlID = "btnUploadButton" />
    				</Triggers>			
				</asp:UpdatePanel>
			</div>
		</asp:Panel>
	</div>		
	<hr/>
</div>

<div style='width:100%; text-align:center;padding-top:16px; padding-bottom:20px; font: 14pt calibri; color:gray;background:rgb(235,235,235);'>
Advertisement
</div>

</asp:Content>
