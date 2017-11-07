using Dianoga.Optimizers;

namespace Dianoga.ImageMagick
{
    public class ImageMagickJpegOptimizer : CommandLineToolOptimizer
    {
        private static int MaxWidth => Sitecore.Configuration.Settings.GetIntSetting("Media.Resizing.MaxWidth", 2000);
        private static int MaxHeight => Sitecore.Configuration.Settings.GetIntSetting("Media.Resizing.MaxHeight", 1000);
        
        private string _additionalImageMagick;
        
        public virtual string AdditionalImageMagick
        {
            get => _additionalImageMagick;
            set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                _additionalImageMagick = value;
            }
        }
        
        protected override string CreateToolArguments(string tempFilePath, string tempOutputPath)
        {
            if(MaxWidth > 0 && MaxHeight > 0)
                return $"convert \"{tempFilePath}\" {AdditionalImageMagick} -resize x{MaxHeight}^> -resize {MaxWidth}^> \"{tempOutputPath}\"";
            
            if (MaxWidth > 0)
                return $"convert \"{tempFilePath}\" {AdditionalImageMagick} -resize {MaxWidth}> \"{tempOutputPath}\"";

            if (MaxHeight > 0)
                return $"convert \"{tempFilePath}\" {AdditionalImageMagick} -resize x{MaxHeight}> \"{tempOutputPath}\"";

            return "";
        }
    }
}