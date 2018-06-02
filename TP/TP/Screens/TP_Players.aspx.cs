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
using System.Globalization;

namespace TournamentPlanner
{
    public partial class TP_Players : System.Web.UI.Page
    {
    	string strSportCode = "BD";
    	string strTournamentCode = "";
    	
        protected void Page_Load(object sender, EventArgs e)
        {	
			try
			{
				Session["SPORTCODE"] = strSportCode;
		        strTournamentCode = Request.QueryString["TCode"];
        		Session["TOURNAMENTCODE"] = strTournamentCode; 
				
	        	//if(IsPostBack)
				{
								
				}
				//else
				{
					PopulateTournamentSummary ();
                    GenerateParticipatedEvents();
                    if(!IsPostBack)
    					PopulatePlayers("All");
				}
			}
			catch (Exception ex)
			{
				System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_Players.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
				Response.Redirect("./TP_BD_Home.aspx");
			}
        }
        /*
        private void GenerateParticipatedEvents()
        {
            //string strSportCode = (String)Session["SPORTCODE"];
            string strTournamentCode = (String)Session["TOURNAMENTCODE"];
            try
            {
                List<String> lstObj = (new TPDAL_Events()).GetParticipatedEvents(strSportCode, strTournamentCode);

                //var eventList = lstObj.Select(x => x.EventCode).Distinct();

                if (lstObj != null && lstObj.Count > 0)
                {
                    Int32 i = 0; ; //creattre a integer variable
                    string strEventCode = "";
                    LinkButton lb = new LinkButton();
                    lb.Text = "[ All ] - ";
                    lb.ID = i.ToString(); // LinkButton ID’s
                    lb.Attributes.Add("runat", "server");
                    //lb.Click += new EventHandler(lb_Click);
                    lb.Command += new CommandEventHandler(lb_Command);//Create Handler for it.
                    lb.CommandName = "All"; // i.ToString(); //LinkButton CommanName
                    PlaceHolder1.Controls.Add(lb); // Adding the LinkButton in PlaceHolder

                    i = 1;

                    foreach (var item in lstObj)
                    {
                        strEventCode = item.ToString();
                        //create instance of LinkButton                
                        lb = new LinkButton();

                        if (i == (lstObj.Count() - 1)) // Last item
                            lb.Text = "[ " + strEventCode + " ]"; //LinkButton Text
                        else
                            lb.Text = "[ " + strEventCode + " ]" + "   -   "; //LinkButton Text

                        lb.ID = i.ToString(); // LinkButton ID’s
                        lb.Attributes.Add("runat", "server");
                        //lb.Click += new EventHandler(lb_Click);
                        lb.Command += new CommandEventHandler(lb_Command);//Create Handler for it.
                        lb.CommandName = strEventCode; // i.ToString(); //LinkButton CommanName
                        PlaceHolder1.Controls.Add(lb); // Adding the LinkButton in PlaceHolder
                        i = i + 1;

                    }
                }
            }
            catch (Exception ex)
            {
                //pnlOwnerLogin.Visible = true;
                //pnlAfterLogin.Visible = false;

                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_OwnerView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
            }
        }

        */
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

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_Players.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        
        }

        public void lb_Command(object sender, CommandEventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            String strSelectedLinkButton = e.CommandName;

            Label1.Text = strSelectedLinkButton; // will display the which Linkbutton clicked

            {
                lnk.Font.Bold = true;
                lnk.ForeColor = System.Drawing.Color.Green;
            }

            PopulatePlayers(strSelectedLinkButton);
        }

