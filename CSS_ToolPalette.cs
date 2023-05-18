using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Windows;
using System.Windows;


namespace CSS_Palette
{
    public class CSS_ToolPalette
    {
        static PaletteSet palette;
        static bool wasVisible;
        [CommandMethod("CSS_Palette")]
        public static void CSS_Utility()
        {
            if (palette == null)
            {
                palette = new PaletteSet("CSS_Reminder", "CSSPALETTE", new Guid("{F60AE3DA-0373-4d24-82D2-B2646517ABCB}"));
                palette.Style = PaletteSetStyles.ShowAutoHideButton |
                                PaletteSetStyles.ShowCloseButton |
                                PaletteSetStyles.ShowPropertiesMenu;
                palette.MinimumSize = new System.Drawing.Size(200, 180);
                palette.AddVisual("Tab 1", new mainForm2());

                var docs = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager;
                docs.DocumentBecameCurrent += (s, e) => palette.Visible = e.Document == null ? false : wasVisible;
                docs.DocumentCreated += (s, e) => palette.Visible = wasVisible;
                docs.DocumentToBeDeactivated += (s, e) => wasVisible = palette.Visible;
                docs.DocumentToBeDestroyed += (s, e) =>
                {
                    wasVisible = palette.Visible;
                    if (docs.Count == 1)
                        palette.Visible = false;
                };
            }
            palette.Visible = true;
            palette.Dock = DockSides.Left;
            palette.RolledUp = true;
        }
    }
}
