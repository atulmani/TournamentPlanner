using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TP.DAL;
using TP.Entity;
using System.Globalization;
using System.Text;
using System.IO;

namespace TournamentPlanner
{
    public partial class TP_PlayerUpdate : System.Web.UI.Page
    {
    	//string strSportCode = "";
    	string strTournamentCode = "";
    	//string strMatchID = "";
    	//string strOwnerUserID = "";
    	string strFilterType = "";
		int const_FileSize = 10240000;
		
    	//List<String> lstObjEvents;
    	//DataTable dtPlayerParticipation;
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        	//pnlErrorMsg.Visible = false;
        	pnlPlayerDetails.Visible = false;
			try
			{				
				strTournamentCode = Request.QueryString["TCode"];
				strTournamentCode = MGCommon.MGGeneric.DecryptData(strTournamentCode.Trim());
	        	
				if (Session["USERID"] != null ||
				    Session["USERTYPE"] != null)
				{	
					
				//	GenerateParticipatedEvents ();
					strFilterType = rblSelection.SelectedValue;
					if(hdType.Value != strFilterType)
					{
					
						GetPlayerList( strFilterType);
						hdType.Value = strFilterType;
					}
				}
				else
				{
					Response.Redirect("./TP_Login.aspx", false);
				}

			}
			catch (Exception ex)
			{
				System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "SF_RegistrationForm.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
                Response.Redirect("./TP_Login.aspx");
			}
        }
        
        protected void GetPlayerList(string strFilter)
        {
        	try
			{
				List<TPKeyValue> lstObj ;
				lstObj = (new TPDAL_Tournament()).GetPlayerLists(strTournamentCode);
				ddlPlayer.DataSource = lstObj;
		
								
				if(strFilterType == "PlayerCode")
				{
					ddlPlayer.DataValueField = "strKey";
					ddlPlayer.DataTextField = "strValue";
					
				}
				else
				{
					ddlPlayer.DataValueField = "strKey";
					ddlPlayer.DataTextField = "strKey";
					
				}
				ddlPlayer.DataBind();
					
			}
			catch (Exception ex)
			{
				System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_PlayerUpdate.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);                
			}
        }

