using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Text;
using System.DirectoryServices;
using System.IO;

namespace Common.DirectoryServices
{
    public class IISManager
    {

        private string _webSiteID;

        public string WebSiteID
        {
            get { return _webSiteID; }
            set { _webSiteID = value; }
        }

        private string _strServerName;
        public string ServerName
        {
            get
            {
                return _strServerName;
            }
            set
            {
                _strServerName = value;
            }
        }

        private string _strVDirName;
        public string VDirName
        {
            get
            {
                return _strVDirName;
            }
            set
            {
                _strVDirName = value;
            }
        }

        private string _strPhysicalPath;
        public string PhysicalPath
        {
            get
            {
                return _strPhysicalPath;
            }
            set
            {
                _strPhysicalPath = value;
            }
        }

        private VDirectoryType _directoryType;
        public VDirectoryType DirectoryType
        {
            get
            {
                return _directoryType;
            }
            set
            {
                _directoryType = value;
            }
        }

        public enum VDirectoryType
        {
            FTP_DIR, WEB_IIS_DIR
        };

        public string CreateVDir()
        {
            System.DirectoryServices.DirectoryEntry oDE;
            System.DirectoryServices.DirectoryEntries oDC;
            System.DirectoryServices.DirectoryEntry oVirDir;
            //try
            // {
            //check whether to create FTP or Web IIS Virtual Directory
            if (this.DirectoryType == VDirectoryType.WEB_IIS_DIR)
            {
                oDE = new DirectoryEntry("IIS://" +
                      this._strServerName + "/W3SVC/" + _webSiteID + "/Root");
            }
            else
            {
                oDE = new DirectoryEntry("IIS://" +
                      this._strServerName + "/MSFTPSVC/1/Root");
            }

            //Get Default Web Site
            oDC = oDE.Children;

            //Add row
            oVirDir = oDC.Add(this._strVDirName,
                      oDE.SchemaClassName.ToString());

            //Commit changes for Schema class File
            oVirDir.CommitChanges();

            //Create physical path if it does not exists
            if (!Directory.Exists(this._strPhysicalPath))
            {
                Directory.CreateDirectory(this._strPhysicalPath);
            }

            //Set virtual directory to physical path
            oVirDir.Properties["Path"].Value = this._strPhysicalPath;

            //Set read access
            oVirDir.Properties["AccessRead"][0] = true;

            //Create Application for IIS Application (as for ASP.NET)
            if (this.DirectoryType == VDirectoryType.WEB_IIS_DIR)
            {
                oVirDir.Invoke("AppCreate", true);
                oVirDir.Properties["AppFriendlyName"][0] = this._strVDirName;
            }

            //Save all the changes
            oVirDir.CommitChanges();

            return null;

            // }
            //catch (Exception exc)
            //{
            //   return exc.Message.ToString();
            //}
        }
    }
}