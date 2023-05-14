using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MoreMountains.Tools;
using UnityEngine.UI;

public class ArchivePanel : BasePanel
{
    public GameObject ButtonAT;
    public GameObject TitleText;
    GameObject Title2;
    public GameObject ArchiveImage;
    public GameObject PLimage;
    public GameObject ArchiveImage2;
    GameObject Image2;
    public GameObject DateText;
    public GameObject DateText23;

    GameObject DateText22;
   
    public GameObject archivePanel;
    public static TextAsset cardData;
    ArchiveDate archiveDate;
    public static List<ArchiveDate> DateList = new();
    string[] CharacterName = new string[] { "盗贼弗尔米奥", "战士卡斯", "重甲妮莫可", "维奥莱特", "工程师格蕾","牧师艾丝", "炼金术士艾什"};
    string[] LevelrName = new string[] { "危机纪元", "狂热纪元" , "火种纪元", "衰败纪元", "浑沌纪元", "秩序纪元" };
    string[] TeachName = new string[] { "阶段" , "时序" , "选牌", "移动和攻击" , "获胜条件" , "档案馆", "护甲和受到X点伤害", "推远和拉近", "特殊回合事件" , "状态" };
    float StartPosition = 336.5f;
    float A = 35f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
       
    }
    public void DateLoad(string ResourcesDate)
    {
        cardData = Resources.Load(ResourcesDate) as TextAsset;
        string[] dataRow = cardData.text.Split('\n');
        foreach (var row in dataRow)
        {
                //还有数据
                string[] rowArray = row.Split(',');
                if (rowArray[0] == "#")
                {
                    continue;
                }
                else if (rowArray[0] == "Text")
                {
                    //储存到链表中
                    string ArchiveName = rowArray[1];
                    string ArchiveImageID = rowArray[2];
                    TextAsset ArchiveText = new(rowArray[3]);
                    archiveDate = new(ArchiveName, ArchiveImageID, ArchiveText);
                    DateList.Add(archiveDate);
            }

            
        }
    }
    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {
            case "Character":
                archivePanel.BroadcastMessage("DestoryThis");
                for (int i = 0; i < CharacterName.Length; i++)
                {
                    GameObject Button = Instantiate(ButtonAT, gameObject.transform);
                    if (i < 7)
                    {
                        Button.transform.position = new(227.00f, StartPosition - A * i, 0f);
                        Button.name = "Character" + i;
                        Button.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = CharacterName[i];
                    }
                    if (i >= 7)
                    {
                        Button.transform.position = new(537.00f, StartPosition - A * (i - 7), 0f);
                        Button.name = "Character" + i;
                        Button.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = CharacterName[i];
                    }
                }

                Find();
                break;
            case "Level":
                archivePanel.BroadcastMessage("DestoryThis");
                for (int i = 0; i < LevelrName.Length; i++)
                {
                    GameObject Button = Instantiate(ButtonAT, gameObject.transform);
                    if (i < 7)
                    {
                        Button.transform.position = new(227.70f, StartPosition - A * i, 0f);
                        Button.name = "Level" + i;
                        Button.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = LevelrName[i];
                    }
                    if (i >= 7)
                    {
                        Button.transform.position = new(537.00f, StartPosition - A * (i - 7), 0f);
                        Button.name = "Level" + i;
                        Button.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = LevelrName[i];
                    }
                }
                Find();
                break;
            case "Teach":
                archivePanel.BroadcastMessage("DestoryThis");
                for (int i = 0; i < TeachName.Length; i++)
                {
                    GameObject Button = Instantiate(ButtonAT, gameObject.transform);
                    if (i < 7)
                    {
                        Button.transform.position = new(227.70f, StartPosition - A * i, 0f);
                        Button.name = "Teach" + i;
                        Button.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = TeachName[i];
                    }
                    if (i >= 7)
                    {
                        Button.transform.position = new(537.00f, StartPosition - A * (i - 7), 0f);
                        Button.name = "Teach" + i;
                        Button.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = TeachName[i];
                    }
                }
                Find();
                break;
            case "Character0":
                archivePanel.BroadcastMessage("DestoryThis");
                DateLoad("PlayerBackGround");
                Title2 = Instantiate(TitleText, gameObject.transform);
                Title2.transform.position = new(232.00f, 342.50f, 0f);
                Title2.GetComponent<TextMeshProUGUI>().text = DateList[0].Archivename;
                Debug.Log(DateList[0].ArchiveimageID);
                Image2 = Instantiate(PLimage, gameObject.transform);
                Image2.transform.position = new(224.00f, 177.00f, 0f);
                Image2.GetComponent<Image>().sprite = Resources.Load("Image/11", typeof(Sprite)) as Sprite;

                DateText22 = Instantiate(DateText23, gameObject.transform);
                DateText22.transform.position = new(487.00f, 342.00f, 0f);
                DateText22.GetComponent<TextMeshProUGUI>().text = DateList[0].Archivetext.ToString();

                break;
            case "Character1":
                archivePanel.BroadcastMessage("DestoryThis");
                DateLoad("PlayerBackGround");
                Title2 = Instantiate(TitleText, gameObject.transform);
                Title2.transform.position = new(399.00f, 336.50f, 0f);
                Title2.GetComponent<TextMeshProUGUI>().text = DateList[1].Archivename;

                Image2 = Instantiate(PLimage, gameObject.transform);
                Image2.transform.position = new(399.00f, 260.00f, 0f);
                Image2.GetComponent<Image>().sprite = Resources.Load("Image/22", typeof(Sprite)) as Sprite;


                DateText22 = Instantiate(DateText23, gameObject.transform);
                DateText22.transform.position = new(487.00f, 342.00f, 0f);
                DateText22.GetComponent<TextMeshProUGUI>().text = DateList[1].Archivetext.ToString();
                break;
            case "Character2":
                archivePanel.BroadcastMessage("DestoryThis");
                DateLoad("PlayerBackGround");
                Title2 = Instantiate(TitleText, gameObject.transform);
                Title2.transform.position = new(399.00f, 336.50f, 0f);
                Title2.GetComponent<TextMeshProUGUI>().text = DateList[2].Archivename;

                Image2 = Instantiate(PLimage, gameObject.transform);
                Image2.transform.position = new(399.00f, 260.00f, 0f);
                Image2.GetComponent<Image>().sprite = Resources.Load("Image/33", typeof(Sprite)) as Sprite;

                DateText22 = Instantiate(DateText23, gameObject.transform);
                DateText22.transform.position = new(487.00f, 342.00f, 0f);
                DateText22.GetComponent<TextMeshProUGUI>().text = DateList[2].Archivetext.ToString();
                break;
            case "Character3":
                archivePanel.BroadcastMessage("DestoryThis");
                DateLoad("PlayerBackGround");
                Title2 = Instantiate(TitleText, gameObject.transform);
                Title2.transform.position = new(399.00f, 336.50f, 0f);
                Title2.GetComponent<TextMeshProUGUI>().text = DateList[3].Archivename;

                Image2 = Instantiate(PLimage, gameObject.transform);
                Image2.transform.position = new(399.00f, 260.00f, 0f);
                Image2.GetComponent<Image>().sprite = Resources.Load("Image/44", typeof(Sprite)) as Sprite;

                DateText22 = Instantiate(DateText23, gameObject.transform);
                DateText22.transform.position = new(487.00f, 342.00f, 0f);
                DateText22.GetComponent<TextMeshProUGUI>().text = DateList[3].Archivetext.ToString();
                break;
            case "Character4":
                archivePanel.BroadcastMessage("DestoryThis");
                DateLoad("PlayerBackGround");
                Title2 = Instantiate(TitleText, gameObject.transform);
                Title2.transform.position = new(399.00f, 336.50f, 0f);
                Title2.GetComponent<TextMeshProUGUI>().text = DateList[4].Archivename;

                Image2 = Instantiate(PLimage, gameObject.transform);
                Image2.transform.position = new(399.00f, 260.00f, 0f);
                Image2.GetComponent<Image>().sprite = Resources.Load("Image/55", typeof(Sprite)) as Sprite;

                DateText22 = Instantiate(DateText23, gameObject.transform);
                DateText22.transform.position = new(487.00f, 342.00f, 0f);
                DateText22.GetComponent<TextMeshProUGUI>().text = DateList[4].Archivetext.ToString();
                break;
            case "Character5":
                archivePanel.BroadcastMessage("DestoryThis");
                DateLoad("PlayerBackGround");
                Title2 = Instantiate(TitleText, gameObject.transform);
                Title2.transform.position = new(399.00f, 336.50f, 0f);
                Title2.GetComponent<TextMeshProUGUI>().text = DateList[5].Archivename;

                Image2 = Instantiate(PLimage, gameObject.transform);
                Image2.transform.position = new(399.00f, 260.00f, 0f);
                Image2.GetComponent<Image>().sprite = Resources.Load("Image/66", typeof(Sprite)) as Sprite;

                DateText22 = Instantiate(DateText23, gameObject.transform);
                DateText22.transform.position = new(487.00f, 342.00f, 0f);
                DateText22.GetComponent<TextMeshProUGUI>().text = DateList[5].Archivetext.ToString();
                break;
            case "Character6":
                archivePanel.BroadcastMessage("DestoryThis");
                DateLoad("PlayerBackGround");
                Title2 = Instantiate(TitleText, gameObject.transform);
                Title2.transform.position = new(399.00f, 336.50f, 0f);
                Title2.GetComponent<TextMeshProUGUI>().text = DateList[6].Archivename;

                Image2 = Instantiate(PLimage, gameObject.transform);
                Image2.transform.position = new(399.00f, 260.00f, 0f);
                Image2.GetComponent<Image>().sprite = Resources.Load("Image/77", typeof(Sprite)) as Sprite;

                DateText22 = Instantiate(DateText23, gameObject.transform);
                DateText22.transform.position = new(487.00f, 342.00f, 0f);
                DateText22.GetComponent<TextMeshProUGUI>().text = DateList[6].Archivetext.ToString();
                break;
            case "Level0":
                archivePanel.BroadcastMessage("DestoryThis");
                DateLoad("TimeLine");
                Title2 = Instantiate(TitleText, gameObject.transform);
                Title2.transform.position = new(399.00f, 336.50f, 0f);
                Title2.GetComponent<TextMeshProUGUI>().text = DateList[0].Archivename;

                

                DateText22 = Instantiate(DateText, gameObject.transform);
                DateText22.transform.position = new(399.00f, 260.00f, 0f);
                DateText22.GetComponent<TextMeshProUGUI>().text = DateList[0].Archivetext.ToString();

                break;
            case "Level1":
                archivePanel.BroadcastMessage("DestoryThis");
                DateLoad("TimeLine");
                Title2 = Instantiate(TitleText, gameObject.transform);
                Title2.transform.position = new(399.00f, 336.50f, 0f);
                Title2.GetComponent<TextMeshProUGUI>().text = DateList[1].Archivename;

              

                DateText22 = Instantiate(DateText, gameObject.transform);
                DateText22.transform.position = new(399.00f, 260.00f, 0f);
                DateText22.GetComponent<TextMeshProUGUI>().text = DateList[1].Archivetext.ToString();

                break;
            case "Level2":
                archivePanel.BroadcastMessage("DestoryThis");
                DateLoad("TimeLine");
                Title2 = Instantiate(TitleText, gameObject.transform);
                Title2.transform.position = new(399.00f, 336.50f, 0f);
                Title2.GetComponent<TextMeshProUGUI>().text = DateList[2].Archivename;

               

                DateText22 = Instantiate(DateText, gameObject.transform);
                DateText22.transform.position = new(399.00f, 260.00f, 0f);
                DateText22.GetComponent<TextMeshProUGUI>().text = DateList[2].Archivetext.ToString();

                break;
            case "Level3":
                archivePanel.BroadcastMessage("DestoryThis");
                DateLoad("TimeLine");
                Title2 = Instantiate(TitleText, gameObject.transform);
                Title2.transform.position = new(399.00f, 336.50f, 0f);
                Title2.GetComponent<TextMeshProUGUI>().text = DateList[3].Archivename;

              

                DateText22 = Instantiate(DateText, gameObject.transform);
                DateText22.transform.position = new(399.00f, 260.00f, 0f);
                DateText22.GetComponent<TextMeshProUGUI>().text = DateList[3].Archivetext.ToString();

                break;
            case "Level4":
                archivePanel.BroadcastMessage("DestoryThis");
                DateLoad("TimeLine");
                Title2 = Instantiate(TitleText, gameObject.transform);
                Title2.transform.position = new(399.00f, 336.50f, 0f);
                Title2.GetComponent<TextMeshProUGUI>().text = DateList[4].Archivename;


                DateText22 = Instantiate(DateText, gameObject.transform);
                DateText22.transform.position = new(399.00f, 260.00f, 0f);
                DateText22.GetComponent<TextMeshProUGUI>().text = DateList[4].Archivetext.ToString();
                break;
            case "Level5":
                archivePanel.BroadcastMessage("DestoryThis");
                DateLoad("TimeLine");
                Title2 = Instantiate(TitleText, gameObject.transform);
                Title2.transform.position = new(399.00f, 336.50f, 0f);
                Title2.GetComponent<TextMeshProUGUI>().text = DateList[5].Archivename;

                Image2 = Instantiate(ArchiveImage, gameObject.transform);
                Image2.transform.position = new(399.00f, 260.00f, 0f);
                Image2.GetComponent<Image>().sprite = Resources.Load("ResourcesDate/ArchiveImage/HIstoryimage/06", typeof(Sprite)) as Sprite;

                DateText22 = Instantiate(DateText, gameObject.transform);
                DateText22.transform.position = new(399.00f, 260.00f, 0f);
                DateText22.GetComponent<TextMeshProUGUI>().text = DateList[5].Archivetext.ToString();

                break;
            case "Teach0":
                archivePanel.BroadcastMessage("DestoryThis");
                DateLoad("Teach");
                Title2 = Instantiate(TitleText, gameObject.transform);
                Title2.transform.position = new(399.00f, 336.50f, 0f);
                Title2.GetComponent<TextMeshProUGUI>().text = DateList[0].Archivename;

                Image2 = Instantiate(ArchiveImage2, gameObject.transform);
                Image2.transform.position = new(399.00f, 260.00f, 0f);
                Image2.GetComponent<Image>().sprite = Resources.Load("Image/001", typeof(Sprite)) as Sprite;

                DateText22 = Instantiate(DateText, gameObject.transform);
                DateText22.transform.position = new(399.00f, 190.00f, 0f);
                DateText22.GetComponent<TextMeshProUGUI>().text = DateList[0].Archivetext.ToString();

                break;
            case "Teach1":
                archivePanel.BroadcastMessage("DestoryThis");
                DateLoad("Teach");
                Title2 = Instantiate(TitleText, gameObject.transform);
                Title2.transform.position = new(399.00f, 336.50f, 0f);
                Title2.GetComponent<TextMeshProUGUI>().text = DateList[1].Archivename;

                Image2 = Instantiate(ArchiveImage2, gameObject.transform);
                Image2.transform.position = new(399.00f, 260.00f, 0f);
                Image2.GetComponent<Image>().sprite = Resources.Load("Image/002", typeof(Sprite)) as Sprite;

                DateText22 = Instantiate(DateText, gameObject.transform);
                DateText22.transform.position = new(399.00f, 190.00f, 0f);
                DateText22.GetComponent<TextMeshProUGUI>().text = DateList[1].Archivetext.ToString();

                break;
            case "Teach2":
                archivePanel.BroadcastMessage("DestoryThis");
                DateLoad("Teach");
                Title2 = Instantiate(TitleText, gameObject.transform);
                Title2.transform.position = new(399.00f, 336.50f, 0f);
                Title2.GetComponent<TextMeshProUGUI>().text = DateList[2].Archivename;

                Image2 = Instantiate(ArchiveImage, gameObject.transform);
                Image2.transform.position = new(399.00f, 260.00f, 0f);
                Image2.GetComponent<Image>().sprite = Resources.Load("Image/003", typeof(Sprite)) as Sprite;

                DateText22 = Instantiate(DateText, gameObject.transform);
                DateText22.transform.position = new(399.00f, 190.00f, 0f);
                DateText22.GetComponent<TextMeshProUGUI>().text = DateList[2].Archivetext.ToString();

                break;
            case "Teach3":
                archivePanel.BroadcastMessage("DestoryThis");
                DateLoad("Teach");
                Title2 = Instantiate(TitleText, gameObject.transform);
                Title2.transform.position = new(399.00f, 336.50f, 0f);
                Title2.GetComponent<TextMeshProUGUI>().text = DateList[3].Archivename;

                Image2 = Instantiate(ArchiveImage, gameObject.transform);
                Image2.transform.position = new(399.00f, 260.00f, 0f);
                Image2.GetComponent<Image>().sprite = Resources.Load("Image/004", typeof(Sprite)) as Sprite;

                DateText22 = Instantiate(DateText, gameObject.transform);
                DateText22.transform.position = new(399.00f, 190.00f, 0f);
                DateText22.GetComponent<TextMeshProUGUI>().text = DateList[3].Archivetext.ToString();

                break;
            case "Teach4":
                archivePanel.BroadcastMessage("DestoryThis");
                DateLoad("Teach");
                Title2 = Instantiate(TitleText, gameObject.transform);
                Title2.transform.position = new(399.00f, 336.50f, 0f);
                Title2.GetComponent<TextMeshProUGUI>().text = DateList[4].Archivename;

                Image2 = Instantiate(ArchiveImage, gameObject.transform);
                Image2.transform.position = new(399.00f, 260.00f, 0f);
                Image2.GetComponent<Image>().sprite = Resources.Load("Image/005", typeof(Sprite)) as Sprite;

                DateText22 = Instantiate(DateText, gameObject.transform);
                DateText22.transform.position = new(399.00f, 190.00f, 0f);
                DateText22.GetComponent<TextMeshProUGUI>().text = DateList[4].Archivetext.ToString();

                break;
            case "Teach5":
                archivePanel.BroadcastMessage("DestoryThis");
                DateLoad("Teach");
                Title2 = Instantiate(TitleText, gameObject.transform);
                Title2.transform.position = new(399.00f, 336.50f, 0f);
                Title2.GetComponent<TextMeshProUGUI>().text = DateList[5].Archivename;

               

                DateText22 = Instantiate(DateText, gameObject.transform);
                DateText22.transform.position = new(399.00f, 260.00f, 0f);
                DateText22.GetComponent<TextMeshProUGUI>().text = DateList[5].Archivetext.ToString();

                break;
            case "Teach6":
                archivePanel.BroadcastMessage("DestoryThis");
                DateLoad("Teach");
                Title2 = Instantiate(TitleText, gameObject.transform);
                Title2.transform.position = new(399.00f, 336.50f, 0f);
                Title2.GetComponent<TextMeshProUGUI>().text = DateList[6].Archivename;

                

                DateText22 = Instantiate(DateText, gameObject.transform);
                DateText22.transform.position = new(399.00f, 260.00f, 0f);
                DateText22.GetComponent<TextMeshProUGUI>().text = DateList[6].Archivetext.ToString();

                break;
            case "Teach7":
                archivePanel.BroadcastMessage("DestoryThis");
                DateLoad("Teach");
                Title2 = Instantiate(TitleText, gameObject.transform);
                Title2.transform.position = new(399.00f, 336.50f, 0f);
                Title2.GetComponent<TextMeshProUGUI>().text = DateList[7].Archivename;

                

                DateText22 = Instantiate(DateText, gameObject.transform);
                DateText22.transform.position = new(399.00f, 260.00f, 0f);
                DateText22.GetComponent<TextMeshProUGUI>().text = DateList[7].Archivetext.ToString();

                break;
            case "Teach8":
                archivePanel.BroadcastMessage("DestoryThis");
                DateLoad("Teach");
                Title2 = Instantiate(TitleText, gameObject.transform);
                Title2.transform.position = new(399.00f, 336.50f, 0f);
                Title2.GetComponent<TextMeshProUGUI>().text = DateList[8].Archivename;

                Image2 = Instantiate(ArchiveImage, gameObject.transform);
                Image2.transform.position = new(399.00f, 260.00f, 0f);
                Image2.GetComponent<Image>().sprite = Resources.Load("Image/009", typeof(Sprite)) as Sprite;

                DateText22 = Instantiate(DateText, gameObject.transform);
                DateText22.transform.position = new(399.00f, 190.00f, 0f);
                DateText22.GetComponent<TextMeshProUGUI>().text = DateList[8].Archivetext.ToString();

                break;
            case "Teach9":
                archivePanel.BroadcastMessage("DestoryThis");
                DateLoad("Teach");
                Title2 = Instantiate(TitleText, gameObject.transform);
                Title2.transform.position = new(399.00f, 336.50f, 0f);
                Title2.GetComponent<TextMeshProUGUI>().text = DateList[9].Archivename;

                

                DateText22 = Instantiate(DateText, gameObject.transform);
                DateText22.transform.position = new(399.00f, 260.00f, 0f);
                DateText22.GetComponent<TextMeshProUGUI>().text = DateList[9].Archivetext.ToString();

                break;
            case "Return":

                MMSceneLoadingManager.LoadScene("UItext", "LoadingScene");
                break;
        }

    }
}
