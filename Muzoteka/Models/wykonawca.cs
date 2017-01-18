namespace Muzoteka.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class wykonawca
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public wykonawca()
        {
            this.utwor = new HashSet<utwor>();
            this.album = new HashSet<album>();
        }
    
        public int id { get; set; }
        [Required]
        [Display(Name = "Imię")]
        public string imie { get; set; }
        [Required]
        [Display(Name = "Nazwisko")]
        public string nazwisko { get; set; }
        [Required]
        [Display(Name = "Pseudonim")]
        public string pseudonim { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data urodzenia")]
        public Nullable<System.DateTime> data_ur { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data urodzenia")]
        public string data_ur_short
        {
            get
            {
                if(data_ur.HasValue)
                {
                    return data_ur.Value.ToShortDateString();
                } else
                {
                    return "";
                }
            }
        }
        public System.DateTime dddb { get; set; }
        [Display(Name = "Opis")]
        public string opis { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [Display(Name = "Utwory")]
        public virtual ICollection<utwor> utwor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [Display(Name = "Albumy")]
        public virtual ICollection<album> album { get; set; }
    }
}
