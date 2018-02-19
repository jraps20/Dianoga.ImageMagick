using Dianoga.Optimizers;
using Sitecore.Configuration;

namespace Dianoga.ImageMagick
{
    public class ImageMagickResizer : CommandLineToolOptimizer
    {
        private static int MaxWidth => Settings.Media.Resizing.MaxWidth;
        private static int MaxHeight => Settings.Media.Resizing.MaxHeight;
        
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
                return $"convert \"{tempFilePath}\" {AdditionalImageMagick} -resize x{MaxHeight}> -resize {MaxWidth}> \"{tempOutputPath}\"";
            
            if (MaxWidth > 0)
                return $"convert \"{tempFilePath}\" {AdditionalImageMagick} -resize {MaxWidth}> \"{tempOutputPath}\"";

            if (MaxHeight > 0)
                return $"convert \"{tempFilePath}\" {AdditionalImageMagick} -resize x{MaxHeight}> \"{tempOutputPath}\"";

            return "";
        }
    }
}