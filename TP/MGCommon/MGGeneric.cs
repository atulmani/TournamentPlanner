using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web;
using System.IO;
using System.Net;
using TP.DAL;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MGCommon
{
    public class MGGeneric
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        /// 

        public const int Const_imageSize = 409600;
        public const string Const_ErrorImage = "Image size is more than 400KB. Please select a smaller image and try again";
        public const string Const_ConfirmMessageFeaturedProduct = "Selected Item(s) are removed from featured product list. To add Featured product please use Update Item or add new Item module";
        public const string Const_ConfirmMessageActivateProduct = "Selected Item(s) are made active. To inactivate product please use Update Item or add new Item module";

        public static System.Text.StringBuilder AlertMessage(string strMsg)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(strMsg);
            sb.Append("')};");
            sb.Append("</script>");

            return sb;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strMsg"></param>
        /// <param name="strRedirectURL"></param>
        /// <returns></returns>
        public static System.Text.StringBuilder AlertMessage(string strMsg, string strRedirectURL)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(strMsg);
            sb.Append("');");
            sb.Append("window.location='" + strRedirectURL + "';");
            sb.Append("};");
            sb.Append("</script>");

            return sb;
        }

        public static System.Text.StringBuilder ErrorMessage()
        {
            string strRedirectURL = "/Home.aspx";

            string strMsg = "Site is down for some time. Inconvenience is deeply regretted !";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(strMsg);
            sb.Append("');");
            sb.Append("window.location='" + strRedirectURL + "';");
            sb.Append("};");
            sb.Append("</script>");

            return sb;
        }
        
        #region Cryptography
        
        /// <summary>
        /// To encrypt the input password
        /// </summary>
        /// <param name="txtPassword"></param>
        /// <returns>It returns encrypted code</returns>
        public static string EncryptData(string strText)
        {
            byte[] passBytes = System.Text.Encoding.Unicode.GetBytes(strText);
            string encryptedText = Convert.ToBase64String(passBytes);
            return encryptedText;
        }

        /// <summary>
        /// To Decrypt password
        /// </summary>
        /// <param name="encryptedPassword"></param>
        /// <returns>It returns plain password</returns>
        public static string DecryptData(string encryptedText)
        {
            byte[] passByteData = Convert.FromBase64String(encryptedText);
            string decryptedText = System.Text.Encoding.Unicode.GetString(passByteData);
            return decryptedText;
        }

        public static string Generatehash512(string text)
        {
            byte[] message = Encoding.UTF8.GetBytes(text);

            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            SHA512Managed hashString = new SHA512Managed();
            string hex = "";
            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;

        }

        
        #endregion Cryptography
                
        /// <summary>
        /// Mail Services
        /// </summary>
        /// <param name="strToMailID"></param>
        /// <param name="strMailSubject"></param>
        /// <param name="strMailBody"></param>
        public static bool MGEMailServices(string strToMailID, string strMailSubject, string strMailBody)
        {
            bool bFlag = true;

            try
            {
             	MailMessage mailMessage = new MailMessage();
                
                mailMessage.IsBodyHtml = true;

                string[] strmaillist  = strToMailID.Split(';');
               	for (int i = 0; i< strmaillist.Length ;i ++)
               		mailMessage.To.Add(strmaillist[i]);
                            
               	string strCC = System.Web.Configuration.WebConfigurationManager.AppSettings["EMAIL_CC"];
               
               	strmaillist = strCC.Split(';');
               	for (int i = 0; i< strmaillist.Length ;i ++)
               		mailMessage.CC.Add(strmaillist[i]);
               	//mailMessage.CC.Add(strCC);
               
                string strBCC = System.Web.Configuration.WebConfigurationManager.AppSettings["EMAIL_BCC"];
                strmaillist = strBCC.Split(';');
               	for (int i = 0; i< strmaillist.Length ;i ++)
               		mailMessage.Bcc.Add(strmaillist[i]);
               
                
                string strFrom = System.Web.Configuration.WebConfigurationManager.AppSettings["EMAIL_FROM"];
                mailMessage.From = new MailAddress(strFrom);
               
                mailMessage.Subject = strMailSubject;

                mailMessage.Body = strMailBody;

                SmtpClient smtpClient = new SmtpClient();// ("smtp.gmail.com", 465);                

                smtpClient.Send(mailMessage);

            }
            catch (Exception ex)
            {
                string strError = ex.Message;
                bFlag = false;
            }

            return bFlag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string CreateRandomPassword()
        {
            int length = 7;
            string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string res = "";
            Random rnd = new Random();
            while (0 < length--)
                res += valid[rnd.Next(valid.Length)];
            return res;
        }
        
        public static string CreateCouponCode()
        {
            int length = 7;
            string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string res = "";
            Random rnd = new Random();
            while (0 < length--)
                res += valid[rnd.Next(valid.Length)];
            res = res + DateTime.Now.Ticks.ToString();
            return res;
        }
        
        public static string UppercaseWords(string value)
        {
            value = value.ToLower();
            char[] array = value.ToCharArray();
            // Handle the first letter in the string.
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            // Scan through the letters, checking for spaces.
            // ... Uppercase the lowercase letters following spaces.
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }


        #region OTP

        private static void SendEmailForOTP(string strTournamentCode, string strOTP, string strUserName, string strMobileNo, string strRegistreredEmailID)
        {
            try
            {
                string strEMailBody = string.Empty;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("");
                sb.AppendLine("Dear " + strUserName + ",");
                sb.AppendLine("");
                sb.AppendLine("Use " + strOTP + " as your login OTP for the tournament code: " + strTournamentCode + ".");
                sb.AppendLine("");
                sb.AppendLine("OTP is confidential. We never calls you asking for OTP. Never share this to anyone.");
                sb.AppendLine("");
                sb.AppendLine("Sharing it with anyone gives them full access to your Tournament Setup.");
                sb.AppendLine("");
                sb.AppendLine("Thankyou for your relationship with us.");

                sb.AppendLine("");
                sb.AppendLine("Best Regards,");
                sb.AppendLine("Team TournamentPlanner.in");

                strEMailBody = sb.ToString();
                string strEmailSubject = "Verify your identiry in TournamentPlanner";

                MGCommon.MGGeneric.MGEMailServices(strRegistreredEmailID, strEmailSubject, strEMailBody);
            }
            catch (Exception ex)
            {

            }
        }

                
        private static void SendSMSForOTP(string strTournamentCode, string strOTP, string strUserName, string strMobileNo)
        {
            try
            {
                //Send OTP as SMS
                StringBuilder sb = new StringBuilder();

                //Header
                sb.AppendLine("TournamentPlanner.in");
                sb.Append("Dear " + strUserName + ", Use " + strOTP + " as your login OTP for the tournament code: " + strTournamentCode + ".");
                sb.Append(" OTP is confidential. We never calls you asking for OTP. Never share this to anyone.");
                //sb.Append("Sharing it with anyone gives them full access to your Tournament Setup.");
                //sb.Append("Thankyou for your relationship with us.");
                string strSMSText = sb.ToString();
                BulkSMS blkSMS = new BulkSMS();
                blkSMS.SendSMS(strMobileNo, strSMSText);
            }
            catch (Exception ex)
            {

            }
        }

        public static string GenerateAndSendOTPonEmailMobile(string strTournamentCode, string strName, string strMobileNo, string strRegisteredEmailID)
        {            
            string numbers = "1234567890";
            string strOTP = string.Empty;
            //Random rand = new Random();

            try
            {
                for (int i = 0; i < 4; i++)
                {
                    string character = string.Empty;
                    do
                    {
                        int index = new Random().Next(0, numbers.Length);
                        character = numbers.ToCharArray()[index].ToString();
                    } while (strOTP.IndexOf(character) != -1);

                    strOTP += character;
                }

                //Send OTP through email
                SendEmailForOTP(strTournamentCode, strOTP, strName, strMobileNo, strRegisteredEmailID);
                
                //Send OTP as through SMS
                string strSMSSwitch = System.Web.Configuration.WebConfigurationManager.AppSettings["SMS_OTP"];
                if (strSMSSwitch.Equals("ON"))
                    SendSMSForOTP(strTournamentCode, strOTP, strName, strMobileNo);
                
            }
            catch (Exception ex)
            { 
                
            }

            return strOTP;
        }

        #endregion OTP
    }

    #region Bulk SMS

    public class BulkSMS
    { 
        //Demo Test details of websms.bulksmslabs.com
        string strusername = "labsdemo";
        string strpwd = "labsdemo";
        string strsenderid = "TESTUS";
        string strRoute = "TA";        
        string strMsgType = "1";
        string strMobileno = "";
        //string strMessage = "Hello World";

        //Demo URL
        //"http://websms.bulksmslabs.com/index.php/smsapi/httpapi/?uname=labsdemo&password=labsdemo&sender=TESTUS&receiver=9822752885&route=TA&msgtype=1&sms=hello"


        //Prod URL Test
        //http://websms.bulksmslabs.com/index.php/smsapi/httpapi/?uname=atulmani&password=9922112886&sender=TPLANN&receiver=9822752885&route=TA&msgtype=1&sms=hello"

        WebClient client = null;
        Stream data = null;
        StreamReader reader = null;

        public void SendSMS(string strPlayerMobileNo, string strSMS)
        {
            try
            {
                strusername = "atulmani";
                strpwd = "9922112886";
                strsenderid = "TPLANN";
                strRoute = "TA";
                strMsgType = "1";
                strMobileno = strPlayerMobileNo;
                //strSMS = "Hello World";
                //http://websms.bulksmslabs.com/index.php/smsapi/httpapi/?uname=atulmani&password=9922112886sender=TPLANN&receiver=9822752885&route=TA&msgtype=1&sms=Test TournamentPlanner.in
                string baseurl = "http://websms.bulksmslabs.com/index.php/smsapi/httpapi/?uname=" + strusername + "&password=" + strpwd + "&sender=" + strsenderid + "&receiver=" + strMobileno + "&route=" + strRoute + "&msgtype=" + strMsgType + "&sms=" + strSMS;

                client = new WebClient();
                data = client.OpenRead(baseurl);
                reader = new StreamReader(data);
                string s = reader.ReadToEnd();
                data.Close();
                reader.Close();
                
            }
            catch (Exception ex)
            {   
                data.Close();
                reader.Close();

                string strErrMessage = ex.Message;
            }
        }

        public void SMS_Tournament_Onboarding(string strTournamentCode, string strOwnerName, string strOwnerMobileNo)
        {
            string strEMailBody = string.Empty;

            StringBuilder sb = new StringBuilder();

            //TournamentPlanner.in
            //Dear AtulManiTripathi, Reg. completed for Events:BS 11,BS13,BS 15.Complete Your Payment,Check Email:atulmanitripathitripathi@gmail.com

            //Header
            sb.AppendLine("TP - TournamentPlanner.in");
            sb.Append("Dear " + strOwnerName + ", ");
            sb.Append("Tournament: " + strTournamentCode + " created successfully. ");
            sb.Append(" Check your email for tournament details. ");
            sb.Append(" Click to setup the tournament: http://tournamentplanner.in/screens/admin/tp_login.aspx");

            SendSMS(strOwnerMobileNo, sb.ToString());
        }

        public void SMS_Registration_Confirmation (string strFirstName,
                                                     string strPlayerMobileno,
                                                    string strEventCategory,
                                                    string strEmailID)
        {
            string strEMailBody = string.Empty;

            StringBuilder sb = new StringBuilder();

            //TournamentPlanner.in
            //Dear AtulManiTripathi, Reg. completed for Events:BS 11,BS13,BS 15.Complete Your Payment,Check Email:atulmanitripathitripathi@gmail.com
            
            //Header
            sb.AppendLine("TournamentPlanner.in");
            sb.Append("Dear " + strFirstName + ",");
            sb.Append("Reg completed for Events:" +strEventCategory);
            sb.Append(" .Complete Your Payment, Check EMail:" + strEmailID);
            
            //string strSMSBody = "TournamentPlanner.in" + "\nDear " + strFirstName + "," + "Reg completed for Events:" + strEventCategory + " ,Complete Your Payment, Check Email:" + strEmailID;

            SendSMS(strPlayerMobileno, sb.ToString());
        }

        public void SMS_Payment_Reminder(string strFirstName, string strPlayerMobileno, string strEmailID)
        {
            string strEMailBody = string.Empty;

            StringBuilder sb = new StringBuilder();

            //TournamentPlanner.in
            //Dear AtulManiTripathi, Tournament Payment Pending. Please do the Payment, Check Email:atulmanitripathitripathi@gmail.com

            //Header
            sb.AppendLine("TournamentPlanner.in");
            sb.Append("Dear " + strFirstName + ", ");
            sb.Append("Tournament Payment Pending, Please do the Payment, ");
            sb.Append("Check EMail:" + strEmailID);

            SendSMS(strPlayerMobileno, sb.ToString());
        }

        public void SMS_PaymentConfirmation(string strFirstName, string strPlayerMobileno, string strEmailID)
        {
            string strEMailBody = string.Empty;

            StringBuilder sb = new StringBuilder();

            //TournamentPlanner.in
            //Dear AtulManiTripathi, Payment Completed. Check Email:atulmanitripathitripathi@gmail.com

            //Header
            sb.AppendLine("TournamentPlanner.in");
            sb.Append("Dear " + strFirstName + ", ");
            sb.Append("Payment Completed. ");
            sb.Append("Check EMail:" + strEmailID);

            SendSMS(strPlayerMobileno, sb.ToString());
        }

        public void SMS_PaymentFailed(string strFirstName, string strPlayerMobileno, string strEmailID)
        {
            string strEMailBody = string.Empty;

            StringBuilder sb = new StringBuilder();

            //TournamentPlanner.in
            //Dear AtulManiTripathi, Payment Completed. Check Email:atulmanitripathitripathi@gmail.com

            //Header
            sb.AppendLine("TournamentPlanner.in");
            sb.Append("Dear " + strFirstName + ", ");
            sb.Append("Payment Failed. ");
            sb.Append("Check EMail:" + strEmailID);

            SendSMS(strPlayerMobileno, sb.ToString());
        }

        public void SMS_DRAWS_Published(string strFirstName,
                                                     string strPlayerMobileno)
        {
            string strEMailBody = string.Empty;

            StringBuilder sb = new StringBuilder();

            //TournamentPlanner.in
            //Dear AtulManiTripathi, DRAWS Published, Please check on website under DRAWS section.

            //Header
            sb.AppendLine("TournamentPlanner.in");
            sb.Append("Dear " + strFirstName + ", ");
            sb.Append("DRAWS Published, Please check the website");

            SendSMS(strPlayerMobileno, sb.ToString());
        }

        public void SMS_MatchSchedule_Published(string strFirstName,
                                                     string strPlayerMobileno)
        {
            string strEMailBody = string.Empty;

            StringBuilder sb = new StringBuilder();

            //TournamentPlanner.in
            //Dear AtulManiTripathi, DRAWS Published, Please check on website under DRAWS section.

            //Header
            sb.AppendLine("TournamentPlanner.in");
            sb.Append("Dear " + strFirstName + ", ");
            sb.Append("Match Scheduled Published, Please check the website");

            SendSMS(strPlayerMobileno, sb.ToString());
        }

        public void SMS_MatchSchedule_Without_Opponent(string strPlayerName,
                                                        string strEventCode,
                                                     string strMatchSchedule,
                                                    string strPlayerMobileNo)
        {
            string strEMailBody = string.Empty;

            StringBuilder sb = new StringBuilder();

            //TournamentPlanner.in
            //Dear AtulManiTripathi, DRAWS Published, Please check on website under DRAWS section.

            //Header
            sb.AppendLine("TournamentPlanner.in");
            sb.Append("Dear " + strPlayerName + ", ");
            sb.Append("Your Match Scheduled for Category: " + strEventCode + " on: " + strMatchSchedule + ". Please check the website");

            SendSMS(strPlayerMobileNo, sb.ToString());
        }

        public void SMS_MatchSchedule_With_Opponent(string strPlayerName,
                                                    string strEventCode,
                                                    string strOpponentName,
                                                     string strMatchSchedule,
                                                    string strPlayerMobileNo)
        {
            string strEMailBody = string.Empty;

            StringBuilder sb = new StringBuilder();

            //TournamentPlanner.in
            //Dear AtulManiTripathi, DRAWS Published, Please check on website under DRAWS section.

            //Header
            sb.AppendLine("TournamentPlanner.in");
            sb.Append("Dear " + strPlayerName + ", ");
            sb.Append("Your Match Scheduled for Category: " + strEventCode + " with Opponent: " + strOpponentName + " on: " + strMatchSchedule + ". Please check the website");

            SendSMS(strPlayerMobileNo, sb.ToString());
        }
    }

    #endregion

    #region Email Templates

    public class EMailTemplate
    {
        private StringBuilder sbEMailHeader = null;
        private StringBuilder sbEMailFooter = null;
        

        private string GetEmailContentFromFile(string strFileName)
        {
        	string strEmailContent = string.Empty;
        
			//Read file from folder
			
			//string strPath = HttpContext.Current.Server.MapPath ("/EMailTemplate");
			// Read the file as one string.
        	//string text = System.IO.File.ReadAllText(@"..\EmailTemplate\BD_ReminderEmail.txt");
			
			//Get content 
			
        	
        	return strEmailContent;
        }
        
        public string MailSubjectLine(string strSubjectLine)
        {
            return strSubjectLine;
        }
        
        public string EMailHeader()
        {
            string strHeader = "";

            sbEMailHeader = new StringBuilder();
            
            sbEMailHeader.AppendLine("Dear Participant,");
            sbEMailHeader.AppendLine("");
            sbEMailHeader.AppendLine("");
            sbEMailHeader.AppendLine("Congratulations for being successfully registered for the Tournament.");
            sbEMailHeader.AppendLine("");

            strHeader = sbEMailHeader.ToString();

            return strHeader;
        }

        public string EMailFooter()
        {
            string strFooter = "";
            sbEMailFooter = new StringBuilder();
            
            sbEMailFooter.AppendLine("We request you to kindly go through the terms and conditions before participating in the tournament.");
            sbEMailFooter.AppendLine("");
            sbEMailFooter.AppendLine("Keep visiting out website for all the updates regarding the tournament");
            sbEMailFooter.AppendLine("");
            //sb.AppendLine("You can also follow us on Facebook | Instagram | Twitter - @thesportfit/<eventpage>");            
            //sb.AppendLine("");
            //sb.AppendLine("For queries / clarifications, email us at: youngshuttlers@sportfit.co.in");            
            //sb.AppendLine("");
            sbEMailFooter.AppendLine("We wish you all the best for the tournament!");
            sbEMailFooter.AppendLine("");
            
            sbEMailFooter.AppendLine("Thanking you,");            
            sbEMailFooter.AppendLine("");
            sbEMailFooter.AppendLine("Best Regards,");            
            //sbEMailFooter.AppendLine("");
            sbEMailFooter.AppendLine("Team TournamentPlanner.in");            
            //sbEMailFooter.AppendLine("Sportft Crest and Wellness Pvt Ltd");
            //sbEMailFooter.AppendLine("www.sportfit.co.in");
            //sbEMailFooter.AppendLine("");
            
            strFooter = sbEMailFooter.ToString();

            return strFooter;
        }

        public void Email_Tournament_Onboarding(string strHTMLEmailBody,
                                                string strTournamentCode, 
                                                string strTournamentName,
                                                string strTournamentOrganization,
                                                string strOwnerName, string strOwnerMobileNo, string strOwnerEmailID)
        {
            string strEMailBody = string.Empty;

            string strEmailSubject = "Tournament: " + strTournamentCode + " created successfully";

            strHTMLEmailBody = strHTMLEmailBody.Replace("{UserName}", strOwnerName); //replacing the required things  
            strHTMLEmailBody = strHTMLEmailBody.Replace("{TournamentName}", strTournamentName);
            strHTMLEmailBody = strHTMLEmailBody.Replace("{TournamentOrganization}", strTournamentOrganization);
            strHTMLEmailBody = strHTMLEmailBody.Replace("{TournamentCode}", strTournamentCode);
            strHTMLEmailBody = strHTMLEmailBody.Replace("{UserCode}", strOwnerEmailID);
            strHTMLEmailBody = strHTMLEmailBody.Replace("{pwd}", strOwnerMobileNo);

            MGCommon.MGGeneric.MGEMailServices(strOwnerEmailID, strEmailSubject, strHTMLEmailBody);

        }


        public string TournamentRegistrationMailBody(string strFirstName, string strLastName, 
                                                     string strMobileNo,string strEventCategory,string strTotalAmountPaid,
                                                     string strTournamentCode, string strPlayerCode,
                                                    string strPlayerDOB, string strPaymentStatus)
        {
            string strEMailBody = string.Empty;

            StringBuilder sb = new StringBuilder();
            
            sb.AppendLine("You are registered under the below mentioned categories:");
            sb.AppendLine("");           
            sb.AppendLine("Participation Details:");
            
            sb.AppendLine("Player Name: " + strFirstName + " " + strLastName);
            sb.AppendLine("Mobile: " + strMobileNo);
            sb.AppendLine("Date Of Birth: " + strPlayerDOB);
            sb.AppendLine("Participated Events: ");
            sb.AppendLine(strEventCategory);
            sb.AppendLine("Total amount: Rs. " + strTotalAmountPaid +" - " + strPaymentStatus);
			sb.AppendLine("");            
			sb.AppendLine("Click the link to track the payment status: http://tournamentplanner.in/screens/TP_PaymentPlayersParticipation.aspx?TCode=" + strTournamentCode + "&PlayerCode=" + strPlayerCode);
            sb.AppendLine("");
                        
            strEMailBody = EMailHeader() + sb.ToString();
            
            return strEMailBody;        
        }
        
        public string PaymentReminderMailBody(string strPlayerName, string strMobileNo, 
                                                    string strTournamentCode, string strPlayerCode,
                                                    string strPlayerDOB, string strPaymentStatus)
        {
            string strEMailBody = string.Empty;

            
            string strEmailContent = GetEmailContentFromFile("BD_ReminderEmail.txt");
            
            StringBuilder sb = new StringBuilder();
            
            sb.AppendLine("Your registration will be completed after the completion of payment.");                       
            sb.AppendLine("");
            sb.AppendLine("Participation Details:");
            sb.AppendLine("Player Name: " + strPlayerName);
            sb.AppendLine("Mobile: " + strMobileNo);
            sb.AppendLine("Date Of Birth: " + strPlayerDOB);
            sb.AppendLine("");
            sb.AppendLine("Click the link to track the payment status: http://tournamentplanner.in/screens/TP_PaymentPlayersParticipation.aspx?TCode=" + strTournamentCode + "&PlayerCode=" + strPlayerCode);
            sb.AppendLine("");
            
            strEMailBody = EMailHeader() + sb.ToString();

            return strEMailBody;        
        }
        
        public string ConfirmPaymentMailBody(string strPlayerName, string strMobileNo, 
                                                    string strTournamentCode, string strPlayerCode,
                                                    string strPlayerDOB, string strPaymentStatus)
        {
            string strEMailBody = string.Empty;

            StringBuilder sb = new StringBuilder();
            
            sb.AppendLine("This is to confirm that we have received the payment and your registration is confirmed.");
            sb.AppendLine("");
            
            sb.AppendLine("Participation Details:");

            sb.AppendLine("Player Name: " + strPlayerName);
            sb.AppendLine("Mobile: " + strMobileNo);
            sb.AppendLine("Date Of Birth: " + strPlayerDOB);
            sb.AppendLine("");
            sb.AppendLine("Click the link to track the payment status: http://tournamentplanner.in/screens/TP_PaymentPlayersParticipation.aspx?TCode=" + strTournamentCode + "&PlayerCode=" + strPlayerCode);
            sb.AppendLine("");
            
            strEMailBody = EMailHeader() + sb.ToString();

            return strEMailBody;        
        }
        
        public string Notification_DrawsANDScheduleMailBody()
        {
            string strEMailBody = string.Empty;

            StringBuilder sb = new StringBuilder();
            
            sb.AppendLine("Draws and Schedule has been published.");
            sb.AppendLine("");
			            
            strEMailBody = EMailHeader() + sb.ToString();

            return strEMailBody;        
        }
        
       public string SendFailedPaymentMailBody(string strPlayerName, string strMobileNo, 
                                                    string strTournamentCode, string strPlayerCode,
                                                    string strPlayerDOB, string strPaymentStatus)
        {
            string strEMailBody = string.Empty;

            StringBuilder sb = new StringBuilder();
                            
            sb.AppendLine("Your last payment attempt was failed or cancelled.");
            sb.AppendLine("");           
            sb.AppendLine("Request you to please complete the payment to confirm your registration.");
            sb.AppendLine("");           
            
            sb.AppendLine("Participation Details:");
            
            sb.AppendLine("Player Name: " + strPlayerName);
            sb.AppendLine("Mobile: " + strMobileNo);
            sb.AppendLine("Date Of Birth: " + strPlayerDOB);
            sb.AppendLine("");

            sb.AppendLine("Click the link to track the payment status: http://tournamentplanner.in/screens/TP_PaymentPlayersParticipation.aspx?TCode=" + strTournamentCode + "&PlayerCode=" + strPlayerCode);
            sb.AppendLine("");
                        
            strEMailBody = EMailHeader() + sb.ToString();

            return strEMailBody;        
        }
 

       	public string SignUpMailBody(string strUserID, string strMobileNo, string strFirstName, string strLastName)
        {
            string strEMailBody = string.Empty;

            StringBuilder sb = new StringBuilder();
            string strDearUser = "Dear " + strFirstName + " " + strLastName + ",";
            sb.AppendLine(strDearUser);
            sb.AppendLine("");
            sb.AppendLine("Thank you for your relationship with PropDial.");
            sb.AppendLine("");
            sb.AppendLine("You have been registered with the following information:");
            sb.AppendLine("");
                        
            sb.AppendLine("User ID: " + strUserID);
            sb.AppendLine("EMail ID: " + strUserID);
            sb.AppendLine("Mobile: " + strMobileNo);

            sb.AppendLine("");
            sb.AppendLine("Your account is currently Inactive. PropDial Executive will reach you soon for account verification, accordingly your account will be activated.");
            sb.AppendLine("");
            sb.AppendLine("Please reach us at: 1-800-555-9999 for account verification and activation.");
            sb.AppendLine("");
            sb.AppendLine("Sincerely,");
            sb.AppendLine("PropDial – Marketing & Sales Team");

            strEMailBody = sb.ToString();

            return strEMailBody;        
        }

        public string ForgotPwdMailSubject()
        {
            return "PropDial - Reset Password";
        }

        public string ForgotPwdMailBody(string strPwd)
        {
            string strEMailBody = string.Empty;

            StringBuilder sb = new StringBuilder();

            //Add mail header
            sb.AppendLine(EMailHeader());

            sb.AppendLine("Your password has been reset as: " + strPwd);                                    
            sb.AppendLine("");
            sb.AppendLine("Once you login with this new password, Please change password immediately.");
            
            //Add Mail Footer
            sb.AppendLine(EMailFooter());            

            strEMailBody = sb.ToString();

            return strEMailBody;
        }

        public string ChangePwdMailSubject()
        {
            return "PropDial - Change Password";
        }

        public string ChangePwdMailBody()
        {
            string strEMailBody = string.Empty;

            StringBuilder sb = new StringBuilder();
            //Add mail header
            sb.AppendLine(EMailHeader());

            sb.AppendLine("Your password has been changed. Please login with your new password.");            
            
            //Add Mail Footer
            sb.AppendLine(EMailFooter());            

            strEMailBody = sb.ToString();

            return strEMailBody;
        }

        public string UserNotificationMailSubject()
        {
            return "PropDial - Notification Updates";
        }

        public string UserNotificationMailBody()
        {
            string strEMailBody = string.Empty;

            StringBuilder sb = new StringBuilder();
            
            //Add mail header
            sb.AppendLine(EMailHeader());

            sb.AppendLine("Your notifications updated successfully.");

            //Add Mail Footer
            sb.AppendLine(EMailFooter());            

            strEMailBody = sb.ToString();

            return strEMailBody;
        }

        public string UserProfileMailSubject()
        {
            return "PropDial - Profile Updates";
        }

        public string UserProfileMailBody()
        {
            string strEMailBody = string.Empty;

            StringBuilder sb = new StringBuilder();

            //Add mail header
            sb.AppendLine(EMailHeader());

            sb.AppendLine("Your profile updated successfully.");

            //Add Mail Footer
            sb.AppendLine(EMailFooter());            

            strEMailBody = sb.ToString();

            return strEMailBody;
        }

        public string UserActivationMailSubject()
        {
            return "PropDial - User Account Activation";
        }

        public string UserActivationMailBody()
        {
            string strEMailBody = string.Empty;

            StringBuilder sb = new StringBuilder();

            //Add mail header
            sb.AppendLine(EMailHeader());

            sb.AppendLine("Your account is activated successfully. Please login with your registered email id.");
            
            //Add Mail Footer
            sb.AppendLine(EMailFooter());

            strEMailBody = sb.ToString();

            return strEMailBody;
        }

        public string PropertySearchCallerAgentMailBody(string strMailBody)
        {
            string strEMailBody = string.Empty;

            StringBuilder sb = new StringBuilder();
            //Add mail header
            sb.AppendLine(EMailHeader());

            //Mail body
            sb.AppendLine(strMailBody);

            //Add Mail Footer
            sb.AppendLine(EMailFooter());            

            strEMailBody = sb.ToString();

            return strEMailBody;
        }

        public string PropertySearchOwnerAgentMailBody(string strCallerAgentName, string strCallerAgentPhone, string strMailBody)
        {
            string strEMailBody = string.Empty;

            StringBuilder sb = new StringBuilder();            
            
            //Add mail header
            sb.AppendLine(EMailHeader());

            sb.AppendLine("This is to inform you that your property has been searched by: ");            
            sb.AppendLine("Name: " + strCallerAgentName);
            sb.AppendLine("Mobile: " + strCallerAgentPhone);
            sb.AppendLine("");
            sb.AppendLine(strMailBody);
            
            //Add Mail Footer
            sb.AppendLine(EMailFooter());            

            strEMailBody = sb.ToString();

            return strEMailBody;
        }            
    }
    
    #endregion    
}
