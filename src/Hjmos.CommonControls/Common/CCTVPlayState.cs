
namespace Hjmos.CommonControls.Commmon
{
   public enum CCTVPlayState
    {
        /// <summary>
        /// 播放器尚未初始化
        /// </summary>
        Uninitialized,
        /// <summary>
        /// 播放器正在初始化
        /// </summary>
        Initializing,
        /// <summary>
        /// 播放器初始化完成
        /// </summary>
        Initialized,
        /// <summary>
        /// 视频正在加载中
        /// </summary>
        Loading,
        /// <summary>
        /// 视频正在播放
        /// </summary>
        Playing,
        /// <summary>
        /// 视频暂停播放
        /// </summary>
        Pause,
        /// <summary>
        /// 视频停止播放
        /// </summary>
        Stop,
        /// <summary>
        /// 异常
        /// </summary>
        Error
    }
}
