using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XReal.XTown.UI;

public class Item : UIBase
{
    // 1. enum �����Ӱ� ����
    enum Images { Image }

    private string _itemName;

    private void Start()
    {
        Init();
    }

    // 2. Item Button�� OnClick_ItemUse Bind
    public override void Init()
    {
        Bind<GameObject>(typeof(Images));
        GameObject image = GetUIComponent<GameObject>((int)Images.Image);
        image.BindEvent(OnClick_ItemUse);
    }

    /// <summary>
    /// 3. OnClick_ItemUse
    /// 1) ItemProperty.GetItemProperty�� _itemName �̿��ؼ� ItemProperty ����
    /// 2) ���� �ش� ������ ������ 0���� ũ�ٸ�
    /// 3) ���� -1 & ��ü �ı�
    /// 4) ItemAction();
    /// </summary>
    public void OnClick_ItemUse(PointerEventData data)
    {
        ItemProperty itemProperty = ItemProperty.GetItemProperty(_itemName);
        if (itemProperty.ItemNumber > 0)
            itemProperty.ItemNumber--;
        Destroy(gameObject);
        ItemAction();
    }

    /// <summary>
    /// 4. ItemAction:
    /// 1) switch ������ itemProperty.PropertyType �μ��� �ް�
    /// 2) ItemProperty.GetItemProperty�� _itemName �̿��ؼ� ItemProperty �����ؼ�
    /// 3) Damage���, GameManager.Instance().GetCharacter("Player")�� �÷��̾� �����ؼ� ������ �߰�
    /// 4) Heal�̶�� �����ϰ� �����ؼ� ü�� �߰� + SceneUI�� CharacterHP() ȣ��
    /// </summary>
    public void ItemAction()
    {
        Character Player = GameManager.Instance().GetCharacter("Player");
        System.Random rand = new System.Random();
        switch (_itemName)
        {
            case "HealItem":
                if (Player._myHp < Player._myHpMax)
                {
                    Player._myHp += (Player._myHpMax - Player._myHp) < 10 ? (Player._myHpMax - Player._myHp) : 10;
                }
                UIManager.UI._sceneUI.GetComponent<SceneUI>().CharacterHp();
                break;
            case "FireSpearItem": // player damage up by random
                if (rand.Next(0, 10) < 5)
                {
                    Player._myDamage += 3;
                }
                break;
            case "FlameItem":// player damage up
                Player._myDamage += 1;
                break;
            default:
                break;
        }
    }

    // 5. SetInfo: itemName�� _itemName�� �Ҵ�
    public void SetInfo(string itemName)
    {
        _itemName = itemName;
    }
}