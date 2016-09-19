using System;

using Slb.Ocean.Core;
using Slb.Ocean.Petrel.UI;
using System.Drawing;

namespace DeCompactPlugIn
{
	/// <summary>
	/// TreeItems usually appear in the Petrel Input tree, as a leaf.
	/// (cannot contain more elements under itself)
	/// </summary>
	class DeCompactPlugIn : INameInfoSource, IImageInfoSource
	{
		/// <summary>
		/// DeCompactPlugIn symbolises this instance in the tree.
		/// </summary>
		private object data;

		public DeCompactPlugIn(object dataobj)
		{
			this.data = dataobj;
			this.Text = "Facies Decompactor";
		}

		public string Text { get; set; }

		#region INameInfoSource Members

		public NameInfo NameInfo
		{
			get { return new DeCompactPlugInNameInfo(this); }
		}

		#endregion

		#region IImageInfoSource Members

		public ImageInfo ImageInfo
		{
			get { return new DeCompactPlugInImageInfo(); }
		}
		
		#endregion
	}

}
