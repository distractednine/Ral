//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RAL_DAL
{
    using System;
    using System.Collections.Generic;
    
    [Table("Anime")]
    public partial class Anime
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string year { get; set; }
        public string type { get; set; }
        public Nullable<int> seasonnum { get; set; }
        public Nullable<int> epcount { get; set; }
        public Nullable<int> waid { get; set; }
    }
}