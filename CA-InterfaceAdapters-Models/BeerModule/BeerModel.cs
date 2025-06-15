namespace CA_InterfaceAdapters_Models.BeerModule
{
    public class BeerModel
    {
        public int nIdBeer { get; set; }
        public string sNombre { get; set; }
        public string sEstilo { get; set; }
        public decimal dAlcohol { get; set; }
        public DateTime dtFechaCrecion { get; set; }
        public string sUsuarioCreacion { get; set; }
        public DateTime? dtFechaActualizacion { get; set; }
        public string sUsuarioActualizacion { get; set; }
    }
}
