/*
 * Created by SharpDevelop.
 * User: 123222
 * Date: 7/26/2017
 * Time: 12:40 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TP.DAL;
using TP.Entity;
using System.Text;
using System.Globalization;

namespace TournamentPlanner
{
	/// <summary>
	/// Description of TPAdmin
	/// </summary>
	public partial class TP_PaymentStatus : System.Web.UI.Page
	{	
		string strSportCode = "BD";
    	string strTournamentCode = "";
    	    	
        protected void Page_Load(object sender, EventArgs e)
        {	
			//pnlErrorMsg.Visible = false;

			try
			{   
				strTournamentCode = Request.QueryString["TCode"];
				strTournamentCode = MGCommon.MGGeneric.DecryptData(strTournamentCode.Trim());
	        	
				if (Session["USERID"] != null ||
				    Session["USERTYPE"] != null)
				{	
					//if(ddlTournamentEvents.Items.Count == 0 )
					{
				//		PopulateTournamentDetails (strSportCode,strTournamentCode);
				//		PopulateOwnerDashboard(strTournamentCode);
					}
				//	GenerateParticipatedEvents ();
					
				}
				else
				{
					Response.Redirect("./TP_Login.aspx", false);
				}
			}
			catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_Dashboard.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
                Response.Redirect("./TP_Login.aspx", false);
        	}
			
        }
        
        protected void lbtnReturn2Dashboard_Click(object sender, EventArgs e)
        {        	
        	string strURL = "./TP_Dashboard.aspx?TCode=" + MGCommon.MGGeneric.EncryptData(strTournamentCode);
        	Response.Redirect(strURL, false);
        }
        
        
        #region Toggle Buttons
                
        protected void btnSetupTournamentView_Click(object sender, EventArgs e)
        {
        	//Hide All other Panels
        	//HideAllOtherPanels();
        	//pnlSetupTournament.Visible = true;
        	
        	if (Session["USERID"] != null ||
				Session["USERTYPE"] != null)
        	{
        		string strUserType = Session["USERTYPE"].ToString();
        		
        		if (strUserType.Equals("OWNER"))
        		{
        			string strTCode = MGCommon.MGGeneric.EncryptData(strTournamentCode.Trim());
        			string strUrl = "./TP_TournamentSetup.aspx?TCode=" + strTCode;
        			Response.Redirect(strUrl,false);
        		}
        		else
        		{
        			Response.Redirect("./TP_Login.aspx", false);
        		}
        	}
        	else
			{
				Response.Redirect("./TP_Login.aspx", false);
			}
        }
        
        protected void GetPaymentSummary()
        {
        	try{
	        	strTournamentCode = (string)Session["TOURNAMENTCODE"]; 
	        	List<TPPaymentSummary> objlist =  (new TPDAL_Registration()).GetPaymentSummary(strTournamentCode);
	        	for(int index = 0; index < objlist.Count ; index++)
	        	{
	        		//if (objlist[index].PaymentType.ToUpper() == "COMPLETED")
	        			//lblTotalConfirmedAmount.Text = objlist[index].PaymentAmount;
	        		//else if (objlist[index].PaymentType.ToUpper() == "PENDING")
	        			//lblTotalPendingAmount.Text = objlist[index].PaymentAmount;
	        		//else if (objlist[index].PaymentType.ToUpper() == "STATUS UPDATE - IN PROGRESS")
	        			//lblTotalInProgressAmount.Text = objlist[index].PaymentAmount;
	        	}
        	}
        	catch (Exception ex)
        	{
				//pnlOwnerLogin.Visible = true;
	        	//pnlAfterLogin.Visible = false;
	        	
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_OwnerView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        }
        
        
        
        
        #endregion
        
        
        
        protected void rbPaymentReportOptions_CheckedChanged(object sender, EventArgs e)
        {
        	
        		string strPaymentStatus = rbPaymentReportOptions.SelectedItem.Text;
        		
				
				if (strPaymentStatus == "Pending")
				{
					btnSelectAndTakeAction.Text = "Send Payment Reminder";
					btnSelectChangeStatus.Visible = false;
					
				}else if (strPaymentStatus == "Status Update In Progress")
				{
					btnSelectAndTakeAction.Text = "Payment Confirmed";
					btnSelectChangeStatus.Text = "Change Status to Pending";
				}
				else 
				{
					btnSelectChangeStatus.Visible = false;
					btnSelectAndTakeAction.Visible = false;
					btnUpdateOfflineStatus.Visible = false;
				}
			
        }

        protected void btnSelectChangeStatus_Click(object sender, EventArgs e)
        {
        	try{
        
        	
        		string strTournamentCode = (String)Session["TOURNAMENTCODE"];
        		string strPaymentStatus = "";
        		strPaymentStatus = rbPaymentReportOptions.SelectedItem.Text;
        
        		string strAction1 =  btnSelectChangeStatus.Text ;
        		List<string> lstIDs = new List<string>();
        		string strID = "";
        		string strEmail, strPlayerCode, strPlayerName, strMobile, strDOB;
        		strEmail =  strPlayerCode =  strPlayerName =  strMobile =  strDOB = "";
        		
        		for (int index = 0; index < dgPayment.Items.Count ; index++)
        		{
        			if(((CheckBox)dgPayment.Items[index].FindControl("chkSelection")).Checked )
        			{
        				
        				strID =  ((HiddenField)dgPayment.Items[index].FindControl("hfPaymentID")).Value;
        				lstIDs.Add(strID);
        				if(strPaymentStatus == "Pending" && strAction1 == "Payment Confirmed")
        				{
        					//Select and Change status to Confirm
        					
        					(new TPDAL_Registration()).UpdatePaymentStatusFromOwnerView(strTournamentCode, strID, "Completed");
        					//send mail for Confirmation
        					strEmail = dgPayment.Items[index].Cells[3].Text;
        					strPlayerCode = dgPayment.Items[index].Cells[0].Text;
        					strPlayerName = dgPayment.Items[index].Cells[1].Text;
        					strMobile = dgPayment.Items[index].Cells[2].Text;
        					strDOB = dgPayment.Items[index].Cells[4].Text;
        					
        					SendConfirmationEmail(strEmail, strPlayerCode, strPlayerName, strMobile, strDOB, "Completed");

                            try
                            {
                                //SMS
                                string strSMSSwitch = System.Web.Configuration.WebConfigurationManager.AppSettings["SMS_PAYMENT_CONFIRMATION"];
                                if (strSMSSwitch.Equals("ON"))
                                    SendSMSForPaymentConfirmation(strPlayerName, strMobile, strEmail);
                            }
                            catch (Exception ex)
                            {
                                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_PaymentStatus.aspx", currentMethod.Name, "Error in Send SMS: " + ex.Message, DateTime.Now, "ip", "");
                                MGError objLogError = new MGError();
                                objLogError.logError(objError);
                            }
        					
        				}
        				else if(strPaymentStatus == "Status Update - In Progress" && strAction1 == "Change status to Pending")
        				{
        					//Select and Change to Pending
        					(new TPDAL_Registration()).UpdatePaymentStatusFromOwnerView(strTournamentCode, strID, "Pending");
        					strEmail = dgPayment.Items[index].Cells[3].Text;
        					strPlayerCode = dgPayment.Items[index].Cells[0].Text;
        					strPlayerName = dgPayment.Items[index].Cells[1].Text;
        					strMobile = dgPayment.Items[index].Cells[2].Text;
        					strDOB = dgPayment.Items[index].Cells[4].Text;
        					
        					SendFailedPayment(strEmail, strPlayerCode, strPlayerName, strMobile, strDOB, "Pending");

                            try
                            {
                                //SMS
                                string strSMSSwitch = System.Web.Configuration.WebConfigurationManager.AppSettings["SMS_PAYMENT_FAILED"];
                                if (strSMSSwitch.Equals("ON"))
                                   SendSMSForPaymentFailed(strPlayerName, strMobile, strEmail);
                            }
                            catch (Exception ex)
                            {
                                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_PaymentStatus.aspx", currentMethod.Name, "Error in Send SMS: " + ex.Message, DateTime.Now, "ip", "");
                                MGError objLogError = new MGError();
                                objLogError.logError(objError);
                            }
        				
        				}
        			}
        			
        		}
        		btnGetPaymentList_Click(null,null);
        		
            }
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_OwnerView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
                
        }
        
        protected void btnUpdateOfflineStatus_Click(object sender, EventArgs e)
        {
        	try{
        
        	
        		string strTournamentCode = (String)Session["TOURNAMENTCODE"];
        		//string strPaymentStatus = "";
        	
        		List<string> lstIDs = new List<string>();
        		string strID = "";
        		string strEmail, strPlayerCode, strPlayerName, strMobile, strDOB;
        		strEmail =  strPlayerCode =  strPlayerName =  strMobile =  strDOB = "";
        		
        		for (int index = 0; index < dgPayment.Items.Count ; index++)
        		{
        			if(((CheckBox)dgPayment.Items[index].FindControl("chkSelection")).Checked )
        			{
        				
        				strID =  ((HiddenField)dgPayment.Items[index].FindControl("hfPaymentID")).Value;
        				lstIDs.Add(strID);
    					
    					(new TPDAL_Registration()).UpdateOfflinePaymentStatusFromOwnerView(strTournamentCode, strID);
    					//send mail for Confirmation
    					strEmail = dgPayment.Items[index].Cells[3].Text;
    					strPlayerCode = dgPayment.Items[index].Cells[0].Text;
    					strPlayerName = dgPayment.Items[index].Cells[1].Text;
    					strMobile = dgPayment.Items[index].Cells[2].Text;
    					strDOB = dgPayment.Items[index].Cells[4].Text;
    					
        				SendConfirmationEmail(strEmail, strPlayerCode, strPlayerName, strMobile, strDOB, "Completed");
        				
        			}
        			
        			
        		}
        		btnGetPaymentList_Click(null,null);
        	}
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_OwnerView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
                
        }

        protected void chkCheckAll_CheckedChanged(object sender, EventArgs e)
        {
        	try{
        		bool bcheck = false;
        		//if (((CheckBox)sender).Checked == true)
        		{
	        		bcheck = ((CheckBox)sender).Checked ;
        		}
    			for (int index = 0 ; index < dgPayment.Items.Count ; index++)
        		{
    				((CheckBox)dgPayment.Items[index].FindControl("chkSelection")).Checked = bcheck;
        		}
        	}
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_OwnerView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        }
		public void ExportPaymentList_Click(object sender, System.EventArgs e)
		{
        	try{
		  string strTitle = "PaymentStatus";
		  //new DataGridExcelExporter(this.dgPlayerList , this.Page).Export(strTitle);
		  
		  	string strfilename = "attachment;filename=" + strTitle + ".xls";
		  	Response.Clear();
			//Response.AddHeader("content-disposition",  "attachment;filename=FileName.xls");
			Response.AddHeader("content-disposition",  strfilename);
			Response.Charset = "";
			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			Response.ContentType = "application/vnd.xls";
			
			System.IO.StringWriter stringWrite = new System.IO.StringWriter();
			System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
			
			dgPayment.RenderControl(htmlWrite);
			Response.Write(stringWrite.ToString());
			Response.End();
			}
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_OwnerView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        
        }        
        protected void btnSelectAndTakeAction_Click(object sender, EventArgs e)
        {
        	try{
        
        	
        		string strTournamentCode = (String)Session["TOURNAMENTCODE"];
        		string strPaymentStatus = "";
        		strPaymentStatus = rbPaymentReportOptions.SelectedItem.Text;
        
        		string strAction1 =  btnSelectAndTakeAction.Text ;
        		List<string> lstIDs = new List<string>();
        		string strID = "";
        		string strEmail, strPlayerCode, strPlayerName, strMobile, strDOB;
        		strEmail =  strPlayerCode =  strPlayerName =  strMobile =  strDOB = "";
        		for (int index = 0; index < dgPayment.Items.Count ; index++)
        		{
        			if(((CheckBox)dgPayment.Items[index].FindControl("chkSelection")).Checked )
        			{
        				
        				strID =  ((HiddenField)dgPayment.Items[index].FindControl("hfPaymentID")).Value;
        				lstIDs.Add(strID);
        				if(strPaymentStatus == "Pending" && strAction1 == "Send Payment Reminder")
        				{
        					//send Reminder mail
        					strEmail = dgPayment.Items[index].Cells[3].Text;
        					strPlayerCode = dgPayment.Items[index].Cells[0].Text;
        					strPlayerName = dgPayment.Items[index].Cells[1].Text;
        					strMobile = dgPayment.Items[index].Cells[2].Text;
        					strDOB = dgPayment.Items[index].Cells[4].Text;

                            try
                            {
                                SendReminderEmail(strEmail, strPlayerCode, strPlayerName, strMobile, strDOB, "Pending");
                            }
                            catch (Exception ex)
                            {
                                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_PaymentStatus.aspx", currentMethod.Name, "Error in Send Email: " + ex.Message, DateTime.Now, "ip", "");
                                MGError objLogError = new MGError();
                                objLogError.logError(objError);
                            }


                            try
                            {
                                //SMS
                                string strSMSSwitch = System.Web.Configuration.WebConfigurationManager.AppSettings["SMS_PAYMENT_REMINDER"];
                                if (strSMSSwitch.Equals("ON"))
                                {
                                    SendSMSForPaymentReminder(strPlayerName, strMobile, strEmail);
                                }
                            }
                            catch (Exception ex)
                            {
                                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_PaymentStatus.aspx", currentMethod.Name, "Error in Send SMS: " + ex.Message, DateTime.Now, "ip", "");
                                MGError objLogError = new MGError();
                                objLogError.logError(objError);
                            }
        				}
        				else if(strPaymentStatus == "Status Update - In Progress" && strAction1 == "Payment Confirmed")
        				{
        					//select and confirm the payment
        					(new TPDAL_Registration()).UpdatePaymentStatusFromOwnerView(strTournamentCode, strID, "Completed");
        					//send confirmation mail
        					strEmail = dgPayment.Items[index].Cells[3].Text;
        					strPlayerCode = dgPayment.Items[index].Cells[0].Text;
        					strPlayerName = dgPayment.Items[index].Cells[1].Text;
        					strMobile = dgPayment.Items[index].Cells[2].Text;
        					strDOB = dgPayment.Items[index].Cells[4].Text;
        					
        					try{

                                SendConfirmationEmail(strEmail, strPlayerCode, strPlayerName, strMobile, strDOB, "Confirmed");
                            }
                            catch (Exception ex)
                            {
                                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_PaymentStatus.aspx", currentMethod.Name, "Error in Send Email: " + ex.Message, DateTime.Now, "ip", "");
                                MGError objLogError = new MGError();
                                objLogError.logError(objError);
                            }

                            try
                            {
                                //SMS
                                string strSMSSwitch = System.Web.Configuration.WebConfigurationManager.AppSettings["SMS_PAYMENT_CONFIRMATION"];
                                if (strSMSSwitch.Equals("ON"))
                                    SendSMSForPaymentConfirmation(strPlayerName, strMobile, strEmail);
                            }
                            catch (Exception ex)
                            {
                                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_PaymentStatus.aspx", currentMethod.Name, "Error in Send SMS: " + ex.Message, DateTime.Now, "ip", "");
                                MGError objLogError = new MGError();
                                objLogError.logError(objError);
                            }
        				}
        			}
        			
        		}
        		btnGetPaymentList_Click(null, null);
            }
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_OwnerView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
                
        }

        #region EMAIL

        private void SendReminderEmail (string strEmail , string strPlayerCode, string strPlayerName, string strMobileNo, string strPlayerDOB, string strPaymentStatus)
        {
        	try
        	{
	        	StringBuilder sb = new StringBuilder();
	        	
        		string strToEmailID = strEmail;
	        	
	        	string strMyEmailSubject = "Payment Reminder";
	        	string strEmailSubject = new MGCommon.EMailTemplate().MailSubjectLine(strMyEmailSubject);
	        		
		        
	        	string strHeader = new MGCommon.EMailTemplate().EMailHeader();
	        	string strBody = new MGCommon.EMailTemplate().PaymentReminderMailBody (strPlayerName, strMobileNo, strTournamentCode, strPlayerCode, strPlayerDOB, strPaymentStatus);
	        	string strFooter = new MGCommon.EMailTemplate().EMailFooter();
	        	
	        	string strEmailBody = strBody + strFooter;
	        	
	        	MGCommon.MGGeneric.MGEMailServices(strToEmailID, strEmailSubject, strEmailBody);
        	}
            catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationForm.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
                Response.Redirect("./TP_BD_Home.aspx", false);
            }
        }
     
        private void SendConfirmationEmail (string strEmail , string strPlayerCode, string strPlayerName, string strMobileNo, string strPlayerDOB, string strPaymentStatus)
        {
        	try
        	{
	        	StringBuilder sb = new StringBuilder();
	        	
        		string strToEmailID = strEmail;
	        	
	        	string strMyEmailSubject = "Payment confirmed";
	        	string strEmailSubject = new MGCommon.EMailTemplate().MailSubjectLine(strMyEmailSubject);
	        		
		        
	        	string strHeader = new MGCommon.EMailTemplate().EMailHeader();
	        	string strBody = new MGCommon.EMailTemplate().ConfirmPaymentMailBody(strPlayerName, strMobileNo, strTournamentCode, strPlayerCode, strPlayerDOB, strPaymentStatus);
	        	string strFooter = new MGCommon.EMailTemplate().EMailFooter();
	        	
	        	string strEmailBody = strBody + strFooter;
	        	
	        	MGCommon.MGGeneric.MGEMailServices(strToEmailID, strEmailSubject, strEmailBody);
        	}
            catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationForm.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
                Response.Redirect("./TP_BD_Home.aspx", false);
            }
        }
				
     	private void SendFailedPayment(string strEmail , string strPlayerCode, string strPlayerName, string strMobileNo, string strPlayerDOB, string strPaymentStatus)
        {
        	try
        	{
	        	StringBuilder sb = new StringBuilder();
	        	
        		string strToEmailID = strEmail;
	        	
	        	string strMyEmailSubject = "Payment Reminder";
	        	string strEmailSubject = new MGCommon.EMailTemplate().MailSubjectLine(strMyEmailSubject);
	        		
		        
	        	string strHeader = new MGCommon.EMailTemplate().EMailHeader();
	        	string strBody = new MGCommon.EMailTemplate().SendFailedPaymentMailBody(strPlayerName, strMobileNo, strTournamentCode, strPlayerCode, strPlayerDOB, strPaymentStatus);
	        	string strFooter = new MGCommon.EMailTemplate().EMailFooter();
	        	
	        	string strEmailBody = strBody + strFooter;
	        	
	        	MGCommon.MGGeneric.MGEMailServices(strToEmailID, strEmailSubject, strEmailBody);
        	}
            catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationForm.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
                Response.Redirect("./TP_BD_Home.aspx", false);
            }
        }

        #endregion

        #region SMS

        private void SendSMSForPaymentReminder(string strPlayerName, string strMobile, string strEmail)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                string strFirstName = strPlayerName;
                string strEmailID = strEmail;
                string strMobileNo = strMobile;

                (new MGCommon.BulkSMS()).SMS_Payment_Reminder (strFirstName, strMobileNo, strEmailID);
                
                //Update DB for SMS Count                
                int iCount = (new TPDAL_SMSTracker()).SMSTracker(strSportCode, strTournamentCode, 1);

            }
            catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationFormSimple.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);

                //Response.Redirect("./TP_BD_Home.aspx", false);
            }
        
        }

        private void SendSMSForPaymentConfirmation(string strPlayerName, string strMobile, string strEmail)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                string strFirstName = strPlayerName;
                string strEmailID = strEmail;
                string strMobileNo = strMobile;

                (new MGCommon.BulkSMS()).SMS_PaymentConfirmation(strFirstName, strMobileNo, strEmailID);

                //Update DB for SMS Count                
                int iCount = (new TPDAL_SMSTracker()).SMSTracker(strSportCode, strTournamentCode, 1);
            }
            catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationFormSimple.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);

                //Response.Redirect("./TP_BD_Home.aspx", false);
            }
        }

        private void SendSMSForPaymentFailed(string strPlayerName, string strMobile, string strEmail)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                string strFirstName = strPlayerName;
                string strEmailID = strEmail;
                string strMobileNo = strMobile;

                (new MGCommon.BulkSMS()).SMS_PaymentConfirmation(strFirstName, strMobileNo, strEmailID);

                //Update DB for SMS Count                
                int iCount = (new TPDAL_SMSTracker()).SMSTracker(strSportCode, strTournamentCode, 1);
            }
            catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationFormSimple.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);

                //Response.Redirect("./TP_BD_Home.aspx", false);
            }
        }


        #endregion


        protected void btnGetPaymentList_Click(object sender, EventArgs e)
        {
        	try{
        
        	
        		string strTournamentCode = (String)Session["TOURNAMENTCODE"];
        		string strPaymentStatus = "";
        		strPaymentStatus = rbPaymentReportOptions.SelectedItem.Text;
        		
				//Show Currently running tournaments
        		List<TPPlayer> lstPlayerList = null;
				lstPlayerList = (new TPDAL_TournamentController()).GetEventListByPaymentStatus(strSportCode, strTournamentCode, strPaymentStatus);
				
				dgPayment.DataSource = lstPlayerList;
				dgPayment.DataBind();
				lblTotalRow.Text = lstPlayerList.Count.ToString();
				PopulateTotalAmount(lstPlayerList);
				if (strPaymentStatus == "Pending")
				{
					btnSelectAndTakeAction.Text = "Send Payment Reminder";
					btnSelectAndTakeAction.Visible = true;
					btnSelectChangeStatus.Text = "Payment Confirmed";
					btnSelectChangeStatus.Visible = true;
					btnUpdateOfflineStatus.Visible = true;
				}else if (strPaymentStatus == "Status Update - In Progress")
				{
					btnSelectAndTakeAction.Text = "Payment Confirmed";
					btnSelectChangeStatus.Text = "Select and Change to Pending";
					btnSelectAndTakeAction.Visible = true;
					btnSelectChangeStatus.Visible = true;
					btnUpdateOfflineStatus.Visible = true;
				}
				else 
				{
					btnSelectChangeStatus.Visible = false;
					btnSelectAndTakeAction.Visible = false;
					btnUpdateOfflineStatus.Visible =false;
				}
        	}
        	
        	
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_OwnerView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
                
        }

		protected void PopulateTotalAmount (List<TPPlayer> objlist)
        {
        	try{
        		int iTotalAmount = 0;
        		for (int index=0 ; index < objlist.Count ; index++)
        		{
        			iTotalAmount += int.Parse( objlist[index].Amount);
        		}
        		lblTotalAmount.Text = iTotalAmount.ToString();
        	}
        	       	
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_OwnerView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        
        }
       	
		public void ExportPlayerList_Click(object sender, System.EventArgs e)
		{
        	try{
                string strTitle = "";// Label1.Text;
		  //new DataGridExcelExporter(this.dgPlayerList , this.Page).Export(strTitle);
		  
		  	string strfilename = "attachment;filename=" + strTitle + ".xls";
		  	Response.Clear();
			//Response.AddHeader("content-disposition",  "attachment;filename=FileName.xls");
			Response.AddHeader("content-disposition",  strfilename);
			Response.Charset = "";
			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			Response.ContentType = "application/vnd.xls";
			
			System.IO.StringWriter stringWrite = new System.IO.StringWriter();
			System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
			
			//dgPlayerList.RenderControl(htmlWrite);
			Response.Write(stringWrite.ToString());
			Response.End();
			}
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_OwnerView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        
        }

        private  void GenerateParticipatedEvents ()
        {		
        	//string strSportCode = (String)Session["SPORTCODE"];
        	string strTournamentCode = (String)Session["TOURNAMENTCODE"];
        	try{
	        	List<String> lstObj = (new TPDAL_Events()).GetParticipatedEvents(strSportCode, strTournamentCode);
	        	
	        	//var eventList = lstObj.Select(x => x.EventCode).Distinct();
	        	
	        	if (lstObj != null && lstObj.Count > 0)
	        	{
	        		Int32 i = 0;; //creattre a integer variable
	        		string strEventCode = "";
	        		LinkButton lb = new LinkButton();
					lb.Text = "[ All ] - ";        		
	        		lb.ID = i.ToString(); // LinkButton ID’s
		            lb.Attributes.Add("runat","server");
	                //lb.Click += new EventHandler(lb_Click);
	                lb.Command += new CommandEventHandler(lb_Command);//Create Handler for it.
	                lb.CommandName = "All"; // i.ToString(); //LinkButton CommanName
	                //PlaceHolder1.Controls.Add(lb); // Adding the LinkButton in PlaceHolder
		                
	                i = 1;
	                
	        		foreach(var item in lstObj)
		            {
	        			strEventCode = item.ToString();
		             	//create instance of LinkButton                
		                lb = new LinkButton();
		                
		                if (i == (lstObj.Count() - 1)) // Last item
		                	lb.Text = "[ " + strEventCode + " ]"; //LinkButton Text
		                else
		                	lb.Text = "[ " + strEventCode + " ]" + "   -   "; //LinkButton Text
		                
		                lb.ID = i.ToString(); // LinkButton ID’s
		                lb.Attributes.Add("runat","server");
		                //lb.Click += new EventHandler(lb_Click);
		                lb.Command += new CommandEventHandler(lb_Command);//Create Handler for it.
		                lb.CommandName = strEventCode; // i.ToString(); //LinkButton CommanName
		                //PlaceHolder1.Controls.Add(lb); // Adding the LinkButton in PlaceHolder
		                i = i + 1;
		            
	        		}
	        	}
	       	}
        	catch (Exception ex)
        	{
				//pnlOwnerLogin.Visible = true;
	        	//pnlAfterLogin.Visible = false;
	        	
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_OwnerView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        }

        public void lb_Command(object sender, CommandEventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            String strSelectedLinkButton = e.CommandName;
            
            //Label1.Text = strSelectedLinkButton; // will display the which Linkbutton clicked
 
            {
                lnk.Font.Bold = true;
                lnk.ForeColor = System.Drawing.Color.Green;
            }
            
            ShowPlayerList (strSelectedLinkButton);
        }
                
        private void ShowPlayerList (string strEventCode)
		{
        	try
        	{
        		string strTournamentCode = (String)Session["TOURNAMENTCODE"];
        		
				//Show Currently running tournaments
        		List<TPPlayer> lstPlayerList = null;
				lstPlayerList = (new TPDAL_TournamentController()).GetPlayerListByEvent(strSportCode, strTournamentCode, strEventCode);
				
				//dgPlayerList.DataSource = lstPlayerList;
				//dgPlayerList.DataBind();
				
				int iPlayerCount = lstPlayerList.Count;
				//Label1.Text = strEventCode + " ( " + iPlayerCount.ToString() + " )";
        	}
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_OwnerView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
        		Response.Redirect("./TP_Down.aspx");
        	}
		}
        
	}
}
