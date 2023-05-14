using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchiveDate 
{
    private string ArchiveName;
    private string ArchiveImageID;
    private TextAsset ArchiveText;

    public string Archivename { get => ArchiveName; set => ArchiveName = value; }
    public string ArchiveimageID { get => ArchiveImageID; set => ArchiveImageID = value; }
    public TextAsset Archivetext { get => ArchiveText; set => ArchiveText = value; }

    public ArchiveDate(string ArchiveName1 , string ArchiveImageID1, TextAsset ArchiveText1)
    {
        ArchiveName = ArchiveName1;
        ArchiveImageID = ArchiveImageID1;
        ArchiveText = ArchiveText1;
    }
}
