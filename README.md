# Dianoga.ImageMagick

An Add-on to the widely used Dianoga Automatic Image Resizing module by Kamsar. 

https://github.com/kamsar/Dianoga

This add-on performs lossy compression with the ImageMagick portable exe, whereas native Dianoga does not drop the quality of the image. JPEGs can often be reduced to 70% quality without any noticable impact on the final rendered image. The result is a drastically smaller file size for the image. It also enforces the Sitecore settings `Media.Resizing.MaxWidth` and `Media.Resizing.MaxHeight`. It will not allow an image of greater dimensions to be served. It also includes a few ImageMagick specific optimizations that can be modififed.

By default, `MaxWidth` and `MaxHeight` are not enforced on an image unless the image is upscaled by Sitecore via query string parameters on the image. Therefore if an image is uploaded at 5000px x 5000px, Sitecore will server this full, large image to the end user. The default settings for Dianoga.ImageMagick are 1920x1080. No image larger than this will be served. When downscaling, the original image proportions are respected. 

# Installation

Simply install the NuGet package "Dianoga.ImageMagick" (https://www.nuget.org/packages/Dianoga.ImageMagick) into your Sitecore web project, build and deploy to your Sitecore root.

That's it :)

## What Does It Install?

Aside from the files included with Dianoga, Dianoga.ImageMagick includes the following:

* **Dianoga.ImageMagick.dll**
* **App_Config\Include\Dianoga\Dianoga.Z.ImageMagick.config**
* **App_Data\Dianoga Tools\imagemagick\magic.xml**
* **App_Data\Dianoga Tools\imagemagick\magick.exe**

# Usage

Dianoga.ImageMagick keys off of the following settings included with Sitecore:

* `Media.Resizing.MaxWidth`
* `Media.Resizing.MaxHeight`

If either `Media.Resizing.MaxWidth` or `Media.Resizing.MaxHeight` are set, or if *both* are set, it will enforce these on the image. Therefore, if an image is requested that has a larger width than is supported in the `Media.Resizing.MaxWidth`, it will be resized keeping its aspect ratio down to the MaxWidth.  The same goes for the height.

To change any of these values, edit **App_Config\Include\Dianoga\Dianoga.Z.ImageMagick.config** or patch in via a separate config file.

## Image Resizing?

When an image is requested and it exceeds the `MaxWidth` or `MaxHeight` settings, ImageMagick resizes the image, retaining the aspect ratio. It then sends the resized image to the default Dianoga processor (JpegTrans for JPEG, PngOptimizer for PNG) to reduce the size even more.

This prevents the server from sending very large images to the client. By default, Sitecore *will allow* images larger than the `MaxWidth` or height if the original image in the Media Library is larger than these set sizes. The only real use for these settings by default, is to disallow upscaling of images, which does not help if content authors upload overly large images to begin with.

## Lossy Compression

Dianoga.ImageMagick intentionally sets the `Media.Resizing.Quality` setting to 100, to allow the ImageMagick-magic to reduce the quality. This prevents double quality drop from occuring.

JPEG

`<AdditionalImageMagick>-quality 70 -dither None -gaussian-blur 0.05 -define jpeg:fancy-upsampling=off -interlace none -colorspace sRGB</AdditionalImageMagick>`

Note that quality was moved from `Media.Resizing.Quality` to direclty within the ImageMagick call. This is due to the fact that Sitecore will not adhere to the quality setting for an image that does not alter the original image.  For example, if an author were to embed a very large, high-quality image into a rich text field, the image will be served as-is. By moving the setting to ImageMagick, it can run the quality check for all images.

PNG

`<AdditionalImageMagick>-define png:compression-filter=5 -define png:compression-level=9 -define png:compression-strategy=1 -gaussian-blur 0.05</AdditionalImageMagick>`

Some various, standard compression parameters included. For quicker resizing, remove some of the options to sacrifice final image size.

### A note regarding Gaussian Blur

This setting reduces the final image size significantly. If the images appear to blurred, remove this option. In my testing it was difficult to notice unless flipping between the original and converted image.