        private void GenerateParticipatedEvents()
        {
            //string strSportCode = (String)Session["SPORTCODE"];
            string strTournamentCode = (String)Session["TOURNAMENTCODE"];
            try
            {
                List<String> lstObj = (new TPDAL_Events()).GetParticipatedEvents(strSportCode, strTournamentCode);
                lstObj.Sort();
                //var eventList = lstObj.Select(x => x.EventCode).Distinct();

                if (lstObj != null && lstObj.Count > 0)
                {
                    Int32 i = 0; ; //creattre a integer variable
                    string strEventCode = "";
                    LinkButton lb = new LinkButton();
                    //lb.Text = " All  - ";
                    //lb.ID = i.ToString(); // LinkButton ID’s
                    //lb.Attributes.Add("runat", "server");
                    //lb.Click += new EventHandler(lb_Click);
                    //lb.Command += new CommandEventHandler(lb_Command);//Create Handler for it.
                    //lb.CommandName = "All"; // i.ToString(); //LinkButton CommanName
                    //PlaceHolder1.Controls.Add(lb); // Adding the LinkButton in PlaceHolder
                    //i = 1;
                    
                    foreach (var item in lstObj)
                    {
                        strEventCode = item.ToString();
                        //create instance of LinkButton                
                        lb = new LinkButton();

                        if (i == (lstObj.Count() - 1)) // Last item
                            lb.Text = "   " + strEventCode + "   "; //LinkButton Text
                        else
                            lb.Text = "   " + strEventCode + "   " + "   -   "; //LinkButton Text

                        lb.ID = i.ToString(); // LinkButton ID’s
                        lb.Attributes.Add("runat", "server");
                        //lb.Click += new EventHandler(lb_Click);
                        lb.Command += new CommandEventHandler(lb_Command);//Create Handler for it.
                        lb.CommandName = strEventCode; // i.ToString(); //LinkButton CommanName
                        PlaceHolder1.Controls.Add(lb); // Adding the LinkButton in PlaceHolder
                        i = i + 1;

                    }
                }
            }
            catch (Exception ex)
            {
                //pnlOwnerLogin.Visible = true;
                //pnlAfterLogin.Visible = false;

                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_OwnerView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
            }
        }

