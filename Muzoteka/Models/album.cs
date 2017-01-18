namespace Muzoteka.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public partial class album
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public album()
        {
            this.utwor = new HashSet<utwor>();
            this.wykonawca = new HashSet<wykonawca>();
        }
    
        public int id { get; set; }
        [Display(Name = "Nazwa")]
        [Required]
        public string nazwa { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data wydania")]
        [Required]
        public System.DateTime data_wydania { get; set; }
        public System.DateTime dddb { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data wydania")]
        public string data_wydania_short
        {
            get
            {
                return data_wydania.ToShortDateString();
            }
        }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [Display(Name = "Utwory")]
        public virtual ICollection<utwor> utwor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [Display(Name = "Wykonawcy")]
        public virtual ICollection<wykonawca> wykonawca { get; set; }
    }
}
