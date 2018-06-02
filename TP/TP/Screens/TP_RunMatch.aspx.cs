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
    public partial class TP_RunMatch : System.Web.UI.Page
    {
        string strSportCode = "";
    	string strTournamentCode = "";
    	string strMatchID = "";
    	TPMatch objMatch = null;
    	
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
	        	
	        	strMatchID = Request.QueryString["MatchID"];
	        	
	        	Session["MATCHID"] = strMatchID;
	        	
	        	if(IsPostBack)
				{
								
				}
				else
				{
					PopulateTournamentSummary ();
					
					//Get Round Details
					GetMatchRounds();
					
					//Get TPMatch details
					GetMatchDetails ();					
				}
			}
			catch (Exception ex)
			{
				System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
	
                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RunMatch.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
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
					lblTournamentVenue.Text = "Venue: "  + lstObj[0].TournamentVenue;
					lblLocationAddress.Text = "Address: " + lstObj[0].TournamentLocationAddress; 
					lblTournamentContacts.Text = "Contacts: " + lstObj[0].TournamentPOCContactNames + " " + lstObj[0].TournamentPOCContactNo + " " + lstObj[0].TournamentLocationContactNo;
				}
        	}
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RunMatch.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        
        }
                
        private void HideObjectsIfMatchDONE ()
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
        }
                
        /// <summary>
        /// PopulateTournamentSummary -- The header part
        /// </summary>
        private void GetMatchDetails ()
        {
        	try
        	{
				objMatch = (new TPDAL_Match()).GetMatchDetails( strSportCode, strTournamentCode, strMatchID,"");//need to update anita
				
				lblMatchID.Text = "Match ID: " + objMatch.ID;
				lblUmpireName.Text = objMatch.UmpireName;
				lblMatchLocation.Text = objMatch.CourtDetails;
				lblMatchDuration.Text = objMatch.MatchDuration;
				lblFirstTeamPlayer.Text = objMatch.FirstTeamPlayerName;
				lblSecondTeamPlayer.Text = objMatch.SecondTeamPlayerName;
								
				//Validation
				//1. If MatchStatus = "NOTSTARTED"
				 	//then Show the Umpire Panel to enter Umpire Details and kickstart the match
				//2. If MatchStatus = "INPROGRESS"
					//then hide Umpire Panel and show the Match Score Panel
				//3. If MatchStatus = "CLOSED"
					//then hide the Umpire Panel and show the Match ScorePanel and hide all the event controls
				string strMatchStatus = objMatch.MatchStatus;
				if (strMatchStatus == "NOTSTARTED")
				{
					pnlMatchRound.Visible = false;
					pnlKickOffMatch.Visible = true;
					
					lblMatchStatus.Text = "Match Status: Not Yet Started ";
				}
				if (strMatchStatus == "INPROGRESS")
				{
					pnlKickOffMatch.Visible = false;
					pnlMatchRound.Visible = true;
					
					lblMatchStatus.Text = "Match Status: In Progress ";
					lblMatchScoreHeading.Text = "Match - Score  Live!";
					
				}
				if (strMatchStatus == "DONE")
				{
					pnlKickOffMatch.Visible = false;
					pnlMatchRound.Visible = true;
					
					lblMatchStatus.Text = "Match Status: Completed ";					
					
					//Get the winner
					string strWinnerName = (new TPDAL_Match()).GetWinnerNameByMatchID(strTournamentCode, strMatchID);
					lblWinnerName.Text = "Winner: " + strWinnerName;
					pnlWinnerName.Visible = true;
					
					//If Match DONE then all the transacional event objects should not be visible
					HideObjectsIfMatchDONE ();					
				}
				
        	}				
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RunMatch.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        	        
        }
        
        protected void btnKickOffMatch_Click(object sender, EventArgs e)
		{
			try
            {
				strSportCode = (String)Session["SPORTCODE"];
	        	strTournamentCode = (String)Session["TOURNAMENTCODE"];
	        	strMatchID = (String) Session["MATCHID"];
	        	
                String strUmpireCode = txtUmpireCode.Text;
	        	String strUmpireName = txtUmpireName.Text;                
                String strMatchLocation = txtMatchLocation.Text;

                if (!string.IsNullOrEmpty(strUmpireName) && !string.IsNullOrEmpty(strUmpireCode))
                {
                	TPMatch objMatch = new TPMatch();
             
                	objMatch.ID = strMatchID;
                	objMatch.UmpireCode = strUmpireCode;
                	objMatch.UmpireName = strUmpireName;
                	objMatch.CourtDetails = strMatchLocation;
                	                	
					TPDAL_Match objDALMatch = new TPDAL_Match();                        
					objDALMatch.KickOffMatch (objMatch);
                }
                
                pnlKickOffMatch.Visible = false;
                pnlMatchRound.Visible = true;
            }
            catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RunMatch.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
            }
			
		}
        
        private List<TPMatchRound> GetMatchRounds ()
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
	        			if (lstObjMatchRound[0].RoundStatus.Equals("INPROGRESS"))
	        			{
	        				lblRound1Status.Text = "Running";
	        				btnFirstSetStart.Text = "Done";
	        			}
	        			if (lstObjMatchRound[0].RoundStatus.Equals("NOTSTARTED"))
	        				lblRound1Status.Text = "Not Yet Started";
	        			
	        			
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

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RunMatch.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
				
        	return lstObjMatchRound;
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
				
                GetMatchRounds ();
            }
            catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_RunMatch.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
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
        	string strMatchID = (String) Session["MATCHID"];
        	
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
         
            	objMatch.ID = strMatchID;
            	            	                	
				TPDAL_Match objDALMatch = new TPDAL_Match();                        
				objDALMatch.FinishMatch (objMatch);            
        	}                
        	        	
        	GetMatchDetails ();
        }
        
        #endregion Finish Match
    }
}