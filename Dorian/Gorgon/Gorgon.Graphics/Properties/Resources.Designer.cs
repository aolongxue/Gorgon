﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18046
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GorgonLibrary.Graphics.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("GorgonLibrary.Graphics.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot bind the blending state.  Alpha to coverage is only available on SM 4.x or better video devices..
        /// </summary>
        internal static string GORGFX_ALPHA_TO_COVERAGE_NOT_AVAILABLE {
            get {
                return ResourceManager.GetString("GORGFX_ALPHA_TO_COVERAGE_NOT_AVAILABLE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The buffer is already locked..
        /// </summary>
        internal static string GORGFX_BUFFER_ALREADY_LOCKED {
            get {
                return ResourceManager.GetString("GORGFX_BUFFER_ALREADY_LOCKED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A generic dynamic buffer must be accessible via a shader view..
        /// </summary>
        internal static string GORGFX_BUFFER_DYNAMIC_NEEDS_SHADER_VIEW {
            get {
                return ResourceManager.GetString("GORGFX_BUFFER_DYNAMIC_NEEDS_SHADER_VIEW", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This buffer is immutable and this cannot be updated..
        /// </summary>
        internal static string GORGFX_BUFFER_IMMUTABLE {
            get {
                return ResourceManager.GetString("GORGFX_BUFFER_IMMUTABLE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot create an immutable buffer without initial data to populate it..
        /// </summary>
        internal static string GORGFX_BUFFER_IMMUTABLE_REQUIRES_DATA {
            get {
                return ResourceManager.GetString("GORGFX_BUFFER_IMMUTABLE_REQUIRES_DATA", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This buffer must be locked with the [Write] and [Discard] flags..
        /// </summary>
        internal static string GORGFX_BUFFER_LOCK_NOT_WRITE_DISCARD {
            get {
                return ResourceManager.GetString("GORGFX_BUFFER_LOCK_NOT_WRITE_DISCARD", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Buffers that allow unordered access views, render target views, or can be used for output cannot have a usage of [Dynamic]..
        /// </summary>
        internal static string GORGFX_BUFFER_NO_DYNAMIC_INVALID_FLAGS {
            get {
                return ResourceManager.GetString("GORGFX_BUFFER_NO_DYNAMIC_INVALID_FLAGS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {0} buffer type cannot output data..
        /// </summary>
        internal static string GORGFX_BUFFER_NO_OUTPUT_INVALID_TYPE {
            get {
                return ResourceManager.GetString("GORGFX_BUFFER_NO_OUTPUT_INVALID_TYPE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot lock this buffer with a lock type of [NoOverwrite].
        /// </summary>
        internal static string GORGFX_BUFFER_NO_OVERWRITE_NOT_VALID {
            get {
                return ResourceManager.GetString("GORGFX_BUFFER_NO_OVERWRITE_NOT_VALID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The buffer does not allow raw views..
        /// </summary>
        internal static string GORGFX_BUFFER_NO_RAW_VIEWS {
            get {
                return ResourceManager.GetString("GORGFX_BUFFER_NO_RAW_VIEWS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The buffer does not allow shader resource views..
        /// </summary>
        internal static string GORGFX_BUFFER_NO_SHADER_VIEWS {
            get {
                return ResourceManager.GetString("GORGFX_BUFFER_NO_SHADER_VIEWS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Buffers that allow indirect arguments, shader views, unordered access views, raw views, render target views or can be used for output cannot have a usage of [Staging]..
        /// </summary>
        internal static string GORGFX_BUFFER_NO_STAGING_INVALID_FLAGS {
            get {
                return ResourceManager.GetString("GORGFX_BUFFER_NO_STAGING_INVALID_FLAGS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The buffer does not allow unordered access views..
        /// </summary>
        internal static string GORGFX_BUFFER_NO_UNORDERED_VIEWS {
            get {
                return ResourceManager.GetString("GORGFX_BUFFER_NO_UNORDERED_VIEWS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot create the buffer.  The buffer size of {0} bytes is not a multiple of {1}..
        /// </summary>
        internal static string GORGFX_BUFFER_NOT_MULTIPLE {
            get {
                return ResourceManager.GetString("GORGFX_BUFFER_NOT_MULTIPLE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A buffer that allows stream output cannot have unordered access..
        /// </summary>
        internal static string GORGFX_BUFFER_OUTPUT_NO_UNORDERED {
            get {
                return ResourceManager.GetString("GORGFX_BUFFER_OUTPUT_NO_UNORDERED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A buffer that provides raw access must allow shader views and/or unordered resource views..
        /// </summary>
        internal static string GORGFX_BUFFER_RAW_ACCESS_REQUIRES_VIEW_ACCESS {
            get {
                return ResourceManager.GetString("GORGFX_BUFFER_RAW_ACCESS_REQUIRES_VIEW_ACCESS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The buffer must use [NoOverwrite] or [Discard] when locking..
        /// </summary>
        internal static string GORGFX_BUFFER_REQUIRES_NOOVERWRITE_DISCARD {
            get {
                return ResourceManager.GetString("GORGFX_BUFFER_REQUIRES_NOOVERWRITE_DISCARD", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The size of the buffers do not match..
        /// </summary>
        internal static string GORGFX_BUFFER_SIZE_MISMATCH {
            get {
                return ResourceManager.GetString("GORGFX_BUFFER_SIZE_MISMATCH", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The buffer requires at least {0} byte(s)..
        /// </summary>
        internal static string GORGFX_BUFFER_SIZE_TOO_SMALL {
            get {
                return ResourceManager.GetString("GORGFX_BUFFER_SIZE_TOO_SMALL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot create a staging buffer because the source buffer is immutable..
        /// </summary>
        internal static string GORGFX_BUFFER_STAGING_NO_IMMUTABLE {
            get {
                return ResourceManager.GetString("GORGFX_BUFFER_STAGING_NO_IMMUTABLE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The structure size must be greater than 0 and no greater than 2048 bytes..
        /// </summary>
        internal static string GORGFX_BUFFER_STRUCTURE_SIZE_INVALID {
            get {
                return ResourceManager.GetString("GORGFX_BUFFER_STRUCTURE_SIZE_INVALID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A buffer with a usage of [{0}] cannot be locked..
        /// </summary>
        internal static string GORGFX_BUFFER_USAGE_CANT_LOCK {
            get {
                return ResourceManager.GetString("GORGFX_BUFFER_USAGE_CANT_LOCK", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot read this buffer..
        /// </summary>
        internal static string GORGFX_BUFFER_WRITE_ONLY {
            get {
                return ResourceManager.GetString("GORGFX_BUFFER_WRITE_ONLY", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not retrieve character widths for the specified characters..
        /// </summary>
        internal static string GORGFX_CANNOT_RETRIEVE_ABC {
            get {
                return ResourceManager.GetString("GORGFX_CANNOT_RETRIEVE_ABC", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot retrieve kerning pairs..
        /// </summary>
        internal static string GORGFX_CANNOT_RETRIEVE_KERNING {
            get {
                return ResourceManager.GetString("GORGFX_CANNOT_RETRIEVE_KERNING", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Color Value: Red={0}, Green={1}, Blue={2}, Alpha={3}.
        /// </summary>
        internal static string GORGFX_COLOR_TOSTR {
            get {
                return ResourceManager.GetString("GORGFX_COLOR_TOSTR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The constant buffer &apos;{0}&apos; is already bound at slot [{1}]..
        /// </summary>
        internal static string GORGFX_CONSTANTBUFFER_ALREADY_BOUND {
            get {
                return ResourceManager.GetString("GORGFX_CONSTANTBUFFER_ALREADY_BOUND", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The parameter &apos;{0}&apos; was required for this effect, but is not present in the parameters list..
        /// </summary>
        internal static string GORGFX_EFFECT_MISSING_REQUIRED_PARAMS {
            get {
                return ResourceManager.GetString("GORGFX_EFFECT_MISSING_REQUIRED_PARAMS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Effect Parameter: {0}, Value = {1}.
        /// </summary>
        internal static string GORGFX_EFFECT_PARAM_TOSTR {
            get {
                return ResourceManager.GetString("GORGFX_EFFECT_PARAM_TOSTR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Must supply a known feature level..
        /// </summary>
        internal static string GORGFX_FEATURE_LEVEL_UNKNOWN {
            get {
                return ResourceManager.GetString("GORGFX_FEATURE_LEVEL_UNKNOWN", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Image pitch information.  Width={0} bytes, Size={1} bytes.  Format is compressed. Block count width: {2}, Block count height: {3}.
        /// </summary>
        internal static string GORGFX_FMTPITCH_COMPRESSED_TOSTR {
            get {
                return ResourceManager.GetString("GORGFX_FMTPITCH_COMPRESSED_TOSTR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Image pitch information.  Width={0} bytes, Size={1} bytes..
        /// </summary>
        internal static string GORGFX_FMTPITCH_TOSTR {
            get {
                return ResourceManager.GetString("GORGFX_FMTPITCH_TOSTR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The format [{0}] is not supported..
        /// </summary>
        internal static string GORGFX_FORMAT_NOT_SUPPORTED {
            get {
                return ResourceManager.GetString("GORGFX_FORMAT_NOT_SUPPORTED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The image type &apos;{0}&apos; is not a valid image type..
        /// </summary>
        internal static string GORGFX_IMAGE_TYPE_INVALID {
            get {
                return ResourceManager.GetString("GORGFX_IMAGE_TYPE_INVALID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The index is out of range.  The index value [{0}] must be be 0 or less than {1}..
        /// </summary>
        internal static string GORGFX_INDEX_OUT_OF_RANGE {
            get {
                return ResourceManager.GetString("GORGFX_INDEX_OUT_OF_RANGE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A valid device context was not found..
        /// </summary>
        internal static string GORGFX_INVALID_D3D_CONTEXT {
            get {
                return ResourceManager.GetString("GORGFX_INVALID_D3D_CONTEXT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The value [{0}] for parameter &apos;{1}&apos; is not valid..
        /// </summary>
        internal static string GORGFX_INVALID_ENUM_VALUE {
            get {
                return ResourceManager.GetString("GORGFX_INVALID_ENUM_VALUE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Gorgon Graphics interface requires Windows Vista Service Pack 2 or greater..
        /// </summary>
        internal static string GORGFX_INVALID_OS {
            get {
                return ResourceManager.GetString("GORGFX_INVALID_OS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The offset [{0}] or context &apos;{1}&apos; is in use by another item with the same index or slot..
        /// </summary>
        internal static string GORGFX_LAYOUT_ELEMENT_IN_USE {
            get {
                return ResourceManager.GetString("GORGFX_LAYOUT_ELEMENT_IN_USE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Multisampling count: {0}, quality: {1}..
        /// </summary>
        internal static string GORGFX_MULTISAMPLE_TOSTR {
            get {
                return ResourceManager.GetString("GORGFX_MULTISAMPLE_TOSTR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not find any supported video devices.  Gorgon requires a device that can support a minimum of pixel shader model 2b and a vertex shader model of 2a..
        /// </summary>
        internal static string GORGFX_NO_SUPPORTED_DEVICES {
            get {
                return ResourceManager.GetString("GORGFX_NO_SUPPORTED_DEVICES", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The usage for this object must be set to &apos;Default&apos;..
        /// </summary>
        internal static string GORGFX_NOT_DEFAULT_USAGE {
            get {
                return ResourceManager.GetString("GORGFX_NOT_DEFAULT_USAGE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The parameter must not be empty..
        /// </summary>
        internal static string GORGFX_PARAMETER_MUST_NOT_BE_EMPTY {
            get {
                return ResourceManager.GetString("GORGFX_PARAMETER_MUST_NOT_BE_EMPTY", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The render target &apos;{0}&apos; is not bound to a swap chain..
        /// </summary>
        internal static string GORGFX_RENDERTARGET_NOT_SWAPCHAIN {
            get {
                return ResourceManager.GetString("GORGFX_RENDERTARGET_NOT_SWAPCHAIN", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A {0} (or better) video device is necessary to use this object or perform this operation..
        /// </summary>
        internal static string GORGFX_REQUIRES_SM {
            get {
                return ResourceManager.GetString("GORGFX_REQUIRES_SM", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Gorgon Shader Include &apos;{0}&apos;.
        /// </summary>
        internal static string GORGFX_SHADER_INCLUDE_TOSTR {
            get {
                return ResourceManager.GetString("GORGFX_SHADER_INCLUDE_TOSTR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Gorgon Shader View.  Format: [{0}].
        /// </summary>
        internal static string GORGFX_SHADER_VIEW_TOSTR {
            get {
                return ResourceManager.GetString("GORGFX_SHADER_VIEW_TOSTR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The value is out of range.  The value [{0}] must be be 0 or less than {1}..
        /// </summary>
        internal static string GORGFX_VALUE_OUT_OF_RANGE {
            get {
                return ResourceManager.GetString("GORGFX_VALUE_OUT_OF_RANGE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The vertex buffer &apos;{0}&apos; is already bound at slot [{1}]..
        /// </summary>
        internal static string GORGFX_VERTEXBUFFER_ALREADY_BOUND {
            get {
                return ResourceManager.GetString("GORGFX_VERTEXBUFFER_ALREADY_BOUND", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Vertex buffer binding.  Stride: {0}, Offset: {1}, VertexBuffer: {2}.
        /// </summary>
        internal static string GORGFX_VERTEXBUFFER_BINDING_TOSTR {
            get {
                return ResourceManager.GetString("GORGFX_VERTEXBUFFER_BINDING_TOSTR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The view was already bound to the shader at index [{0}]..
        /// </summary>
        internal static string GORGFX_VIEW_ALREADY_BOUND {
            get {
                return ResourceManager.GetString("GORGFX_VIEW_ALREADY_BOUND", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The array count must be between 1 and {0}..
        /// </summary>
        internal static string GORGFX_VIEW_ARRAY_COUNT_INVALID {
            get {
                return ResourceManager.GetString("GORGFX_VIEW_ARRAY_COUNT_INVALID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The array start must be between 0 and less than {0}..
        /// </summary>
        internal static string GORGFX_VIEW_ARRAY_START_INVALID {
            get {
                return ResourceManager.GetString("GORGFX_VIEW_ARRAY_START_INVALID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot bind the view to the resource, the resource type is not known..
        /// </summary>
        internal static string GORGFX_VIEW_CANNOT_BIND_UNKNOWN_RESOURCE {
            get {
                return ResourceManager.GetString("GORGFX_VIEW_CANNOT_BIND_UNKNOWN_RESOURCE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The format [{0}] is not compatiable or cannot be cast to [{1}]..
        /// </summary>
        internal static string GORGFX_VIEW_CANNOT_CAST_FORMAT {
            get {
                return ResourceManager.GetString("GORGFX_VIEW_CANNOT_CAST_FORMAT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The resource view cannot be bound, it is either a staging resource or has no default shader view..
        /// </summary>
        internal static string GORGFX_VIEW_CANT_BIND_STAGING_NO_VIEW {
            get {
                return ResourceManager.GetString("GORGFX_VIEW_CANT_BIND_STAGING_NO_VIEW", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The array count for a cube texture must be a multiple of 6..
        /// </summary>
        internal static string GORGFX_VIEW_CUBE_ARRAY_SIZE_INVALID {
            get {
                return ResourceManager.GetString("GORGFX_VIEW_CUBE_ARRAY_SIZE_INVALID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The depth count must be between 1 and {0}..
        /// </summary>
        internal static string GORGFX_VIEW_DEPTH_COUNT_INVALID {
            get {
                return ResourceManager.GetString("GORGFX_VIEW_DEPTH_COUNT_INVALID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The depth start must be between 0 and less than {0}..
        /// </summary>
        internal static string GORGFX_VIEW_DEPTH_START_INVALID {
            get {
                return ResourceManager.GetString("GORGFX_VIEW_DEPTH_START_INVALID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The start element must be between 0 and {0}, and the element count must be between 1 and {1}..
        /// </summary>
        internal static string GORGFX_VIEW_ELEMENT_OUT_OF_RANGE {
            get {
                return ResourceManager.GetString("GORGFX_VIEW_ELEMENT_OUT_OF_RANGE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The resource format [{0}] is not the same group as the view format [{1}]..
        /// </summary>
        internal static string GORGFX_VIEW_FORMAT_GROUP_INVALID {
            get {
                return ResourceManager.GetString("GORGFX_VIEW_FORMAT_GROUP_INVALID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The mip map count must be between 1 and {0}..
        /// </summary>
        internal static string GORGFX_VIEW_MIP_COUNT_INVALID {
            get {
                return ResourceManager.GetString("GORGFX_VIEW_MIP_COUNT_INVALID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The mip map start must be between 0 and less than {0}..
        /// </summary>
        internal static string GORGFX_VIEW_MIP_START_INVALID {
            get {
                return ResourceManager.GetString("GORGFX_VIEW_MIP_START_INVALID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The view cannot use a typeless format..
        /// </summary>
        internal static string GORGFX_VIEW_NO_TYPELESS {
            get {
                return ResourceManager.GetString("GORGFX_VIEW_NO_TYPELESS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Raw shader views require a format of [R32_Uint], [R32_Int], or [R32_Float]..
        /// </summary>
        internal static string GORGFX_VIEW_RAW_INVALID_FORMAT {
            get {
                return ResourceManager.GetString("GORGFX_VIEW_RAW_INVALID_FORMAT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The view at index [{0}] is not bound to a resource of type &apos;{1}&apos;..
        /// </summary>
        internal static string GORGFX_VIEW_RESOURCE_NOT_TYPE {
            get {
                return ResourceManager.GetString("GORGFX_VIEW_RESOURCE_NOT_TYPE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Shader resource views cannot be bound to resources that have a usage of [Staging]..
        /// </summary>
        internal static string GORGFX_VIEW_SRV_NO_STAGING {
            get {
                return ResourceManager.GetString("GORGFX_VIEW_SRV_NO_STAGING", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unordered access views cannot be applied to texture cubes or multi-sampled textures..
        /// </summary>
        internal static string GORGFX_VIEW_UAV_NOT_SUPPORTED {
            get {
                return ResourceManager.GetString("GORGFX_VIEW_UAV_NOT_SUPPORTED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The format for the view must not be [Unknown]..
        /// </summary>
        internal static string GORGFX_VIEW_UNKNOWN_FORMAT {
            get {
                return ResourceManager.GetString("GORGFX_VIEW_UNKNOWN_FORMAT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot bind an unordered access view to a resource that has a usage of [Staging] or [Dynamic]..
        /// </summary>
        internal static string GORGFX_VIEW_UNORDERED_NO_STAGING_DYNAMIC {
            get {
                return ResourceManager.GetString("GORGFX_VIEW_UNORDERED_NO_STAGING_DYNAMIC", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Raw unordered views require a format of [R32] (Typeless)..
        /// </summary>
        internal static string GORGFX_VIEW_UNORDERED_RAW_INVALID_FORMAT {
            get {
                return ResourceManager.GetString("GORGFX_VIEW_UNORDERED_RAW_INVALID_FORMAT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap Gorgon_2_x_Logo_Small {
            get {
                object obj = ResourceManager.GetObject("Gorgon_2_x_Logo_Small", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
    }
}
