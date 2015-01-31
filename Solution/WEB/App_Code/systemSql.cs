using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.IO;
using System.Web.Util;
using System.Web.Mail;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using System.Data.OleDb;
namespace SystemSql
{
   public class  sql_str
   {        
    
      String StrCon = System.Configuration.ConfigurationSettings.AppSettings["SqlConnStr"];
    SqlConnection cn; 	
		
	
		public string conn_str {
		get{
	       	return StrCon;
		 }
		}
		
      public string sql_Execute(string sqlstr) {
       string tt="";
      
          cn = new SqlConnection(StrCon); 
           cn.Open();
           SqlCommand cm=new SqlCommand(sqlstr,cn);
					try
					{
					cm.ExecuteNonQuery();
                tt="操作成功!";
					}
					catch (SqlException e) 
					{
						tt="操作失败!"+"<br>"+e;
                        throw (e);
					}
					finally
					{
						cn.Close();
                        
					} 
				
					return tt ;
	
	       }

       public int SQLExecuteNonQuery(string sqlstr)
       {
        int tt = -1;
        
               cn = new SqlConnection(StrCon);
               cn.Open();
               SqlCommand cm = new SqlCommand(sqlstr, cn);
               try
               {
                   tt =cm.ExecuteNonQuery();
               }
               catch (SqlException e)
               {
                   throw (e);
                // string js = @"alert(操作失败!" + e + "');";
                  // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "操作失败", js, true);
               }
               finally
               {
                   cn.Close();
                   
              
           }
           return tt;

       }


       public string SQLExecuteScalar(string sqlstr)
       {
          string  tt = "";

         
               cn = new SqlConnection(StrCon);
               cn.Open();
               SqlCommand cm = new SqlCommand(sqlstr, cn);
               try
               {
                   tt =cm.ExecuteScalar().ToString();

               }
               catch (SqlException e)
               {
                   throw (e);
                 //  string js = @"alert(操作失败!" + e + "');";
                 // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "操作失败", js, true);
               }
               finally
               {
                   cn.Close();
                   
               }
          
           return tt;

       }
		  

		   //执行select
		   public DataSet sql_select(string sqlstr) {
		   
		  // String  StrCon= System.Configuration.ConfigurationSettings.AppSettings["conn_mds"];
         cn = new SqlConnection(StrCon); 
		  DataSet    ds;
           try{
              SqlDataAdapter sqlAdapter1 = new SqlDataAdapter(sqlstr,cn);
              ds = new DataSet();
              sqlAdapter1.Fill(ds, "product");
            }
        catch (Exception ex){
          throw (ex);
      }
      finally{
          cn.Close();
          
         }  
		 
		   
		  return ds; 
        }

        //执行select
        public DataSet GetDataSet(string sqlstr)
        {

            // String  StrCon= System.Configuration.ConfigurationSettings.AppSettings["conn_mds"];
            cn = new SqlConnection(StrCon);
            DataSet ds;
            try
            {
                SqlDataAdapter sqlAdapter1 = new SqlDataAdapter(sqlstr, cn);
                ds = new DataSet();
                sqlAdapter1.Fill(ds, "product");
           
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                cn.Close();
                
            }

           
            return ds;
        } 


 public SqlDataAdapter da_select(string sqlstr) {
		   
		  // String  StrCon= System.Configuration.ConfigurationSettings.AppSettings["conn_mds"];
         cn = new SqlConnection(StrCon); 
		  SqlDataAdapter da;
           try{
              da = new SqlDataAdapter(sqlstr,cn);
            }
        catch (Exception ex){
          throw (ex);
      }
      finally{
          cn.Close();
          
         }  
		 
		 
		  return da; 
        } 

	   //计算记录数
	   public int RecordNum(string sql)
	   {
		   int intCount;
		   cn = new SqlConnection(StrCon); 
		   
		   SqlCommand MyComm = new SqlCommand(sql,cn);
		   cn.Open();
		   SqlDataReader dr = MyComm.ExecuteReader();
		   if(dr.Read())
		   {
			   intCount = Int32.Parse(dr["co"].ToString());
		   }
		   else
		   {
			   intCount = 0;
		   }
		    dr.Close();
		   cn.Close();
           
		   return intCount;
		  
	   }
//判断记录是否定在 存在是
public	bool  Validation(string strSQL)
		{
			cn=new SqlConnection(StrCon);
			cn.Open();
			bool tt;
			SqlCommand cm=new SqlCommand(strSQL,cn);
			SqlDataReader dr=cm.ExecuteReader();
			if(dr.Read())
			{
				tt=false;
			} 
			else 
			{
				tt=true;
			}
			cn.Close();
            
			return tt;
		}

