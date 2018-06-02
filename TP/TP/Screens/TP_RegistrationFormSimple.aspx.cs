using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TP.DAL;
using TP.Entity;
using System.Globalization;
using System.Text;
using System.IO;


namespace TournamentPlanner
{
    public partial class TP_RegistrationFormSimple : System.Web.UI.Page
    {
    	string strSportCode = "BD";
    	string strTournamentCode = "";
    	
    	//int const_FileSize = 10240000;
    	List<String> lstObjEvents;
    	DataTable dtPlayerParticipation;
    	List<TPEventListItem> lstEvent;
        protected void Page_Load(object sender, EventArgs e)
        {
        	Page.Form.Attributes.Add("enctype", "multipart/form-data");
        	
        	pnlErrorMsg.Visible = false;
        	
			try
			{
                if (!IsPostBack)
                {
                    string script = "$(document).ready(function () { $('[id*=btnPopulateEventCategory]').click(); });";
                    ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);
                }


				Session["SPORTCODE"] = strSportCode;
		        strTournamentCode = Request.QueryString["TCode"];

                List<TPStatus> lstTPStatusObj = (new TPDAL_Tournament()).GetTournamentStatus(strSportCode, strTournamentCode);

                if (lstTPStatusObj != null && lstTPStatusObj.Count > 0)
                {
                string strRegistrationStatus = lstTPStatusObj[0].RegistrationStatus;

                if (!strRegistrationStatus.Equals("OPEN"))  //than means CLOSED
                {
                    //Registration has been CLOSED
                    lblErrorMsg.Text = "Registration has been CLOSED";
                }
                else
                {
                    Session["TOURNAMENTCODE"] = strTournamentCode;

                    if (string.IsNullOrEmpty(strTournamentCode))
                    {
                        Response.Redirect("./TP_BD_Home.aspx", false);
                    }
                    else
                    {
                        pnlErrorMsg.Visible = false;

                        if (IsPostBack)
                        {
                            //PopulateEventForm();									
                        }
                        else
                        {
                            PopulateTournamentSummary();

                            //PopulateEvents();

                            lstObjEvents = new List<string>();
                        }
                    }
                }
            }
			}
			catch (Exception ex)
			{
				System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationFormSimple.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
                //Response.Redirect("sportfit.co.in", false);
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
        
      	protected void DateChange(object sender, EventArgs e)
	    {
	        //txtBirthdate.Text = Calendar1.SelectedDate.ToShortDateString() + '.';
	    }
      
      	protected void CustomValidator1_ServerValidate(object sender, ServerValidateEventArgs e)
		{
		    DateTime d;
		    e.IsValid = DateTime.TryParseExact(e.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out d);
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
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationFormSimple.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        
        }
        
        #region Already Registered


        private TPPlayer GetPlayerDetailsByPlayerCode(string strSelectedPlayerCode)
        {
            //Get PlayerDetails as per selected PlayerCode
            TPPlayer obj = (new TPDAL_PlayerDetails()).GetPlayerDetailsWithPlayerCode(strTournamentCode, strSelectedPlayerCode);

            return obj;
        }

        private void FillPrimaryPlayerFormWithSelectedPlayerDetails(TPPlayer obj)
        {
            if (obj != null)
            {
                txtFirstName.Text = obj.PlayerFName;
                txtLastName.Text = obj.PlayerLName;
                txtContact.Text = obj.PlayerContact;
                txtEmailID.Text = obj.PlayerEmailID;
                txtAddress.Text = obj.PlayerAddress;
                ddlGender.ClearSelection();
                ddlGender.Items.FindByText(obj.PlayerGender).Selected = true;
                //ddlGender.SelectedItem.Text = obj.PlayerGender;

                string strPlayerDOB = obj.PlayerDOB;
                ddlDate.ClearSelection();
                ddlDate.Items.FindByValue(strPlayerDOB.Split('/')[0].Trim()).Selected = true;
                //ddlDate.SelectedItem.Text = strPlayerDOB.Split ('/')[0].Trim();

                ddlMonth.ClearSelection();
                ddlMonth.Items.FindByValue(strPlayerDOB.Split('/')[1].Trim()).Selected = true;
                //ddlMonth.SelectedItem.Text = strPlayerDOB.Split('/')[1].Trim();

                txtDOBYear.Text = strPlayerDOB.Split('/')[2].Trim();

            }
        }

        protected void btnCheckAlreadyRegPlayer_Click(object sender, EventArgs e)
        { 
            //Check if the player is already exists based on mobile no
            string strMobileNo =  txtAlreadyRegisteredMobile.Text;

            TPDAL_PlayerDetails obj = new TPDAL_PlayerDetails();

            List<TPPlayer> lstObj = obj.GetPlayerDetailsByMobileNo(strTournamentCode, strMobileNo);

            if (lstObj.Count > 0)
            {
                pnlAlreadyRegisteredPlayers.Visible = true;
                //pnlPlayerDetails.Visible =  false;
                lblErrMsgPlayerDetails.Text = "";
                ddlAlreadyRegisteredPlayers.DataSource = lstObj;

                ddlAlreadyRegisteredPlayers.DataTextField = "PlayerFullName";
                ddlAlreadyRegisteredPlayers.DataValueField = "PlayerCode";
                ddlAlreadyRegisteredPlayers.DataBind();

                string strSelectedPlayerCode = ddlAlreadyRegisteredPlayers.SelectedValue;
                                
                FillPrimaryPlayerFormWithSelectedPlayerDetails(GetPlayerDetailsByPlayerCode(strSelectedPlayerCode));

            }
            else
            {
                pnlAlreadyRegisteredPlayers.Visible = false;
                lblErrMsgPlayerDetails.Text = "No Player Found, Please Register as a new Player";
                //pnlPlayerDetails.Visible = true;
            }
        }

        protected void ddlAlreadyRegisteredPlayers_SelectedIndexChanged(object sender, EventArgs e)   
        {
            string strSelectedPlayerCode = ddlAlreadyRegisteredPlayers.SelectedValue;

            FillPrimaryPlayerFormWithSelectedPlayerDetails(GetPlayerDetailsByPlayerCode(strSelectedPlayerCode));            
        }
        
        #endregion


        #region Curtains Expand Collapse
        
        private void AllPanelCollapse()
        {
        	pnlPlayerDetails.Visible = false;
        	btnTogglePlayerDetailsPlus.Text = "+";
        	
        	pnlAddEvent.Visible = false;
			btnToggleAddEventPlus.Text = "+";
        	
        	pnlSummary.Visible = false;
        	btnToggleSummaryPlus.Text = "+";
        }
        
        private void ClearAllErrorMsgs ()
        {
        	lblErrMsgPlayerDetails.Text = "";
        	lblErrMsgPAddEvents.Text = "";
        	lblPartnerErrorMsg.Text = "";        	
        }
        
		protected void btnTogglePlayerDetails_Click(object sender, EventArgs e)
		{
			try
			{
				if (btnTogglePlayerDetailsPlus.Text.Equals("+"))
				{				
					AllPanelCollapse();
					ClearAllErrorMsgs();
					
					btnTogglePlayerDetailsPlus.Text = "-";
					pnlPlayerDetails.Visible = true;
				}
				else
				{				
					btnTogglePlayerDetailsPlus.Text = "+";
					pnlPlayerDetails.Visible = false;
				}			
			}
			catch (Exception ex)
			{
				System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationFormSimple.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);                
			}
		}
		
		private void AddEventCollapse()
		{
			btnToggleAddEventPlus.Text = "+";
			pnlAddEvent.Visible = false;
		}
		
		private void AddEventExpand()
		{
			btnToggleAddEventPlus.Text = "-";
			
			chkbDistrictReg.Checked = false;
			lblTotalAmount.Text = "";
			
			pnlAddEvent.Visible = true;
		}
		
		protected void btnToggleAddEvent_Click(object sender, EventArgs e)
		{
			try
			{
				if(UIValidationforPlayerDetails())
				{					
					if (btnToggleAddEventPlus.Text.Equals("+"))
					{	
						AllPanelCollapse();
						ClearAllErrorMsgs();
						
						btnToggleAddEventPlus.Text = "-";
				
						chkbDistrictReg.Checked = false;
						lblTotalAmount.Text = "";
						
						pnlAddEvent.Visible = true;
				
						//PopulateEventForm ();			
					}
					else
					{								
						btnToggleAddEventPlus.Text = "+";
						pnlAddEvent.Visible = false;
					}					
				}
				else
				{
					AllPanelCollapse();
					ClearAllErrorMsgs();
						
					btnTogglePlayerDetailsPlus.Text = "-";
					pnlPlayerDetails.Visible = true;
					
					lblErrMsgPlayerDetails.Text = "Please Complete the Form";
				}
			}
			catch (Exception ex)
			{
				System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationFormSimple.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);                
			}
		}
		
		private void SummaryCollapse()
		{
			btnToggleSummaryPlus.Text = "+";
			pnlSummary.Visible = false;
		}
		
		private void SummaryExpand()
		{
			btnToggleSummaryPlus.Text = "-";
			pnlSummary.Visible = true;
		}
		
