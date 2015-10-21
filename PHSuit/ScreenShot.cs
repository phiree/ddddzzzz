﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace PHSuit
{
   public class DrawShot
    {
        static bool saveToClipboard = false;
        //part1 capture screen
        public static void CaptureImage(bool showCursor, Size curSize, Point curPos,
     Point SourcePoint, Point DestinationPoint, Rectangle SelectionRectangle,
     string FilePath, string extension)
        {
            using (Bitmap bitmap = new Bitmap(SelectionRectangle.Width,
        SelectionRectangle.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(SourcePoint, DestinationPoint,
            SelectionRectangle.Size);

                    if (showCursor)
                    {
                        Rectangle cursorBounds = new Rectangle(curPos, curSize);
                        Cursors.Default.Draw(g, cursorBounds);
                    }
                }

                if (saveToClipboard)
                {
                    Image img = (Image)bitmap;
                    Clipboard.SetImage(img);
                }
                else
                {
                    switch (extension)
                    {
                        case ".bmp":
                            bitmap.Save(FilePath, ImageFormat.Bmp);
                            break;
                        case ".jpg":
                            bitmap.Save(FilePath, ImageFormat.Jpeg);
                            break;
                        case ".gif":
                            bitmap.Save(FilePath, ImageFormat.Gif);
                            break;
                        case ".tiff":
                            bitmap.Save(FilePath, ImageFormat.Tiff);
                            break;
                        case ".png":
                            bitmap.Save(FilePath, ImageFormat.Png);
                            break;
                        default:
                            bitmap.Save(FilePath, ImageFormat.Jpeg);
                            break;
                    }
                }
            }
        }
    }
}
