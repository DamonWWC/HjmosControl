using System;

namespace Hjmos.CommonControls.Tools
{
    public class ImageSource
    {
        private const string IMAGE_ROOT_PATH = @"pack://application:,,,/Hjmos.CommonControls;component/Resources/Image/";

        public const string Easy = "宽松.svg";
        public const string Normal = "一般.svg";
        public const string Congestion = "拥挤.svg";


        public string GetValue<T>(T enumName)
        {
            var propertyName = Enum.GetName(typeof(T), enumName);
            var result = GetType().GetField(propertyName).GetValue(null).ToString();
            return $"{IMAGE_ROOT_PATH}{result}";
        }

    }
}