        //判断记录是否定在 存在是
        public bool IsValidation(string strSQL)
        {
            cn = new SqlConnection(StrCon);
            cn.Open();
            bool tt;
            SqlCommand cm = new SqlCommand(strSQL, cn);
            SqlDataReader dr = cm.ExecuteReader();
            if (dr.Read())
            {
                tt = true;
            }
            else
            {
                tt = false;
            }
            cn.Close();
            
            return tt;
        }
		

		//返回一字值　
		public string strBack(string sql,int t)
		{
			string strSQL=sql,strback="";
			  DataTable dt;
			    dt=sql_select(sql).Tables[0];
			try{
			if(dt.Rows.Count>0){
		   
			 strback=dt.Rows[0][t].ToString();
			 }
			 }catch (Exception ex){ throw (ex);}
              finally{}
		  	
			return strback;
		}

        public SqlDataReader SqlDataReader(string strSQL)
		{
            SqlDataReader dr=null;
		  cn=new SqlConnection(StrCon);
		cn.Open();
		try{
			SqlCommand cm=new SqlCommand(strSQL,cn);
			 dr=cm.ExecuteReader();
			
        }catch{  }
        finally{
           //dr.Close();
            //cn.Close();
        }
	
        return dr;
		}
		
		
		
		public void DDL_DataBind(System.Web.UI.WebControls.DropDownList ddl,string strsql,string ValueField,string TextField)
		{
		    //string sqlstr="select keyId,sales_ditch from CRM_sales_ditch where release='1'";	
			DataTable aa=sql_select(strsql).Tables[0];
			ddl.DataValueField=aa.Columns[ValueField].ToString();
			ddl.DataTextField=aa.Columns[TextField].ToString();
			ddl.DataSource=aa;
			ddl.DataBind();
			
		}	
	public void DDL_DataBind(System.Web.UI.WebControls.DropDownList ddl,string strsql)
		{
		    //string sqlstr="select keyId,sales_ditch from CRM_sales_ditch where release='1'";	
			DataTable aa=sql_select(strsql).Tables[0];
			ddl.DataValueField=aa.Columns[0].ToString();
			ddl.DataTextField=aa.Columns[1].ToString();
			ddl.DataSource=aa;
			ddl.DataBind();
			
		}
		
		public void DDL_DataBind(System.Web.UI.WebControls.ListBox  ddl,string strsql,string ValueField,string TextField)
		{
		    //string sqlstr="select keyId,sales_ditch from CRM_sales_ditch where release='1'";	
			DataTable aa=sql_select(strsql).Tables[0];
			ddl.DataValueField=aa.Columns[ValueField].ToString();
			ddl.DataTextField=aa.Columns[TextField].ToString();
			ddl.DataSource=aa;
			ddl.DataBind();
			
		}		


	   //发送邮件(mailBody邮件正文，Subject邮件标题，ToMail目的邮箱,FromMail发件箱,Cc秒送,Bcc暗送) 
 public string SendMail(string mailBody,string Subject ,string ToMail,string FromMail,string Cc,string Bcc) 
		{string reTurn="";
        
					string strBody =mailBody;
                    System.Web.Mail.MailMessage msgMail = new System.Web.Mail.MailMessage();
					msgMail.To =ToMail; //邮件接受者 
					msgMail.Cc =Cc; 
					msgMail.Bcc = Bcc; 
					msgMail.From = FromMail+"<dzjb@donlim.com>"; 
					msgMail.Subject = Subject; 
					msgMail.Body = strBody;
                    msgMail.Priority = System.Web.Mail.MailPriority.High;
                    msgMail.BodyFormat = System.Web.Mail.MailFormat.Html; 
					msgMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1"); //basic authentication
                    msgMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "dzjb@donlim.com"); //set your username here
					msgMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "d061034b"); //set your password here
                    System.Web.Mail.SmtpMail.SmtpServer = "192.168.117.9";
                    try
                    {
                        System.Web.Mail.SmtpMail.Send(msgMail);
                    reTurn = "1";
                        //"  您的反馈内容已成功发送，\r 感谢您对我们的支持和关注。";
					}
                      catch (Exception ex){
                          reTurn = "0";
                          //"邮件发送失败，新稍后再发送，\r或直接写邮件到chenjingguo@donlim.com! \r 感谢您对我们的支持和关注。\r"+ex;  
	}
	
