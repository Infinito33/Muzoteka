using Muzoteka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Muzoteka.ViewModel
{
    public class WykonawcaViewModel
    {
        public wykonawca wykonawca { get; set; }
        public IEnumerable<SelectListItem> AllAlbums { get; set; }

        private List<int> _selectedAlbums;
        public List<int> SelectedAlbums
        {
            get
            {
                if (_selectedAlbums == null)
                {
                    _selectedAlbums = wykonawca.album.Select(m => m.id).ToList();
                }
                return _selectedAlbums;
            }
            set { _selectedAlbums = value; }
        }

        public IEnumerable<SelectListItem> AllUtwors { get; set; }
        private List<int> _selectedUtwors;
        public List<int> SelectedUtwors
        {
            get
            {
                if (_selectedUtwors == null)
                {
                    _selectedUtwors = wykonawca.utwor.Select(m => m.id).ToList();
                }
                return _selectedUtwors;
            }
            set { _selectedUtwors = value; }
        }
    }
}