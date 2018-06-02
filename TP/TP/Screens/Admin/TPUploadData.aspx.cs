using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.OleDb;
using TP.DAL;

namespace TP.Screens.Admin
{
    public partial class TPUploadData : System.Web.UI.Page
    {
        string strTournamentCode = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            strTournamentCode = Request.QueryString["TCode"];
            strTournamentCode = MGCommon.MGGeneric.DecryptData(strTournamentCode.Trim());

        }

        public void ImportDataFromExcel(string excelFilePath)   
        {   
            //declare variables - edit these based on your particular situation   
            //string ssqltable = "DC_TempExcelPlayerDetails";   
            // make sure your sheet name is correct, here sheet name is sheet1,  so you can change your sheet name if have    different   
            //string myexceldataquery = "select SNO,	PlayerFirstName,	PlayerMiddleName,	PlayerLastName,	PlayerFullName,	PlayerEMailID,	PlayerContactNo,	PlayerGender,	PlayerDateOfBirth,	PlayerAddress  from [Sheet1$]";   
            string myexceldataquery = "select * from [Sheet1$]";   
            
            try   
            {   
                //create our connection strings   
                string sexcelconnectionstring = "provider=microsoft.jet.oledb.4.0;data source=" + excelFilePath +   
                ";extended properties=" + "\"excel 8.0;hdr=yes;\"";   
        
                //series of commands to bulk copy data from the excel file into our sql table   
                OleDbConnection oledbconn = new OleDbConnection(sexcelconnectionstring);   
                OleDbCommand oledbcmd = new OleDbCommand(myexceldataquery, oledbconn);   
                oledbconn.Open();   
                OleDbDataReader dr = oledbcmd.ExecuteReader();
                TPDAL_Tournament obj = new TPDAL_Tournament();
                obj.uploadDataInTable("BD", strTournamentCode, dr);
                dr.Close();   
                oledbconn.Close();   
                //Label1.Text = "File imported into sql server successfully.";   
            }   
            catch (Exception ex)   
            {   
                //handle exception   
            }   
        }

        protected void btnUploadData_Click(object sender, EventArgs e)
        {
            ImportDataFromExcel(FileUpload1.PostedFile.FileName);
        }  
   }
}