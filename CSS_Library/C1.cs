using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.PlottingServices;
using System.Reflection;
using System.IO;

namespace CSS_Library
{
    public class C1
    {
        //insert block

        string blockname = "";
        string sourfile = "";
        double ScaleX = 1;
        double ScaleY = 1;
        double ScaleZ = 1;

        Point3d insertpoint = new Point3d();

        public void AddBlocktoDataBase()
        {
            Document aDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            ObjectIdCollection blockIds = new ObjectIdCollection();
            using (Database OpenDb = new Database(false, true))
            {
                OpenDb.ReadDwgFile(sourfile, System.IO.FileShare.ReadWrite, true, "");
                ObjectIdCollection ids = new ObjectIdCollection();
                using (Transaction tr = OpenDb.TransactionManager.StartTransaction())
                {
                    BlockTable bt = (BlockTable)tr.GetObject(OpenDb.BlockTableId, OpenMode.ForRead);
                    if (bt.Has(blockname))
                    {
                        ids.Add(bt[blockname]);
                    }
                    tr.Commit();
                }
                //if found, add the block
                if (ids.Count != 0)
                {
                    Database destdb = aDoc.Database;
                    IdMapping iMap = new IdMapping();
                    destdb.WblockCloneObjects(ids, destdb.BlockTableId, iMap, DuplicateRecordCloning.Ignore, false);
                }
            }
        }
        public void AddBlocktoModelSpace()
        {
            Database accurDb = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Database;
            using (Transaction tr = accurDb.TransactionManager.StartTransaction())
            {
                BlockTable bt = accurDb.BlockTableId.GetObject(OpenMode.ForRead) as BlockTable;
                BlockTableRecord blockDef = bt[blockname].GetObject(OpenMode.ForRead) as BlockTableRecord;
                BlockTableRecord ms = (BlockTableRecord)tr.GetObject(accurDb.CurrentSpaceId, OpenMode.ForWrite);
                using (BlockReference blockRef = new BlockReference(insertpoint, blockDef.ObjectId))
                {
                    blockRef.ScaleFactors = new Scale3d(ScaleX, ScaleY, ScaleZ);
                    ms.AppendEntity(blockRef);
                    tr.AddNewlyCreatedDBObject(blockRef, true);
                }
                tr.Commit();
            }
        }
        public void insert_block(string sf, string block_name)
        {
            try
            {
                Document aDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                Editor acDocEd = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                using (aDoc.LockDocument())
                {
                    sourfile = sf;
                    blockname = block_name;
                    ScaleX = 1;
                    ScaleY = 1;
                    ScaleZ = 1;
                    PromptPointOptions ppo = new PromptPointOptions("\nSpecify block position:");
                    PromptPointResult ppr = acDocEd.GetPoint(ppo);

                    if (ppr.Status != PromptStatus.OK)
                    {
                        return;
                    }
                    insertpoint = ppr.Value;
                    AddBlocktoDataBase();
                    AddBlocktoModelSpace();

                }
            }
            catch
            {

            }
        }
        public void insert_block_position(string sf, string block_name, double X, double Y)
        {
            try
            {
                Document aDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                Editor acDocEd = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                sourfile = sf;
                blockname = block_name;
                ScaleX = 1;
                ScaleY = 1;
                ScaleZ = 1;

                insertpoint = new Point3d(X - 0.4133, Y, 0);
                AddBlocktoDataBase();
                AddBlocktoModelSpace();
                acDocEd.Regen();
            }
            catch
            {
            }
        }
        public void Pure_UnrefedBlocks()
        {
            Document aDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database accurDb = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Database;
            Editor acDocEd = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            using (Transaction trans = accurDb.TransactionManager.StartTransaction())
            {
                BlockTable bt = trans.GetObject(accurDb.BlockTableId, OpenMode.ForWrite) as BlockTable;

                foreach (ObjectId oid in bt)
                {
                    BlockTableRecord btr = trans.GetObject(oid, OpenMode.ForWrite) as BlockTableRecord;

                    if (btr.GetBlockReferenceIds(false, false).Count == 0
                        && !btr.IsLayout)
                    {
                        btr.Erase();
                    }
                }
                trans.Commit();
            }
        }
    }
}
