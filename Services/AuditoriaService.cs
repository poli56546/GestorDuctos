using System;
using System.Collections.Generic;
using GestorDuctos.Models;

namespace GestorDuctos.Services
{
    public class AuditoriaService
    {
        public event Action? OnMovimientoAgregado;

        private readonly List<MovimientoDto> _movimientos = new()
        {
            new MovimientoDto { Descripcion = "Actualización de ingeniería básica de ducto", Categoria = "PROYECTO", Estado = "Completado", FechaTexto = "Hace 2 horas" },
            new MovimientoDto { Descripcion = "Ingreso de Plano As-Built Revisión B", Categoria = "PLANO", Estado = "En revisión", FechaTexto = "Hoy" },
            new MovimientoDto { Descripcion = "Oficio de liberación de materiales", Categoria = "OFICIO", Estado = "Registrado", FechaTexto = "Ayer" },
            new MovimientoDto { Descripcion = "Apertura de nuevo folio de integración", Categoria = "SISTEMA", Estado = "Iniciado", FechaTexto = "Hace 3 días" }
        };

        public IReadOnlyList<MovimientoDto> ObtenerUltimosMovimientos() => _movimientos;

        public void RegistrarMovimiento(string descripcion, string categoria, string estado, string fechaTexto)
        {
            _movimientos.Insert(0, new MovimientoDto
            {
                Descripcion = descripcion,
                Categoria = categoria,
                Estado = estado,
                FechaTexto = fechaTexto
            });

            // Notificar a los componentes subscritos en tiempo real
            OnMovimientoAgregado?.Invoke();
        }
    }
}