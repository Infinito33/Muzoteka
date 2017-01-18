namespace Muzoteka.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class utwor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public utwor()
        {
            this.ocena_utwor = new HashSet<ocena_utwor>();
        }

        public int id { get; set; }
        [Required]
        [Display(Name = "Nazwa")]
        public string nazwa { get; set; }
        [Required]
        [Display(Name = "Gatunek")]
        public string gatunek { get; set; }
        [Display(Name = "Długość [s]")]
        public Nullable<int> dlugosc { get; set; }
        [Display(Name = "Link")]
        [Required]
        public string link { get; set; }
        public System.DateTime dddb { get; set; }
        [Display(Name = "Album")]
        [Required]
        public int idalbum { get; set; }
        [Required]
        [Display(Name = "Wykonawca")]
        public int idwyk { get; set; }
        [Display(Name = "Playlista")]
        public Nullable<int> idplaylista { get; set; }

        [Display(Name = "Album")]
        public virtual album album { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [Display(Name = "Ocena")]
        public virtual ICollection<ocena_utwor> ocena_utwor { get; set; }
        [Display(Name = "Playlista")]
        public virtual playlista playlista { get; set; }
        [Display(Name = "Wykonawca")]
        public virtual wykonawca wykonawca { get; set; }
    }
}
