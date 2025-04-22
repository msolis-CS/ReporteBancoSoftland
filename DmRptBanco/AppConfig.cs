

using System.Configuration;

namespace DmRptBanco
{
    public class AppConfig
    {
        //Clases para guardar variables en un appConfig
        public static void establecer_valor(string llave, string valor)
        {
            //Crear objeto de configuracion
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //Borrar configuracion actual
            config.AppSettings.Settings.Remove(llave);
            //Guardar cambios
            config.Save(ConfigurationSaveMode.Modified);
            //Forzar la recarga de seccion
            ConfigurationManager.RefreshSection("appSettings");
            //Guardar la configuracion
            config.AppSettings.Settings.Add(llave, valor);
            //Guardar los cambios
            config.Save(ConfigurationSaveMode.Modified);
            //Forzar la recarga de la seccion
            ConfigurationManager.RefreshSection("appSettings");
        }

        public static string Recuperar(string llave, string val_guardado)
        {
            string retornar = ConfigurationManager.AppSettings[llave];
            if (retornar == null) { retornar = val_guardado; }
            return retornar;
        }
    }
}