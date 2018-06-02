/*
 * Created by SharpDevelop.
 * User: 123222
 * Date: 6/17/2017
 * Time: 4:57 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TP.Entity;
using TP.DAL;

namespace TournamentPlanner
{
	/// <summary>
	/// Description of TP_CreateTournament
	/// </summary>
	public partial class TPSuperAdmin : Page
	{	
    	
		#region Page Load
		private void Page_Load(object sender, System.EventArgs e)
		{
			//Response.Write(@"Hello #Develop<br>");
			//------------------------------------------------------------------
			Session["SPORTCODE"] = "BD";
	        Session["TOURNAMENTCODE"] = "BD_TP1";
			
	        string strSportCode = (String)Session["SPORTCODE"];
	        
	        
	        
			if(IsPostBack)
			{
					
			}
			else
			{
				List<TPEvent> lstObj= (new TPDAL_Tournament()).GetEvents (strSportCode);
			
				cblEvents.DataSource = lstObj;
				cblEvents.DataTextField = "EventName";
        		cblEvents.DataValueField = "EventCode";
				cblEvents.DataBind();
			}
			//------------------------------------------------------------------
		}
		#endregion
				
		#region Login
        
        private bool CheckUserAuthentication (String strUserCode, String strOTP)
        {
        	bool bIsUserExists = true;
        	
        	try{
	        	
	        	String strUserType = "SUPERADMIN";
	        	
	        	bIsUserExists = true;  //(new TPDAL_UserAuthenticationAuthorization()).CheckUserAuthentication ("", strUserCode, strOTP, strUserType);	        	        	
	        	
        	}
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_SuperAdmin.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        	
        	return bIsUserExists;
        }
        
        protected void btnLogin_Click(object sender, EventArgs e)
        {
        	try
        	{	        	
        		string strUserCode = txtUserCode.Text.Trim();
        		string strOTP = txtOTPCode.Text.Trim();
        		string strCaptcha = txtCaptchaCode.Text.Trim();
	        		        	
	        	//Check Username & Password
	        	bool bIsUserExists = CheckUserAuthentication (strUserCode, strOTP);
	        	
	        	if (bIsUserExists == true)
	        	{	
		        	pnlToggleCreateTournament.Visible = true;
	        		pnlCreateTournament.Visible = true;
		        	
					pnlLogin.Visible = false;	
					pnlErrorMsg.Visible = false;					
	        	}
	        	else
	        	{
	        		pnlErrorMsg.Visible = true;
	        		lblErrorMsg.Text = "User Credentials are not valid";
	        	}
        	}
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_SuperAdmin.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        	
        }        
        #endregion
		
		protected void btnToggleCreateTournament_Click(object sender, EventArgs e)
		{
			if (btnToggleCreateTournament.Text.Equals("+ Create Tournament"))
			{
				btnToggleCreateTournament.Text = "- Create Tournament";
				pnlCreateTournament.Visible = true;
			}
			else
			{
				btnToggleCreateTournament.Text = "+ Create Tournament";
				pnlCreateTournament.Visible = false;
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
                	
					int iCount = (new TPDAL_SuperAdmin()).CreateTournament (objTournament);
					
					lblErrorMsg.Text = "Tournament created successfully";
					
                }
            }
            catch (Exception ex)
            {
            	lblErrorMsg.Text = "Tournament not created due to some exception";
            	
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_SuperAdmin.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
            }
			
		}
		
		
	}
}
