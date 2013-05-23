﻿#region MIT.
// 
// Gorgon.
// Copyright (C) 2012 Michael Winsor
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
// Created: Tuesday, January 31, 2012 8:21:21 AM
// 
#endregion

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Shaders = SharpDX.D3DCompiler;
using GorgonLibrary.IO;
using GorgonLibrary.Native;
using GorgonLibrary.Graphics.Properties;

namespace GorgonLibrary.Graphics
{
	/// <summary>
	/// Used to manage shader bindings and shader buffers.
	/// </summary>
	public sealed class GorgonShaderBinding
	{
		#region Constants.
		/// <summary>
		/// Header for Gorgon binary shaders.
		/// </summary>
		internal const string BinaryShaderHeader = "GORBINSHD2.0";
		#endregion

		#region Variables.
		private readonly GorgonGraphics _graphics;		// Graphics interface.
		#endregion

		#region Properties.
		/// <summary>
		/// Property to return the current vertex shader states.
		/// </summary>
		public GorgonVertexShaderState VertexShader
		{
			get;
			private set;
		}

		/// <summary>
		/// Property to return the current vertex shader states.
		/// </summary>
		public GorgonPixelShaderState PixelShader
		{
			get;
			private set;
		}

		/// <summary>
		/// Property to return a list of include files for the shaders.
		/// </summary>
		public GorgonShaderIncludeCollection IncludeFiles
		{
			get;
			private set;
		}
		#endregion

		#region Methods.
		/// <summary>
		/// Function clean up any resources within this interface.
		/// </summary>
		internal void CleanUp()
		{
			if (PixelShader != null)
			{
				PixelShader.CleanUp();
			}

			if (VertexShader != null)
			{
				VertexShader.CleanUp();
			}

			PixelShader = null;
			VertexShader = null;
		}

		/// <summary>
		/// Function to re-seat a shader after it's been altered.
		/// </summary>
		/// <param name="shader">Shader to re-seat.</param>
		internal void Reseat(GorgonShader shader)
		{
			var pixelShader = shader as GorgonPixelShader;

			if (pixelShader != null)
			{
				if (PixelShader.Current == pixelShader)
				{
					PixelShader.Current = null;
					PixelShader.Current = pixelShader;
				}

				return;
			}

			// Lastly, check for vertex shaders.
			var vertexShader = shader as GorgonVertexShader;

			if ((vertexShader == null) || (VertexShader.Current != vertexShader))
			{
				return;
			}

			VertexShader.Current = null;
			VertexShader.Current = vertexShader;
		}

        /// <summary>
        /// Function to re-seat a shader resource after it's been altered.
        /// </summary>
        /// <param name="resource">Shader resource to re-seat.</param>
        internal void Reseat(GorgonTexture resource)
        {
            PixelShader.Resources.ReSeat(resource);
            VertexShader.Resources.ReSeat(resource);
        }

		/// <summary>
		/// Function to re-seat a shader resource after it's been altered.
		/// </summary>
		/// <param name="resource">Shader resource to re-seat.</param>
		internal void Reseat(GorgonShaderBuffer resource)
		{
			PixelShader.Resources.ReSeat(resource);
			VertexShader.Resources.ReSeat(resource);
		}

        /// <summary>
        /// Function to re-seat a shader resource after it's been altered.
        /// </summary>
        /// <param name="view">The view to re-seat.</param>
        internal void Reseat(GorgonShaderView view)
        {
            PixelShader.Resources.ReSeat(view);
            VertexShader.Resources.ReSeat(view);
        }

        /// <summary>
        /// Function to unbind a shader resource from all shaders.
        /// </summary>
        /// <param name="resource">Shader resource to unbind.</param>
        internal void Unbind(GorgonTexture resource)
        {
            PixelShader.Resources.Unbind(resource);
            VertexShader.Resources.Unbind(resource);
        }

		/// <summary>
		/// Function to unbind a shader resource from all shaders.
		/// </summary>
		/// <param name="resource">Shader resource to unbind.</param>
		internal void Unbind(GorgonShaderBuffer resource)
		{
			PixelShader.Resources.Unbind(resource);
			VertexShader.Resources.Unbind(resource);
		}

