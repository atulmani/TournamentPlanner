using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TP.Entity;
using TP.DAL;

namespace TournamentPlanner
{
    public partial class TP_CreateUmpire : System.Web.UI.Page
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

        protected void btnCreateUmpire_Click(object sender, EventArgs e)
        {
            try
            {   

                string strUmpireName = txtUmpireName.Text.Trim();
                string strUmpireMobile = txtUmpireMobileNo.Text.Trim();
                string strEmailIDasUserCode =  txtEmailIDasUserCode.Text.Trim();
                string strPWD = txtPWD.Text.Trim();
                string strPassword = MGCommon.MGGeneric.EncryptData(strPWD);

                TPLogin obj = new TPLogin();
                obj.SportCode = strSportCode.ToUpper();
                obj.TournamentCode = strTournamentCode.ToUpper();
                obj.UserName = strUmpireName;
                obj.UserMobile = strUmpireMobile;
                obj.UserID = strEmailIDasUserCode;
                obj.UserPwd = strPassword;
                
                //Create UMPIRE Login Details
                TPDAL_UserAuthenticationAuthorization objDAL = new TPDAL_UserAuthenticationAuthorization();
                int iCount = objDAL.CreateUmpireLogin (obj);

                if (iCount == 0) // Success
                {
                    lblMsg.Text = "Umpire Login Created Successfully";
                }
                else  //Success
                {
                    lblMsg.Text = "There is some issue while creating Umpire Login";
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
    }
}