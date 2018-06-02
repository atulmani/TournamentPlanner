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
	public partial class TP_TournamentSetup : System.Web.UI.Page
	{	
		string strSportCode = "BD";
    	string strTournamentCode = "";
    	TPTournament objTornamentDetails ; 
    	
    	protected void Page_Init(object sender, System.EventArgs e) 
		{
				
		}
    	
        protected void Page_Load(object sender, EventArgs e)
        {	
			pnlErrorMsg.Visible = false;

			try
			{	        	
	        	strTournamentCode = Request.QueryString["TCode"];
				Session["TOURNAMENTCODE"] = strTournamentCode = MGCommon.MGGeneric.DecryptData(strTournamentCode.Trim());

                if (Session["USERID"] != null ||
                    Session["USERTYPE"] != null)
                {
                    List<TPStatus> lstTPStatusObj = (new TPDAL_Tournament()).GetTournamentStatus(strSportCode, strTournamentCode);

                    if (lstTPStatusObj != null && lstTPStatusObj.Count > 0)
                    {
                        string strTournamentStatus = lstTPStatusObj[0].TournamentStatus;

                        if (strTournamentStatus.Equals("INACTIVE"))
                        {
                            OFFStatusButtons();
                            btnStatusINACTIVE.Attributes["class"] = "button_Atul";
                            lblStatusTooltip.Text = "Tournament is not OPEN for All now, only OWNER can setup the tournament details";
                        }
                        if (strTournamentStatus.Equals("UPCOMING"))
                        {
                            OFFStatusButtons();
                            btnStatusUPCOMING.Attributes["class"] = "button_Atul";
                            lblStatusTooltip.Text = "Tournament will show on homepage but registration will not be open";
                        }
                        else if (strTournamentStatus.Equals("OPEN"))
                        {
                            OFFStatusButtons();
                            btnStatusOPEN.Attributes["class"] = "button_Atul";
                            lblStatusTooltip.Text = "Tournament is OPEN for registration";
                        }
                        else if (strTournamentStatus.Equals("RUNNING"))
                        {
                            OFFStatusButtons();
                            btnStatusRUNNING.Attributes["class"] = "button_Atul";
                            lblStatusTooltip.Text = "Registration closed and Tournament is running now";

                            //visible only for superadmin else not visible	        		
                            string strUserID = Session["USERNAME"].ToString();

                            if (strUserID.ToUpper().Equals("ADMIN"))
                            {
                                btnStatusCLOSED.Visible = true;
                            }
                            else
                            {
                                btnStatusCLOSED.Visible = false;
                            }
                        }
                        else if (strTournamentStatus.Equals("CLOSED"))
                        {
                            OFFStatusButtons();

                            btnStatusCLOSED.Attributes["class"] = "button_Atul";
                            lblStatusTooltip.Text = "Tournament is CLOSED";

                            //Save and Update buttons will be disabled now
                            btnStatusINACTIVE.Visible = false;
                            btnStatusUPCOMING.Visible = false;
                            btnStatusOPEN.Visible = false;
                            btnStatusRUNNING.Visible = false;
                            btnStatusCLOSED.Visible = false;

                            btnTournamentDetailSave.Visible = false;
                            btnUpdateEvent.Visible = false;
                            btnSetupTournament.Visible = false;
                        }

                        if (IsPostBack)
                        {

                        }
                        else
                        {
                            //GenerateParticipatedEvents ();	
                            List<TPEvent> lstObj = (new TPDAL_Tournament()).GetEvents("BD");

                            cblEvents.DataSource = lstObj;
                            cblEvents.DataTextField = "EventName";
                            cblEvents.DataValueField = "Gender";
                            cblEvents.DataBind();

                            PopulateTournamentDetails("BD", strTournamentCode);

                        }

                        lblTournamentID.Text = strTournamentCode;
                    }
                    else
                    {
                        Response.Redirect("./TP_Login.aspx", false);
                    }
                }
	        	
			}
			catch (Exception ex)
        	{
				
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_TournamentSetup.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
                Response.Redirect("./TP_Login.aspx", false);
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
	        	List <TPTornamentEventDetail> lstEvent = new List<TPTornamentEventDetail>();
	        	//TPTornamentEventDetail objEvent = null;
	        	if (Session["TournamentDetails"] == null)
	        	{
	        		lstObj = (new TPDAL_Tournament()).GetTournamentLists(strSportCode, strTournamentCode, "");
	        		lstEvent = (new TPDAL_Tournament()).GetTournamentEventDetails(strSportCode, strTournamentCode);
	        	}
				else
				{
					lstObj = new List<TPTournament>();
					lstObj.Add ((TPTournament) Session["TournamentDetails"]);
				}
					
				if (lstObj != null && lstObj.Count > 0)
				{
					Session["TournamentDetails"] = lstObj[0];
					//Populate Summary
					lblTournamentID.Text = lstObj[0].TournamentCode;
					//lblTournamentOrganisation.Text =  lstObj[0].TournamentOrganisation;
					//lblTournamentVenue.Text = lstObj[0].TournamentVenue;
					//lblOwnerName.Text = lstObj[0].TournamentOwnerName;
					//lblOwnerID.Text = lstObj[0].TournamentOwnerIDType + ": " + lstObj[0].TournamentOwnerIDNo;
					//lblOwnerAddress.Text = lstObj[0].TournamentOwnerAddress;
					
					//Populate Tournament Setup Section
					txtTournamentName.Text = lstObj[0].TournamentName;
					txtTournamentVenue.Text = lstObj[0].TournamentVenue;
					txtTournamentOrganisation.Text = lstObj[0].TournamentOrganisation;					
					txtTournamentStartDate.Text = lstObj[0].TournamentStartDate.ToString("dd/MM/yyyy");
					txtTournamentEndDate.Text = lstObj[0].TournamentEndDate.ToString("dd/MM/yyyy");
					txtTournamentPOCNames.Text = lstObj[0].TournamentPOCContactNames;
					txtTournamentPOCContacts.Text = lstObj[0].TournamentPOCContactNames;
					lblRegistrationFormType.Text = lstObj[0].TournamentRegistrationFormType;
					
                	//objTournament.TournamentEvents = strCBLEvents; 
                	//Write a code for event population
                	
                	string strCBLEvents = lstObj[0].TournamentEvents;
                	
                	//string hobby = GetHobbyFromDB();
					string[] lstEvents = strCBLEvents.Split(new []{"; "}, StringSplitOptions.None);
					List<TPTornamentEventDetail> objList = (new TPDAL_Tournament()).GetTournamentEventDetails( strSportCode, strTournamentCode);
					foreach (ListItem li in cblEvents.Items)
					{
					    li.Selected = lstEvents.Contains(li.Text);					    
					}
					
					
					Session["EventList"] = objList;
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
			//	string strStatus = (new TPDAL_Tournament()).GetTournamentStatus(strSportCode, strTournamentCode);
				//ddlTournamentStatus.SelectedItem.Text = strStatus;
				
				//Get all the participated events	        	
	        //	List<String> lstParticipatedEvents = new TPDAL_Events().GetParticipatedEvents(strSportCode, strTournamentCode);
					
	        //	if (lstParticipatedEvents.Count > 0)
	        //	{
			//		ddlTournamentEvents.DataSource = lstParticipatedEvents;
			//		ddlTournamentEvents.DataBind();	
	        //	}
	        	
	        	
	        	
        	}
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_OwnerView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        
        }
        
        protected void lbtnReturn2Dashboard_Click(object sender, EventArgs e)
        {        	
        	string strURL = "./TP_Dashboard.aspx?TCode=" + MGCommon.MGGeneric.EncryptData(strTournamentCode);
        	Response.Redirect(strURL, false);
        }
        
        #endregion
        
        
        #region Update Tournament Status
        
        private void OFFStatusButtons()
        {
        	btnStatusINACTIVE.Attributes["class"] = "buttonOFF_Atul";
        	btnStatusUPCOMING.Attributes["class"] = "buttonOFF_Atul";
        	btnStatusOPEN.Attributes["class"] = "buttonOFF_Atul";
        	btnStatusRUNNING.Attributes["class"] = "buttonOFF_Atul";
        	btnStatusCLOSED.Attributes["class"] = "buttonOFF_Atul";
        }
        
        protected void btnStatusINACTIVE_Click(object sender, EventArgs e)
        {
        	OFFStatusButtons();
        	btnStatusINACTIVE.Attributes["class"] = "button_Atul";
        	lblStatusTooltip.Text = "Tournament is not OPEN for All now, only OWNER can setup the tournament details";
        	
        	string strStatus = btnStatusINACTIVE.Text;
        	        	
        	UpdateTournamentStatus (strStatus);
        }
        
        protected void btnStatusUPCOMING_Click(object sender, EventArgs e)
        {
        	OFFStatusButtons();
        	btnStatusUPCOMING.Attributes["class"] = "button_Atul";
        	lblStatusTooltip.Text = "Tournament will show on homepage but registration will not be open";
        	
        	string strStatus = btnStatusUPCOMING.Text;
        	        	
        	UpdateTournamentStatus (strStatus);
        }
        
        protected void btnStatusOPEN_Click(object sender, EventArgs e)
        {        	
        	OFFStatusButtons();
        	btnStatusOPEN.Attributes["class"] = "button_Atul";
        	lblStatusTooltip.Text = "Tournament is OPEN for registration";
        	
        	string strStatus = btnStatusOPEN.Text;
        	
        	UpdateTournamentStatus (strStatus);        	
        }
        
        protected void btnStatusRUNNING_Click(object sender, EventArgs e)
        {
        	OFFStatusButtons();
        	btnStatusRUNNING.Attributes["class"] = "button_Atul";
        	lblStatusTooltip.Text = "Registration closed and Tournament is running now";
        	
        	string strStatus = btnStatusRUNNING.Text;
        	
        	UpdateTournamentStatus (strStatus);        	
        }
        
        protected void btnStatusCLOSED_Click(object sender, EventArgs e)
        {        	
        	OFFStatusButtons();
        	btnStatusCLOSED.Attributes["class"] = "button_Atul";
        	lblStatusTooltip.Text = "Tournament is closed and will be available only in readonly mode";
        	
        	string strStatus = btnStatusCLOSED.Text;
        	
        	UpdateTournamentStatus (strStatus);
        }
        
        private void UpdateTournamentStatus (string strStatus)
        {
        	try
        	{
	       		TPStatus objTPStatus = new TPStatus();

                objTPStatus.SportCode = strSportCode;
                objTPStatus.TournamentCode = strTournamentCode;
                objTPStatus.TournamentStatus = strStatus;
	        	
	        	TPDAL_TournamentController objDALTPController = new TPDAL_TournamentController();
                objDALTPController.UpdateTournamentStatus(objTPStatus);
        	}
        	catch (Exception ex)
			{
				System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_TournamentSetup.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);                
			}
        }
        
        #endregion
        
        
        private void AllPanelCollapse()
        {
        	pnlTournamentDetails.Visible = false;
        	btnToggleTournamentDetailsPlus.Text = "+";
        	
        	pnlTournamentAdditionalDetails.Visible = false;
			btnToggleTournamentAdditionalDetailsPlus.Text = "+";
        	
        	pnlTournamentEventSetup.Visible = false;
			btnToggleTournamentEventSetupPlus.Text = "+";        	
        }
                
        protected void btnToggleTournamentDetails_Click(object sender, EventArgs e)
        {
        	try
			{
				if (btnToggleTournamentDetailsPlus.Text.Equals("+"))
				{				
					AllPanelCollapse();
					
					btnToggleTournamentDetailsPlus.Text = "-";
					pnlTournamentDetails.Visible = true;
				}
				else
				{				
					btnToggleTournamentDetailsPlus.Text = "+";
					pnlTournamentDetails.Visible = false;
				}
			}
			catch (Exception ex)
			{
				System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "SF_RegistrationFormCorporate.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);                
			}
        }
        
        private void GetTournamentDetails()
        {
        	try
			{
				
        		
        	/*	strTournamentCode = (string)Session["TOURNAMENTCODE"]; 
	        	List<TPPaymentSummary> objlist =  (new TPDAL_Registration()).GetPaymentSummary(strTournamentCode);
	        	
        		//Copy all form details to Object
				if ( Session["TournamentDetails"] == null)
				
					objTornamentDetails = new TPTournament();
				else
					objTornamentDetails = (TPTournament) Session["TournamentDetails"] ;
				
				objTornamentDetails.SportCode = strSportCode;
				objTornamentDetails.TournamentCode = strTournamentCode;
				objTornamentDetails.TournamentName = txtTournamentName.Text;
				objTornamentDetails.TournamentVenue = txtTournamentVenue.Text;
				objTornamentDetails.TournamentEntryOpenDate = DateTime.Parse( txtTournamentEntryOpenDate.Text);
				objTornamentDetails.TournamentEntryCloseDate = DateTime.Parse( txtTournamentEntryEndDate.Text);
				objTornamentDetails.TournamentEntryWithdrawlDate = DateTime.Parse(  txtTournamentWithdrawaldate.Text);
				objTornamentDetails.TournamentStartDate = DateTime.Parse( txtTournamentStartDate.Text);
				objTornamentDetails.TournamentEndDate = DateTime.Parse( txtTournamentEndDate.Text);
				objTornamentDetails.TournamentPOCContactNames = txtTournamentPOCNames.Text;
				objTornamentDetails.TournamentPOCContactNo = txtTournamentPOCContacts.Text;
				Session["TournamentDetails"] = objTornamentDetails;
				*/	
			}
			catch (Exception ex)
			{
				System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_TournamentSetup.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);                
			}
        }
                
		private void CopyTournamentDetails()
		{
			try
			{
				//Copy all form details to Object
				if (Session["TournamentDetails"] == null)
				
					objTornamentDetails = new TPTournament();
				else
					objTornamentDetails = (TPTournament) Session["TournamentDetails"] ;
				
				objTornamentDetails.SportCode = strSportCode;
				objTornamentDetails.TournamentCode = strTournamentCode;
				objTornamentDetails.TournamentName = txtTournamentName.Text;
				objTornamentDetails.TournamentVenue = txtTournamentVenue.Text;
				objTornamentDetails.TournamentEntryOpenDate = DateTime.Parse( txtTournamentEntryOpenDate.Text);
				objTornamentDetails.TournamentEntryCloseDate = DateTime.Parse( txtTournamentEntryEndDate.Text);
				objTornamentDetails.TournamentEntryWithdrawlDate = DateTime.Parse(  txtTournamentWithdrawaldate.Text);
				objTornamentDetails.TournamentStartDate = DateTime.Parse( txtTournamentStartDate.Text);
				objTornamentDetails.TournamentEndDate = DateTime.Parse( txtTournamentEndDate.Text);
				objTornamentDetails.TournamentPOCContactNames = txtTournamentPOCNames.Text;
				objTornamentDetails.TournamentPOCContactNo = txtTournamentPOCContacts.Text;
				Session["TournamentDetails"] = objTornamentDetails;
					
			}
			catch (Exception ex)
			{
				System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_TournamentSetup.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);                
			}
		}
		
        protected void btnToggleTournamentAdditionalDetails_Click(object sender, EventArgs e)
        {
        	try
			{				
				if (btnToggleTournamentAdditionalDetailsPlus.Text.Equals("+"))
				{				
					AllPanelCollapse();
					
					btnToggleTournamentAdditionalDetailsPlus.Text = "-";
					pnlTournamentAdditionalDetails.Visible = true;
										
					if (Session["TournamentDetails"] != null)
					{
						CopyTournamentDetails();
					}					
					else	
					{
						lblErrorMsg.Text = "Set the tournament details";						
						pnlErrorMsg.Visible = true;
						
						btnToggleTournamentAdditionalDetailsPlus.Text = "+";
						pnlTournamentAdditionalDetails.Visible = false;
						
						pnlTournamentDetails.Visible = true;
        				btnToggleTournamentDetailsPlus.Text = "-";
					}
				}
				else
				{				
					btnToggleTournamentAdditionalDetailsPlus.Text = "+";
					pnlTournamentAdditionalDetails.Visible = false;
				}
			}
			catch (Exception ex)
			{
				System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_TournamentSetup.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);                
			}
        }
        
        private void TournamentEventSetupCollapse()
		{
			btnToggleTournamentEventSetupPlus.Text = "+";
			pnlTournamentEventSetup.Visible = false;
		}
		
		private void TournamentEventSetupExpand()
		{
			
			try{
				btnToggleTournamentEventSetupPlus.Text = "-";
				pnlTournamentEventSetup.Visible = true;
				//Bind Event list
	
				List<TPTornamentEventDetail> objeventList =  (List<TPTornamentEventDetail> ) Session["EventList"];
				dgTournamentEvent.DataSource =  objeventList;
				dgTournamentEvent.DataBind () ;
				TPTornamentEventDetail obj ;
				RadioButtonList objRadio ;
				string strEvent ;
				for (int i = 0; i< dgTournamentEvent.Items.Count ; i++)
				{
					objRadio = (RadioButtonList) dgTournamentEvent.Items[i].FindControl("rblBeforeAfter");
					strEvent = ((Label) dgTournamentEvent.Items[i].FindControl("dgtbEventCode")).Text;
					obj = objeventList.Find(x => x.strEventCode == strEvent);
					if(obj.strAfterBeforeFlag != null)
						objRadio.Items.FindByText(obj.strAfterBeforeFlag).Selected = true;
				}
				
			}
			catch (Exception ex)
			{
				System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_TournamentSetup.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);                
			}
		}
        
        protected void btnToggleTournamentEventSetup_Click(object sender, EventArgs e)
        {
        	try
			{
        		
				if (btnToggleTournamentEventSetupPlus.Text.Equals("+"))
				{		
					AllPanelCollapse();					
					TournamentEventSetupExpand();					
				}
				else
				{				
					TournamentEventSetupCollapse();					
				}
			}
			catch (Exception ex)
			{
				System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "SF_RegistrationFormCorporate.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);                
			}
        }
        
        protected void btnUpdateEvent_Click(object sender, EventArgs e)
        {
        	try
			{
        		string strEventCode ;
        		string strBeforeAfter ;
        		string strFee;
        		string strDateRefernce;
        		string strGender;
        		TPTornamentEventDetail objEvent ;
				List<TPTornamentEventDetail> objeventList =  (List<TPTornamentEventDetail> ) Session["EventList"];	
				for (int i = 0 ; i< dgTournamentEvent.Items.Count ; i++)
				{
					Label lblEventCode= (Label) dgTournamentEvent.Items[i].FindControl("dgtbEventCode");
					strEventCode = lblEventCode.Text;
					
					Label lblGender= (Label) dgTournamentEvent.Items[i].FindControl("dgtbGender");
					strGender = lblGender.Text;

					TextBox tbDate= (TextBox) dgTournamentEvent.Items[i].FindControl("dgtbDOBRefernce");
					strDateRefernce = tbDate.Text;
					
					RadioButtonList rblBeforeAfter= (RadioButtonList) dgTournamentEvent.Items[i].FindControl("rblBeforeAfter");
					strBeforeAfter = rblBeforeAfter.SelectedItem.Value;
					
					TextBox tbFee= (TextBox) dgTournamentEvent.Items[i].FindControl("dgtbFee");
					strFee = tbFee.Text;
					
					objEvent = objeventList.Find(x => x.strEventCode == strEventCode);
					
					objeventList.Remove(objeventList.Find(x => x.strEventCode == strEventCode));
					
					objEvent.dlReferenceDate = strDateRefernce;
					objEvent.iEventRateCard = strFee;
					objEvent.strGender = strGender;
					objEvent.strAfterBeforeFlag = strBeforeAfter;
					
					objeventList.Add(objEvent);
					
				}
				
				Session["EventList"] = objeventList;
				
				AllPanelCollapse();
				
				lblErrorMsg.Text = "Intrim Data Saved, Click on SUBMIT to Reflect Data into Database";
				pnlErrorMsg.Visible = true;
			}
			catch (Exception ex)
			{
				System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_TournamentSetup.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);                
			}
        }
        
        #region Toggle Buttons
                
        protected void btnToggleQuickAccess_Click(object sender, EventArgs e)
        {
        	
        		HideAllOtherPanels();
        	
        }
        	
        	
        private void HideAllOtherPanels()
        {
        	pnlSetupTournament.Visible = false;
        	//pnlPlayerList.Visible = false;
        	//pnlGenerateDraws.Visible = false;
        	//pnlQuickAccess.Visible = false;
        	//pnlPayment.Visible = false;
        	//pnlOfflineRegistration.Visible = false;
        	
        	//btnToggleQuickAccess.Visible = true;
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
        	//pnlPlayerList.Visible = true;
        }
        
        protected void btnSetupDrawsView_Click(object sender, EventArgs e)
        {
        	//Hide All other Panels
        	HideAllOtherPanels();
        	//pnlGenerateDraws.Visible = true;
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
        	//pnlPayment.Visible = true;
        	//GetPaymentSummary();
        }
        
        protected void btnOfflineRegistration_Click(object sender, EventArgs e)
        {
        	//Hide All other Panels
        	HideAllOtherPanels();
        	//pnlOfflineRegistration.Visible = true;
        	//GetPaymentSummary();
        }
        
        protected void GetPaymentSummary()
        {
        	try{
	        	strTournamentCode = (string)Session["TOURNAMENTCODE"]; 
	        	List<TPPaymentSummary> objlist =  (new TPDAL_Registration()).GetPaymentSummary(strTournamentCode);
	        	for(int index = 0; index < objlist.Count ; index++)
	        	{
	        		//if (objlist[index].PaymentType.ToUpper() == "COMPLETED")
	        			//lblTotalConfirmedAmount.Text = objlist[index].PaymentAmount;
	        		//else if (objlist[index].PaymentType.ToUpper() == "PENDING")
	        			//lblTotalPendingAmount.Text = objlist[index].PaymentAmount;
	        		//else if (objlist[index].PaymentType.ToUpper() == "STATUS UPDATE - IN PROGRESS")
	        			//lblTotalInProgressAmount.Text = objlist[index].PaymentAmount;
	        	}
        	}
        	catch (Exception ex)
        	{
				//pnlOwnerLogin.Visible = true;
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
			//if (btnToggleGenerateDraws.Text.Equals("+ Generate Draws"))
			{
			//	btnToggleGenerateDraws.Text = "- Generate Draws";
			//	pnlGenerateDraws.Visible = true;
			}
			//else
			{
			//	btnToggleGenerateDraws.Text = "+ Generate Draws";
			//	pnlGenerateDraws.Visible = false;
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
	                //PlaceHolder1.Controls.Add(lb); // Adding the LinkButton in PlaceHolder
		                
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
		                //PlaceHolder1.Controls.Add(lb); // Adding the LinkButton in PlaceHolder
		                i = i + 1;
		            
	        		}
	        	}
	       	}
        	catch (Exception ex)
        	{
				//pnlOwnerLogin.Visible = true;
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
            
            //Label1.Text = strSelectedLinkButton; // will display the which Linkbutton clicked
 
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
				
				//dgPlayerList.DataSource = lstPlayerList;
				//dgPlayerList.DataBind();
				
				int iPlayerCount = lstPlayerList.Count;
				//Label1.Text = strEventCode + " ( " + iPlayerCount.ToString() + " )";
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
                
        #region Setup Tournament
        
        protected void btnTournamentDetailSave_Click(object sender , EventArgs e)
        {
        	try
            {
        		TPTournament objTornamentDetails = (TPTournament) Session["TournamentDetails"] ;
        		
        		List<TPTornamentEventDetail> objeventList =  (List<TPTornamentEventDetail> ) Session["EventList"];	
				TPTornamentEventDetail objevent ;
        		for ( int i = 0 ; i < cblEvents.Items.Count ; i ++)
        		{
        			if(!cblEvents.Items[i].Selected)
        				objeventList.Remove(objeventList.Find( x => x.strEventCode == cblEvents.Items[i].Text));
        			else
        			{
        				if (objeventList.Find( x => x.strEventCode == cblEvents.Items[i].Text) == null)
        				{
        					objevent = new TPTornamentEventDetail();
        					objevent.strEventCode = cblEvents.Items[i].Text;
        					objevent.strGender = cblEvents.Items[i].Value;
        					objeventList.Add(objevent);
        				}
        			}
        		}
        		Session["EventList"] = objeventList;
        		
        		lblErrorMsg.Text = "Intrim Data Saved, Click on SUBMIT to Reflect Data into Database";
        		pnlErrorMsg.Visible = true;
        		
        		AllPanelCollapse();
        		
        	}
        	catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_TournamentSetup.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
            }
        }
        
        protected void btnSetupTournament_Click(object sender, EventArgs e)
		{
			try
            {
				if (Session["TOURNAMENTCODE"] == null)
	        	{
		        	//pnlOwnerLogin.Visible = true;
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
		                	
							TPDAL_TournamentController objDALTPController = new TPDAL_TournamentController();                        
							objDALTPController.SetupTournament (objTournament);
							List<TPTornamentEventDetail> objeventList =  (List<TPTornamentEventDetail> ) Session["EventList"];	
				
							objDALTPController.SetupTournamentEvent(strSportCode, strTournamentCode, objeventList);
							
							pnlErrorMsg.Visible = true;
	                		lblErrorMsg.Text = "Data Submitted Successfully";
	                		
	                		AllPanelCollapse();
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
        
        #region Export
        
        public void ExportPlayerList_Click(object sender, System.EventArgs e)
		{
        	try{
                string strTitle = ""; // Label1.Text;
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
			
			//dgPlayerList.RenderControl(htmlWrite);
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
                string strTitle = ""; // Label1.Text;
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
			
			//dgPlayerList.RenderControl(htmlWrite);
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
        
	}
}
