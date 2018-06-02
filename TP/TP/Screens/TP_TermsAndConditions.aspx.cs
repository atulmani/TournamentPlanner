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

namespace TournamentPlanner
{
    public partial class TP_TermsAndConditions : System.Web.UI.Page
    {
    	string strSportCode = "";
    	string strTournamentCode = "";
    	    	
        protected void Page_Load(object sender, EventArgs e)
        {	
			try
			{
				Session["SPORTCODE"] = "BD";
		        Session["TOURNAMENTCODE"] = "BD_TP2"; 
				
				if (!Session["SPORTCODE"].Equals (null))
	        	{
		        	strSportCode = (String)Session["SPORTCODE"];
		        	strTournamentCode = (String)Session["TOURNAMENTCODE"];        	
	        	}
	        	else
	        	{
	        		Response.Redirect("./TP_BD_Home.aspx");
	        	}
	        	
	        	if(IsPostBack)
				{
								
				}
				else
				{
					PopulateTournamentSummary ();
				}
			}
			catch (Exception ex)
			{
				System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_TermsAndConditions.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
				Response.Redirect("./TP_BD_Home.aspx");
			}
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
								
                List<TPStatus> lstTPStatusObj = (new TPDAL_Tournament()).GetTournamentStatus(strSportCode, strTournamentCode);

                if (lstTPStatusObj != null && lstTPStatusObj.Count > 0)
                {
                    string strTournamentStatus = lstTPStatusObj[0].TournamentStatus;

                    if (strTournamentStatus.ToUpper().Equals("OPEN"))
                    {
                        lbtnRegistrationForm.Visible = true;
                    }
                    else
                    {
                        lbtnRegistrationForm.Visible = false;
                    }
                }
        	}
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_TermsAndConditions.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        
        }
           
    }
}