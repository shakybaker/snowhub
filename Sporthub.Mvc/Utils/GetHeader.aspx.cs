using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Web.Security;

namespace Sporthub.Mvc.Utils
{
    public partial class GetHeader : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //This is a quick prototype to test the font rendering of
            //C#/.NET against the PHP/GD2 rendering for a possible
            //replacement of the PHP backend of FLIR*.
            //*http://facelift.mawhorter.net/examples/

            //Grab the text for the image
            String InputText = Request.QueryString["text"];
            //See snippet in the article to get the list of names for fonts on your machine
            if (!string.IsNullOrEmpty(InputText))
            {
                String FontName = "Hybrea";
                //String FontName = "Vtks good luck for you";
                int FontSize = 14; //default
                int FontColor = 1; //default
                if (!string.IsNullOrEmpty(InputText))
                {
                    FontSize = int.Parse(Request.QueryString["size"]);
                    try
                    {
                        FontColor = int.Parse(Request.QueryString["c"]);
                    }
                    catch { }
                }
                String SavePath = HttpContext.Current.Server.MapPath("~/App_Data/");
                String BaseFilename = FormsAuthentication.HashPasswordForStoringInConfigFile(InputText, "MD5");
                Bitmap HeadlineImage = CreateBitmapImage(InputText, FontName, FontSize, FontColor);
                HeadlineImage.Save(SavePath + BaseFilename + ".png", System.Drawing.Imaging.ImageFormat.Png);
                MemoryStream stream = new MemoryStream();
                HeadlineImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                Response.AppendHeader("Content-Type", "image/png");
                Response.OutputStream.Write(stream.ToArray(), 0, (int)stream.Length);
                Response.End();
            }
        }

        private static Bitmap CreateBitmapImage(String sImageText, String FontName, int FontSize, int FontColor)
        {
            Bitmap objBmpImage = new Bitmap(1, 1);
            int intWidth = 0;
            int intHeight = 0;

            Font objFont = new Font(FontName, FontSize, FontStyle.Regular);
            //Font objFont = new Font(FontName, FontSize, FontStyle.Bold);
            Graphics objGraphics = Graphics.FromImage(objBmpImage);
            intWidth = (int)objGraphics.MeasureString(sImageText, objFont).Width;
            intHeight = (int)objGraphics.MeasureString(sImageText, objFont).Height;

            objBmpImage = new Bitmap(objBmpImage, new Size(intWidth, intHeight));
            objGraphics = Graphics.FromImage(objBmpImage);
            objGraphics.Clear(Color.Transparent);
            objGraphics.SmoothingMode = SmoothingMode.HighQuality;
            objGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            Color color;
            if (FontColor == 1) //white
            {
                //color = Color.FromArgb(31, 110, 190);
                //color = Color.FromArgb(0, 0, 0);
                //color = Color.FromArgb(147, 138, 121);
                color = Color.FromArgb(255, 255, 255);
            }
            else if (FontColor == 2) //pink #e74077
            {
                color = Color.FromArgb(231, 64, 119);
            }
            else if (FontColor == 3) //green #98c959
            {
                color = Color.FromArgb(152, 201, 89);
            }
            else if (FontColor == 4) //blue #69b3ff
            {
                color = Color.FromArgb(105, 179, 255);
            }
            else if (FontColor == 5) //blue #93bce5
            {
                color = Color.FromArgb(147, 188, 229);
            }
            else
            {
                color = Color.FromArgb(255, 255, 255);
            }
            SolidBrush brush = new SolidBrush(color);
            //SolidBrush brush = new SolidBrush(Color.FromArgb(91, 204, 30));
            objGraphics.DrawString(sImageText, objFont, brush, 0, 0);
            objGraphics.Flush();

            return (objBmpImage);

        }

    }
}
