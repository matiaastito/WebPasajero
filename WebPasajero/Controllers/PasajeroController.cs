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
            return View("Index", listXFecha);

        }

        [HttpGet]
        public IActionResult ListarPorCiudad(string Ciudad)
        {
            IEnumerable<Pasajero> listXCiudad = BuscarXCiudad(Ciudad);
            return View("Index", listXCiudad);
        }

        #region metodos nonAction

        [NonAction]

        public IEnumerable<Pasajero> BuscarXFecha(DateTime FechaNacimiento)
        {
            if (FechaNacimiento != null)
            {
                IEnumerable<Pasajero> ListPasajeros = (from p in _context.Pasajeros
                                                       where DateTime.Compare(p.FechaNacimiento, FechaNacimiento) == 0
                                                       select p).ToList();
                return ListPasajeros;
            }
            return Enumerable.Empty<Pasajero>();
        }

        [NonAction]

        public IEnumerable<Pasajero> BuscarXCiudad(string Ciudad)
        {
            if (Ciudad != null)
            {
                IEnumerable<Pasajero> ListPasajeros = (from p in _context.Pasajeros
                                                       where p.Ciudad.ToLower() == Ciudad.ToLower()
                                                       select p).ToList();
                return ListPasajeros;
            }
            else
                return Enumerable.Empty<Pasajero>();
        }
        #endregion
    }
}
