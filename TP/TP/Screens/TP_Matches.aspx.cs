using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TP.DAL;
using TP.Entity;

namespace TournamentPlanner
{
    public partial class TP_Matches : System.Web.UI.Page
    {
        string strSportCode = "BD";
    	string strTournamentCode = "";
    	    	
        protected void Page_Load(object sender, EventArgs e)
        {			
        	try{
        		
	        	Session["SPORTCODE"] = strSportCode;
		        strTournamentCode = Request.QueryString["TCode"];
        		Session["TOURNAMENTCODE"] = strTournamentCode;
	        		        	
	        	if(IsPostBack)
				{
								
				}
				else
				{
					PopulateTournamentSummary ();
				//	PopulateMatches("4Feb");
                    PopulateMatches("");
				}

                List<TPStatus> lstTPStatusObj = (new TPDAL_Tournament()).GetTournamentStatus(strSportCode, strTournamentCode);

                if (lstTPStatusObj != null && lstTPStatusObj.Count > 0)
                {
                    string strMatchStatus = lstTPStatusObj[0].MatchSchedulePublished;
                    string strTournamentStatus = lstTPStatusObj[0].TournamentStatus;

                    if (strMatchStatus.ToUpper().Equals("ON"))
                    {
                        if (!string.IsNullOrEmpty(strTournamentCode))
                        {                            
                            pnlMatchPublished.Visible = true;
                            pnlMatchNotPublished.Visible = false;
                            pnlUpcomingMatches.Visible = true;
                        }
                    }
                    else
                    {
                        pnlMatchNotPublished.Visible = true;
                        pnlMatchPublished.Visible = false;
                        pnlUpcomingMatches.Visible = false;
                    }

                    if (strTournamentStatus.Equals("CLOSED"))
                    {
                        pnlUpcomingMatches.Visible = false;
                    }
                }
        	}
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_Matches.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
				Response.Redirect("./TP_BD_Home.aspx");
        	}
        }
                
        protected void lbt3Feb_Click(object sender, EventArgs e)
        {
        	try
        	{
        		PopulateMatches("3Feb");
        	}
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_Matches.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
				Response.Redirect("./TP_BD_Home.aspx");
        	}
        }
        protected void lbt4Feb_Click(object sender, EventArgs e)
        {
        	try
        	{
        		PopulateMatches("4Feb");
        	}
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_Matches.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
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

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_Matches.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnViewUpcomingMatches_Click(object sender, EventArgs e)
        {
            string strURL = "./TP_Match_Upcoming.aspx?TCode=" + strTournamentCode;
            Response.Redirect(strURL);
        }


        /// <summary>
        /// Populate Matches
        /// </summary>
        private void PopulateMatches (string strDate)
		{
        	try
        	{
        		string strEventCode = "";
				//lstObj = new List<TPMatch>();
				//List<TPMatch> lstObj = null;
				List<TPMatch> lstObj = (new TPDAL_Match()).GetMatchesBySportANDTournament(strSportCode, strTournamentCode, strEventCode,strDate);

                if (lstObj != null && lstObj.Count > 0)
                {
                    pnlMatchNotPublished.Visible = false;
                    pnlMatchPublished.Visible = true;
                    pnlUpcomingMatches.Visible = true;

                    dgMatchList.DataSource = lstObj;
                    dgMatchList.DataBind();
                }
                else
                {
                    pnlMatchNotPublished.Visible = true;
                    pnlMatchPublished.Visible = false;
                    pnlUpcomingMatches.Visible = false;
                }
        	}
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_Matches.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}			
		}
        
        protected void dgMatchList_RowDataBound(object sender, DataGridItemEventArgs e)
		{
        	if (e.Item.Cells[9].Text.Equals("First"))
        	{
        		//e.Item.Cells[3].BackColor = System.Drawing.Color.Blue;
        		e.Item.Cells[4].Font.Bold = true; 
        	}
        	
        	if (e.Item.Cells[9].Text.Equals("Second"))
        	{
        		//e.Item.Cells[3].BackColor = System.Drawing.Color.Blue;
        		e.Item.Cells[5].Font.Bold = true; 
        	}
		}
                
    }
}