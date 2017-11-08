# Dianoga.ImageMagick

An Add-on to the widely used Dianoga Automatic Image Resizing module by Kamsar. 

https://github.com/kamsar/Dianoga

It utilizes the ImageMagick portable exe to reduce dimensions of images in Sitecore by enforcing the `Media.Resizing.MaxWidth` and `Media.Resizing.MaxHeight` settings as well as a few ImageMagick specific optimizations.

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

## How Does It Do it?

When an image is requested and it exceeds the `MaxWidth` or `MaxHeight` settings, ImageMagick resizes the image, retaining the aspect ratio. It then sends the resized image to the default Dianoga processor (JpegTrans for JPEG, PngOptimizer for PNG) to reduce the size even more.

This prevents the server from sending very large images to the client. By default, Sitecore *will allow* images larger than the `MaxWidth` or height if the original image in the Media Library is larger than these set sizes. The only real use for these settings by default, is to disallow upscaling of images, which does not help if content authors upload overly large images to begin with.

## Extra Features

JPEG

`<AdditionalImageMagick>-quality 70 -dither None -gaussian-blur 0.05 -define jpeg:fancy-upsampling=off -interlace none -colorspace sRGB</AdditionalImageMagick>`

Note that quality was moved from `Media.Resizing.Quality` to direclty within the ImageMagick call. This is due to the fact that Sitecore will not adhere to the quality setting for an image that does not alter the original image.  For example, if an author were to embed a very large, high-quality image into a rich text field, the image will be served as-is. By moving the setting to ImageMagick, it can run the quality check for all images.

PNG

`<AdditionalImageMagick>-define png:compression-filter=5 -define png:compression-level=9 -define png:compression-strategy=1 -gaussian-blur 0.05</AdditionalImageMagick>`

Some various, standard compression parameters included. For quicker resizing, remove some of the options to sacrifice final image size.

### A note regarding Gaussian Blur

This setting reduces the final image size significantly. If the images appear to blurred, remove this option. In my testing it was difficult to notice unless flipping between the original and converted image.