using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TP.Entity;
using System.Data.SqlClient;
using System.Data.OleDb;
using TP.DAL;

namespace TournamentPlanner
{
    public partial class TP_OfflineRegistration : System.Web.UI.Page
    {
        String strSportCode = "BD";
        String strTournamentCode = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                Session["SPORTCODE"] = strSportCode;
                strTournamentCode = Request.QueryString["TCode"];
                strTournamentCode = MGCommon.MGGeneric.DecryptData(strTournamentCode);
                Session["TOURNAMENTCODE"] = strTournamentCode;

                if (!IsPostBack)
                {
                    try
                    {
            
                        if (string.IsNullOrEmpty(strTournamentCode))
                        {
                            strTournamentCode = (String)Session["TOURNAMENTCODE"];
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                        MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_OfflineRegistration.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                        MGError objLogError = new MGError();
                        objLogError.logError(objError);

                        strTournamentCode = (String)Session["TOURNAMENTCODE"];
                    }

                    Session["TOURNAMENTCODE"] = strTournamentCode;

                    //string strTournamentStatus = (new TPDAL_Tournament()).GetTournamentStatus(strSportCode, strTournamentCode);
                    List<TPStatus> lstObj = (new TPDAL_Tournament()).GetTournamentStatus(strSportCode, strTournamentCode);

                    if (lstObj != null && lstObj.Count > 0)
                    {
                        string strTournamentStatus = lstObj[0].TournamentStatus;

                        if (strTournamentStatus.Equals("OPEN") || strTournamentStatus.Equals("RUNNING"))
                        {
                            lbOnlineRegistrationForm.Enabled = true;
                        }
                        else
                        {
                            lbOnlineRegistrationForm.Enabled = false;
                        }
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_OfflineRegistation.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
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

        protected void lbtnOnlineRegistrationForm_Click(object sender, EventArgs e)
        {
            //Open Registration form as per tournament registration form selection

            //string strTournamentCode = (sender as LinkButton).CommandArgument;

            //Step1: Get the selected tournament form
            //Step2: redirect Tournamentform aspx accordingly			
            try
            {
                strTournamentCode = (String)Session["TOURNAMENTCODE"];

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
                
                //http://localhost:52321/Screens/Admin/TP_OfflineRegistration.aspx?TCode=QgBEAF8AVABQADQA"

                if (strRegistrationFormType.Trim().Equals("SIMPLE"))
                {
                    //if(strPath.ToUpper().Contains("SCREENS"))
                    strRedirectURL = strPath.ToUpper().Replace("ADMIN/TP_OFFLINEREGISTRATION.ASPX","TP_RegistrationFormSimple.aspx");

                    string[] strURL = strRedirectURL.Split('?');
                    strURL[1] = "?TCODE=" + strTournamentCode;
                    strRedirectURL = strURL[0] + strURL[1];

                    //else
                    //	strRedirectURL = "./screens/TP_RegistrationFormSimple.aspx?TCode=" + strTournamentCode;	
                }

                if (strRegistrationFormType.Trim().Equals("ACADEMY"))
                {
                    //if(strPath.ToUpper().Contains("SCREENS"))
                    //strRedirectURL = strPath.ToUpper().Replace("Admin/TP_OfflineRegistration.aspx", "TP_RegistrationFormAcademy.aspx");
                    strRedirectURL = strPath.ToUpper().Replace("ADMIN/TP_OFFLINEREGISTRATION.ASPX", "TP_RegistrationFormAcademy.aspx");

                    string[] strURL = strRedirectURL.Split('?');
                    strURL[1] = "?TCODE=" + strTournamentCode;
                    strRedirectURL = strURL[0] + strURL[1];

                }

                if (strRegistrationFormType.Trim().Equals("CORPORATE"))
                {
                    //if(strPath.ToUpper().Contains("SCREENS"))
                    //strRedirectURL = strPath.ToUpper().Replace("Admin/TP_OfflineRegistration.aspx", "TP_RegistrationFormCorporate.aspx");
                    strRedirectURL = strPath.ToUpper().Replace("ADMIN/TP_OFFLINEREGISTRATION.ASPX", "TP_RegistrationFormCorporate.aspx");

                    string[] strURL = strRedirectURL.Split('?');
                    strURL[1] = "?TCODE=" + strTournamentCode;
                    strRedirectURL = strURL[0] + strURL[1];
                }

                if (strRegistrationFormType.Trim().Equals("CORPORATE+TEAM"))
                {
                    //if(strPath.ToUpper().Contains("SCREENS"))
                    //strRedirectURL = strPath.ToUpper().Replace("Admin/TP_OfflineRegistration.aspx", "TP_TeamCorporateNavigation.aspx");
                    strRedirectURL = strPath.ToUpper().Replace("ADMIN/TP_OFFLINEREGISTRATION.ASPX", "TP_TeamCorporateNavigation.aspx");

                    string[] strURL = strRedirectURL.Split('?');
                    strURL[1] = "?TCODE=" + strTournamentCode;
                    strRedirectURL = strURL[0] + strURL[1];
                }

                Response.Redirect(strRedirectURL, false);

            }
            catch (Exception ex)
            {
                System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_BD_Home.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);

                Response.Redirect("./TP_Down.aspx", false);
            }
        }

        public void ImportDataFromExcel(string excelFilePath)
        {
            //declare variables - edit these based on your particular situation   
            //string ssqltable = "DC_TempExcelPlayerDetails";   
            // make sure your sheet name is correct, here sheet name is sheet1,  so you can change your sheet name if have    different   
            //string myexceldataquery = "select SNO,	PlayerFirstName,	PlayerMiddleName,	PlayerLastName,	PlayerFullName,	PlayerEMailID,	PlayerContactNo,	PlayerGender,	PlayerDateOfBirth,	PlayerAddress ,PlayerCity,	PlayerZIPCode,	PlayerDistrict,	PlayerState,	PlayerCountry,	TournamentCode, PlayerTShirtSize,	AcademyCoachName,	AcademyCoachContact,	AcademyCoachEmailID,	AcademyName,	AcademyContact,	AcademyAddress,	OrgnizationName,	OrgnizationDesignation,	OrgnizationHRName,	OrgnizationHREmailID,	OrgnizationHRContact,	OrgnizationIDProof,	OrganizationGovtIDProof,	eventcode,	partnerFName,	partnerLName,	partnerDOB from [Sheet1$]";   
              
            string myexceldataquery = "select * from [Sheet1$]";
            OleDbConnection oledbconn = null;
            OleDbCommand oledbcmd = null;
            OleDbDataReader dr = null;
            try
            {
                //create our connection strings   
                string sexcelconnectionstring = "provider=microsoft.jet.oledb.4.0;data source=" + excelFilePath +
                ";extended properties=" + "\"excel 8.0;hdr=yes;\"";

                //string sexcelconnectionstring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelFilePath + 
                 //   ";Extended Properties=\"Excel 12.0;HDR=YES;\"";


                //series of commands to bulk copy data from the excel file into our sql table   
                oledbconn = new OleDbConnection(sexcelconnectionstring);
                oledbcmd = new OleDbCommand(myexceldataquery, oledbconn);
                oledbconn.Open();
                dr = oledbcmd.ExecuteReader();
                TPDAL_Tournament obj = new TPDAL_Tournament();
                obj.uploadDataInTable("BD", strTournamentCode, dr);
                //dr.Close();
                //oledbconn.Close();
                //Label1.Text = "File imported into sql server successfully.";   
            }
            catch (Exception ex)
            {
                int i = 0;
                //handle exception   
            }
            finally
            {   
                if(dr != null)
                dr.Close();
                if(oledbconn != null)
                oledbconn.Close();
            }
        }

        protected void btnUploadData_Click(object sender, EventArgs e)
        {
            ImportDataFromExcel(FileUpload1.PostedFile.FileName);
        }
    }
}