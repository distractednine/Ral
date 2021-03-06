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
    
    [Table("Plans")]
    public partial class Plan
    {
        [Key]
        public int id { get; set; }
        public Nullable<int> num { get; set; }

        public int animeId { get; set; }
        public int userId { get; set; }

        [ForeignKey("animeId")]
        public virtual Anime anime { get; set; }

        [ForeignKey("userId")]
        public virtual User users { get; set; }
    }
}
