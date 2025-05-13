using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_EntrerpriseLayer.Helpers
{
    public static class LoginHelper
    {

        public static string ObtenerNombreVisual(string nombres, string apellido)
        {
            var primerNombre = ObtenerPrimerNombre(nombres);
            var apellidoFormateado = CapitalizarTexto(apellido);

            return string.IsNullOrWhiteSpace(primerNombre) || string.IsNullOrWhiteSpace(apellidoFormateado)
                ? string.Empty
                : $"{primerNombre} {apellidoFormateado}";
        }
        public static string ObtenerPrimerNombre(string nombres)
        {
            if (string.IsNullOrWhiteSpace(nombres))
                return string.Empty;

            var partes = nombres
                .Trim()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            return partes.Length > 0 ? CapitalizarTexto(partes[0]) : string.Empty;
        }

        private static string CapitalizarTexto(string input)
        {
            return string.IsNullOrWhiteSpace(input)
                ? string.Empty
                : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.Trim().ToLowerInvariant());
        }
    }
}
