namespace Muzoteka.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class playlista
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public playlista()
        {
            this.utwor = new HashSet<utwor>();
            this.utwor_w_playlista = new HashSet<utwor_w_playlista>();
        }
    
        public int id { get; set; }
        [Display(Name = "Nazwa")]
        [Required]
        public string nazwa { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data utworzenia")]
        public System.DateTime data_utworzenia { get; set; }
        [Display(Name = "Data utworzenia")]
        public string data_utworzenia_short
        {
            get
            {
                return data_utworzenia.ToShortDateString();
            }
        }
        [Display(Name = "Opis")]
        public string opis { get; set; }
        public int iduzytkownik { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [Display(Name = "Utwory")]
        public virtual ICollection<utwor> utwor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<utwor_w_playlista> utwor_w_playlista { get; set; }
        [Display(Name = "Użytkownik")]
        public virtual uzytkownik uzytkownik { get; set; }
    }
}
