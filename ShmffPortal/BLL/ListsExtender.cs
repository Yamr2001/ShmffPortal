using ShmffPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShmffPortal.BLL
{
    public class ListsExtender
    {
        public IList<SelectListItem> GetCountries(Dictionary<string, string> dic)
        {
            // This comes from database.
            var _dbCountries = new List<DpList>();
            foreach (var item in dic)
            {
                DpList lst = new DpList();
                lst.Value = item.Key;
                lst.Name = item.Value;
                _dbCountries.Add(lst);
            }
            var countries = _dbCountries
                .Select(x => new SelectListItem { Text = x.Name, Value = x.Value.ToString() })
                .ToList();
            // countries.Insert(0, new SelectListItem { Text = "Choose a Country", Value = "" });
            return countries;
        }

        public SelectList GetCountriesEdit(Dictionary<string, string> dic, string Svalue)
        {
            // This comes from database.
            var _dbCountries = new List<DpList>();
            foreach (var item in dic)
            {
                DpList lst = new DpList();
                lst.Value = item.Key;
                lst.Name = item.Value;
                _dbCountries.Add(lst);
            }
            var countrieslst = _dbCountries.Select(x => new { Text = x.Name, Value = x.Value.ToString() }).ToList();
            //  .Select(x => new SelectListItem { Text = x.Name, Value = x.Value.ToString() })
            var countries = new SelectList(countrieslst, "Value", "Text", Svalue);
            // countries.Insert(0, new SelectListItem { Text = "Choose a Country", Value = "" });
            return countries;
        }
    }
}