using System.IO;

namespace MobiMetadataReader.Net.Metadata
{
    public class MobiMetadata
    {
        PDBHead pdbHeader;
        PalmDOCHead palmDocHeader;
        MobiHead mobiHeader;

        public PDBHead PDBHeader
        {
            get { return pdbHeader; }
        }

        public PalmDOCHead PalmDocHeader
        {
            get { return palmDocHeader; }
        }

        public MobiHead MobiHeader
        {
            get { return mobiHeader; }
        }

        public MobiMetadata(FileStream fs)
        {
            SetUpData(fs);
        }

        public MobiMetadata(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            SetUpData(fs);
            fs.Close();
        }

        void SetUpData(FileStream fs)
        {
            pdbHeader = new PDBHead(fs);
            palmDocHeader = new PalmDOCHead(fs);
            mobiHeader = new MobiHead(fs, pdbHeader.MobiHeaderSize);


        }
    }
}
