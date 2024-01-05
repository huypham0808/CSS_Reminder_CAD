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
using AcAp = Autodesk.AutoCAD.ApplicationServices.Application;
using System.IO;
//using Autodesk.AutoCAD.ApplicationServices.Application;

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
                //palette.AddVisual("Tab 2", new mainForm2());

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
            //palette.AutoRollUp = true;
        }
        [CommandMethod("CSS_TEMPLATE")]
        public void CSS_Template()
        {
            Document doc = AcAp.DocumentManager.MdiActiveDocument;
            Editor ed = doc.Editor;
            string templateDirectory = UtilsMethod.UpdateMethod.UpdateFileTemplate("*ST-SST_CAD TEMPLATE.dwt");

            DocumentCollection docColl = AcAp.DocumentManager;
            Document newDrawing = docColl.Add(templateDirectory);
            docColl.MdiActiveDocument = newDrawing;
            ed.WriteMessage("Template was loaded successfully!");
        }
        [CommandMethod("CSS_INSTALLATION_NOTE")]
        public void CSS_InstallNote()
        {
            Document doc = AcAp.DocumentManager.MdiActiveDocument;
            Editor ed = doc.Editor;

            string templateDirectory = UtilsMethod.UpdateMethod.UpdateFileTemplate("*ST-SST_INSTALLATION NOTES.dwt");

            DocumentCollection docColl = AcAp.DocumentManager;
            Document newDrawing = docColl.Add(templateDirectory);
            docColl.MdiActiveDocument = newDrawing;
            ed.WriteMessage("Installation Note was loaded successfully!");
        }
    }
}
