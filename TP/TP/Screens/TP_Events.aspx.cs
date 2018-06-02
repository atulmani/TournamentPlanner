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
    public partial class TP_Events : System.Web.UI.Page
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
				
				PopulateEvents();
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
        
        private void PopulateEvents ()
        {
        	try
        	{
	        	//Get all the player list from database
	        	String strEvents = (new TPDAL_Events()).GetAllEventsBySportANDTournament(strSportCode, strTournamentCode);
				
				String[] lstObj = strEvents.Split(new []{"; "}, StringSplitOptions.None);
	        	int iEventCount = lstObj.Length - 1;
	        	
	        	String strEventName = "";
				String strEventEntries = "";			
				
	        	StringBuilder htmlTable = new StringBuilder();   
	        	htmlTable.Append("<table border='0' width='100%'>");   
	            //htmlTable.Append("<tr style='background-color:green; color: White;'><th>Name</th><th>Entries</th></tr>");   
	   
	            if (lstObj != null)   
	            {   
	            	if (iEventCount > 0)
	                {
	            		
	                	lblEventCount.Text = "EVENTS " + iEventCount;
	                	
	                	htmlTable.Append("<tr>");
	                    for (int i = 0; i < iEventCount; i++)						                    
	                    {   
	                    	strEventName = lstObj[i];
	                    	
	                    	
	                    	//Check Entries for EventName
	                    	int iEventEntryCount = (new TPDAL_Events()).GetEntriesbyEventANDTournament(strEventName, strTournamentCode);
	                    	
	                    	strEventEntries = iEventEntryCount.ToString();
	                    	                    	
	                        htmlTable.Append("<tr>");
	                    	
	                        htmlTable.Append("<td style='width:50%;border-right:0px;padding-left:10px;'>" + strEventName + "<hr/></td>");
                            htmlTable.Append("<td style='border-right:0px;text-decoration: underline;padding-left:15px;'><a href=\"./TP_Players.aspx?TCode=" + strTournamentCode + "\">" + strEventEntries + "</a><hr/></td>");
	                           
	                    }   
	                    htmlTable.Append("</table>");   
	                    //DBDataPlaceHolder.Controls.Add(new Literal { Text = htmlTable.ToString() });   
	                }
                    else
                    {
                        lblMsg.Text = "Events will be announced soon";
                    }  
	                
	                phEvents.Controls.Add(new Literal { Text = htmlTable.ToString() });
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
    }
}