        /// <summary>
        /// Function to unbind a shader resource from all shaders.
        /// </summary>
        /// <param name="view">View to unbind.</param>
        internal void Unbind(GorgonShaderView view)
        {
            PixelShader.Resources.Unbind(view);
            VertexShader.Resources.Unbind(view);
        }

		/// <summary>
		/// Function to unbind a constant buffer from all shaders.
		/// </summary>
		/// <param name="buffer">Bufferr to unbind.</param>
		internal void Unbind(GorgonConstantBuffer buffer)
		{
			PixelShader.ConstantBuffers.Unbind(buffer);
			VertexShader.ConstantBuffers.Unbind(buffer);
		}

		/// <summary>
		/// Function to create an effect object.
		/// </summary>
		/// <typeparam name="T">Type of effect to create.</typeparam>
		/// <param name="name">Name of the effect.</param>
		/// <param name="parameters">Parameters to pass to the shader.</param>
		/// <returns>The new effect object.</returns>
		/// <remarks>Effects are used to simplify rendering with multiple passes when using a shader, similar to the old Direct 3D effects framework.
		/// <para>The <paramref name="parameters"/> parameter is optional, however some effects may require a specific set of parameters passed upon creation. 
		/// This is dependent on the effect and may thrown an exception if a parameter is missing.  Parameter names are case sensitive.</para>
		/// </remarks>
		/// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="name"/> parameter is NULL (Nothing in VB.Net).</exception>
		/// <exception cref="System.ArgumentException">Thrown when the name parameter is an empty string.
		/// <para>-or-</para>
		/// <para>Thrown when the parameter list does not contain a required parameter.</para>
		/// </exception>
		public T CreateEffect<T>(string name, params GorgonEffectParameter[] parameters)
			where T : GorgonEffect
		{
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(Resources.GORGFX_PARAMETER_MUST_NOT_BE_EMPTY, "name");
            }

			// Create the effect.
			var effect = (T)Activator.CreateInstance(typeof(T), new object[] {_graphics, name});

			// Check its required parameters.
			if ((effect.RequiredParameters.Count > 0) && ((parameters == null) || (parameters.Length == 0)))
			{
				throw new ArgumentException(string.Format(Resources.GORGFX_EFFECT_MISSING_REQUIRED_PARAMS, effect.RequiredParameters[0]), "parameters");
			}

			if ((parameters != null) && (parameters.Length > 0))
			{
				// Only get parameters where the key name has a value.
				var validParameters = parameters.Where(item => !string.IsNullOrWhiteSpace(item.Name)).ToArray();

				// Check for predefined required parameters from the effect.
				foreach (var effectParam in effect.RequiredParameters)
				{
					if ((!string.IsNullOrWhiteSpace(effectParam)) 
						&& (!validParameters.Any(item => string.Compare(item.Name, effectParam, StringComparison.OrdinalIgnoreCase) == 0)))
					{
						throw new ArgumentException(string.Format(Resources.GORGFX_EFFECT_MISSING_REQUIRED_PARAMS, effectParam), "parameters");
					}
				}

				// Add/update the parameters.
				foreach (var param in validParameters)
				{
					effect.Parameters[param.Name] = param.Value;
				}
			}

			// Initialize our effect parameters.
			effect.InitializeEffectParameters();

			_graphics.AddTrackedObject(effect);

			return effect;
		}

		/// <summary>
        /// Function to load a shader from a byte array.
        /// </summary>
        /// <typeparam name="T">The shader type.  Must be inherited from <see cref="GorgonLibrary.Graphics.GorgonShader">GorgonShader</see>.</typeparam>
        /// <param name="name">Name of the shader object.</param>
        /// <param name="entryPoint">Entry point method to call in the shader.</param>
        /// <param name="shaderData">Array of bytes containing the shader data.</param>
        /// <returns>The new shader loaded from the data stream.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="shaderData"/>, <paramref name="name"/> or <paramref name="entryPoint"/> parameters are NULL (Nothing in VB.Net).
        /// </exception>
        /// <exception cref="System.ArgumentException">Thrown when the name or entryPoint parameters are empty.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when the shaderData parameter length is less than or equal to 0.</exception>
        /// <exception cref="System.TypeInitializationException">Thrown when the type of shader is unrecognized.</exception>
        /// <exception cref="GorgonLibrary.GorgonException">Thrown when there are compile errors in the shader.</exception>
        public T FromMemory<T>(string name, string entryPoint, byte[] shaderData)
            where T : GorgonShader
        {
#if DEBUG
            return FromMemory<T>(name, entryPoint, shaderData, true);
#else
            return FromMemory<T>(name, entryPoint, shaderData, false);
#endif
        }
        
