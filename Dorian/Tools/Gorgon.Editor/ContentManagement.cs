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
// Created: Tuesday, September 24, 2013 10:37:50 PM
// 
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using GorgonLibrary.Editor.Properties;
using GorgonLibrary.IO;

namespace GorgonLibrary.Editor
{
    /// <summary>
    /// Interface for content management.
    /// </summary>
    static class ContentManagement
    {
        #region Variables.
        private readonly static Dictionary<GorgonFileExtension, ContentPlugIn> _contentFiles;
	    private static ContentObject _currentContentObject;
	    private static bool _contentChanged ;
        #endregion

		#region Properties.
		/// <summary>
		/// Property to return whether the current content has been changed or not.
		/// </summary>
	    public static bool Changed
	    {
		    get
		    {
			    return ((Current != null) && (_contentChanged));
			}
	    }

		/// <summary>
		/// Property to set or return the method to call if a dependency could not be loaded.
		/// </summary>
	    public static Action<string, IList<string>> DependencyNotFound
	    {
		    get;
		    set;
	    }

		/// <summary>
		/// Property to set or return the method to call after a content pane is unloaded.
		/// </summary>
	    public static Action ContentPanelUnloadAction
	    {
		    get;
		    set;
	    }

		/// <summary>
		/// Property to set or return the method to call after the content object is initialized.
		/// </summary>
	    public static Action<ContentPanel> ContentInitializedAction
		{
			get;
			set;
		}

		/// <summary>
		/// Property to set or return the method to call to enumerate the properties for a content pane.
		/// </summary>
		/// <remarks>The parameter of the method indicates whether properties are used by the content or not.</remarks>
	    public static Action<bool> ContentEnumerateProperties
	    {
		    get;
		    set;
	    }

		/// <summary>
		/// Property to set or return the method to call when a property on the content has changed.
		/// </summary>
		/// <remarks>This only takes effect if properties are available for public use on the content.</remarks>
	    public static Action<ContentPropertyChangedEventArgs> ContentPropertyChanged
	    {
		    get;
		    set;
	    }

		/// <summary>
		/// Property to set or return the method to call when the current content is persisted back to the file system.
		/// </summary>
	    public static Action ContentSaved
	    {
		    get;
		    set;
	    }

		/// <summary>
		/// Property to set or return the method to call when a dependency needs to be loaded for content..
		/// </summary>
	    public static Func<string, GorgonFileSystemFileEntry> OnGetDependency
	    {
		    get;
		    set;
	    }

		/// <summary>
		/// Property to return the currently active content.
		/// </summary>
	    public static ContentObject Current
	    {
		    get;
		    private set;
	    }
        #endregion

        #region Methods.
		/// <summary>
		/// Function called during idle time for rendering.
		/// </summary>
		/// <returns>TRUE to continue rendering, FALSE to stop.</returns>
	    private static bool IdleLoop()
	    {
		    if (_currentContentObject != null)
		    {
			    _currentContentObject.Draw();
		    }
		    return true;
	    }

		/// <summary>
		/// Event handler fired when any content property is changed.
		/// </summary>
		/// <param name="sender">Sender of the event.</param>
		/// <param name="e">Event parameters.</param>
		private static void OnContentChanged(object sender, ContentPropertyChangedEventArgs e)
		{
			if (Current == null)
			{
				return;
			}

			_contentChanged = true;
			if (ContentPropertyChanged != null)
			{
				ContentPropertyChanged(e);
			}
		}

		/// <summary>
		/// Function to update the content properties.
		/// </summary>
		/// <param name="propertyValue">The updated property and value information.</param>
	    public static void UpdateProperties(PropertyValueChangedEventArgs propertyValue)
	    {
			if ((Current == null)
			    || (!Current.HasProperties))
			{
				return;
			}

			Current.PropertyChanged(propertyValue);
			_contentChanged = true;
	    }

		/// <summary>
        /// Function to retrieve the list of available content extensions.
        /// </summary>
        /// <returns>A list of content file name extensions.</returns>
        public static IEnumerable<GorgonFileExtension> GetContentExtensions()
        {
            return _contentFiles.Keys;
        }

