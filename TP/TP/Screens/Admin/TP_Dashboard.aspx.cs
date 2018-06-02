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
	public partial class TP_Dashboard : System.Web.UI.Page
	{	
		string strSportCode = "BD";
    	string strTournamentCode = "";
    	    	
        protected void Page_Load(object sender, EventArgs e)
        {	
			pnlErrorMsg.Visible = false;

			try
			{   
				strTournamentCode = Request.QueryString["TCode"];
				strTournamentCode = MGCommon.MGGeneric.DecryptData(strTournamentCode.Trim());
	        	
				if (Session["USERID"] != null ||
				    Session["USERTYPE"] != null)
				{	
					PopulateTournamentDetails (strSportCode,strTournamentCode);

                    string strUserType = Session["USERTYPE"].ToString();

                    if (strUserType.Equals("OWNER"))
                    {
                        PopulateOwnerDashboard(strTournamentCode);
                    }
                    if (strUserType.Equals("UMPIRE"))
                    {
                        PopulateUmpireDashboard(strTournamentCode);
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

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_Dashboard.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
                Response.Redirect("./TP_Login.aspx", false);
        	}
			
        }
        
        
        #region PopulateTournamentSummary
        
        /// <summary>
        /// PopulateTournamentSummary -- The header part
        /// </summary>
        /// 
        
        private void PopulateTournamentDetails (string strSportCode, string strTournamentCode)
        {
        	try
        	{
        		//List<String> lstSetupEvents = null;
	        	List<TPTournament> lstObj = new List<TPTournament>();
				lstObj = (new TPDAL_Tournament()).GetTournamentLists(strSportCode, strTournamentCode, "");
								
				if (lstObj != null && lstObj.Count > 0)
				{

					//Populate Summary
					lblTournamentID.Text = lstObj[0].TournamentCode;
					lblTournamentName.Text = lstObj[0].TournamentName;
					lblTournamentOrganisation.Text =  lstObj[0].TournamentOrganisation;
					lblTournamentVenue.Text = lstObj[0].TournamentVenue;
					lblOwnerName.Text = lstObj[0].TournamentOwnerName;
					lblOwnerID.Text = lstObj[0].TournamentOwnerIDType + ": " + lstObj[0].TournamentOwnerIDNo;
					lblOwnerAddress.Text = lstObj[0].TournamentOwnerAddress;
					                	
                	string strCBLEvents = lstObj[0].TournamentEvents;
                	
                	
				}
				
				//Get all the participated events	        	
	        	List<String> lstParticipatedEvents = new TPDAL_Events().GetParticipatedEvents(strSportCode, strTournamentCode);
					
	        	if (lstParticipatedEvents.Count > 0)
	        	{
					//ddlTournamentEvents.DataSource = lstParticipatedEvents;
				
					//ddlTournamentEvents.DataBind();	
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
        
        
        private bool PopulateOwnerDashboard	(String strTournamentCode)
        {
        	bool bIsUserExists = true;
        	//String strUserType = "OWNER";
        	
        	try
            {
        		TPOwnerDashboard objDashboard = (new TPDAL_Tournament()).GetOwnerDashboard(strTournamentCode);
        		lblTotalPlayer.Text = objDashboard.iTotalPlayer.ToString();
        		lblTotalEntries.Text = objDashboard.iTotalEntry.ToString();
        		lblSinglesEntries.Text = objDashboard.iTotalSingleEntry.ToString();
        		lblDoublesEntries.Text = objDashboard.iTotalDoubleEntry.ToString();
        		lblTotalFeesReceivable.Text = objDashboard.iTotalFees.ToString() ;
        		lblTotalPaymentReceived.Text = objDashboard.iTotalAmountReceived.ToString();
        		lblSinglesAmount.Text = objDashboard.iTotalSingleAmount.ToString();
        		lblDoublesAmount.Text = objDashboard.iTotalDoubleAmount.ToString();
        		lblTotalAmountReceived.Text = objDashboard.iTotalAmountReceived.ToString();        		
        		lblTotalAmountPending.Text = objDashboard.iTotalPendingAmount.ToString();
        		lblTotalRegAmountReceivable.Text = objDashboard.iTotalRegistrationAmount.ToString();
        		lblTotalRegAmountReceived.Text = objDashboard.iTotalRegistrationAmountReceived.ToString();
        
        	}
        	catch (Exception ex)
        	{
				//pnlOwnerLogin.Visible = true;
	        	pnlAfterLogin.Visible = false;
	        	
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_OwnerView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        	return bIsUserExists;
        }


        private bool PopulateUmpireDashboard(String strTournamentCode)
        {
            bool bIsUserExists = true;
            
            try
            {
                if (Session["USERID"] != null ||
                    Session["USERTYPE"] != null)
                {
                    string strUserType = Session["USERTYPE"].ToString();

                    if (strUserType.Equals("UMPIRE"))
                    {


                        //TPOwnerDashboard objDashboard = (new TPDAL_Tournament()).GetOwnerDashboard(strTournamentCode);
                        pnlTournamentSummary.Visible = false;
                        
                        btnSetupTournamentView.Visible = false;
                        btnPaymentView.Visible = false;
                        btnSetupDrawsView.Visible = false;
                        btnSetupMatchScheduleView.Visible = false;
                        btnPlayerListView.Visible = false;
                        btnOfflineRegistration.Visible = false;
                        btnSMSSetup.Visible = false;
                        btnCreateUmpire.Visible = false;
                    }                    
                }
                else
                {
                    Response.Redirect("./TP_Login.aspx", false);
                }
            }
            catch (Exception ex)
            {
                //pnlOwnerLogin.Visible = true;
                pnlAfterLogin.Visible = false;

                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_OwnerView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
            }
            return bIsUserExists;
        }

        
        #endregion
        
        #region Toggle Buttons
                
        protected void btnToggleQuickAccess_Click(object sender, EventArgs e)
        {
        	if (pnlQuickAccess.Visible == true)
        	{
        		HideAllOtherPanels();
        		pnlQuickAccess.Visible = false;        		
        	}
        	else
        	{
        		HideAllOtherPanels();
        		pnlQuickAccess.Visible = true;        		
        	}
        }
        	
        private void HideAllOtherPanels()
        {
        //	pnlSetupTournament.Visible = false;
        //	pnlPlayerList.Visible = false;
        //	pnlGenerateDraws.Visible = false;
        	pnlQuickAccess.Visible = false;
        //	pnlPayment.Visible = false;
        //	pnlOfflineRegistration.Visible = false;
        	
        //	btnToggleQuickAccess.Visible = true;
        }
                
        protected void btnSetupTournamentView_Click(object sender, EventArgs e)
        {
        	//Hide All other Panels
        	//HideAllOtherPanels();
        	//pnlSetupTournament.Visible = true;
        	
        	if (Session["USERID"] != null ||
				Session["USERTYPE"] != null)
        	{
        		string strUserType = Session["USERTYPE"].ToString();
        		
        		if (strUserType.Equals("SUPERADMIN") ||
                    strUserType.Equals("OWNER"))
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
        
        protected void btnPaymentView_Click(object sender, EventArgs e)
        {
        	
        	if (Session["USERID"] != null ||
				Session["USERTYPE"] != null)
        	{
        		string strUserType = Session["USERTYPE"].ToString();

                if (strUserType.Equals("SUPERADMIN") || strUserType.Equals("OWNER"))
        		{
        			string strTCode = MGCommon.MGGeneric.EncryptData(strTournamentCode.Trim());
        			string strUrl = "./TP_PaymentStatus.aspx?TCode=" + strTCode;
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
        
        protected void btnPlayerListView_Click(object sender, EventArgs e)
        {
        	if (Session["USERID"] != null ||
				Session["USERTYPE"] != null)
        	{
        		string strUserType = Session["USERTYPE"].ToString();

                if (strUserType.Equals("OWNER") ||
                    strUserType.Equals("SUPERADMIN"))
        		{
        			string strTCode = MGCommon.MGGeneric.EncryptData(strTournamentCode.Trim());
        			string strUrl = "./TP_PlayerList.aspx?TCode=" + strTCode;
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
        
        protected void btnSetupDrawsView_Click(object sender, EventArgs e)
        {
        	if (Session["USERID"] != null ||
				Session["USERTYPE"] != null)
        	{
        		string strUserType = Session["USERTYPE"].ToString();
        		
        		if (strUserType.Equals("OWNER") ||
                    strUserType.Equals("SUPERADMIN"))
        		{
        			string strTCode = MGCommon.MGGeneric.EncryptData(strTournamentCode.Trim());
        			string strUrl = "./TP_GenerateDraw.aspx?TCode=" + strTCode;
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
        
        protected void btnSetupMatchScheduleView_Click(object sender, EventArgs e)
        {
        	//Hide All other Panels
        	//HideAllOtherPanels();
        	//pnlSetupTournament.Visible = true;
        }
        
        protected void btnUpdateMatchScore_Click(object sender, EventArgs e)
        {
        	if (Session["USERID"] != null ||
				Session["USERTYPE"] != null)
        	{
        		string strUserType = Session["USERTYPE"].ToString();

                if (strUserType.Equals("UMPIRE") ||
                    strUserType.Equals("OWNER") ||
                    strUserType.Equals("SUPERADMIN"))
        		{
        			string strTCode = MGCommon.MGGeneric.EncryptData(strTournamentCode.Trim());
        			string strUrl = "./TP_UpdateMatchScore.aspx?TCode=" + strTCode;
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


        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (Session["USERID"] != null ||
                Session["USERTYPE"] != null)
            {
                string strUserType = Session["USERTYPE"].ToString();

                if (strUserType.Equals("SUPERADMIN"))
                {
                    string strTCode = MGCommon.MGGeneric.EncryptData(strTournamentCode.Trim());
                    string strUrl = "./TP_ResetPassword.aspx?TCode=" + strTCode;
                    Response.Redirect(strUrl, false);
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


        protected void btnOfflineRegistration_Click(object sender, EventArgs e)
        {
            if (Session["USERID"] != null ||
                Session["USERTYPE"] != null)
            {
                string strUserType = Session["USERTYPE"].ToString();

                if (strUserType.Equals("OWNER") ||
                    strUserType.Equals("SUPERADMIN"))
                {
                    string strTCode = MGCommon.MGGeneric.EncryptData(strTournamentCode.Trim());
                    string strUrl = "./TP_OfflineRegistration.aspx?TCode=" + strTCode;                    
                    Response.Redirect(strUrl, false);
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


        protected void btnSMSSetup_Click(object sender, EventArgs e)
        {
            if (Session["USERID"] != null ||
                Session["USERTYPE"] != null)
            {
                string strUserType = Session["USERTYPE"].ToString();

                if (strUserType.Equals("OWNER") ||
                    strUserType.Equals("SUPERADMIN"))
                {
                    string strTCode = MGCommon.MGGeneric.EncryptData(strTournamentCode.Trim());
                    string strUrl = "./TP_SMSSetup.aspx?TCode=" + strTCode;
                    Response.Redirect(strUrl, false);
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

        protected void btnCreateUmpire_Click(object sender, EventArgs e)
        {
            if (Session["USERID"] != null ||
                Session["USERTYPE"] != null)
            {
                string strUserType = Session["USERTYPE"].ToString();

                if (strUserType.Equals("OWNER") ||
                    strUserType.Equals("SUPERADMIN"))
                {
                    string strTCode = MGCommon.MGGeneric.EncryptData(strTournamentCode.Trim());
                    string strUrl = "./TP_CreateUmpire.aspx?TCode=" + strTCode;
                    Response.Redirect(strUrl, false);
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
        
        #endregion
        
        
        
		protected void PopulateTotalAmount (List<TPPlayer> objlist)
        {
        	try{
        		int iTotalAmount = 0;
        		for (int index=0 ; index < objlist.Count ; index++)
        		{
        			iTotalAmount += int.Parse( objlist[index].Amount);
        		}
        		
                //lblTotalAmount.Text = iTotalAmount.ToString();
        	}
        	       	
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_OwnerView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        
        }
	}
}
