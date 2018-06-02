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
	public partial class TP_Login : System.Web.UI.Page
	{	
		//string strSportCode = "BD";
    	string strTournamentCode = "";
    	    	
        protected void Page_Load(object sender, EventArgs e)
        {	
			try
			{	        	
	        	if(IsPostBack)
				{
									
				}
				else
				{
						
					
				}				
			}
			catch (Exception ex)
        	{
                string strExMsg = ex.Message;
        	}	
        }
        
        #region Login
        
        private string CheckUserAuthentication (TPLogin obj)
        {
        	string strUserType = "";
        	     	
        	try
            {
        		List<TPLogin> objList = (new TPDAL_UserAuthenticationAuthorization()).CheckUserAuthentication (obj);
        		
        		if (objList != null)
        		{
        			if (objList.Count > 0)
        			{
        				string strUserID = objList[0].UserID;
        				string strPWD = objList[0].UserPwd;
        				
        				if (obj.UserID.ToUpper().Equals(strUserID.ToUpper()) && 
        				    obj.UserPwd.ToUpper().Equals(strPWD.ToUpper()))
        				{   
        					strUserType = objList[0].UserType;
        				}
        			}
        		}
        	}
        	catch (Exception ex)
        	{	        	
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_OwnerView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        	
        	return strUserType;
        }
        
        protected void btnAuthenticationANDGenerateOTP_Click(object sender, EventArgs e)
        {
        	try
        	{	
        		Session["TOURNAMENTCODE"] = strTournamentCode = txtTournamentCode.Text.Trim().ToUpper();
        		
        		Session["USERNAME"] = txtUserCode.Text.Trim();
        		
        		string strUserCode = txtUserCode.Text.Trim();
        		        		
        		string strPassword = MGCommon.MGGeneric.EncryptData(txtOwnerPassword.Text.Trim());        	
        		//string strOTP = txtOwnerPassword.Text.Trim();        	        		
        		TPLogin obj = new TPLogin();
        		obj.TournamentCode = strTournamentCode.ToUpper();
        		obj.UserID = strUserCode;
                obj.UserPwd = strPassword;
                
                pnlLoginErrorMsg.Visible = false;
	        	
	        	//Check Username & Password
	        	string strUserType = CheckUserAuthentication (obj);
	        	
	        	if (!string.IsNullOrEmpty(strUserType))
	        	{
                    if (strUserType.ToUpper().Equals("SUPERADMIN"))
                    {
                        string strSuperAdmin = "Anita-Atul";
                        string strSuperAdminContact = "9822752885,9922112886";
                        string strRegisteredEmailID = "atulmani@gmail.com";
                        //Send Email to SuperAdmin

                        //Send OTP to SuperAdmin
                        string strSMSSwitch = System.Web.Configuration.WebConfigurationManager.AppSettings["SMS_ADMIN_LOGIN_SWITCH"];
                        if (strSMSSwitch.Equals("ON"))
                        {
                            Session["OTP"] = MGCommon.MGGeneric.GenerateAndSendOTPonEmailMobile(strTournamentCode, strSuperAdmin, strSuperAdminContact, strRegisteredEmailID);
                        }
                        else
                        {
                            Session["OTP"] = "3407";
                        }
                        Session["USERTYPE"] = strUserType;
                        pnlOwnerLogin.Visible = false;
                        pnlOTP.Visible = true;
                        
                    }
                    else if (strUserType.ToUpper().Equals("OWNER"))
                    {
                        TPTournament objOwnerDetails = (new TPDAL_Tournament()).GetTournamentOwnerDetails(strTournamentCode);

                        if (objOwnerDetails != null)
                        {
                            string strTOwnerName = objOwnerDetails.TournamentOwnerName;
                            string strTOwnerContactNo = objOwnerDetails.TournamentOwnerContactNo;
                            string strRegisteredEmailID = objOwnerDetails.TournamentOwnerEmailID;

                            if (!(string.IsNullOrEmpty(strTOwnerName)) && !(string.IsNullOrEmpty(strTOwnerContactNo)))
                            {
                                strTOwnerContactNo = strTOwnerContactNo + ",9822752885,9922112886";

                                Session["OTP"] = MGCommon.MGGeneric.GenerateAndSendOTPonEmailMobile(strTournamentCode, strTOwnerName, strTOwnerContactNo, strRegisteredEmailID);
                                Session["USERTYPE"] = strUserType;
                                pnlOwnerLogin.Visible = false;
                                pnlOTP.Visible = true;

                            }
                        }
                    }
                    else if (strUserType.ToUpper().Equals("UMPIRE"))
                    {
                        TPTournament objUmpireDetails = (new TPDAL_Tournament()).GetTournamentUmpireDetails(strTournamentCode, strUserCode);

                        if (objUmpireDetails != null)
                        {
                            string strTUmpireName = objUmpireDetails.TournamentOwnerName;
                            string strTUmpireContactNo = objUmpireDetails.TournamentOwnerContactNo;
                            string strRegisteredEmailID = strUserCode;

                            if (!(string.IsNullOrEmpty(strRegisteredEmailID)) && !(string.IsNullOrEmpty(strTUmpireContactNo)))
                            {
                                strTUmpireContactNo = strTUmpireContactNo + ",9822752885,9922112886";

                                Session["OTP"] = MGCommon.MGGeneric.GenerateAndSendOTPonEmailMobile(strTournamentCode, strTUmpireName, strTUmpireContactNo, strRegisteredEmailID);
                                Session["USERTYPE"] = strUserType;
                                pnlOwnerLogin.Visible = false;
                                pnlOTP.Visible = true;

                            }
                        }
                    }
                }
                else
                {
                    pnlOwnerLogin.Visible = true;
                    pnlOTP.Visible = false;

                    pnlLoginErrorMsg.Visible = true;
                    lblLoginErrorMsg.Text = "User Credentials are not valid";
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

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Session["TOURNAMENTCODE"] = strTournamentCode = txtTournamentCode.Text.Trim().ToUpper();

                Session["USERNAME"] = txtUserCode.Text.Trim();

                string strUserCode = txtUserCode.Text.Trim();

                //Check Username & Password
                string strUserType = (string)Session["USERTYPE"];

                pnlLoginErrorMsg.Visible = false;

                if (!string.IsNullOrEmpty(strUserType) && !string.IsNullOrEmpty(txtOTP.Text))
                {
                    if (txtOTP.Text.Equals((string)Session["OTP"]))
                    {
                        Session["USERID"] = strUserCode;
                        Session["USERTYPE"] = strUserType;

                        //Encrypt TournamentCode
                        string encrptedTCode = MGCommon.MGGeneric.EncryptData(strTournamentCode);

                        string strUrl = "";

                        if (strUserType.ToUpper().Equals("SUPERADMIN"))
                        {
                            strUrl = "./TP_AdminDashboard.aspx?TCode=" + encrptedTCode;
                        }
                        else if (strUserType.ToUpper().Equals("OWNER"))
                        {
                            strUrl = "./TP_Dashboard.aspx?TCode=" + encrptedTCode;
                        }
                        else if (strUserType.ToUpper().Equals("UMPIRE"))
                        {
                            strUrl = "./TP_Dashboard.aspx?TCode=" + encrptedTCode;
                        }

                        if (!string.IsNullOrEmpty(strUrl))
                        {
                            Response.Redirect(strUrl, false);
                        }
                    }
                    else
                    {
                        pnlLoginErrorMsg.Visible = true;
                        lblLoginErrorMsg.Text = "OTP is not valid";
                    }

                }
                else
                {
                    pnlLoginErrorMsg.Visible = true;
                    lblLoginErrorMsg.Text = "User Credentials are not valid";
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
        
        #endregion
	}
}
