﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WifiShare.Properties {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WifiShare.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to &apos; VBScript source code
        ///OPTION EXPLICIT
        ///DIM ICSSC_DEFAULT, CONNECTION_PUBLIC, CONNECTION_PRIVATE, CONNECTION_ALL
        ///DIM NetSharingManager
        ///DIM PublicConnection, PrivateConnection
        ///DIM EveryConnectionCollection
        ///
        ///DIM objArgs
        ///DIM priv_con, publ_con
        ///dim switch
        ///
        ///ICSSC_DEFAULT         = 0
        ///CONNECTION_PUBLIC     = 0
        ///CONNECTION_PRIVATE    = 1
        ///CONNECTION_ALL        = 2
        ///
        ///Main()
        ///
        ///sub Main( )
        ///    Set objArgs = WScript.Arguments
        ///
        ///    if objArgs.Count = 3 then
        ///        priv_con = objArgs(0)&apos;内网连接名,供别人连接的网卡名字 [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string vbsOpenShare {
            get {
                return ResourceManager.GetString("vbsOpenShare", resourceCulture);
            }
        }
    }
}
