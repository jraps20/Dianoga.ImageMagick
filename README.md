# Dianoga.ImageMagick

An Add-on to the widely used Dianoga Automatic Image Resizing module by Kamsar. 

https://github.com/kamsar/Dianoga

This add-on specifically targets the JPEG image format.  It utilizes the ImageMagick portable exe to reduce the overall size and dimensions (if desired) of images in Sitecore.

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

* `Media.Resizing.Quality`
* `Media.Resizing.MaxWidth`
* `Media.Resizing.MaxHeight`

`Media.Resizing.Quality` is the only required setting.  Given a value, ImageMagick will convert the requested image to the set quality and store it in the sites `MediaCache`.

If either `Media.Resizing.MaxWidth` or `Media.Resizing.MaxHeight` are set, or if *both* are set, it will enforce these on the image. Therefore, if an image is requested that has a larger width than is supported in the `Media.Resizing.MaxWidth`, it will be resized keeping its aspect ratio down to the MaxWidth.  The same goes for the height.

To change any of these values, edit **App_Config\Include\Dianoga\Dianoga.Z.ImageMagick.config** or patch in via a separate config file.