			return reTurn;
		
		}
		
  public string pwd(int format, string cleanString)//密码加密
		{
			Byte[] clearBytes;
			Byte[] hashedBytes;
            string salt="CalusyCRM";
	Encoding encoding = Encoding.GetEncoding(1252);

			clearBytes = encoding.GetBytes(salt.ToLower().Trim() + cleanString.Trim());

			switch (format)
			{
				case 0: //UserPasswordFormat.ClearText
					return cleanString;
				case 1: //UserPasswordFormat.Sha1Hash
					hashedBytes = ((HashAlgorithm) CryptoConfig.CreateFromName("SHA1")).ComputeHash(clearBytes);
					return BitConverter.ToString(hashedBytes);
				case 3:
				 hashedBytes = ((HashAlgorithm) CryptoConfig.CreateFromName("MD5")).ComputeHash(clearBytes);
				return BitConverter.ToString(hashedBytes);
					
				default:
					hashedBytes = ((HashAlgorithm) CryptoConfig.CreateFromName("MD5")).ComputeHash(clearBytes);
					return BitConverter.ToString(hashedBytes);
                    
			}
		}
		
		
		
public void ExportToFile(string fileName, System.Data.DataTable dt)
		{
			int colHeight = 19;
			int colWidth = 100;
			int colWidthpt = (int)(colWidth * 0.75f);
			int width = colWidth * dt.Columns.Count;
			int widthpt = (int)(width * 0.75f);
			ArrayList colArray = new ArrayList();
			StreamWriter sw = new StreamWriter(fileName, false, System.Text.Encoding.Default);
			try
			{
				sw.WriteLine("<html xmlns:o=\"urn:schemas-microsoft-com:office:office\"");
				sw.WriteLine("xmlns:x=\"urn:schemas-microsoft-com:office:excel\"");
				sw.WriteLine("xmlns=\"http://www.w3.org/TR/REC-html40\">");

				// 定义head
				sw.WriteLine("<head>");
				sw.WriteLine("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
				sw.WriteLine("<meta name=ProgId content=Excel.Sheet>");
				sw.WriteLine("<meta name=Generator content=\"Microsoft Excel 9\">");
				sw.WriteLine("<link rel=File-List href=\"./Book1.files/filelist.xml\">");
				sw.WriteLine("<link rel=Edit-Time-Data href=\"./Book1.files/editdata.mso\">");
				sw.WriteLine("<link rel=OLE-Object-Data href=\"./Book1.files/oledata.mso\">");
				sw.WriteLine("<style>");
				sw.WriteLine("<!--table");
				sw.WriteLine("{mso-displayed-decimal-separator:\"\\.\";");
				sw.WriteLine("mso-displayed-thousand-separator:\"\\,\";}");

				// 页边距
				sw.WriteLine("@page");
				sw.WriteLine("{margin:1.0in .75in 1.0in .75in;");
				sw.WriteLine(" mso-header-margin:.5in;");
				sw.WriteLine(" mso-footer-margin:.5in;}");

				sw.WriteLine("tr");
				sw.WriteLine("{mso-height-source:auto;");
				sw.WriteLine(" mso-ruby-visibility:none;}");

				sw.WriteLine("col");
				sw.WriteLine("{mso-width-source:auto;");
				sw.WriteLine(" mso-ruby-visibility:none;}");

				sw.WriteLine("br");
				sw.WriteLine("{mso-data-placement:same-cell;}");

				sw.WriteLine(".style0");
				sw.WriteLine("{mso-number-format:General;");
				sw.WriteLine(" text-align:general;");
				sw.WriteLine(" vertical-align:bottom;");
				sw.WriteLine(" white-space:nowrap;");
				sw.WriteLine(" mso-rotate:0;");
				sw.WriteLine(" mso-background-source:auto;");
				sw.WriteLine(" mso-pattern:auto;");
				sw.WriteLine(" color:windowtext;");
				sw.WriteLine(" font-size:12.0pt;");
				sw.WriteLine(" font-weight:400;");
				sw.WriteLine(" font-style:normal;");
				sw.WriteLine(" text-decoration:none;");
				sw.WriteLine(" font-family:宋体;");
				sw.WriteLine(" mso-generic-font-family:auto;");
				sw.WriteLine(" mso-font-charset:134;");
				sw.WriteLine(" border:none;");
				sw.WriteLine(" mso-protection:locked visible;");
				sw.WriteLine(" mso-style-name:常规;");
				sw.WriteLine(" mso-style-id:0;}");

				sw.WriteLine("td");
				sw.WriteLine("{mso-style-parent:style0;");
				sw.WriteLine(" padding-top:1px;");
				sw.WriteLine(" padding-right:1px;");
				sw.WriteLine(" padding-left:1px;");
				sw.WriteLine(" mso-ignore:padding;");
				sw.WriteLine(" color:windowtext;");
				sw.WriteLine(" font-size:12.0pt;");
				sw.WriteLine(" font-weight:400;");
				sw.WriteLine(" font-style:normal;");
				sw.WriteLine(" text-decoration:none;");
				sw.WriteLine(" font-family:宋体;");
				sw.WriteLine(" mso-generic-font-family:auto;");
				sw.WriteLine(" mso-font-charset:134;");
				sw.WriteLine(" mso-number-format:General;");
				sw.WriteLine(" text-align:general;");
				sw.WriteLine(" vertical-align:middle;");
				sw.WriteLine(" border:none;");
				sw.WriteLine(" mso-background-source:auto;");
				sw.WriteLine(" mso-pattern:auto;");
				sw.WriteLine(" mso-protection:locked visible;");
				sw.WriteLine(" white-space:nowrap;");
				sw.WriteLine(" mso-rotate:0;}");

				sw.WriteLine(".x124"); // 表头
				sw.WriteLine("{mso-style-parent:style0;");
				sw.WriteLine(" text-align:center;}");

				sw.WriteLine(".x125");// 数字
				sw.WriteLine("{mso-style-parent:style0;");
				sw.WriteLine(" text-align:right;}");


				sw.WriteLine(".x126");// 日期
				sw.WriteLine("{mso-style-parent:style0;");
				sw.WriteLine(" mso-number-format:\"Short Date\";");
				sw.WriteLine(" text-align:right;}");

				sw.WriteLine("ruby");
				sw.WriteLine("{ruby-align:left;}");

				sw.WriteLine("rt");
				sw.WriteLine("{color:windowtext;");
				sw.WriteLine(" font-size:9.0pt;");
				sw.WriteLine(" font-weight:400;");
				sw.WriteLine(" font-style:normal;");
				sw.WriteLine(" text-decoration:none;");
				sw.WriteLine(" font-family:宋体;");
				sw.WriteLine(" mso-generic-font-family:auto;");
				sw.WriteLine(" mso-font-charset:134;");
				sw.WriteLine(" mso-char-type:none;");
				sw.WriteLine(" display:none;}");

				sw.WriteLine("-->");
				sw.WriteLine("</style>");

				// excel 的属性
				sw.WriteLine("<!--[if gte mso 9]><xml>");
				sw.WriteLine("<x:ExcelWorkbook>");
				sw.WriteLine("<x:ExcelWorksheets>");
				sw.WriteLine("<x:ExcelWorksheet>");
				sw.WriteLine("<x:Name>" + dt.TableName+ "</x:Name>");// 表单的名称
				sw.WriteLine("<x:WorksheetOptions>");
				sw.WriteLine("<x:DefaultRowHeight>285</x:DefaultRowHeight>");
				sw.WriteLine("<x:Selected/>");
				sw.WriteLine("<x:ProtectContents>False</x:ProtectContents>");
				sw.WriteLine("<x:ProtectObjects>False</x:ProtectObjects>");
				sw.WriteLine("<x:ProtectScenarios>False</x:ProtectScenarios>");
				sw.WriteLine("</x:WorksheetOptions>");
				sw.WriteLine("</x:ExcelWorksheet>");
				sw.WriteLine("</x:ExcelWorksheets>");
				sw.WriteLine("</x:ExcelWorkbook>");
				sw.WriteLine("</xml><![endif]-->");
				sw.WriteLine("</head>");

				// 定义body
				sw.WriteLine("<body link=blue vlink=purple>");
				sw.WriteLine("");
				sw.WriteLine("<table x:str border=0 cellpadding=0 cellspacing=0 width=" + width.ToString() + " style='border-collapse:");
				sw.WriteLine(" collapse;table-layout:fixed;width:" + widthpt.ToString() + "pt'>");

				sw.WriteLine("<col width=" + colWidth.ToString() + " span=" + dt.Columns.Count.ToString() + " style='mso-width-source:userset;width:" + colWidthpt.ToString() + "pt'>");

				// 生成表头
				sw.WriteLine("<tr height=" + colHeight.ToString() + ">");
				foreach(DataColumn dc in dt.Columns)
				{
					colArray.Add(dc.DataType);
					sw.WriteLine("<td class=x124 height=" + colHeight.ToString() + " width = " + colWidth.ToString() + ">" + dc.ColumnName + "</td>");
				}
				sw.WriteLine("</tr>");

				foreach(DataRow dr in dt.Rows)
				{
					sw.WriteLine("<tr height="+ colHeight.ToString() +">");
					for(int i=0; i<dr.ItemArray.Length; i++)
					{
						this.GetStyle((Type)colArray[i], sw, dr.ItemArray[i]);
					}
				}

				sw.WriteLine("</table>");
				sw.WriteLine("</body>");

				sw.WriteLine("</html>");
			}
			finally
			{
				sw.Close();
			}
		}

		private void GetStyle(Type type, StreamWriter sw, object obj)
		{
			switch(type.FullName)
			{
				case "System.Decimal":
				case "System.Double":
				case "System.Int16":
				case "System.Int32":
				case "System.Int64":
					sw.WriteLine("<td class=x125 height=19 x:num>" + this.ConvertSpecialSymbols(obj.ToString()) + "</td>");
					break;
				case "System.DateTime":
					sw.WriteLine("<td class=x126 height=19>" + this.ConvertSpecialSymbols(obj.ToString()) + "</td>");
					break;
				default:
					sw.WriteLine("<td height=19>" + this.ConvertSpecialSymbols(obj.ToString()) + "</td>");
					break;
			}
		}

		private string ConvertSpecialSymbols(string text) 
		{
			string result = "";
			for(int i = 0; i < text.Length; i++) 
			{
				if(text[i] == '<')
					result += "&lt;";
				else if(text[i] == '>')
					result += "&gt;";
				else if(text[i] == '&')
					result += "&amp;";
				else if(text[i] == '"')
					result += "&quot;";
				else
					result += text[i];
			}
			return result;
		}
		
		public string power(string userId,string S_Table_Nmae)
		{
		string strSQL="select Aadd,Adel,Amod,Aexport,Adefine from S_User_Table a ,S_Table_Name b where  a.TbKeyId=b.keyId  and tableN='"+S_Table_Nmae+"' and UserKeyId='"+userId+"'",
		  strback="";
		    DataTable dt=sql_select(strSQL).Tables[0];
		  	strback=dt.Rows[0][0].ToString()+","+dt.Rows[0][1].ToString()+","+dt.Rows[0][2].ToString()+","+dt.Rows[0][3].ToString()+","+dt.Rows[0][4].ToString();
			return strback;
		}
		
public string XmlBack(string xmlpath,string NodeStr,string NodeName)//返回IP地 点
		{
  XmlDocument  xmlDoc=new XmlDocument();
   xmlDoc.Load(HttpContext.Current.Server.MapPath(xmlpath));
 string  BackStr="没找到相关内容!";
  XmlNode   root  =   xmlDoc.DocumentElement; 
   XmlNode   node   =   root.SelectSingleNode(NodeStr); 
  if(node!=null)
  if (node.SelectSingleNode(NodeName)!=null) BackStr=node.SelectSingleNode(NodeName).InnerText;
  return BackStr ;
  }	
		}
   }

