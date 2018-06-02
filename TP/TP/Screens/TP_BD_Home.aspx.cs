using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using TP.Entity;
using TP.DAL;
using System.Text;

namespace TournamentPlanner
{
    public partial class TP_BD_Home : System.Web.UI.Page
    {
    	String strSportCode = "BD";
    	String strTournamentCode = "";
    	
    	DataTable dtTournamentList = null;
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        	try
        	{
	        	Session["SPORTCODE"] = strSportCode;
	        	Session["TOURNAMENTCODE"] = "";
	        	
	        	strSportCode = (String)Session["SPORTCODE"];
	        	strTournamentCode = (String)Session["TOURNAMENTCODE"];
		        
				dtTournamentList = new DataTable("TournamentList");
				dtTournamentList.Columns.Add("ID");
				dtTournamentList.Columns.Add("TournamentName");
				dtTournamentList.Columns.Add("Organisation");
				dtTournamentList.Columns.Add("Entry Open");
				dtTournamentList.Columns.Add("Entry Closes");
				dtTournamentList.Columns.Add("Duration");
				dtTournamentList.Columns.Add("Venue");
				
				Session["TOURNAMENTLIST"] = dtTournamentList;

                if (!Page.IsPostBack)
                {

                    ShowTournaments("RUNNING");
                }
        	}
        	catch (Exception ex)
        	{
        		
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_BD_Home.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
        		Response.Redirect("./TP_Down.aspx");
        		
        	}
        }
        
        protected void btnRunning_Click(object sender, EventArgs e)
        {
        	string strTabText = btnRunning.Text; // "RUNNING";
        	ShowTournaments (strTabText);
        }
        
        protected void btnRegistrationOpen_Click(object sender, EventArgs e)
        {
        	string strTabText = btnRegistrationOpen.Text; // "REGISTRATION";
        	ShowTournaments (strTabText);
        }
        
        protected void btnUpcomingMatches_Click(object sender, EventArgs e)
        {
        	string strTabText = btnUpcomingMatches.Text; // "UPCOMING";
        	ShowTournaments (strTabText);
        }
        
        protected void btnHistory_Click(object sender, EventArgs e)
        {
        	string strTabText = btnHistory.Text; // "HISTORY";
        	ShowTournaments (strTabText);
        }
        
        private void AllPanelCollapse()
        {
        	pnlRunning.Visible = false;
        	btnRunning.Attributes["class"] = "tabMenuInactive_Atul";
        	        	
        	pnlRegistrationOpen.Visible = false;
        	btnRegistrationOpen.Attributes["class"] = "tabMenuInactive_Atul";
        	
        	pnlUpcoming.Visible = false;
        	btnUpcomingMatches.Attributes["class"] = "tabMenuInactive_Atul";
        	
        	pnlHistory.Visible = false;
        	btnHistory.Attributes["class"] = "tabMenuInactive_Atul";
        }
        
