﻿#region MIT.
// 
// Gorgon.
// Copyright (C) 2013 Michael Winsor
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// 
// Created: Saturday, October 12, 2013 10:28:27 PM
// 
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using GorgonLibrary.Graphics.Properties;
using GorgonLibrary.IO;

namespace GorgonLibrary.Graphics
{
	/// <summary>
	/// A brush used to draw glyphs using a texture.
	/// </summary>
	/// <remarks>The texture used by this brush is a System.Drawing.Image object and not a Gorgon texture.</remarks>
	public class GorgonGlyphTextureBrush
		: GorgonGlyphBrush
	{
		#region Variables.
		private string _deferredTextureName = string.Empty;				// Name of a deferred texture.
		private readonly GorgonTexture2DSettings _textureSettings;		// Texture settings.
		private GorgonTexture2D _texture;								// Texture to bind with the brush.
		private readonly GorgonGraphics _graphics;						// Graphics interface to manage the texture used by the brush.
		#endregion

		#region Properties.
		/// <summary>
		/// Property to return the type of brush.
		/// </summary>
		public override GlyphBrushType BrushType
		{
			get
			{
				return GlyphBrushType.Texture;
			}
		}

		/// <summary>
		/// Property to set or return the wrapping mode for the gradient fill.
		/// </summary>
		public WrapMode WrapMode
		{
			get;
			set;
		}

		/// <summary>
		/// Property to set or return the region for a single gradient run.
		/// </summary>
		/// <remarks>This value is in texture coordinates.  To use image coordinates, use one of the <see cref="GorgonTexture2D.ToTexel(SlimMath.Vector2)"/> functions to convert.</remarks>
		public RectangleF TextureRegion
		{
			get;
			set;
		}

		/// <summary>
		/// Property to set or return the texture to apply to the brush.
		/// </summary>
		public GorgonTexture2D Texture
		{
			get
			{
				if ((_texture != null) || (string.IsNullOrWhiteSpace(_deferredTextureName)))
				{
					return _texture;
				}

				FindTexture();

				return _texture;
			}
			private set
			{
				_deferredTextureName = string.Empty;
				_texture = value;
			}
		}
		#endregion

		#region Methods.
		/// <summary>
		/// Function to find a previously loaded texture with the same dimensions, format and name as the texture assigned to this brush.
		/// </summary>
		private void FindTexture()
		{
			IEnumerable<GorgonTexture2D> textures = _graphics.GetTrackedObjectsOfType<GorgonTexture2D>();

			_texture = textures.FirstOrDefault(item =>
			                                   (string.Equals(item.Name,
			                                                  _deferredTextureName,
			                                                  StringComparison.OrdinalIgnoreCase)
			                                    && (item.Settings.Width == _textureSettings.Width)
												&& (item.Settings.Height == _textureSettings.Height)
												&& (item.Settings.Format == _textureSettings.Format)
												&& (item.Settings.ArrayCount == _textureSettings.ArrayCount)
												&& (item.Settings.MipCount == _textureSettings.MipCount)));

			if (_texture != null)
			{
				_deferredTextureName = string.Empty;
			}
		}

		/// <summary>
		/// Function to convert this brush to the equivalent GDI+ brush type.
		/// </summary>
		/// <returns>
		/// The GDI+ brush type for this object.
		/// </returns>
		internal override Brush ToGDIBrush()
		{
			if (Texture == null)
			{
				return null;
			}

			Image[] gdiImage = GorgonGDIImageConverter.CreateGDIImagesFromTexture(Texture);

			if (gdiImage.Length == 0)
			{
				return null;
			}

			// Only use the first level.
			for (int i = 1; i < gdiImage.Length; ++i)
			{
				gdiImage[i].Dispose();
			}

			var textureRect = new RectangleF(0, 0, Texture.Settings.Width, Texture.Settings.Height);
			var imageRect = Texture.ToPixel(TextureRegion);

			imageRect = RectangleF.Intersect(textureRect, imageRect);
			
			if (imageRect == RectangleF.Empty)
			{
				imageRect = textureRect;
			}

			return new TextureBrush(gdiImage[0], imageRect)
			       {
				       WrapMode = WrapMode
			       };
		}

		/// <summary>
		/// Function to write the brush elements out to a chunked file.
		/// </summary>
		/// <param name="chunk">Chunk writer used to persist the data.</param>
		internal override void Write(GorgonChunkWriter chunk)
		{
			// We have no texture.
			if (Texture == null)
			{
				return;
			}

			chunk.Begin("BRSHDATA");
			chunk.Write(BrushType);
			chunk.Write(WrapMode);

			chunk.WriteRectangle(TextureRegion);
			chunk.WriteString(_texture.Name);
			chunk.Write(_texture.Settings.Width);
			chunk.Write(_texture.Settings.Height);
			chunk.Write(_texture.Settings.Format);
			chunk.Write(_texture.Settings.ArrayCount);
			chunk.Write(_texture.Settings.MipCount);

			chunk.End();
		}

		/// <summary>
		/// Function to read the brush elements in from a chunked file.
		/// </summary>
		/// <param name="chunk">Chunk reader used to read the data.</param>
		internal override void Read(GorgonChunkReader chunk)
		{
			WrapMode = chunk.Read<WrapMode>();

			if (Texture != null)
			{
				Texture.Dispose();
				Texture = null;
			}

			TextureRegion = chunk.ReadRectangleF();
			_deferredTextureName = chunk.ReadString();
			_textureSettings.Width = chunk.ReadInt32();
			_textureSettings.Height = chunk.ReadInt32();
			_textureSettings.Format = chunk.Read<BufferFormat>();
			_textureSettings.ArrayCount = chunk.ReadInt32();
			_textureSettings.MipCount = chunk.ReadInt32();

			// Try to find the appropriate texture in any previously loaded texture list.
			FindTexture();
		}
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="GorgonGlyphTextureBrush"/> class.
		/// </summary>
		/// <param name="graphics">Graphics interface that is managing the texture used by the brush.</param>
		internal GorgonGlyphTextureBrush(GorgonGraphics graphics)
		{
			_graphics = graphics.ImmediateContext;
			_textureSettings = new GorgonTexture2DSettings
			                   {
								   Format = BufferFormat.R8G8B8A8_UIntNormal,
								   ArrayCount = 1,
								   MipCount = 1
			                   };
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GorgonGlyphPathGradientBrush"/> class.
		/// </summary>
		/// <param name="textureImage">The texture to use.</param>
		/// <remarks>The texture format for the brush must be R8G8B8A8_UIntNormal, R8G8B8A8_UIntNormal_sRGB, B8G8R8A8_UIntNormal, or B8G8R8A8_UintNormal_sRGB.</remarks>
		/// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="textureImage"/> parameter is NULL (Nothing in VB.Net).</exception>
		/// <exception cref="System.ArgumentException">Thrown when the <paramref name="textureImage"/> parameter uses an unsupported format.</exception>
		public GorgonGlyphTextureBrush(GorgonTexture2D textureImage)
		{
			if (textureImage == null)
			{
				throw new ArgumentNullException("textureImage");	
			}

			if ((textureImage.Settings.Format != BufferFormat.R8G8B8A8_UIntNormal_sRGB)
				&& (textureImage.Settings.Format != BufferFormat.R8G8B8A8_UIntNormal)
				&& (textureImage.Settings.Format != BufferFormat.B8G8R8A8_UIntNormal)
				&& (textureImage.Settings.Format != BufferFormat.B8G8R8A8_UIntNormal_sRGB))
			{
				throw new ArgumentException(string.Format(Resources.GORGFX_FORMAT_NOT_SUPPORTED, textureImage.Settings.Format));
			}

			Texture = textureImage;
			TextureRegion = new RectangleF(0, 0, 1, 1);
			_textureSettings = textureImage.Settings;
			_graphics = Texture.Graphics.ImmediateContext;
		}
		#endregion
	}
}