private string ValidateEmail(string strEmailTemp)
        {
        	string strFinalEmail = "";
        	strFinalEmail = strEmailTemp;
        	try {
        	
	        	if(strFinalEmail.Contains(" "))
	        	{
	        		//remove extra charactor
	        		if(strFinalEmail.Contains("@"))
	        		{
	        			string [] strEmailList = strFinalEmail.Split(' ');
	        			for (int index = 0 ; index < strEmailList.Length ; index++)
	        			{
	        				if(strEmailList[index].Contains("@"))
	        				{
	        					strFinalEmail = strEmailList[index];
	        					break;
	        				}
	        			}
	        		}
	        	}
        	}
        	catch (Exception ex)
            {
                string strExMsg = ex.Message;
                //System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                //MGErrorMsg objError = new MGErrorMsg("Error", 0, "SF_RegistrationFormCorporate.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                //MGError objLogError = new MGError();
                //objLogError.logError(objError);
            }
        	
        	return strFinalEmail;
        	
        }
                
        private bool UIValidationforPlayerDetails ()
        {
        	bool bFlag = true;
        	
        	try {
        	
        	String strFirstName = txtFirstName.Text.Trim();
        	String strLastName = txtLastName.Text.Trim();
        	String strContact = txtContact.Text.Trim();
        	String strEMailID = ValidateEmail(txtEmailID.Text.Trim());
        	String strAddress = txtAddress.Text.Trim();
        	String strSelectedGender = ddlGender.SelectedItem.Text;
        	String strTShirtSize = ddlTShirtSize.SelectedItem.Text;
        	String strDOB ; 
        	
        	strDOB = ddlDate.SelectedItem.Text +"/"+ ddlMonth.SelectedValue +"/"+ txtDOBYear.Text;
	       	
        	DateTime dt;
			bool valid = DateTime.TryParseExact(strDOB, "dd/MM/yyyy", null, DateTimeStyles.None, out dt);
			 				
        	//strEMailID = ValidateEmail(strEMailID);
        	
	        	if (string.IsNullOrEmpty(strFirstName) ||
	                string.IsNullOrEmpty(strLastName) ||
	                string.IsNullOrEmpty(strContact) ||
	                string.IsNullOrEmpty(strEMailID) ||
	                //string.IsNullOrEmpty(txtBirthdate.Text) ||
	                string.IsNullOrEmpty(strAddress)||
	                string.IsNullOrEmpty(strTShirtSize) ||
	                strSelectedGender.Equals("Select Gender"))
	            {
	            	//Show error message
					pnlErrorMsg.Visible = true;
	            	lblErrorMsg.Text = "Complete Player Details Form"; 
					//pnlPlayerDetails.Visible = false; 
									
	            	
	            	bFlag = false;
	            }
	        	else 
	        	{
	        		if (!valid)
	        		{
						bFlag = false;
						//Show error message
						pnlErrorMsg.Visible = true;
	                	lblErrorMsg.Text = "Please select valid Date of birth for Player";  
			        	//pnlPlayerDetails.Visible = false; 
						
						bFlag = false;
					}
	        	}
        	}
			catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "SF_RegistrationFormCorporate.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
            }
        	return bFlag;
        }
        

    	protected void UploadButton_Click(object sender, EventArgs e)
		{
        	
    		if(FileUploadControl.HasFile)
		    {
		        try
		        {
		        	if(FileUploadControl.PostedFile.ContentType == "image/jpeg" || 
		        	   FileUploadControl.PostedFile.ContentType == "image/png" ||
		        	  FileUploadControl.PostedFile.ContentType == "application/pdf" )
		            {
		                if(FileUploadControl.PostedFile.ContentLength < const_FileSize)
		                {
		                    string filename = Path.GetFileName(FileUploadControl.FileName);
		                    ShowImage(FileUploadControl.PostedFile.ContentType);
		                    
		                }
		                else
		                {
		                    //lblStatusLabel.Text = "Upload status: The file has to be less than 100 kb!";
		                    
		    				pnlErrorMsg.Visible = true;
    						lblErrorMsg.Text = "Upload status: The file has to be less than 100 kb!";
    		
		                }
		            }
		            else
		            {
		            //    lblStatusLabel.Text = "Upload status: Only JPEG/PNG files are accepted!";
	            		pnlErrorMsg.Visible = true;
						lblErrorMsg.Text = "Upload status: Only JPEG/PNG files are accepted!";
		
		            }
		        }
		        catch(Exception ex)
		        {
		    		pnlErrorMsg.Visible = true;
    				lblErrorMsg.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
    		
		        	//lblStatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
		        }
		    }
    		else
    		{
    			pnlErrorMsg.Visible = true;
    			lblErrorMsg.Text = "Please select ID proof for registration to be completed";
    		}
		}
        
    	
    	protected void btnDownloadImage_Click(object sender, EventArgs e)
		{
			try
			{
        
        		//SaveImageinSystem(hfContentType.Value , "Download");
			}
			catch (Exception ex)
			{
				System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_PlayerUpdate.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);                
			}
        }
        protected void btnSaveImage_Click(object sender, EventArgs e)
		{
			try
			{
        
        		SaveImageinDB(hfContentType.Value , "DB");
			}
			catch (Exception ex)
			{
				System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_PlayerUpdate.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);                
			}
        }
        
        protected void SaveImageinDB(string strContentType,string strSavein )
		{
			try
			{
		    	 if(imgProof.ImageUrl != "")
	             {
		    	 	byte[] imagebyt = Convert.FromBase64String(imgProof.ImageUrl.Replace("data:"+strContentType+";base64,", ""));
						
		    	 	if(strSavein == "DB")
		    	 	{
		                //lblStatusLabel.Text = "Upload status: File uploaded!";
		          		//byte[] imagebyt = new byte[ const_FileSize];
						string strDocType = "";
						strDocType = ddlDocType.SelectedValue;
		          		//imagebyt = imgProof.ImageUrl;
						//imagebyt = File.ReadAllBytes(imgProof.ImageUrl);
						//imgProo
						(new TPDAL_Registration()).SaveImagetoDB(strTournamentCode, ddlPlayer.SelectedValue, imagebyt, strContentType, strDocType);
		    	 	}
		    	 	else
		    	 	{
	    	 		    //objItemMgmt.SaveAddionalImage(iItemID, iImageCnt - 1, imgByte);
                        MemoryStream ms;
                        System.Drawing.Image image;
                        string strDestFile = "";
        
    
                        ms = new MemoryStream(imagebyt);
                        image = System.Drawing.Image.FromStream(ms);
                        //strDestFile = Server.MapPath("/Images/Products/Item_" + iItemID.ToString() + "_" + iImageCnt.ToString() + ".jpg");
                        image.Save(strDestFile);
                        
         
		    	 	}
		    	 }
			}
		    catch (Exception ex)
            {
		    	System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "SF_PaymentPlayersParticipation.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
            }
		
		}
        protected void btnUpdatePlayer_Click(object sender, EventArgs e)
		{
			try
			{
				if(UIValidationforPlayerDetails())
				{
					string strPlayerCode = "";
					//int iDate = 0;
					//int iMonth = 0;
					//int iYear = 0;
					//DateTime dt ;
					strPlayerCode = ddlPlayer.SelectedItem.Value;
					TPPlayer objPlayer = new TPPlayer();
					objPlayer.PlayerCode = ddlPlayer.SelectedValue;
					objPlayer.PlayerFName = txtFirstName.Text;
					objPlayer.PlayerLName = txtLastName.Text;
					objPlayer.PlayerEmailID = txtEmailID.Text;
					objPlayer.PlayerContact = txtContact.Text;
					objPlayer.PlayerGender = ddlGender.SelectedItem.Text;
					objPlayer.PlayerState = ddlStates.SelectedValue;
					objPlayer.PlayerDistrict = ddlDistricts.SelectedValue;
					objPlayer.PlayerCity = txtCity.Text;
					objPlayer.PlayerAddress = txtAddress.Text;
					objPlayer.TShirtSize = ddlTShirtSize.SelectedValue;
					string strPlayerDOB = ddlDate.SelectedItem.Text +"/"+ ddlMonth.SelectedValue +"/"+ txtDOBYear.Text;
	        		objPlayer.PlayerDOB = strPlayerDOB;
						
					int iFlag = (new TPDAL_PlayerDetails()).GetUpdatePlayerDetailsWithPlayerCode(strTournamentCode, objPlayer);
					pnlDocType.Visible = true;
					GetDocumentProof();
					
				}
			}
			catch (Exception ex)
			{
				System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_PlayerUpdate.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);                
			}
		}	
        
        private void GetDocumentProof()
        {
			try{
        		TPDocumentProof objDocument = (new TPDAL_Registration()).GetDocumentProof(strTournamentCode, ddlPlayer.SelectedValue );
        		if ( objDocument!= null)
        		{
    			     if(objDocument.document != null && objDocument.ContentType != null && 
        			   objDocument.ContentType != "" )
        			{
        				string base64String = Convert.ToBase64String(objDocument.document , 0, objDocument.document.Length);
                    	imgProof.ImageUrl = "data:"+objDocument.ContentType+";base64," + base64String;

                     	imgProof.Visible = true;
            			lblDocType.Text = "you have uploaded : " + objDocument.DocumentType ;
                     	pnlDocTypeMsg.Visible = true;
                     	lblDocTypeMsg.Text = "you have uploaded : " + objDocument.DocumentType ;
                     	//pnlDocType.Visible = false;
            			//FileUploadControl.Visible = false;
                      	//btnUploadButton.Visible = false;
				    	//ddlDocType.Visible	 = false;
				    	//imgProof.Visible = false;
                       	hfContentType.Value = objDocument.ContentType;
                       	//lblSelectDocType.Visible = false;
         			}
        		
        		}
        		else
        		{
        			pnlDocType.Visible = true;
        			pnlDocTypeMsg.Visible = false;
        		}
			}
			  catch (Exception ex)
            {
			  	System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "SF_PaymentPlayersParticipation.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
            }
        }
        protected void ShowImage(string strContentType)
        {
            FileUpload fileup = null;
            System.Web.UI.WebControls.Image img = null;
            HttpPostedFile File = null;
            //Button btnDeleteObj = null;
            fileup = (FileUpload)FileUploadControl;
            if (fileup.HasFile && fileup.PostedFile != null)
            {
                //To create a PostedFile
                File = fileup.PostedFile;
            }
            img = imgProof;
            

            if (fileup != null)
            {
                try
                {

                    Byte[] imgByte = null;
                    if (fileup.HasFile && fileup.PostedFile != null)
                    {
                        if (File.ContentLength < const_FileSize)
                        {
                            imgByte = new Byte[File.ContentLength];
                            //force the control to load data in array
                            File.InputStream.Read(imgByte, 0, File.ContentLength);

                            string base64String = Convert.ToBase64String(imgByte, 0, imgByte.Length);
                            imgProof.ImageUrl = "data:"+strContentType+";base64," + base64String;

                            imgProof.Visible = true;
                           	hfContentType.Value = strContentType;
                           // MemoryStream ms;
                          //  System.Drawing.Image image;
                            //string strDestFile = "";
            
                          //  ms = new MemoryStream(imgByte);
                          //  image = System.Drawing.Image.FromStream(ms);
                            //strDestFile = Server.MapPath("/Images/Products/Item_" + iItemID.ToString() + "_" + iImageCnt.ToString() + ".jpg");
                            //image.Save(strDestFile);
                          //  btnDeleteObj.Enabled = true;
                            
                        }
                        else
                        {
                            //Show error in UI that file size is mor ethat 150KB
                            
							pnlErrorMsg.Visible = true;
							lblErrorMsg.Text = "Image size is more , please file size less than 400KB";
                        }
                    }



                }
                catch (Exception ex)
                {
                	System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                	MGErrorMsg objError = new MGErrorMsg("Error", 0, "SF_PaymentPlayersParticipation.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                	MGError objLogError = new MGError();
                	objLogError.logError(objError);
                }
            }
        }

        protected void btnGetPlayerList_Click(object sender, EventArgs e)
		{
			try
			{
				string strPlayerCode = "";
				//int iDate = 0;
				//int iMonth = 0;
				int iYear = 0;
				DateTime dt ;
				strPlayerCode = ddlPlayer.SelectedItem.Value;
				TPPlayer objPlayer = (new TPDAL_PlayerDetails()).GetPlayerDetailsWithPlayerCode(strTournamentCode, strPlayerCode);

				if(objPlayer !=null)
				{
                    txtFirstName.Text = objPlayer.PlayerFName;
                    txtLastName.Text = objPlayer.PlayerLName;
                    if (objPlayer.PlayerDOB != null)
					{
                        bool IsDOB = DateTime.TryParseExact(objPlayer.PlayerDOB, "dd/MM/yyyy", null, DateTimeStyles.None, out dt);
						if(IsDOB )
						{
							string strdateformat = "dd/MM/yyyy";
                            dt = DateTime.ParseExact(objPlayer.PlayerDOB, strdateformat, CultureInfo.InvariantCulture);
							ddlDate.ClearSelection();
							string str = dt.Day.ToString();
							if(str.Trim().Length < 2)
								str = "0" + str;	
							if(ddlDate.Items.FindByValue(str) !=null)
								ddlDate.Items.FindByValue(str).Selected = true;
							
							ddlMonth.ClearSelection();
							str = dt.Month.ToString();
							if(str.Trim().Length <2)
								str = "0" +  str;
							if(ddlMonth.Items.FindByValue(str) !=null)
								ddlMonth.Items.FindByValue(str).Selected= true;
							
							iYear = dt.Year;
							txtDOBYear.Text = iYear.ToString();
						}
					}
		
					ddlGender.ClearSelection();
                    if (ddlGender.Items.FindByText(objPlayer.PlayerGender) != null)
                        ddlGender.Items.FindByText(objPlayer.PlayerGender).Selected = true;

                    txtContact.Text = objPlayer.PlayerContact;
                    txtEmailID.Text = objPlayer.PlayerEmailID;
					ddlStates.ClearSelection();
                    if (ddlStates.Items.FindByValue(objPlayer.PlayerState) != null)
                        ddlStates.Items.FindByValue(objPlayer.PlayerState).Selected = true;

                    if (ddlDistricts.Items.FindByValue(objPlayer.PlayerDistrict) != null)
                        ddlDistricts.Items.FindByValue(objPlayer.PlayerDistrict).Selected = true;

                    txtCity.Text = objPlayer.PlayerCity;
                    txtAddress.Text = objPlayer.PlayerAddress;
					ddlTShirtSize.ClearSelection();
                    if (ddlTShirtSize.Items.FindByValue(objPlayer.TShirtSize) != null)
                        ddlTShirtSize.Items.FindByValue(objPlayer.TShirtSize).Selected = true;
				}
				pnlPlayerDetails.Visible = true;
			}
			catch (Exception ex)
			{
				System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_PlayerUpdate.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);                
			}
		}
        
        /*
      	protected void DateChange(object sender, EventArgs e)
	    {
	        //txtBirthdate.Text = Calendar1.SelectedDate.ToShortDateString() + '.';
	    }
      
      	protected void CustomValidator1_ServerValidate(object sender, ServerValidateEventArgs e)
		{
		    DateTime d;
		    e.IsValid = DateTime.TryParseExact(e.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out d);
		}
        
        /// <summary>
        /// PopulateTournamentSummary -- The header part
        /// </summary>
        private void PopulateTournamentSummary ()
        {
        	try
        	{
	        	List<TPTournament> lstObj = new List<TPTournament>();
				lstObj = (new TPDAL_Tournament()).GetTournamentLists(strSportCode, strTournamentCode, "");
				
				//dgTournamentList.DataSource = lstObj;
				//dgTournamentList.DataBind();
				if (lstObj != null && lstObj.Count > 0)
				{
					lblTournamentName.Text = lstObj[0].TournamentName;
					lblEntryOpenDate.Text = lstObj[0].TournamentEntryOpenDate.ToString ("dddd, dd MMMM yyyy HH:mm") + " IST";
					lblEntryClosesDate.Text = lstObj[0].TournamentEntryCloseDate.ToString ("dddd, dd MMMM yyyy HH:mm") + " IST";
					lblEntryWithdrawalDate.Text = lstObj[0].TournamentEntryWithdrawlDate.ToString ("dddd, dd MMMM yyyy HH:mm") + " IST";
					lblTournamentDuration.Text = lstObj[0].TournamentStartDate.ToString ("dddd, dd MMMM yyyy") + " -TO- " + lstObj[0].TournamentEndDate.ToString ("dddd, dd MMMM yyyy");
					lblTournamentOrganisation.Text =  lstObj[0].TournamentOrganisation;
					lblTournamentVenue.Text = lstObj[0].TournamentVenue;
					lblLocationAddress.Text = lstObj[0].TournamentLocationAddress; 
					lblTournamentContacts.Text = lstObj[0].TournamentPOCContactNames + " " + lstObj[0].TournamentPOCContactNo + " " + lstObj[0].TournamentLocationContactNo;
				}
        	}
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "SF_RegistrationForm.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        
        }
        
        #region Curtains Expand Collapse
        
		private void PlayerDetailsCollapse()
		{
			btnTogglePlayerDetailsPlus.Text = "+";
			pnlPlayerDetails.Visible = false;
		}
		
		private void PlayerDetailsExpand()
		{
			btnTogglePlayerDetailsPlus.Text = "-";
			pnlPlayerDetails.Visible = true;
			AddEventCollapse();
		}
		
		protected void btnTogglePlayerDetails_Click(object sender, EventArgs e)
		{
			try
			{
				if (btnTogglePlayerDetailsPlus.Text.Equals("+"))
				{				
					PlayerDetailsExpand();					
				}
				else
				{				
					PlayerDetailsCollapse();
					SummaryCollapse();
				}			
			}
			catch (Exception ex)
			{
				System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "SF_RegistrationForm.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);                
			}
		}
		
		private void AddEventCollapse()
		{
			btnToggleAddEventPlus.Text = "+";
			pnlAddEvent.Visible = false;
		}
		
		private void AddEventExpand()
		{
			btnToggleAddEventPlus.Text = "-";
			
			chkbDistrictReg.Checked = false;
			lblTotalAmount.Text = "";
			
			pnlAddEvent.Visible = true;
		}
		
		protected void btnToggleAddEvent_Click(object sender, EventArgs e)
		{
			try
			{
				if (btnToggleAddEventPlus.Text.Equals("+"))
				{	
					if (UIValidationforPlayerDetails())
					{
						//chkbPlayer.Text = txtFirstName.Text + " " + txtLastName.Text; 
						AddEventExpand();
						PlayerDetailsCollapse();
						SummaryCollapse();
						PopulateEventForm ();
					}
				}
				else
				{								
					AddEventCollapse();
					SummaryCollapse();
				}
			}
			catch (Exception ex)
			{
				System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationForm.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);                
			}
		}
		
		private void SummaryCollapse()
		{
			btnToggleSummaryPlus.Text = "+";
			pnlSummary.Visible = false;
		}
		
		private void SummaryExpand()
		{
			btnToggleSummaryPlus.Text = "-";
			pnlSummary.Visible = true;
		}
		
		protected void btnToggleSummary_Click(object sender, EventArgs e)
		{
			SummaryExpand();
			AddEventCollapse();
			PopulateSummary();
		}
        
		protected void btnViewSummary_Click(object sender, EventArgs e)
		{
			SummaryExpand();
			AddEventCollapse();
			PopulateSummary();
		}
		
		
		#endregion Curtains Expand Collapse

		#region Event Details		
		
		protected void chkview_CheckedChanged(object sender, EventArgs e)
		{
			try
        	{
			   //CheckBox cb1= ((CheckBox)sender);
			   //HiddenField hfFees = (HiddenField)((CheckBox)sender).NamingContainer.FindControl("hfFees");
			//    CheckBox cb1 = (CheckBox)dgEventParticipation.SelectedItem.FindControl("chkview");
			    //string yourvalue = cb1.Text;
			  //  HiddenField hfFees = (HiddenField)dgEventParticipation.SelectedItem.FindControl("hfFees");
			    //string str = hfFees.Value;
			    //here you can find your control and get value(Id).
			    
			    int iRowCount = dgEventParticipation.Items.Count;
			    int iTotalFees = 0;
			    for (int i = 0; i < iRowCount; i++)
			    {
			    	HiddenField hfFees = (HiddenField)dgEventParticipation.Items[i].FindControl("hfFees");
			    	CheckBox cb1 = (CheckBox)dgEventParticipation.Items[i].FindControl("dgcbEventSelect");
			    	if (cb1.Checked)
			    	{
			    		string strFees = hfFees.Value;
			    		int iFees = int.Parse(strFees);
			    		iTotalFees = iTotalFees + iFees;
			    	}
			    }
			    
			    if(chkbDistrictReg.Checked)
			    	iTotalFees += 30;
				lblTotalAmount.Text = iTotalFees.ToString();
			   
			}
			catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "SF_RegistrationForm.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}		    
		}		
		
		/// <summary>
		/// Populate form with all the registered events for tournament
		/// </summary>
		///
		private void PopulateEventForm ()
		{
			
			try
        	{
				if(UIValidationforPlayerDetails())
				{
					string strGender = ddlGender.SelectedValue;
		        	CultureInfo provider = CultureInfo.InvariantCulture;
	
	
		        	//Reference Date for Tournament Age Limit
		        	DateTime dtReferenceAgeDate = new DateTime(2018,01,01);
		        	//if(txtBirthdate.Text .Trim() != "")
		        	{
		        		string strPlayerDOB = ddlDate.SelectedItem.Text + "/" + ddlMonth.SelectedValue + "/" + ddlYear.SelectedItem.Text;
		        	
		        		DateTime dtDOB = DateTime.ParseExact(strPlayerDOB,"dd/MM/yyyy", provider);	        	
		        		int iAgeLimit = dtReferenceAgeDate.Year - dtDOB.Year ;//difference between dtDOB - dtReferenceAgeDate;
		        	
						List<TPEventRegistration> objEvents= (new TPDAL_Events()).GetAllEventsBySportANDTournamentForRegistration(strSportCode, strTournamentCode, strGender, iAgeLimit);
							        	
						if (objEvents != null)
						{
							if (objEvents.Count > 0)
							{
								lblTournamentEvents.Text = "Tournament Events";
								dtPlayerParticipation = new DataTable("PlayerParticipation");
								dtPlayerParticipation.Columns.Add("ID");
								dtPlayerParticipation.Columns.Add("TournamentCode");
								dtPlayerParticipation.Columns.Add("Event");
								dtPlayerParticipation.Columns.Add("Fees");					
								dtPlayerParticipation.Columns.Add("PartnerName");
								dtPlayerParticipation.Columns.Add("PartnerDOB");
								
								for (int i = 0; i < objEvents.Count; i++)						                    
			                    {					        	
						        	
									String strEventName =  objEvents[i].EventName;
					        		
									String strFeesAmount = objEvents[i].EventRate.ToString();
					        		
									String strID = (dtPlayerParticipation.Rows.Count+1).ToString();
					        		   
					        		String strPartnerName = "";
					        			
					        		String strPartnerDOB = "";
					        		
									dtPlayerParticipation.Rows.Add(strID, strTournamentCode, strEventName, strFeesAmount, strPartnerName, strPartnerDOB);				
						        	
								}
								
								Session["PLAYERPARTICIPATION"] = dtPlayerParticipation;
								dgEventParticipation.DataSource = dtPlayerParticipation;
								dgEventParticipation.DataBind();
									
								
								for (int i = 0; i < objEvents.Count; i++)						                    
								{
									String strEventName =  objEvents[i].EventName;							
					        		//int iIndexOfOpenBracket = strEventDetails.IndexOf ("(");
					        		//int iIndexOfCloseBracket = strEventDetails.IndexOf (")");
					        		String strEventSingle = strEventName.Substring (1, 1);					
									
					        		if (strEventSingle.Equals("S"))
					        		{
					        			TextBox tbPartnName = (TextBox)dgEventParticipation.Items[i].FindControl("dgtbPartnerName");
					        			tbPartnName.Enabled = false;			        			
					        			
					        			TextBox tbPartnerDOB = (TextBox)dgEventParticipation.Items[i].FindControl("dgtbPartnerDOB");
					        			tbPartnerDOB.Enabled = false;			        			
					        		}
								}
								
							}
							else
							{
								lblTournamentEvents.Text = "There is no Event/Category matching as per the Player BirthDate";
							}
						}
		        	}
				}
				else
				{
						//bFlag = false;
	                	lblErrorMsg.Text = "Birthdate format is incorrect. Please enter value in dd/mm/yyyy format"; 
				}
        	}
        	
        	
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "SF_RegistrationForm.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}		
		}
		
		#endregion Event Details
		
		#region Obsolete Methods
		
		private void PopulateEvents ()
        {
        	try
        	{
	        	//Get all the player list from database
	        	String strEvents = (new TPDAL_Events()).GetAllEventsBySportANDTournament(strSportCode, strTournamentCode);
				
				String[] lstObj = strEvents.Split(new []{"; "}, StringSplitOptions.None);
	        	int iEventCount = lstObj.Length - 1;
	        	
	        	List<TPEvent> lstEventObj = new List<TPEvent>();
	        	TPEvent obj = null;
				
	        	String strEventName = "";
				
	            if (lstObj != null)   
	            {   
	            	if (iEventCount > 0)
	                {
	                    for (int i = 0; i < iEventCount; i++)						                    
	                    {   
	                    	strEventName = lstObj[i];
	                    	
	                    	obj = new TPEvent();
	                    	obj.EventName = strEventName;
	                    	obj.EventCode = strEventName;
	                    	lstEventObj.Add(obj);
	                    }   	                       
	                }
	            }
				
				ddlEvents.DataSource = lstEventObj;
				ddlEvents.DataTextField = "EventName";
        		ddlEvents.DataValueField = "EventCode";
				ddlEvents.DataBind();	            
        	}
        	
        	
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "SF_RegistrationForm.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        }
				
        protected void btnAddEvent_Click(object sender, EventArgs e)
        {
        	try
        	{	        	
	        	String strSelectedEvent = ddlEvents.SelectedItem.Text;
	        	
	        	bool isDuplicateEvent = CheckDuplicateEntry (strSelectedEvent);
	        	
	        	if (!isDuplicateEvent)
	        	{
		        	//lstObjEvents.Add (strSelectedEvent);
		        	dtPlayerParticipation = (DataTable)Session["PLAYERPARTICIPATION"];
		        	
		        	//for (int i = 0; i < lstObjEvents.Count; i++)
		        	{
		        		//String strEventDetails = strSelectedEvent ;//lstObjEvents[0].ToString();
		        		//int iIndexOfOpenBracket = strEventDetails.IndexOf ("(");
		        		//int iIndexOfCloseBracket = strEventDetails.IndexOf (")");
		        		//String strEventName = strEventDetails.Substring (0, iIndexOfOpenBracket);
		        		String strEventName = strSelectedEvent; 
		        		//String strFeesAmount = strEventDetails.Substring (iIndexOfOpenBracket+1, strEventDetails.Length - iIndexOfCloseBracket-4);
		        		String strFeesAmount = "0";
		        		String strID = (dtPlayerParticipation.Rows.Count+1).ToString();
		        		   
		        		String strPartnerName = txtPartnerFName.Text + " " + txtPartnerLName.Text;
		        			
		        		String strPartnerDOB = txtPartnerDOB.Text;
		        		
						dtPlayerParticipation.Rows.Add(strID, strTournamentCode, strEventName, strFeesAmount, "PartnerCode", strPartnerName, strPartnerDOB);				
		        	}
		        	
		        	Session["PLAYERPARTICIPATION"] = dtPlayerParticipation;
					
					dgEventParticipation.DataSource = dtPlayerParticipation;
					dgEventParticipation.DataBind();
					
					Session["PLAYERPARTICIPATION"] = dtPlayerParticipation;
	        	}
	        	else
	        	{
	        		//Duplicate Event
	        		pnlErrorMsg.Visible = true;
	        		lblErrorMsg.Text = "Event already added into your list";
	        	}
        	}
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "SF_RegistrationForm.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        	
        }
                
        private bool CheckDuplicateEntry (string strEventName)
        {
        	bool IsDuplicate = false;
        	
        	DataTable dt =  (DataTable)Session["PLAYERPARTICIPATION"];
        	
        	int iRowCount = dt.Rows.Count;
        	for (int i = 0; i < iRowCount; i++)
        	{        		
        		string val = dt.Rows[i]["Event"].ToString();
        		if (val.Equals(strEventName))
        		{
        			IsDuplicate = true;
        			break;
        		}        		
        	}
        	
        	return IsDuplicate;
        }
        
        protected void dgEventParticipation_DeleteCommand(object source, DataGridCommandEventArgs e)
        {
        	int iRowIndex = e.Item.ItemIndex;
        	
        	DataTable dt =  (DataTable)Session["PLAYERPARTICIPATION"];
        	DataRow dr = dt.Rows[iRowIndex];
    	    dt.Rows.Remove(dr);
    	    
        	dgEventParticipation.DataSource = dt;
			dgEventParticipation.DataBind();
        }
        
        protected void ddlEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
        	try
        	{
	        	//String strEventDetails = ddlEvents.SelectedItem.Text;
	    		
	        	//int iIndexOfOpenBracket = strEventDetails.IndexOf ("(");
	    		//int iIndexOfCloseBracket = strEventDetails.IndexOf (")");
	    		//String strEventName = strEventDetails.Substring (0, iIndexOfOpenBracket);
	    		
	    		//String strFeesAmount = strEventDetails.Substring (iIndexOfOpenBracket+1, strEventDetails.Length - iIndexOfCloseBracket-4);
	    		
	    		//String strEventType = strEventDetails.Substring (iIndexOfCloseBracket+ 4);
	    		
	    		String strEventCode = ddlEvents.SelectedItem.Text; //"BS U11";
				String strEventType = strEventCode.Substring(1,1); //"S" for Singles & "D" for Doubles
				        	    		
	    		if (strEventType.Equals("S"))
	    		{
	    			pnlPartnerDetails.Visible = false;
	    		}
	    		else
	    		{
	    			pnlPartnerDetails.Visible = true;
	    		}
    		}
			catch (Exception ex)
			{
				System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "SF_RegistrationForm.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);                
			}
        	
        }
        
        #endregion Obsolete Methods
        
        #region Summary
        
        private void PopulateSummary()
        {
        	lblPlayerName.Text = txtFirstName.Text + " " + txtLastName.Text ;
        	lblPlayerGender.Text = ddlGender.SelectedItem.Text;
        	lblPlayerDOB.Text = ddlDate.SelectedItem.Text + "/" + ddlMonth.SelectedItem.Text + "/" + ddlYear.SelectedItem.Text;
        	lblPlayerContact.Text = txtContact.Text;
        	lblPlayerEmailID.Text = txtEmailID.Text;
        	        	
        	try
        	{ 
				//string strPlayerCode = "BD80250"; 
	        	//Get all the player list from database
	        	//List<TPPlayerParticipation> lstObj = new TPDAL_PlayerDetails().GetPlayerParticipationDetails(strTournamentCode, strPlayerCode);
				
	        	List<TPEvent> lstObjTPEvent = GetEventDetails();
	        	
	        	if (lstObjTPEvent.Count > 0)
	        	{
		        	dgPlayerParticilation.DataSource = lstObjTPEvent;
					dgPlayerParticilation.DataBind();
					
					if (chkbDistrictReg.Checked)
					{
						lblDistricRegistration.Text = "Selected";
					}
					else
					{
						lblDistricRegistration.Text = "Not Selected";
					}
					
					lblParticipationAmount.Text = "Total Amount Payable: " + lblTotalAmount.Text;
					lblEventMsg.Text = "";
					btnRegisterPay.Enabled = true;
	        	}
	        	else
	        	{
	        		lblEventMsg.Text = "No Event Selected, Please Select atleast One Event to Proceed";
	        		btnRegisterPay.Enabled = false;
	        		lblParticipationAmount.Text = "Rs. 0";
	        		lblDistricRegistration.Text = "Not Selected";
	        		dgPlayerParticilation.DataSource = null;
					dgPlayerParticilation.DataBind();
	        	}	        	
        	}
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "SF_PlayersParticipation.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        }
        
        #endregion
        
        #region Registration & Pay
        private string ValidateEmail(string strEmailTemp)
        {
        	string strFinalEmail = "";
        	strFinalEmail = strEmailTemp;
        	try {
        	
	        	if(strFinalEmail.Contains(" "))
	        	{
	        		//remove extra charactor
	        		if(strFinalEmail.Contains("@"))
	        		{
	        			string [] strEmailList = strFinalEmail.Split(' ');
	        			for (int index = 0 ; index < strEmailList.Length ; index++)
	        			{
	        				if(strEmailList[index].Contains("@"))
	        				{
	        					strFinalEmail = strEmailList[index];
	        					break;
	        				}
	        			}
	        		}
	        	}
        	}
        	catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "SF_RegistrationForm.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
            }
        	
        	return strFinalEmail;
        	
        }
        
        private bool UIValidationforPlayerDetails ()
        {
        	bool bFlag = true;
        	
        	try {
        		
        	
        	
        	String strFirstName = txtFirstName.Text.Trim();
        	String strLastName = txtLastName.Text.Trim();
        	String strContact = txtContact.Text.Trim();
        	String strEMailID = ValidateEmail(txtEmailID.Text.Trim());
        	String strAddress = txtAddress.Text.Trim();
        	String strSelectedGender = ddlGender.SelectedItem.Text;
        	//strEMailID = ValidateEmail(strEMailID);
        	
	        	if (string.IsNullOrEmpty(strFirstName) ||
	                string.IsNullOrEmpty(strLastName) ||
	                string.IsNullOrEmpty(strContact) ||
	                string.IsNullOrEmpty(strEMailID) ||
	                //string.IsNullOrEmpty(txtBirthdate.Text) ||
	                string.IsNullOrEmpty(strAddress)||
	                strSelectedGender.Equals("Select Gender"))
	            {
	            	//Show error message
					pnlErrorMsg.Visible = true;
	            	lblErrorMsg.Text = "Complete Player Details Form"; 
					pnlPlayerDetails.Visible = false; 
					btnTogglePlayerDetailsPlus.Text = "+";
									
	            	
	            	bFlag = false;
	            }
	        	else 
	        	{
	        		int iBirthdate = int.Parse(ddlDate.SelectedItem.Text);
	        			        		
	        		if (ddlDate.SelectedValue.Equals("0") || ddlMonth.SelectedValue.Equals("0") || ddlYear.SelectedValue.Equals("0"))
	        		{	        			
						bFlag = false;
						//Show error message
						pnlErrorMsg.Visible = true;
	                	lblErrorMsg.Text = "Please select birth month and year";  
			        	pnlPlayerDetails.Visible = false; 
						btnTogglePlayerDetailsPlus.Text = "+";							
					}
	        		else if (iBirthdate > 29 && ddlMonth.SelectedValue.Equals("02"))
	        		{	        			
						bFlag = false;
						//Show error message
						pnlErrorMsg.Visible = true;
	                	lblErrorMsg.Text = "Please enter correct date and month";  
			        	pnlPlayerDetails.Visible = false; 
						btnTogglePlayerDetailsPlus.Text = "+";							
					}
	        		
	        	}
        	}
			catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "SF_RegistrationForm.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
            }
        	return bFlag;
        }
        
        private void SendRegistrationEmail (string strPlayerCode)
        {
        	try
        	{
	        	StringBuilder sb = new StringBuilder();
	        	
	        	string strToEmailID = ValidateEmail(txtEmailID.Text);
	        	
	        	string strMyEmailSubject = "Corporate Shuttlers";
	        	string strEmailSubject = new MGCommon.EMailTemplate().MailSubjectLine(strMyEmailSubject);
	        		
		        List<TPEvent> lstObjTPEvent = (List<TPEvent>)Session["PARTICIPATIONEVENTS"];
		        
		        if (lstObjTPEvent != null)
		        {
		        	for(int i = 0; i < lstObjTPEvent.Count; i++)
		        	{
		        		sb.AppendLine(i+1 + "." + "Category: " + lstObjTPEvent[i].EventName);
		        	}
		        }
	        	
		        string strParticipationEvents = sb.ToString();
	        	
	        	string strHeader = new MGCommon.EMailTemplate().EMailHeader();
	        	
	        	string strPlayerDOB = ddlDate.SelectedItem.Text +"/"+ ddlMonth.SelectedValue +"/"+ ddlYear.SelectedItem.Text;
	        	string strBody = new MGCommon.EMailTemplate().TournamentRegistrationMailBody(txtFirstName.Text, txtLastName.Text, 
	        	                                                                             txtContact.Text, strParticipationEvents, 
	        	                                                                             lblTotalAmount.Text, strPlayerCode,strPlayerDOB, "Pending" );
	        	string strFooter = new MGCommon.EMailTemplate().EMailFooter();
	        	
	        	string strEmailBody = strBody + strFooter;
	        	
	        	MGCommon.MGGeneric.MGEMailServices(strToEmailID, strEmailSubject, strEmailBody);
        	}
            catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "SF_RegistrationForm.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
                Response.Redirect("./TP_BD_Home.aspx", false);
            }
        }
        
        protected void chkbDistrictReg_CheckedChanged(object sender, EventArgs e)
        {
        	try
        	{
        		int iTotalAmount = 0;
        		if(lblTotalAmount.Text == "")
        			iTotalAmount = 0;
        		else
	        		iTotalAmount = int.Parse(lblTotalAmount.Text);
	        	
        		if (chkbDistrictReg.Checked)
	        	{
	        		//if (iTotalAmount > 0)
	        		{
	        			iTotalAmount = iTotalAmount + 30;
	        			lblTotalAmount.Text = iTotalAmount.ToString();
	        		}
	        	}
	        	else
	        	{
	        		iTotalAmount = iTotalAmount - 30;
	        		lblTotalAmount.Text = iTotalAmount.ToString();
	        	}
        	}
            catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "SF_RegistrationForm.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
            }
        }
        
        protected bool UIValidatePartnerPlayerDetails()
        {
        	
        		bool bFlag = true;
        		string strEtName = "";
    			string strPartnFnm = "";
    			string strPartnLnm = "";
    			string strPartnDOB = "";
    			int iRowCount = 0;
    		try
            {
    			iRowCount = dgEventParticipation.Items.Count;
    				  	
    		//Get Player Participation Details in multiple events
			for(int i=0; i < iRowCount; i++)
			{
        		strEtName = "";
    			strPartnFnm = "";
    			strPartnLnm = "";
    			strPartnDOB = "";

				CheckBox cb1 = (CheckBox)dgEventParticipation.Items[i].FindControl("dgcbEventSelect");
				if (cb1.Checked)
				{
					Label lblEtName = (Label)dgEventParticipation.Items[i].FindControl("dgtbEventCode");
    				strEtName = lblEtName.Text; 
					if(strEtName.Contains("D"))
    				{
    					TextBox tbPartnName = (TextBox)dgEventParticipation.Items[i].FindControl("dgtbPartnerName");
    					TextBox tbPartnerDOB = (TextBox)dgEventParticipation.Items[i].FindControl("dgtbPartnerDOB");
    					if (!string.IsNullOrEmpty(tbPartnName.Text))
	    				{
	        				String [] strPartnerNames = tbPartnName.Text.Split (' ');
	        				
	        				
	        				if (!string.IsNullOrEmpty(strPartnerNames[0]))
	        				{
		        				strPartnFnm =  strPartnerNames[0];		        			
	        				}
	        				if (!string.IsNullOrEmpty(strPartnerNames[1]))
	        				{
		        				strPartnLnm = strPartnerNames[1];
	        				}
	        				
	        				strPartnDOB = tbPartnerDOB.Text;
	    				}

 				if (string.IsNullOrEmpty(strPartnFnm) ||
		                string.IsNullOrEmpty(strPartnLnm) ||
		                string .IsNullOrEmpty(strPartnDOB) )
		            {
		            	//Show error message
						pnlErrorMsg.Visible = true;
		            	lblErrorMsg.Text = "Complete Partner Details Form with Full name and DOB";                	
		            	
		            	bFlag = false;
		            }
	    			else
	    			{
		        		
	    				DateTime dt;
						bool valid = DateTime.TryParseExact(strPartnDOB, "dd/MM/yyyy", null, DateTimeStyles.None, out dt);
		 				
						if (!valid)
						{	
							//Show error message
							pnlErrorMsg.Visible = true;
		                	lblErrorMsg.Text = "Partner's Birthdate format is incorrect. Please enter value in dd/mm/yyyy format"; 
							bFlag = false;
						}
						else{
		  				string strGender = ddlGender.SelectedValue;
			        	CultureInfo provider = CultureInfo.InvariantCulture;
		
		
			        	//Reference Date for Tournament Age Limit
			        	DateTime dtReferenceAgeDate = new DateTime(2018,01,01);
			        	
			        	DateTime dtDOB = DateTime.ParseExact(strPartnDOB,"dd/MM/yyyy", provider);
			        	int iAgeLimit = dtReferenceAgeDate.Year - dtDOB.Year ;//difference between dtDOB - dtReferenceAgeDate;
			        	string strEventAge =  strEtName.Substring(strEtName.Length - 2,  2);
			        	if(iAgeLimit > int.Parse(strEventAge))
			        	{
			        		//Show error message
							pnlErrorMsg.Visible = true;
		                	lblErrorMsg.Text = "Partner's Birthdate is more then Age limit permitted"; 
							bFlag = false;
			        	}
						}
					}
			} 

        }
			}
    		}
    		catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "SF_RegistrationForm.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
                Response.Redirect("./TP_BD_Home.aspx", false);
            }
    		
			return bFlag;
        }
        
        private List<TPEvent> GetEventDetails()
        {
        	List<TPEvent> lstObjTPEvent = null;
        	
        	try
        	{
        		dtPlayerParticipation = (DataTable)Session["PLAYERPARTICIPATION"];
			        			
    			string strEtName = "";
    			string strFees = "";
    			//string strPartnName = "";
    			string strPartnFnm = "";
    			string strPartnLnm = "";    			
    			string strPartnDOB = "";
    			
    			lstObjTPEvent = new List<TPEvent>();
    			
    			TPEvent objTPEvent = null;
    			
    			if (dtPlayerParticipation != null && dtPlayerParticipation.Rows.Count > 0)
    			{
	        			//Get Player Participation Details in multiple events
        			for(int i=0; i < dtPlayerParticipation.Rows.Count; i++)
        			{
        				CheckBox cb1 = (CheckBox)dgEventParticipation.Items[i].FindControl("dgcbEventSelect");
	    				if (cb1.Checked)
	    				{
	        				strEtName = dtPlayerParticipation.Rows[i][2].ToString();
	        				strFees = dtPlayerParticipation.Rows[i][3].ToString();
	        				
	        				TextBox tbPartnName = (TextBox)dgEventParticipation.Items[i].FindControl("dgtbPartnerName");
	        				TextBox tbPartnerDOB = (TextBox)dgEventParticipation.Items[i].FindControl("dgtbPartnerDOB");
	        				
	        				if (!string.IsNullOrEmpty(tbPartnName.Text))
	        				{
		        				String [] strPartnerNames = tbPartnName.Text.Split (' ');
		        				
		        				
		        				if (!string.IsNullOrEmpty(strPartnerNames[0]))
		        				{
			        				strPartnFnm =  strPartnerNames[0];		        			
		        				}
		        				if (!string.IsNullOrEmpty(strPartnerNames[1]))
		        				{
			        				strPartnLnm = strPartnerNames[1];
		        				}
		        				
		        				strPartnDOB = tbPartnerDOB.Text;
	        				}
	        				
	        				objTPEvent = new TPEvent();
	        				//objTPEvent.EventCode = "TP1";
	        				objTPEvent.EventName = strEtName;
	        				objTPEvent.EventRateCard = strFees;
	        				objTPEvent.PartnerCode = "";
	        				objTPEvent.ParterFName = strPartnFnm;
	        				objTPEvent.ParterLName = strPartnLnm;
	        				objTPEvent.PartnerFullName = strPartnFnm + " " + strPartnLnm;
	        				objTPEvent.ParterDOB = strPartnDOB;
	        				
        					lstObjTPEvent.Add (objTPEvent);		
        					strPartnFnm = strPartnLnm = strFees = strEtName = strPartnDOB = "";
        					
    					}
    				}
    			}
        	}        	
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "SF_PlayersParticipation.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        	
        	return lstObjTPEvent;
        }
        
        protected void btnRegisterPay_Click(object sender, EventArgs e)
        {
            try
            {
            	//btnRegisterPay.Enabled = false;
            	
				if (UIValidationforPlayerDetails())
                {
                	
                	DateTime dt;
					//bool valid = DateTime.TryParseExact(txtBirthdate.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out dt);
										
					//if (!valid)
					//{	
						//Show error message
						//pnlErrorMsg.Visible = true;
	                	//lblErrorMsg.Text = "Birthdate format is incorrect. Please enter value in dd/mm/yyyy format"; 
					//}
					//else
					{
						bool bFlag = UIValidatePartnerPlayerDetails();
						if (bFlag ==true)
						{
							if (int.Parse(lblTotalAmount.Text) > 0)
							{
	                	
			                	TPPlayer objPlayer = new TPPlayer();
			                	             
			                	objPlayer.PlayerFName = txtFirstName.Text;
			                	objPlayer.PlayerLName = txtLastName.Text;
			                	objPlayer.PlayerDOB = ddlDate.SelectedItem.Text + "/" + ddlMonth.SelectedValue + "/" + ddlYear.SelectedItem.Text;
			        			objPlayer.PlayerContact =  txtContact.Text;
			        			//objPlayer.PlayerEmailID =  txtEmailID.Text;        			
			        			objPlayer.PlayerEmailID = ValidateEmail(txtEmailID.Text);
									
			        			objPlayer.PlayerGender =  ddlGender.SelectedItem.Text;
			        			objPlayer.PlayerState = ddlStates.SelectedItem.Text;
			        			objPlayer.PlayerDistrict = ddlDistricts.SelectedItem.Text;
			        			objPlayer.PlayerCity = txtCity.Text;
			        			objPlayer.PlayerAddress = txtAddress.Text;
			         			//ddlEvents.SelectedItem.Text;
			                			
			        			dtPlayerParticipation = (DataTable)Session["PLAYERPARTICIPATION"];
			        			
			        			string strEtName = "";
			        			string strFees = "";
			        			string strPartnName = "";
			        			string strPartnFnm = "";
			        			string strPartnLnm = "";
			        			string strPartnDOB = "";
			        			List<TPEvent> lstObjTPEvent = new List<TPEvent>();
			        			TPEvent objTPEvent = null;
			        			if (dtPlayerParticipation != null && dtPlayerParticipation.Rows.Count > 0)
			        			{
					        			//Get Player Participation Details in multiple events
				        			for(int i=0; i < dtPlayerParticipation.Rows.Count; i++)
				        			{
				        				CheckBox cb1 = (CheckBox)dgEventParticipation.Items[i].FindControl("dgcbEventSelect");
					    				if (cb1.Checked)
					    				{
					        				strEtName = dtPlayerParticipation.Rows[i][2].ToString();
					        				strFees = dtPlayerParticipation.Rows[i][3].ToString();
					        				
					        				TextBox tbPartnName = (TextBox)dgEventParticipation.Items[i].FindControl("dgtbPartnerName");
					        				TextBox tbPartnerDOB = (TextBox)dgEventParticipation.Items[i].FindControl("dgtbPartnerDOB");
					        				
					        				if (!string.IsNullOrEmpty(tbPartnName.Text))
					        				{
						        				String [] strPartnerNames = tbPartnName.Text.Split (' ');
						        				
						        				
						        				if (!string.IsNullOrEmpty(strPartnerNames[0]))
						        				{
							        				strPartnFnm =  strPartnerNames[0];		        			
						        				}
						        				if (!string.IsNullOrEmpty(strPartnerNames[1]))
						        				{
							        				strPartnLnm = strPartnerNames[1];
						        				}
						        				
						        				strPartnDOB = tbPartnerDOB.Text;
					        				}
					        				
					        				objTPEvent = new TPEvent();					        				
					        				objTPEvent.EventName = strEtName;
					        				objTPEvent.EventRateCard = strFees;
					        				objTPEvent.PartnerCode = "";
					        				objTPEvent.ParterFName = strPartnFnm;
					        				objTPEvent.ParterLName = strPartnLnm;
					        				objTPEvent.ParterDOB = strPartnDOB;
					        				
				        					lstObjTPEvent.Add (objTPEvent);		
				        					strPartnFnm = strPartnLnm = strFees = strEtName = strPartnDOB = "";
				        					
				    					}
			        				} 

									Session["PARTICIPATIONEVENTS"] = lstObjTPEvent;			        			
								
									TPTournamentRegistration objRegistration = new TPTournamentRegistration();
		                	
				        			objRegistration.SportCode = strSportCode;
				        			objRegistration.TournamentCode = strTournamentCode;
				        			objRegistration.RegisteredEvents = lstObjTPEvent;	        			
				         			//objRegistration.RegistrationCaptcha = txtCaptcha.Text;	                	
				         			//Session["TotalAmount"] = lblTotalAmount.Text;
				         			//Check if Player already registered in the same category
			         			 	//bool bIsPlayerExists = (new TPDAL_Registration()).IsPlayerAlreadyExists (strFirstName, txtBirthdate.Text, strEtName);
			         			 	
		         			 			         			 	
		         			 	
			         			 	if (!string.IsNullOrEmpty(strTournamentCode))
			         			 	{
						
										//check for duplocate write
										TPDAL_Registration objDALRegistration = new TPDAL_Registration();
										TPPlayerDuplicateCheck objCheckDuplicate = objDALRegistration.PlayerRegisterationCheckDuplicate(objPlayer, objRegistration, dtPlayerParticipation, int.Parse(lblTotalAmount.Text));
										string strDuplicateMessage = "Even(s) ";
										bool blDuplicate = false;
										
										if(objCheckDuplicate !=null)
										{
											for (int index = objCheckDuplicate.EventList.Count -1; index >=0 ; index--)
											{
												if (objCheckDuplicate.EventList[index].isAlreadyPresent == true)
												{
													strDuplicateMessage += " : " +objCheckDuplicate.EventList[index].EventCode ;
													blDuplicate = true;
													//remove from objRegistration
													objRegistration.RegisteredEvents.RemoveAt(index);
												}
											}
										}
										int iTotal = 0;
										for(int irow = 0; irow <  objRegistration.RegisteredEvents.Count ; irow++)
										{	try{
											iTotal += int.Parse (objRegistration.RegisteredEvents[irow].EventRateCard);
											}
											 catch (Exception ex)
            								{
                									System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

									                MGErrorMsg objError = new MGErrorMsg("Error", 0, "SF_RegistrationForm.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
									                MGError objLogError = new MGError();
									                objLogError.logError(objError);
									                
									                Response.Redirect("./TP_BD_Home.aspx", false);
																				   
											 }
										}
										
										lblTotalAmount.Text = iTotal.ToString();
										if(objRegistration.RegisteredEvents.Count ==0)
										{
											strDuplicateMessage = "Registeration for all events are already done";
											lblErrorMsg.Text = strDuplicateMessage;	
											lblErrorMsg.Visible=true;
											
											string strPlayerDOB = ddlDate.SelectedItem.Text + "/" + ddlMonth.SelectedValue + "/" + ddlYear.SelectedItem.Text;
											string strPlayerCode = 	objDALRegistration.GetPlayerCode (strTournamentCode , txtFirstName.Text , txtLastName.Text , strPlayerDOB);
									
											TPSessionPlayer objSessionPlayer = new TP.Entity.TPSessionPlayer();
					         			 	objSessionPlayer.Address = txtCity.Text +":" + ddlStates.Text + ":"+ txtAddress.Text;
					         			 	objSessionPlayer.DOB	= strPlayerDOB;
					         			 	objSessionPlayer.TournamentCode = strTournamentCode;
					         			 	//objSessionPlayer.Email = txtEmailID.Text;
					         			 	objSessionPlayer.Email = ValidateEmail(txtEmailID.Text);
					         			 	objSessionPlayer.FullName = txtFirstName.Text + " " +txtLastName.Text;
					         			 	objSessionPlayer.Mobile = txtContact.Text;
					         			 	objSessionPlayer.PlayerCode = strPlayerCode;
					         			 	objSessionPlayer.TotalAmount = lblTotalAmount.Text;
					         				Session["PlayerForPayment"] = objSessionPlayer;
			         			 		 					                    	
											string strPath = HttpContext.Current.Request.Url.AbsoluteUri;
											string strURL ="";
			                				string strSourceURL = "SF_RegistrationForm.aspx?TCode="+strTournamentCode;											
											if(strPath.ToUpper().Contains("SPORTFIT"))												
												strURL = strPath.ToUpper().Replace( strSourceURL.ToUpper(), "SF_PaymentPlayersParticipation.aspx?PlayerCode="+ strPlayerCode);
											else
												strURL = strPath.ToUpper().Replace(strSourceURL.ToUpper(),"./sportfit/SF_PaymentPlayersParticipation.aspx?PlayerCode="+ strPlayerCode);
			                				Response.Redirect(strURL, false);
			                				
										}
										else
										{
											if(strDuplicateMessage != "Even(s) ")
												
											{
												//alert error message
											}
										//check for duplicate end
			         			 		objDALRegistration = new TPDAL_Registration();
			         			 		int iFlag = objDALRegistration.PlayerRegisteration(objPlayer, objRegistration, dtPlayerParticipation, int.Parse(lblTotalAmount.Text));
					                    
					                    if (iFlag == 0)
					                    {
					                    	//Successful Registration
					                    	
					                    	string strPlayerDOB = ddlDate.SelectedItem.Text + "/" + ddlMonth.SelectedValue + "/" + ddlYear.SelectedItem.Text;
					                    	string strPlayerCode = 	objDALRegistration.GetPlayerCode (strTournamentCode , txtFirstName.Text , txtLastName.Text , strPlayerDOB);
									
											TPSessionPlayer objSessionPlayer = new TP.Entity.TPSessionPlayer();
					         			 	objSessionPlayer.Address = txtCity.Text +":" + ddlStates.Text + ":"+ txtAddress.Text;
					         			 	objSessionPlayer.DOB	= strPlayerDOB;
					         			 	objSessionPlayer.TournamentCode = strTournamentCode;
					         			 	//objSessionPlayer.Email = txtEmailID.Text;
					         			 	objSessionPlayer.Email = ValidateEmail(txtEmailID.Text);
					         			 	
					         			 	objSessionPlayer.FullName = txtFirstName.Text + " " +txtLastName.Text;
					         			 	objSessionPlayer.Mobile = txtContact.Text;
					         			 	objSessionPlayer.PlayerCode = strPlayerCode;
					         			 	objSessionPlayer.TotalAmount = lblTotalAmount.Text;
					         				Session["PlayerForPayment"] = objSessionPlayer;
			         			 		 					                    	
					                    	SendRegistrationEmail(strPlayerCode);
											string strPath = HttpContext.Current.Request.Url.AbsoluteUri;
											string strURL ="";
											string strSourceURL = "SF_RegistrationForm.aspx?TCode="+strTournamentCode;											
											if(strPath.ToUpper().Contains("SPORTFIT"))												
												strURL = strPath.ToUpper().Replace( strSourceURL.ToUpper(), "SF_PaymentPlayersParticipation.aspx?PlayerCode="+ strPlayerCode);
											else
												strURL = strPath.ToUpper().Replace(strSourceURL.ToUpper(),"./sportfit/SF_PaymentPlayersParticipation.aspx?PlayerCode="+ strPlayerCode);
			                				Response.Redirect(strURL, false);
					                    }
					                    else // Non zero - unsuccessful
					                    {
					                    	//Unsuccessful registration
					                    	//Show error to user
					                    }
			         			 	}
			         			 	}
			         			 	else
			         			 	{
			         			 		Response.Redirect("./TP_BD_Home.aspx", false);
			         			 	}
			         			 	
				                    
		        			}
						}
	        			else
	        			{
	        				pnlErrorMsg.Visible = true;
	                		lblErrorMsg.Text = "Please select atleast one event to participate";  
	        			}
					}
					else
					{
							pnlErrorMsg.Visible = true;
	                		lblErrorMsg.Text = "Please enter partner Full name and DOB (in dd/mm/yyyy format) , based on Age Category for registration";  
	        			
					}
                    
                }
            }
				else
				{
						pnlErrorMsg.Visible = true;
	                	lblErrorMsg.Text = "Please enter player details : name , address, Email, contact number , DOB (in dd/mm/yyyy format) .";  
	        		
				}
            }
            catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "SF_RegistrationForm.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
                Response.Redirect("./TP_BD_Home.aspx", false);
            }
        }
        
        #endregion
        */
    }
}