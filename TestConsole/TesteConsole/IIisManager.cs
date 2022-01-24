using System;
using Microsoft.Web.Administration;
using System.Linq;
using System.IO;

namespace TesteConsole
{


    /// <summary>
    /// IISManager Class to modify IIS Apps
    /// </summary>
    public class IISManager
    {
        /// <summary>
        /// Creates the IIS application.
        /// </summary>
        /// <param name="applicationName">Name of the application.</param>
        /// <param name="physicalPath">The physical path.</param>
        public static void CreateIISApplication(string applicationName, string physicalPath)
        {
            using (ServerManager serverManager = new ServerManager())
            {
                var defaultSite = serverManager.Sites["Default Web Site"];
                Application newApplication = defaultSite.Applications["/" + applicationName];

                // Remove if exists?!
                if (newApplication != null)
                {
                    defaultSite.Applications.Remove(newApplication);
                    serverManager.CommitChanges();
                }

                defaultSite = serverManager.Sites["Default Web Site"];//TestConfig.DefaultWebsiteName];
                newApplication = defaultSite.Applications.Add("/" + applicationName, physicalPath);

                newApplication.ApplicationPoolName = "TESTE";

                serverManager.CommitChanges();
                serverManager.Dispose();
            }
        }

        /// <summary>
        /// Recycles the application pool.
        /// </summary>
        /// <param name="applicationName">Name of the application.</param>
        public static void RecycleApplicationPool(string applicationName)
        {
            ServerManager iisManager = new ServerManager();
            Site defaultSite = iisManager.Sites["Default Web Site"];
            var application = defaultSite.Applications["/" + applicationName];//"/{0}".FormatWith(applicationName)];

            if (application == null)
            {
                return;
            }

            string appPool = application.ApplicationPoolName;
            ApplicationPool pool = iisManager.ApplicationPools[appPool];
            pool.Recycle();
        }

        /// <summary>
        /// Deletes the IIS application.
        /// </summary>
        /// <param name="applicationName">Name of the application.</param>
        public static void DeleteIISApplication(string applicationName)
        {
            using (ServerManager serverManager = new ServerManager())
            {
                var defaultSite = serverManager.Sites["Default Web Site"];
                Application newApplication = defaultSite.Applications["/" + applicationName];

                // Remove
                if (newApplication != null)
                {
                    defaultSite.Applications.Remove(newApplication);
                    serverManager.CommitChanges();
                }

                serverManager.Dispose();
            }
        }

        private const string SERVER_IP = "192.168.111.112";// put your ip address
        private const int PORT = 80;
        private const string WEB_DOMAIN_PATH = @"F:\SourceAnderson\Projetos\LojaRiceli_2014\WebForm\1-WebForm\App_Code\Cache\{0}\";
        public static string CreateUserSite(string user, string domain)//string ip, string diretorio, 
        {


            string path = string.Format(WEB_DOMAIN_PATH, domain);

            string userpath = path + user;

            string userUrl = user + "." + domain;

            using (ServerManager serverManager = new ServerManager())
            {

                bool siteExists = false;
                int number = serverManager.Sites.Where(p => p.Name.ToLower().Equals(userUrl.ToLower())).Count();

                if (number == 0)
                {
                    siteExists = false;
                }
                else
                {
                    siteExists = true;
                }

                if (!siteExists)
                {

                    //create user directory
                    Directory.CreateDirectory(userpath);

                    //copy every files from a-base to a new created folder
                /*    FileInfo[] d = new DirectoryInfo(path + @"\a-base").GetFiles();
                    foreach (FileInfo fi in d)
                    {
                        File.Copy(fi.FullName, userpath + @"\" + fi.Name, true);
                    }

                    //create a directory
                    Directory.CreateDirectory(userpath + @"\swfobject");

                    FileInfo[] d1 = new DirectoryInfo(path + @"\a-base\swfobject").GetFiles();
                    foreach (FileInfo fi in d1)
                    {
                        File.Copy(fi.FullName, userpath + @"\swfobject\" + fi.Name, true);
                    }
                    */


                    //create site
                    Site mySite = serverManager.Sites.Add(userUrl, path + user, PORT);
                    mySite.ServerAutoStart = true;
                    mySite.Applications[0].ApplicationPoolName = domain;

                    //create bindings
                    mySite.Bindings.Clear();
                    mySite.Bindings.Add(string.Format("{0}:{2}:{1}", SERVER_IP, userUrl, PORT), "http");
                    mySite.Bindings.Add(string.Format("{0}:{2}:www.{1}", SERVER_IP, userUrl, PORT), "http");


                    Configuration config = serverManager.GetApplicationHostConfiguration();
                    ConfigurationSection httpLoggingSection = config.GetSection("system.webServer/httpLogging", userUrl);
                    httpLoggingSection["dontLog"] = true;

                    serverManager.CommitChanges();

                    //  ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('" + userUrl + " created');", true);

                }
                else
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "alert('user exists. Please use other name');", true);
                    throw new Exception("user exists. Please use other name");
                }


                return userUrl + " has been successfully created";
            }
        }
    }
}

