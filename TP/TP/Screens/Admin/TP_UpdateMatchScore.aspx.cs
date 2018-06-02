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
	public partial class TP_UpdateMatchScore : System.Web.UI.Page
	{	
		string strSportCode = "BD";
    	string strTournamentCode = "";
    	string strMatchID = "";
    	
        protected void Page_Load(object sender, EventArgs e)
        {	
			pnlErrorMsg.Visible = false;

			try
			{   
				strTournamentCode = Request.QueryString["TCode"];
				strTournamentCode = MGCommon.MGGeneric.DecryptData(strTournamentCode.Trim());
	        	
				if (Session["USERID"] != null ||
				    Session["USERTYPE"] != null)
				{	
					//if(ddlTournamentEvents.Items.Count == 0 )
					{
						PopulateMatches();
					}
				//	GenerateParticipatedEvents ();
					
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
        
		protected void lbtnMatchList_Click(object sender, EventArgs e)
        {        	
        	pnlMatchList.Visible = true;
        	pnlUpdateScore.Visible = false;
        	lblErrorMsg.Visible = false;
        }
		
        private void PopulateMatches ()
		{
        	try
        	{
        		string strEventCode = "";
				//lstObj = new List<TPMatch>();
				//List<TPMatch> lstObj = null;
				List<TPMatch> lstObj = (new TPDAL_Match()).GetMatchesBySportANDTournamentUmpire(strSportCode, strTournamentCode, strEventCode, "");
				
				dgMatchList.DataSource = lstObj;
				dgMatchList.DataBind();
				
				
				
        	}
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_Matches.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}			
		}
        
        protected void dgMatchList_RowDataBound(object sender, DataGridItemEventArgs e)
		{
        	if (e.Item.Cells[8].Text.Equals("First"))
        	{
        		//e.Item.Cells[3].BackColor = System.Drawing.Color.Blue;
        		e.Item.Cells[3].Font.Bold = true; 
        	}
        	
        	else if (e.Item.Cells[8].Text.Equals("Second"))
        	{
        		//e.Item.Cells[3].BackColor = System.Drawing.Color.Blue;
        		e.Item.Cells[4].Font.Bold = true; 
        	}       	
		}
        
        protected void lbMatchID_Click(object sender, EventArgs e)
        {
        	strMatchID = (sender as LinkButton).CommandArgument;
        				
        	try
        	{        		
				//Show UpdateMatchScore Panel

				
				 Label lblevent= (Label)((LinkButton) sender).Parent.FindControl("lbEventCode");
				lblEventCode.Text = lblevent.Text;

				 TPMatch lstMatch = (new TPDAL_Match()).GetMatchDetails(strSportCode, strTournamentCode, strMatchID, lblEventCode.Text);

                 HiddenField hfType = (HiddenField)((LinkButton)sender).Parent.FindControl("hfMatchType");
                 hfMatchType.Value = hfType.Value;


				pnlUpdateScore.Visible = true;
				pnlMatchList.Visible = false;
				lblMatchID.Text = 	strMatchID;			
        		rbFirstPlayer.Text = lstMatch.FirstTeamPlayerName;
        		rbSecondPlayer.Text = lstMatch.SecondTeamPlayerName;

                if (lstMatch.FirstTeamPlayerName == "" || lstMatch.SecondTeamPlayerName == "")
                {
                    //one of the player is not present so can not start the match
                    btnUpdateScore.Enabled = false;
                    btnRollbackStatus.Enabled = false;
                    NoplayerMessame.Text = "This match can not be updated now, as both players are not ready";
                }
                else
                {
                    string strByePlayer = "";
                    bool blFlag = false;
                    if (lstMatch.FirstTeamPlayerName.ToUpper().Equals("BYE"))
                    {
                        rbSecondPlayer.Checked = true;
                        rbFirstPlayer.Checked = false;
                        strByePlayer = rbSecondPlayer.Text;
                        blFlag = true;
                    }
                    if (lstMatch.SecondTeamPlayerName.ToUpper().Equals("BYE"))
                    {

                        rbSecondPlayer.Checked = false;
                        rbFirstPlayer.Checked = true;
                        strByePlayer = rbFirstPlayer.Text;
                        blFlag = true;
                    }
                    if (blFlag == true)
                    {

                        btnUpdateScore.Enabled = false;
                        btnRollbackStatus.Enabled = false;
                        NoplayerMessame.Text = "Player :" + strByePlayer +" has already got Bye, match can not be udpated";
                    }
                    else
                    {

                        btnUpdateScore.Enabled = true;
                        btnRollbackStatus.Enabled = true;
                        NoplayerMessame.Text = "";
                    }
                }//Update score against match id
								
        	}
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_UpdateMatchScore.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
        		Response.Redirect("./TP_Down.aspx");
        	}
        }

        protected void btnUpdateScore_Click(object sender, EventArgs e)
        {
            try
            {
                string strWinner = "";

                if (rbFirstPlayer.Checked)
                    strWinner = "First";
                else if (rbSecondPlayer.Checked)
                    strWinner = "Second";
                else if (rbNoMatch.Checked)
                    strWinner = "No Match";

                string strMatchid = lblMatchID.Text;
                string strMatchType = hfMatchType.Value;

                string strMatchScore = txtMatchScore.Text;
                string strMatchDuration = txtMatchDuration.Text;
                string strCourtName = txtCourtName.Text;
                int iFirstPoint = 0;
                int iSecondPoint = 0;
                int ii;

                if (strWinner != "No Match")
                {
                    strMatchScore = txtMatchScore.Text;
                    strMatchDuration = txtMatchDuration.Text;
                    strCourtName = txtCourtName.Text;

                    if (strMatchType.ToUpper().Equals("KNOCKOUT"))
                        (new TPDAL_Match()).UpdateMatchDetails(strSportCode, strTournamentCode, lblEventCode.Text, strMatchid, strMatchScore, strMatchDuration, strCourtName, strWinner);
                    else
                    {
                        //update league match
                        if (strMatchScore.ToUpper().Equals("BYE"))
                        {
                            iFirstPoint = iSecondPoint = 0;
                        }
                        else
                        {
                            String[] objscore = strMatchScore.Split(',');
                            for (int i = 0; i < objscore.Count(); i++)
                            {
                                String[] objS = objscore[i].Split('-');
                                if (objS[0].Trim() != "")
                                {
                                    if (int.TryParse(objS[0].Trim(), out ii))
                                        iFirstPoint += int.Parse(objS[0].Trim());

                                }
                                if (objS[1].Trim() != "")
                                {
                                    if (int.TryParse(objS[1].Trim(), out ii))
                                        iSecondPoint += int.Parse(objS[1].Trim());

                                }


                            }
                        }
                        (new TPDAL_Match()).UpdateMatchDetailsLeague(strSportCode,
                            strTournamentCode, lblEventCode.Text,
                            strMatchid,
                            strMatchScore,
                            strMatchDuration,
                            strCourtName,
                            strWinner,
                            iFirstPoint,
                            iSecondPoint
                            );

                    }
                }
                else
                {
                    //update no match 
                    (new TPDAL_Match()).UpdateNoMatchStatus(strSportCode,
                          strTournamentCode, lblEventCode.Text, strMatchType,
                          strMatchid);

                }

                //pnlMatchList.Visible = true;
                //pnlUpdateScore.Visible = false;
                //lblErrorMsg.Visible = false;

                lblErrorMsg.Text = "Match details saved Succesfully";
                pnlErrorMsg.Visible = true;

            }
            catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_UpdateMatchScore.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);

                Response.Redirect("./TP_Down.aspx");
            }
        }

        protected void btnRollbackScore_Click(object sender, EventArgs e)
        {	
        	try{
	        	string strWinner="";

                if (rbFirstPlayer.Checked)
                    strWinner = "First";
                else if (rbSecondPlayer.Checked)
                    strWinner = "Second";
                else if (rbNoMatch.Checked)
                    strWinner = "No Match";

                string strMatchid = lblMatchID.Text;
                string strMatchType = hfMatchType.Value;
                
                string strMatchScore = txtMatchScore.Text;
	        	string strMatchDuration = txtMatchDuration.Text;
	        	string strCourtName = txtCourtName.Text;
	        	int iFirstPoint = 0;
                int iSecondPoint = 0;
                int ii;

                if (strWinner != "No Match")
                {
                    strMatchScore = txtMatchScore.Text;
                    strMatchDuration = txtMatchDuration.Text;
                    strCourtName = txtCourtName.Text;

                    if (strMatchType.ToUpper().Equals("KNOCKOUT"))
                        (new TPDAL_Match()).RollbackMatchDetails(strSportCode, strTournamentCode, lblEventCode.Text, strMatchid);
                    else
                    {
                        (new TPDAL_Match()).RollbackMatchDetailsLeague(strSportCode,
                            strTournamentCode, lblEventCode.Text,
                            strMatchid                            );

                    }
                }
                else
                { 
                    //update no match 
              //      (new TPDAL_Match()).UpdateNoMatchStatus(strSportCode,
               //           strTournamentCode, lblEventCode.Text, strMatchType,
                //          strMatchid);
                      
                }


            
	        	lblErrorMsg.Text = "Match details saved Succesfully";
	        	pnlErrorMsg.Visible = true;
	        	
        	}catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_UpdateMatchScore.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
        		Response.Redirect("./TP_Down.aspx");
        	}
        }
    }
                
        
	}

