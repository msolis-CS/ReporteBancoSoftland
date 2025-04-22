using DevExpress.XtraWaitForm;
using DmRptBanco;
using Softland.Tools.ConexionBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConfigInicial
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                string Usuario;
                string Password;
                string Compania;
                string BaseDatos;
                string Servidor;

                if (args.Length < 5)
                {
                    var result = InicioSesion.ins.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        try
                        {
                            Application.Run(new MainForm());
                        }
                        catch (Exception err)
                        {
                            MessageBox.Show("Error inesperado en la ejecucion: " + err.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    return;
                }

                Usuario = args[0];
                Password = args[1];
                Compania = args[2];
                BaseDatos = args[3];
                Servidor = args[4];


                StaticData.oCnn = new Conexion(TipoBDEnum.SqlServer, Servidor, BaseDatos, Compania, Usuario, Password);
                StaticData.oCnn.Open();
                if (StaticData.oCnn.IsOpen)
                {
                    StaticData.UsuarioSinX = Usuario.ToUpper();
                    string userx, passx;
                    StaticData.oCnn.Close();
                    if (Usuario.ToUpper() != "SA" && Usuario.ToUpper() != "SYSTEM" && Usuario.ToUpper() != "XERPADMIN")
                    {
                        StaticData.GetUserX(Usuario, out userx, out passx);
                    }
                    else
                    {
                        userx = Usuario;
                        passx = Password;
                    }
                    StaticData.oCnn = new Conexion(TipoBDEnum.SqlServer, Servidor, BaseDatos, Compania, userx, passx);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR CONECTANDO: " + e.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            catch (Exception err)
            {
                MessageBox.Show("Error inesperado en la ejecucion: " + err.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }
    }
}
