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
	public partial class TP_GenerateDraw : System.Web.UI.Page
	{	
		string strSportCode = "BD";
    	string strTournamentCode = "";

        protected void Page_Init(object sender, System.EventArgs e)
        {
            try
            {
                strTournamentCode = Request.QueryString["TCode"];
                strTournamentCode = MGCommon.MGGeneric.DecryptData(strTournamentCode.Trim());

                GenerateEvents();
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
					if(ddlTournamentEvents.Items.Count == 0 )
					{
						PopulateTournamentDetails (strSportCode,strTournamentCode);
			///			PopulateOwnerDashboard(strTournamentCode);
					}
				//	GenerateParticipatedEvents ();
					
				}
				else
				{
					Response.Redirect("./TP_Login.aspx", false);
				}
                if (!IsPostBack)
                {
                    List<TPStatus> lstTPStatusObj = (new TPDAL_Tournament()).GetTournamentStatus(strSportCode, strTournamentCode);

                    if (lstTPStatusObj != null && lstTPStatusObj.Count > 0)
                    {
                        string strDRAWSStatus = lstTPStatusObj[0].DRAWSPublished;

                        if (strDRAWSStatus.ToUpper().Equals("ON"))
                        {
                            pnlGenerateDraws.Visible = false;
                            btnPublished.Attributes["class"] = "button_Atul";
                            btnNotPublished.Attributes["class"] = "buttonOFF_Atul";
                        }
                        else
                        {
                            pnlGenerateDraws.Visible = true;
                            btnPublished.Attributes["class"] = "buttonOFF_Atul";
                            btnNotPublished.Attributes["class"] = "button_Atul";
                        }

                        if (lstTPStatusObj[0].TournamentStatus.Equals("CLOSED"))
                        {
                            pnlGenerateDraws.Visible = false;
                            btnPublished.Enabled = false;
                            btnNotPublished.Enabled = false;
                        }
                    }
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

        #region Published/Not Published

        protected void btnNotPublished_Click(object sender, EventArgs e)
        {
            //Set the Published status as "NO"
            btnPublished.Attributes["class"] = "buttonOFF_Atul";
            btnNotPublished.Attributes["class"] = "button_Atul";

            TPStatus objTPStatus = new TPStatus();

            objTPStatus.SportCode = strSportCode;
            objTPStatus.TournamentCode = strTournamentCode;
            objTPStatus.DRAWSPublished = "OFF";

            TPDAL_TournamentController objDALTPController = new TPDAL_TournamentController();
            objDALTPController.UpdateTournamentStatus(objTPStatus);

            pnlGenerateDraws.Visible = true;           
        }

        protected void btnPublished_Click(object sender, EventArgs e)
        {
            //Set the Published status as "YES"
            btnNotPublished.Attributes["class"] = "buttonOFF_Atul";
            btnPublished.Attributes["class"] = "button_Atul";
            
            TPStatus objTPStatus = new TPStatus();

            objTPStatus.SportCode = strSportCode;
            objTPStatus.TournamentCode = strTournamentCode;
            objTPStatus.DRAWSPublished = "ON";

            TPDAL_TournamentController objDALTPController = new TPDAL_TournamentController();
            objDALTPController.UpdateTournamentStatus(objTPStatus);

            pnlGenerateDraws.Visible = false;
        }

        #endregion

        protected void rdDraws_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = rdDraws.SelectedIndex;
            string strSelectedItem = rdDraws.SelectedItem.Value;

            if (i == 0) //KnockOut Draws
            {
                //Knockout Panel should be visible
                //League Panel should not be visible
                pnlGenerateDraws.Visible = true;
                pnlLeagueDraws.Visible = false;

            }
            if (i == 1) // League
            {
                //Knockout Panel should not be visible
                //League Panel should be visible
                pnlGenerateDraws.Visible = false;
                pnlLeagueDraws.Visible = true;
            }
        }

        #region KnockOut Draws

        #region Generate draw
        
        protected void btnUpdateSeed_Click(object sender, EventArgs e)
        {
        	try
        	{
        		strTournamentCode = (string)Session["TOURNAMENTCODE"]; 
	        	
	        	//If status code is OPEN then only GenerateDraws will be possible else not
	        	//int iFlag = (new TPDAL_Tournament()).UpdateSeedForEvent(strSportCode, strTournamentCode, ddlTournamentEvents.SelectedItem.Text, 
	        	//                                                               txtSeed1.Text, txtSeed2.Text, txtSeed3.Text, txtSeed4.Text);

                int iFlag = (new TPDAL_Tournament()).UpdateSeedForEvent(strSportCode, strTournamentCode, ddlTournamentEvents.SelectedItem.Text,
                                                                               txtSeed1.Text, txtSeed2.Text, txtSeed3.Text, txtSeed4.Text, 
                                                                               txtSeed5.Text, txtSeed6.Text, txtSeed7.Text, txtSeed8.Text);
					
		    	pnlErrorMsg.Visible = true;
				lblErrorMsg.Text = "Seeded Entry is updated for Event : "+ddlTournamentEvents.SelectedItem.Text +"  !!!";
        
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
        		strTournamentCode = (string)Session["TOURNAMENTCODE"]; 
	        	
                String strEventType = "";
                String strEventCode = ddlTournamentEvents.SelectedItem.Text; //"BS U11";
                if (strEventCode == "Team")
                    strEventType = "S";
                else
                    strEventType = strEventCode.Substring(1, 1); //"S" for Singles & "D" for Doubles

                int i = (new TPDAL_TournamentController()).GenerateDraws(strSportCode, strTournamentCode, strEventCode, strEventType);

                pnlErrorMsg.Visible = true;
                lblErrorMsg.Text = "Draws Generated Successfully for event : " + strEventCode + "  !!!";
                   
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
        
        #region Populate Draws

        public class Match
        {
            public int id { get; set; }
            public int teamid1 { get; set; }
            public int teamid2 { get; set; }
            public string firstTeamName { get; set; }
            public string secondTeamName { get; set; }
            public int roundnumber { get; set; }
            public int winner { get; set; }
            public string strWinner { get; set; }
            public string strMatchScore { get; set; }
            public string strMatchSchedule { get; set; }

            public Match(int id, int teamid1, int teamid2, string firstTeamName, string secondTeamName, int roundnumber, int winner, string strWinner, string strMatchScore ,string strMatchSchedule)
            {
                this.id = id;
                this.teamid1 = teamid1;
                this.teamid2 = teamid2;
                this.firstTeamName = firstTeamName;
                this.secondTeamName = secondTeamName;
                this.roundnumber = roundnumber;
                this.winner = winner;
                this.strWinner = strWinner;
                this.strMatchScore = strMatchScore;
                this.strMatchSchedule = strMatchSchedule;
            }
        }

        public class Tournament
        {
            public SortedList<int, SortedList<int, Match>> TournamentRoundMatches { get; private set; }

            public Match ThirdPlaceMatch { get; private set; }

            public Tournament(int rounds, String strSportCode, String strTournamentCode, String strEventCode)
            {
                this.TournamentRoundMatches = new SortedList<int, SortedList<int, Match>>();

                this.GenerateTournamentResults(rounds, strSportCode, strTournamentCode, strEventCode);

                if (rounds > 1)
                {
                    //this.GenerateThirdPlaceResult(rounds);
                }
            }

            public void AddMatch(Match m)
            {
                if (this.TournamentRoundMatches.ContainsKey(m.roundnumber))
                {
                    if (!this.TournamentRoundMatches[m.roundnumber].ContainsKey(m.id))
                    {
                        this.TournamentRoundMatches[m.roundnumber].Add(m.id, m);
                    }
                }
                else
                {
                    this.TournamentRoundMatches.Add(m.roundnumber, new SortedList<int, Match>());
                    this.TournamentRoundMatches[m.roundnumber].Add(m.id, m);
                }
            }

            private void GenerateTournamentResults(int rounds, String strSportCode, String strTournamentCode, String strEventCode)
            {
                //string strSportCode = (String)Session["SPORTCODE"];
                //string strTournamentCode = (String)Session["TOURNAMENTCODE"];
                //string strEventCode = "BS U11";

                List<TPMatch> lstObj = (new TPDAL_Match()).GetMatchesBySportANDTournament(strSportCode, strTournamentCode, strEventCode, "");

                int iMatchCount = lstObj.Count;

                if (iMatchCount < 2)
                    rounds = 1;
                else if (iMatchCount >= 2 && iMatchCount < 5)
                    rounds = 2;
                else if (iMatchCount >= 5 && iMatchCount < 8)
                    rounds = 3;
                else if (iMatchCount >= 8 && iMatchCount < 16)
                    rounds = 4;
                else if (iMatchCount >= 16 && iMatchCount < 32)
                    rounds = 5;
                else if (iMatchCount >= 32 && iMatchCount < 64)
                    rounds = 6;
                else if (iMatchCount >= 64 && iMatchCount < 128)
                    rounds = 7;
                else if (iMatchCount >= 128 && iMatchCount < 256)
                    rounds = 8;
                else if (iMatchCount >= 256 && iMatchCount < 512)
                    rounds = 9;
                else if (iMatchCount >= 512 && iMatchCount < 1024)
                    rounds = 9;
                else if (iMatchCount >= 1024 && iMatchCount < 2048)
                    rounds = 10;

                Random WinnerRandomizer = new Random();

                for (int round = 1, match_id = 1; round <= rounds; round++)
                {
                    int matches_in_round = 1 << (rounds - round);
                    for (int round_match = 1; round_match <= matches_in_round; round_match++, match_id++)
                    {
                        int team1_id;
                        int team2_id;
                        string strFirstTeam = "";
                        string strSecondTeam = "";
                        int winner;
                        string strWinner = "";
                        string strMatchScore = "";
                        string strMatchSchedule = "";
                        if (round == 1)
                        {
                            if (lstObj != null && lstObj.Count > 0)
                            {
                                team1_id = (match_id * 2) - 1;
                                team2_id = (match_id * 2);

                                strFirstTeam = lstObj[match_id - 1].FirstTeamPlayerName;
                                strSecondTeam = lstObj[match_id - 1].SecondTeamPlayerName;

                            }
                            else
                            {
                                team1_id = (match_id * 2) - 1;
                                team2_id = (match_id * 2);
                            }
                        }
                        else
                        {
                            int match1 = (match_id - (matches_in_round * 2) + (round_match - 1));
                            int match2 = match1 + 1;
                            team1_id = this.TournamentRoundMatches[round - 1][match1].winner;
                            team2_id = this.TournamentRoundMatches[round - 1][match2].winner;

                            //if (match_id > lstObj.Count)
                            {
                                if (!string.IsNullOrEmpty(lstObj[match1 - 1].WinnerTeamCode))
                                {
                                    if (lstObj[match1 - 1].WinnerTeamCode.Equals("First"))
                                        strFirstTeam = lstObj[match1 - 1].FirstTeamPlayerName + " : " + lstObj[match1 - 1].MatchScore;
                                    else
                                        strFirstTeam = lstObj[match1 - 1].SecondTeamPlayerName + " : " + lstObj[match1 - 1].MatchScore;
                                }
                                else
                                {
                                    strFirstTeam = "";
                                }

                                if (!string.IsNullOrEmpty(lstObj[match2 - 1].WinnerTeamCode))
                                {
                                    if (lstObj[match2 - 1].WinnerTeamCode.Equals("First"))
                                        strSecondTeam = lstObj[match2 - 1].FirstTeamPlayerName + " : " + lstObj[match2 - 1].MatchScore;
                                    else
                                        strSecondTeam = lstObj[match2 - 1].SecondTeamPlayerName + " : " + lstObj[match2 - 1].MatchScore;
                                }
                                else
                                {
                                    strSecondTeam = "";
                                }

                            }
                        }
                        /*if(lstObj[match_id - 1].WinnerTeamCode.ToUpper() == "SECOND")
                            winner = team2_id;
                        else if (lstObj[match_id - 1].WinnerTeamCode.ToUpper() == "FIRST")	
                            winner = team1_id;
                        */
                        int iCount = lstObj.Count;
                        winner = 0;
                        if (!string.IsNullOrEmpty(lstObj[iCount - 1].WinnerTeamCode))
                        {
                            if (lstObj[iCount - 1].WinnerTeamCode.Equals("First"))
                            {
                                //strWinner = lstObj[iCount - 1].FirstTeamPlayerName ;
                                strWinner = lstObj[iCount - 1].FirstTeamPlayerName + " : " + lstObj[iCount - 1].MatchScore;
                                winner = team1_id;
                            }
                            else
                            {
                                //strWinner = lstObj[iCount - 1].SecondTeamPlayerName;
                                strWinner = lstObj[iCount - 1].SecondTeamPlayerName + " : " + lstObj[iCount - 1].MatchScore;
                                winner = team2_id;
                            }
                        }
                        else
                        {
                            strWinner = "";
                        }


                        //winner = 0;
                        strMatchScore = lstObj[match_id - 1].MatchScore;

                        this.AddMatch(new Match(match_id, team1_id, team2_id, strFirstTeam, strSecondTeam, round, winner, strWinner, strMatchScore , strMatchSchedule));
                    }
                }
            }

            private void GenerateThirdPlaceResult(int rounds)
            {
                Random WinnerRandomizer = new Random();
                int semifinal_matchid1 = this.TournamentRoundMatches[rounds - 1].Keys.ElementAt(0);
                int semifinal_matchid2 = this.TournamentRoundMatches[rounds - 1].Keys.ElementAt(1);
                Match semifinal_1 = this.TournamentRoundMatches[rounds - 1][semifinal_matchid1];
                Match semifinal_2 = this.TournamentRoundMatches[rounds - 1][semifinal_matchid2];
                int semifinal_loser1 = (semifinal_1.winner == semifinal_1.teamid1) ? semifinal_1.teamid2 : semifinal_1.teamid1;
                int semifinal_loser2 = (semifinal_2.winner == semifinal_2.teamid1) ? semifinal_2.teamid2 : semifinal_2.teamid1;

                int third_place_winner = (WinnerRandomizer.Next(1, 3) == 1) ? semifinal_loser1 : semifinal_loser2;
                this.ThirdPlaceMatch = new Match((1 << rounds) + 1, semifinal_loser1, semifinal_loser2, "", "", 1, third_place_winner, "", "" , "");
            }
        }

        private string GenerateHTMLResultsTable(Tournament tournament)
        {
            int match_white_span;
            int match_span;
            int position_in_match_span;
            int column_stagger_offset;
            int effective_row;
            int col_match_num;
            int cumulative_matches;
            int effective_match_id;
            int rounds = tournament.TournamentRoundMatches.Count;
            int teams = 1 << rounds;
            int max_rows = teams << 1;
            StringBuilder HTMLTable = new StringBuilder();


            HTMLTable.AppendLine("<style type=\"text/css\">");

            //Set the backgroud color and Font of the Top Header
            HTMLTable.AppendLine("    .thd {background: rgb(220,220,220); font: bold 18pt Arial; text-align: center;}");

            //Set font for Player Name
            HTMLTable.AppendLine("    .team {color: white; background:rgb(105,105,105); font: 18pt Calibri; border-right: solid 2px black;}");

            //Set the font and background for winner
            HTMLTable.AppendLine("    .winner {color: Black; background: rgb(156,156,156); font: bold 18pt Calibri; text-align:top;}");

            //Set the Font for Vs
            HTMLTable.AppendLine("    .vs {font: bold 10pt Calibri; border-right: solid 2px black;text-align: center;}");

            //Set width of the Columns
            HTMLTable.AppendLine("    td, th {padding: 5px 5px 5px 5px; border-right: dotted 2px rgb(200,200,200); text-align: left;}");

            //Set the font for Header "Tournament Results"
            HTMLTable.AppendLine("    h1 {font: bold 12pt Arial; margin-top: 2pt;}");

            HTMLTable.AppendLine("</style>");

            HTMLTable.AppendLine("<table border=\"0\" cellspacing=\"0\">");


            for (int row = 0; row <= max_rows; row++)
            {
                cumulative_matches = 0;
                HTMLTable.AppendLine("    <tr>");
                for (int col = 1; col <= rounds + 1; col++)
                {

                    match_span = 1 << (col + 1);
                    match_white_span = (1 << col) - 1;
                    column_stagger_offset = match_white_span >> 1;
                    if (row == 0)
                    {
                        if (col <= rounds)
                        {
                            HTMLTable.AppendLine("        <th class=\"thd_Atul\">Round " + col + "</th>");
                        }
                        else
                        {
                            HTMLTable.AppendLine("        <th class=\"thd_Atul\">Winner</th>");
                        }
                    }
                    else if (row == 1)
                    {
                        HTMLTable.AppendLine("        <td class=\"white_span\" rowspan=\"" + (match_white_span - column_stagger_offset) + "\">&nbsp;</td>");
                    }
                    else
                    {
                        effective_row = row + column_stagger_offset;
                        if (col <= rounds)
                        {
                            position_in_match_span = effective_row % match_span;
                            position_in_match_span = (position_in_match_span == 0) ? match_span : position_in_match_span;

                            col_match_num = (effective_row / match_span) + ((position_in_match_span < match_span) ? 1 : 0);

                            effective_match_id = cumulative_matches + col_match_num;

                            if ((position_in_match_span == 1) && (effective_row % match_span == position_in_match_span))
                            {
                                HTMLTable.AppendLine("        <td class=\"white_span\" rowspan=\"" + match_white_span + "\">&nbsp;</td>");
                            }
                            else if ((position_in_match_span == (match_span >> 1)) && (effective_row % match_span == position_in_match_span))
                            {
                                string strTeamID1 = tournament.TournamentRoundMatches[col][effective_match_id].teamid1.ToString();

                                if (string.IsNullOrEmpty(tournament.TournamentRoundMatches[col][effective_match_id].firstTeamName))
                                {
                                    strTeamID1 = "";
                                }


                                //HTMLTable.AppendLine("        <td class=\"team_Atul\"> " + strTeamID1 + " " + tournament.TournamentRoundMatches[col][effective_match_id].firstTeamName + "</td>");
                                //without numbering
                                HTMLTable.AppendLine("        <td class=\"team_Atul\"> " + " " + tournament.TournamentRoundMatches[col][effective_match_id].firstTeamName + "</td>");

                            }
                            else if ((position_in_match_span == ((match_span >> 1) + 1)) && (effective_row % match_span == position_in_match_span))
                            {
                                HTMLTable.AppendLine("        <td class=\"vs\" rowspan=\"" + match_white_span + "\">VS</td>");
                            }
                            else if ((position_in_match_span == match_span) && (effective_row % match_span == 0))
                            {
                                string strTeamID2 = tournament.TournamentRoundMatches[col][effective_match_id].teamid2.ToString();

                                if (string.IsNullOrEmpty(tournament.TournamentRoundMatches[col][effective_match_id].secondTeamName))
                                    strTeamID2 = "";

                                //HTMLTable.AppendLine("        <td class=\"team_Atul\"> " + strTeamID2 + " " + tournament.TournamentRoundMatches[col][effective_match_id].secondTeamName + "</td>");
                                //without numbering
                                HTMLTable.AppendLine("        <td class=\"team_Atul\"> " + " " + tournament.TournamentRoundMatches[col][effective_match_id].secondTeamName + "</td>");

                            }
                        }
                        else
                        {
                            if (row == column_stagger_offset + 2)
                            {
                                string strWinnerID = tournament.TournamentRoundMatches[rounds][cumulative_matches].winner.ToString();
                                if (string.IsNullOrEmpty(tournament.TournamentRoundMatches[rounds][cumulative_matches].strWinner))
                                {
                                    strWinnerID = "";
                                }

                                //HTMLTable.AppendLine("        <td class=\"winner_Atul\"> " + strWinnerID + " " + tournament.TournamentRoundMatches[rounds][cumulative_matches].strWinner  + " <img src='..\\Images\\icoTrophy.png' width=40px; height=30px; style=\" padding-top:-40px;\"></td>");
                                //HTMLTable.AppendLine("        <td class=\"winner_Atul\"> " + strWinnerID + " " + tournament.TournamentRoundMatches[rounds][cumulative_matches].strWinner + " " + tournament.TournamentRoundMatches[rounds][cumulative_matches].strMatchScore + " <img src='..\\Images\\icoTrophy.png' width=40px; height=30px; style=\" padding-top:-40px;\"></td>");
                                //without numbering
                                HTMLTable.AppendLine("        <td class=\"winner_Atul\"> " + " " + tournament.TournamentRoundMatches[rounds][cumulative_matches].strWinner + " <img src='..\\Images\\icoTrophy.png' width=40px; height=30px; style=\" padding-top:-40px;\"></td>");

                            }
                            else if (row == column_stagger_offset + 3)
                            {
                                HTMLTable.AppendLine("        <td class=\"white_span\" rowspan=\"" + (match_white_span - column_stagger_offset) + "\">&nbsp;</td>");
                            }
                        }
                    }
                    if (col <= rounds)
                    {
                        cumulative_matches += tournament.TournamentRoundMatches[col].Count;
                    }
                }
                HTMLTable.AppendLine("    </tr>");
            }
            HTMLTable.AppendLine("</table>");

            return HTMLTable.ToString();
        }
        
        private void GenerateEvents()
        {
            //string strSportCode = (String)Session["SPORTCODE"];
            //string strTournamentCode = (String)Session["TOURNAMENTCODE"];

            List<TPMatch> lstObj = (new TPDAL_Match()).GetMatchesBySportANDTournament(strSportCode, strTournamentCode, "");

            if (lstObj != null)
            {
                if (lstObj.Count > 0)
                {
                    var eventList = lstObj.Select(x => x.EventCode).Distinct();

                    if (lstObj != null && lstObj.Count > 0)
                    {
                        Int32 i = 0; ; //create a integer variable
                        string strEventCode = "";
                        //for(i = 0; i < 5; i++) // will generate 10 LinkButton
                        foreach (var item in eventList)
                        {
                            strEventCode = item.ToString();
                            //create instance of LinkButton                
                            LinkButton lb = new LinkButton();
                            if (i == (eventList.Count() - 1)) // Last item
                                lb.Text = "[ " + strEventCode + " ]"; //LinkButton Text
                            else
                                lb.Text = "[ " + strEventCode + " ]" + "   -   "; //LinkButton Text

                            lb.ID = i.ToString(); // LinkButton ID’s
                            lb.Attributes.Add("runat", "server");
                            lb.Attributes.Add("OnClientClick", "showLoading();");
                            //lb.Click += new EventHandler(lb_Click);
                            lb.Command += new CommandEventHandler(lb_Command);//Create Handler for it.
                            lb.CommandName = strEventCode; // i.ToString(); //LinkButton CommanName
                            PlaceHolderEventName.Controls.Add(lb); // Adding the LinkButton in PlaceHolder
                            i = i + 1;
                        }
                    }
                }
            }
        }

        public void lb_Command(object sender, CommandEventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            String strSelectedLinkButton = e.CommandName;

            Label1.Text = strSelectedLinkButton; // will display the which Linkbutton clicked

            //if(lnk.Font.Bold == true)
            //{
            //    lnk.Font.Bold = false;
            //    lnk.ForeColor = System.Drawing.Color.Blue;
            //}
            //else
            {
                lnk.Font.Bold = true;
                lnk.ForeColor = System.Drawing.Color.Green;
            }

            PopulateDraws(strSelectedLinkButton);
        }
        
        private void PopulateDraws(String strSelectedEvent)
        {

            TPDAL_Match obj = new TPDAL_Match();
            //   List<TPMatch> lstObj = (new TPDAL_Match()).GetMatchesBySportANDTournament(strSportCode, strTournamentCode, strSelectedEvent);
            String strMatchType = obj.GetMatchesTypeByeventCode(strSportCode, strTournamentCode, strSelectedEvent);

            if (strMatchType.ToUpper() == "LEAGUE")
            {
                divLeague.Visible = true;
                divKnocOut.Visible = false;

                List<TPLeagueTable> lstObj = (new TPDAL_Match()).GetLeagueTable(strSportCode, strTournamentCode, strSelectedEvent);

                dgLeague.DataSource = lstObj;
                dgLeague.DataBind();

            }
            else if (strMatchType.ToUpper() == "KNOCKOUT")
            {

                divLeague.Visible = false;
                divKnocOut.Visible = true;
                Tournament Test2RoundTournament = new Tournament(2, strSportCode, strTournamentCode, strSelectedEvent);
                String strDraws = GenerateHTMLResultsTable(Test2RoundTournament);
                lit1.Text = strDraws;
            }
            ///////
           
        }

        #endregion

        #endregion

        #region League Draws

        protected void btnLeagueDraws_Click(object sender, EventArgs e)
        {
            try
            {
                strTournamentCode = (string)Session["TOURNAMENTCODE"];

                String strEventType = "";
                String strEventCode = ddlTournamentEvents.SelectedItem.Text; //"BS U11";
                if (strEventCode == "Team")
                    strEventType = "S";
                else
                    strEventType = strEventCode.Substring(1, 1); //"S" for Singles & "D" for Doubles

                int i = (new TPDAL_TournamentController()).GenerateLeagueDraws(strSportCode, strTournamentCode, strEventCode, strEventType);

                pnlErrorMsg.Visible = true;
                lblErrorMsg.Text = "Draws Generated Successfully for event : " + strEventCode + "  !!!";

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
