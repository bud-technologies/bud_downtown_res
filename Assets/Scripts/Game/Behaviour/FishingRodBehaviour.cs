/// <summary>
/// Author: Tee Li
/// ���ڣ�2022/8/30
/// �����Ϊ
/// </summary>

public class FishingRodBehaviour : NodeBaseBehaviour
{
    public override void OnInitByCreate()
    {
        if (! gameObject.GetComponent<SpawnPointConstrainer>())
            gameObject.AddComponent<SpawnPointConstrainer>();
    }
}
