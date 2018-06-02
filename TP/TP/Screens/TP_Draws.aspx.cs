using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TP.DAL;
using TP.Entity;

namespace TournamentPlanner
{
    public partial class TP_Draws : System.Web.UI.Page
    {
        string strSportCode = "BD";
        string strTournamentCode = "";
        //List<TPMatch> lstObj = null;

        #region Page Initiation and Load

        protected void Page_Init(object sender, System.EventArgs e)
        {
            //GenerateEvents ();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Session["SPORTCODE"] = strSportCode;
                strTournamentCode = Request.QueryString["TCode"];
                Session["TOURNAMENTCODE"] = strTournamentCode;

                if (!IsPostBack)
                {
                    PopulateTournamentSummary();
                }
                //   else
                {
                    List<TPStatus> lstTPStatusObj = (new TPDAL_Tournament()).GetTournamentStatus(strSportCode, strTournamentCode);

                    if (lstTPStatusObj != null && lstTPStatusObj.Count > 0)
                    {
                        string strDRAWSStatus = lstTPStatusObj[0].DRAWSPublished;

                        if (strDRAWSStatus.ToUpper().Equals("ON"))
                        {
                            if (!string.IsNullOrEmpty(strTournamentCode))
                            {
                                GenerateEvents();
                                PlaceHolderEventName.Visible = true;
                                Label1.Visible = true;
                                pnlDraws.Visible = true;
                                pnlDrawsNotPublished.Visible = false;
                            }
                            else
                            {
                                PlaceHolderEventName.Visible = false;
                                Label1.Visible = false;
                            }
                        }
                        else
                        {
                            pnlDrawsNotPublished.Visible = true;
                            pnlDraws.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_Draws.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);

                Response.Redirect("./TP_BD_Home.aspx", false);
            }
        }

        #endregion


        protected void lbtnTPMenu_Click(object sender, EventArgs e)
        {
            LinkButton lbtnObj = (LinkButton)sender;
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

        #region PopulateTournamentSummary

        /// <summary>
        /// PopulateTournamentSummary -- The header part
        /// </summary>
        private void PopulateTournamentSummary()
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
                    lblTournamentDuration.Text = lstObj[0].TournamentStartDate.ToString("dddd, dd MMMM yyyy") + " -TO- " + lstObj[0].TournamentEndDate.ToString("dddd, dd MMMM yyyy");
                    lblTournamentOrganisation.Text = lstObj[0].TournamentOrganisation;
                    lblTournamentVenue.Text = lstObj[0].TournamentVenue;
                    //lblLocationAddress.Text = lstObj[0].TournamentLocationAddress; 
                    //lblTournamentContacts.Text = lstObj[0].TournamentPOCContactNames + " " + lstObj[0].TournamentPOCContactNo + " " + lstObj[0].TournamentLocationContactNo;
                }
            }
            catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_Draws.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
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


            public Match(int id, int teamid1, int teamid2, string firstTeamName, string secondTeamName, int roundnumber, int winner, string strWinner, string strMatchScore, string strMatchSchedule)
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
                if (lstObj[0].MatchType == "League")
                {
                    //        divKnocOut.Visible = false;
                    //      divLeague.Visible = true;

                }
                else
                {
                    //    divKnocOut.Visible = true;
                    //   divLeague.Visible = false;
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
                                    strMatchSchedule = lstObj[match_id - 1].MatchSchedule;
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
                                        {
                                            if (string.IsNullOrEmpty(lstObj[match1 - 1].MatchSchedule))
                                                strFirstTeam = lstObj[match1 - 1].FirstTeamPlayerName + " : " + lstObj[match1 - 1].MatchScore;
                                            else
                                                strFirstTeam = lstObj[match1 - 1].FirstTeamPlayerName + " : " + lstObj[match1 - 1].MatchScore + "  (" + lstObj[match1 - 1].MatchSchedule + ")";
                                        }
                                        else
                                        {
                                            if (string.IsNullOrEmpty(lstObj[match1 - 1].MatchSchedule))
                                                strFirstTeam = lstObj[match1 - 1].SecondTeamPlayerName + " : " + lstObj[match1 - 1].MatchScore;
                                            else
                                                strFirstTeam = lstObj[match1 - 1].SecondTeamPlayerName + " : " + lstObj[match1 - 1].MatchScore + "  (" + lstObj[match1 - 1].MatchSchedule + ")";
                                        }
                                    }
                                    else
                                    {
                                        strFirstTeam = lstObj[match1 - 1].MatchSchedule;
                                    }

