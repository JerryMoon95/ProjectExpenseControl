using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ProjectExpenseControl.Services
{
    public class FilesRepository
    {
        public void CrearDirectorio(string solicitud, string rutaServidor)
        {
            string elDirectorio = rutaServidor + @"\\Xml\\" + solicitud.Replace("/", "");
            CrearDirectorios(elDirectorio, solicitud);
        }

        public List<string> ArchivosDirectorio(string solicitud, string rutaServidor)
        {
            string elDirectorio = rutaServidor + @"Xml\" + solicitud.Replace("/", "");
            return ArchivosDirectorio(solicitud, rutaServidor, elDirectorio);
        }

        #region Achivos y Directorios

        public void CrearDirectorios(string elDirectorio, string directorio)
        {
            bool existe = Directory.Exists(elDirectorio);
            if (!existe)
            {
                Directory.CreateDirectory(elDirectorio);
            }
        }

        //string[] ficheros;
        public List<string> ArchivosDirectorio(string oficio, string rutaServidor, string elDirectorio)
        {
            List<string> ficheros;
            string ruta = elDirectorio;
            ficheros = Directory.GetFiles(ruta, "*.xml").ToList();
            return ficheros;
        }
        #endregion
    }
}