using System;
using Softland.Tools.ConexionBD;
using Softland.EDK;
using System.Windows.Forms;
using Microsoft.Win32;

namespace DmRptBanco
{
    public class StaticData
    {
        public static Conexion oCnn;
        public static string UsuarioSinX { get; set; }
        public static string usuario_sin_x()
        {
            if (oCnn.UsuarioActual.ToUpper() == "SA" || oCnn.UsuarioActual.ToUpper() == "SYSTEM")
            {
                return oCnn.UsuarioActual.ToUpper();
            }
            return oCnn.UsuarioActual.Substring(1, oCnn.UsuarioActual.Length - 1);
        }

        public static bool ObtenerServidorODBC(string baseDatos, out string servidor, out string nombreBaseDatos)
        {

            bool flag = false;
            servidor = "";
            nombreBaseDatos = "";
            try
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\ODBC\ODBC.INI");
                if (key == null)
                {
                    return flag;
                }
                RegistryKey key2 = key.OpenSubKey(baseDatos);
                if (key2 == null)
                {
                    return flag;
                }
                object obj2 = key2.GetValue("Server");
                if (obj2 != null)
                {
                    servidor = obj2.ToString().ToUpper();
                    flag = true;
                }
                else
                {
                    MessageBox.Show(string.Format("No fue posible obtener el servidor a partir de la configuraci\x00f3n del ODBC con nombre '{0}'.", baseDatos), "Obteniendo Servidor de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    flag = false;
                }
                if (!flag)
                {
                    return flag;
                }
                obj2 = key2.GetValue("Database");
                if (obj2 != null)
                {
                    nombreBaseDatos = obj2.ToString().ToUpper();
                    return true;
                }
                MessageBox.Show(string.Format("No fue posible obtener el servidor a partir de la configuraci\x00f3n del ODBC con nombre '{0}'.", baseDatos), "Obteniendo Servidor de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            catch (Exception exception)
            {
                MessageBox.Show(string.Format("No fue posible verificar el ODBC para el alias de base de datos '{0}'.\nDetalle: {1}", baseDatos, exception), "Obteniendo Servidor de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
        }

        public static string ProximoCodigo(string psCodigoOriginal, int pnLongitudMaxima)
        {
            char[] chArray = new char[50];
            string s = "";
            if (string.IsNullOrEmpty(psCodigoOriginal))
            {
                chArray = "1".ToCharArray();
            }
            else
            {
                try
                {
                    char ch;
                    bool flag2 = false;
                    bool flag = true;
                    chArray = psCodigoOriginal.ToCharArray();
                    int length = psCodigoOriginal.Length;
                    int index = length - 1;
                    if (pnLongitudMaxima >= length)
                    {
                        goto Label_01DC;
                    }
                    chArray = psCodigoOriginal.ToCharArray();
                    goto Label_01EC;
                    Label_0052:
                    ch = psCodigoOriginal[index];
                    string str = ch.ToString()[0].ToString();
                    if (char.IsDigit(str[0]))
                    {
                        flag2 = false;
                        if (str.Equals("9"))
                        {
                            s = "0";
                            flag = true;
                        }
                        else
                        {
                            s = (Convert.ToInt32(str) + 1).ToString();
                            flag = false;
                        }
                        chArray[index] = char.Parse(s);
                    }
                    else if (char.IsLetter(str[0]))
                    {
                        flag2 = false;
                        if (str.Equals("Z"))
                        {
                            chArray[index] = 'A';
                            flag = true;
                        }
                        else if (str.Equals("z"))
                        {
                            chArray[index] = 'a';
                            flag = true;
                        }
                        else
                        {
                            flag = false;
                            chArray[index] = (char)(chArray[index] + '\x0001');
                        }
                    }
                    else if (str.Equals(Environment.NewLine))
                    {
                        flag2 = true;
                        flag = false;
                        if (char.IsLower(str[0]))
                        {
                            s = "A";
                        }
                        else
                        {
                            s = "a";
                        }
                        chArray[index] = char.Parse(s);
                    }
                    else if (index == length - 1)
                    {
                        flag = true;
                    }
                    if (index == 0 && flag)
                    {
                        if (chArray.Length == pnLongitudMaxima)
                        {
                            chArray = psCodigoOriginal.ToCharArray();
                        }
                        else if (!flag2)
                        {
                            string str3;
                            if (s.Equals("0"))
                            {
                                str3 = "1" + new string(chArray);
                            }
                            else if (s.Equals("A"))
                            {
                                str3 = "A" + new string(chArray);
                            }
                            else
                            {
                                str3 = "a" + new string(chArray);
                            }
                            chArray = str3.ToCharArray();
                        }
                    }
                    index--;
                    Label_01DC:
                    if (index >= 0 && flag)
                    {
                        goto Label_0052;
                    }
                }
                catch
                {
                }
            }
            Label_01EC:
            return new string(chArray);
        }

        /* public static void SendToExcel(string formName, UltraGrid grid)
         {
             try
             {
                 CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
                 Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
                 ApplicationClass class2 = new ApplicationClass();
                 class2.Visible = true;
                 Worksheet worksheet = (Worksheet)class2.Application.Workbooks.Add(Type.Missing).Sheets[1];
                 string str = formName.Trim();
                 if (str.IndexOfAny(@":\/?*[]".ToCharArray()) > -1)
                 {
                     str = str.Trim(@":\/?*[]".ToCharArray());
                 }
                 if (str.Length <= 0)
                 {
                     str = "Sin Nombre";
                 }
                 if (str.Length > 0x1f)
                 {
                     str = str.Substring(0, 0x1f);
                 }
                 worksheet.Name = str;
                 int num = 1;
                 foreach (UltraGridColumn column in grid.Rows.Band.Columns)
                 {
                     if (column.IsVisibleInLayout)
                     {
                         worksheet.Cells[1, num] = column.Header.Caption.Trim();
                         num++;
                     }
                 }
                 Range range = (Range)worksheet.Rows["1:1", Type.Missing];
                 range.Font.Bold = true;
                 range.Font.Size = 12;
                 int num2 = 2;
                 string s = "";
                 foreach (UltraGridRow row in grid.Rows)
                 {
                     if (row.Selected)
                     {
                         num = 1;
                         foreach (UltraGridColumn column2 in row.Band.Columns)
                         {
                             if (!column2.IsVisibleInLayout)
                             {
                                 continue;
                             }
                             if (row.Cells[column2.Index].Value != null)
                             {
                                 s = row.Cells[column2.Index].Value.ToString();
                                 decimal result = 0M;
                                 if (column2.ValueList != null)
                                 {
                                     s = row.Cells[column2.Index].Text;
                                 }
                                 if (decimal.TryParse(s, out result))
                                 {
                                     worksheet.Cells[num2, num] = "'" + s;
                                 }
                                 else
                                 {
                                     worksheet.Cells[num2, num] = s;
                                 }
                             }
                             num++;
                         }
                         range = (Range)worksheet.Rows[string.Format("{0}:{0}", num2.ToString()), Type.Missing];
                         range.Font.Bold = false;
                         range.Font.Size = 11;
                         if (grid.Rows.Count == (num2 - 1))
                         {
                             range = (Range)worksheet.Columns["A:Z", Type.Missing];
                             range.EntireColumn.AutoFit();
                         }
                         num2++;
                     }
                 }
                 Thread.CurrentThread.CurrentCulture = currentCulture;
             }
             catch
             {
                 MessageBox.Show("Error al enviar a Excel", "Hubo un problema al enviar la informacion a Excel");
             }
         }
         */
        /// <summary>
        /// Función para limpiar los controles de un formulario.
        /// </summary>
        /// <param name="frm">Formulario al cual se limpiaran los controles </param>
        public static void CleanControls(Form frm)
        {
            foreach (Control control in frm.Controls)
            {
                if (control is TextBox)
                    control.Text = "";
                else if (control is DateTimePicker)
                    control.Text = "";
                else if (control is TabControl)
                {
                    foreach (TabPage tp in control.Controls)
                    {
                        foreach (Control c1 in tp.Controls)
                        {
                            if (c1 is TextBox)
                                c1.Text = "";
                            else if (c1 is DateTimePicker)
                                c1.Text = "";
                            else if (c1 is GroupBox)
                            {
                                foreach (Control c in c1.Controls)
                                {
                                    if (c is TextBox)
                                        c.Text = "";
                                }
                            }
                        }
                    }
                }
                else if (control is GroupBox)
                {
                    foreach (Control c in control.Controls)
                    {
                        if (c is TextBox)
                            c.Text = "";
                    }
                }
            }
        }

        /// <param name="control">control para limpiar su contenido </param>
        public static void CleanControls(Control.ControlCollection control)
        {
            foreach (Control ctr in control)
            {
                if (ctr is TabPage)
                {
                    foreach (Control c in ctr.Controls)
                    {
                        if (c is TextBox)
                            c.Text = "";
                        else if (c is DateTimePicker)
                            c.Text = "";
                        else if (c is GroupBox)
                        {
                            foreach (Control cg in c.Controls)
                            {
                                if (cg is TextBox)
                                    cg.Text = "";
                            }
                        }
                    }
                }
            }
        }

        public static void GetUserX(string user, out string xUser, out string xPwd)
        {
            EDK obj = new EDK();
            string usuarioProxy;
            string passProxy;
            bool flag = obj.GetProxyInfo(user, out usuarioProxy, out passProxy);
            xUser = usuarioProxy;
            xPwd = passProxy;
        }

        // IsNumeric Function
        public static bool IsNumeric(object Expression)
        {
            // Variable to collect the Return value of the TryParse method.
            bool isNum;

            // Define variable to collect out parameter of the TryParse method. If the conversion fails, the out parameter is zero.
            double retNum;

            // The TryParse method converts a string in a specified style and culture-specific format to its double-precision floating point number equivalent.
            // The TryParse method does not generate an exception if the conversion fails. If the conversion passes, True is returned. If it does not, False is returned.
            isNum = double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;

        }


    }
}
