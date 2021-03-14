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
using System.Web.UI.HtmlControls;
//using RestSharp;

namespace Login_demo
{
    public partial class Login : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
        DataTable dt = new DataTable();
        SqlCommand cmd;
        SqlDataAdapter adp = new SqlDataAdapter();
        string randomNumber = "";
        //string Cust_No = "12345";
        //string Uname = "Rswain";
        //string Mobile_no = "0263936";
        // HttpCookie myCookie;


        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Page.IsPostBack)
            //{
            lblMsg.Visible = false;
            txtOTP.Visible = false;
            verifyOTP.Visible = false;
            HyperLink1.Visible = false;
            hylnNext.Visible = false;
            //txtPhone.Text = string.Format("997") + Mobile_no;//.Substring(Mobile_no.Length - 4, 4);
            //}
        }

        public void InsertOTPinDB()
        {
            string query = "INSERT INTO OTPhistorytbl (Customer_No,User_Id,OTP,ctime_Stamp,Status) VALUES ('" + txtPhone.Text + "','" + txtUser.Text.ToString() + "','" + randomNumber + "','" + System.DateTime.Now.ToString() + "','" + "0".ToString() + "')";
            cmd = new SqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            txtOTP.Visible = true;
            verifyOTP.Visible = true;
            HyperLink1.Visible = true;

        }
        protected void Btnsubmit_Click(object sender, EventArgs e)
        {
            lblMsg.Visible = true;

            if (txtUser.Text == "")
            {
                lblMsg.Text = "Enter UserName";
            }
            else if (txtPassword.Text == "")
            {
                lblMsg.Text = "Enter Password";
            }
            else if (txtPhone.Text == "")
            {
                lblMsg.Text = "Enter Your Mobile Number";
            }
            else
            {
                try
                {
                    //lblMsg.Visible = true;

                    adp = new SqlDataAdapter("SELECT COUNT(*) FROM Login_Check_Sp WHERE Username='" + txtUser.Text + "' AND Password='" + txtPassword.Text + "'", con);

                    adp.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        //HttpCookie userCookie = new HttpCookie("myCookie");
                        //userCookie.Values.Add("customerID", txtPhone.Text);
                        //userCookie.Expires = DateTime.Now.AddHours(24);
                        //Response.Cookies.Add(userCookie);
                        //myCookie = Request.Cookies["myCookie"];
                        //if (myCookie == null)
                        //{
                        //    // No cookie found or cookie expired. :(
                        //   // txtPhone.Text = txtPhoneNo.Text;
                        //   //txtPhoneNo.Text = txtPhone.Text;
                        //   // ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#send_OTP').modal('show');</script>", false);

                        //}

                        // Verify the cookie value
                        //if (!string.IsNullOrEmpty(myCookie.Values["customerID"]))  // userId is found
                        //{

                        //ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#send_OTP').modal('show');</script>", false);
                        //string apiKey = "oz55vkpu3hfy6h0";//"l0xtk2g2z1tp1v2";
                        //string apisecret = "zj9sdn3ptay9ubb"

                        //string userId = myCookie.Values["customerID"].ToString();
                        String result;
                        String name = txtUser.Text;
                        string apiKey = "UGquzA0tm69hoC5BvO7WbLFcyTVEpiQXsfZ48IHMnNP2e3rlxYaDyQI3Z5WN8UP9eoczqMJtjEibBV1A";
                        string route = "v3";
                        string language = "english";
                        string flash = "0";
                        string numbers = txtPhone.Text.ToString();
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
                            //lblMsg.Text = eX.Message;
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
                        //lblMsg.Text = result;

                        // ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#Receive_OTP').modal('show');</script>", false);
                        // }

                        //if (Request.Cookies["customerID"] != null)
                        //{
                        //    // This will delete the cookie userId
                        //    Response.Cookies["customerID"].Expires = DateTime.Now.AddDays(-1);
                        //    lblMsg.Text = "You are logged in successfully";
                        //}

                    }
                    else
                    {
                        lblMsg.Text = "User Does Not exist          ";
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
            lblMsg.Visible = true;
            if (txtOTP.Text == "")
                lblMsg.Text = "Enter One Time Password (OTP)";
            else
            {
                adp = new SqlDataAdapter("SELECT ctime_Stamp FROM OTPhistorytbl WHERE OTP='" + txtOTP.Text + "' and Status='0'", con);
                adp.Fill(dt);
                DateTime OtpCrtDate = Convert.ToDateTime(dt.Rows[0][0].ToString());
                //randomNumber= Convert.ToString(dt.Rows[0][0].ToString());
                //DateTime OtpCrtDate = DateTime.Now.AddDays(-1);


                if (txtOTP.Text != randomNumber)
                {
                    TimeSpan timeSub = DateTime.Now - OtpCrtDate;
                    if (timeSub.TotalMinutes < 300)
                    {
                        //cmd = new SqlCommand("update OTPhistorytbl set Status='1' where Customer_No='" + txtPhone.Text + "'", con);
                        cmd = new SqlCommand("update OTPhistorytbl set Status='1' where OTP='" + txtOTP.Text + "'", con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        lblMsg.Text = "You are logged in successfully";
                        con.Close();
                    }
                    else
                    {
                        ////cmd = new SqlCommand("update OTPhistorytbl set Status='0' where Customer_No='" + txtPhone.Text + "'", con);
                        //cmd = new SqlCommand("Delete from OTPhistorytbl where OTP='" + txtOTP.Text + "'", con);
                        //con.Open();
                        //cmd.ExecuteNonQuery();
                        //con.Close();
                        lblMsg.Text = "Your OTP has expired. Log In again.";
                    }
                }
                else
                {
                    //cmd = new SqlCommand("Delete from OTPhistorytbl where OTP='" + txtOTP.Text + "'", con);
                    //con.Open();
                    //cmd.ExecuteNonQuery();
                    //con.Close();
                    lblMsg.Text = "Your OTP is Invalid. Try again.";

                }
            }
            txtOTP.Text = "";
        }
    }
}