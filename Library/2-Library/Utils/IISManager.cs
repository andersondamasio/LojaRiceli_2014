using Microsoft.Web.Administration;
using System.Linq;

namespace _2_Library.Utils
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
        public static void CreateIISApplication(string defaultWebsiteName, string applicationName, string physicalPath)
        {
            using (ServerManager serverManager = new ServerManager())
            {
                var defaultSite = serverManager.Sites[defaultWebsiteName];
                Application newApplication = defaultSite.Applications["/" + applicationName];

                // Remove if exists?!
                if (newApplication != null)
                {
                    defaultSite.Applications.Remove(newApplication);
                    serverManager.CommitChanges();
                }

                defaultSite = serverManager.Sites[defaultWebsiteName];//TestConfig.DefaultWebsiteName];
                newApplication = defaultSite.Applications.Add("/" + applicationName, physicalPath);

                newApplication.ApplicationPoolName = defaultWebsiteName;

                serverManager.CommitChanges();
                serverManager.Dispose();
            }
        }

        /// <summary>
        /// Recycles the application pool.
        /// </summary>
        /// <param name="applicationName">Name of the application.</param>
        public static void RecycleApplicationPool(string defaultWebsiteName, string applicationName)
        {
            ServerManager iisManager = new ServerManager();
            Site defaultSite = iisManager.Sites[defaultWebsiteName];
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
        public static void DeleteIISApplication(string defaultWebsiteName,string applicationName)
        {
            using (ServerManager serverManager = new ServerManager())
            {
                var defaultSite = serverManager.Sites[defaultWebsiteName];
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


        public static void AddHostSite(string defaultWebsiteName, string urlHost)
        {
            using (ServerManager serverManager = new ServerManager())
            {
                var defaultSite = serverManager.Sites[defaultWebsiteName];
                if (defaultSite != null)
                {
                    Binding b = defaultSite.Bindings.CreateElement();
                    b.SetAttributeValue("protocol", "http");
                    b.SetAttributeValue("bindingInformation", ":80:" + urlHost);
                    defaultSite.Bindings.Add(b);

                    serverManager.CommitChanges();
                }
                serverManager.Dispose();
            }
        }

        public static void RemoveHostSite(string defaultWebsiteName, string urlHost)
        {
            using (ServerManager serverManager = new ServerManager())
            {
                var defaultSite = serverManager.Sites[defaultWebsiteName];
                if (defaultSite != null)
                {
                    Binding b = defaultSite.Bindings.Where(s => s.Host == urlHost).FirstOrDefault();
                    if (b != null)
                        defaultSite.Bindings.Remove(b);
                    serverManager.CommitChanges();
                }
                serverManager.Dispose();
            }
        }
    }
}