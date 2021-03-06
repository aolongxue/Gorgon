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
// Created: Tuesday, June 4, 2013 10:17:36 PM
// 
#endregion

using SharpDX.DXGI;
using D3D = SharpDX.Direct3D11;
using GorgonLibrary.Diagnostics;

namespace GorgonLibrary.Graphics
{
	/// <summary>
	/// A render target bound to a 3D texture.
	/// </summary>
	/// <remarks>
	/// A 3D render target is a texture that can be used to receive rendering data in the pipeline by binding it as a render target.  Because it is inherited from <see cref="GorgonLibrary.Graphics.GorgonTexture3D">GorgonTexture3D</see> 
	/// it can be cast to that type and used as a normal 2D texture.  Also, for convenience, it can also be cast to a <see cref="GorgonLibrary.Graphics.GorgonRenderTargetView">GorgonRenderTargetView</see> or 
	/// a <see cref="GorgonLibrary.Graphics.GorgonShaderView">GorgonTextureShaderView</see> to allow ease of <see cref="GorgonLibrary.Graphics.GorgonOutputMerger.SetRenderTarget">binding a render target</see> to the output merger stage in the pipeline or 
	/// to the <see cref="GorgonLibrary.Graphics.GorgonShaderState{T}.Resources">shader resource list</see>.
	/// </remarks>
	public class GorgonRenderTarget3D
		: GorgonTexture3D
	{
		#region Variables.
        private GorgonRenderTargetTextureView _defaultRenderTargetView;     // The default render target view for this render target.
        #endregion

		#region Properties.
		/// <summary>
		/// Property to return the settings for the render target.
		/// </summary>
		public new GorgonRenderTarget3DSettings Settings
		{
			get;
			private set;
		}

		/// <summary>
		/// Property to return the default viewport for the render target.
		/// </summary>
		public GorgonViewport Viewport
		{
			get;
			private set;
		}
		#endregion

		#region Methods.
        /// <summary>
        /// Function to clean up any internal resources.
        /// </summary>
        protected override void CleanUpResource()
        {
            Gorgon.Log.Print("GorgonRenderTarget '{0}': Releasing D3D11 render target view...", LoggingLevel.Intermediate, Name);
            GorgonRenderStatistics.RenderTargetCount--;
            GorgonRenderStatistics.RenderTargetSize -= SizeInBytes;

            base.CleanUpResource();
        }

        /// <summary>
        /// Function to initialize the texture with optional initial data.
        /// </summary>
        /// <param name="initialData">Data used to populate the image.</param>
        protected override void OnInitialize(GorgonImageData initialData)
        {
            if ((Settings.Format != BufferFormat.Unknown) && (Settings.TextureFormat == BufferFormat.Unknown))
            {
                Settings.TextureFormat = Settings.Format;
            }

            var desc = new D3D.Texture3DDescription
            {
                Format = (Format)Settings.TextureFormat,
                Width = Settings.Width,
                Height = Settings.Height,
                Depth = Settings.Depth,
                MipLevels = Settings.MipCount,
                BindFlags = GetBindFlags(false, true),
                Usage = D3D.ResourceUsage.Default,
                CpuAccessFlags = D3D.CpuAccessFlags.None,
                OptionFlags = D3D.ResourceOptionFlags.None
            };

            Gorgon.Log.Print("{0} {1}: Creating 2D render target texture...", LoggingLevel.Verbose, GetType().Name, Name);

            // Create the texture.
            D3DResource = initialData != null
							  ? new D3D.Texture3D(Graphics.D3DDevice, desc, initialData.Buffers.DataBoxes)
                              : new D3D.Texture3D(Graphics.D3DDevice, desc);

            // Create the default render target view.
            _defaultRenderTargetView = GetRenderTargetView(Settings.Format, 0, 0, 1);

            GorgonRenderStatistics.RenderTargetCount++;
            GorgonRenderStatistics.RenderTargetSize += SizeInBytes;

            // Set default viewport.
            Viewport = new GorgonViewport(0, 0, Settings.Width, 1.0f, 0.0f, 1.0f);
        }

        /// <summary>
        /// Function to retrieve a render target view.
        /// </summary>
        /// <param name="format">Format of the new render target view.</param>
        /// <param name="mipSlice">Mip level index to use in the view.</param>
        /// <param name="arrayIndex">Array index to use in the view.</param>
        /// <param name="arrayCount">Number of array indices to use.</param>
        /// <returns>A render target view.</returns>
        /// <remarks>Use this to create/retrieve a render target view that can bind a portion of the target to the pipeline as a render target.
        /// <para>The <paramref name="format"/> for the render target view does not have to be the same as the render target backing texture, and if the format is set to Unknown, then it will 
        /// use the format from the texture.</para>
        /// </remarks>
		/// <exception cref="GorgonLibrary.GorgonException">Thrown when the view could not created or retrieved from the internal cache.</exception>
        public GorgonRenderTargetTextureView GetRenderTargetView(BufferFormat format, int mipSlice, int arrayIndex,
                                                               int arrayCount)
        {
            return OnGetRenderTargetView(format, mipSlice, arrayIndex, arrayCount);
        }

        /// <summary>
        /// Function to clear the render target.
        /// </summary>
        /// <param name="color">Color used to clear the render target.</param>
        /// <param name="deferred">[Optional] A deferred context to use when clearing the render target.</param>
        /// <remarks>This will only clear the render target.  Only the default view will be cleared, any extra views will not be cleared. Any attached depth/stencil buffer will remain untouched.
        /// <para>
        /// If the <paramref name="deferred"/> parameter is NULL (Nothing in VB.Net), the immediate context will be used to clear the render target.  If it is non-NULL, then it 
        /// will use the specified deferred context to clear the render target.
        /// <para>If you are using a deferred context, it is necessary to use that context to clear the render target because 2 threads may not access the same resource at the same time.  
        /// Passing a separate deferred context will alleviate that.</para>
        /// </para>
        /// </remarks>
        public void Clear(GorgonColor color, GorgonGraphics deferred = null)
        {
            _defaultRenderTargetView.Clear(color, deferred);
        }

        /// <summary>
        /// Function to retrieve the render target view for a render target.
        /// </summary>
        /// <param name="target">Render target to evaluate.</param>
        /// <returns>The render target view for the swap chain.</returns>
        public static GorgonRenderTargetTextureView ToRenderTargetView(GorgonRenderTarget3D target)
        {
            return target == null ? null : target._defaultRenderTargetView;
        }

        /// <summary>
        /// Implicit operator to return the render target view for a render target
        /// </summary>
        /// <param name="target">Render target to evaluate.</param>
        /// <returns>The render target view for the swap chain.</returns>
        public static implicit operator GorgonRenderTargetTextureView(GorgonRenderTarget3D target)
        {
            return target == null ? null : target._defaultRenderTargetView;
        }
        #endregion

		#region Constructor/Destructor.
		/// <summary>
		/// Initializes a new instance of the <see cref="GorgonRenderTarget3D"/> class.
		/// </summary>
		/// <param name="graphics">The graphics interface that created this object.</param>
		/// <param name="name">The name of the render target.</param>
		/// <param name="settings">Settings to apply to the render target.</param>
		internal GorgonRenderTarget3D(GorgonGraphics graphics, string name, GorgonRenderTarget3DSettings settings)
			: base(graphics, name, settings)
		{
            Settings = (GorgonRenderTarget3DSettings)settings.Clone();
		}
		#endregion
	}
}