                                    if (!string.IsNullOrEmpty(lstObj[match2 - 1].WinnerTeamCode))
                                    {
                                        if (lstObj[match2 - 1].WinnerTeamCode.Equals("First"))
                                        {
                                            if (string.IsNullOrEmpty(lstObj[match2 - 1].MatchSchedule))
                                                strSecondTeam = lstObj[match2 - 1].FirstTeamPlayerName + " : " + lstObj[match2 - 1].MatchScore;
                                            else
                                                strSecondTeam = lstObj[match2 - 1].FirstTeamPlayerName + " : " + lstObj[match2 - 1].MatchScore + "  (" + lstObj[match2 - 1].MatchSchedule + ")";
                                        }
                                        else
                                        {
                                            if (string.IsNullOrEmpty(lstObj[match2 - 1].MatchSchedule))
                                                strSecondTeam = lstObj[match2 - 1].SecondTeamPlayerName + " : " + lstObj[match2 - 1].MatchScore;
                                            else
                                                strSecondTeam = lstObj[match2 - 1].SecondTeamPlayerName + " : " + lstObj[match2 - 1].MatchScore + "  (" + lstObj[match2 - 1].MatchSchedule + ")";
                                        }
                                    }
                                    else
                                    {
                                        strSecondTeam = lstObj[match2 - 1].MatchSchedule;
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
                                    if (string.IsNullOrEmpty(lstObj[iCount - 1].MatchSchedule))
                                        strWinner = lstObj[iCount - 1].FirstTeamPlayerName + " : " + lstObj[iCount - 1].MatchScore;
                                    else
                                        strWinner = lstObj[iCount - 1].FirstTeamPlayerName + " : " + lstObj[iCount - 1].MatchScore + "  (" + lstObj[iCount - 1].MatchSchedule + ")";

                                    winner = team1_id;
                                }
                                else
                                {
                                    //strWinner = lstObj[iCount - 1].SecondTeamPlayerName;
                                    if (string.IsNullOrEmpty(lstObj[iCount - 1].MatchSchedule))
                                        strWinner = lstObj[iCount - 1].SecondTeamPlayerName + " : " + lstObj[iCount - 1].MatchScore;
                                    else
                                        strWinner = lstObj[iCount - 1].SecondTeamPlayerName + " : " + lstObj[iCount - 1].MatchScore + " (" + lstObj[iCount - 1].MatchSchedule + ")";

                                    winner = team2_id;
                                }
                            }
                            else
                            {
                                strWinner = lstObj[iCount - 1].MatchSchedule;
                            }


                            //winner = 0;
                            strMatchScore = lstObj[match_id - 1].MatchScore;
                            strMatchSchedule = lstObj[match_id - 1].MatchSchedule;

                            this.AddMatch(new Match(match_id, team1_id, team2_id, strFirstTeam, strSecondTeam, round, winner, strWinner, strMatchScore, strMatchSchedule));
                        }
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
                this.ThirdPlaceMatch = new Match((1 << rounds) + 1, semifinal_loser1, semifinal_loser2, "", "", 1, third_place_winner, "", "", "");
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
                                //HTMLTable.AppendLine("        <td class=\"vs\" rowspan=\"" + match_white_span + "\">VS" + tournament.TournamentRoundMatches[col][effective_match_id].strMatchSchedule + "</td>");                                
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

        #endregion

        private void GenerateEvents()
        {
            string strSportCode = (String)Session["SPORTCODE"];
            string strTournamentCode = (String)Session["TOURNAMENTCODE"];

            //Check Draws Published or not, if not don't do anything
            List<TPStatus> lstTPStatusObj = (new TPDAL_Tournament()).GetTournamentStatus(strSportCode, strTournamentCode);

            if (lstTPStatusObj != null && lstTPStatusObj.Count > 0)
            {
                string strDRAWSStatus = lstTPStatusObj[0].DRAWSPublished;

                if (!strDRAWSStatus.ToUpper().Equals("ON"))
                {
                    pnlDrawsNotPublished.Visible = true;
                    pnlDraws.Visible = false;
                }
                else
                {
                    pnlDrawsNotPublished.Visible = false;
                    pnlDraws.Visible = true;

                    List<TPMatch> lstObj = (new TPDAL_Match()).GetMatchesBySportANDTournament(strSportCode, strTournamentCode, "");
                    //logic for league and knockout

                    if (lstObj != null)
                    {

                        if (lstObj.Count <= 0)
                        {
                            pnlDraws.Visible = false;
                        }
                        else
                        {
                            pnlDraws.Visible = true;

                            /*                            if (lstObj[0].MatchType == "League")
                                                        {
                                                            divLeague.Visible = true;
                                                            divKnocOut.Visible = true;
                                                            dgLeague.DataSource = lstObj;
                                                            dgLeague.DataBind();
                                                        }*/
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
                                    //lb.Attributes.Add("OnClientClick", "showLoading();");
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
            }
        }

        public void lb_Command(object sender, CommandEventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            String strSelectedLinkButton = e.CommandName;
            Label1.Text = strSelectedLinkButton; // will display the which Linkbutton clicked
            Label123.Visible = true;
            Label1.Visible = true;
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

        //Click to generate HTML results
        protected void btnGenerateDraws_Click(object sender, EventArgs e)
        {
            //GenerateDraws ();
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
        }

    }
}