        /// <summary>
		/// Function to load a shader from a byte array.
		/// </summary>
		/// <typeparam name="T">The shader type.  Must be inherited from <see cref="GorgonLibrary.Graphics.GorgonShader">GorgonShader</see>.</typeparam>
		/// <param name="name">Name of the shader object.</param>
		/// <param name="entryPoint">Entry point method to call in the shader.</param>
		/// <param name="shaderData">Array of bytes containing the shader data.</param>
        /// <param name="isDebug">TRUE to apply debug information, FALSE to exclude it.</param>
        /// <returns>The new shader loaded from the data stream.</returns>
		/// <remarks>The <paramref name="isDebug"/> parameter is only applicable to source code shaders.</remarks>
		/// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="shaderData"/>, <paramref name="name"/> or <paramref name="entryPoint"/> parameters are NULL (Nothing in VB.Net).
		/// </exception>
		/// <exception cref="System.ArgumentException">Thrown when the name or entryPoint parameters are empty.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when the shaderData parameter length is less than or equal to 0.</exception>
		/// <exception cref="System.TypeInitializationException">Thrown when the type of shader is unrecognized.</exception>
		/// <exception cref="GorgonLibrary.GorgonException">Thrown when there are compile errors in the shader.</exception>
        public T FromMemory<T>(string name, string entryPoint, byte[] shaderData, bool isDebug)
            where T : GorgonShader
        {
            if (shaderData == null)
            {
                throw new ArgumentNullException("shaderData");
            }

            using (var memoryStream = new GorgonDataStream(shaderData))
            {
                return FromStream<T>(name, entryPoint, memoryStream, shaderData.Length, isDebug);
            }
        }

		/// <summary>
		/// Function to load a shader from a stream of data.
		/// </summary>
		/// <typeparam name="T">The shader type.  Must be inherited from <see cref="GorgonLibrary.Graphics.GorgonShader">GorgonShader</see>.</typeparam>
		/// <param name="name">Name of the shader object.</param>
		/// <param name="entryPoint">Entry point method to call in the shader.</param>
		/// <param name="stream">Stream to load the shader from.</param>
		/// <param name="size">Size of the shader, in bytes.</param>
		/// <returns>The new shader loaded from the data stream.</returns>
		/// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="stream"/>, <paramref name="name"/> or <paramref name="entryPoint"/> parameters are NULL (Nothing in VB.Net).
		/// </exception>
		/// <exception cref="System.ArgumentException">Thrown when the name or entryPoint parameters are empty.</exception>
		/// <exception cref="System.ArgumentOutOfRangeException">Thrown when the <paramref name="size"/> parameter is less than or equal to 0.</exception>
		/// <exception cref="System.TypeInitializationException">Thrown when the type of shader is unrecognized.</exception>
		/// <exception cref="GorgonLibrary.GorgonException">Thrown when there are compile errors in the shader.</exception>
		public T FromStream<T>(string name, string entryPoint, Stream stream, int size)
			where T : GorgonShader
		{
#if DEBUG
			return FromStream<T>(name, entryPoint, stream, size, true);
#else
			return FromStream<T>(name, entryPoint, stream, size, false);
#endif
		}

