using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using TP.DAL;
using TP.Entity;

namespace TournamentPlanner
{
    public partial class TP_TeamCorporateNavigation : System.Web.UI.Page
    {
    	String strSportCode = "BD";
    	String strTournamentCode = "";
    	
        protected void Page_Load(object sender, EventArgs e)
        {   
        	
        	Session["SPORTCODE"] = strSportCode;		
        	try
        	{
        		strTournamentCode = Request.QueryString["TCode"];
        		Session["TOURNAMENTCODE"] = strTournamentCode; 
        		
        		if (string.IsNullOrEmpty(strTournamentCode))
        		{
        			strTournamentCode = (String)Session["TOURNAMENTCODE"];
        		}
        	}
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_Events.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
        		strTournamentCode = (String)Session["TOURNAMENTCODE"];
        	}
        	
        	Session["TOURNAMENTCODE"] = strTournamentCode;
        	
        	if(IsPostBack)
			{
					
			}
			else
			{
				PopulateTournamentSummary ();
				
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
					//lblEntryOpenDate.Text = lstObj[0].TournamentEntryOpenDate.ToString ("dddd, dd MMMM yyyy");
					//lblEntryClosesDate.Text = lstObj[0].TournamentEntryCloseDate.ToString ("dddd, dd MMMM yyyy");
					//lblEntryWithdrawalDate.Text = lstObj[0].TournamentEntryWithdrawlDate.ToString ("dddd, dd MMMM yyyy");
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

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_Events.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        
        }
        
        protected void btnGO_Click(object sender, EventArgs e)
        {
        	string strURL = "";
        	
        	if (rbIndividualEvent.Checked)        	
        	{
        		//Go to RegistrationFormCorporate.aspx
        		strURL = "http://www.sportfit.co.in/sportfit/SF_RegistrationFormCorporate.aspx?TCode=" + strTournamentCode;        		
        	}
        	
        	if (rbTeamEvent.Checked)
        	{
        		//Go to RegistrationFormTeam.aspx
        		strURL = "http://www.sportfit.co.in/sportfit/SF_RegistrationFormTeam.aspx?TCode=" + strTournamentCode;        		
        	}
        	
        	Response.Redirect(strURL, false);
        	
        }
        
        
    }
}