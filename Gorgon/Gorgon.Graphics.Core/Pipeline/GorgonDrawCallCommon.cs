﻿#region MIT
// 
// Gorgon.
// Copyright (C) 2018 Michael Winsor
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
// Created: May 23, 2018 12:18:45 PM
// 
#endregion

using D3D = SharpDX.Direct3D;
using Gorgon.Collections;

namespace Gorgon.Graphics.Core
{
    /// <summary>
    /// Common values for a draw call.
    /// </summary>
    public abstract class GorgonDrawCallCommon
    {
        #region Properties.
        /// <summary>
        /// Property to return the internal D3D state.
        /// </summary>
        internal D3DState D3DState
        {
            get;
        } = new D3DState();

        /// <summary>
        /// Property to return the pipeline state for this draw call.
        /// </summary>
        public GorgonPipelineState PipelineState
        {
            get => D3DState.PipelineState;
            internal set => value.CopyTo(D3DState.PipelineState);
        }

        /// <summary>
        /// Property to return the vertex buffers bound to the draw call.
        /// </summary>
        public IGorgonReadOnlyArray<GorgonVertexBufferBinding> VertexBufferBindings => D3DState.VertexBuffers;

        /// <summary>
        /// Property to return the input layout for the draw call.
        /// </summary>
        /// <remarks>
        /// This is derived from the <see cref="VertexBufferBindings"/> passed to the call.
        /// </remarks>
        public GorgonInputLayout InputLayout => D3DState.InputLayout;

        /// <summary>
        /// Property to return the topology for a primitive.
        /// </summary>
        public PrimitiveType PrimitiveType
        {
            get => (PrimitiveType)D3DState.Topology;
            internal set => D3DState.Topology = (D3D.PrimitiveTopology)value;
        }
        #endregion

        #region Methods.
        /// <summary>
        /// Function to update a single vertex buffer binding.
        /// </summary>
        /// <param name="inputLayout">The input layout for the vertex buffers.</param>
        /// <param name="binding">The bindings to use.</param>
        /// <param name="slot">The slot to use.</param>
        internal void UpdateVertexBufferBinding(GorgonInputLayout inputLayout, in GorgonVertexBufferBinding binding, int slot)
        {
            if (D3DState.VertexBuffers[slot].Equals(in binding))
            {
                return;
            }

            if (D3DState.InputLayout != inputLayout)
            {
                D3DState.VertexBuffers.InputLayout = inputLayout;
            }

            D3DState.VertexBuffers.Clear();
            D3DState.VertexBuffers[slot] = binding;
        }

        /// <summary>
        /// Function to update the vertex buffer bindings.
        /// </summary>
        /// <param name="inputLayout">The input layout for the vertex buffers.</param>
        /// <param name="bindings">The bindings to use.</param>
        internal void UpdateVertexBufferBindings(GorgonInputLayout inputLayout, IGorgonReadOnlyArray<GorgonVertexBufferBinding> bindings)
        {
            (int start, int count) = bindings.GetDirtyItems();
            
            for (int i = start; i < start + count; ++i)
            {
                if (D3DState.VertexBuffers[i].Equals(bindings[i]))
                {
                    continue;
                }

                D3DState.VertexBuffers[i] = bindings[i];
            }

            if (D3DState.InputLayout == inputLayout)
            {
                return;
            }

            D3DState.VertexBuffers.InputLayout = inputLayout;
        }
        #endregion
    }
}
