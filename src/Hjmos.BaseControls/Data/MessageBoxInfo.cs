using System.Windows;
using System.Windows.Media;

namespace Hjmos.BaseControls.Data
{
    public class MessageBoxInfo
    {
        public MessageBoxButton Button { get; set; } = MessageBoxButton.OK;
        public string CancelContent { get; set; }
        public string Caption { get; set; }
        public string ConfirmContent { get; set; }
        public MessageBoxResult DefaultResult { get; set; } = MessageBoxResult.None;
        public Geometry Icon { get; set; }
        public Brush IconBrush { get; set; }
        public string IconBrushKey { get; set; }
        public string IconKey { get; set; }
        public string Message { get; set; }
        public string NoContent { get; set; }
        public Style Style { get; set; }

        public string StyleKey { get; set; }
        public string YesContent { get; set; }
    }
}