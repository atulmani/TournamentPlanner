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
    public partial class TP_Winners : System.Web.UI.Page
    {
        string strSportCode = "";
    	string strTournamentCode = "";
    	
        protected void Page_Load(object sender, EventArgs e)
        {	
			try
			{
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
					
					PopulateWinners();
				}
			}
			catch (Exception ex)
			{
				System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_Winners.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
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
				
				//Check Tournament Status accordingly show RegistrationForm link
				//If Status = OPEN then only show the link else hide
				//string strTournamentStatus = (new TPDAL_Tournament()).GetTournamentStatus(strSportCode, strTournamentCode);

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

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_Winners.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        
        }
        
        private void PopulateWinners ()
        {
        	try
        	{
	        	//Get all the participated events	        	
	        	List<String> lstObj = new TPDAL_Events().GetParticipatedEvents(strSportCode, strTournamentCode);
				
	        	String strEventName = "";        	 
	        	StringBuilder htmlTable = new StringBuilder();   
	        	htmlTable.Append("<table border='0' width='100%'>");   
	            //htmlTable.Append("<tr style='background-color:green; color: White;'><th>Customer ID.</th><th>Name</th><th>Address</th><th>Contact No</th></tr>");   
	   
	            if (lstObj != null)   
	            {   
	                if (lstObj.Count > 0)   
	                {
	                	htmlTable.Append("<tr>");
	                    for (int i = 0; i < lstObj.Count; i++)   
	                    {   
	                    	strEventName = lstObj[i];
	                    	
	                    	String strWinnerName = new TPDAL_Tournament().GetWinnerRunner(strSportCode, strTournamentCode, strEventName, "WINNER");
	                    	String strRunnerName = new TPDAL_Tournament().GetWinnerRunner(strSportCode, strTournamentCode, strEventName, "RUNNER");
	                    	
	                    	strWinnerName = "1 - " + strWinnerName;
	                    	strRunnerName = "2 - " + strRunnerName;
	                    	
	                    	htmlTable.Append("<tr width='100%'>");			                		
	                		htmlTable.Append("<td colspan='5' style='width:100%;border-right:0px;font: bold 18pt Calibri;color:rgb(116,116,116);'>" + strEventName + "<hr/></td>");
	                		htmlTable.Append("</tr>");
	                		htmlTable.Append("<tr width='100%'>");
	                		htmlTable.Append("<td style='text-align:center;width='20%''>" + strWinnerName + "</td>");
	                		htmlTable.Append("<td style='text-align:center;width='20%''>" + strRunnerName + "</td>");
	                		htmlTable.Append("</tr>");
	                    }
                    }   
                    htmlTable.Append("</table>");   
	                    //DBDataPlaceHolder.Controls.Add(new Literal { Text = htmlTable.ToString() });   
                } 
	                phWinners.Controls.Add(new Literal { Text = htmlTable.ToString() });
	             
        	}
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_Winners.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}

        
        }
    }
}