        /// <summary>
        /// Function to return a related plug-in for the given content file.
        /// </summary>
        /// <param name="fileExtension">Name of the content file.</param>
        /// <returns>The plug-in used to access the file.</returns>
        public static ContentPlugIn GetContentPlugInForFile(string fileExtension)
        {
            if (fileExtension.IndexOf('.') > 0)
            {
                fileExtension = Path.GetExtension(fileExtension);
            }

            return !CanOpenContent(fileExtension) ? null : _contentFiles[new GorgonFileExtension(fileExtension, null)];
        }

        /// <summary>
        /// Function to determine if a certain type of content can be opened by a plug-in.
        /// </summary>
        /// <param name="fileName">Filename of the content.</param>
        /// <returns>TRUE if the content can be opened, FALSE if not.</returns>
        public static bool CanOpenContent(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return false;
            }

            if (fileName.IndexOf('.') > 0)
            {
                fileName = Path.GetExtension(fileName);
            }

            return !string.IsNullOrWhiteSpace(fileName) && _contentFiles.ContainsKey(new GorgonFileExtension(fileName, null));
        }

		/// <summary>
		/// Function to unload any current content.
		/// </summary>
	    public static void UnloadCurrentContent()
	    {
			if (_currentContentObject == null)
			{
				return;
			}

			// Turn off the public facing content object.
			Current = null;

			// Turn off any idle time activity during the load.
			Gorgon.ApplicationIdleLoopMethod = null;

			if (ContentPanelUnloadAction != null)
			{
				ContentPanelUnloadAction();
			}

			// Close the content object.  This should preserve any changes.
			_currentContentObject.ContentPropertyChanged -= OnContentChanged;
			_currentContentObject.Dispose();
			_currentContentObject = null;
	    }

		/// <summary>
		/// Function to load a content object into the content pane in the interface.
		/// </summary>
		/// <param name="contentObject">Content object to load into the interface.</param>
	    public static void LoadContentPane(ContentObject contentObject)
	    {
			if (contentObject == null)
			{
				throw new ArgumentNullException("contentObject");
			}
			
			// Unload any content that's currently active.
			UnloadCurrentContent();

			// Initialize content resources.
			ContentPanel contentWindow = contentObject.InitializeContent();

			_currentContentObject = contentObject;

			// Do not count the default content pane as being "content".
			// This "Current" content being null will be our indicator that no content is currently active.
			if (!(contentObject is DefaultContent))
			{
				Current = contentObject;
			}

			if (contentWindow == null)
			{
				return;
			}

			if (ContentInitializedAction != null)
			{
				contentWindow.Dock = DockStyle.Fill;
				ContentInitializedAction(contentWindow);
			}

			if (ContentEnumerateProperties != null)
			{
				// Enumerate properties for the content.
				ContentEnumerateProperties(contentObject.HasProperties);
			}

			if (contentObject.HasProperties)
			{
				contentObject.ContentPropertyChanged += OnContentChanged;
			}

			// Force focus to the content window.
			if (contentWindow.Parent != null)
			{
				contentWindow.Focus();
			}

			if (contentObject.HasRenderer)
			{
				Gorgon.ApplicationIdleLoopMethod = IdleLoop;
			}
	    }
		
		/// <summary>
		/// Function load the default content pane into the interface.
		/// </summary>
	    public static void LoadDefaultContentPane()
	    {
			// We already have the default pane loaded.
		    if ((Current == null) && (_currentContentObject != null))
		    {
			    return;
		    }

		    LoadContentPane(new DefaultContent());
	    }

        /// <summary>
        /// Function to create a new content object.
        /// </summary>
        /// <param name="plugIn">The plug-in used to create the object.</param>
        /// <returns>The content object or NULL if the content object was not created.</returns>
        public static ContentObject Create(ContentPlugIn plugIn)
        {
            if (plugIn == null)
            {
                throw new ArgumentNullException("plugIn");
            }

            // Perform any set up required on the content.
            ContentSettings settings = plugIn.GetContentSettings();

            if (settings != null)
            {
                if (!settings.PerformSetup())
                {
                    return null;
                }

                settings.CreateContent = true;
            }

            ContentObject content = plugIn.CreateContentObject(settings);

            if (content.HasProperties)
            {
                content.SetDefaults();
            }

            return content;
        }