		protected void btnToggleSummary_Click(object sender, EventArgs e)
		{
			if(UIValidationforPlayerDetails())
			{
				bool blFlag = ValidateEntrycompletion();
				if(blFlag == true)
				{
					AllPanelCollapse();
					ClearAllErrorMsgs();
					
					btnToggleSummaryPlus.Text = "-";
					pnlSummary.Visible = true;
									
					PopulateSummary();
				}
				else
				{
					btnToggleAddEventPlus.Text = "-";
			
					chkbDistrictReg.Checked = false;
					lblTotalAmount.Text = "";
					
					pnlAddEvent.Visible = true;
				}
			}
			else
			{
				btnTogglePlayerDetailsPlus.Text = "-";
				pnlPlayerDetails.Visible = true;
			}
		}
        		
		protected void btnViewSummary_Click(object sender, EventArgs e)
		{
			SummaryExpand();
			AddEventCollapse();
			PopulateSummary();
		}
				
		#endregion Curtains Expand Collapse

		#region Event Details		
		
        private void PopulatePlayerDetailstoSelectPartner()
        {
            TPDAL_Registration obj = new TPDAL_Registration();

            List<TPPlayer> lstObj = obj.GetAllPlayerList(strSportCode, strTournamentCode);

            if (lstObj.Count > 0)
            {
                ddlRegisteredPartner.DataSource = lstObj;

                ddlRegisteredPartner.DataTextField = "PlayerFullName";// +" ( " + "PlayerContact" + " ) ";
                ddlRegisteredPartner.DataValueField = "PlayerCode";
                ddlRegisteredPartner.DataBind();

                //string strSelectedPlayerCode = ddlRegisteredPartner.SelectedValue;

                //GetPlayerDetailsByPlayerCode(strSelectedPlayerCode);

            }
            else
            {
                pnlAlreadyRegisteredPlayers.Visible = false;
                lblErrMsgPlayerDetails.Text = "No Player Found, Please Register as a new Player";
                //pnlPlayerDetails.Visible = true;
            }
        
        }
        
        protected void btnAlreadyRegisteredPlayerAsPartner_Click(object sender, EventArgs e)
        {
            pnlExistingPlayerAsPartner.Visible = true;
            pnlNewPartnerDetails.Visible =  false;

            PopulatePlayerDetailstoSelectPartner();

            string strSelectedPlayerCode = ddlRegisteredPartner.SelectedValue;
            FillPartnerPlayerFormWithSelectedPlayerDetails(GetPlayerDetailsByPlayerCode(strSelectedPlayerCode));
        }

        protected void btnNewPartnerDetails_Click(object sender, EventArgs e)
        {
            pnlNewPartnerDetails.Visible = true;
            pnlExistingPlayerAsPartner.Visible = false;
        }

        //Partner form file
        private void FillPartnerPlayerFormWithSelectedPlayerDetails(TPPlayer obj)
        {
            if (obj != null)
            {
                txtPartnerFirstName.Text = obj.PlayerFName;
                txtPartnerLastName.Text = obj.PlayerLName;
                txtPartnerContact.Text = obj.PlayerContact;
                txtPartnerEmailID.Text = obj.PlayerEmailID;
                txtPartnerAddress.Text = obj.PlayerAddress;
                ddlPartnerGender.ClearSelection();
                ddlPartnerGender.Items.FindByText(obj.PlayerGender).Selected = true;
                //ddlGender.SelectedItem.Text = obj.PlayerGender;

                string strPlayerDOB = obj.PlayerDOB;
                ddlPartnerDate.ClearSelection();
                ddlPartnerDate.Items.FindByValue(strPlayerDOB.Split('/')[0].Trim()).Selected = true;
                //ddlDate.SelectedItem.Text = strPlayerDOB.Split ('/')[0].Trim();

                ddlPartnerMonth.ClearSelection();
                ddlPartnerMonth.Items.FindByValue(strPlayerDOB.Split('/')[1].Trim()).Selected = true;
                //ddlMonth.SelectedItem.Text = strPlayerDOB.Split('/')[1].Trim();

                txtPartnerDOBYear.Text = strPlayerDOB.Split('/')[2].Trim();

            }
        }

        protected void ddlRegisteredPartner_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strSelectedPlayerCode = ddlRegisteredPartner.SelectedValue;

