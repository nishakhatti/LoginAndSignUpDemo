using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Login_demo
{
    public partial class SignUpOTP : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
        DataTable dt = new DataTable();
        SqlCommand cmd;
        SqlDataAdapter adp = new SqlDataAdapter();
        string randomNumber = "";
        //string Cust_No = "12345";
        //string Uname = "Rswain";
        //string Mobile_no = "0263936";
        //HttpCookie myCookie;


        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Page.IsPostBack)
            //{
            lblMsgSU.Visible = false;
            txtValidOTP.Visible = false;
            btnVaildOTP.Visible = false;
            HyperLink2.Visible = false;
            hylnNext.Visible = false;
            //txtPhone.Text = string.Format("997") + Mobile_no;//.Substring(Mobile_no.Length - 4, 4);
            //}
        }

        public void InsertOTPinDB()
        {
            string query = "INSERT INTO [OTPhistorytbl] (Customer_No,User_Id,OTP,ctime_Stamp,Status) VALUES ('" + txtMobNo.Text + "','" + txtName.Text.ToString() + "','" + randomNumber + "','" + System.DateTime.Now.ToString() + "','" + "0".ToString() + "')";
            query += "       INSERT INTO [Login_Check_SP] (Username,Password,MobileNo) VALUES ('" + txtName.Text + "','" + txtPass.Text + "','" + txtMobNo.Text + "')";
            cmd = new SqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            txtValidOTP.Visible = true;
            btnVaildOTP.Visible = true;
            HyperLink2.Visible = true;

        }
        public bool FieldValidation(bool result)
        {
            result = true;

            if (txtName.Text == "")
            {
                lblMsgSU.Text = "Enter your Full Name";
                result = false;
                return result;
            }
            if (txtPass.Text == "")
            {
                lblMsgSU.Text = "Enter New Password";
                result = false;
                return result;
            }
            if (txtConPass.Text == "")
            {
                lblMsgSU.Text = "Enter Confirm Password";
                result = false;
                return result;
            }
            if (txtPass.Text != txtConPass.Text)
            {
                lblMsgSU.Text = "Entered Password doesnot match";
                result = false;
                return result;
            }
            if (txtMobNo.Text == "")
            {
                lblMsgSU.Text = "Enter your Mobile no";
                result = false;
                return result;
            }
            return result;
        }
        protected void Btnsubmit_Click(object sender, EventArgs e)
        {
            lblMsgSU.Visible = true;
            //bool rst = new bool();
            //bool valid= new bool();
            //FieldValidation(valid);
            //valid = rst;

            if (txtName.Text == "")
            {
                lblMsgSU.Text = "Enter your Full Name";
            }
            else if (txtPass.Text == "")
            {
                lblMsgSU.Text = "Enter New Password";
            }
            else if (txtConPass.Text == "")
            {
                lblMsgSU.Text = "Enter Confirm Password";
            }
            else if (txtPass.Text != txtConPass.Text)
            {
                lblMsgSU.Text = "Confirm Password doesnot match";
            }
            else if (txtMobNo.Text == "")
            {
                lblMsgSU.Text = "Enter your Mobile no";
            }
            else
            {
                try
                {
                    //lblMsg.Visible = true;

                    adp = new SqlDataAdapter("SELECT COUNT(*) FROM Login_Check_Sp WHERE Username='" + txtName.Text + "' AND Password='" + txtPass.Text + "'", con);

                    adp.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "0")
                    {
                        //HttpCookie userCookie = new HttpCookie("myCookie");
                        //userCookie.Values.Add("customerID", txtMobNo.Text);
                        //userCookie.Expires = DateTime.Now.AddHours(24);
                        //Response.Cookies.Add(userCookie);
                        //myCookie = Request.Cookies["myCookie"];
                        //if (myCookie == null)
                        //{
                        //    // No cookie found or cookie expired. :(
                        //    // txtPhone.Text = txtPhoneNo.Text;
                        //    //txtPhoneNo.Text = txtPhone.Text;
                        //    // ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#send_OTP').modal('show');</script>", false);

                        //}

                        // Verify the cookie value
                        //if (!string.IsNullOrEmpty(myCookie.Values["customerID"]))  // userId is found
                        //{

                        //ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#send_OTP').modal('show');</script>", false);
                        //string apiKey = "oz55vkpu3hfy6h0";//"l0xtk2g2z1tp1v2";
                        //string apisecret = "zj9sdn3ptay9ubb"

                        //string userId = myCookie.Values["customerID"].ToString();
                        String result;
                        String name = txtName.Text;
                        string apiKey = "UGquzA0tm69hoC5BvO7WbLFcyTVEpiQXsfZ48IHMnNP2e3rlxYaDyQI3Z5WN8UP9eoczqMJtjEibBV1A";
                        string route = "v3";
                        string language = "english";
                        string flash = "0";
                        string numbers = txtMobNo.Text.ToString();
                        Random rnd = new Random();
                        randomNumber = (rnd.Next(100000, 999999)).ToString();
                        string message = "Hey%20" + name + "%20you%20OTP%20is%20" + randomNumber;
                        string send = "TXTIND";

                        String url = "https://www.fast2sms.com/dev/bulkV2?authorization=" + apiKey + "&route=" + route + "&sender_id=" + send + "&message_text=" + message + "&language=" + language + "&flash=" + flash + "&numbers=" + numbers;


                        //String url = "https://api.txtlocal.in/send/?apikey=" + apiKey + "&numbers=" + numbers + "&message=" + message + "&sender=" + send;
                        //String url = "https://www.thetexting.com/rest/sms/json/Message/Send?api_key=" + apiKey + "&api_secret=" + apisecret + "&from=" + send + "&to=" + "91" + numbers + "&text=" + message + "&type=text";
                        // String url = "http://cloud.smsindiahub.in/vendorsms/pushsms.aspx?user=nisha.khatti88@gmail.com&password=Nishak@88&msisdn=91" + txtPhone.Text + "&sid=NK&msg="+ message + "&fl=0";


                        StreamWriter myWriter = null;
                        HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
                        objRequest.Method = "POST";
                        objRequest.ContentLength = Encoding.UTF8.GetByteCount(url);
                        objRequest.ContentType = "application/x-www-form-urlencoded";
                        try
                        {
                            myWriter = new StreamWriter(objRequest.GetRequestStream());
                            myWriter.Write(url);
                            InsertOTPinDB();
                        }
                        catch (Exception eX)
                        {
                            // lblMsgSU.Text = eX.Message;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Following error occured : " + eX.Message.ToString() + "');", true);
                        }
                        finally
                        {
                            myWriter.Close();
                        }
                        WebClient client = new WebClient();
                        client.Credentials = CredentialCache.DefaultCredentials;
                        Stream data = client.OpenRead(new Uri(url.Trim()));

                        //StreamReader reader = new StreamReader(data);
                        //HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
                        //using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))

                        using (StreamReader sr = new StreamReader(data))
                        {
                            result = sr.ReadToEnd();
                            sr.Close();
                        }
                        //lblMsgSU.Text = result;

                        // ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#Receive_OTP').modal('show');</script>", false);
                        //}

                        //if (Request.Cookies["customerID"] != null)
                        //{
                        //    // This will delete the cookie userId
                        //    Response.Cookies["customerID"].Expires = DateTime.Now.AddDays(-1);
                        //    lblMsgSU.Text = "You are logged in successfully";
                        //}

                    }
                    else
                    {
                        lblMsgSU.Text = "User already exist             ";
                        hylnNext.Visible = true;
                    }
                }
                catch (Exception ex)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Following error occured : " + ex.Message.ToString() + "');", true);
                    // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
                }
                finally
                {
                    dt.Clear();
                    dt.Dispose();
                    adp.Dispose();
                }
            }
        }
        protected void btnSendOTP_Click(object sender, EventArgs e)
        {
            //if (txtPhoneNo.Text == "")
            //    lblSend.Text = "Enter Valid No";
            //txtPhone.Text = txtPhoneNo.Text;
            //  ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#Receive_OTP').modal('show');</script>", false);
        }

        protected void ValidateOTP_Click(object sender, EventArgs e)
        {
            lblMsgSU.Visible = true;
            if (txtValidOTP.Text == "")
                lblMsgSU.Text = "Enter One Time Password (OTP)";
            else
            {
                adp = new SqlDataAdapter("SELECT ctime_Stamp FROM OTPhistorytbl WHERE OTP='" + txtValidOTP.Text + "' and Status='0'", con);
                adp.Fill(dt);
                DateTime OtpCrtDate = Convert.ToDateTime(dt.Rows[0][0].ToString());
                //randomNumber= Convert.ToString(dt.Rows[0][0].ToString());
                //DateTime OtpCrtDate = DateTime.Now.AddDays(-1);


                if (txtValidOTP.Text != randomNumber)
                {
                    TimeSpan timeSub = DateTime.Now - OtpCrtDate;
                    if (timeSub.TotalMinutes < 300)
                    {
                        //cmd = new SqlCommand("update OTPhistorytbl set Status='1' where Customer_No='" + txtPhone.Text + "'", con);
                        cmd = new SqlCommand("update OTPhistorytbl set Status='1' where OTP='" + txtValidOTP.Text + "'", con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        lblMsgSU.Text = "signed up successfully!!!           ";
                        con.Close();
                        hylnNext.Visible = true;
                    }
                    else
                    {
                        ////cmd = new SqlCommand("update OTPhistorytbl set Status='0' where Customer_No='" + txtPhone.Text + "'", con);
                        //cmd = new SqlCommand("Delete from OTPhistorytbl where OTP='" + txtOTP.Text + "'", con);
                        //con.Open();
                        //cmd.ExecuteNonQuery();
                        //con.Close();
                        lblMsgSU.Text = "Your OTP has expired. Log In again.";
                    }
                }
                else
                {
                    //cmd = new SqlCommand("Delete from OTPhistorytbl where OTP='" + txtOTP.Text + "'", con);
                    //con.Open();
                    //cmd.ExecuteNonQuery();
                    //con.Close();
                    lblMsgSU.Text = "Your OTP is Invalid. Try again.";

                }
            }
            txtValidOTP.Text = "";
        }
    }
}