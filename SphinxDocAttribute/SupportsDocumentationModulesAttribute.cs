using System;

namespace SphinxDocAttribute
{
    /// <summary>
    /// The existance of this attribute on the assembly indicates that this
    /// assembly supports documentation modules (that is, the documentation
    /// for classes are expected to have the &lt;module&gt; tag).
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class SupportsDocumentationModulesAttribute : Attribute
    {
    }
}

