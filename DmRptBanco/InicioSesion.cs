    using Softland.Tools.ConexionBD;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

namespace DmRptBanco
{
    public partial class InicioSesion : DevExpress.XtraEditors.DirectXForm
    {
        int valor = 0;
        string Usuario;
        string Password;
        string Compania;
        string BaseDatos;
        string Servidor;

        public InicioSesion()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen; 
        }
        public bool ValidarShema()
        {
            bool check = false;

            string compania = AppConfig.Recuperar("Compania", "");

            foreach (DataRow row in StaticData.oCnn.ExecuteDatatable(string.Format("SELECT TOP 1 SCHEMA_NAME(schema_id) AS schema_name FROM sys.objects" +
                                                                   " WHERE SCHEMA_NAME(schema_id) ='{0}' group by SCHEMA_NAME(schema_id)", compania)).Rows)
            {
                if (compania.Equals(row["schema_name"]))
                    check = true;
            }
            return check;
        }

        public void conectar()
        {
            try
            {
                if (checkBox1.Checked == true)
                {
                    AppConfig.establecer_valor("Recordar", "S");
                    AppConfig.establecer_valor("User", TUser.Text);
                    AppConfig.establecer_valor("Pass", TPass.Text);
                }
                else
            if (checkBox1.Checked == false)
                {
                    AppConfig.establecer_valor("Recordar", "N");
                }

                Usuario = TUser.Text; Password = TPass.Text;
                Compania = comboBox1.SelectedValue.ToString();
                BaseDatos = comboBox2.SelectedValue.ToString();
                Servidor = AppConfig.Recuperar("SERVER", "");

                StaticData.oCnn = new Conexion(TipoBDEnum.SqlServer, Servidor, BaseDatos, Compania, Usuario, Password);
                StaticData.oCnn.Open();
                if (StaticData.oCnn.IsOpen)
                {
                    StaticData.UsuarioSinX = Usuario.ToUpper();
                    string userx, passx;
                    StaticData.oCnn.Close();
                    if (Usuario.ToUpper() != "SA" && Usuario.ToUpper() != "SYSTEM" && Usuario.ToUpper() != "XERPADMIN")
                        StaticData.GetUserX(Usuario, out userx, out passx);
                    else
                    {
                        userx = Usuario;
                        passx = Password;
                    }
                    StaticData.oCnn = new Conexion(TipoBDEnum.SqlServer, Servidor, BaseDatos, Compania, userx, passx);
                    DialogResult = DialogResult.OK;
                }

                //if (valor >= 3)
                //{
                //    MessageBox.Show("El programa se ha bloqueado por que ha superado el numero de intentos disponible, Contacte con el Administrador");
                //    AppConfig.establecer_valor("Bloqueado", "S"); this.Close();
                //}
            }
            catch (Exception e)
            {
                MessageBox.Show("Error Inesperado " + e.ToString(), Compania, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        public string[] separar(string cadena)
        {
            string str = cadena;
            string[] arr = str.Split(new string[] { "," }, StringSplitOptions.None);

            return arr;
        }

        public void CargarCombo(string cadena, System.Windows.Forms.ComboBox cbx)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("COLUMNA");
            foreach (string a in separar(cadena))
            {
                DataRow row = dt.NewRow();

                row["COLUMNA"] = a;

                dt.Rows.Add(row);
            }

            cbx.DataSource = dt;
            cbx.ValueMember = "COLUMNA";
            cbx.DisplayMember = "COLUMNA";
        }

        //Evento para poder capturar la combinacion de teclas (Ctrl + L)
        private void frmBase_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Control && e.KeyCode == Keys.L)
            //{
            //    //Mandamos a abrir el formulario o lo mandamos a mostrar al frente de estar abierto ya
            //    Configuracion.DefInstance.Show();
            //    Configuracion.DefInstance.BringToFront();
            //}

            //if (e.Control && e.KeyCode == Keys.D0)
            //{
            //    datos();
            //}
        }

        public void datos()
        {
            String Bloqueado = AppConfig.Recuperar("Bloqueado", "");

            if (Bloqueado.Equals("S") || Bloqueado.Equals("s"))
            {
                TUser.Enabled = false;
                TPass.Enabled = false;
                Ingresar.Enabled = false;
            }
            else
                if (Bloqueado.Equals("N") || Bloqueado.Equals("n"))
            {
                TUser.Enabled = true;
                TPass.Enabled = true;
                Ingresar.Enabled = true;
            }
        }

        private static InicioSesion form;
        public static InicioSesion ins
        {
            get { if (form == null || form.IsDisposed) form = new InicioSesion(); return form; }
            set { form = value; }
        }

        private void InicioSesion_Load(object sender, EventArgs e)
        {

                labelControl6.Text = AppConfig.Recuperar("TITULO", "");
                CargarCombo(AppConfig.Recuperar("COMPANIA", ""), comboBox1);
                CargarCombo(AppConfig.Recuperar("BD", ""), comboBox2);

                labelControl1.Text = labelControl1.Text + " " + Convert.ToString(DateTime.Now.ToString("yyyy"));

                //Desde la carga del formulario mandamos a esperar el evento
                this.KeyDown += new KeyEventHandler(this.frmBase_KeyDown);
                datos();
                if (AppConfig.Recuperar("Recordar", "").Equals("S"))
                {
                    TPass.Text = AppConfig.Recuperar("Pass", "");
                    TUser.Text = AppConfig.Recuperar("User", "");
                    checkBox1.Checked = true;
                }
            

        }

        private void Ingresar_Click_1(object sender, EventArgs e)
        {
            conectar();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            { conectar(); }
        }

        private void TPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            { conectar(); }
        }
    }
}
