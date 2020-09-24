using System.Windows.Input;

namespace Hjmos.BaseControls.Interactivity
{
    public static class ControlCommands
    {
        /// <summary>
        /// 关闭
        /// </summary>
        public static RoutedCommand Close { get; } = new RoutedCommand(nameof(Close), typeof(ControlCommands));
        /// <summary>
        /// 取消
        /// </summary>
        public static RoutedCommand Cancel { get; } = new RoutedCommand(nameof(Cancel), typeof(ControlCommands));
        /// <summary>
        /// 确定  
        /// </summary>
        public static RoutedCommand Confirm { get; } = new RoutedCommand(nameof(Confirm), typeof(ControlCommands));

        public static RoutedCommand Prev { get; } = new RoutedCommand(nameof(Prev), typeof(ControlCommands));

        public static RoutedCommand Next { get; } = new RoutedCommand(nameof(Next), typeof(ControlCommands));

        public static RoutedCommand Clear { get; } = new RoutedCommand(nameof(Clear), typeof(ControlCommands));

        public static RoutedCommand Switch { get; } = new RoutedCommand(nameof(Switch), typeof(ControlCommands));

    }
}
