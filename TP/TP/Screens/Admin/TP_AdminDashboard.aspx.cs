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
using System.IO;

namespace TournamentPlanner
{
	/// <summary>
	/// Description of TPAdmin
	/// </summary>
	public partial class TP_AdminDashboard : System.Web.UI.Page
	{	
    	string strTournamentCode = "";
    	    	
        protected void Page_Load(object sender, EventArgs e)
        {	
			
			try
			{   
				if (Session["USERID"] != null ||
				    Session["USERTYPE"] != null)
				{
                    strTournamentCode = Request.QueryString["TCode"];
                    strTournamentCode = MGCommon.MGGeneric.DecryptData(strTournamentCode.Trim());	
				}
				else
				{
					Response.Redirect("./TP_Login.aspx", false);
				}
			}
			catch (Exception ex)
        	{
                string strExMsg = ex.Message;
        		//System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                //MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_Dashboard.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                //MGError objLogError = new MGError();
                //objLogError.logError(objError);
                
                Response.Redirect("./TP_Login.aspx", false);
        	}
			
        }
                
                
        protected void btnCreateTournamentView_Click(object sender, EventArgs e)
        {        	
        	if (Session["USERID"] != null || Session["USERTYPE"] != null)
        	{
        		string strUserType = Session["USERTYPE"].ToString();
        		
        		if (strUserType.Equals("SUPERADMIN"))
        		{
        			//Open Create Tournament form
        			pnlCreateTournament.Visible = true;        			
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
        
        
        protected void btnCreateTournament_Click(object sender, EventArgs e)
        {
        	try
            {
                String strTournamentName = txtTournamentName.Text;

                if (!string.IsNullOrEmpty(strTournamentName))
                {
                	TPTournament objTournament = new TPTournament();             
                	
                	objTournament.SportCode = ddlSports.SelectedValue;
                	objTournament.TournamentName = txtTournamentName.Text;
                	objTournament.TournamentOrganisation = txtTournamentOrganisation.Text;
                	objTournament.TournamentVenue = txtTournamentVenue.Text;
                	objTournament.TournamentOwnerName = txtTournamentOwnerName.Text;
                	objTournament.TournamentOwnerContactNo = txtTournamentOwnerContactNo.Text;
                	objTournament.TournamentOwnerIDType = ddlTournamentOwnerIDType.SelectedItem.Text;                	
                	objTournament.TournamentOwnerIDNo = txtTournamentOwnerIDNo.Text;
                	objTournament.TournamentOwnerAddress = txtTournamentOwnerAddress.Text;
                    objTournament.TournamentOwnerEmailID = txtTournamentOwnerEmailID.Text;
                	
					int iCount = (new TPDAL_SuperAdmin()).CreateTournament (objTournament);
					
					
					string strLatestTournamentCode = (new TPDAL_Tournament()).GetLatestTournamentCode();
					
					//Reset Password for Owner					
                    string strOwnerPwd = txtTournamentOwnerContactNo.Text;					
					string encrptedPWD = MGCommon.MGGeneric.EncryptData(strOwnerPwd);
					
					string strSportCode = ddlSports.SelectedValue;
					string strTournamentCode = strLatestTournamentCode;
                    string strUserCode = txtTournamentOwnerEmailID.Text;
					string strUserPWD = encrptedPWD;
					
					iCount = (new TPDAL_SuperAdmin()).ResetPwd(strSportCode, strTournamentCode, strUserCode, strUserPWD);
					
					//Reset Password for Umpire
                    string strUmpirePwd = txtTournamentOwnerContactNo.Text;					
					encrptedPWD = MGCommon.MGGeneric.EncryptData(strUmpirePwd);

                    strUserCode = txtTournamentOwnerEmailID.Text;
					strUserPWD = encrptedPWD;
					
					iCount = (new TPDAL_SuperAdmin()).ResetPwd(strSportCode, strTournamentCode, strUserCode, strUserPWD);
					
                    //Send Email to Owner's Email ID
                    string strEmailStatus = SendEmail(strTournamentCode, 
                                                        txtTournamentName.Text,
                                                        txtTournamentOrganisation.Text,
                                                        txtTournamentOwnerName.Text, 
                                                        txtTournamentOwnerContactNo.Text, 
                                                        txtTournamentOwnerEmailID.Text);
   
                    //Send SMS to Owner's Mobile No
                    string strSMSStatus = SendSMS(strTournamentCode, txtTournamentOwnerName.Text, txtTournamentOwnerContactNo.Text);

					lblErrorMsg.Text = "Tournament created successfully. " + strEmailStatus + ". " + strSMSStatus;					
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "Tournament not created due to some exception";

                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_AdminDashboard.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
            }        
        }

        private string SendEmail(string strTournamentCode, 
                                 string strTournamentName,
                                 string strTournamentOrganization,
                                string strOwnerName, string strOwnerMobileNo, string strOwnerEmailID)
        {
            string strEmailStatus = "Email Sent Successfully";
            try{

                string strHTMLTournamentOnboarding = System.Web.Configuration.WebConfigurationManager.AppSettings["EMAIL_TEMPLATE_TOURNAMENT_ONBOARDING"];
                string strHTMLEmailBody = string.Empty;
                using (StreamReader reader = new StreamReader(Server.MapPath(strHTMLTournamentOnboarding)))
                {
                    strHTMLEmailBody = reader.ReadToEnd();
                }

                (new MGCommon.EMailTemplate()).Email_Tournament_Onboarding(strHTMLEmailBody, 
                                                                            strTournamentCode, 
                                                                            strTournamentName,
                                                                            strTournamentOrganization,
                                                                            strOwnerName, strOwnerMobileNo, strOwnerEmailID);
                
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "Tournament not created due to some exception";

                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_AdminDashboard.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);

                strEmailStatus = "Email Sending Failed";
            }

            return strEmailStatus;
        }

        private string SendSMS(string strTournamentCode, string strOwnerName, string strOwnerMobileNo)
        {
            string strSMSStatus = "SMS Sent Successfully";

            try
            {
                (new MGCommon.BulkSMS()).SMS_Tournament_Onboarding(strTournamentCode, strOwnerName, strOwnerMobileNo);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "Tournament not created due to some exception";

                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_AdminDashboard.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);

                strSMSStatus = "SMS Sending Failed";
            }

            return strSMSStatus;
        
        }
        
        protected void btnTPDashboardView_Click(object sender, EventArgs e)
        {
        	//Encrypt TournamentCode
			string encrptedTCode = MGCommon.MGGeneric.EncryptData(strTournamentCode);  					
	        		
        	//Navigate to Tournament Dashboard
        	string strUrl = "./TP_Dashboard.aspx?TCode=" + encrptedTCode;
        	Response.Redirect(strUrl, false);
        }
	}
}