		/// <summary>
		/// Function to load a shader from a stream of data.
		/// </summary>
		/// <typeparam name="T">The shader type.  Must be inherited from <see cref="GorgonLibrary.Graphics.GorgonShader">GorgonShader</see>.</typeparam>
		/// <param name="name">Name of the shader object.</param>
		/// <param name="entryPoint">Entry point method to call in the shader.</param>
		/// <param name="stream">Stream to load the shader from.</param>
		/// <param name="size">Size of the shader, in bytes.</param>
        /// <param name="isDebug">TRUE to apply debug information, FALSE to exclude it.</param>
        /// <returns>The new shader loaded from the data stream.</returns>
		/// <remarks>The <paramref name="isDebug"/> parameter is only applicable to source code shaders.</remarks>
		/// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="stream"/>, <paramref name="name"/> or <paramref name="entryPoint"/> parameters are NULL (Nothing in VB.Net).
		/// </exception>
		/// <exception cref="System.ArgumentException">Thrown when the name or entryPoint parameters are empty.</exception>
		/// <exception cref="System.ArgumentOutOfRangeException">Thrown when the <paramref name="size"/> parameter is less than or equal to 0.</exception>
		/// <exception cref="System.TypeInitializationException">Thrown when the type of shader is unrecognized.</exception>
		/// <exception cref="GorgonLibrary.GorgonException">Thrown when there are compile errors in the shader.</exception>
        public T FromStream<T>(string name, string entryPoint, Stream stream, int size, bool isDebug)
			where T : GorgonShader
		{
			GorgonShader shader;
			byte[] shaderData;

			if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (entryPoint == null)
            {
                throw new ArgumentNullException("entryPoint");
            }

            if (size < 1)
            {
                throw new ArgumentOutOfRangeException("size");
            }

            if (string.IsNullOrWhiteSpace("name"))
            {
                throw new ArgumentException(Resources.GORGFX_PARAMETER_MUST_NOT_BE_EMPTY, "name");
            }

            if (string.IsNullOrWhiteSpace("entryPoint"))
            {
				throw new ArgumentException(Resources.GORGFX_PARAMETER_MUST_NOT_BE_EMPTY, "entryPoint");
            }           
			
			long streamPosition = stream.Position;

			// Check for the binary header.  If we have it, load the file as a binary file.
			// Otherwise load it as source code.
			var header = new byte[Encoding.UTF8.GetBytes(BinaryShaderHeader).Length];
			bool isBinary = (string.Compare(Encoding.UTF8.GetString(header), stream.ReadString(), StringComparison.OrdinalIgnoreCase) == 0);
			if (isBinary)
			{
				shaderData = new byte[size - BinaryShaderHeader.Length];
			}
			else
			{
				stream.Position = streamPosition;
				shaderData = new byte[size];
			}

			stream.Read(shaderData, 0, shaderData.Length);

			if (isBinary)
			{
				shader = CreateShader<T>(name, entryPoint, string.Empty, isDebug);
				shader.D3DByteCode = new Shaders.ShaderBytecode(shaderData);
				shader.LoadShader();
			}
			else
			{
				string sourceCode = Encoding.UTF8.GetString(shaderData);
				shader = CreateShader<T>(name, entryPoint, sourceCode, isDebug);
			}

			return (T)shader;
		}

		/// <summary>
		/// Function to load a shader from a file.
		/// </summary>
		/// <typeparam name="T">The shader type.  Must be inherited from <see cref="GorgonLibrary.Graphics.GorgonShader">GorgonShader</see>.</typeparam>
		/// <param name="name">Name of the shader object.</param>
		/// <param name="entryPoint">Entry point method to call in the shader.</param>
		/// <param name="fileName">File name and path to the shader file.</param>
		/// <returns>The new shader loaded from the file.</returns>
		/// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="name"/>, <paramref name="entryPoint"/> or <paramref name="fileName"/> parameters are NULL (Nothing in VB.Net).
		/// </exception>
		/// <exception cref="System.ArgumentException">Thrown when the name, entryPoint or fileName parameters are empty.</exception>
		/// <exception cref="System.TypeInitializationException">Thrown when the type of shader is unrecognized.</exception>
		/// <exception cref="GorgonLibrary.GorgonException">Thrown when there are compile errors in the shader.</exception>
		public T FromFile<T>(string name, string entryPoint, string fileName)
			where T : GorgonShader
		{
#if DEBUG
			return FromFile<T>(name, entryPoint, fileName, true);
#else
			return FromFile<T>(name, entryPoint, fileName, false);
#endif
		}

