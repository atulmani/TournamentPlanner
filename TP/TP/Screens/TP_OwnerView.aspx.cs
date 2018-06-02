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
	public partial class TP_OwnerView : System.Web.UI.Page
	{	
		string strSportCode = "BD";
    	string strTournamentCode = "";
    	
    	protected void Page_Init(object sender, System.EventArgs e) 
		{
				GenerateParticipatedEvents ();
		}
    	
        protected void Page_Load(object sender, EventArgs e)
        {	
			pnlErrorMsg.Visible = false;

			try
			{
	        	//if (Session["USERNAME"].Equals(null))
	        	//{        	
		        //	pnlOwnerLogin.Visible = true;
	        	//	pnlAfterLogin.Visible = false;
	        	//}
        	    
	        	if(IsPostBack)
				{
								
				}
				else
				{
					List<TPEvent> lstObj= (new TPDAL_Tournament()).GetEvents ("BD");
				
					cblEvents.DataSource = lstObj;
					cblEvents.DataTextField = "EventName";
	        		cblEvents.DataValueField = "EventCode";
					cblEvents.DataBind();
					
				}
				string strTournamentCode = "";
				if(Session["TOURNAMENTCODE"] != null)
				{
					strTournamentCode = Session["TOURNAMENTCODE"].ToString();
//					PopulateOwnerDashboard(strTournamentCode);
				}
			}
			catch (Exception ex)
        	{
				pnlOwnerLogin.Visible = true;
	        	pnlAfterLogin.Visible = false;
	        	
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_OwnerView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
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
                	
                	string strCBLEvents = lstObj[0].TournamentEvents;
                	
                	//string hobby = GetHobbyFromDB();
					string[] lstEvents = strCBLEvents.Split(new []{"; "}, StringSplitOptions.None);
					
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
                	
				}
				
				//Populate Tournament Status				
				string strStatus = (new TPDAL_Tournament()).GetTournamentStatus(strSportCode, strTournamentCode);
				ddlTournamentStatus.SelectedItem.Text = strStatus;
				
				//Get all the participated events	        	
	        	List<String> lstParticipatedEvents = new TPDAL_Events().GetParticipatedEvents(strSportCode, strTournamentCode);
					
	        	if (lstParticipatedEvents.Count > 0)
	        	{
					ddlTournamentEvents.DataSource = lstParticipatedEvents;
				
					ddlTournamentEvents.DataBind();	
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
        
        #region Login
        
        
        
        private bool PopulateOwnerDashboard
        	(String strTournamentCode)
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
        		lblTotalFeesReceivable.Text = "Rs. " + objDashboard.iTotalFees.ToString() ;
        		lblTotalPaymentReceived.Text = "Rs. " + objDashboard.iTotalAmountReceived.ToString();
        		lblSinglesAmount.Text = "Rs. " + objDashboard.iTotalSingleAmount.ToString();
        		lblDoublesAmount.Text = "Rs. " + objDashboard.iTotalDoubleAmount.ToString();
        		lblTotalAmountReceived.Text = "Rs. " + objDashboard.iTotalAmountReceived.ToString();        		
        		lblTotalAmountPending.Text = "Rs. " + objDashboard.iTotalPendingAmount.ToString();
        		lblTotalRegAmountReceivable.Text = "Rs. " + objDashboard.iTotalRegistrationAmount.ToString();
        		lblTotalRegAmountReceived.Text = "Rs. " + objDashboard.iTotalRegistrationAmountReceived.ToString();
        
        	}
        	catch (Exception ex)
        	{
				pnlOwnerLogin.Visible = true;
	        	pnlAfterLogin.Visible = false;
	        	
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_OwnerView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        	return bIsUserExists;
        }
        
        private List<TPLogin> CheckUserAuthentication (TPLogin obj)
        {
        	List<TPLogin> objList = null;
        	        	
        	try
            {
        		objList = (new TPDAL_UserAuthenticationAuthorization()).CheckUserAuthentication (obj);
        	}
        	catch (Exception ex)
        	{
				pnlOwnerLogin.Visible = true;
	        	pnlAfterLogin.Visible = false;
	        	
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_OwnerView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        	return objList;
        }
        
        protected void btnOwnerLogin_Click(object sender, EventArgs e)
        {
        	try
        	{	
        		Session["TOURNAMENTCODE"] = strTournamentCode = txtTournamentCode.Text.Trim();
        		
        		Session["USERNAME"] = txtUserCode.Text.Trim();
        		
        		string strUserCode = txtUserCode.Text.Trim();
        		
        		string strOTP = txtOwnerPassword.Text.Trim();
	        	
	        	//Check Username & Password
	        	bool bIsUserExists = true; //CheckUserAuthentication (strTournamentCode, strUserCode, strOTP);
	        	
	        	if (bIsUserExists == true)
	        	{	        	
		        	pnlOwnerLogin.Visible = false;
		        	pnlAfterLogin.Visible = true;
		        	
		        	PopulateTournamentDetails (strSportCode, strTournamentCode);
		        	PopulateOwnerDashboard(strTournamentCode);
		        	//ShowPlayerList(strSportCode, strTournamentCode);
	        	}
	        	else
	        	{
	        		pnlLoginErrorMsg.Visible = true;
	        		lblLoginErrorMsg.Text = "User Credentials are not valid. Please Enter valid Tournament Code & Password";
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
        	pnlSetupTournament.Visible = false;
        	pnlPlayerList.Visible = false;
        	pnlGenerateDraws.Visible = false;
        	pnlQuickAccess.Visible = false;
        	pnlPayment.Visible = false;
        	pnlOfflineRegistration.Visible = false;
        	
        	btnToggleQuickAccess.Visible = true;
        }
                
        protected void btnSetupTournamentView_Click(object sender, EventArgs e)
        {
        	//Hide All other Panels
        	HideAllOtherPanels();
        	pnlSetupTournament.Visible = true;
        }
        
        protected void btnPlayerListView_Click(object sender, EventArgs e)
        {
        	//Hide All other Panels
        	HideAllOtherPanels();
        	pnlPlayerList.Visible = true;
        }
        
        protected void btnSetupDrawsView_Click(object sender, EventArgs e)
        {
        	//Hide All other Panels
        	HideAllOtherPanels();
        	pnlGenerateDraws.Visible = true;
        }
        
        protected void btnSetupMatchScheduleView_Click(object sender, EventArgs e)
        {
        	//Hide All other Panels
        	HideAllOtherPanels();
        	//pnlSetupTournament.Visible = true;
        }
        
        protected void btnPaymentView_Click(object sender, EventArgs e)
        {
        	//Hide All other Panels
        	HideAllOtherPanels();
        	pnlPayment.Visible = true;
        	//GetPaymentSummary();
        }
        
        protected void btnOfflineRegistration_Click(object sender, EventArgs e)
        {
        	//Hide All other Panels
        	HideAllOtherPanels();
        	pnlOfflineRegistration.Visible = true;
        	//GetPaymentSummary();
        }
        
        protected void GetPaymentSummary()
        {
        	try{
	        	strTournamentCode = (string)Session["TOURNAMENTCODE"]; 
	        	List<TPPaymentSummary> objlist =  (new TPDAL_Registration()).GetPaymentSummary(strTournamentCode);
	        	for(int index = 0; index < objlist.Count ; index++)
	        	{
	        		if (objlist[index].PaymentType.ToUpper() == "COMPLETED")
	        			lblTotalConfirmedAmount.Text = objlist[index].PaymentAmount;
	        		else if (objlist[index].PaymentType.ToUpper() == "PENDING")
	        			lblTotalPendingAmount.Text = objlist[index].PaymentAmount;
	        		else if (objlist[index].PaymentType.ToUpper() == "STATUS UPDATE - IN PROGRESS")
	        			lblTotalInProgressAmount.Text = objlist[index].PaymentAmount;
	        	}
        	}
        	catch (Exception ex)
        	{
				pnlOwnerLogin.Visible = true;
	        	pnlAfterLogin.Visible = false;
	        	
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_OwnerView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        }
        
        protected void btnToggleSetupTournament_Click(object sender, EventArgs e)
		{
			if (btnToggleSetupTournament.Text.Equals("+ Setup Tournament"))
			{
				btnToggleSetupTournament.Text = "- Setup Tournament";
				pnlSetupTournament.Visible = true;
			}
			else
			{
				btnToggleSetupTournament.Text = "+ Setup Tournament";
				pnlSetupTournament.Visible = false;
			}
		}
        
        protected void btnToggleGenerateDraws_Click(object sender, EventArgs e)
		{
			if (btnToggleGenerateDraws.Text.Equals("+ Generate Draws"))
			{
				btnToggleGenerateDraws.Text = "- Generate Draws";
				pnlGenerateDraws.Visible = true;
			}
			else
			{
				btnToggleGenerateDraws.Text = "+ Generate Draws";
				pnlGenerateDraws.Visible = false;
			}
		}
        
        #endregion
        
        #region Setup Tournament
        
        protected void btnSetupTournament_Click(object sender, EventArgs e)
		{
			try
            {
				if (Session["TOURNAMENTCODE"].Equals(null))
	        	{
		        	pnlOwnerLogin.Visible = true;
	        		pnlAfterLogin.Visible = false;
	        	}
				else
				{
					
					strTournamentCode = (string)Session["TOURNAMENTCODE"]; //txtOwnerUserName.Text;
		        	
					
					
	                String strTournamentName = txtTournamentName.Text;
	
	                if (!(string.IsNullOrEmpty(strTournamentName) || 
	                      (string.IsNullOrEmpty(strTournamentCode))))
	                {
	                	
	                	
	                	DateTime dt;
						bool IsStartDtFormatCorrect = DateTime.TryParseExact(txtTournamentStartDate.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out dt);
						bool IsEndDtFormatCorrect = DateTime.TryParseExact(txtTournamentEndDate.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out dt);
	                	
						if (!IsStartDtFormatCorrect || !IsEndDtFormatCorrect)
						{
							//Show error message
							pnlErrorMsg.Visible = true;
	                		lblErrorMsg.Text = "Date format is incorrect. Please enter value in dd/mm/yyyy format"; 
						}
						else
						{
							string strdateformat = "dd/MM/yyyy";
							
		                	TPTournament objTournament = new TPTournament();
		             
		                	objTournament.SportCode = strSportCode;
		                	objTournament.TournamentCode = strTournamentCode;	                	
		                	objTournament.TournamentName = strTournamentName;                	
		                	objTournament.TournamentOrganisation = txtTournamentOrganisation.Text;
		                	objTournament.TournamentVenue = txtTournamentVenue.Text;
		                	
		                	objTournament.TournamentStartDate = DateTime.ParseExact(txtTournamentStartDate.Text, strdateformat , CultureInfo.InvariantCulture);
		                	objTournament.TournamentEndDate = DateTime.ParseExact(txtTournamentEndDate.Text, strdateformat , CultureInfo.InvariantCulture);
		                	objTournament.TournamentPOCContactNames = txtTournamentPOCNames.Text;
		                	objTournament.TournamentPOCContactNo = txtTournamentPOCContacts.Text;
		                	
		                	string strCBLEvents = "";
		                	foreach (ListItem li in cblEvents.Items)
					        {
					            if (li.Selected)
					            {
					                strCBLEvents += li.Text + "; ";
					            }
					        }
		                	
		                	objTournament.TournamentEvents = strCBLEvents;
		                	
		                	objTournament.OranisationLogo = txtOranisationLogo.Text;                	
		                	objTournament.TournamentLocationAddress = txtTournamentLocationAddress.Text;
		                	objTournament.TournamentLocationContactNo = txtTournamentLocationContactNos.Text;                	
		                	objTournament.TournamentEntryOpenDate = DateTime.ParseExact(txtTournamentEntryOpenDate.Text, strdateformat , CultureInfo.InvariantCulture);
		                	objTournament.TournamentEntryCloseDate = DateTime.ParseExact(txtTournamentEntryEndDate.Text, strdateformat , CultureInfo.InvariantCulture);
		                	objTournament.TournamentEntryWithdrawlDate = DateTime.ParseExact(txtTournamentWithdrawaldate.Text, strdateformat , CultureInfo.InvariantCulture);
		                	
		                	objTournament.TournamentDuration = txtTournamentDuration.Text;
		                	objTournament.TournamentSponsers = txtTournamentSponsers.Text;
		                	
		                	objTournament.TournamentStatus = ddlTournamentStatus.SelectedValue;
		                	
							TPDAL_TournamentController objDALTPController = new TPDAL_TournamentController();                        
							objDALTPController.SetupTournament (objTournament);
						}
	                }
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
        
        #region Generate Draws
        
        protected void ddlSports_SelectedIndexChanged(object sender, EventArgs e)
        {
        	try 
        	{
	        	String strSelectedSport = ddlSports.SelectedValue;
	        	
	        	List<string> lstSports = (new TPDAL_TournamentController()).GetINACTIVETournaments(strSelectedSport);
	        	
	    		ddlTournaments.DataSource = lstSports;
	    		ddlTournaments.DataBind ();        	
        	}
            catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_OwnerView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
            }
        }
        
        protected void btnGenerateDraws_Click (object sender, EventArgs e)
        {
        	try
        	{
        		strTournamentCode = lblTournamentID.Text;
	        	//If status code is OPEN then only GenerateDraws will be possible else not
	        	string strStatus = (new TPDAL_Tournament()).GetTournamentStatus(strSportCode, strTournamentCode);
									
				//If TournamentStatus = "OPEN" then GenerateDraws will be enabled else disabled
				if (strStatus.Equals("OPEN"))
				{
	        		//if (!(ddlTournamentEventType.SelectedValue.Equals("0") ))
		        	{	
			        	String strEventCode = ddlTournamentEvents.SelectedItem.Text; //"BS U11";
			        	String strEventType = strEventCode.Substring(1,1); //"S" for Singles & "D" for Doubles
			        	
			        	int i = (new TPDAL_TournamentController()).GenerateDraws(strSportCode, strTournamentCode, strEventCode, strEventType);
			        	
			        	pnlErrorMsg.Visible = true;
						lblErrorMsg.Text = "Draws Generated Successfully!!!";
		        	}
		        	//else
		        	{
		        		//strMsg = "All options are mandatory. Please select options.";
		        	//	pnlErrorMsg.Visible = true;
					//	lblErrorMsg.Text = "Please Select Tournament Event Type (Singles/Doubles) to Generate Draws.";
		        	}
				}
				else
				{
					pnlErrorMsg.Visible = true;
					lblErrorMsg.Text = "Check tournament status; Tournament Status should be OPEN to Generate Draws.";
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
        
        #region Show Player List
        
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
				pnlOwnerLogin.Visible = true;
	        	pnlAfterLogin.Visible = false;
	        	
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
        
        #endregion
        
        #region Export
        
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
        
        public void ExportAllPlayerList_Click(object sender, System.EventArgs e)
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
        
        #endregion
        
        #region Payment View
        
        protected void rbPaymentReportOptions_CheckedChanged(object sender, EventArgs e)
        {
        	
        		string strPaymentStatus = rbPaymentReportOptions.SelectedItem.Text;
        		
				
				if (strPaymentStatus == "Pending")
				{
					btnSelectAndTakeAction.Text = "Send Payment Reminder";
					btnSelectChangeStatus.Visible = false;
					
				}else if (strPaymentStatus == "Status Update In Progress")
				{
					btnSelectAndTakeAction.Text = "Payment Confirmed";
					btnSelectChangeStatus.Text = "Change Status to Pending";
				}
				else 
				{
					btnSelectChangeStatus.Visible = false;
					btnSelectAndTakeAction.Visible = false;
					btnUpdateOfflineStatus.Visible = false;
				}
			
        }        
        
        protected void btnSelectAndTakeAction_Click(object sender, EventArgs e)
        {
        	try{
        
        	
        		string strTournamentCode = (String)Session["TOURNAMENTCODE"];
        		string strPaymentStatus = "";
        		strPaymentStatus = rbPaymentReportOptions.SelectedItem.Text;
        
        		string strAction1 =  btnSelectAndTakeAction.Text ;
        		List<string> lstIDs = new List<string>();
        		string strID = "";
        		string strEmail, strPlayerCode, strPlayerName, strMobile, strDOB;
        		strEmail =  strPlayerCode =  strPlayerName =  strMobile =  strDOB = "";
        		for (int index = 0; index < dgPayment.Items.Count ; index++)
        		{
        			if(((CheckBox)dgPayment.Items[index].FindControl("chkSelection")).Checked )
        			{
        				
        				strID =  ((HiddenField)dgPayment.Items[index].FindControl("hfPaymentID")).Value;
        				lstIDs.Add(strID);
        				if(strPaymentStatus == "Pending" && strAction1 == "Send Payment Reminder")
        				{
        					//send Reminder mail
        					strEmail = dgPayment.Items[index].Cells[3].Text;
        					strPlayerCode = dgPayment.Items[index].Cells[0].Text;
        					strPlayerName = dgPayment.Items[index].Cells[1].Text;
        					strMobile = dgPayment.Items[index].Cells[2].Text;
        					strDOB = dgPayment.Items[index].Cells[4].Text;
        					
        					SendReminderEmail(strEmail, strPlayerCode, strPlayerName, strMobile, strDOB, "Pending");
        				}
        				else if(strPaymentStatus == "Status Update - In Progress" && strAction1 == "Payment Confirmed")
        				{
        					//select and confirm the payment
        					(new TPDAL_Registration()).UpdatePaymentStatusFromOwnerView(strTournamentCode, strID, "Completed");
        					//send confirmation mail
        					strEmail = dgPayment.Items[index].Cells[3].Text;
        					strPlayerCode = dgPayment.Items[index].Cells[0].Text;
        					strPlayerName = dgPayment.Items[index].Cells[1].Text;
        					strMobile = dgPayment.Items[index].Cells[2].Text;
        					strDOB = dgPayment.Items[index].Cells[4].Text;
        					
        					SendConfirmationEmail(strEmail, strPlayerCode, strPlayerName, strMobile, strDOB, "Confirmed");
        				}
        			}
        			
        		}
        		btnGetPaymentList_Click(null, null);
        /*		//Show Currently running tournaments
        		List<TPPlayer> lstPlayerList = null;
				lstPlayerList = (new TPDAL_TournamentController()).GetEventListByPaymentStatus(strSportCode, strTournamentCode, strPaymentStatus);
				
				dgPayment.DataSource = lstPlayerList;
				dgPayment.DataBind();
		*/		
				//int iPlayerCount = lstPlayerList.Count;
				//Label1.Text = strEventCode + " ( " + iPlayerCount.ToString() + " )";
        	}
        	
        	
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_OwnerView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
                
        }
                
        protected void btnRefreshDashboard_Click(object sender, EventArgs e)
        {
        	try{
        			if(Session["TOURNAMENTCODE"] != null)
				{
					strTournamentCode = Session["TOURNAMENTCODE"].ToString();
					PopulateOwnerDashboard(strTournamentCode);
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
        
                
        protected void btnUpdateOfflineStatus_Click(object sender, EventArgs e)
        {
        	try{
        
        	
        		string strTournamentCode = (String)Session["TOURNAMENTCODE"];
        		//string strPaymentStatus = "";
        	
        		List<string> lstIDs = new List<string>();
        		string strID = "";
        		string strEmail, strPlayerCode, strPlayerName, strMobile, strDOB;
        		strEmail =  strPlayerCode =  strPlayerName =  strMobile =  strDOB = "";
        		
        		for (int index = 0; index < dgPayment.Items.Count ; index++)
        		{
        			if(((CheckBox)dgPayment.Items[index].FindControl("chkSelection")).Checked )
        			{
        				
        				strID =  ((HiddenField)dgPayment.Items[index].FindControl("hfPaymentID")).Value;
        				lstIDs.Add(strID);
    					
    					(new TPDAL_Registration()).UpdateOfflinePaymentStatusFromOwnerView(strTournamentCode, strID);
    					//send mail for Confirmation
    					strEmail = dgPayment.Items[index].Cells[3].Text;
    					strPlayerCode = dgPayment.Items[index].Cells[0].Text;
    					strPlayerName = dgPayment.Items[index].Cells[1].Text;
    					strMobile = dgPayment.Items[index].Cells[2].Text;
    					strDOB = dgPayment.Items[index].Cells[4].Text;
    					
        				SendConfirmationEmail(strEmail, strPlayerCode, strPlayerName, strMobile, strDOB, "Completed");
        				
        			}
        			
        			
        		}
        		btnGetPaymentList_Click(null,null);
        		
        /*		//Show Currently running tournaments
        		List<TPPlayer> lstPlayerList = null;
				lstPlayerList = (new TPDAL_TournamentController()).GetEventListByPaymentStatus(strSportCode, strTournamentCode, strPaymentStatus);
				
				dgPayment.DataSource = lstPlayerList;
				dgPayment.DataBind();
		*/		
				//int iPlayerCount = lstPlayerList.Count;
				//Label1.Text = strEventCode + " ( " + iPlayerCount.ToString() + " )";
        	}
        	
        	
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_OwnerView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
                
        }
        
        protected void btnDrawsANDSchedulePublish_Click(object sender, EventArgs e)
        {
        	try{
        
        	
        		string strTournamentCode = (String)Session["TOURNAMENTCODE"];
        		//string strPaymentStatus = "";
        	
        		List<string> lstIDs = new List<string>();
        		string strID = "";
        		string strEmail, strPlayerCode, strPlayerName, strMobile, strDOB;
        		strEmail =  strPlayerCode =  strPlayerName =  strMobile =  strDOB = "";
        		
        		for (int index = 0; index < dgPayment.Items.Count ; index++)
        		{
        			if(((CheckBox)dgPayment.Items[index].FindControl("chkSelection")).Checked )
        			{
        				
        				strID =  ((HiddenField)dgPayment.Items[index].FindControl("hfPaymentID")).Value;
        				lstIDs.Add(strID);
    					
    					(new TPDAL_Registration()).UpdateOfflinePaymentStatusFromOwnerView(strTournamentCode, strID);
    					//send mail for Confirmation
    					strEmail = dgPayment.Items[index].Cells[3].Text;
    					strPlayerCode = dgPayment.Items[index].Cells[0].Text;
    					strPlayerName = dgPayment.Items[index].Cells[1].Text;
    					strMobile = dgPayment.Items[index].Cells[2].Text;
    					strDOB = dgPayment.Items[index].Cells[4].Text;
    					
        				Notification_DrawsScheduleEmail(strEmail);
        				
        			}
        			
        			
        		}
        		//btnGetPaymentList_Click(null,null);
        		
        /*		//Show Currently running tournaments
        		List<TPPlayer> lstPlayerList = null;
				lstPlayerList = (new TPDAL_TournamentController()).GetEventListByPaymentStatus(strSportCode, strTournamentCode, strPaymentStatus);
				
				dgPayment.DataSource = lstPlayerList;
				dgPayment.DataBind();
		*/		
				//int iPlayerCount = lstPlayerList.Count;
				//Label1.Text = strEventCode + " ( " + iPlayerCount.ToString() + " )";
        	}
        	
        	
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_OwnerView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
                
        }
        
        protected void btnSelectChangeStatus_Click(object sender, EventArgs e)
        {
        	try{
        
        	
        		string strTournamentCode = (String)Session["TOURNAMENTCODE"];
        		string strPaymentStatus = "";
        		strPaymentStatus = rbPaymentReportOptions.SelectedItem.Text;
        
        		string strAction1 =  btnSelectChangeStatus.Text ;
        		List<string> lstIDs = new List<string>();
        		string strID = "";
        		string strEmail, strPlayerCode, strPlayerName, strMobile, strDOB;
        		strEmail =  strPlayerCode =  strPlayerName =  strMobile =  strDOB = "";
        		
        		for (int index = 0; index < dgPayment.Items.Count ; index++)
        		{
        			if(((CheckBox)dgPayment.Items[index].FindControl("chkSelection")).Checked )
        			{
        				
        				strID =  ((HiddenField)dgPayment.Items[index].FindControl("hfPaymentID")).Value;
        				lstIDs.Add(strID);
        				if(strPaymentStatus == "Pending" && strAction1 == "Payment Confirmed")
        				{
        					//Select and Change status to Confirm
        					
        					(new TPDAL_Registration()).UpdatePaymentStatusFromOwnerView(strTournamentCode, strID, "Completed");
        					//send mail for Confirmation
        					strEmail = dgPayment.Items[index].Cells[3].Text;
        					strPlayerCode = dgPayment.Items[index].Cells[0].Text;
        					strPlayerName = dgPayment.Items[index].Cells[1].Text;
        					strMobile = dgPayment.Items[index].Cells[2].Text;
        					strDOB = dgPayment.Items[index].Cells[4].Text;
        					
        					SendConfirmationEmail(strEmail, strPlayerCode, strPlayerName, strMobile, strDOB, "Completed");
        					
        				}
        				else if(strPaymentStatus == "Status Update - In Progress" && strAction1 == "Change status to Pending")
        				{
        					//Select and Change to Pending
        					(new TPDAL_Registration()).UpdatePaymentStatusFromOwnerView(strTournamentCode, strID, "Pending");
        					strEmail = dgPayment.Items[index].Cells[3].Text;
        					strPlayerCode = dgPayment.Items[index].Cells[0].Text;
        					strPlayerName = dgPayment.Items[index].Cells[1].Text;
        					strMobile = dgPayment.Items[index].Cells[2].Text;
        					strDOB = dgPayment.Items[index].Cells[4].Text;
        					
        					SendFailedPayment(strEmail, strPlayerCode, strPlayerName, strMobile, strDOB, "Pending");
        				
        				}
        			}
        			
        		}
        		btnGetPaymentList_Click(null,null);
        		
        /*		//Show Currently running tournaments
        		List<TPPlayer> lstPlayerList = null;
				lstPlayerList = (new TPDAL_TournamentController()).GetEventListByPaymentStatus(strSportCode, strTournamentCode, strPaymentStatus);
				
				dgPayment.DataSource = lstPlayerList;
				dgPayment.DataBind();
		*/		
				//int iPlayerCount = lstPlayerList.Count;
				//Label1.Text = strEventCode + " ( " + iPlayerCount.ToString() + " )";
        	}
        	
        	
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_OwnerView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
                
        }
        
        protected void PopulateTotalAmount (List<TPPlayer> objlist)
        {
        	try{
        		int iTotalAmount = 0;
        		for (int index=0 ; index < objlist.Count ; index++)
        		{
        			iTotalAmount += int.Parse( objlist[index].Amount);
        		}
        		lblTotalAmount.Text = iTotalAmount.ToString();
        	}
        	       	
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_OwnerView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        
        }
       	
        protected void btnGetPaymentList_Click(object sender, EventArgs e)
        {
        	try{
        
        	
        		string strTournamentCode = (String)Session["TOURNAMENTCODE"];
        		string strPaymentStatus = "";
        		strPaymentStatus = rbPaymentReportOptions.SelectedItem.Text;
        		
				//Show Currently running tournaments
        		List<TPPlayer> lstPlayerList = null;
				lstPlayerList = (new TPDAL_TournamentController()).GetEventListByPaymentStatus(strSportCode, strTournamentCode, strPaymentStatus);
				
				dgPayment.DataSource = lstPlayerList;
				dgPayment.DataBind();
				lblTotalRow.Text = lstPlayerList.Count.ToString();
				PopulateTotalAmount(lstPlayerList);
				if (strPaymentStatus == "Pending")
				{
					btnSelectAndTakeAction.Text = "Send Payment Reminder";
					btnSelectAndTakeAction.Visible = true;
					btnSelectChangeStatus.Text = "Payment Confirmed";
					btnSelectChangeStatus.Visible = true;
					btnUpdateOfflineStatus.Visible = true;
				}else if (strPaymentStatus == "Status Update - In Progress")
				{
					btnSelectAndTakeAction.Text = "Payment Confirmed";
					btnSelectChangeStatus.Text = "Select and Change to Pending";
					btnSelectAndTakeAction.Visible = true;
					btnSelectChangeStatus.Visible = true;
					btnUpdateOfflineStatus.Visible = true;
				}
				else 
				{
					btnSelectChangeStatus.Visible = false;
					btnSelectAndTakeAction.Visible = false;
					btnUpdateOfflineStatus.Visible =false;
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
        
        protected void chkCheckAll_CheckedChanged(object sender, EventArgs e)
        {
        	try{
        		bool bcheck = false;
        		//if (((CheckBox)sender).Checked == true)
        		{
	        		bcheck = ((CheckBox)sender).Checked ;
        		}
    			for (int index = 0 ; index < dgPayment.Items.Count ; index++)
        		{
    				((CheckBox)dgPayment.Items[index].FindControl("chkSelection")).Checked = bcheck;
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
        
        public void ExportPaymentList_Click(object sender, System.EventArgs e)
		{
        	try{
		  string strTitle = "PaymentStatus";
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
			
			dgPayment.RenderControl(htmlWrite);
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
        
        #endregion
        
        #region Email Templates
        
        //btnDrawsANDSchedulePublish_Click
        

		private void Notification_DrawsScheduleEmail (string strEmail)
        {
        	try
        	{
	        	StringBuilder sb = new StringBuilder();
	        	
        		string strToEmailID = strEmail;
	        	
	        	string strMyEmailSubject = "Corporate Shuttlers - Draws & Schedule Published";
	        	string strEmailSubject = new MGCommon.EMailTemplate().MailSubjectLine(strMyEmailSubject);
	        		
		        
	        	string strHeader = new MGCommon.EMailTemplate().EMailHeader();
	        	string strBody = new MGCommon.EMailTemplate().Notification_DrawsANDScheduleMailBody();
	        	string strFooter = new MGCommon.EMailTemplate().EMailFooter();
	        	
	        	string strEmailBody = strBody + strFooter;
	        	
	        	MGCommon.MGGeneric.MGEMailServices(strToEmailID, strEmailSubject, strEmailBody);
        	}
            catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationForm.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
                Response.Redirect("./TP_BD_Home.aspx", false);
            }
        }
        
		private void SendReminderEmail (string strEmail , string strPlayerCode, string strPlayerName, string strMobileNo, string strPlayerDOB, string strPaymentStatus)
        {
        	try
        	{
	        	StringBuilder sb = new StringBuilder();
	        	
        		string strToEmailID = strEmail;
	        	
	        	string strMyEmailSubject = "Corporate Shuttlers - Payment Reminder";
	        	string strEmailSubject = new MGCommon.EMailTemplate().MailSubjectLine(strMyEmailSubject);
	        		
		        
	        	string strHeader = new MGCommon.EMailTemplate().EMailHeader();
	        	string strBody = new MGCommon.EMailTemplate().PaymentReminderMailBody(strPlayerName, strMobileNo, strPlayerCode, strPlayerDOB, strPaymentStatus);
	        	string strFooter = new MGCommon.EMailTemplate().EMailFooter();
	        	
	        	string strEmailBody = strBody + strFooter;
	        	
	        	MGCommon.MGGeneric.MGEMailServices(strToEmailID, strEmailSubject, strEmailBody);
        	}
            catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationForm.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
                Response.Redirect("./TP_BD_Home.aspx", false);
            }
        }
     
		private void SendConfirmationEmail (string strEmail , string strPlayerCode, string strPlayerName, string strMobileNo, string strPlayerDOB, string strPaymentStatus)
        {
        	try
        	{
	        	StringBuilder sb = new StringBuilder();
	        	
        		string strToEmailID = strEmail;
	        	
	        	string strMyEmailSubject = "Corporate Shuttlers - Payment confirmed";
	        	string strEmailSubject = new MGCommon.EMailTemplate().MailSubjectLine(strMyEmailSubject);
	        		
		        
	        	string strHeader = new MGCommon.EMailTemplate().EMailHeader();
	        	string strBody = new MGCommon.EMailTemplate().ConfirmPaymentMailBody(strPlayerName, strMobileNo, strPlayerCode, strPlayerDOB, strPaymentStatus);
	        	string strFooter = new MGCommon.EMailTemplate().EMailFooter();
	        	
	        	string strEmailBody = strBody + strFooter;
	        	
	        	MGCommon.MGGeneric.MGEMailServices(strToEmailID, strEmailSubject, strEmailBody);
        	}
            catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationForm.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
                Response.Redirect("./TP_BD_Home.aspx", false);
            }
        }
				
     	private void SendFailedPayment(string strEmail , string strPlayerCode, string strPlayerName, string strMobileNo, string strPlayerDOB, string strPaymentStatus)
        {
        	try
        	{
	        	StringBuilder sb = new StringBuilder();
	        	
        		string strToEmailID = strEmail;
	        	
	        	string strMyEmailSubject = "Corporate Shuttlers - Payment Reminder";
	        	string strEmailSubject = new MGCommon.EMailTemplate().MailSubjectLine(strMyEmailSubject);
	        		
		        
	        	string strHeader = new MGCommon.EMailTemplate().EMailHeader();
	        	string strBody = new MGCommon.EMailTemplate().SendFailedPaymentMailBody(strPlayerName, strMobileNo, strPlayerCode, strPlayerDOB, strPaymentStatus);
	        	string strFooter = new MGCommon.EMailTemplate().EMailFooter();
	        	
	        	string strEmailBody = strBody + strFooter;
	        	
	        	MGCommon.MGGeneric.MGEMailServices(strToEmailID, strEmailSubject, strEmailBody);
        	}
            catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationForm.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
                Response.Redirect("./TP_BD_Home.aspx", false);
            }
        }

     	#endregion Email Template

        #region Offline Registration
        
        protected void lbtnOfflineRegistrationForm_Click(object sender, EventArgs e)
        {
        	//Get Owner's User ID        	
        	string strTournamentCode = (string) Session["TOURNAMENTCODE"];        	
        	string strUserName = (string)Session["USERNAME"];
        	
        	//Encrypt Owner's User ID
        	string strTournamentCodeEncrypt = MGCommon.MGGeneric.EncryptData(strTournamentCode);
        	string strOwnerUserIDEncrypt = MGCommon.MGGeneric.EncryptData(strUserName);
         	        	
        	//Pass  the user id to registration form as url paramenter 
        	string strURL = "TP_RegistrationForm.aspx?TournamentCode="+strTournamentCodeEncrypt + "&" + "OwnerUserID="+ strOwnerUserIDEncrypt;
        	Response.Redirect(strURL, false);
        	                                  
        }
        
        #endregion     	
	}
}
