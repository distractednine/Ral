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
    
    [Table("Watcheda")]
    public partial class Watcheda
    {
        [Key]
        public int id { get; set; }
        public string startdate { get; set; }
        public string finishdate { get; set; }
        public Nullable<double> rating { get; set; }
        public Nullable<int> watchedtime { get; set; }
        public string adddate { get; set; }
        public string status { get; set; }
        public Nullable<int> epdropped { get; set; }

        public int animeId { get; set; }
        public int usersId { get; set; }

        [ForeignKey("animeId")]
        public virtual Anime anime { get; set; }

        [ForeignKey("usersId")]
        public virtual User users { get; set; }
    }
}
