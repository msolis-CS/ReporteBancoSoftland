using DevExpress.XtraEditors;
using Softland.Tools.ConexionBD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static DevExpress.XtraEditors.RoundedSkinPanel;


namespace DmRptBanco
{
    public partial class MainForm : DevExpress.XtraEditors.DirectXForm
    {
        static string compania = StaticData.oCnn.CompaniaActual;
        static string usuario_actual = StaticData.oCnn.UsuarioActual;

        public MainForm()
        {
            InitializeComponent();
            CargarComboNomina();
            CargarComboEntidadFinanciera();
        }

        private void CargarComboNomina()
        {
            DataTable dt;

            try
            {
                string sql = $@"SELECT Nomina, Descripcion FROM {compania}.NOMINA";


                dt = StaticData.oCnn.ExecuteDatatable(sql);

                cmbNomina.Properties.DataSource = dt;
                cmbNomina.Properties.ValueMember = "Nomina";
                cmbNomina.Properties.DisplayMember = "Descripcion";
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error al cargar las nominas: {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void CargarComboEntidadFinanciera()
        {
            DataTable dt;

            try
            {
                string sql = $@"SELECT ENTIDAD_FINANCIERA Entidad, Descripcion FROM {compania}.ENTIDAD_FINANCIERA WHERE ENTIDAD_FINANCIERA <> 'ND'";


                dt = StaticData.oCnn.ExecuteDatatable(sql);

                cmbEntidadFinanciera.Properties.DataSource = dt;
                cmbEntidadFinanciera.Properties.ValueMember = "Entidad";
                cmbEntidadFinanciera.Properties.DisplayMember = "Descripcion";
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error al cargar las entidades financieras: {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void CargarComboNumeroNomina(string nomina)
        {
            DataTable dt;

            try
            {
                string sql = $@"SELECT NUMERO_NOMINA as Numero FROM {compania}.NOMINA_HISTORICO WHERE NOMINA = '{nomina}' ORDER BY NUMERO_NOMINA DESC";

                dt = StaticData.oCnn.ExecuteDatatable(sql);

                cmbNumeroNomina.Properties.DataSource = dt;
                cmbNumeroNomina.Properties.ValueMember = "Numero";
                cmbNumeroNomina.Properties.DisplayMember = "Numero";
                //cmbNumeroNomina.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;



            }
            catch (Exception e)
            {
                MessageBox.Show($"Error al cargar los numeros de nomina: {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void cmbNomina_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbNomina.EditValue == null)
            {
                return;
            }
            cmbNumeroNomina.EditValue = null;
            string nomina = cmbNomina.EditValue.ToString();
            CargarComboNumeroNomina(nomina);

        }

        private string getMonthNameFromNumber(int monthNumber)
        {
            switch (monthNumber)
            {
                case 1:
                    return "ENE";
                case 2:
                    return "FEB";
                case 3:
                    return "MAR";
                case 4:
                    return "ABR";
                case 5:
                    return "MAY";
                case 6:
                    return "JUN";
                case 7:
                    return "JUL";
                case 8:
                    return "AGO";
                case 9:
                    return "SEP";
                case 10:
                    return "OCT";
                case 11:
                    return "NOV";
                case 12:
                    return "DIC";
                default:
                    throw new Exception("Error al definir el mes de la nómina.");
            }
        }

        public string GenerarArchivo(string nomina, string numero_nomina, string entidad, out string mensajeError)
        {

            mensajeError = string.Empty;

            if (string.IsNullOrEmpty(nomina) || string.IsNullOrEmpty(numero_nomina))
            {
                mensajeError = "Debe seleccionar una nómina válida y un número de nómina.";
                return null;
            }

            if (string.IsNullOrEmpty(entidad))
            {
                mensajeError = "Debe seleccionar una entidad financiera.";
                return null;
            }

            try
            {

                var nominaHistDtScript = $@"
                    SELECT FECHA_PAGO 
                    FROM {compania}.NOMINA_HISTORICO 
                    WHERE NOMINA = '{nomina}' 
                    AND NUMERO_NOMINA = {numero_nomina}";

                var nominaHistDt = StaticData.oCnn.ExecuteDatatable(nominaHistDtScript);
                if (nominaHistDt.Rows.Count == 0)
                {
                    mensajeError = "No se encontró información de nómina.";
                    return null;
                }

                DateTime fechaPago = DateTime.Parse(nominaHistDt.Rows[0][0].ToString() ?? "");

                var entidadFinanciera = entidad;

                var queryNomiNetos = $@"
                    SELECT E.CUENTA_ENTIDAD, CONVERT(DECIMAL(18,2),NN.NETO) NETO, E.E_MAIL
                    FROM {compania}.EMPLEADO_NOMI_NETO NN
                    INNER JOIN {compania}.EMPLEADO E ON E.EMPLEADO = NN.EMPLEADO
                    WHERE NN.NUMERO_NOMINA = {numero_nomina}
                    AND NN.NOMINA = '{nomina}'
                    AND E.FORMA_PAGO = 'T'
                    AND E.ENTIDAD_FINANCIERA = '{entidadFinanciera}'";

                var nomiNetosDt = StaticData.oCnn.ExecuteDatatable(queryNomiNetos);
                var totalRegistros = nomiNetosDt.Rows.Count;
                decimal totalMonto = 0;

                foreach (DataRow row in nomiNetosDt.Rows)
                {
                    totalMonto += Convert.ToDecimal(row["NETO"]);
                }

                var archivoCSV = new StringBuilder();
                string mesNomina = getMonthNameFromNumber(fechaPago.Month);
                string nombreNomina = fechaPago.Day <= 15
                    ? $"COMPLEMENTO NOMINA 1Q {mesNomina}-{fechaPago.Year}"
                    : $"COMPLEMENTO NOMINA 2Q {mesNomina}-{fechaPago.Year}";

                archivoCSV.AppendLine($"B,{totalRegistros},{fechaPago.Year},{fechaPago.Month},{fechaPago.Day},{totalMonto},N,S");

                for (int i = 0; i < nomiNetosDt.Rows.Count; i++)
                {
                    var cuentaEntidad = nomiNetosDt.Rows[i]["CUENTA_ENTIDAD"].ToString();
                    var neto = Convert.ToDecimal(nomiNetosDt.Rows[i]["NETO"]);
                    var correo = nomiNetosDt.Rows[i]["E_MAIL"].ToString();
                    //archivoCSV.AppendLine($"T,{i + 1},{cuentaEntidad},{neto},{nombreNomina},,30,1,,,{nombreNomina}");
                    archivoCSV.AppendLine($"T,{i + 1},{cuentaEntidad},{neto},{nombreNomina},{correo},30,1");
                }

                // Insertar en histórico
                var insertHistScript = $@"
                    INSERT INTO {compania}.CS_HISTORICO_GEN_ARCHIVO 
                    VALUES ('{Guid.NewGuid()}', '{usuario_actual}', '{nomina}', '{Convert.ToInt16(numero_nomina)}', 
                    '{fechaPago:yyyyMMdd}', '{DateTime.Now:yyyyMMdd HH:mm:ss:fff}', '{entidadFinanciera}', '{totalMonto}')";

                StaticData.oCnn.ExecuteNonQuery(insertHistScript);

                string rutaArchivo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"{nombreNomina}.csv");
                File.WriteAllText(rutaArchivo, archivoCSV.ToString(), Encoding.UTF8);

                return rutaArchivo;
            }

            catch (Exception ex)
            {
                mensajeError = $"Error generando archivo: {ex.Message}";
                return null; ;
            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string nomina = cmbNomina.EditValue?.ToString();
            string numeroNomina = cmbNumeroNomina.EditValue?.ToString();
            string entidad = cmbEntidadFinanciera.EditValue?.ToString();

            string mensajeError;
            string rutaArchivo = GenerarArchivo(nomina, numeroNomina, entidad, out mensajeError);

            if (rutaArchivo != null)
            {
                XtraMessageBox.Show($"Archivo generado con éxito:\n{rutaArchivo}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Diagnostics.Process.Start("explorer.exe", Path.GetDirectoryName(rutaArchivo));
            }
            else
            {
                XtraMessageBox.Show($"Error: {mensajeError}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }

}