		/// <summary>
		/// Function to load a shader from a file.
		/// </summary>
		/// <typeparam name="T">The shader type.  Must be inherited from <see cref="GorgonLibrary.Graphics.GorgonShader">GorgonShader</see>.</typeparam>
		/// <param name="name">Name of the shader object.</param>
		/// <param name="entryPoint">Entry point method to call in the shader.</param>
		/// <param name="fileName">File name and path to the shader file.</param>
        /// <param name="isDebug">TRUE to apply debug information, FALSE to exclude it.</param>
        /// <returns>The new shader loaded from the file.</returns>
		/// <remarks>The <paramref name="isDebug"/> parameter is only applicable to source code shaders.</remarks>
		/// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="name"/>, <paramref name="entryPoint"/> or <paramref name="fileName"/> parameters are NULL (Nothing in VB.Net).
		/// </exception>
		/// <exception cref="System.ArgumentException">Thrown when the name, entryPoint or fileName parameters are empty.</exception>
		/// <exception cref="System.TypeInitializationException">Thrown when the type of shader is unrecognized.</exception>
		/// <exception cref="GorgonLibrary.GorgonException">Thrown when there are compile errors in the shader.</exception>
        public T FromFile<T>(string name, string entryPoint, string fileName, bool isDebug)
			where T : GorgonShader
		{
			FileStream stream = null;

            if (fileName == null)
            {
                throw new ArgumentNullException("fileName");
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException(Resources.GORGFX_PARAMETER_MUST_NOT_BE_EMPTY, "fileName");
            }

			try
			{
				stream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                return FromStream<T>(name, entryPoint, stream, (int)stream.Length, isDebug);
			}
			finally
			{
				if (stream != null)
				{
					stream.Dispose();
				}
			}
		}

		/// <summary>
		/// Function to create a shader.
		/// </summary>
		/// <typeparam name="T">The shader type.  Must be inherited from <see cref="GorgonLibrary.Graphics.GorgonShader">GorgonShader</see>.</typeparam>
		/// <param name="name">Name of the shader.</param>
		/// <param name="entryPoint">Entry point for the shader.</param>
		/// <param name="sourceCode">Source code for the shader.</param>
		/// <param name="debug">TRUE to include debug information, FALSE to exclude.</param>
		/// <returns>A new vertex shader.</returns>
		/// <exception cref="System.ArgumentException">Thrown when the <paramref name="name"/> or <paramref name="entryPoint"/> parameters are empty strings.</exception>
		/// <exception cref="System.ArgumentNullException">Thrown when the name or entryPoint parameters are NULL (Nothing in VB.Net).</exception>
		/// <exception cref="System.TypeInitializationException">Thrown when the type of shader is unrecognized.</exception>
		/// <exception cref="GorgonLibrary.GorgonException">Thrown when there are compile errors in the shader.</exception>
		public T CreateShader<T>(string name, string entryPoint, string sourceCode, bool debug)
			where T : GorgonShader
		{
			if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(Resources.GORGFX_PARAMETER_MUST_NOT_BE_EMPTY, "name");
            }

			var shader = (T)Activator.CreateInstance(typeof(T), BindingFlags.Instance | BindingFlags.NonPublic, null, new object[]
				{
					_graphics, name, entryPoint
				}, null, null);

			if (shader == null)
			{
				throw new TypeInitializationException(typeof(T).FullName, null);
			}

			shader.IsDebug = debug;

			if (!string.IsNullOrEmpty(sourceCode))
			{
				shader.SourceCode = sourceCode;
				shader.Compile();
			}

			_graphics.AddTrackedObject(shader);

			return shader;
		}
		#endregion

		#region Constructor/Destructor.
		/// <summary>
		/// Initializes a new instance of the <see cref="GorgonShaderBinding"/> class.
		/// </summary>
		/// <param name="graphics">The graphics.</param>
		internal GorgonShaderBinding(GorgonGraphics graphics)
		{
			IncludeFiles = new GorgonShaderIncludeCollection();
			VertexShader = new GorgonVertexShaderState(graphics);
			PixelShader = new GorgonPixelShaderState(graphics);
			_graphics = graphics;
		}
		#endregion
	}
}
