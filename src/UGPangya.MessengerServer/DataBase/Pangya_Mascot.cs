//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UGPangya.MessengerServer.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pangya_Mascot
    {
        public int MID { get; set; }
        public int UID { get; set; }
        public int MASCOT_TYPEID { get; set; }
        public string MESSAGE { get; set; }
        public Nullable<System.DateTime> DateEnd { get; set; }
        public Nullable<byte> VALID { get; set; }
    }
}