using Muzoteka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Muzoteka.ViewModel
{
    public class AlbumViewModel
    {
        public album album { get; set; }
        public IEnumerable<SelectListItem> AllWykonawcas { get; set; }
        private List<int> _selectedWykonawcas;
        public List<int> SelectedWykonawcas
        {
            get
            {
                if (_selectedWykonawcas == null)
                {
                    _selectedWykonawcas = album.wykonawca.Select(m => m.id).ToList();
                }
                return _selectedWykonawcas;
            }
            set { _selectedWykonawcas = value; }
        }

        public IEnumerable<SelectListItem> AllUtwors { get; set; }
        private List<int> _selectedUtwors;
        public List<int> SelectedUtwors
        {
            get
            {
                if (_selectedUtwors == null)
                {
                    _selectedUtwors = album.utwor.Select(m => m.id).ToList();
                }
                return _selectedUtwors;
            }
            set { _selectedUtwors = value; }
        }
    }
}