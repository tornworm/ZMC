using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActorItme : BasePanel
{
    public Text lv;
    public Text Name;
    public Image image;
    public Button singinBtn;
    public void Init(Actor actor,GameObject ins)
    {       
        ins.transform.Find("Lv/LvNum").GetComponent<Text>().text = actor.lv.ToString();
        ins.transform.Find("name").GetComponent<Text>().text = actor.name;
        if (actor.gender == "男")
        {
            ins.GetComponent<Image>().sprite = Resources.Load<Sprite>("Texture/UI/equip/character_002");
        }
        else
        {
            ins.GetComponent<Image>().sprite = Resources.Load<Sprite>("Texture/UI/equip/equip_role_001");
        }
        ins.GetComponent<Button>().onClick.AddListener(OnClickBtnSingIn);
    }
    public void OnClickBtnSingIn()
    {
        GameSceneManager.Instance.LoadSceneShowLoadingAsync("Main", () => UIManager.Instance.ShowPanel(UIPanelType.PanelMain, PanelFrom.Normal));
        UIManager.Instance.ClosePanel(UIPanelType.PanelActor);
    }
   
}
