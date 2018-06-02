using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using TP.DAL;
using TP.Entity;

namespace TournamentPlanner
{
    public partial class TP_Gallery : System.Web.UI.Page
    {
        string strSportCode = "";
    	string strTournamentCode = "";
    	
        protected void Page_Load(object sender, EventArgs e)
        {			
        	Session["SPORTCODE"] = "BD";
        	        	
        	strSportCode = (String)Session["SPORTCODE"];
        	
        	try
        	{
        		strTournamentCode = Request.QueryString["TournamentCode"];
        		
        		if (string.IsNullOrEmpty(strTournamentCode))
        		{
        			strTournamentCode = (String)Session["TOURNAMENTCODE"];
        		}
        	}
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_Gallery.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
                
        		strTournamentCode = (String)Session["TOURNAMENTCODE"];
        	}
        	
        	Session["TOURNAMENTCODE"] = strTournamentCode;
        	
        	if(IsPostBack)
			{
					
			}
			else
			{
				PopulateTournamentSummary ();
				
				BindImage();
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
					lblTournamentVenue.Text = lstObj[0].TournamentVenue;
					lblLocationAddress.Text = lstObj[0].TournamentLocationAddress; 
					lblTournamentContacts.Text = lstObj[0].TournamentPOCContactNames + " " + lstObj[0].TournamentPOCContactNo + " " + lstObj[0].TournamentLocationContactNo;
				}
								
                List<TPStatus> lstTPStatusObj = (new TPDAL_Tournament()).GetTournamentStatus(strSportCode, strTournamentCode);

                if (lstTPStatusObj != null && lstTPStatusObj.Count > 0)
                {
                    string strGalleryPublishedStatus = lstTPStatusObj[0].GalleryPublished;

                    if (strGalleryPublishedStatus.ToUpper().Equals("YES"))
                    {
                        lbtnRegistrationForm.Visible = true;
                    }
                    else
                    {
                        lbtnRegistrationForm.Visible = false;
                    }
                }
        	}
        	catch (Exception ex)
        	{
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_Gallery.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
        
        }
        
        private void BindImage()
	    {
        	try
        	{
		       DataTable dt = new DataTable();
		       dt.Columns.Add(new DataColumn("Image", typeof(string)));
		       DataRow dr;
		       int i = 0;	
			   int picCounter = 1;		       
		       string srchString = "BD_TP";
		       
		       strTournamentCode = (String)Session["TOURNAMENTCODE"];
		       	
		       //string strFilePath = Directory.GetFiles((Server.MapPath(@"..\Images\TournamentPics\" + )
		       
		 		//fetching files from savedImages folder
		 		foreach (string file in Directory.GetFiles((Server.MapPath(@"..\Images\TournamentPics\"))))
		       {
		           dr = dt.NewRow();
		           dt.Rows.Add(dr);
		           
		           int iIndex = file.IndexOf(srchString);
		           string strPicName = file.Substring  (0, iIndex);
		           
		           string strNewFilePath = strPicName + strTournamentCode + "-" + picCounter + ".jpg"; 
		           	
		           //string strFileName = "../Images/TournamentPics/" + System.IO.Path.GetFileName(strNewFilePath);
		           
		           dr["Image"] = "../Images/TournamentPics/" + System.IO.Path.GetFileName(strNewFilePath);
		           i += 1;
		       }
		       GridView2.DataSource = dt;
		       GridView2.DataBind();
		       
	       }
           catch (Exception ex)
           {
        		System.Reflection.MethodBase currentMethod = System.Reflection.MethodBase.GetCurrentMethod();

                MGErrorMsg objError = new MGErrorMsg("Error", 0, "TP_Gallery.aspx", currentMethod.Name, ex.Message, DateTime.Now, "ip", "");
                MGError objLogError = new MGError();
                objLogError.logError(objError);
        	}
	    }
    }
}