using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;

namespace InactivityLogger
{
    public static class FontManager
    {
        private static Dictionary<string, object> fontFamilyMap = new Dictionary<string, object>();

        private static PrivateFontCollection fontCollection = new PrivateFontCollection();
        
        // Returns a font family from the fonts directory, or on error returns a default font family and pops up a warning message box.
        // The name must be the same as the filename of the font without the extension.
        public static FontFamily Get(string name)
        {
            if (fontFamilyMap.ContainsKey(name))
            {
                return (FontFamily)fontFamilyMap[name];
            }

            try
            {
                string fontDir = Directory.GetParent(Application.ExecutablePath).FullName + @"\fonts\";
                string path = fontDir + name + ".ttf";
                fontCollection.AddFontFile(path);
                // Get the newly added font family.
                FontFamily[] families = fontCollection.Families;
                FontFamily family = families[families.Length - 1];
                fontFamilyMap[name] = family;
                return family;
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem loading a font: \n\n" + ex.Message, Program.Name, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Font loading error. Return default font.
            return SystemFonts.DefaultFont.FontFamily;
        }

        // Disposes of loaded font families.
        // Call this at the end of the program.
        public static void CleanUp()
        {
            foreach (FontFamily family in fontCollection.Families)
            {
                family.Dispose();
            }

            fontFamilyMap = null;
            fontCollection = null;
        }
    }
}
