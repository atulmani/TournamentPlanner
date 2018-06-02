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
	public partial class TP_PlayerList : System.Web.UI.Page
	{	
		string strSportCode = "BD";
    	string strTournamentCode = "";
    	    	
        protected void Page_Load(object sender, EventArgs e)
        {	
			//pnlErrorMsg.Visible = false;

			try
			{   
				strTournamentCode = Request.QueryString["TCode"];
				strTournamentCode = MGCommon.MGGeneric.DecryptData(strTournamentCode.Trim());
	        	
				if (Session["USERID"] != null ||
				    Session["USERTYPE"] != null)
				{	
					
					GenerateParticipatedEvents ();
					
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

        protected void lbtnReturn2Dashboard_Click(object sender, EventArgs e)
        {
            string strURL = "./TP_Dashboard.aspx?TCode=" + MGCommon.MGGeneric.EncryptData(strTournamentCode);
            Response.Redirect(strURL, false);
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
				/*	lblTournamentID.Text = lstObj[0].TournamentCode;
					lblTournamentName.Text = lstObj[0].TournamentName;
					lblTournamentOrganisation.Text =  lstObj[0].TournamentOrganisation;
					lblTournamentVenue.Text = lstObj[0].TournamentVenue;
					lblOwnerName.Text = lstObj[0].TournamentOwnerName;
					lblOwnerID.Text = lstObj[0].TournamentOwnerIDType + ": " + lstObj[0].TournamentOwnerIDNo;
					lblOwnerAddress.Text = lstObj[0].TournamentOwnerAddress;
					
					//Populate Tournament Setup Section
					txtTournamentName.Text = lstObj[0].TournamentName;
					txtTournamentVenue.Text = lstObj[0].TournamentVenue;
					txtTournamentOrganisation.Text = lstObj[0].TournamentOrganisation;					
					txtTournamentStartDate.Text = lstObj[0].TournamentStartDate.ToString("dd/MM/yyyy");
					txtTournamentEndDate.Text = lstObj[0].TournamentEndDate.ToString("dd/MM/yyyy");
					txtTournamentPOCNames.Text = lstObj[0].TournamentPOCContactNames;
					txtTournamentPOCContacts.Text = lstObj[0].TournamentPOCContactNames;
					
                	//objTournament.TournamentEvents = strCBLEvents; 
                	//Write a code for event population
                */	
                	string strCBLEvents = lstObj[0].TournamentEvents;
                	
                	//string hobby = GetHobbyFromDB();
					/*string[] lstEvents = strCBLEvents.Split(new []{"; "}, StringSplitOptions.None);
					
					foreach (ListItem li in cblEvents.Items)
					{
					    li.Selected = lstEvents.Contains(li.Text);					    
					}
					
					txtOranisationLogo.Text = lstObj[0].OranisationLogo;
					txtTournamentLocationAddress.Text = lstObj[0].TournamentLocationAddress;					
                	txtTournamentLocationContactNos.Text = lstObj[0].TournamentLocationContactNo;                	
                	txtTournamentEntryOpenDate.Text = lstObj[0].TournamentEntryOpenDate.ToString("dd/MM/yyyy");
                	txtTournamentEntryEndDate.Text = lstObj[0].TournamentEntryCloseDate.ToString("dd/MM/yyyy");
                	txtTournamentWithdrawaldate.Text = lstObj[0].TournamentEntryWithdrawlDate.ToString("dd/MM/yyyy");
                	
                	txtTournamentDuration.Text = lstObj[0].TournamentDuration;
                	txtTournamentSponsers.Text = lstObj[0].TournamentSponsers;
                	*/
				}
				
				//Populate Tournament Status				
			/*	string strStatus = (new TPDAL_Tournament()).GetTournamentStatus(strSportCode, strTournamentCode);
				ddlTournamentStatus.SelectedItem.Text = strStatus;
			*/	
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
        
        
        
        
        
        #endregion
                               
		
		public void ExportPlayerList_Click(object sender, System.EventArgs e)
		{
        	try{
		  string strTitle = Label1.Text;
		  //new DataGridExcelExporter(this.dgPlayerList , this.Page).Export(strTitle);
		  
		  	string strfilename = "attachment;filename=" + strTitle + ".xls";
		  	Response.Clear();
			//Response.AddHeader("content-disposition",  "attachment;filename=FileName.xls");
			Response.AddHeader("content-disposition",  strfilename);
			Response.Charset = "";
			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			Response.ContentType = "application/vnd.xls";
			
			System.IO.StringWriter stringWrite = new System.IO.StringWriter();
			System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
			
			dgPlayerList.RenderControl(htmlWrite);
			Response.Write(stringWrite.ToString());
			Response.End();
			}
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_OwnerView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        
        }

        private  void GenerateParticipatedEvents ()
        {		
        	//string strSportCode = (String)Session["SPORTCODE"];
        	string strTournamentCode = (String)Session["TOURNAMENTCODE"];
        	try{
	        	List<String> lstObj = (new TPDAL_Events()).GetParticipatedEvents(strSportCode, strTournamentCode);
	        	
	        	//var eventList = lstObj.Select(x => x.EventCode).Distinct();
	        	
	        	if (lstObj != null && lstObj.Count > 0)
	        	{
	        		Int32 i = 0;; //creattre a integer variable
	        		string strEventCode = "";
	        		LinkButton lb = new LinkButton();
					lb.Text = "[ All ] - ";        		
	        		lb.ID = i.ToString(); // LinkButton ID’s
		            lb.Attributes.Add("runat","server");
	                //lb.Click += new EventHandler(lb_Click);
	                lb.Command += new CommandEventHandler(lb_Command);//Create Handler for it.
	                lb.CommandName = "All"; // i.ToString(); //LinkButton CommanName
	                PlaceHolder1.Controls.Add(lb); // Adding the LinkButton in PlaceHolder
		                
	                i = 1;
	                
	        		foreach(var item in lstObj)
		            {
	        			strEventCode = item.ToString();
		             	//create instance of LinkButton                
		                lb = new LinkButton();
		                
		                if (i == (lstObj.Count() - 1)) // Last item
		                	lb.Text = "[ " + strEventCode + " ]"; //LinkButton Text
		                else
		                	lb.Text = "[ " + strEventCode + " ]" + "   -   "; //LinkButton Text
		                
		                lb.ID = i.ToString(); // LinkButton ID’s
		                lb.Attributes.Add("runat","server");
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

        public void lb_Command(object sender, CommandEventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            String strSelectedLinkButton = e.CommandName;
            
            Label1.Text = strSelectedLinkButton; // will display the which Linkbutton clicked
 
            {
                lnk.Font.Bold = true;
                lnk.ForeColor = System.Drawing.Color.Green;
            }
            
            ShowPlayerList (strSelectedLinkButton);
        }
                
        private void ShowPlayerList (string strEventCode)
		{
        	try
        	{
        		string strTournamentCode = (String)Session["TOURNAMENTCODE"];
        		
				//Show Currently running tournaments
        		List<TPPlayer> lstPlayerList = null;
				lstPlayerList = (new TPDAL_TournamentController()).GetPlayerListByEvent(strSportCode, strTournamentCode, strEventCode);
				
				dgPlayerList.DataSource = lstPlayerList;
				dgPlayerList.DataBind();
				
				int iPlayerCount = lstPlayerList.Count;
				Label1.Text = strEventCode + " ( " + iPlayerCount.ToString() + " )";
        	}
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_OwnerView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
        		Response.Redirect("./TP_Down.aspx");
        	}
		}
        
	}
}
