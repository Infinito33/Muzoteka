using Muzoteka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Muzoteka.ViewModel
{
    public class PlaylistaUtworViewModel
    {
        public List<playlista> playlistas { get; set; }
        public utwor utwor { get; set; }
        public playlista chosenPlaylista { get; set; }

        public playlista playlista { get; set; }
        public IEnumerable<SelectListItem> AllPlaylistas { get; set; }
        private List<int> _selectedPlaylistas;
        public List<int> SelectedPlaylistas
        {
            get
            {
                if (_selectedPlaylistas == null)
                {
                    using (muzotekaEntities db = new muzotekaEntities())
                    {
                        //_selectedPlaylistas = playlista.wykonawca.Select(m => m.id).ToList();
                        _selectedPlaylistas = db.playlista.Select(m => m.id).ToList();
                    }
                }
                return _selectedPlaylistas;
            }
            set { _selectedPlaylistas = value; }
        }

    }
}