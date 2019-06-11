using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace WebStore.User.Dev
{
    public partial class Upload : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-3ECI093;Initial Catalog=WebStore;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["Developer"] == null)
            {
                Session.RemoveAll();
                Response.Redirect("~/Home.aspx");
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (Allowed_Extension()==true)
                {
                    if (!CheckAppExist())
                    {
                        try
                        {
                            UpdateAppDatabase();
                        }
                        catch (Exception ex)
                        {
                            Status.Text = "Error occured try again, if problem persists try later. Reason: " + ex.Message;
                        }
                    }
                    else
                    {
                        Status.Text = "App already Exists. Check again or try with another Name";
                    } 
                }
                else
                {
                    Status.Text = "Upload Status: Failed. Reason: Category and file type mismatched...";
                }
            }
        }

        bool CheckAppExist()//Checks if there is a file or not both in database and in directory.....
        {
            int val = 1;
            try
            {
                con.Open();
                string command = "select * from fn_AppExist('" + AppName.Text + "');";
                SqlCommand cmd = new SqlCommand(command, con);
                val = int.Parse(cmd.ExecuteScalar().ToString());
                con.Close();
            }
            catch (Exception ex)
            {
                Status.Text = "Exception Occured </br> Reason: " + ex.Message;
            }
            if (val == 0 && !CheckIfDirectoryExist())
                return false;
            else
                return true;
        }

        bool CheckIfDirectoryExist()//checks if there is directory of AppName.....
        {
            string path = Server.MapPath("~/Apps/" + AppName.Text + "/");
            if (Directory.Exists(path))            
                return true;
            else
                return false;
        }

        void UpdateAppDatabase()//Updates database and send control to upload fles to save files.....
        {
            try
            {
                con.Open();
                int ID = GetNewID();
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_AddApp", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ID", ID);
                    cmd.Parameters.AddWithValue("AppName", AppName.Text);
                    cmd.Parameters.AddWithValue("Price", Price.Text);
                    cmd.Parameters.AddWithValue("Description", Description.Text);
                    cmd.Parameters.AddWithValue("Ext", getTypeFromExtension(Path.GetExtension(FControl.FileName)));
                    cmd.Parameters.AddWithValue("Size", float.Parse(((float)FControl.FileBytes.Length / 1024.0).ToString("0.00")));
                    cmd.Parameters.AddWithValue("IconName",IControl.FileName.Replace(" ","_"));
                    cmd.Parameters.AddWithValue("FileName", FControl.FileName.Replace(" ","_"));
                    cmd.Parameters.AddWithValue("Dev", Session["Developer"].ToString());
                    cmd.ExecuteNonQuery();

                    foreach (ListItem li in Category.Items)
                    {
                        if (li.Selected)
                        {
                            SqlCommand cmd2 = new SqlCommand("sp_AddCategory", con);
                            cmd2.CommandType = CommandType.StoredProcedure;
                            cmd2.Parameters.AddWithValue("AppID", ID);
                            cmd2.Parameters.AddWithValue("CatID", li.Value);
                            cmd2.ExecuteNonQuery();
                        }
                    }

                    UploadFiles(); //It will save those files.....

                    Status.Text = "App Uploaded";
                }
                catch (Exception ex)
                {
                    Status.Text += "Failed </br> Reason: " + ex.Message;
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Status.Text = "Error occured. Reason: " + ex.Message;
            }
        }

        int GetNewID()//Gets a random unique ID fron 1 to 100.....
        {
            SqlDataReader rd;
            int APPID;
            Random r = new Random();
            do
            {
                APPID = r.Next(1, 100);
                SqlCommand cmd = new SqlCommand("select * from App where ID = " + APPID + ";", con);
                rd = cmd.ExecuteReader();
            } while (rd.HasRows);
            rd.Close();

            return APPID;
        }

        void UploadFiles()//Saves the image and files to server.....
        {
            try
            {
                string folderpath = Server.MapPath("~/Apps/" + AppName.Text + "/");
                Directory.CreateDirectory(folderpath);
                string imagepath = Server.MapPath("~/Apps/" + AppName.Text + "/img/");
                Directory.CreateDirectory(imagepath);
                string reviewpath = Server.MapPath("~/Apps/" + AppName.Text + "/Review/");
                Directory.CreateDirectory(reviewpath);
                string filename = Path.GetFileName(FControl.FileName.Replace(" ","_"));
                string iconname = Path.GetFileName(IControl.FileName.Replace(" ","_"));
                FControl.SaveAs(folderpath + filename);
                IControl.SaveAs(imagepath + iconname);
            }
            catch (Exception ex)
            {
                Status.Text = "Error occured: " + ex.Message;
                //if some error occured then delete every thing with same ID from database(will do it later)....
            }

        }

        string getTypeFromExtension(string str)//It will return the type of file from extension of file.....
        {
            switch (str)
            {
                case ".7z": return "7-zip compressed file";
                case ".arj": return "ARJ compressed file";
                case ".deb": return "Debian software package file";
                case ".pkg": return "Package file";
                case ".rar": return "RAR file";
                case ".rpm": return "Red Hat Package Manager";
                case ".tar.gz": return "Tarball compressed file";
                case ".z": return "Z compressed file";
                case ".zip": return "Zip compressed file";
                case ".dmg": return "macOS X disk image";
                case ".iso": return "ISO disc image";
                case ".toast": return "Toast disc image";
                case ".vcd": return "Virtual CD";
                case ".csv": return "Comma separated value file";
                case ".dat": return "Data file";
                case ".db":
                case ".dbf": return "Database file";
                case ".log": return "Log file";
                case ".mdb": return "Microsoft Access database file";
                case ".sav": return "Save file";
                case ".sql": return "SQL database file";
                case ".tar": return "Linux / Unix tarball file archive";
                case ".xml": return "XML file";
                case ".apk": return "Android package file";
                case ".bat": return "Batch file";
                case ".bin": return "Binary file";
                case ".cgi":
                case ".pl": return "Perl script file";
                case ".com": return "MS-DOS command file";
                case ".exe": return "Executable file";
                case ".gadget": return "Windows gadget";
                case ".jar": return "Java Archive file";
                case ".py": return "Python file";
                case ".wsv": return "Windows Script File";
                case ".fnt": return "Windows font file";
                case ".fon": return "Generic font file";
                case ".otf": return "Open type font file";
                case ".ttf": return "TrueType font file";
                case ".ai": return "Adobe Illustrator file";
                case ".bmp": return "Bitmap image";
                case ".gif": return "GIF image";
                case ".ico": return "Icon file";
                case ".jpeg":
                case ".jpg": return "JPEG image";
                case ".png": return "PNG image";
                case ".ps": return "PostScript file";
                case ".psd": return "PSD image";
                case ".svg": return "Scalable Vector Graphics file";
                case ".tif":
                case ".tiff": return "TIFF image";
                case ".asp":
                case ".aspx": return "Active Server Page file";
                case ".cer": return "Internet security certificate";
                case ".cfm": return "ColdFusion Markup file";
                case ".css": return "Cascading Style Sheet file";
                case ".htm":
                case ".html": return "HTML file";
                case ".js": return "JavaScript file";
                case ".jsp": return "Java Server Page file";
                case ".part": return "Partially downloaded file";
                case ".php": return "PHP file";
                case ".rss": return "RSS file";
                case ".xhtml": return "XHTML file";
                case ".key": return "Keynote presentation";
                case ".odp": return "OpenOffice Impress presentation file";
                case ".pps": return "PowerPoint slide show";
                case ".ppt": return "PowerPoint presentation";
                case ".pptx": return "PowerPoint Open XML presentation";
                case ".c": return "C source code file";
                case ".class": return "Java class file";
                case ".cpp": return "C++ source code file";
                case ".cs": return "Visual C# source code file";
                case ".h": return "C, C++, and Objective-C header file";
                case ".java": return "Java Source code file";
                case ".sh": return "Bash shell script";
                case ".swift": return "Swift source code file";
                case ".vb": return "Visual Basic file";
                case ".ods": return "OpenOffice Calc spreadsheet file";
                case ".xlr": return "Microsoft Works spreadsheet file";
                case ".xls": return "Microsoft Excel file";
                case ".xlsx": return "Microsoft Excel Open XML spreadsheet file";
                case ".bak": return "Backup file";
                case ".cab": return "Windows Cabinet file";
                case ".cfg": return "Configuration file";
                case ".clp": return "Windows Control panel file";
                case ".cur": return "Windows cursor file";
                case ".dll": return "DLL file";
                case ".dmp": return "DUMP file";
                case ".drv": return "Device driver file";
                case ".icns": return "macOS X icon resource file";
                case ".ini": return "Initialization file";
                case ".ink": return "Windows shortcut file";
                case ".msi": return "Windows installer package";
                case ".sys": return "Windows system file";
                case ".tmp": return "Temporary file";
                case ".3g2": return "3GPP2 multimedia file";
                case ".3gp": return "3GPP multimedia file";
                case ".avi": return "AVI file";
                case ".flv": return "Adobe Flash file";
                case ".h264": return "H.264 video file";
                case ".m4v": return "Apple MP4 video file";
                case ".mkv": return "Matroska Multimedia Container";
                case ".mov": return "Apple QuickTime movie file";
                case ".mp4": return "MPEG4 video file";
                case ".mpg":
                case ".mpeg": return "MPEG video file";
                case ".rm": return "RealMedia file";
                case ".swf": return "Shockwave flash file";
                case ".vob": return "DVD Video Object";
                case ".wmv": return "Windows Media Video file";
                case ".docx":
                case ".doc": return "Microsoft Word file";
                case ".odt": return "OpenOffice Writer document file";
                case ".pdf": return "PDF file";
                case ".rtf": return "Rich Text Format";
                case ".tex": return "A LaTeX document file";
                case ".txt": return "Plain text file";
                case ".wks":
                case ".wps": return "Microsoft Works file";
                case ".wpd": return "WordPerfect document";
                case ".ipa": return "iOS App Store Package";
                case ".mp3": return "MPEG4 audio file";
                case ".aac": return "audio File";
                default: return str;
            }
        }

        protected bool Allowed_Extension()//Check allowed extension based on categories selected.....
        {
            FileInfo fin = new FileInfo(Path.GetFileName(FControl.FileName));
            foreach (ListItem li in Category.Items)
            {
                if (li.Selected)
                {
                    string str = li.Text;
                    switch (str)
                    {
                        case "Windows":
                            {
                                if (fin.Extension == ".exe" || fin.Extension == ".msi")
                                    return true;
                                else
                                    continue;
                            }
                        case "Android":
                            {
                                if (fin.Extension == ".apk")
                                    return true;
                                else
                                    continue;
                            }
                        case "Linux":
                            {
                                if (fin.Extension == ".deb")
                                    return true;
                                else
                                    continue;
                            }
                        case "Games": return true;
                        default: continue;
                    }
                }
                else
                {
                    continue;
                }
            }
            return false;
        }
    }
}