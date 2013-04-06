using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RogueFeature.Backend.Units;
using System.Windows.Media.Imaging;

namespace RogueFeature.Backend
{
    public enum Direction { UP, DOWN, LEFT, RIGHT };

    public static class ImageLib
    {
        public static Dictionary<string, BitmapImage> Images { private set; get; }              
        public static BitmapImage GrabImage(string path)
        {
            if (Images == null)
            {
                Images = new Dictionary<string, BitmapImage>();
            }
          
            if (path.Length > 0)
            {
                if (!path.Substring(0, 1).Equals("\\"))
                {
                    path = "\\" + path;
                }
            }
            path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + path;
            
            if (Images.ContainsKey(path))
            {
                BitmapImage bitmapImage = null;
                Images.TryGetValue(path, out bitmapImage);
                return bitmapImage;
            }
            
            return AddImage(path);
        }

        private static BitmapImage AddImage(string path)
        {
            BitmapImage bitmapImage = new BitmapImage();
            try
            {
                bitmapImage.UriSource = new Uri(path);
                Images.Add(path, bitmapImage);
                return bitmapImage;
            }
            catch (Exception ex)
            {
                DebugLogging.DebugLogger.LogIt("$E ImageLib.AddImage: " + ex.Message);
                return null;
            }
        }
    }
}