            FillPartnerPlayerFormWithSelectedPlayerDetails(GetPlayerDetailsByPlayerCode(strSelectedPlayerCode));
        }

        protected void btnExistingPlayerAsPartner_Click(object sender, EventArgs e)
        {
            PartnerDetailsToBeAddedInTheGrid();
        }

		protected void btnAddPartner_Click(object sender, EventArgs e)
		{   
            try
            {
                Label lblEvent = (Label)((Button)sender).Parent.FindControl("dgtbEventCode");
                txtEventName.Text = lblEvent.Text;
                
                txtEventName.Enabled = true;
                pnlPartnerDetails.Visible = true;
                ddlPartnerGender.ClearSelection();

                if (txtEventName.Text.ToUpper().Contains("M"))
                    ddlPartnerGender.Items.FindByValue("M").Selected = true;
                else if (txtEventName.Text.ToUpper().Contains("W"))
                    ddlPartnerGender.Items.FindByValue("F").Selected = true;
                else if (txtEventName.Text.ToUpper().Contains("B"))
                    ddlPartnerGender.Items.FindByValue("M").Selected = true;
                else if (txtEventName.Text.ToUpper().Contains("G"))
                    ddlPartnerGender.Items.FindByValue("F").Selected = true;
                else if (txtEventName.Text.ToUpper().Contains("X"))
                {
                    if (ddlGender.SelectedValue == "M")
                        ddlPartnerGender.Items.FindByValue("F").Selected = true;
                    else
                        ddlPartnerGender.Items.FindByValue("M").Selected = true;
                }

                HiddenField hfDOBReference = (HiddenField)((Button)sender).Parent.FindControl("hfReferenceDOB");
                HiddenField hfAfterBefore = (HiddenField)((Button)sender).Parent.FindControl("hfAfterBefore");

                hfAfterBeforePartner.Value = hfAfterBefore.Value;
                hfReferencePartnerDOB.Value = hfDOBReference.Value;

                //Populated already registered Players to select for partner
                PopulatePlayerDetailstoSelectPartner();

                pnlNewPartnerDetails.Visible = false;
                pnlExistingPlayerAsPartner.Visible = false;
            }
            catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationFormSimple.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
            }
        }
        		
		protected void chkview_CheckedChanged(object sender, EventArgs e)
		{
			try
        	{
			   //CheckBox cb1= ((CheckBox)sender);
			   //HiddenField hfFees = (HiddenField)((CheckBox)sender).NamingContainer.FindControl("hfFees");
			//    CheckBox cb1 = (CheckBox)dgEventParticipation.SelectedItem.FindControl("chkview");
			    //string yourvalue = cb1.Text;
			  //  HiddenField hfFees = (HiddenField)dgEventParticipation.SelectedItem.FindControl("hfFees");
			    //string str = hfFees.Value;
			    //here you can find your control and get value(Id).
			    
			    if( ((CheckBox)sender).Checked == false)
			    {
			    	Label lblName=  (Label)((CheckBox)sender).Parent.FindControl("dgtbPartnerName");
			    	lblName.Text = "";
			    }
			    int iRowCount = dgEventParticipation.Items.Count;
			    int iTotalFees = 0;
			    lstEvent = (List<TPEventListItem>) Session["EventList"];
		    	if (lstEvent == null)
		    			lstEvent = new List<TPEventListItem>();
		    	
			    for (int i = 0; i < iRowCount; i++)
			    {
			    	
			    	Label lbl1 =  (Label)dgEventParticipation.Items[i].FindControl("dgtbEventCode");
			    	HiddenField hfFees = (HiddenField)dgEventParticipation.Items[i].FindControl("hfFees");
			    	CheckBox cb1 = (CheckBox)dgEventParticipation.Items[i].FindControl("dgcbEventSelect");
			    	Button btn1 = (Button) dgEventParticipation.Items[i].FindControl("dgtbAddPartner");
			    		
			    	if (cb1.Checked)
			    	{
			    		
			    		string strFees = hfFees.Value;
			    		int iFees = int.Parse(strFees);
			    		
			    		if ( lstEvent.Find(x => x.EventCode == lbl1.Text) == null)
			    		{
			    			TPEventListItem objEvent = new TPEventListItem();
			    			objEvent.EventCode = lbl1.Text;
			    			objEvent.EventFee = iFees;
			    			lstEvent.Add(objEvent);
			    			
			    		}
			    		if(lbl1.Text.Contains("D"))
			    		{
			    			btn1.Enabled = true;
			    			if(lbl1.Text.Contains("B"))
			    				ddlPartnerGender.SelectedValue = "M";
			    			else if	(lbl1.Text.Contains("G"))
			    				ddlPartnerGender.SelectedValue = "F";
			    			else if (ddlGender.SelectedValue == "M") 
			    				ddlPartnerGender.SelectedValue = "F";
			    			else
			    				ddlPartnerGender.SelectedValue = "M";
			    			
			    			//ddlPartnerGender.Enabled= false;
			    		}
			    		else
			    			btn1.Enabled = false;
			    		
			    		iTotalFees = iTotalFees + iFees;
			    	}
			    	else
			    	{
			    		btn1.Enabled = false;
			    		if ( lstEvent.Find(x => x.EventCode == lbl1.Text) != null)
			    			lstEvent.Remove(lstEvent.Find(x => x.EventCode == lbl1.Text) );
			    	}
			    }
			    
			    Session ["EventList"] = lstEvent;
			    if(chkbDistrictReg.Checked)
			    	iTotalFees += 30;
				lblTotalAmount.Text = iTotalFees.ToString();
			   
				pnlPartnerDetails.Visible  =false;
			}
			catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationFormSimple.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}		    
		}		
		
		/// <summary>
		/// Populate form with all the registered events for tournament
		/// </summary>
		///
		private bool ValidateEntrycompletion()
		{
			bool blFlag = true;
			
			try{
				
				lstEvent = (List<TPEventListItem>) Session["EventList"];
				dtPlayerParticipation = (DataTable)Session["PLAYERPARTICIPATION"];
				if(dtPlayerParticipation == null)
				{
					blFlag = false;
					lblErrMsgPlayerDetails.Text = "Please enter Player Details";					
				}
				else if(lstEvent == null )
				{
					blFlag = false;
					lblErrMsgPAddEvents.Text = "No Events selected, Please select events.";
				}
				else if (lstEvent.Count == 0) {
					blFlag = false;
					lblErrMsgPAddEvents.Text = "No Events selected, Please select events.";
					
				}
				else
				{
					for (int i = 0 ;i <lstEvent.Count ; i++)
					{
						if(lstEvent[i].EventCode.Contains("D"))
						{
							if (lstEvent[i].PartnerDetails == null)
							{
								blFlag = false;
								lblErrMsgPAddEvents.Text = "Please enter partner details for Doubles events.";
							}
						}
					}
				}
					
				
			}
			catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationFormSimple.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}	
			return blFlag;
		}
		
		
		protected void btnPopulateEventCategory_Click(object sender, EventArgs e)
		{
			String strSelectedGender = ddlGender.SelectedItem.Text;
			
			if (strSelectedGender.Equals("Select Gender"))
			{
				lblErrMsgPAddEvents.Text = "Please Select Gender";
			}
			else
			{
				//Reference Date for Tournament Age Limit
			    DateTime dtReferenceAgeDate = new DateTime(2018,01,01);
				
			    string strPlayerDOB = ddlDate.SelectedItem.Text + "/" + ddlMonth.SelectedValue + "/" + txtDOBYear.Text;
				DateTime d;
	    		bool blFlag = DateTime.TryParseExact(strPlayerDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out d);
	    	 	
	    		if (blFlag)
	    		{
		    		string strGender = ddlGender.SelectedValue;
				    CultureInfo provider = CultureInfo.InvariantCulture;
				        		        			
		        	DateTime dtDOB = DateTime.ParseExact(strPlayerDOB,"dd/MM/yyyy", provider);	        	
		        	int iAgeLimit = dtReferenceAgeDate.Year - dtDOB.Year ;//difference between dtDOB - dtReferenceAgeDate;
                    
		     		List<TPEventRegistration> objEvents= (new TPDAL_Events()).GetAllEventsBySportANDTournamentForRegistration(strSportCode, strTournamentCode, strGender, dtDOB );
		    		
		    		if (objEvents != null)
					{
						if (objEvents.Count > 0)
		    			{		    
							PopulateEventForm(objEvents);
                            lblTournamentEvents.Visible = false;
		    			}    		
		    			else
		    			{
                            lblTournamentEvents.Visible = true;
		    				lblTournamentEvents.Text = "There is no Event/Category matching as per the Player BirthDate";
		    			}
		    		}
		    		
		    		lblErrMsgPAddEvents.Text = "";
	    		}
	    		else
	    		{
	    			lblErrMsgPAddEvents.Text = "Please enter a valid DOB for player";
	    		}
			}
		}
		
		
		private void PopulateEventForm (List<TPEventRegistration> objEvents)
		{
			
			try
        	{				
				//lblTournamentEvents.Text = "Tournament Events";
				dtPlayerParticipation = new DataTable("PlayerParticipation");
				dtPlayerParticipation.Columns.Add("ID");
				dtPlayerParticipation.Columns.Add("TournamentCode");
				dtPlayerParticipation.Columns.Add("Event");
				dtPlayerParticipation.Columns.Add("Fees");					
				dtPlayerParticipation.Columns.Add("PartnerName");
				dtPlayerParticipation.Columns.Add("PartnerDOB");
				dtPlayerParticipation.Columns.Add("ReferenceDate");
				dtPlayerParticipation.Columns.Add("AfterBefore");
				
				
				for (int i = 0; i < objEvents.Count; i++)						                    
                {					        	
		        	
					String strEventName =  objEvents[i].EventName;
	        		
					String strFeesAmount = objEvents[i].EventRate.ToString();
	        		
					String strID = (dtPlayerParticipation.Rows.Count+1).ToString();
	        		   
	        		String strPartnerName = "";
	        			
	        		String strPartnerDOB = "";
	        		
	        		dtPlayerParticipation.Rows.Add(strID, strTournamentCode, strEventName, strFeesAmount, strPartnerName, strPartnerDOB, objEvents[i].AgeReference, objEvents[i].strAfterBefore );
		        	
				}
				
				Session["PLAYERPARTICIPATION"] = dtPlayerParticipation;
				dgEventParticipation.DataSource = dtPlayerParticipation;
				dgEventParticipation.DataBind();
				
				
				for (int i = 0; i < objEvents.Count; i++)						                    
				{
					String strEventName =  objEvents[i].EventName;							
	        		//int iIndexOfOpenBracket = strEventDetails.IndexOf ("(");
	        		//int iIndexOfCloseBracket = strEventDetails.IndexOf (")");
	        		String strEventSingle = strEventName.Substring (1, 1);

                    if (strEventSingle.Equals("S"))
                    {
                        //Label tbPartnName = (Label)dgEventParticipation.Items[i].FindControl("dgtbPartnerName");
                        //tbPartnName.Enabled = false;			        			

                        //TextBox tbPartnerDOB = (TextBox)dgEventParticipation.Items[i].FindControl("dgtbPartnerDOB");

                        Button btnAdd = (Button)dgEventParticipation.Items[i].FindControl("dgtbAddPartner");

                        //btnAdd.Enabled = false;
                        btnAdd.Visible = false;
                    }
                    else
                    {
                        Button btnAdd = (Button)dgEventParticipation.Items[i].FindControl("dgtbAddPartner");

                        btnAdd.Enabled = false;
                        btnAdd.Visible = true;
                    }
        		
				}
				
        	}
        	
        	
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationFormSimple.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}		
		}
	
	protected void btnPartnerClose_Click(object sender, EventArgs e)
		{
			try
        	{
				pnlPartnerDetails.Visible	 = false;				
			}
			catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationFormSimple.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
		}
	
	protected bool UIValidatePartnerPlayerDetailsForForm()
	{
		bool bFlag = true;
		try{
			string strPartnDOB = ddlPartnerDate.SelectedItem.Text + "/" + ddlPartnerMonth.SelectedValue + "/" + txtPartnerDOBYear.Text;
		    			
			DateTime dtPartnerDOB;
			DateTime dt;
			bool valid = DateTime.TryParseExact(strPartnDOB, "dd/MM/yyyy", null, DateTimeStyles.None, out dtPartnerDOB);
				
			if (string.IsNullOrEmpty(txtPartnerFirstName.Text) ||
                string.IsNullOrEmpty(txtPartnerLastName.Text) ||
                valid == false )
            {
            	//Show error message
				pnlPartnerErrorMsg.Visible = true;
            	lblPartnerErrorMsg.Text = "Complete Partner Details Form";                	
            	
            	bFlag = false;
            }
			else
			{
        		
				//	valid = DateTime.TryParseExact(hfReferencePartnerDOB.Value, "d/M/yyyy", null, DateTimeStyles.None, out dt);
					//if(valid)
					dt = DateTime.Parse(hfReferencePartnerDOB.Value);
					if(hfAfterBeforePartner.Value.ToUpper() == "AFTER" && dt > dtPartnerDOB)
					{
						lblPartnerErrorMsg.Text = "Partner's Birthdate is more then Age limit permitted"; 
						valid = false;
					}
					else if(hfAfterBeforePartner.Value.ToUpper() == "BEFORE" && dt < dtPartnerDOB)
					{
						lblPartnerErrorMsg.Text = "Partner's Birthdate is less then Age limit permitted"; 
						valid = false;
					}
		        	if(valid == false)
		        	{
		        		//Show error message
						pnlPartnerErrorMsg.Visible = true;
	                	//lblErrorMsg.Text = "Partner's Birthdate is more then Age limit permitted"; 
						bFlag = false;
		        	}
				}
			}

		
		catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationFormSimple.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
		return bFlag;
	}

    private void PartnerDetailsToBeAddedInTheGrid()
    {
        try
        {

            if (UIValidatePartnerPlayerDetailsForForm())
            {
                lstEvent = (List<TPEventListItem>)Session["EventList"];
                if (lstEvent == null)
                {

                }
                else
                {
                    TPEventListItem objEvent = lstEvent.Find(x => x.EventCode == txtEventName.Text);
                    if (objEvent != null)
                    {
                        TPPlayer objParternerDetails = new TPPlayer();
                        objParternerDetails.PlayerFName = txtPartnerFirstName.Text;
                        objParternerDetails.PlayerLName = txtPartnerLastName.Text;
                        objParternerDetails.PlayerFullName = txtPartnerFirstName.Text + " " + txtPartnerLastName.Text;
                        objParternerDetails.PlayerCity = txtPartnerCity.Text;
                        objParternerDetails.PlayerContact = txtPartnerContact.Text;
                        objParternerDetails.PlayerAddress = txtPartnerAddress.Text;
                        objParternerDetails.PlayerEmailID = txtPartnerEmailID.Text;

                        //objParternerDetails.PlayerDOB = ddlPartnerDate.SelectedValue + "/" +ddlPartnerMonth.SelectedValue + "/" +  ddlPartnerYear.SelectedItem.Text;

                        objParternerDetails.PlayerDOB = ddlPartnerDate.SelectedValue + "/" + ddlPartnerMonth.SelectedValue + "/" + txtPartnerDOBYear.Text;

                        objParternerDetails.PlayerGender = ddlPartnerGender.SelectedItem.Text;
                        objParternerDetails.TShirtSize = ddlPartnerTShirtSize.SelectedItem.Text;
                        objEvent.PartnerDetails = objParternerDetails;


                    }
                }

                //object obj = ((Button)sender).Parent;
                for (int i = 0; i < dgEventParticipation.Items.Count; i++)
                {
                    Label lblEventCodeCopy = (Label)dgEventParticipation.Items[i].FindControl("dgtbEventCode");
                    if (lblEventCodeCopy.Text == txtEventName.Text)
                    {
                        Label lblPartername = (Label)dgEventParticipation.Items[i].FindControl("dgtbPartnerName");
                        lblPartername.Text = txtPartnerFirstName.Text + " " + txtPartnerLastName.Text;
                        pnlPartnerDetails.Visible = false;
                        break;
                    }
                }
                ClearPartnerForm();
            }
            else
            {
                //lblPartnerErrorMsg.Text = "Enter All details for Partner with valid DOB";
                pnlPartnerErrorMsg.Visible = true;
            }
        }
        catch (Exception ex)
        {
            System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

            MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationFormSimple.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
            MGError objLogError = new MGError();
            objLogError.logError(objError);
        }    
    }


		protected void btnPartnerSave_Click(object sender, EventArgs e)
		{
            PartnerDetailsToBeAddedInTheGrid();
		}
    		
		private void ClearPartnerForm()
		{
			try{
					txtPartnerFirstName.Text = "";
	    			txtPartnerLastName.Text = "";
	    			txtPartnerCity.Text = "";
	    			txtPartnerContact.Text = "";
	    			txtPartnerAddress.Text = "";
	    			txtPartnerEmailID.Text = "";
	    			
	    			ddlPartnerDate.ClearSelection();
	    			ddlPartnerMonth.ClearSelection();
	    			txtPartnerDOBYear.Text = "";
	    			
	    			ddlPartnerGender.ClearSelection();
	    			ddlPartnerTShirtSize.ClearSelection();
	    
			}
			catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationFormSimple.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
		}
		#endregion Event Details
		
		protected string GetTeamCode()
        {
        	string strTeamCode = "";
        	try{
        	   	TPDAL_Registration   objDALRegistration = new TPDAL_Registration();
            	strTeamCode = 	objDALRegistration.GetTeamCode (strTournamentCode , txtFirstName.Text + " " + txtLastName.Text);
        	}
        	catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationFormSimple.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
                //Response.Redirect("./TP_BD_Home.aspx", false);
            }
            return strTeamCode;
        }
		
        //protected void UploadButton_Click(object sender, EventArgs e)
        //{
        	
        //    if(FileUploadControl.HasFile)
        //    {
        //        try
        //        {
        //            TPDAL_Registration objDALRegistration = new TPDAL_Registration();
										
        //            if(FileUploadControl.PostedFile.ContentType == "image/jpeg" || FileUploadControl.PostedFile.ContentType == "image/png")
        //            {
        //                if(FileUploadControl.PostedFile.ContentLength < 10240000)
        //                {
        //                    string filename = Path.GetFileName(FileUploadControl.FileName);
        //                    ShowImage();
        //                    //btnRegisterPay_Click (null, null);
        //                    /* 
        //                    string strTeamCode = GetTeamCode();
        //                    if(strTeamCode != "")
        //                    {
        //                      //  byte[] byImage = ConvertImageToByteArray(filename);
        //                       // img.ImageUrl = filename;
        //                        string strEtx = "";
			                    
        //                        strEtx = filename.Split('.')[filename.Split('.').Length-1];
        //                        FileUploadControl.SaveAs(Server.MapPath("../UploadedFile/") + strTeamCode+ "." + strEtx);
        //                        //lblStatusLabel.Text = "Upload status: File uploaded!";
        //                        string strPlayerDOB = ddlDate.SelectedItem.Text +"/"+ ddlMonth.SelectedValue +"/"+ txtDOBYear.Text;
	        
        //                        string strPlayerCode = 	objDALRegistration.GetPlayerCode (strTournamentCode , txtFirstName.Text , txtLastName.Text , strPlayerDOB);
		
        //                        SendMailForConfirmation(strPlayerCode);
        //                        RedirectToNextPage(strPlayerCode);
			                    
        //                    }*/
        		    	
        //                }
        //                else
        //                {
        //                    //lblStatusLabel.Text = "Upload status: The file has to be less than 100 kb!";
		                    
        //                    pnlErrorMsg.Visible = true;
        //                    lblErrorMsg.Text = "Upload status: The file has to be less than 100 kb!";
    		
        //                }
        //            }
        //            else
        //            {
        //            //    lblStatusLabel.Text = "Upload status: Only JPEG/PNG files are accepted!";
        //                pnlErrorMsg.Visible = true;
        //                lblErrorMsg.Text = "Upload status: Only JPEG/PNG files are accepted!";
		
        //            }
        //        }
        //        catch(Exception ex)
        //        {
        //            pnlErrorMsg.Visible = true;
        //            lblErrorMsg.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
    		
        //            //lblStatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
        //        }
        //    }
        //    else
        //    {
        //        pnlErrorMsg.Visible = true;
        //        lblErrorMsg.Text = "Please select ID proof for registration to be completed";
        //    }
        //}
			
        //protected void ShowImage()
        //{
        //    FileUpload fileup = null;
        //    System.Web.UI.WebControls.Image img = null;
        //    HttpPostedFile File = null;
        //    fileup = (FileUpload)FileUploadControl;
        //    if (fileup.HasFile && fileup.PostedFile != null)
        //    {
        //        //To create a PostedFile
        //        File = fileup.PostedFile;
        //    }
        //    img = imgProof;
            

        //    if (fileup != null)
        //    {
        //        try
        //        {

        //            Byte[] imgByte = null;
        //            if (fileup.HasFile && fileup.PostedFile != null)
        //            {
        //                if (File.ContentLength < const_FileSize)
        //                {
        //                    imgByte = new Byte[File.ContentLength];
        //                    //force the control to load data in array
        //                    File.InputStream.Read(imgByte, 0, File.ContentLength);

        //                    string base64String = Convert.ToBase64String(imgByte, 0, imgByte.Length);
        //                    imgProof.ImageUrl = "data:image/jpg;base64," + base64String;

        //                    imgProof.Visible = true;
                           
        //                    //MemoryStream ms;
        //                    //System.Drawing.Image image;
        //                    //string strDestFile = "";
            
        //                  //  ms = new MemoryStream(imgByte);
        //                  //  image = System.Drawing.Image.FromStream(ms);
        //                    //strDestFile = Server.MapPath("/Images/Products/Item_" + iItemID.ToString() + "_" + iImageCnt.ToString() + ".jpg");
        //                    //image.Save(strDestFile);
        //                  //  btnDeleteObj.Enabled = true;
                            
        //                }
        //                else
        //                {
        //                    //Show error in UI that file size is mor ethat 150KB
                            
        //                    pnlErrorMsg.Visible = true;
        //                    lblErrorMsg.Text = "Image size is more , please file size less than 400KB";
        //                }
        //            }



        //        }
        //        catch (Exception ex)
        //        {
        //            System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
        //            MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationFormSimple.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
        //            MGError objLogError = new MGError();
        //            objLogError.logError(objError);
        //        }
        //    }
        //}
		
		#region Obsolete Methods
		
		private void PopulateEvents ()
        {
        	try
        	{
	        	//Get all the player list from database
	        	String strEvents = (new TPDAL_Events()).GetAllEventsBySportANDTournament(strSportCode, strTournamentCode);
				
				String[] lstObj = strEvents.Split(new []{"; "}, StringSplitOptions.None);
	        	int iEventCount = lstObj.Length - 1;
	        	
	        	List<TPEvent> lstEventObj = new List<TPEvent>();
	        	TPEvent obj = null;
				
	        	String strEventName = "";
				
	            if (lstObj != null)   
	            {   
	            	if (iEventCount > 0)
	                {
	                    for (int i = 0; i < iEventCount; i++)						                    
	                    {   
	                    	strEventName = lstObj[i];
	                    	
	                    	obj = new TPEvent();
	                    	obj.EventName = strEventName;
	                    	obj.EventCode = strEventName;
	                    	lstEventObj.Add(obj);
	                    }   	                       
	                }
	            }
				
				//ddlEvents.DataSource = lstEventObj;
				//ddlEvents.DataTextField = "EventName";
        		//ddlEvents.DataValueField = "EventCode";
				//ddlEvents.DataBind();	            
        	}
        	
        	
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationFormSimple.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        }
		
		
	    
                
        private bool CheckDuplicateEntry (string strEventName)
        {
        	bool IsDuplicate = false;
        	
        	DataTable dt =  (DataTable)Session["PLAYERPARTICIPATION"];
        	
        	int iRowCount = dt.Rows.Count;
        	for (int i = 0; i < iRowCount; i++)
        	{        		
        		string val = dt.Rows[i]["Event"].ToString();
        		if (val.Equals(strEventName))
        		{
        			IsDuplicate = true;
        			break;
        		}        		
        	}
        	
        	return IsDuplicate;
        }
        
        protected void dgEventParticipation_DeleteCommand(object source, DataGridCommandEventArgs e)
        {
        	int iRowIndex = e.Item.ItemIndex;
        	
        	DataTable dt =  (DataTable)Session["PLAYERPARTICIPATION"];
        	DataRow dr = dt.Rows[iRowIndex];
    	    dt.Rows.Remove(dr);
    	    
        	dgEventParticipation.DataSource = dt;
			dgEventParticipation.DataBind();
        }
        
        
        
        #endregion Obsolete Methods
        
        #region Summary
        
        private void PopulateSummary()
        {
        	lblPlayerName.Text = txtFirstName.Text + " " + txtLastName.Text ;
        	lblPlayerGender.Text = ddlGender.SelectedItem.Text;
        	//lblPlayerDOB.Text = ddlDate.SelectedItem.Text + "/" + ddlMonth.SelectedItem.Text + "/" + ddlYear.SelectedItem.Text;
			lblPlayerDOB.Text = ddlDate.SelectedItem.Text + "/" + ddlMonth.SelectedItem.Text + "/" + txtDOBYear.Text;
        	lblPlayerContact.Text = txtContact.Text;
        	lblPlayerEmailID.Text = txtEmailID.Text;
        	        	
        	try
        	{ 
				//string strPlayerCode = "BD80250"; 
	        	//Get all the player list from database
	        	//List<TPPlayerParticipation> lstObj = new TPDAL_PlayerDetails().GetPlayerParticipationDetails(strTournamentCode, strPlayerCode);
				lstEvent = (List<TPEventListItem>) Session["EventList"];
		    			
	        	
	        	if (lstEvent.Count > 0)
	        	{
	        		
	        		/*
	        		 
	        		 <asp:BoundColumn HeaderText="Category" DataField="EventCode" DataFormatString="{0:MMM-d-yyy}" ItemStyle-Width="10%"></asp:BoundColumn>
							<asp:BoundColumn HeaderText="Fees" DataField="EventRateCard" DataFormatString="{0:MMM-d-yyy}" ItemStyle-Width="10%"></asp:BoundColumn>
							<asp:BoundColumn HeaderText="Partner Name" DataField="PartnerFullName" ItemStyle-Width="20%"></asp:BoundColumn>
							<asp:BoundColumn HeaderText="Partner DOB" DataField="ParterDOB" ItemStyle-Width="20%"></asp:BoundColumn>
	        		 */
	        		DataTable dt = new DataTable();
	        		dt.Columns.Add("EventCode",  typeof(String));
	        		dt.Columns.Add("EventRateCard", typeof(String));
	        		dt.Columns.Add("PartnerFullName" , typeof(String));
	        		dt.Columns.Add("ParterDOB", typeof(String));
	        		for (int i = 0; i < lstEvent.Count ; i++)
	        		{
	        			DataRow dr = dt.NewRow();
	        			dr["EventCode"] = lstEvent[i].EventCode;
	        			dr["EventRateCard"] = lstEvent[i].EventFee.ToString();
	        			if(lstEvent[i].PartnerDetails != null)
	        			{
		        			dr["PartnerFullName"] = lstEvent[i].PartnerDetails.PlayerFullName;
		        			dr["ParterDOB"] = lstEvent[i].PartnerDetails.PlayerDOB;
		        		}
	        			dt.Rows.Add(dr);
	        		}
	        		
	        		dgPlayerParticilation.DataSource = dt;
					dgPlayerParticilation.DataBind();
					
					if (chkbDistrictReg.Checked)
					{
						lblDistricRegistration.Text = "Selected";
					}
					else
					{
						lblDistricRegistration.Text = "Not Selected";
					}
					
					lblParticipationAmount.Text = "Total Amount Payable: Rs. " + lblTotalAmount.Text;
					//lblEventMsg.Text = "";
					btnRegisterPay.Enabled = true;
	        	}
	        	else
	        	{
	        		//lblEventMsg.Text = "No Event Selected, Please Select atleast One Event to Proceed";
	        		btnRegisterPay.Enabled = false;
	        		lblParticipationAmount.Text = "Rs. 0";
	        		lblDistricRegistration.Text = "Not Selected";
	        		dgPlayerParticilation.DataSource = null;
					dgPlayerParticilation.DataBind();
	        	}	        	
        	}
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationFormSimple.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        }
        
        #endregion
        
        #region Registration & Pay
        private string ValidateEmail(string strEmailTemp)
        {
        	string strFinalEmail = "";
        	strFinalEmail = strEmailTemp;
        	try {
        	
	        	if(strFinalEmail.Contains(" "))
	        	{
	        		//remove extra charactor
	        		if(strFinalEmail.Contains("@"))
	        		{
	        			string [] strEmailList = strFinalEmail.Split(' ');
	        			for (int index = 0 ; index < strEmailList.Length ; index++)
	        			{
	        				if(strEmailList[index].Contains("@"))
	        				{
	        					strFinalEmail = strEmailList[index];
	        					break;
	        				}
	        			}
	        		}
	        	}
        	}
        	catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationFormSimple.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
            }
        	
        	return strFinalEmail;
        	
        }
        
        private bool UIValidationforPlayerDetails ()
        {
        	bool bFlag = true;
        	
        	try {
        	
        	String strFirstName = txtFirstName.Text.Trim();
        	String strLastName = txtLastName.Text.Trim();
        	String strContact = txtContact.Text.Trim();
        	String strEMailID = ValidateEmail(txtEmailID.Text.Trim());
        	String strAddress = txtAddress.Text.Trim();
        	//String strSelectedGender = ddlGender.SelectedItem.Text;
        	        	        	
        	//string strDOB = ddlDate.SelectedItem.Text +"/"+ ddlMonth.SelectedValue +"/"+ txtDOBYear.Text;
	       	
        	//DateTime dt;
			//bool valid = DateTime.TryParseExact(strDOB, "dd/MM/yyyy", null, DateTimeStyles.None, out dt);
			 				
        	//strEMailID = ValidateEmail(strEMailID);
        	
	        	if (string.IsNullOrEmpty(strFirstName) ||
	                string.IsNullOrEmpty(strLastName) ||
	                string.IsNullOrEmpty(strContact) ||
	                string.IsNullOrEmpty(strEMailID) ||
	                //string.IsNullOrEmpty(txtBirthdate.Text) ||
	                string.IsNullOrEmpty(strAddress))
	            {
	            	//Show error message
					pnlErrorMsg.Visible = true;
	            	lblErrMsgPlayerDetails.Text = "Complete Player Details Form"; 
					pnlPlayerDetails.Visible = false; 
					btnTogglePlayerDetailsPlus.Text = "+";
									
	            	
	            	bFlag = false;
	            }
	        	//else 
	        	//{
	        		//if (!valid)
	        		//{
						//bFlag = false;
						//Show error message
						//pnlErrorMsg.Visible = true;
	                	//lblErrMsgPlayerDetails.Text = "Please select valid Date of birth for Player";  
			        	//pnlPlayerDetails.Visible = false; 
						//btnTogglePlayerDetailsPlus.Text = "+";							
						
						//bFlag = false;
					//}
	        	//}
        	}
			catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationFormSimple.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
            }
        	return bFlag;
        }
        
        private void SendRegistrationEmail (string strPlayerCode)
        {
        	try
        	{
	        	StringBuilder sb = new StringBuilder();
	        	
	        	string strToEmailID = ValidateEmail(txtEmailID.Text);

                string strMyEmailSubject = lblTournamentName.Text;
	        	string strEmailSubject = new MGCommon.EMailTemplate().MailSubjectLine(strMyEmailSubject);
	        		
		        List<TPEvent> lstObjTPEvent = (List<TPEvent>)Session["PARTICIPATIONEVENTS"];
		        
		        if (lstObjTPEvent != null)
		        {
		        	for(int i = 0; i < lstObjTPEvent.Count; i++)
		        	{
		        		sb.AppendLine(i+1 + "." + "Category: " + lstObjTPEvent[i].EventName);
		        	}
		        }
	        	
		        string strParticipationEvents = sb.ToString();
	        	
	        	string strHeader = new MGCommon.EMailTemplate().EMailHeader();
	        	
	        	//string strPlayerDOB = ddlDate.SelectedItem.Text +"/"+ ddlMonth.SelectedValue +"/"+ ddlYear.SelectedItem.Text;
	        	string strPlayerDOB = ddlDate.SelectedItem.Text +"/"+ ddlMonth.SelectedValue +"/"+ txtDOBYear.Text;
	        	
	        	string strBody = new MGCommon.EMailTemplate().TournamentRegistrationMailBody(txtFirstName.Text, txtLastName.Text,
	        	                                                                             txtContact.Text, strParticipationEvents, 
	        	                                                                             lblTotalAmount.Text, strTournamentCode, strPlayerCode,strPlayerDOB, "Pending" );
	        	string strFooter = new MGCommon.EMailTemplate().EMailFooter();
	        	
	        	string strEmailBody = strBody + strFooter;
	        	
	        	MGCommon.MGGeneric.MGEMailServices(strToEmailID, strEmailSubject, strEmailBody);
        	}
            catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationFormSimple.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
                //Response.Redirect("./TP_BD_Home.aspx", false);
            }
        }
        
        protected void chkbDistrictReg_CheckedChanged(object sender, EventArgs e)
        {
        	try
        	{
        		int iTotalAmount = 0;
        		if(lblTotalAmount.Text == "")
        			iTotalAmount = 0;
        		else
	        		iTotalAmount = int.Parse(lblTotalAmount.Text);
	        	
        		if (chkbDistrictReg.Checked)
	        	{
	        		//if (iTotalAmount > 0)
	        		{
	        			iTotalAmount = iTotalAmount + 30;
	        			lblTotalAmount.Text = iTotalAmount.ToString();
	        		}
	        	}
	        	else
	        	{
	        		iTotalAmount = iTotalAmount - 30;
	        		lblTotalAmount.Text = iTotalAmount.ToString();
	        	}
        	}
            catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationFormSimple.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
            }
        }
        
        protected bool UIValidatePartnerPlayerDetails()
        {
        	
        		bool bFlag = true;
        		string strEtName = "";
    			string strPartnFnm = "";
    			string strPartnLnm = "";
    			string strPartnDOB = "";
    			//int iRowCount = 0;
    		try
            {
    			//iRowCount = dgEventParticipation.Items.Count;
    			lstEvent = (List<TPEventListItem>) Session["EventList"];
								
    		//Get Player Participation Details in multiple events
			for(int i=0; i < lstEvent.Count; i++)
			{
        		strEtName = "";
    			strPartnFnm = "";
    			strPartnLnm = "";
    			strPartnDOB = "";

				//CheckBox cb1 = (CheckBox)dgEventParticipation.Items[i].FindControl("dgcbEventSelect");
				//if (cb1.Checked)
				//{
					
				strEtName = lstEvent[i].EventCode;
				if(strEtName.Contains("D"))
    			{
					if(lstEvent[i].PartnerDetails == null)
					{
						pnlErrorMsg.Visible = true;
			            lblErrMsgPAddEvents.Text = "Complete Partner Details Form";                	
			            	
			            bFlag = false;
					}
					else
					{
						string strPartnerName = lstEvent[i].PartnerDetails.PlayerFullName;
					
	    				strPartnDOB = lstEvent[i].PartnerDetails.PlayerDOB;
	    				DateTime dt;
	    				bool valid = DateTime.TryParseExact(strPartnDOB, "dd/MM/yyyy", null, DateTimeStyles.None, out dt);
	    					
	    				if (!string.IsNullOrEmpty(strPartnerName))
		    			{
                           strPartnerName = strPartnerName.Replace("  ", " ");
		        			String [] strPartnerNames = strPartnerName.Split (' ');
		        				
		        			if (!string.IsNullOrEmpty(strPartnerNames[0]))
		        			{
			        			strPartnFnm =  strPartnerNames[0];		        			
		        			}
		        			if (!string.IsNullOrEmpty(strPartnerNames[1]))
		        			{
			        			strPartnLnm = strPartnerNames[1];
		        			}
		        				
		        			
		    			}

	 					if (string.IsNullOrEmpty(strPartnFnm) ||
			                string.IsNullOrEmpty(strPartnLnm) ||
			                valid == false )
			            {
			            	//Show error message
							pnlErrorMsg.Visible = true;
			            	lblErrMsgPAddEvents.Text = "Complete Partner Details Form";                	
			            	
			            	bFlag = false;
			            }
		    			else
		    			{
			        		
		    				if (!valid)
							{	
								//Show error message
								pnlErrorMsg.Visible = true;
			                	lblErrMsgPAddEvents.Text = "Partner's Birthdate format is incorrect. Please enter value in dd/mm/yyyy format"; 
								bFlag = false;
							}
							else{
				  				string strGender = ddlGender.SelectedValue;
					        	CultureInfo provider = CultureInfo.InvariantCulture;
				
				
					        	//Reference Date for Tournament Age Limit
					        	DateTime dtReferenceAgeDate = new DateTime(2018,01,01);
                                if (strEtName.Length > 2)
                                {
                                    DateTime dtDOB = DateTime.ParseExact(strPartnDOB, "dd/MM/yyyy", provider);
                                    int iAgeLimit = dtReferenceAgeDate.Year - dtDOB.Year;//difference between dtDOB - dtReferenceAgeDate;
                                    string strEventAge = strEtName.Substring(strEtName.Length - 2, 2);

                                    if (iAgeLimit > int.Parse(strEventAge))
                                    {
                                        //Show error message
                                        pnlErrorMsg.Visible = true;
                                        lblErrMsgPAddEvents.Text = "Partner's Birthdate is more then Age limit permitted";
                                        bFlag = false;
                                    }
                                }
							}
						}
					}
        		}
			}
    		}
    		catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationFormSimple.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
                //Response.Redirect("./TP_BD_Home.aspx", false);
            }
    		
			return bFlag;
        }
        
        private List<TPEvent> GetEventDetails()
        {
        	List<TPEvent> lstObjTPEvent = null;
        	
        	try
        	{
        		dtPlayerParticipation = (DataTable)Session["PLAYERPARTICIPATION"];
			        			
    			string strEtName = "";
    			string strFees = "";
    			//string strPartnName = "";
    			string strPartnFnm = "";
    			string strPartnLnm = "";    			
    			string strPartnDOB = "";
    			
    			lstObjTPEvent = new List<TPEvent>();
    			
    			TPEvent objTPEvent = null;
    			
    			if (dtPlayerParticipation != null && dtPlayerParticipation.Rows.Count > 0)
    			{
	        			//Get Player Participation Details in multiple events
        			for(int i=0; i < dtPlayerParticipation.Rows.Count; i++)
        			{
        				CheckBox cb1 = (CheckBox)dgEventParticipation.Items[i].FindControl("dgcbEventSelect");
	    				if (cb1.Checked)
	    				{
	        				strEtName = dtPlayerParticipation.Rows[i][2].ToString();
	        				strFees = dtPlayerParticipation.Rows[i][3].ToString();
	        				
	        				TextBox tbPartnName = (TextBox)dgEventParticipation.Items[i].FindControl("dgtbPartnerName");
	        				//TextBox tbPartnerDOB = (TextBox)dgEventParticipation.Items[i].FindControl("dgtbPartnerDOB");
	        				Button tbPartnerDOB = (Button)dgEventParticipation.Items[i].FindControl("dgtbAddPartner");
	        				
	        				if (!string.IsNullOrEmpty(tbPartnName.Text))
	        				{
		        				String [] strPartnerNames = tbPartnName.Text.Split (' ');
		        				
		        				
		        				if (!string.IsNullOrEmpty(strPartnerNames[0]))
		        				{
			        				strPartnFnm =  strPartnerNames[0];		        			
		        				}
		        				if (!string.IsNullOrEmpty(strPartnerNames[1]))
		        				{
			        				strPartnLnm = strPartnerNames[1];
		        				}
		        				
		        				strPartnDOB = tbPartnerDOB.Text;
	        				}
	        				
	        				objTPEvent = new TPEvent();
	        				//objTPEvent.EventCode = "TP1";
	        				objTPEvent.EventName = strEtName;
	        				objTPEvent.EventRateCard = strFees;
	        				objTPEvent.PartnerCode = "";
	        				objTPEvent.ParterFName = strPartnFnm;
	        				objTPEvent.ParterLName = strPartnLnm;
	        				objTPEvent.PartnerFullName = strPartnFnm + " " + strPartnLnm;
	        				objTPEvent.ParterDOB = strPartnDOB;
	        				
        					lstObjTPEvent.Add (objTPEvent);		
        					strPartnFnm = strPartnLnm = strFees = strEtName = strPartnDOB = "";
        					
    					}
    				}
    			}
        	}        	
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationFormSimple.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        	
        	return lstObjTPEvent;
        }
        
        protected TPPlayer PopulatePlayer()
        {

        	TPPlayer objPlayer = new TPPlayer();
        	try{
        		objPlayer.PlayerFName = txtFirstName.Text;
            	objPlayer.PlayerLName = txtLastName.Text;
            	//objPlayer.PlayerDOB = ddlDate.SelectedItem.Text + "/" + ddlMonth.SelectedValue + "/" + ddlYear.SelectedItem.Text;
    			objPlayer.PlayerDOB = ddlDate.SelectedItem.Text + "/" + ddlMonth.SelectedValue + "/" + txtDOBYear.Text;
    			
            	objPlayer.PlayerContact =  txtContact.Text;
    			//objPlayer.PlayerEmailID =  txtEmailID.Text;        			
    			objPlayer.PlayerEmailID = ValidateEmail(txtEmailID.Text);
					
    			objPlayer.PlayerGender =  ddlGender.SelectedItem.Text;
    			objPlayer.PlayerState = ddlStates.SelectedItem.Text;
    			objPlayer.PlayerDistrict = ddlDistricts.SelectedItem.Text;
    			objPlayer.PlayerCity = txtCity.Text;
	        	objPlayer.PlayerAddress = txtAddress.Text;
           	}
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationFormSimple.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}    	
	        	return objPlayer;
        }
        
        private List<TPEvent> GetEventList()
        {
        	List<TPEvent> lstObjTPEvent = new List<TPEvent>();
			try
			{
	        	string strEtName = "";
				string strFees = "";
				//string strPartnName = "";
				string strPartnFnm = "";
				string strPartnLnm = "";
				string strPartnDOB = "";
			
				lstEvent = (List<TPEventListItem>) Session["EventList"];
				TPEvent objTPEvent = null;
	
				if (lstEvent != null && lstEvent.Count > 0)
				{
		    		//Get Player Participation Details in multiple events
					for(int i=0; i < lstEvent.Count; i++)
					{
						
						strEtName = lstEvent[i].EventCode;
						strFees = lstEvent[i].EventFee.ToString();
						string strPartnerName = "";
						if(lstEvent[i].PartnerDetails != null)
						{
							strPartnerName = lstEvent[i].PartnerDetails.PlayerFullName;
						
						
		    				if (!string.IsNullOrEmpty(strPartnerName))
		    				{
		        				String [] strPartnerNames = strPartnerName.Split (' ');
		        				
		        				
		        				if (!string.IsNullOrEmpty(strPartnerNames[0]))
		        				{
			        				strPartnFnm =  strPartnerNames[0];		        			
		        				}
		        				if (!string.IsNullOrEmpty(strPartnerNames[1]))
		        				{
			        				strPartnLnm = strPartnerNames[1];
		        				}
		        				
		        				strPartnDOB = lstEvent[i].PartnerDetails.PlayerDOB;
		    				}
						}
						objTPEvent = new TPEvent();					        				
						objTPEvent.EventName = strEtName;
						objTPEvent.EventRateCard = strFees;
						objTPEvent.PartnerCode = "";
						objTPEvent.ParterFName = strPartnFnm;
						objTPEvent.ParterLName = strPartnLnm;
						objTPEvent.ParterDOB = strPartnDOB;
						
						lstObjTPEvent.Add (objTPEvent);		
						strPartnFnm = strPartnLnm = strFees = strEtName = strPartnDOB = "";
						
						
					} 
				}
				Session["PARTICIPATIONEVENTS"] = lstObjTPEvent;			        			
			}
			catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationFormSimple.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
			return lstObjTPEvent;
        }
        	
        protected int GetTotalAmountDue(TPTournamentRegistration objRegistration )
        {
        	int iTotal = 0;
        	try
        	{
        			for(int irow = 0; irow <  objRegistration.RegisteredEvents.Count ; irow++)
					{	
        				try
        				{
							iTotal += int.Parse (objRegistration.RegisteredEvents[irow].EventRateCard);
						}
						 catch (Exception ex)
						{
								System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                				MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationFormSimple.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                				MGError objLogError = new MGError();
                				objLogError.logError(objError);
				                
				                //Response.Redirect("./TP_BD_Home.aspx", false);
															   
						 }
					}
					
					if(chkbDistrictReg.Checked )
						iTotal = iTotal + 30;
							
									
        	}
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationFormSimple.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        	return iTotal;
        }
        
        protected void btnRegisterPay_Click(object sender, EventArgs e)
        {
            try
            {
            	//btnRegisterPay.Enabled = false;
            	List<TPEvent> lstObjTPEvent = new List<TPEvent>();
				
            	if (UIValidationforPlayerDetails())
                {
                	
                	//DateTime dt;
					//bool valid = DateTime.TryParseExact(txtBirthdate.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out dt);
										
					//if (!valid)
					//{	
						//Show error message
						//pnlErrorMsg.Visible = true;
	                	//lblErrorMsg.Text = "Birthdate format is incorrect. Please enter value in dd/mm/yyyy format"; 
					//}
					//else
					{
						bool bFlag = UIValidatePartnerPlayerDetails();
						if (bFlag ==true)
						{
							if (int.Parse(lblTotalAmount.Text) >= 0)
							{
									TPPlayer objPlayer = PopulatePlayer();
				                			
				        			dtPlayerParticipation = (DataTable)Session["PLAYERPARTICIPATION"];
				        			lstObjTPEvent = GetEventList();
				        	
									TPTournamentRegistration objRegistration = new TPTournamentRegistration();
		                	
				        			objRegistration.SportCode = strSportCode;
				        			objRegistration.TournamentCode = strTournamentCode;
				        			objRegistration.RegisteredEvents = lstObjTPEvent;		         			 			         			 	
		         			 	
			         			 	if (!string.IsNullOrEmpty(strTournamentCode))
			         			 	{
						
										//check for duplocate write
										TPDAL_Registration objDALRegistration = new TPDAL_Registration();
										TPPlayerDuplicateCheck objCheckDuplicate = objDALRegistration.PlayerRegisterationCheckDuplicate(objPlayer, objRegistration, dtPlayerParticipation, int.Parse(lblTotalAmount.Text));
										string strDuplicateMessage = "Even(s) ";
										//bool blDuplicate = false;
										
										if(objCheckDuplicate !=null)
										{
											for (int index = objCheckDuplicate.EventList.Count -1; index >=0 ; index--)
											{
												if (objCheckDuplicate.EventList[index].isAlreadyPresent == true)
												{
													strDuplicateMessage += " : " +objCheckDuplicate.EventList[index].EventCode ;
													//blDuplicate = true;
													//remove from objRegistration
													objRegistration.RegisteredEvents.RemoveAt(index);
													lstEvent.Remove( lstEvent.Find(x => x.EventCode ==  objCheckDuplicate.EventList[index].EventCode ));
												}
											}
										}
										
										int iTotal = GetTotalAmountDue(objRegistration);
										
										lblTotalAmount.Text = iTotal.ToString();
										
										if(objRegistration.RegisteredEvents.Count ==0)
										{
											strDuplicateMessage = "Registeration for all events are already done";
											lblErrMsgPAddEvents.Text = strDuplicateMessage;	
											lblErrorMsg.Visible=true;
											
											//string strPlayerDOB = ddlDate.SelectedItem.Text + "/" + ddlMonth.SelectedValue + "/" + ddlYear.SelectedItem.Text;
											string strPlayerDOB = ddlDate.SelectedItem.Text + "/" + ddlMonth.SelectedValue + "/" + txtDOBYear.Text;
											
											string strPlayerCode = 	objDALRegistration.GetPlayerCode (strTournamentCode , txtFirstName.Text , txtLastName.Text , strPlayerDOB);
									
											TPSessionPlayer objSessionPlayer = new TP.Entity.TPSessionPlayer();
					         			 	objSessionPlayer.Address = txtCity.Text +":" + ddlStates.Text + ":"+ txtAddress.Text;
					         			 	objSessionPlayer.DOB	= strPlayerDOB;
					         			 	objSessionPlayer.TournamentCode = strTournamentCode;
					         			 	//objSessionPlayer.Email = txtEmailID.Text;
					         			 	objSessionPlayer.Email = ValidateEmail(txtEmailID.Text);
					         			 	objSessionPlayer.FullName = txtFirstName.Text + " " +txtLastName.Text;
					         			 	objSessionPlayer.Mobile = txtContact.Text;
					         			 	objSessionPlayer.PlayerCode = strPlayerCode;
					         			 	objSessionPlayer.TotalAmount = lblTotalAmount.Text;
					         				Session["PlayerForPayment"] = objSessionPlayer;
			         			 		 					                    	
											string strPath = HttpContext.Current.Request.Url.AbsoluteUri;
											string strURL ="";
			                				string strSourceURL = "TP_RegistrationFormSimple.aspx?TCode="+strTournamentCode;											
											if(strPath.ToUpper().Contains("SCREENS"))												
												strURL = strPath.ToUpper().Replace( strSourceURL.ToUpper(), "TP_PaymentPlayersParticipation.aspx?TCode=" + strTournamentCode +"&PlayerCode=" + strPlayerCode);
											else
												strURL = strPath.ToUpper().Replace(strSourceURL.ToUpper(),"./screens/TP_PaymentPlayersParticipation.aspx?TCode=" + strTournamentCode +"&PlayerCode=" + strPlayerCode);
			                				Response.Redirect(strURL, false);
			                				
										}
										else
										{
											if(strDuplicateMessage != "Even(s) ")
												
											{
												//alert error message
											}
											//check for duplicate end
				         			 		objDALRegistration = new TPDAL_Registration();
				         			 		
				         			 		
			         			 			
				         			 		//objPlayer.TShirtSize = ddlTShirtSize.SelectedItem.Text;
				         			 		
				         			 		//int iFlag = objDALRegistration.PlayerRegisteration(objPlayer, objRegistration, dtPlayerParticipation, int.Parse(lblTotalAmount.Text));
					                    	int iFlag = objDALRegistration.PlayerRegisterationDetails(objPlayer, strSportCode, strTournamentCode, lstEvent, int.Parse(lblTotalAmount.Text));
					                    
						                    if (iFlag == 0)
						                    {
						                    	//Successful Registration
												//string strTeamCode = GetTeamCode();
					        		    		//if(strTeamCode != "")
					        		    		{
								                  //  byte[] byImage = ConvertImageToByteArray(filename);
								                   // img.ImageUrl = filename;
								                    //string strEtx = "";
								                    
								                 //   strEtx = filename.Split('.')[filename.Split('.').Length-1];
								                //    FileUploadControl.SaveAs(Server.MapPath("../UploadedFile/") + strTeamCode+ "." + strEtx);
								                    //lblStatusLabel.Text = "Upload status: File uploaded!";
								                    string strPlayerDOB = ddlDate.SelectedItem.Text +"/"+ ddlMonth.SelectedValue +"/"+ txtDOBYear.Text;
						        
								                    string strPlayerCode = 	objDALRegistration.GetPlayerCode (strTournamentCode , txtFirstName.Text , txtLastName.Text , strPlayerDOB);
								                    if(strPlayerCode != "")
								                    {
								                    /*	 if(imgProof.ImageUrl != "")
										                 {
										                    //lblStatusLabel.Text = "Upload status: File uploaded!";
										              		//byte[] imagebyt = new byte[ const_FileSize];
			                								byte[] imagebyt = Convert.FromBase64String(imgProof.ImageUrl.Replace("data:image/jpg;base64,", ""));
		
										              		//imagebyt = imgProof.ImageUrl;
			                								//imagebyt = File.ReadAllBytes(imgProof.ImageUrl);
			                								//imgProo
			                								(new TPDAL_Registration()).SaveImagetoDB(strTournamentCode, strPlayerCode, imagebyt);
										                 }
														*/
								                  	  															
														RedirectToNextPage(strPlayerCode);

                                                        try
                                                        {
                                                            //Email Send
                                                            SendMailForConfirmation(strPlayerCode);

                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                                                            MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationFormSimple.aspx", currentMethod.Name, "Error in Send Email: " + ex.Message, DateTime.Now, "ip", "");
                                                            MGError objLogError = new MGError();
                                                            objLogError.logError(objError);

                                                            //Response.Redirect("./TP_BD_Home.aspx", false);
                                                        }

                                                        try
                                                        {
                                                            //SMS
                                                            string strSMSSwitch = System.Web.Configuration.WebConfigurationManager.AppSettings["SMS_REGISTRATION_CONFIRMATION"];
                                                            if (strSMSSwitch.Equals("ON"))
                                                                SendSMSForConfirmation(strPlayerCode);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                                                            MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationFormSimple.aspx", currentMethod.Name, "Error in Send SMS: " + ex.Message, DateTime.Now, "ip", "");
                                                            MGError objLogError = new MGError();
                                                            objLogError.logError(objError);

                                                            //Response.Redirect("./TP_BD_Home.aspx", false);
                                                        }
								                    }
					        		    		}						                    	
						        
						                    	//SendMailForConfirmation();
						                     	
						                    }
						                    else // Non zero - unsuccessful
						                    {
						                    	//Unsuccessful registration
						                    	//Show error to user
						                    }
			         			 		}
			         			 	}
			         			 	else
			         			 	{
			         			 		Response.Redirect("./TP_BD_Home.aspx", false);
			         			 	}
			         		
						}
	        			else
	        			{
	        				pnlErrorMsg.Visible = true;
	                		lblErrMsgPAddEvents.Text = "Please select atleast one event to participate";  
	        			}
	        				
						}
						else
						{
							pnlErrorMsg.Visible = true;
	                		lblErrMsgPAddEvents.Text = "Please enter partner Full name and DOB (in dd/mm/yyyy format) , based on Age Category for registration";  
	        			
						}
                    
                	}
            	}
				else
				{
						pnlErrorMsg.Visible = true;
	                	lblErrMsgPlayerDetails.Text = "Please enter player details & select events";  
	        		
				}
            }
            catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationFormSimple.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
                //Response.Redirect("./TP_BD_Home.aspx", false);
            }
        }
        
        private void RedirectToNextPage(string strPlayerCode)
        {
        	try{
        		string strPath = HttpContext.Current.Request.Url.AbsoluteUri;
				string strURL ="";
				string strSourceURL = "TP_RegistrationFormSimple.aspx?TCode="+strTournamentCode;											
				if(strPath.ToUpper().Contains("SCREENS"))												
					strURL = strPath.ToUpper().Replace( strSourceURL.ToUpper(), "TP_PaymentPlayersParticipation.aspx?TCode=" + strTournamentCode +"&PlayerCode=" + strPlayerCode);
				else
					strURL = strPath.ToUpper().Replace(strSourceURL.ToUpper(),"./screens/TP_PaymentPlayersParticipation.aspx?TCode=" + strTournamentCode +"&PlayerCode=" + strPlayerCode);
            				Response.Redirect(strURL, false);
        	}
        	catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationFormSimple.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
                //Response.Redirect("./TP_BD_Home.aspx", false);
            }
        	
        }
        
        private void SendMailForConfirmation(string strPlayerCode)
        {        
        	try        		
        	{	
            	//string strPlayerDOB = ddlDate.SelectedItem.Text + "/" + ddlMonth.SelectedValue + "/" + ddlYear.SelectedItem.Text;
            	string strPlayerDOB = ddlDate.SelectedItem.Text + "/" + ddlMonth.SelectedValue + "/" + txtDOBYear.Text;
            	//string strPlayerCode = 	objDALRegistration.GetPlayerCode (strTournamentCode , txtFirstName.Text , txtLastName.Text , strPlayerDOB);
		
				TPSessionPlayer objSessionPlayer = new TP.Entity.TPSessionPlayer();
 			 	objSessionPlayer.Address = txtCity.Text +":" + ddlStates.Text + ":"+ txtAddress.Text;
 			 	objSessionPlayer.DOB	= strPlayerDOB;
 			 	objSessionPlayer.TournamentCode = strTournamentCode;
 			 	//objSessionPlayer.Email = txtEmailID.Text;
 			 	objSessionPlayer.Email = ValidateEmail(txtEmailID.Text);
 			 	
 			 	objSessionPlayer.FullName = txtFirstName.Text + " " +txtLastName.Text;
 			 	objSessionPlayer.Mobile = txtContact.Text;
 			 	objSessionPlayer.PlayerCode = strPlayerCode;
 			 	objSessionPlayer.TotalAmount = lblTotalAmount.Text;
 				Session["PlayerForPayment"] = objSessionPlayer;
		 		 					                    	
             	//Send emails to players
 				SendRegistrationEmail(strPlayerCode);
        	}
        	catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationFormSimple.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
                //Response.Redirect("./TP_BD_Home.aspx", false);
            }
        }

        private void SendSMSForConfirmation(string strPlayerCode)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                string strToEmailID = ValidateEmail(txtEmailID.Text);

                string strMyEmailSubject = lblTournamentName.Text;
                string strEmailSubject = new MGCommon.EMailTemplate().MailSubjectLine(strMyEmailSubject);

                List<TPEvent> lstObjTPEvent = (List<TPEvent>)Session["PARTICIPATIONEVENTS"];
                //sb.Append("");
                if (lstObjTPEvent != null)
                {
                    for (int i = 0; i < lstObjTPEvent.Count; i++)
                    {
                        sb.Append(lstObjTPEvent[i].EventName);
                        
                        if (i != lstObjTPEvent.Count -1 )
                            sb.Append(",");
                    }
                }

                string strParticipationEvents = sb.ToString();
                
                string strFirstName = txtFirstName.Text;
                string strEmailID =  txtEmailID.Text;
                string strMobileNo = txtContact.Text;
                string strEventCategory = strParticipationEvents;

                (new MGCommon.BulkSMS()).SMS_Registration_Confirmation(strFirstName, strMobileNo, strEventCategory, strEmailID);
                
                //Update DB for SMS Count                
                int iCount = (new TPDAL_SMSTracker()).SMSTracker(strSportCode, strTournamentCode, 1);
            }
            catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RegistrationFormSimple.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);

                //Response.Redirect("./TP_BD_Home.aspx", false);
            }
        }

        #endregion
        
    }
}