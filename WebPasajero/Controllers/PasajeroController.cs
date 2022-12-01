using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WebPasajero.Data;
using WebPasajero.Models;

namespace WebPasajero.Controllers
{
    public class PasajeroController : Controller
    {
        private readonly PasajeroContext _context;

        public PasajeroController(PasajeroContext context) { _context = context; }
        public IActionResult Index()
        {
            return View(_context.Pasajeros.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            Pasajero pasajero = new Pasajero();
            return View("Create", pasajero);
        }

        [HttpPost]
        public IActionResult Create(Pasajero pasajero)
        {
            if (pasajero != null)
            {
                _context.Add(pasajero);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Create", pasajero);

        }

        [HttpGet]
        public IActionResult ListarPorFechaNacimiento(DateTime fechaNacimiento)
        {
            IEnumerable<Pasajero> listXFecha = BuscarXFecha(fechaNacimiento);
            if(listXFecha != null)
            {
                return View("ListXFecha", listXFecha);
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult ListarPorCiudad(string Ciudad)
        {
            IEnumerable<Pasajero> listXCiudad = BuscarXCiudad(Ciudad);
            if (listXCiudad != null)
            {
                return View("ListXCiudad", listXCiudad);
            }
            return NotFound();
        }

        #region metodos nonAction

        [NonAction]

        public IEnumerable<Pasajero> BuscarXFecha(DateTime FechaNacimiento)
        {
            IEnumerable<Pasajero> ListPasajeros = (from p in _context.Pasajeros
                                                   where DateTime.Compare(p.FechaNacimiento, FechaNacimiento)==0
                                                   select p).ToList();
            return ListPasajeros;
        }

        [NonAction]

        public IEnumerable<Pasajero> BuscarXCiudad(string Ciudad)
        {
            IEnumerable<Pasajero> ListPasajeros = (from p in _context.Pasajeros
                                                   where p.Ciudad.ToLower() == Ciudad.ToLower()
                                                   select p).ToList();
            return ListPasajeros;
        }
        #endregion
    }
}
