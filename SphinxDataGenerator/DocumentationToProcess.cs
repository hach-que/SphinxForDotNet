namespace SphinxDataGenerator
{
    /// <summary>
    /// Keeps track of an assembly and associate documentation file
    /// that needs to be processed.
    /// </summary>
    public class DocumentationToProcess
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SphinxDataGenerator.DocumentationToProcess"/> class.
        /// </summary>
        /// <param name="assembly">The full path to the assembly to process.</param>
        /// <param name="doc">The full path to the XML documentation to process.</param>
        /// <param name="supportsModules">If set to <c>true</c>, the assembly supports documentation modules.</param>
        public DocumentationToProcess(string assembly, string doc, bool supportsModules)
        {
            AssemblyFile = assembly;
            DocumentationFile = doc;
            SupportsModules = supportsModules;
        }

        /// <summary>
        /// Gets or sets the full path to the assembly.
        /// </summary>
        /// <value>The full path to the assembly.</value>
        public string AssemblyFile { get; set; }

        /// <summary>
        /// Gets or sets the full path to the XML documentation.
        /// </summary>
        /// <value>The full path to the XML documentation.</value>
        public string DocumentationFile { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether assembly
        /// supports documentation modules.
        /// </summary>
        /// <value><c>true</c> if the assembly supports documentation modules; otherwise, <c>false</c>.</value>
        public bool SupportsModules { get; set; }
    }
}

