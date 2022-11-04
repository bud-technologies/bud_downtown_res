using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public enum FlashLightType
{
    Directional = 0,
    SpotLight = 1
}

public enum FlashLightMode
{
    Queue = 0,
    Random = 1
}

[System.Serializable]
public class FlashLightData
{
    public int id; //uid
    public int type; //0 ������1 �۹��
    public float range; //��Χ
    public float inten; //ǿ��
    public float radius; //�뾶   
    public int isReal; //0 ��ʵʱ�⣻1 ��ʵʱ��
    public int mode; //0 ˳��1 ���
    public int time; //���ż��
    public List<string> colors; //��ɫ
}

public class FlashLightComponent : IComponent
{
    public int id; //uid
    public int type; //0 ������1 �۹��
    public float range; //��Χ
    public float inten; //ǿ��
    public float radius; //�뾶  
    public int isReal; //0 ��ʵʱ�⣻1 ��ʵʱ��
    public int mode; //0 ˳��1 ���
    public int time; //���ż��
    public List<Color> colors; //��ɫ

    public IComponent Clone()
    {
        return new FlashLightComponent
        {
            id = id,
            type = type,
            range = range,
            inten = inten,
            radius = radius,
            colors = new List<Color>(colors),
            mode = mode,
            time = time,
            isReal = isReal
        };
    }

    public BehaviorKV GetAttr()
    {
        FlashLightData data = new FlashLightData
        {
            id = id,
            type = type,
            range = range,
            inten = inten,
            radius = radius,
            colors = ColorToStringList(),
            mode = mode,
            time = time,
            isReal = isReal
        };
        return new BehaviorKV { k = (int)BehaviorKey.FlashLight, v = JsonConvert.SerializeObject(data) };
    }

    public List<string> ColorToStringList()
    {
        List<string> list = new List<string>();
        for(int i = 0; i < colors.Count; ++i)
        {
            list.Add(DataUtils.ColorToString(colors[i]));
        }
        return list;
    }
}
