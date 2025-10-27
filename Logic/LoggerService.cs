using Microsoft.VisualBasic;
using System.Text.Json;
using System.Text;
using Newtonsoft.Json;
using ErpApi.Models;

namespace ErpApi.Logic
{
    public class LoggerService
    {
        ErpContext db;

        public LoggerService(ErpContext context)
        {
            db = context;
        }
        public int LogInformation1(string username, string action, string step, string reference, string status, string message, object obj)
        {
            string objstr = string.Empty;
            string replacepassword = string.Empty;

            if (obj != null)
            {
                objstr = JsonConvert.SerializeObject(obj);
            }

            return LogInformation(username, action, step, reference, status, message, objstr);
        }
        public int LogInformation(string username, string action, string step, string reference, string status, string message, object obj)
        {
            //Prepare the log entry 
            string rawlog = string.Empty;
            //rawlog = buildlog(action, "--->|", step, "--->|", reference, "--->|", status.Code.ToString(), "--->|", message, "--->|", ConvertObjectToJson(obj));

            if (obj == null)
            {
                rawlog = buildlog(action, "--->|", step, "--->|", reference, "--->|", status, "--->|", message, "--->|", ConvertObjectToJson(obj));
            }
            else if (obj.ToString().Contains("Exception"))
            {
                rawlog = buildlog(action, "--->|", step, "--->|", reference, "--->|", status, "--->|", message, "--->|", obj.ToString());
            }
            else
            {
                rawlog = buildlog(action, "--->|", step, "--->|", reference, "--->|", status, "--->|", message, "--->|", ConvertObjectToJson(obj));
            }

            //Write the log entry into the txt file
            WriteLog(username, rawlog);
            return 0;
        }

        public static string ConvertObjectToJson(object obj)
        {
            string inputJson = string.Empty;
            //inputJson = (new JavaScriptSerializer()).Serialize(obj);
            // inputJson = JsonSerializer.Serialize(obj, null);
            inputJson = System.Text.Json.JsonSerializer.Serialize(obj, (JsonSerializerOptions)null);
            return inputJson;
        }

        public void WriteLog(string strusrid, string strLogEntry)
        {
            string[] str;
            try
            {
                var LOGS_DIR = decryptvar("LOGS_DIR");
                string archive = Path.GetDirectoryName(LOGS_DIR);
                archive = Path.Combine(archive, "SUPPLIERCHAIN_API\\ONLINE", DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("dd"));

                if (!Directory.Exists(archive))
                {
                    Directory.CreateDirectory(archive);
                }

                archive = EndingPathSlash(archive);
                string strLogEntryFile = string.Concat(EndingPathSlash(archive), "SUPPLIERCHAIN_API_Log_", Strings.Format(DateAndTime.Now, "ddMMMyyyHH"), ".log");

                if (FileInUse(strLogEntryFile))
                {
                    int x = 1;

                    do
                    {
                        str = new string[] { archive, "SUPPLIERCHAIN_API_Log_ForcedWrite", Strings.Format(DateAndTime.Now, "ddMMyyyy_HHmmss_fff"), "_", Convert.ToString(x), ".log" };
                        strLogEntryFile = string.Concat(str);

                        if (FileInUse(strLogEntryFile))
                        {
                            x++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    while (x <= 10);
                }

                StreamWriter strLogFileWrite = new StreamWriter(strLogEntryFile, true);
                string strUser = Convert.ToString("SUPPLIERCHAIN_API"); //HttpContext.Current.Session["UserName"]
                str = new string[] { Strings.Format(DateAndTime.Now, "ddMMyyyy_HHmmss_fff"), "|---> ", strusrid, "|---> ", strLogEntry };
                //str = new string[] { Convert.ToString(DateAndTime.Now), "|", strUser, "|", strLogEntry };
                strLogFileWrite.WriteLine(string.Concat(str));
                strLogFileWrite.Dispose();
                strLogFileWrite.Close();
            }
            catch (Exception ex)
            {
                string x = ex.Message;
                //ProjectData.SetProjectError(exception);
                //ProjectData.ClearProjectError();
            }
        }

        public static string decryptvar(string variab)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            switch (variab)
            {

                case "LOGS_DIR":
                    return (config["LogSettings:LogDirectory"]);
                default:
                    return "";
            }
        }

        public static string EndingPathSlash(string strPath)
        {
            string strPathToAmends = "";
            strPathToAmends = strPath;

            if (string.Compare(Strings.Right(strPathToAmends, 1), "\\", false) != 0)
            {
                strPathToAmends = string.Concat(strPathToAmends, "\\");
            }
            return strPathToAmends;
        }

        public static bool FileInUse(string sFile)
        {
            bool FileInUse = false;

            if (File.Exists(sFile))
            {
                try
                {
                    short F = checked((short)FileSystem.FreeFile());
                    FileSystem.FileOpen(F, sFile, OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.LockReadWrite, -1);
                    FileSystem.FileClose(new int[] { F });
                }
                catch (Exception ex)
                {
                    string x = ex.Message;
                    FileInUse = true;
                    return FileInUse;
                }
            }
            return FileInUse;
        }

        public static string buildlog(string strparam1, string strparam2, string strparam3, string strparam4, string strparam5, string strparam6, string strparam7, string strparam8, string strparam9, string strparam10, string strparam11)
        {
            string builtresp = string.Empty;

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(strparam1);
            stringBuilder.Append(strparam2);
            stringBuilder.Append(strparam3);
            stringBuilder.Append(strparam4);
            stringBuilder.Append(strparam5);
            stringBuilder.Append(strparam6);
            stringBuilder.Append(strparam7);
            stringBuilder.Append(strparam8);
            stringBuilder.Append(strparam9);
            stringBuilder.Append(strparam10);
            stringBuilder.Append(strparam11);
            builtresp = stringBuilder.ToString();

            return builtresp;
        }
    }
}