		/// <summary>
		/// Function to load content data from the file system.
		/// </summary>
		/// <param name="file">The file system file that contains the content data.</param>
	    public static void Load(GorgonFileSystemFileEntry file)
	    {
		    if (file == null)
		    {
		        throw new ArgumentNullException("file");
		    }

		    ContentPlugIn plugIn = GetContentPlugInForFile(file.Name);

            if (plugIn == null)
            {
                throw new IOException(string.Format(Resources.GOREDIT_NO_CONTENT_PLUG_IN_FOR_FILE, file.Name, file.Extension));
            }

		    ContentSettings settings = plugIn.GetContentSettings();        // Get default settings.

		    if (settings != null)
		    {
                // Assign the name from the file.
                settings.Name = file.Name;
		        settings.CreateContent = false;
		    }

            ContentObject content = plugIn.CreateContentObject(settings);

            Debug.Assert(_currentContentObject != null, "Content should not be NULL!");

			// Indicate that this content is linked to another piece of content.
			content.HasOwner = Program.EditorMetaData.HasFileLinks(file);

			// Load the content dependencies if any exist.
			EditorMetaDataFile metaData = Program.EditorMetaData;

			// Check for dependencies.
			if ((metaData.Dependencies.ContainsKey(file.FullPath))
				&& (metaData.Dependencies[file.FullPath].Count > 0))
			{
				var missingDependencies = new List<string>();

				foreach (Dependency dependencyFile in metaData.Dependencies[file.FullPath])
				{
					try
					{
						GorgonFileSystemFileEntry externalFile = OnGetDependency(dependencyFile.Path);

						if (externalFile == null)
						{
							missingDependencies.Add(string.Format(Resources.GOREDIT_CANNOT_FIND_DEPENDENCY_FILE, dependencyFile.Path));
							continue;
						}

						// Read the external content.
						using (Stream dependencyStream = externalFile.OpenStream(false))
						{
							content.LoadDependencyFile(dependencyFile, dependencyStream);
							content.Dependencies[dependencyFile.Path, dependencyFile.Type] = dependencyFile;
						}
					}
					catch (Exception ex)
					{
						missingDependencies.Add(string.Format(Resources.GOREDIT_CANNOT_LOAD_DEPENDENCY, dependencyFile.Path, ex.Message));
					}
				}

				if ((missingDependencies.Count > 0)
					&& (DependencyNotFound != null))
				{
					DependencyNotFound(file.Name, missingDependencies);
				}
			}

            LoadContentPane(content);

            // Load in the content data.
		    using(Stream stream = file.OpenStream(false))
		    {
		        content.Read(stream);
		    }

		    _contentChanged = false;
	    }

		/// <summary>
		/// Function to save the current content.
		/// </summary>
		/// <param name="file">The file that will contain the content data.</param>
	    public static void Save(GorgonFileSystemFileEntry file)
	    {
		    if (Current == null)
		    {
			    return;
		    }

		    if (file == null)
		    {
		        throw new ArgumentNullException("file");
		    }

			// Write the content out to the scratch file system.
			using (var contentStream = file.OpenStream(true))
			{
				Current.Persist(contentStream);
			}

			if ((Current.Dependencies.Count == 0) 
				&& (Program.EditorMetaData.Dependencies.ContainsKey(file.FullPath)))
			{
				Program.EditorMetaData.Dependencies.Remove(file.FullPath);
				Program.EditorMetaData.Save();
			}
			else
			{
				if (Current.Dependencies.Count > 0)
				{
					Program.EditorMetaData.Dependencies[file.FullPath] = Current.Dependencies;
					Program.EditorMetaData.Save();
				}
			}

			_contentChanged = false;

			// Indicate that we've saved the content.
			if (ContentSaved != null)
			{
				ContentSaved();
			}
	    }

	    /// <summary>
        /// Function used to initialize the file types for the content types.
        /// </summary>
        public static void InitializeContentFileTypes()
        {
            // Get content extensions.
            foreach (var contentPlugIn in PlugIns.ContentPlugIns)
            {
                if (contentPlugIn.Value.FileExtensions.Count == 0)
                {
                    continue;
                }

                // Associate the content file type with the plug-in.
                foreach (var extension in contentPlugIn.Value.FileExtensions)
                {
                    _contentFiles[extension] = contentPlugIn.Value;
                }
            }
        }
        #endregion

        #region Constructor/Destructor.
        /// <summary>
        /// Initializes the <see cref="ContentManagement"/> class.
        /// </summary>
        static ContentManagement()
        {
            _contentFiles = new Dictionary<GorgonFileExtension, ContentPlugIn>();
        }
        #endregion
    }
}
