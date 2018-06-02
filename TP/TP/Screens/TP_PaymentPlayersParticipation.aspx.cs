using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TP.DAL;
using TP.Entity;
using System.Text;
using System.Data;
using System.IO;

namespace TournamentPlanner
{
    public partial class TP_PaymentPlayersParticipation : System.Web.UI.Page
    {
    	string strSportCode = "BD";
    	string strTournamentCode = "";
    	//string strPlayerFullName = "";
    	//string strPlayerDOB = "";
    	//string strTotalAmount = "";
    	string strPlayerCode = "";
    	int const_FileSize = 10240000;
		
        protected void Page_Load(object sender, EventArgs e)
        {	
			try
			{        		
        		Session["SPORTCODE"] = strSportCode;
        		
        		strTournamentCode = Request.QueryString["TCode"];
        		Session["TOURNAMENTCODE"] = strTournamentCode;
        		
	        	strPlayerCode = Request.QueryString["PlayerCode"];        		
        		
				if(IsPostBack)
				{
								
				}
				else
				{
					//if( Session["PlayerForPayment"] == null)
					PopulatePlayerInSession();
					PopulateTournamentSummary ();
					PopulatePendingPayment();
					PopulatePlayerParticipationDetails();
					GetDocumentProof();
				}
			}
			catch (Exception ex)
			{
				System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_PaymentPlayersParticipation.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
				Response.Redirect("./TP_BD_Home.aspx");
			}
        }
        
        protected void lbtnTPMenu_Click(object sender, EventArgs e)
        {
        	LinkButton lbtnObj = (LinkButton) sender;        	
        	string strSelectedMenu = lbtnObj.Text;
        	
        	string strURL = "";
        	
        	if (strSelectedMenu.Equals("HOME"))
        	{
        		strURL = "./TP_BD_HOME.aspx";
        	}
        	if (strSelectedMenu.Equals("EVENTS"))
        	{    	
        		strURL = "./TP_Events.aspx?TCode=" + strTournamentCode;
        	}
        	if (strSelectedMenu.Equals("PLAYERS"))
        	{
        		strURL = "./TP_Players.aspx?TCode=" + strTournamentCode;        		
        	}
        	if (strSelectedMenu.Equals("DRAWS"))
        	{
        		strURL = "./TP_Draws.aspx?TCode=" + strTournamentCode;
        	}
        	if (strSelectedMenu.Equals("MATCHES"))
        	{
        		strURL = "./TP_Matches.aspx?TCode=" + strTournamentCode;
        	}   

			Response.Redirect(strURL, false);        	
        }
        
        private void PopulatePlayerInSession()
        {
        	TPPlayer objPlayer = (new TPDAL_PlayerDetails()).GetPlayerDetailsWithPlayerCode(strTournamentCode, strPlayerCode);
        	TPSessionPlayer objPlayerSession = new TPSessionPlayer();
            objPlayerSession.Address = objPlayer.PlayerAddress + ":" + objPlayer.PlayerCity + ":" + objPlayer.PlayerState;
            objPlayerSession.DOB = objPlayer.PlayerDOB;
        	objPlayerSession.TournamentCode = strTournamentCode;
            objPlayerSession.Email = objPlayer.PlayerEmailID;
            objPlayerSession.FullName = objPlayer.PlayerFullName;
            objPlayerSession.Mobile = objPlayer.PlayerContact;
            objPlayerSession.PlayerCode = objPlayer.PlayerCode;
        		
        	Session["PlayerForPayment"]  = objPlayerSession;
        }
        
