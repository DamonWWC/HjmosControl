using System.ComponentModel;


namespace Hjmos.CommonControls
{
    public enum ConfestionStatus
    {
        /// <summary>
        /// 宽松车厢
        /// </summary>
        [Description("宽松车厢")]
        Easy,
        [Description("一般车厢")]
        Normal,
        [Description("拥挤车厢")]
        Congestion

    }
}
