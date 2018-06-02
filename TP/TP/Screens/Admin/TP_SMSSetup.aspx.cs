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
    public partial class TP_SMSSetup : System.Web.UI.Page
    {
        string strSportCode = "BD";
        string strTournamentCode = "";
    	

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                strTournamentCode = Request.QueryString["TCode"];
                Session["TOURNAMENTCODE"] = strTournamentCode = MGCommon.MGGeneric.DecryptData(strTournamentCode.Trim());

                if (Session["USERID"] != null ||
                    Session["USERTYPE"] != null)
                {
                    //string strTournamentStatus = (new TPDAL_Tournament()).GetTournamentStatus(strSportCode, strTournamentCode);

                    List<TPSMS> lstObj = (new TPDAL_SMSTracker()).GetTournamentSMSLimit(strSportCode, strTournamentCode);
                    
                    if (lstObj != null && lstObj.Count > 0)
                    {
                        //Get Tournament SMS Limit
                        lblSMSLimit.Text = lstObj[0].SMSLimit;
                        
                        //Get count of SMS sent already
                        lblUsedSMSCount.Text = lstObj[0].SMSCount;

                        lblRemainingSMSCount.Text = (int.Parse(lblSMSLimit.Text) - int.Parse(lblUsedSMSCount.Text)).ToString(); 
                    }

                    if (IsPostBack)
                    {

                    }
                    else
                    {
                        
                    }

                }
                else
                {
                    Response.Redirect("./TP_Login.aspx", false);
                }

            }
            catch (Exception ex)
            {

                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_SMSSetup.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
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

        protected void txtSMSMsg_TextChanged(object sender, EventArgs e)
        {
            lblSMSMsgLength.Text = txtSMSMsg.Text.Length.ToString();
        }

        protected void btnSMSSend_Click(object sender, EventArgs e)
        { 
            //Send SMS to Mentioned Mobile No
            string strMobileNo = txtMobileNo.Text;
            string strMsg = txtSMSMsg.Text;

            (new MGCommon.BulkSMS()).SendSMS(strMobileNo, strMsg);

            txtMobileNo.Text = "";
            txtSMSMsg.Text = "";
        }


        /// <summary>
        /// DRAWS Notification
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDRAWSPublishedNotification_Click(object sender, EventArgs e)
        {
            string strPlayerFName = "";
            string strPlayerMobileNo = "";
            //Get all mobile nos of players
            //Send 10 mobile no at a time accordingly prepare loop

            try
            {
                List<TPPlayer> lstObj = new TPDAL_Registration().GetAllPlayerList(strSportCode, strTournamentCode);

                lblPlayerCount.Text = lstObj.Count.ToString();

                for (int i = 0; i < lstObj.Count; i++)
                {
                    //if (i % 10 == 0)
                    //{
                    //Send SMS
                    //(new MGCommon.BulkSMS()).SMS_DRAWS_Published (strPlayerFirstName, strMobileNo);
                    //}

                    strPlayerFName = lstObj[i].PlayerFName;
                    strPlayerMobileNo = lstObj[i].PlayerContact;

                    (new MGCommon.BulkSMS()).SMS_DRAWS_Published(strPlayerFName, strPlayerMobileNo);
                }

                lblDrawsStatus.Text = "SMS Sent Successfully";
            }
            catch (Exception ex)
            {
                lblDrawsStatus.Text = "There is some ISSUE sending SMS, Please check";
            }
        }
        
        /// <summary>
        /// Match Schedule Notification
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMatchScheduleNotification_Click(object sender, EventArgs e)
        {
            string strPlayerFName = "";
            string strOpponentName = "";
            string strMatchSchedule = "";
            string strPlayerMobileNo = "";
            string strEventCode = "";
            //Get all mobile nos of players
            //Send 10 mobile no at a time accordingly prepare loop

            try
            {
                strEventCode = ddlEventCode.SelectedItem.Text;

                if (strEventCode.Equals("BULK"))
                {
                    List<TPPlayer> lstObj = new TPDAL_Registration().GetAllPlayerList(strSportCode, strTournamentCode);

                    //lblPlayerCount.Text = lstObj.Count.ToString();

                    for (int i = 0; i < lstObj.Count; i++)
                    {                        
                        strPlayerFName = lstObj[i].PlayerFName;
                        strPlayerMobileNo = lstObj[i].PlayerContact;

                        (new MGCommon.BulkSMS()).SMS_MatchSchedule_Published(strPlayerFName, strPlayerMobileNo);
                    }
                }
                else
                {

                    List<TPMatchScheduleSMS> lstObj = new TPDAL_SMSTracker().GetMatchScheduleToSMS(strTournamentCode, strEventCode);

                    //lblPlayerCount.Text = lstObj.Count.ToString();

                    for (int i = 0; i < lstObj.Count; i++)
                    {
                        if (string.IsNullOrEmpty(lstObj[i].PlayerName) ||
                            string.IsNullOrEmpty(lstObj[i].MobileNo))
                        {
                            //SKIP - DO NOT SEND SMS if
                            //Player Name not available
                            //Mobile No not available
                        }
                        else
                        {
                            strPlayerFName = lstObj[i].PlayerName;
                            strOpponentName = lstObj[i].Opponent;
                            strMatchSchedule = lstObj[i].MatchSchedule;
                            strPlayerMobileNo = lstObj[i].MobileNo;

                            if (!string.IsNullOrEmpty(strMatchSchedule))
                            {

                                if (string.IsNullOrEmpty(strOpponentName))
                                {
                                    //Send SMS without opponent name
                                    (new MGCommon.BulkSMS()).SMS_MatchSchedule_Without_Opponent(strPlayerFName, strEventCode, strMatchSchedule, strPlayerMobileNo);
                                }
                                else
                                {
                                    //Send SMS with Opponent Name
                                    (new MGCommon.BulkSMS()).SMS_MatchSchedule_With_Opponent(strPlayerFName, strEventCode, strOpponentName, strMatchSchedule, strPlayerMobileNo);
                                }
                            }
                        }
                    }
                }

                lblMatchScheduleStatus.Text = "SMS Sent Successfully";
            }
            catch (Exception ex)
            {
                lblDrawsStatus.Text = "There is some ISSUE sending SMS, Please check";
            }
        }
    }
}