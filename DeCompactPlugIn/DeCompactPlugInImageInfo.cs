using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Slb.Ocean.Petrel.UI;

namespace DeCompactPlugIn
{
	/// <summary>
	/// This class provides the image information for domain objects and Slb.Ocean.Petrel.UI.ToggleWindows.
	/// </summary>
	internal class DeCompactPlugInImageInfo : ImageInfo
	{
		public DeCompactPlugInImageInfo()
		{
		}

		/// <summary>
		/// Gets the image to display with the Type in the Settings dialog.  Return null
		/// for default image.		
		/// </summary>
		/// <remarks>
		/// The ImageInfo implementation is responsible for disposing the bitmap.
		/// </remarks>
		public override Bitmap TypeImage
		{
			get
			{
				return PetrelImages.Ball;
			}
		}

		/// <summary>
		/// Gets the display image.  Return null for default image.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <returns>Tree icon.</returns>
		/// <remarks>
		/// The ImageInfo implementation is responsible for disposing the bitmap.
		/// </remarks>
		public override Bitmap GetDisplayImage(ImageInfoContext context)
		{
			return PetrelImages.Ball;
		}
	}
}
