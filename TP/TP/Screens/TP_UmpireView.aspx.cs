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
    public partial class TP_UmpireView : System.Web.UI.Page
    {
    	string strSportCode = "";
    	string strTournamentCode = "";
    	
    	TPMatch objMatch = null;
    	//bool IsUmpire = false;
    	
    	#region Page Load
    	
        protected void Page_Load(object sender, EventArgs e)
        {	
        	
        	pnlErrorMsg.Visible = false;
        	
        	
			try
			{
	        	//if (!Session["SPORTCODE"].Equals (null))
	        	//{
		        //	strSportCode = (String)Session["SPORTCODE"];
		        //	strTournamentCode = (String)Session["TOURNAMENTCODE"];        	
	        	//}
	        	//else
	        	//{
	        	//	Response.Redirect("./TP_BD_Home.aspx");
	        	//}
	        		        	
	        	
	        	
	        	
	        	if(IsPostBack)
				{
									
				}
				else
				{
					//Get Round Details
					//GetMatchRounds();
					
					//Get TPMatch details
					//GetMatchDetails ();	
				}
			}
			catch (Exception ex)
			{
				System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_UmpireView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
                
                Response.Redirect("./TP_BD_Home.aspx");
			}
        }
        
        #endregion
        
        #region Login
        
        private List<TPLogin> CheckUserAuthentication (TPLogin obj)
        {
        	List<TPLogin> objList = null;
        	
        	try{
	        	
	        	objList = (new TPDAL_UserAuthenticationAuthorization()).CheckUserAuthentication (obj);	        	        	
	        	
        	}
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_UmpireView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        	
        	return objList;
        }
        
        protected void btnOwnerLogin_Click(object sender, EventArgs e)
        {
        	try
        	{
	        	Session["USERNAME"] = txtTournamentCode.Text;
	        	
	        	string strTournamentCode = txtTournamentCode.Text.Trim();
	        	string strUserCode = txtUserCode.Text.Trim();
	        	string strOTP = txtOTPCode.Text.Trim();
	        	string strCaptcha = txtCaptchaCode.Text.Trim();
	        	TPLogin obj = new TPLogin();
	        	//Check Username & Password
	        	bool bIsUserExists = true; //CheckUserAuthentication (obj);
	        	
	        	if (bIsUserExists == true)
	        	{	
		        	pnlMatchList.Visible = true;
		        	pnlUmpireLogin.Visible = false;
		        	
		        	lblMatchScoreHeading.Text = "Matche List";
		        	
		        	PopulateMatches (strTournamentCode);
	        	}
	        	else
	        	{
	        		pnlErrorMsg.Visible = true;
	        		lblErrorMsg.Text = "User Credentials are not valid";
	        	}
        	}
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_UmpireView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        	
        }        
        #endregion
        
        #region Match List Grid
        /// <summary>
        /// Populate Matches
        /// </summary>
        private void PopulateMatches (string strTournamentCode)
		{
        	try
        	{        		
        		string strEventCode = "";
				
				List<TPMatch> lstObj = (new TPDAL_Match()).GetPendingMatchesByTournamentID(strTournamentCode, strEventCode);
				
				dgMatchList.DataSource = lstObj;
				dgMatchList.DataBind();
				
        	}
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_UmpireView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}			
		}
        
        protected void lbMatchID_Click(object sender, EventArgs e)
        {
        	string strMatchID = "";
        	string strEventCode = "";
        	if (!string.IsNullOrEmpty(Request.QueryString["MatchID"].ToString()))
            {
                strMatchID = Request.QueryString["MatchID"].ToString();	        		
        		Session["MATCHID"] = strMatchID;
        	}
        	if (!string.IsNullOrEmpty(Request.QueryString["EventCode"].ToString()))
            {
                strEventCode = Request.QueryString["EventCode"].ToString();	        		
        		Session["EVENTCODE"] = strEventCode;
        	}
        	
        	pnlMatchList.Visible = false;
        	lblMatchScoreHeading.Text = "Umpire Details";
        	
        	//Get Round Details
			GetMatchRounds(strMatchID);
			
			//Get TPMatch details
			GetMatchDetails (strMatchID);	
        }
        
        
        
        #endregion
        
        #region Get Match / Round Details
        
        private void HideActionObjects ()
        {
        	btnFirstSetStart.Visible = false;
        	btnSecondSetStart.Visible = false;
        	btnThirdSetStart.Visible = false;
        	btnIncrementScoreFirstTeam.Visible = false;
        	lblFirstHypenBeteenIncrementDecrementButton.Visible = false;
			btnDecrementScoreFirstTeam.Visible = false;
			btnIncrementScoreSecondTeam.Visible = false;
			lblSecondHypenBeteenIncrementDecrementButton.Visible = false;
			btnDecrementScoreSecondTeam.Visible = false;
			pnlFinishMatch.Visible = false;
			btnFinishMatch.Visible = false;
        }
              
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<TPMatchRound> GetMatchRounds (string strMatchID)
        {
        	List<TPMatchRound> lstObjMatchRound = null;
        	try
        	{
        		lstObjMatchRound = (new TPDAL_Match()).GetMatchRoundDetails(strMatchID);
				
        		if (lstObjMatchRound != null && lstObjMatchRound.Count > 0)
        		{
	        		if (lstObjMatchRound.Count == 3)
	        		{
	        			//Update Round Status
	        			if (lstObjMatchRound[0].RoundStatus.Equals("DONE"))
	        			{
	        				lblRound1Status.Text = "Completed";	  
							btnFirstSetStart.Visible = false;
							btnSecondSetStart.Visible = true;							
	        			}
	        			else if (lstObjMatchRound[0].RoundStatus.Equals("INPROGRESS"))
	        			{
	        				lblRound1Status.Text = "Running";
	        				btnFirstSetStart.Text = "Done";
	        				btnIncrementScoreFirstTeam.Visible = true;
	        				btnDecrementScoreFirstTeam.Visible = true;
	        				btnIncrementScoreSecondTeam.Visible = true;
	        				btnDecrementScoreSecondTeam.Visible = true;
	        				btnWalkoverFirstTeam.Visible = false;
	        				btnWalkoverSecondTeam.Visible = false;
	        			}
	        			else if (lstObjMatchRound[0].RoundStatus.Equals("NOTSTARTED"))
	        			{
	        				lblRound1Status.Text = "Not Yet Started";
	        				btnIncrementScoreFirstTeam.Visible = false;
	        				btnDecrementScoreFirstTeam.Visible = false;
	        				btnIncrementScoreSecondTeam.Visible = false;
	        				btnDecrementScoreSecondTeam.Visible = false;
	        				btnWalkoverFirstTeam.Visible = true;
	        				btnWalkoverSecondTeam.Visible = true;
	        			}
	        			
	        			
	        			if (lstObjMatchRound[1].RoundStatus.Equals("DONE"))
	        			{
	        				lblRound2Status.Text = "Completed";
							btnSecondSetStart.Visible = false;
							btnThirdSetStart.Visible = true;

							//Check Winner if exists
							int iRoundCountofFirstTeam = (new TPDAL_Match()).RoundCountByFirstTeam(strMatchID);
							if (iRoundCountofFirstTeam > 1)
							{
								pnlFinishMatch.Visible = true;
								btnThirdSetStart.Visible = false;								
							}
							
							int iRoundCountofSecondTeam = (new TPDAL_Match()).RoundCountBySecondTeam(strMatchID);
							if (iRoundCountofSecondTeam > 1)
							{
								pnlFinishMatch.Visible = true;
								btnThirdSetStart.Visible = false;							
							}
	        			}
	        			if (lstObjMatchRound[1].RoundStatus.Equals("INPROGRESS"))
	        			{
	        				lblRound2Status.Text = "Running";
	        				btnSecondSetStart.Text = "Done";
	        			}
	        			if (lstObjMatchRound[1].RoundStatus.Equals("NOTSTARTED"))
	        				lblRound2Status.Text = "Not Yet Started";
	        			
	        			if (lstObjMatchRound[2].RoundStatus.Equals("DONE"))
	        			{
	        				lblRound3Status.Text = "Completed";	        			
	        				btnThirdSetStart.Visible = false;
	        				pnlFinishMatch.Visible = true;
	        			}
	        			if (lstObjMatchRound[2].RoundStatus.Equals("INPROGRESS"))
	        			{
	        				lblRound3Status.Text = "Running";
	        				btnThirdSetStart.Text = "Done";	        				
	        			}
	        			if (lstObjMatchRound[2].RoundStatus.Equals("NOTSTARTED"))
	        				lblRound3Status.Text = "Not Yet Started";
	        			
	        			if (lstObjMatchRound[2].RoundStatus.Equals("WALKOVER"))
	        			{
	        				lblRound3Status.Text = "Not Yet Started";
	        				HideActionObjects ();
	        			}
	        			
	        			//Update Scores
	        			lblFirstPlayerFirstSetScore.Text = lstObjMatchRound[0].FirstTeamScore;
	        			lblSecondPlayerFirstSetScore.Text = lstObjMatchRound[0].SecondTeamScore;
	        			lblFirstPlayerSecondSetScore.Text = lstObjMatchRound[1].FirstTeamScore;
	        			lblSecondPlayerSecondSetScore.Text = lstObjMatchRound[1].SecondTeamScore;
	        			lblFirstPlayerThirdSetScore.Text = lstObjMatchRound[2].FirstTeamScore;
	        			lblSecondPlayerThirdSetScore.Text = lstObjMatchRound[2].SecondTeamScore;
	        			
	        			//Update Start End Time of Set/Round
	        			lblFirstSetStartTime.Text = lstObjMatchRound[0].RoundStartTime;
	        			lblFirstSetEndTime.Text = lstObjMatchRound[0].RoundEndTime;
	        			lblSecondSetStartTime.Text = lstObjMatchRound[1].RoundStartTime;
	        			lblSecondSetEndTime.Text = lstObjMatchRound[1].RoundEndTime;
	        			lblThirdSetStartTime.Text = lstObjMatchRound[2].RoundStartTime;
	        			lblThirdSetEndTime.Text = lstObjMatchRound[2].RoundEndTime;
	        		}
        		}
        	}
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_UmpireView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
				
        	return lstObjMatchRound;
        }
        
        /// <summary>
        /// PopulateTournamentSummary -- The header part
        /// </summary>
        private void GetMatchDetails (string strMatchID)
        {
        	try
        	{
				objMatch = (new TPDAL_Match()).GetMatchDetails(strSportCode, strTournamentCode, strMatchID,"");
				
				lblMatchID.Text = "Match ID: " + objMatch.ID;
				lblUmpireName.Text = objMatch.UmpireName;
				lblMatchLocation.Text = objMatch.CourtDetails;
				lblMatchDuration.Text = objMatch.MatchDuration;
				lblFirstTeamPlayer.Text = objMatch.FirstTeamPlayerName;
				lblSecondTeamPlayer.Text = objMatch.SecondTeamPlayerName;
				lblNoOfSetsPoints.Text = objMatch.MatchPoints + " Points";
								
				//Validation
				//1. If MatchStatus = "NOTSTARTED"
				 	//then Show the Umpire Panel to enter Umpire Details and kickstart the match
				//2. If MatchStatus = "INPROGRESS"
					//then hide Umpire Panel and show the Match Score Panel
				//3. If MatchStatus = "CLOSED"
					//then hide the Umpire Panel and show the Match ScorePanel and hide all the event controls
				string strMatchStatus = objMatch.MatchStatus;
				if (strMatchStatus == "NOTSTARTED" || string.IsNullOrEmpty(strMatchStatus))
				{
					pnlMatchRound.Visible = false;
					
					pnlKickOffMatch.Visible = true;
					
					lblMatchStatus.Text = "Match Status: Not Yet Started ";
					
					//HideActionObjects ();
				}
				if (strMatchStatus == "INPROGRESS")
				{
					pnlKickOffMatch.Visible = false;
					pnlMatchRound.Visible = true;
					
					lblMatchStatus.Text = "Match Status: In Progress ";
					lblMatchScoreHeading.Text = "Match - Score  Live!";
										
				}
				if (strMatchStatus == "DONE" || strMatchStatus == "WALKOVER")
				{
					pnlKickOffMatch.Visible = false;
					pnlMatchRound.Visible = true;
					
					if(strMatchStatus.Equals("DONE"))
						lblMatchStatus.Text = "Match Status: Completed ";
					else
						lblMatchStatus.Text = "Match Status: Walkover ";
					
					//Get the winner
					string strWinnerName = (new TPDAL_Match()).GetWinnerNameByMatchID(strTournamentCode, strMatchID);
					lblWinnerName.Text = "Winner: " + strWinnerName;
					pnlWinnerName.Visible = true;
					
					//If Match DONE then all the transacional event objects should not be visible
					HideActionObjects ();					
				}
				
        	}				
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_UmpireView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        	        
        }
        
        #endregion
        
        #region Match Kckoff
        
        protected void btnKickOffMatch_Click(object sender, EventArgs e)
		{
			try
            {
				strSportCode = (String)Session["SPORTCODE"];
	        	String strTournamentCode = (String)Session["TOURNAMENTCODE"];
	        	string strMatchID = (String) Session["MATCHID"];
	        	
	        	strTournamentCode = txtTournamentCode.Text;
                String strUmpireCode = txtUmpireCode.Text;
	        	String strUmpireName = txtUmpireName.Text;                
                String strMatchLocation = txtMatchLocation.Text;
                String strMatchPoints = ddlMatchPoints.SelectedItem.Text;
                String strOTP = txtOTPCode.Text;
                //String strUserType = "UMPIRE";
                
                if (string.IsNullOrEmpty(strUmpireName) 
              		|| string.IsNullOrEmpty(strUmpireCode)
					|| string.IsNullOrEmpty(strMatchLocation)
					|| strMatchPoints.Equals ("Select Match Points"))
                {
                	pnlErrorMsg.Visible = true;
                	lblErrorMsg.Text = "All fields are mandatory";
                }
                else
                {
                	TPMatch objMatch = new TPMatch();
             
                	objMatch.ID = strMatchID;
                	objMatch.UmpireCode = strUmpireCode;
                	objMatch.UmpireName = lblUmpireName.Text = strUmpireName;
                	objMatch.CourtDetails = lblMatchLocation.Text = strMatchLocation;
                	objMatch.MatchPoints = lblNoOfSetsPoints.Text = strMatchPoints;
                	
					TPDAL_Match objDALMatch = new TPDAL_Match();                        
					objDALMatch.KickOffMatch (objMatch);
					
					pnlKickOffMatch.Visible = false;
                	pnlMatchRound.Visible = true;
                	btnFirstSetStart.Visible = true;
                	lblMatchScoreHeading.Text = "Match Score";
                	                	
                }
            }
            catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_UmpireView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
            }
			
		}
        
        #endregion
        
        #region Match Round
        
        protected void btnWalkoverFirstTeam_Click(object sender, EventArgs e)
		{
			try
	        {
				strSportCode = (String)Session["SPORTCODE"];
	        	strTournamentCode = (String)Session["TOURNAMENTCODE"];
	        	string strMatchID = (String) Session["MATCHID"];
	        	
        	    TPMatchRound objMatchRound = new TPMatchRound();
         
	        	objMatchRound.MatchID = strMatchID;
	        	
    			string strFlag2Update = "Walkover2FirstTeam";
    	
    			UpdateMatchRounds (objMatchRound, strFlag2Update);

    			GetMatchDetails (strMatchID);
            }
            catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_UmpireView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
            }
			
		}
        
        protected void btnWalkoverSecondTeam_Click(object sender, EventArgs e)
		{
			try
	        {
				strSportCode = (String)Session["SPORTCODE"];
	        	strTournamentCode = (String)Session["TOURNAMENTCODE"];
	        	string strMatchID = (String) Session["MATCHID"];
	        	
        	    TPMatchRound objMatchRound = new TPMatchRound();
         
	        	objMatchRound.MatchID = strMatchID;
	        	
    			string strFlag2Update = "Walkover2SecondTeam";
    	
    			UpdateMatchRounds (objMatchRound, strFlag2Update);					
    			
    			GetMatchDetails (strMatchID);
            }
            catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_UmpireView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
            }
			
		}
        
        private void UpdateMatchRounds (TPMatchRound objMatchRound, string strFlag2Update)
        {
        	try
            {
                if (objMatchRound != null)
                {                	
					TPDAL_Match objDALMatch = new TPDAL_Match();                        
					objDALMatch.SetMatchRounds (objMatchRound, strFlag2Update);
                }
				
                GetMatchRounds (objMatchRound.MatchID);
            }
            catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_UmpireView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
            }
        }
        
        protected void btnFirstSetStartEnd_Click(object sender, EventArgs e)
        {
        	//Once the first set started then only +/- button should be enabled else should be disabled
        	//Once the first set is over then only the next set Start button will be enabled else will be disabled
        	string strMatchID = (String) Session["MATCHID"];
        	string strSetCounter = "1";
        	string strRoundStatus = "";
        	string strFlag2Update = "";
        	
        	TPMatchRound objMatchRound = new TPMatchRound();
             
        	objMatchRound.MatchID = strMatchID;
        	objMatchRound.RoundCounter = strSetCounter;
        	
        	
        	if (btnFirstSetStart.Text.Equals("Start"))
        	{
        		//strRoundStatus = "INPROGRESS";        		
        		strFlag2Update = "ROUNDSTARTTIME";
        		
				//btnFirstSetStart.Text = "Done";        		
        	}
        	
        	if (btnFirstSetStart.Text.Equals("Done"))
        	{
        		//strRoundStatus = "DONE";        
				strFlag2Update = "ROUNDENDTIME";
				
        		//btnFirstSetStart.Text = "Resume";
        	}
        	
        	//if (btnFirstSetStart.Text.Equals("Resume"))
        	//{
        	//	strRoundStatus = "RESUME";        		
        	//	btnFirstSetStart.Text = "Done";
        	//}
        	
        	objMatchRound.RoundStatus = strRoundStatus;
        	
        	UpdateMatchRounds (objMatchRound, strFlag2Update);
        	
        	btnFirstSetStart.Text = "Done";
        }
        
        protected void btnSecondSetStartEnd_Click(object sender, EventArgs e)
        {
        	string strMatchID = (String) Session["MATCHID"];
        	string strSetCounter = "2";
        	string strRoundStatus = "";
        	string strFlag2Update = "";
        	
        	TPMatchRound objMatchRound = new TPMatchRound();
             
        	objMatchRound.MatchID = strMatchID;
        	objMatchRound.RoundCounter = strSetCounter;
        	
        	
        	if (btnSecondSetStart.Text.Equals("Start"))
        	{
        		strRoundStatus = "INPROGRESS";        		
        		strFlag2Update = "ROUNDSTARTTIME";
        		
        	}
        	
        	if (btnSecondSetStart.Text.Equals("Done"))
        	{
        		strRoundStatus = "DONE";        
				strFlag2Update = "ROUNDENDTIME";
				
        		//btnFirstSetStart.Text = "Resume";
        	}
        	
        	//if (btnFirstSetStart.Text.Equals("Resume"))
        	//{
        	//	strRoundStatus = "RESUME";        		
        	//	btnFirstSetStart.Text = "Done";
        	//}
        	
        	objMatchRound.RoundStatus = strRoundStatus;
        	
        	UpdateMatchRounds (objMatchRound, strFlag2Update);
        	
        	btnSecondSetStart.Text = "Done";
        }
        
        protected void btnThirdSetStartEnd_Click(object sender, EventArgs e)
        {
        	string strMatchID = (String) Session["MATCHID"];
        	string strSetCounter = "3";
        	string strRoundStatus = "";
        	string strFlag2Update = "";
        	
        	TPMatchRound objMatchRound = new TPMatchRound();
             
        	objMatchRound.MatchID = strMatchID;
        	objMatchRound.RoundCounter = strSetCounter;
        	        	
        	if (btnThirdSetStart.Text.Equals("Start"))
        	{
        		strRoundStatus = "INPROGRESS";        		
        		strFlag2Update = "ROUNDSTARTTIME";        						
        	}
        	
        	if (btnThirdSetStart.Text.Equals("Done"))
        	{
        		strRoundStatus = "DONE";        
				strFlag2Update = "ROUNDENDTIME";
				
        		//btnFirstSetStart.Text = "Resume";
        	}
        	
        	//if (btnFirstSetStart.Text.Equals("Resume"))
        	//{
        	//	strRoundStatus = "RESUME";        		
        	//	btnFirstSetStart.Text = "Done";
        	//}
        	
        	objMatchRound.RoundStatus = strRoundStatus;
        	
        	UpdateMatchRounds (objMatchRound, strFlag2Update);
        	
        	
        	btnThirdSetStart.Text = "Done";
        }
   
        #endregion
        
		#region Increment Decrement points for players
        
		//Increment Decrement points
        protected void btnIncrementScoreFirstTeam_Click(object sender, EventArgs e)
        {
        	string strMatchID = (String) Session["MATCHID"];
        	string strSetCounter = "";        	
        	string strFlag2Update = "INCREMENTFIRSTTEAMSCORE";
        	
        	//Get the current Running Set
        	if (lblRound1Status.Text.Equals ("Running"))
        		strSetCounter = "1";
        	if (lblRound2Status.Text.Equals ("Running"))
        		strSetCounter = "2";
        	if (lblRound3Status.Text.Equals ("Running"))
        		strSetCounter = "3";
        	        	
        	TPMatchRound objMatchRound = new TPMatchRound();
             
        	objMatchRound.MatchID = strMatchID;
        	objMatchRound.RoundCounter = strSetCounter;
        	
        	UpdateMatchRounds (objMatchRound, strFlag2Update);
        }
        
        protected void btnDecrementScoreFirstTeam_Click(object sender, EventArgs e)
        {
        	
        	string strMatchID = (String) Session["MATCHID"];
        	string strSetCounter = "";        	
        	string strFlag2Update = "DECREMENTFIRSTTEAMSCORE";
        	
        	//Get the current Running Set
        	if (lblRound1Status.Text.Equals ("Running"))
        		strSetCounter = "1";
        	if (lblRound2Status.Text.Equals ("Running"))
        		strSetCounter = "2";
        	if (lblRound3Status.Text.Equals ("Running"))
        		strSetCounter = "3";
        	        	
        	TPMatchRound objMatchRound = new TPMatchRound();
             
        	objMatchRound.MatchID = strMatchID;
        	objMatchRound.RoundCounter = strSetCounter;
        	
        	UpdateMatchRounds (objMatchRound, strFlag2Update);
        }
        
        protected void btnIncrementScoreSecondTeam_Click(object sender, EventArgs e)
        {
        	string strMatchID = (String) Session["MATCHID"];
        	string strSetCounter = "";        	
        	string strFlag2Update = "INCREMENTSECONDTEAMSCORE";
        	
        	//Get the current Running Set
        	if (lblRound1Status.Text.Equals ("Running"))
        		strSetCounter = "1";
        	if (lblRound2Status.Text.Equals ("Running"))
        		strSetCounter = "2";
        	if (lblRound3Status.Text.Equals ("Running"))
        		strSetCounter = "3";
        	        	
        	TPMatchRound objMatchRound = new TPMatchRound();
             
        	objMatchRound.MatchID = strMatchID;
        	objMatchRound.RoundCounter = strSetCounter;
        	
        	UpdateMatchRounds (objMatchRound, strFlag2Update);
        }
        
        protected void btnDecrementScoreSecondTeam_Click(object sender, EventArgs e)
        {
        	string strMatchID = (String) Session["MATCHID"];
        	string strSetCounter = "";        	
        	string strFlag2Update = "DECREMENTSECONDTEAMSCORE";
        	
        	//Get the current Running Set
        	if (lblRound1Status.Text.Equals ("Running"))
        		strSetCounter = "1";
        	if (lblRound2Status.Text.Equals ("Running"))
        		strSetCounter = "2";
        	if (lblRound3Status.Text.Equals ("Running"))
        		strSetCounter = "3";
        	        	
        	TPMatchRound objMatchRound = new TPMatchRound();
             
        	objMatchRound.MatchID = strMatchID;
        	objMatchRound.RoundCounter = strSetCounter;
        	
        	UpdateMatchRounds (objMatchRound, strFlag2Update);
        }
        
        #endregion Increment Decrement points for players
        
        #region Finish Match
        
        protected void btnFinishMatch_Click(object sender, EventArgs e)
        {        	
        	try
        	{
	        	string strMatchID = (String) Session["MATCHID"];
	        	string strEventCode = (String) Session["EVENTCODE"];
	        	strTournamentCode = (String) Session["USERNAME"];
	        	strSportCode = "BD";
	        		
	        	//If Match Round done and winner decided
	        	//Calculate the points to decide the winner
	        	//Step 1:
	        		// At least two rounds has been completed
	        	//Step 2:
	        		//Check the rounds	
	        		//
	        	
	        	//Then Finish the match
	        	
	        	//if (two round completed and match winner decided)
	        	{
	            	TPMatch objMatch = new TPMatch();
	         
	            	objMatch.SportCode = strSportCode;
					objMatch.TournamentCode = strTournamentCode; 
					objMatch.EventCode = strEventCode;				
	            	objMatch.ID = strMatchID;
	            	            	                	
					TPDAL_Match objDALMatch = new TPDAL_Match();                        
					objDALMatch.FinishMatch (objMatch);            
	        	}                
	        	        	
	        	GetMatchDetails (strMatchID);
        	
        	}
            catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_UmpireView.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
                Response.Redirect("./TP_BD_Home.aspx", false);
            }
        }
        
        #endregion Finish Match
    }
}