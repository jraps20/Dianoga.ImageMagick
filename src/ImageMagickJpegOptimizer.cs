using Dianoga.Optimizers;

namespace Dianoga.ImageMagick
{
    public class ImageMagickJpegOptimizer : CommandLineToolOptimizer
    {
        private static int Quality => Sitecore.Configuration.Settings.GetIntSetting("Media.Resizing.Quality", 72);

        private static int MaxWidth => Sitecore.Configuration.Settings.GetIntSetting("Media.Resizing.MaxWidth", 2000);
        private static int MaxHeight => Sitecore.Configuration.Settings.GetIntSetting("Media.Resizing.MaxHeight", 1000);

        protected override string CreateToolArguments(string tempFilePath, string tempOutputPath)
        {
            if (Quality <= 0)
                return string.Empty;

            if(MaxWidth > 0 && MaxHeight > 0)
                return $"convert \"{tempFilePath}\" -resize x{MaxHeight}^> -resize {MaxWidth}^> -quality {Quality} \"{tempOutputPath}\"";
            
            if (MaxWidth > 0)
                return $"convert \"{tempFilePath}\" -resize {MaxWidth}> -quality {Quality} \"{tempOutputPath}\"";

            if (MaxHeight > 0)
                return $"convert \"{tempFilePath}\" -resize x{MaxHeight}> -quality {Quality} \"{tempOutputPath}\"";

            return $"convert \"{tempFilePath}\" -quality {Quality} \"{tempOutputPath}\"";
        }
    }
}