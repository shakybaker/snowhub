﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sporthub.Repository.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=213.175.193.168;Initial Catalog=thesnowhub;Persist Security Info=True" +
            ";User ID=hasselhof;Password=anewstart2008")]
        public string sandpitConnectionString {
            get {
                return ((string)(this["sandpitConnectionString"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=tcp:esql2k501.discountasp.net;Initial Catalog=SQL2005_615410_sporthub" +
            ";Persist Security Info=True;User ID=SQL2005_615410_sporthub_user;Password=first2" +
            "009")]
        public string SQL2005_615410_sporthubConnectionString {
            get {
                return ((string)(this["SQL2005_615410_sporthubConnectionString"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=localhost;Initial Catalog=SQL2005_615410_sporthub;Integrated Security" +
            "=True")]
        public string SQL2005_615410_sporthubConnectionString1 {
            get {
                return ((string)(this["SQL2005_615410_sporthubConnectionString1"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=esql2k501.discountasp.net;Initial Catalog=SQL2005_615410_sporthub;Per" +
            "sist Security Info=True;User ID=SQL2005_615410_sporthub_user;Password=first2010")]
        public string SQL2005_615410_sporthubConnectionString2 {
            get {
                return ((string)(this["SQL2005_615410_sporthubConnectionString2"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=NH-LAPTOP-120;Initial Catalog=sporthub;Persist Security Info=True;Use" +
            "r ID=sporthubuser;Password=first2010")]
        public string sporthubConnectionString {
            get {
                return ((string)(this["sporthubConnectionString"]));
            }
        }
    }
}