        private void ShowTournaments (string strTabText)
		{
        	try
        	{
        		List<TPTournament> lstObj = null;
        		
        		//Show Currently running tournaments
                if (strTabText.Equals(btnRunning.Text))
                {
                    lstObj = (new TPDAL_Tournament()).GetTournamentLists(strSportCode, strTournamentCode, "RUNNING"); // "RUNNING"

                    //dgRunningTournamentList.DataSource = lstObj;
                    //dgRunningTournamentList.DataBind();

                    AllPanelCollapse();
                    pnlRunning.Visible = true;
                    btnRunning.Attributes["class"] = "tabMenuActive_Atul";

                    ShowTPRunningInCardsFormat(lstObj);
                }
                else
                {
                    //Show registration open tournaments
                    if (!strTabText.Equals(btnRunning.Text))
                    {
                        lstObj = (new TPDAL_Tournament()).GetTournamentLists(strSportCode, strTournamentCode, "OPEN"); // "REGISTRATION"

                        //Check the count of Registration open tournament, if count zero then automatically open RUNNING tab
                        if (lstObj.Count <= 0) // Open RUNNING Tab
                        {                            
                            lstObj = (new TPDAL_Tournament()).GetTournamentLists(strSportCode, strTournamentCode, "UPCOMING"); // "UPCOMING"

                            if (lstObj.Count <= 0) // Open CLOSED Tab to show history
                            {

                                lstObj = (new TPDAL_Tournament()).GetTournamentLists(strSportCode, strTournamentCode, "CLOSED");

                                //dgClosedTournamentList.DataSource = lstObj;
                                //dgClosedTournamentList.DataBind();

                                AllPanelCollapse();
                                pnlHistory.Visible = true;
                                btnHistory.Attributes["class"] = "tabMenuActive_Atul";

                                ShowTPHistoryInCardsFormat(lstObj);
                            }
                            else
                            {
                                //dgUpcomingTournamentList.DataSource = lstObj;
                                //dgUpcomingTournamentList.DataBind();

                                AllPanelCollapse();
                                pnlUpcoming.Visible = true;
                                btnUpcomingMatches.Attributes["class"] = "tabMenuActive_Atul";

                                ShowTPUpComingInCardsFormat(lstObj);
                            }
                            
                        }
                        else //Open Registration Tab
                        {
                            //dgRegistrationOpenTournamentList.DataSource = lstObj;
                            //dgRegistrationOpenTournamentList.DataBind();

                            AllPanelCollapse();
                            pnlRegistrationOpen.Visible = true;
                            btnRegistrationOpen.Attributes["class"] = "tabMenuActive_Atul";

                            ShowTPRegistrationInCardsFormat(lstObj);
                        }

                    }
                }
				
				
				//Show registration open tournaments
				/*if (strTabText.Equals(btnUpcomingMatches.Text))
				{
                    
                        lstObj = (new TPDAL_Tournament()).GetTournamentLists(strSportCode, strTournamentCode, "UPCOMING"); // "REGISTRATION"

                        //dgUpcomingTournamentList.DataSource = lstObj;
                        //dgUpcomingTournamentList.DataBind();

                        AllPanelCollapse();
                        pnlUpcoming.Visible = true;
                        btnUpcomingMatches.Attributes["class"] = "tabMenuActive_Atul";

                        ShowTPUpComingInCardsFormat(lstObj);
                    
				}*/
				
				//Show finished tournaments				
				if (strTabText.Equals(btnHistory.Text))
				{
					lstObj = (new TPDAL_Tournament()).GetTournamentLists(strSportCode, strTournamentCode, "CLOSED");
				
					//dgClosedTournamentList.DataSource = lstObj;
					//dgClosedTournamentList.DataBind();
					
					AllPanelCollapse();
					pnlHistory.Visible = true;
					btnHistory.Attributes["class"] = "tabMenuActive_Atul";

                    ShowTPHistoryInCardsFormat(lstObj);
				}
        	}
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_BD_Home.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
        		Response.Redirect("./TP_Down.aspx");
        	}
		}

        private void ShowTPUpComingInCardsFormat(List<TPTournament> lstObj)
        {

            phTPUpComing.Controls.Remove (new Literal { Text = "" });


            int icount = lstObj.Count;

            for (int i = 0; i < icount; i++)
            {
                StringBuilder htmlTable = new StringBuilder();
                htmlTable.Append("<div style='padding-top:2px;background-color:#1E5C8B;'>");
                htmlTable.Append("</hr>");
                htmlTable.Append("</div>");

                if (i % 2 == 0)
                {
                    //Use div background color                    
                    htmlTable.Append("<div style='padding-top:2px;padding-bottom:5px;background-color:white;'>");
                }
                else
                {
                    //white background color
                    //htmlTable.Append("<div style='padding-top:7px;padding-bottom:5px;background-color:#F5F5F5'>");
                    htmlTable.Append("<div style='padding-top:2px;padding-bottom:5px;background-color:white;'>");
                }

                string strTournamentID = lstObj[i].TournamentCode.ToString();
                string strTournamentName = lstObj[i].TournamentName;
                string strTournamentOrganisation = lstObj[i].TournamentOrganisation;
                string strTournamentStartDate = lstObj[i].TournamentStartDate.ToString("MMM yyyy");
                string strTournamentDuration = lstObj[i].TournamentStartDate.ToString("dddd, dd MMM yyyy") + " - TO - " + lstObj[i].TournamentEndDate.ToString("dddd, dd MMMM yyyy");
                string strTournamentVenue = lstObj[i].TournamentVenue;
                string strContactDetails = lstObj[i].TournamentPOCContactNames + " " + lstObj[i].TournamentLocationContactNo;
                //string strContactDetails = lstObj[i].TournamentPOCContactNames + " " + lstObj[i].TournamentPOCContactNo;


                //Outer Table
                htmlTable.Append("<table border='0' width='98%' class='h4Text_Atul'>");
                htmlTable.Append("<tr>"); // Outer Table Row
                htmlTable.Append("<td width='75px' style='text-align:center;background-color: #F5F5F5;color: #1E5C8B;'>");  //Outer Table Data
                htmlTable.Append("<span class='h3BoldText_Atul'>" + strTournamentStartDate + "</span>");
                htmlTable.Append("</td>");  //Outer Table Data

                htmlTable.Append("<td style='padding-left:8px'>");  //Outer Table Data
                htmlTable.Append("<table border='0' width='100%' cellpadding='2px' class='h4Text_Atul'>");


                //htmlTable.Append("<tr>");
                //htmlTable.Append("<td style='width:100%;'>" + "Tournament Code: " + strTournamentID + "<br/></td>");
                //htmlTable.Append("</tr>");
                htmlTable.Append("<tr>");
                htmlTable.Append("<td style='text-decoration: underline;' class='h3BoldText_Atul'><a style='color:#1E5C8B' href=\"/screens/TP_TournamentDetails.aspx?TCode=" + strTournamentID + "\">" + " " + strTournamentName + "</a><br/></td>");
                htmlTable.Append("</tr>");
                htmlTable.Append("<tr>");
                htmlTable.Append("<td>" + "<b>BY:</b> " + strTournamentOrganisation + "<br/></td>");
                htmlTable.Append("</tr>");
                //htmlTable.Append("<tr>");
                //htmlTable.Append("<td>" + "Duration: " + strTournamentDuration + "<br/></td>");
                //htmlTable.Append("</tr>");
                htmlTable.Append("<tr>");
                htmlTable.Append("<td>" + "<b>Venue:</b> " + strTournamentVenue + "<br/></td>");
                htmlTable.Append("</tr>");
                htmlTable.Append("<tr>");
                htmlTable.Append("<td>" + "<b>Contacts:</b> " + strContactDetails + "<br/></td>");
                htmlTable.Append("</tr>");

                htmlTable.Append("</table>");

                htmlTable.Append("</td>");
                htmlTable.Append("</tr>"); //Outer Table Row Closed
                htmlTable.Append("</table>");  //Outer Table Closed


                htmlTable.Append("</div>");


                htmlTable.Append("<div style='padding-top:2px;background-color:#1E5C8B;'>");
                htmlTable.Append("</hr>");
                htmlTable.Append("</div>");

                phTPUpComing.Controls.Add(new Literal { Text = htmlTable.ToString() });
            }
        }

        private void ShowTPRegistrationInCardsFormat(List<TPTournament> lstObj)
        {
            int icount = lstObj.Count;

            for (int i = 0; i < icount; i++)
            {
                StringBuilder htmlTable = new StringBuilder();
                htmlTable.Append("<div style='padding-top:2px;background-color:#1E5C8B;'>");
                htmlTable.Append("</hr>");
                htmlTable.Append("</div>");

                if (i % 2 == 0)
                {
                    //Use div background color                    
                    htmlTable.Append("<div style='padding-top:2px;padding-bottom:5px;background-color:white;'>");
                }
                else
                {
                    //white background color
                    //htmlTable.Append("<div style='padding-top:7px;padding-bottom:5px;background-color:#F5F5F5'>");
                    htmlTable.Append("<div style='padding-top:2px;padding-bottom:5px;background-color:white;'>");
                }

                string strTournamentID = lstObj[i].TournamentCode.ToString();
                string strTournamentName = lstObj[i].TournamentName;
                string strTournamentOrganisation = lstObj[i].TournamentOrganisation;
                //string strTournamentStartDate = lstObj[i].TournamentStartDate.ToString("dddd dd MMM yyyy");
                string strRegistrationStartDate = lstObj[i].TournamentEntryOpenDate.ToString("dddd dd MMM yyyy");
                string strTournamentDuration = lstObj[i].TournamentStartDate.ToString("dddd, dd MMM yyyy") + " - TO - " + lstObj[i].TournamentEndDate.ToString("dddd, dd MMMM yyyy");
                string strTournamentVenue = lstObj[i].TournamentVenue;
                string strContactDetails = lstObj[i].TournamentPOCContactNames + " " + lstObj[i].TournamentLocationContactNo;
                //string strContactDetails = lstObj[i].TournamentPOCContactNames + " " + lstObj[i].TournamentPOCContactNo;


                //Outer Table
                htmlTable.Append("<table border='0' width='98%' class='h4Text_Atul'>");
                htmlTable.Append("<tr>"); // Outer Table Row
                htmlTable.Append("<td width='75px' style='text-align:center;background-color: #F5F5F5;color: #1E5C8B;'>");  //Outer Table Data
                htmlTable.Append("<span class='h3BoldText_Atul'>" + strRegistrationStartDate + "</span>");
                htmlTable.Append("</td>");  //Outer Table Data

                htmlTable.Append("<td style='padding-left:8px'>");  //Outer Table Data
                htmlTable.Append("<table border='0' width='100%' class='h4Text_Atul'>");


                //htmlTable.Append("<tr>");
                //htmlTable.Append("<td style='width:100%;'>" + "Tournament Code: " + strTournamentID + "<br/></td>");
                //htmlTable.Append("</tr>");
                htmlTable.Append("<tr>");
                htmlTable.Append("<td style='text-decoration: underline' class='h3BoldText_Atul'><a style='color:#1E5C8B' href=\"/screens/TP_TournamentDetails.aspx?TCode=" + strTournamentID + "\">" + " " + strTournamentName + "</a><br/></td>");
                htmlTable.Append("</tr>");
                htmlTable.Append("<tr>");
                htmlTable.Append("<td>" + "<b>BY:</b> " + strTournamentOrganisation + "<br/></td>");
                htmlTable.Append("</tr>");
                htmlTable.Append("<tr>");
                htmlTable.Append("<td>" + "<b>Duration:</b> " + strTournamentDuration + "<br/></td>");
                htmlTable.Append("</tr>");
                htmlTable.Append("<tr>");
                htmlTable.Append("<td>" + "<b>Venue:</b> " + strTournamentVenue + "<br/></td>");
                htmlTable.Append("</tr>");
                htmlTable.Append("<tr>");
                htmlTable.Append("<td>" + "<b>Contacts:</b> " + strContactDetails + "<br/></td>");
                htmlTable.Append("</tr>");

                htmlTable.Append("</table>");

                htmlTable.Append("</td>");
                htmlTable.Append("</tr>"); //Outer Table Row Closed
                htmlTable.Append("</table>");  //Outer Table Closed


                htmlTable.Append("</div>");


                htmlTable.Append("<div style='padding-top:2px;background-color:#1E5C8B;'>");
                htmlTable.Append("</hr>");
                htmlTable.Append("</div>");

                phTPRegistration.Controls.Add(new Literal { Text = htmlTable.ToString() });
            }
        }

        private void ShowTPRunningInCardsFormat(List<TPTournament> lstObj)
        {
            int icount = lstObj.Count;

            for (int i = 0; i < icount; i++)
            {
                StringBuilder htmlTable = new StringBuilder();
                htmlTable.Append("<div style='padding-top:2px;background-color:#1E5C8B;'>");
                htmlTable.Append("</hr>");
                htmlTable.Append("</div>");

                if (i % 2 == 0)
                {
                    //Use div background color                    
                    htmlTable.Append("<div style='padding-top:2px;padding-bottom:5px;background-color:white;'>");
                }
                else
                {
                    //white background color
                    //htmlTable.Append("<div style='padding-top:7px;padding-bottom:5px;background-color:#F5F5F5'>");
                    htmlTable.Append("<div style='padding-top:2px;padding-bottom:5px;background-color:white;'>");
                }

                string strTournamentID = lstObj[i].TournamentCode.ToString();
                string strTournamentName = lstObj[i].TournamentName;
                string strTournamentOrganisation = lstObj[i].TournamentOrganisation;
                string strTournamentStartDate = lstObj[i].TournamentStartDate.ToString("dddd dd MMM yyyy");
                string strTournamentDuration = lstObj[i].TournamentStartDate.ToString("dddd, dd MMM yyyy") + " - TO - " + lstObj[i].TournamentEndDate.ToString("dddd, dd MMMM yyyy");
                string strTournamentVenue = lstObj[i].TournamentVenue;
                string strContactDetails = lstObj[i].TournamentPOCContactNames + " " + lstObj[i].TournamentLocationContactNo;
                //string strContactDetails = lstObj[i].TournamentPOCContactNames + " " + lstObj[i].TournamentPOCContactNo;


                //Outer Table
                htmlTable.Append("<table border='0' width='98%' class='h4Text_Atul'>");
                htmlTable.Append("<tr>"); // Outer Table Row
                htmlTable.Append("<td width='75px' style='text-align:center;background-color: #F5F5F5;color: #1E5C8B;'>");  //Outer Table Data
                htmlTable.Append("<span class='h3BoldText_Atul'>" + strTournamentStartDate + "</span>");
                htmlTable.Append("</td>");  //Outer Table Data

                htmlTable.Append("<td style='padding-left:8px'>");  //Outer Table Data
                htmlTable.Append("<table border='0' width='98%' cellpadding='2px' class='h4Text_Atul'>");


                //htmlTable.Append("<tr>");
                //htmlTable.Append("<td style='width:100%;'>" + "Tournament Code: " + strTournamentID + "<br/></td>");
                //htmlTable.Append("</tr>");
                htmlTable.Append("<tr>");
                htmlTable.Append("<td style='text-decoration: underline' class='h3BoldText_Atul'><a style='color:#1E5C8B' href=\"/screens/TP_TournamentDetails.aspx?TCode=" + strTournamentID + "\">" + " " + strTournamentName + "</a><br/></td>");
                htmlTable.Append("</tr>");
                htmlTable.Append("<tr>");
                htmlTable.Append("<td>" + "<b>BY:</b> " + strTournamentOrganisation + "<br/></td>");
                htmlTable.Append("</tr>");
                htmlTable.Append("<tr>");
                htmlTable.Append("<td>" + "<b>Duration:</b> " + strTournamentDuration + "<br/></td>");
                htmlTable.Append("</tr>");
                htmlTable.Append("<tr>");
                htmlTable.Append("<td>" + "<b>Venue:</b> " + strTournamentVenue + "<br/></td>");
                htmlTable.Append("</tr>");
                htmlTable.Append("<tr>");
                htmlTable.Append("<td>" + "<b>Contacts:</b> " + strContactDetails + "<br/></td>");
                htmlTable.Append("</tr>");

                htmlTable.Append("</table>");

                htmlTable.Append("</td>");
                htmlTable.Append("</tr>"); //Outer Table Row Closed
                htmlTable.Append("</table>");  //Outer Table Closed


                htmlTable.Append("</div>");


                htmlTable.Append("<div style='padding-top:2px;background-color:#1E5C8B;'>");
                htmlTable.Append("</hr>");
                htmlTable.Append("</div>");

                phTPRunning.Controls.Add(new Literal { Text = htmlTable.ToString() });
            }
        }

        private void ShowTPHistoryInCardsFormat(List<TPTournament> lstObj)
        {
            int icount = lstObj.Count;

            for (int i = 0; i < icount; i++)
            {
                StringBuilder htmlTable = new StringBuilder();
                htmlTable.Append("<div style='padding-top:2px;background-color:#1E5C8B;'>");
                htmlTable.Append("</hr>");
                htmlTable.Append("</div>");

                if (i % 2 == 0)
                {
                    //Use div background color                    
                    htmlTable.Append("<div style='padding-top:2px;padding-bottom:5px;background-color:white;'>");
                }
                else
                {
                    //white background color
                    //htmlTable.Append("<div style='padding-top:7px;padding-bottom:5px;background-color:#F5F5F5'>");
                    htmlTable.Append("<div style='padding-top:2px;padding-bottom:5px;background-color:white;'>");
                }

                string strTournamentID = lstObj[i].TournamentCode.ToString();
                string strTournamentName = lstObj[i].TournamentName;
                string strTournamentOrganisation = lstObj[i].TournamentOrganisation;
                string strTournamentStartDate = lstObj[i].TournamentStartDate.ToString("dddd dd MMM yyyy");
                string strTournamentDuration = lstObj[i].TournamentStartDate.ToString("dddd, dd MMM yyyy") + " - TO - " + lstObj[i].TournamentEndDate.ToString("dddd, dd MMMM yyyy");
                string strTournamentVenue = lstObj[i].TournamentVenue;
                string strContactDetails = lstObj[i].TournamentPOCContactNames + " " + lstObj[i].TournamentLocationContactNo;
                //string strContactDetails = lstObj[i].TournamentPOCContactNames + " " + lstObj[i].TournamentPOCContactNo;


                //Outer Table
                htmlTable.Append("<table border='0' width='100%' class='h4Text_Atul'>");
                htmlTable.Append("<tr>"); // Outer Table Row
                htmlTable.Append("<td width='75px' style='text-align:center;background-color: #F5F5F5;color: #1E5C8B'>");  //Outer Table Data
                htmlTable.Append("<span class='h3BoldText_Atul'>" + strTournamentStartDate + "</span>");
                htmlTable.Append("</td>");  //Outer Table Data

                htmlTable.Append("<td style='padding-left:8px'>");  //Outer Table Data
                htmlTable.Append("<table border='0' width='100%' class='h4Text_Atul'>");

                
                //htmlTable.Append("<tr>");
                //htmlTable.Append("<td style='width:100%;'>" + "Tournament Code: " + strTournamentID + "<br/></td>");
                //htmlTable.Append("</tr>");
                htmlTable.Append("<tr>");
                htmlTable.Append("<td style='text-decoration: underline' class='h3BoldText_Atul'><a style='color:#1E5C8B' href=\"/screens/TP_TournamentDetails.aspx?TCode=" + strTournamentID + "\">" + " " + strTournamentName + "</a><br/></td>");
                htmlTable.Append("</tr>");
                htmlTable.Append("<tr>");
                htmlTable.Append("<td>" + "<b>BY:</b> " + strTournamentOrganisation + "<br/></td>");
                htmlTable.Append("</tr>");
                htmlTable.Append("<tr>");
                htmlTable.Append("<td>" + "<b>Duration:</b> " + strTournamentDuration + "<br/></td>");
                htmlTable.Append("</tr>");
                htmlTable.Append("<tr>");
                htmlTable.Append("<td>" + "<b>Venue:</b> " + strTournamentVenue + "<br/></td>");
                htmlTable.Append("</tr>");
                htmlTable.Append("<tr>");
                htmlTable.Append("<td>" + "<b>Contacts:</b> " + strContactDetails + "<br/></td>");
                htmlTable.Append("</tr>");

                htmlTable.Append("</table>");

                htmlTable.Append("</td>"); 
                htmlTable.Append("</tr>"); //Outer Table Row Closed
                htmlTable.Append("</table>");  //Outer Table Closed
                
                
                htmlTable.Append("</div>");


                htmlTable.Append("<div style='padding-top:2px;background-color:#1E5C8B;'>");
                htmlTable.Append("</hr>");
                htmlTable.Append("</div>");
                                
                phTPHistory.Controls.Add(new Literal { Text = htmlTable.ToString() });
            }
        }
        
        protected void btnRegistrationForm_Click(object sender, EventArgs e)
        {
            string strTournamentCode = (sender as LinkButton).CommandArgument;

            string strPath = HttpContext.Current.Request.Url.AbsoluteUri;

            string strRedirectURL = strPath.ToUpper().Replace("TP_BD_HOME.ASPX", "TP_TournamentDetails.aspx?TCode=" + strTournamentCode);
            Response.Redirect(strRedirectURL, false);

            //Open Registration form as per tournament registration form selection

			//Step1: Get the selected tournament form
			//Step2: redirect Tournamentform aspx accordingly			
            //try
            //{
            //    List<TPTournament> lstObj = null;
            //    string strRegistrationFormType = "";
            //    string strRedirectURL = "";
        		        		
            //    lstObj = (new TPDAL_Tournament()).GetTournamentLists(strSportCode, strTournamentCode, "REGISTRATION"); // "RUNNING"
				
            //    if (lstObj != null)
            //    {
            //        if (lstObj.Count > 0)
            //        {
            //            strRegistrationFormType = lstObj[0].TournamentRegistrationFormType;
            //        }
            //    }
				
            //    string strPath = HttpContext.Current.Request.Url.AbsolutePath;
            //    strPath = HttpContext.Current.Request.Url.AbsoluteUri;
				
            //    if (strRegistrationFormType.Trim().Equals("SIMPLE"))
            //    {
            //        //if(strPath.ToUpper().Contains("SCREENS"))
            //        strRedirectURL = strPath.ToUpper().Replace("TP_BD_HOME.ASPX", "TP_RegistrationFormSimple.aspx?TCode=" + strTournamentCode);
            //        //else
            //        //	strRedirectURL = "./screens/TP_RegistrationFormSimple.aspx?TCode=" + strTournamentCode;	
            //    }
				
            //    if (strRegistrationFormType.Trim().Equals("ACADEMY"))
            //    {
            //        //if(strPath.ToUpper().Contains("SCREENS"))
            //        strRedirectURL = strPath.ToUpper().Replace("TP_BD_HOME.ASPX", "TP_RegistrationFormAcademy.aspx?TCode=" + strTournamentCode);
            //        //else
            //        //	strRedirectURL = "./screens/TP_RegistrationFormAcademy.aspx?TCode=" + strTournamentCode;
            //    }
				
            //    if (strRegistrationFormType.Trim().Equals("CORPORATE"))
            //    {
            //        //if(strPath.ToUpper().Contains("SCREENS"))
            //        strRedirectURL = strPath.ToUpper().Replace("TP_BD_HOME.ASPX", "TP_RegistrationFormCorporate.aspx?TCode=" + strTournamentCode);
            //        //else
            //        //	strRedirectURL = "./screens/TP_RegistrationFormCorporate.aspx?TCode=" + strTournamentCode;
            //    }
				
            //    if (strRegistrationFormType.Trim().Equals("CORPORATE+TEAM"))
            //    {
            //        //if(strPath.ToUpper().Contains("SCREENS"))
            //        strRedirectURL = strPath.ToUpper().Replace("TP_BD_HOME.ASPX", "TP_TeamCorporateNavigation.aspx?TCode=" + strTournamentCode);
            //        //else
            //        //	strRedirectURL = "./screens/TP_TeamCorporateNavigation.aspx?TCode=" + strTournamentCode;
            //    }
				
            //    Response.Redirect (strRedirectURL, false);
				
            //}
            //catch (Exception ex)
            //{
            //    System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

            //    MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_BD_Home.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
            //    MGError objLogError = new MGError();
            //    objLogError.logError(objError);
                
            //    Response.Redirect("./TP_Down.aspx");
            //}
        }        
    }
}