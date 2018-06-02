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
    public partial class TP_TournamentDetails : System.Web.UI.Page
    {
        String strSportCode = "BD";
        String strTournamentCode = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            Session["SPORTCODE"] = strSportCode;
            try
            {
                strTournamentCode = Request.QueryString["TCode"];
                Session["TOURNAMENTCODE"] = strTournamentCode;

                if (string.IsNullOrEmpty(strTournamentCode))
                {
                    strTournamentCode = (String)Session["TOURNAMENTCODE"];
                }
            }
            catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_Events.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);

                strTournamentCode = (String)Session["TOURNAMENTCODE"];
            }


            PopulateTournamentSummary();

            Session["TOURNAMENTCODE"] = strTournamentCode;

            List<TPStatus> lstTPStatusObj = (new TPDAL_Tournament()).GetTournamentStatus(strSportCode, strTournamentCode);

            if (lstTPStatusObj != null && lstTPStatusObj.Count > 0)
            {
                string strTournamentStatus = lstTPStatusObj[0].TournamentStatus; 
                
                if (strTournamentStatus.Equals("INACTIVE"))
                {
                    pnlRegistrationOPENUp.Visible = pnlRegistrationOPENBottom.Visible = false;
                    pnlOtherThanRegistrationUp.Visible = pnlOtherThanRegistrationBottom.Visible = false;
                }
                else if (strTournamentStatus.Equals("UPCOMING"))
                {
                    pnlRegistrationOPENUp.Visible = pnlRegistrationOPENBottom.Visible = false;
                    pnlOtherThanRegistrationUp.Visible = pnlOtherThanRegistrationBottom.Visible = true;
                    lblTournamentStatusUp.Text = lblTournamentStatusBottom.Text = "Tournament details will be announced soon...";

                    //lblTournamentDuration.Text = lblDuration.Text = "Date and Duration will be announced soon";

                }
                else if (strTournamentStatus.Equals("OPEN"))
                {
                    pnlOtherThanRegistrationUp.Visible = pnlOtherThanRegistrationBottom.Visible = false;
                    pnlRegistrationOPENUp.Visible = pnlRegistrationOPENBottom.Visible = true;
                }
                else if (strTournamentStatus.Equals("RUNNING"))
                {
                    pnlRegistrationOPENUp.Visible = pnlRegistrationOPENBottom.Visible = false;
                    pnlOtherThanRegistrationUp.Visible = pnlOtherThanRegistrationBottom.Visible = true;
                    lblTournamentStatusUp.Text = lblTournamentStatusBottom.Text = "Tournament is RUNNING...";

                    string strDRAWSPublishStatus = lstTPStatusObj[0].DRAWSPublished;

                    if (strDRAWSPublishStatus.ToUpper().Equals("OFF"))
                    {
                        lblTournamentStatusUp.Text = lblTournamentStatusBottom.Text = "Tournament DRAWS will be published soon...";
                    }
                }
                else if (strTournamentStatus.Equals("CLOSED"))
                {
                    pnlRegistrationOPENUp.Visible = pnlRegistrationOPENBottom.Visible = false;
                    pnlOtherThanRegistrationUp.Visible = pnlOtherThanRegistrationBottom.Visible = true;
                    lblTournamentStatusUp.Text = lblTournamentStatusBottom.Text = "Tournament is CLOSED";
                }
            }
            if (IsPostBack)
            {

            }
            else
            {
                //PopulateTournamentSummary();

                //PopulateEvents();
            }
        }

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
                    lblTournamentName.Text = lblTPName.Text = lstObj[0].TournamentName;
                    //lblEntryOpenDate.Text = lstObj[0].TournamentEntryOpenDate.ToString("dddd, dd MMMM yyyy");
                    //lblEntryClosesDate.Text = lblEntryCloseDate.Text = lstObj[0].TournamentEntryCloseDate.ToString("dddd, dd MMMM yyyy");
                    //lblEntryWithdrawalDate.Text = lstObj[0].TournamentEntryWithdrawlDate.ToString("dddd, dd MMMM yyyy");
                    lblTournamentDuration.Text = lblDuration.Text = lstObj[0].TournamentStartDate.ToString("dddd, dd MMMM yyyy") + " -TO- " + lstObj[0].TournamentEndDate.ToString("dddd, dd MMMM yyyy");
                    lblTournamentOrganisation.Text = lblOrganisation.Text = lstObj[0].TournamentOrganisation;
                    lblTournamentVenue.Text = lblVenue.Text = lstObj[0].TournamentVenue;
                    //lblLocationAddress.Text = lstObj[0].TournamentLocationAddress;
                    //lblTournamentContacts.Text = lblContacts.Text = lstObj[0].TournamentPOCContactNames + " " + lstObj[0].TournamentPOCContactNo + " " + lstObj[0].TournamentLocationContactNo;
                    lblContacts.Text = lstObj[0].TournamentPOCContactNames + " " + lstObj[0].TournamentLocationContactNo;
                }
            }
            catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_Events.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
            }

        }
        
        protected void lbtnOnlineRegistrationForm_Click(object sender, EventArgs e)
        {
            //Open Registration form as per tournament registration form selection

            //string strTournamentCode = (sender as LinkButton).CommandArgument;

            //Step1: Get the selected tournament form
            //Step2: redirect Tournamentform aspx accordingly			
            try
            {
                List<TPTournament> lstObj = null;
                string strRegistrationFormType = "";
                string strRedirectURL = "";

                lstObj = (new TPDAL_Tournament()).GetTournamentLists(strSportCode, strTournamentCode, "REGISTRATION"); // "RUNNING"

                if (lstObj != null)
                {
                    if (lstObj.Count > 0)
                    {
                        strRegistrationFormType = lstObj[0].TournamentRegistrationFormType;
                    }
                }

                string strPath = HttpContext.Current.Request.Url.AbsolutePath;
                strPath = HttpContext.Current.Request.Url.AbsoluteUri;

                if (strRegistrationFormType.Trim().Equals("SIMPLE"))
                {
                    //if(strPath.ToUpper().Contains("SCREENS"))
                    strRedirectURL = strPath.ToUpper().Replace("TP_TOURNAMENTDETAILS.ASPX", "TP_RegistrationFormSimple.aspx");
                    //else
                    //	strRedirectURL = "./screens/TP_RegistrationFormSimple.aspx?TCode=" + strTournamentCode;	
                }

                if (strRegistrationFormType.Trim().Equals("ACADEMY"))
                {
                    //if(strPath.ToUpper().Contains("SCREENS"))
                    strRedirectURL = strPath.ToUpper().Replace("TP_TOURNAMENTDETAILS.ASPX", "TP_RegistrationFormAcademy.aspx");
                    //else
                    //	strRedirectURL = "./screens/TP_RegistrationFormAcademy.aspx?TCode=" + strTournamentCode;
                }

                if (strRegistrationFormType.Trim().Equals("CORPORATE"))
                {
                    //if(strPath.ToUpper().Contains("SCREENS"))
                    strRedirectURL = strPath.ToUpper().Replace("TP_TOURNAMENTDETAILS.ASPX", "TP_RegistrationFormCorporate.aspx");
                    //else
                    //	strRedirectURL = "./screens/TP_RegistrationFormCorporate.aspx?TCode=" + strTournamentCode;
                }

                if (strRegistrationFormType.Trim().Equals("CORPORATE+TEAM"))
                {
                    //if(strPath.ToUpper().Contains("SCREENS"))
                    strRedirectURL = strPath.ToUpper().Replace("TP_TOURNAMENTDETAILS.ASPX", "TP_TeamCorporateNavigation.aspx");
                    //else
                    //	strRedirectURL = "./screens/TP_TeamCorporateNavigation.aspx?TCode=" + strTournamentCode;
                }

                Response.Redirect(strRedirectURL, false);

            }
            catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_BD_Home.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);

                Response.Redirect("./TP_Down.aspx");
            }
        }

        protected void lbEvents_Click(object sender, EventArgs e)
        {
            string strPath = HttpContext.Current.Request.Url.AbsolutePath;
            strPath = HttpContext.Current.Request.Url.AbsoluteUri;

            string strRedirectURL = strPath.ToUpper().Replace("TP_TOURNAMENTDETAILS.ASPX", "TP_Events.aspx");

            Response.Redirect(strRedirectURL, false);
        }

        protected void lbPlayers_Click(object sender, EventArgs e)
        {
            string strPath = HttpContext.Current.Request.Url.AbsolutePath;
            strPath = HttpContext.Current.Request.Url.AbsoluteUri;

            string strRedirectURL = strPath.ToUpper().Replace("TP_TOURNAMENTDETAILS.ASPX", "TP_Players.aspx");

            Response.Redirect(strRedirectURL, false);
        }

        protected void lbDraws_Click(object sender, EventArgs e)
        {
            string strPath = HttpContext.Current.Request.Url.AbsolutePath;
            strPath = HttpContext.Current.Request.Url.AbsoluteUri;

            string strRedirectURL = strPath.ToUpper().Replace("TP_TOURNAMENTDETAILS.ASPX", "TP_Draws.aspx");

            Response.Redirect(strRedirectURL, false);
        }
                
        protected void lbMatches_Click(object sender, EventArgs e)
        {
            string strPath = HttpContext.Current.Request.Url.AbsolutePath;
            strPath = HttpContext.Current.Request.Url.AbsoluteUri;

            string strRedirectURL = strPath.ToUpper().Replace("TP_TOURNAMENTDETAILS.ASPX", "TP_Matches.aspx");

            Response.Redirect(strRedirectURL, false);
        }

        protected void lbTermsandConditons_Click(object sender, EventArgs e)
        {
            string strPath = HttpContext.Current.Request.Url.AbsolutePath;
            strPath = HttpContext.Current.Request.Url.AbsoluteUri;

            string strRedirectURL = strPath.ToUpper().Replace("TP_TOURNAMENTDETAILS.ASPX?TCODE=" + strTournamentCode, "TP_TermsAndConditions.aspx");

            Response.Redirect(strRedirectURL, false);
        }
    }
}