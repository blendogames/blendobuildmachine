﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace blendobuildmachine.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://yoursubversion12345.com/repositoryaddress")]
        public string repositoryUrl {
            get {
                return ((string)(this["repositoryUrl"]));
            }
            set {
                this["repositoryUrl"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("c:\\Program Files\\TortoiseSVN\\bin\\svn.exe")]
        public string svnExecutable {
            get {
                return ((string)(this["svnExecutable"]));
            }
            set {
                this["svnExecutable"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("c:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\msbuild.exe")]
        public string compilerExecutable {
            get {
                return ((string)(this["compilerExecutable"]));
            }
            set {
                this["compilerExecutable"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("c:\\games\\gamename")]
        public string localFolderpath {
            get {
                return ((string)(this["localFolderpath"]));
            }
            set {
                this["localFolderpath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("c:\\games\\gamename\\solutionfile.sln")]
        public string solutionfile {
            get {
                return ((string)(this["solutionfile"]));
            }
            set {
                this["solutionfile"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("VCTargetsPath c:\\Program Files (x86)\\MSBuild\\Microsoft.Cpp\\v4.0\\v140")]
        public string environmentVars {
            get {
                return ((string)(this["environmentVars"]));
            }
            set {
                this["environmentVars"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool openexefolder {
            get {
                return ((bool)(this["openexefolder"]));
            }
            set {
                this["openexefolder"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool runexewhendone {
            get {
                return ((bool)(this["runexewhendone"]));
            }
            set {
                this["runexewhendone"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool runonstart {
            get {
                return ((bool)(this["runonstart"]));
            }
            set {
                this["runonstart"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool verbose {
            get {
                return ((bool)(this["verbose"]));
            }
            set {
                this["verbose"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string lastexefolder {
            get {
                return ((string)(this["lastexefolder"]));
            }
            set {
                this["lastexefolder"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool playbeep {
            get {
                return ((bool)(this["playbeep"]));
            }
            set {
                this["playbeep"] = value;
            }
        }
    }
}
