
using RedDot;

namespace EmoRedDotSystem
{
    public enum ENodeType
    {
        Root,
        Emo, //��ڽڵ㣬���������
        Emoji, //Emoji ��������
        SingleEmo, //���˶�������
        DoubleEmo, //˫�˽�����������
        StateEmo, //״̬����
        EmojiItem,
        SingleEmoItem,
        DoubleEmoItem,
        CollectEmoItem,
        StateEmoItem,
    }
    public class EmoRedDotTreeConstructer : IRedDotTreeConstructer
    {
        public void Construct(RedDotTree tree)
        {
            tree.Construct((int)ENodeType.Root);
        }
    }
    public class EmoRedDotNodeFactory : RedDotNodeFactoryBase
    {
        protected override void OnInit()
        {
            Register<Node>((int)ENodeType.Root);

        }
    }
}