        /// <summary>
        /// PopulateTournamentSummary -- The header part
        /// </summary>
        /// 
        private void PopulatePendingPayment()
        {
            		
        	int iPaymentAmount  = (new TPDAL_Registration()).GetTotalPendingPayment(strTournamentCode, strPlayerCode);
        	
        	btnPayment.Text = "Click to Pay Rs. " + iPaymentAmount.ToString();
        	if(Session["PlayerForPayment"] != null)
        	{
        		TPSessionPlayer objSessionPlayer = (TPSessionPlayer) Session["PlayerForPayment"];
        		objSessionPlayer.TotalAmount = iPaymentAmount.ToString();
        		
        		Session["PlayerForPayment"] = objSessionPlayer;
        		lblPlayerEmail.Text = objSessionPlayer.Email;
        	}
        }
        
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
					//lblEntryOpenDate.Text = lstObj[0].TournamentEntryOpenDate.ToString ("dddd, dd MMMM yyyy HH:mm") + " IST";
					//lblEntryClosesDate.Text = lstObj[0].TournamentEntryCloseDate.ToString ("dddd, dd MMMM yyyy HH:mm") + " IST";
					//lblEntryWithdrawalDate.Text = lstObj[0].TournamentEntryWithdrawlDate.ToString ("dddd, dd MMMM yyyy HH:mm") + " IST";
					lblTournamentDuration.Text = lstObj[0].TournamentStartDate.ToString ("dddd, dd MMMM yyyy") + " -TO- " + lstObj[0].TournamentEndDate.ToString ("dddd, dd MMMM yyyy");
					lblTournamentOrganisation.Text =  lstObj[0].TournamentOrganisation;
					lblTournamentVenue.Text = lstObj[0].TournamentVenue;
					//lblLocationAddress.Text = lstObj[0].TournamentLocationAddress; 
					//lblTournamentContacts.Text = lstObj[0].TournamentPOCContactNames + " " + lstObj[0].TournamentPOCContactNo + " " + lstObj[0].TournamentLocationContactNo;
				}
        	}
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_PaymentPlayersParticipation.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        
        }
        
        private void PopulatePlayerParticipationDetails ()
        {
        	try
        	{        		
	        	//Get all the player list from database
	        	List<TPPlayerParticipation> lstObj = new TPDAL_PlayerDetails().GetPlayerParticipationDetails(strTournamentCode, strPlayerCode);
				
	        	dgPlayerParticilationList.DataSource = lstObj;
				dgPlayerParticilationList.DataBind();
				
				CheckPaymentStatus(lstObj);
				
	        	
        	}
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_PaymentPlayersParticipation.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}

        
        }
        
        private void CheckPaymentStatus(List<TPPlayerParticipation> lstObj)
        {
        	
        	try
        	{       
				bool bFlag = false; 
				bool bMessage =false;				
        		for (int index = 0 ;index < lstObj.Count ; index++)
        		{
        			if(lstObj[index].PaymentStatus == "Pending")
        			{
        				if(lstObj[index].PartnerPlayerCode.Contains("(*)"))
        					bMessage = true;
        				else
							bFlag=true;        					
        					
        			}
        			
        		}
        		
        		if(!bFlag) //if no payment pending
        		{
        			chkbAgree.Visible =false;
        			btnPayment.Visible=false;
        			pnlPaymentInformation.Visible = false;
        			
        		}
        		else
        		{
        			chkbAgree.Visible =true; //will need to uncomments once onlien payment is activated
        			btnPayment.Visible=true;
        			pnlPaymentInformation.Visible = true;
        		}
        		if(bMessage)
        			lblPartnerMessage.Visible = true;
        		else
        			lblPartnerMessage.Visible =false;
        	}
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_PaymentPlayersParticipation.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        }
        
        protected void chkbAgree_CheckedChanged(object sender, EventArgs e)
        {
        	lblPleaseWaitMsg.Visible = true;
        	
        	if (chkbAgree.Checked)
        	{
        		btnPayment.Enabled = true;
        	}
        	else
        	{
        		btnPayment.Enabled = false;
        	}
        	
        	lblPleaseWaitMsg.Visible = false;
        }
        
        private void SendPaymentEmail ()
        {
        	try
        	{
	        	string strToEmailID = lblPlayerEmail.Text;
	        	
	        	string strMyEmailSubject = "BADMINTON TOURNAMENT";
	        	string strEmailSubject = new MGCommon.EMailTemplate().MailSubjectLine(strMyEmailSubject);
	        	
	        	string strHeader = new MGCommon.EMailTemplate().EMailHeader();
	        	//string strBody = new MGCommon.EMailTemplate().TournamentRegistrationMailBody(txtFirstName.Text, txtLastName.Text, txtContact.Text, "BS U11", lblTotalAmount.Text);
	        	string strFooter = new MGCommon.EMailTemplate().EMailFooter();
	        	
	        	//string strEmailBody = strHeader + strBody + strFooter;
	        	
	        	//MGCommon.MGGeneric.MGEMailServices(strToEmailID, strEmailSubject, strEmailBody);
        	}
            catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_PaymentPlayersParticipation.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
                Response.Redirect("./TP_BD_Home.aspx", false);
            }
        }
                
        protected void btnPayment_Click(object sender, EventArgs e)
        {
        	try
        	{
        		
        		SaveImageinDB(hfContentType.Value);
        		SendPaymentEmail();
        		
        		//PayToPayU();
        	
        		Response.Redirect("SF_Payment2PayU.aspx", false);
        	}
        	catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_PaymentPlayersParticipation.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
                Response.Redirect("./TP_BD_Home.aspx", false);
            }
        }
		#region image - ID proof
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
        
		protected void SaveImageinDB(string strContentType)
		{
			try
			{
		    	 if(imgProof.ImageUrl != "")
	             {
	                //lblStatusLabel.Text = "Upload status: File uploaded!";
	          		//byte[] imagebyt = new byte[ const_FileSize];
					byte[] imagebyt = Convert.FromBase64String(imgProof.ImageUrl.Replace("data:"+strContentType+";base64,", ""));
					string strDocType = "";
					strDocType = ddlDocType.SelectedValue;
	          		//imagebyt = imgProof.ImageUrl;
					//imagebyt = File.ReadAllBytes(imgProof.ImageUrl);
					//imgProo
					(new TPDAL_Registration()).SaveImagetoDB(strTournamentCode, strPlayerCode, imagebyt, strContentType, strDocType);
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
		private void GetDocumentProof()
        {
			try{
        		TPDocumentProof objDocument = (new TPDAL_Registration()).GetDocumentProof(strTournamentCode, strPlayerCode);
        		if ( objDocument!= null)
        		{
    			     if(objDocument.document != null && objDocument.ContentType != null && 
        			   objDocument.ContentType != "" )
        			{
        				//	string base64String = Convert.ToBase64String(objDocument.document , 0, objDocument.document.Length);
                    	//    imgProof.ImageUrl = "data:"+objDocument.ContentType+";base64," + base64String;

                     	//   imgProof.Visible = true;
            			lblDocType.Text = "you have uploaded : " + objDocument.DocumentType ;
                     	pnlDocTypeMsg.Visible = true;
                     	lblDocTypeMsg.Text = "you have uploaded : " + objDocument.DocumentType ;
                     	pnlDocType.Visible = false;
            			FileUploadControl.Visible = false;
                      	btnUploadButton.Visible = false;
				    	ddlDocType.Visible	 = false;
				    	imgProof.Visible = false;
                       	hfContentType.Value = objDocument.ContentType;
                       	lblSelectDocType.Visible = false;
          /*           	if(objDocument.DocumentType != "")
                       	{
                       		ddlDocType.ClearSelection();
                       		if(ddlDocType.Items.FindByValue(objDocument.DocumentType) != null)
                       			ddlDocType.Items.FindByValue(objDocument.DocumentType).Selected  = true;
                       	}
                       	*/
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

		#endregion
        
        #region Payment Gateway 

        /*
        public string Generatehash512(string text)
        {

            byte[] message = Encoding.UTF8.GetBytes(text);

            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            SHA512Managed hashString = new SHA512Managed();
            string hex = "";
            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;

        }
        
        protected void PayToPayU()
        {
            try
            {
					//bool bCheckBillingAddress = CheckBillingAddress();

					iTotalAmount =1;
                    string payAmount = Convert.ToDecimal(iTotalAmount).ToString("g29");
                    string firstName = "anita";// txtName.Text;
                    string email = "anitatripathi@gmail.com"; //txtEMail.Text;
                    string phone = "11111"; //.Text;
                    string productinfo = "1";

				if (Session["PlayerForPayment"] != null)
                {
					TP.Entity.TPSessionPlayer objPlayer =new TPSessionPlayer();
                	objPlayer = (TP.Entity.TPSessionPlayer)Session["PlayerForPayment"];
                
                	iTotalAmount = int.Parse( objPlayer.TotalAmount);
					payAmount = Convert.ToDecimal(iTotalAmount).ToString("g29");
					firstName = objPlayer.FullName;
					email = objPlayer.Email;
					phone = objPlayer.Mobile;
					productinfo = "P1";
				}
//                if (bCheckBillingAddress == true)
                {
				
                    //string payAmount = Convert.ToDecimal(2).ToString("g29");
                    //string firstName = "atul";
                    //string email = "atulmani@gmail.com";
                    //string phone = "9822752885";
                    //string productinfo = iOrderID.ToString();
                    //string successURL = "http://localhost:25163/PG/receipt.aspx";
                    //string failureURL = "http://localhost:25163/PG/receipt.aspx";


                    string[] hashVarsSeq;
                    string hash_string = string.Empty;


                    if (string.IsNullOrEmpty(Request.Form["txnid"])) // generating txnid
                    {
                        Random rnd = new Random();
                        string strHash = Generatehash512(rnd.ToString() + DateTime.Now);
                        txnid1 = strHash.ToString().Substring(0, 20);

                    }
                    else
                    {
                        txnid1 = Request.Form["txnid"];
                    }

                    if (string.IsNullOrEmpty(Request.Form["hash"])) // generating hash value
                    {
                        if (
                            string.IsNullOrEmpty(System.Web.Configuration.WebConfigurationManager.AppSettings["MERCHANT_KEY"]) ||
                            string.IsNullOrEmpty(txnid1) //||
                            //string.IsNullOrEmpty(Request.Form["amount"]) ||
                            //string.IsNullOrEmpty(Request.Form["firstname"]) ||
                            //string.IsNullOrEmpty(Request.Form["email"]) ||
                            //string.IsNullOrEmpty(Request.Form["phone"]) ||
                            //string.IsNullOrEmpty(Request.Form["productinfo"]) ||
                            //string.IsNullOrEmpty(Request.Form["surl"]) ||
                            //string.IsNullOrEmpty(Request.Form["furl"])
                            )
                        {
                            //error

                            //frmError.Visible = true;
                            return;
                        }

                        else
                        {
                            //frmError.Visible = false;

                            hashVarsSeq = System.Web.Configuration.WebConfigurationManager.AppSettings["hashSequence"].Split('|'); // spliting hash sequence from config
                            hash_string = "";
                            hash_string = hash_string + System.Web.Configuration.WebConfigurationManager.AppSettings["MERCHANT_KEY"];
                            hash_string = hash_string + '|';
                            hash_string = hash_string + txnid1;
                            hash_string = hash_string + '|';
                            hash_string = hash_string + payAmount;
                            hash_string = hash_string + '|';
                            hash_string = hash_string + productinfo;
                            hash_string = hash_string + '|';

                            hash_string = hash_string + firstName;
                            hash_string = hash_string + '|';
                            hash_string = hash_string + email;
                            hash_string = hash_string + '|';
                            hash_string = hash_string + "";// Convert.ToDecimal(phone).ToString("g29"); ;// "";// phone;

                            hash_string = hash_string + '|';

                            hash_string = hash_string + successURL;
                            hash_string = hash_string + '|';
                            hash_string = hash_string + failureURL;
                            hash_string = hash_string + '|';
                            hash_string = hash_string + "";
                            hash_string = hash_string + '|';
                            hash_string = hash_string + "";
                            hash_string = hash_string + '|';
                            hash_string = hash_string + "";
                            hash_string = hash_string + '|';

                            hash_string = hash_string + "";
                            hash_string = hash_string + '|';
                            hash_string = hash_string + "";
                            hash_string = hash_string + '|';

                            hash_string = hash_string + "";
                            hash_string = hash_string + '|';
                            hash_string = hash_string + "";
                            hash_string = hash_string + '|';



                            //string strhash = "";
                            //foreach (string hash_var in hashVarsSeq)
                            //{
                            //    if (hash_var == "key")
                            //    {
                            //        strhash = strhash + ConfigurationManager.AppSettings["MERCHANT_KEY"];
                            //        strhash = strhash + '|';
                            //    }
                            //    else if (hash_var == "txnid")
                            //    {
                            //        strhash = strhash + txnid1;
                            //        strhash = strhash + '|';
                            //    }
                            //    else if (hash_var == "amount")
                            //    {
                            //        //hash_string = hash_string + Convert.ToDecimal(Request.Form[hash_var]).ToString("g29");
                            //        strhash = strhash + Convert.ToDecimal(2).ToString("g29");
                            //        strhash = strhash + '|';
                            //    }
                            //    else
                            //    {

                            //        strhash = strhash + (Request.Form[hash_var] != null ? Request.Form[hash_var] : "");// isset if else
                            //        strhash = strhash + '|';
                            //    }
                            //}

                            //hash_string = "";
                            //foreach (string hash_var in hashVarsSeq)
                            //{
                            //    if (hash_var == "key")
                            //    {
                            //        hash_string = hash_string + ConfigurationManager.AppSettings["MERCHANT_KEY"];
                            //        hash_string = hash_string + '|';
                            //    }
                            //    else if (hash_var == "txnid")
                            //    {
                            //        hash_string = hash_string + txnid1;
                            //        hash_string = hash_string + '|';
                            //    }
                            //    else if (hash_var == "amount")
                            //    {
                            //        //hash_string = hash_string + Convert.ToDecimal(Request.Form[hash_var]).ToString("g29");
                            //        hash_string = hash_string + Convert.ToDecimal(2).ToString("g29");
                            //        hash_string = hash_string + '|';
                            //    }
                            //    else
                            //    {

                            //        hash_string = hash_string + (Request.Form[hash_var] != null ? Request.Form[hash_var] : "");// isset if else
                            //        hash_string = hash_string + '|';
                            //    }
                            //}

                            hash_string += System.Web.Configuration.WebConfigurationManager.AppSettings["SALT"];// appending SALT

                            hash1 = Generatehash512(hash_string).ToLower();         //generating hash
                            action1 = System.Web.Configuration.WebConfigurationManager.AppSettings["PAYU_BASE_URL"] + "/_payment";// setting URL

                        }


                    }

                    else if (!string.IsNullOrEmpty(Request.Form["hash"]))
                    {
                        hash1 = Request.Form["hash"];
                        action1 = System.Web.Configuration.WebConfigurationManager.AppSettings["PAYU_BASE_URL"] + "/_payment";

                    }




                    if (!string.IsNullOrEmpty(hash1))
                    {
                        hash.Value = hash1;
                        txnid.Value = txnid1;

                        System.Collections.Hashtable data = new System.Collections.Hashtable(); // adding values in gash table for data post
                        data.Add("hash", hash.Value);
                        data.Add("txnid", txnid.Value);
                        data.Add("key", key.Value);
                        //string AmountForm = Convert.ToDecimal(amount.Text.Trim()).ToString("g29");// eliminating trailing zeros
                        //amount.Text = AmountForm;
                        //data.Add("amount", AmountForm);
                        //data.Add("firstname", firstname.Text.Trim());
                        //data.Add("email", email.Text.Trim());
                        //data.Add("phone", phone.Text.Trim());
                        //data.Add("productinfo", productinfo.Text.Trim());
                        //data.Add("surl", surl.Text.Trim());
                        //data.Add("furl", furl.Text.Trim());
                        //data.Add("lastname", lastname.Text.Trim());
                        //data.Add("curl", curl.Text.Trim());
                        //data.Add("address1", address1.Text.Trim());
                        //data.Add("address2", address2.Text.Trim());
                        //data.Add("city", city.Text.Trim());
                        //data.Add("state", state.Text.Trim());
                        //data.Add("country", country.Text.Trim());
                        //data.Add("zipcode", zipcode.Text.Trim());
                        //data.Add("udf1", udf1.Text.Trim());
                        //data.Add("udf2", udf2.Text.Trim());
                        //data.Add("udf3", udf3.Text.Trim());
                        //data.Add("udf4", udf4.Text.Trim());
                        //data.Add("udf5", udf5.Text.Trim());
                        //data.Add("pg", pg.Text.Trim());

                        ////Only mandatory fields
                        //string payAmount = Convert.ToDecimal(2).ToString("g29");
                        //string firstName = "Atul";
                        //string email = "atulmani@gmail.com";
                        //string phone = "9822752885";
                        //string productinfo = "Product 1";
                        //string successURL = "http://merashop.biz";
                        //string failureURL = "http://mera-group.com";
                        data.Add("amount", payAmount);
                        data.Add("firstname", firstName);
                        data.Add("email", email);
                        data.Add("phone", phone);
                        data.Add("productinfo", productinfo);
                        data.Add("surl", successURL);
                        data.Add("furl", failureURL);
                        data.Add("lastname", "");
                        data.Add("curl", "");
                        data.Add("address1", "");
                        data.Add("address2", "");
                        data.Add("city", "");
                        data.Add("state", "");
                        data.Add("country", "");
                        data.Add("zipcode", "");
                        data.Add("udf1", "");
                        data.Add("udf2", "");
                        data.Add("udf3", "");
                        data.Add("udf4", "");
                        data.Add("udf5", "");
                        data.Add("pg", "");

                        string strForm = PreparePOSTForm(action1, data);
                        Page.Controls.Add(new LiteralControl(strForm));

                    }

                    else
                    {
                        //no hash

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
         
        private string PreparePOSTForm(string url, System.Collections.Hashtable data)      // post form
        {
            //Set a name for the form
            string formID = "PostForm";
            //Build the form using the specified data to be posted.
            StringBuilder strForm = new StringBuilder();
            strForm.Append("<form id=\"" + formID + "\" name=\"" +
                           formID + "\" action=\"" + url +
                           "\" method=\"POST\">");

            foreach (System.Collections.DictionaryEntry key in data)
            {

                strForm.Append("<input type=\"hidden\" name=\"" + key.Key +
                               "\" value=\"" + key.Value + "\">");
            }


            strForm.Append("</form>");
            //Build the JavaScript which will do the Posting operation.
            StringBuilder strScript = new StringBuilder();
            strScript.Append("<script language='javascript'>");
            strScript.Append("var v" + formID + " = document." +
                             formID + ";");
            strScript.Append("v" + formID + ".submit();");
            strScript.Append("</script>");
            //Return the form and the script concatenated.
            //(The order is important, Form then JavaScript)
            return strForm.ToString() + strScript.ToString();
        }
		
		*/

        #endregion
        
    }
}