        private void PopulatePlayers(string strEventCode)
        {
            try
            {
                //Get all the player list from database
                //List<TPPlayer> lstObj = new TPDAL_Registration().GetAllPlayerList(strSportCode, strTournamentCode);

                //List<TPPlayer> lstPlayerList = null;
                List<TPPlayer> lstObj1 = (new TPDAL_TournamentController()).GetPlayerListByEvent(strSportCode, strTournamentCode, strEventCode);
                List<TPPlayer> lstObj = null;
                lstObj= lstObj1.OrderBy(x => x.PlayerFullName).ToList();

                String strFullName = "";
                StringBuilder htmlTable = new StringBuilder();
                htmlTable.Append("<table border='0' width='98%' cellpadding='2px' class='h5Text_Atul'>");
                //htmlTable.Append("<tr style='background-color:green; color: White;'><th>Customer ID.</th><th>Name</th><th>Address</th><th>Contact No</th></tr>");   

                if (lstObj != null)
                {
                    if (lstObj.Count > 0)
                    {
                        lblPlayerCount.Text = "Count: " + lstObj.Count;

                        strFullName = lstObj[0].PlayerFullName;
                        strFullName = strFullName.ToUpper();
                        String cFirstLetter = strFullName[0].ToString();
                        String cFirstLetterOfNextName;

                        //htmlTable.Append("<tr>");
                        int iNameCount = 0;
                        for (int i = 0; i < lstObj.Count; i++)
                        {
                            strFullName = lstObj[i].PlayerFullName;
                            strFullName = strFullName.ToUpper();


                            if (i > 0)
                            {
                                cFirstLetterOfNextName = strFullName[0].ToString();
                                if (!cFirstLetterOfNextName.Equals(cFirstLetter))
                                {
                                    cFirstLetter = cFirstLetterOfNextName;

                                    if (strFullName.ToUpper().StartsWith((cFirstLetter)))
                                    {
                                        htmlTable.Append("<tr width='100%'>");
                                        //htmlTable.Append("<td colspan='4' style='width:100%;border-right:0px;font: bold 16pt Calibri;color:rgb(116,116,116);'><hr/>" + cFirstLetter + "</td>");
                                        htmlTable.Append("<td colspan='4' style='width:100%;border-right:0px;font: bold 16pt Calibri;color:rgb(116,116,116);text-align:center;'><hr/>" + cFirstLetter + "</td>");
                                        //htmlTable.Append("<hr/>");
                                        htmlTable.Append("</tr>");

                                    }

                                    iNameCount = 0;
                                }
                            }
                            else
                            {
                                if (strFullName.ToUpper().StartsWith((cFirstLetter)))
                                {
                                    htmlTable.Append("<tr width='100%'>");
                                    //htmlTable.Append("<td colspan='4' style='width:100%;border-right:0px;font: bold 16pt Calibri;color:rgb(116,116,116);'>" + cFirstLetter + "</td>");
                                    htmlTable.Append("<td colspan='4' style='width:100%;border-right:0px;font: bold 16pt Calibri;color:rgb(116,116,116); text-align:center;'>" + cFirstLetter + "</td>");
                                    htmlTable.Append("</tr>");

                                }
                            }

                            //int iRemainder = i % 5;
                            int iRemainder = iNameCount % 4;
                            //if (iRemainder == 0 && iNameCount > 0)
                            if (iRemainder == 0)
                            {
                                htmlTable.Append("<tr>");
                            }

                            {
                                //"./TP_Events.aspx?TournamentCode=" + Eval("TournamentCode"
                                TextInfo myTextInfo = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo;
                                string strPlayerName = myTextInfo.ToTitleCase(lstObj[i].PlayerFullName.ToLower());

                                htmlTable.Append("<td class='h5Text_Atul' style='text-align:center;text-decoration: underline;width='20%'><a href=\"./TP_PlayersParticipation.aspx?TCode=" + strTournamentCode + "&PlayerCode=" + lstObj[i].PlayerCode + "\">" + strPlayerName + "</a></td>");

                                //htmlTable.Append("<td style='text-align:center;text-decoration: underline;width='20%''><a href=\"./TP_PlayersParticipation.aspx?PlayerCode="+lstObj[i].PlayerCode+"\">" + strPlayerName + "</a></td>");

                                //htmlTable.Append("<td style='text-align:center;text-decoration: underline;width='20%''><a href=\"./TP_PlayersParticipation.aspx?PlayerCode="+lstObj[i].PlayerCode+"\">" + lstObj[i].PlayerFullName + "</a></td>");
                            }

                            iNameCount = iNameCount + 1;

                            int iCloseTR = 1;
                            if (iNameCount > 1)
                                iCloseTR = (iNameCount) % 4;
                            if (iCloseTR == 0)
                                //if (iRemainder == 0)
                                htmlTable.Append("</tr>");


                        }
                        htmlTable.Append("</table>");
                        //DBDataPlaceHolder.Controls.Add(new Literal { Text = htmlTable.ToString() });   
                    }
                    else
                    {
                        lblMsg.Text = "Registration not yet done";
                        lblPlayerCount.Text = "Count: 0";
                    }

                    phPlayers.Controls.Add(new Literal { Text = htmlTable.ToString() });
                }
            }
            catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_Players.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
            }
        }

        //private void PopulatePlayers ()
        //{
        //    try
        //    {
        //        //Get all the player list from database
        //        List<TPPlayer> lstObj = new TPDAL_Registration().GetAllPlayerList(strSportCode, strTournamentCode);
				
        //        String strFullName = "";        	 
        //        StringBuilder htmlTable = new StringBuilder();
        //        htmlTable.Append("<table border='0' width='98%' cellpadding='2px' class='h5Text_Atul'>");   
        //        //htmlTable.Append("<tr style='background-color:green; color: White;'><th>Customer ID.</th><th>Name</th><th>Address</th><th>Contact No</th></tr>");   
	   
        //        if (lstObj != null)   
        //        {   
        //            if (lstObj.Count > 0)   
        //            {
        //                lblPlayerCount.Text = "PLAYERS " + lstObj.Count;
	                	
        //                strFullName = lstObj[0].PlayerFullName;
        //                strFullName = strFullName.ToUpper();
        //                String cFirstLetter = strFullName[0].ToString();
        //                String cFirstLetterOfNextName;
	    	
        //                //htmlTable.Append("<tr>");
        //                int iNameCount = 0;
        //                for (int i = 0; i < lstObj.Count; i++)   
        //                {   
        //                    strFullName = lstObj[i].PlayerFullName;
        //                    strFullName = strFullName.ToUpper();
	                    	
	                    	
        //                    if (i > 0)
        //                    {
        //                        cFirstLetterOfNextName = strFullName[0].ToString();
        //                        if (!cFirstLetterOfNextName.Equals(cFirstLetter))
        //                        {
        //                            cFirstLetter = cFirstLetterOfNextName;
	                    			
        //                            if (strFullName.ToUpper().StartsWith ( (cFirstLetter)))
        //                            {
        //                                htmlTable.Append("<tr width='100%'>");
        //                                //htmlTable.Append("<td colspan='4' style='width:100%;border-right:0px;font: bold 16pt Calibri;color:rgb(116,116,116);'><hr/>" + cFirstLetter + "</td>");
        //                                htmlTable.Append("<td colspan='4' style='width:100%;border-right:0px;font: bold 16pt Calibri;color:rgb(116,116,116);text-align:center;'><hr/>" + cFirstLetter + "</td>");
        //                                //htmlTable.Append("<hr/>");
        //                                htmlTable.Append("</tr>");
				                	
        //                            }

        //                            iNameCount = 0;
        //                        }
        //                    }
        //                    else
        //                    {                    	
        //                        if (strFullName.ToUpper().StartsWith ( (cFirstLetter)))
        //                        {
        //                            htmlTable.Append("<tr width='100%'>");
        //                            //htmlTable.Append("<td colspan='4' style='width:100%;border-right:0px;font: bold 16pt Calibri;color:rgb(116,116,116);'>" + cFirstLetter + "</td>");
        //                            htmlTable.Append("<td colspan='4' style='width:100%;border-right:0px;font: bold 16pt Calibri;color:rgb(116,116,116); text-align:center;'>" + cFirstLetter + "</td>");
        //                            htmlTable.Append("</tr>");
			                	
        //                        }
        //                    }
	                    	
        //                    //int iRemainder = i % 5;
        //                    int iRemainder = iNameCount % 4;
        //                    //if (iRemainder == 0 && iNameCount > 0)
        //                    if (iRemainder == 0)
        //                    {
        //                        htmlTable.Append("<tr>");
        //                    }
	                    	
        //                    {
        //                        //"./TP_Events.aspx?TournamentCode=" + Eval("TournamentCode"
        //                        TextInfo myTextInfo = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo;
        //                        string strPlayerName = myTextInfo.ToTitleCase(lstObj[i].PlayerFullName.ToLower());
                                
        //                        htmlTable.Append("<td class='h5Text_Atul' style='text-align:center;text-decoration: underline;width='20%'><a href=\"./TP_PlayersParticipation.aspx?TCode=" + strTournamentCode + "&PlayerCode=" + lstObj[i].PlayerCode + "\">" + strPlayerName + "</a></td>");
                                
        //                        //htmlTable.Append("<td style='text-align:center;text-decoration: underline;width='20%''><a href=\"./TP_PlayersParticipation.aspx?PlayerCode="+lstObj[i].PlayerCode+"\">" + strPlayerName + "</a></td>");
		                        
        //                        //htmlTable.Append("<td style='text-align:center;text-decoration: underline;width='20%''><a href=\"./TP_PlayersParticipation.aspx?PlayerCode="+lstObj[i].PlayerCode+"\">" + lstObj[i].PlayerFullName + "</a></td>");
        //                     }

        //                    iNameCount = iNameCount + 1;

        //                    int iCloseTR = 1;
        //                    if (iNameCount > 1)
        //                        iCloseTR = (iNameCount) % 4;
        //                    if (iCloseTR == 0)
        //                    //if (iRemainder == 0)
        //                        htmlTable.Append("</tr>");

                            
        //                }   
        //                htmlTable.Append("</table>");   
        //                //DBDataPlaceHolder.Controls.Add(new Literal { Text = htmlTable.ToString() });   
        //            }   
        //            else   
        //            {
        //                lblMsg.Text = "Player registration will be announced soon";   
        //            }   
	                
        //            phPlayers.Controls.Add(new Literal { Text = htmlTable.ToString() });
        //        }  
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

        //        MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_Players.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
        //        MGError objLogError = new MGError();
        //        objLogError.logError(objError);
        //    }
        //}
    }
}