using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
using Negocio;
using System.IO;
using System.Security.Cryptography;
using Comun;
using System.Data.SqlClient;
using System.Configuration;
using ReportesTranspesa.Properties;
using System.Xml;
using System.Diagnostics;
using System.Net.NetworkInformation;


namespace ReportesTranspesa
{
    public partial class Login : MetroFramework.Forms.MetroForm
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            validarUsuario(txtUsuario.Text, txtPassword.Text);
        }

        private void Login_Load(object sender, EventArgs e)
        {
            try
            {
                //ActualizarGTR();
                this.BringToFront();
                txtUsuario.Text = Environment.UserName.ToUpper();
                txtUsuario.Select();
                txtUsuario.Focus();
                //toggleSwitch1.IsOn = true;
                toggleSwitch1_EditValueChanged(sender, e);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }


    static void EnsureMappedDrive(string drive, string share, string user, string pass)
    {
        // Normalizar "R:" -> "R:\"
        string driveRoot = drive.EndsWith(@":\") ? drive : (drive.TrimEnd('\\') + @"\");

        // 0) Si ya existe la raíz, intentamos usarla tal cual.
        if (Directory.Exists(driveRoot))
            return;

        // 1) Limpiar posibles conflictos
        RunHidden("net", string.Format("use {0} /delete /y", drive));                       // liberar la letra
        RunHidden("net", string.Format("use {0} /delete /y", share));                       // cerrar sesiones directas al share
        RunHidden("net", string.Format("use \\\\{0}\\* /delete /y", GetServerFromShare(share))); // cerrar sesiones al servidor

        // 2) Registrar credencial (evita el error 1219)
        RunHidden("cmdkey", string.Format("/delete:{0}", GetServerFromShare(share)));
        RunHidden("cmdkey", string.Format("/add:{0} /user:\"{1}\" /pass:\"{2}\"",
                                          GetServerFromShare(share), user, pass));

        // 3) Mapear
        Tuple<int,string> map = RunHidden("net", string.Format("use {0} \"{1}\" /persistent:no", drive, share));
        if (map.Item1 != 0)
            throw new InvalidOperationException(
                string.Format("No se pudo mapear {0} -> {1}. Código {2}. Salida: {3}",
                              drive, share, map.Item1, map.Item2));

        // Verificación final
        if (!Directory.Exists(driveRoot))
            throw new IOException(drive + " no está accesible después del mapeo.");
    }

    static string GetServerFromShare(string share)
    {
        // share esperado: \\server\recurso  -> devuelve "server"
        if (share.StartsWith(@"\\"))
        {
            string rest = share.Substring(2);
            int idx = rest.IndexOf('\\');
            if (idx > 0) return rest.Substring(0, idx);
        }
        return share;
    }


    static bool IsSourceNewer(string sourcePath, string destPath, TimeSpan tolerance)
    {
        if (!File.Exists(sourcePath))
            throw new FileNotFoundException("No se encontró el archivo de origen.", sourcePath);

        if (!File.Exists(destPath))
            return true; // si no existe destino, hay que actualizar

        DateTime s = File.GetLastWriteTimeUtc(sourcePath);
        DateTime d = File.GetLastWriteTimeUtc(destPath);
        return (s - d) > tolerance;
    }

        static Tuple<int, string> RunHidden(string file, string args)
        {
            var psi = new ProcessStartInfo();
            psi.FileName = file;
            psi.Arguments = args;
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardError = true;
            psi.RedirectStandardOutput = true;

            using (var p = Process.Start(psi))
            {
                if (p == null)
                    return Tuple.Create(-1, "No se pudo iniciar el proceso: " + file);

                string stdout = p.StandardOutput.ReadToEnd();
                string stderr = p.StandardError.ReadToEnd();
                p.WaitForExit();
                return Tuple.Create(p.ExitCode, stdout + stderr);
            }
        }

        static bool IsShareReachable(string share, int timeoutMs)
        {
            string host = GetServerFromShare(share);
            try
            {
                using (var p = new Ping())
                {
                    var r = p.Send(host, timeoutMs);
                    return (r != null && r.Status == IPStatus.Success);
                }
            }
            catch
            {
                return false; // si falla (DNS/ICMP bloqueado), lo tratamos como no accesible
            }
        }


        private void ActualizarGTR()
        {
            string rutaBat = @"D:\ReportesTranspesa2\ActualizarGTR.bat";
            const string drive = "R:";
            const string share = @"\\192.168.4.237\ReportesTranspesa2";
            const string user  = @"transpesa\administrador";
            const string pass  = @"Peru2016";

            string srcExe =  @"\\192.168.4.237\ReportesTranspesa2\ReportesTranspesa.exe";
            string dstExe =  @"D:\ReportesTranspesa2\ReportesTranspesa.exe";
            TimeSpan tolerance = TimeSpan.FromSeconds(2);

            if (IsShareReachable(share, 1500))
            {
                EnsureMappedDrive(drive, share, user, pass);
                bool hayActualizacion = IsSourceNewer(srcExe, dstExe, tolerance);

                if (hayActualizacion)
                {
                    if (File.Exists(rutaBat))
                    {
                        try
                        {
                            Process proceso = new Process();
                            proceso.StartInfo.FileName = rutaBat;
                            proceso.Start();
                            Application.Exit();

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Ocurrió un error al ejecutar el archivo: " + ex.Message);
                        }
                    }
                    else
                    {

                        try
                        {
                            rutaBat = @"\\192.168.4.237\ReportesTranspesa2\ActualizarGTR.bat";    
                            Process proceso = new Process();
                            proceso.StartInfo.FileName = rutaBat;
                            proceso.Start();
                            Application.Exit();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Ocurrió un error al ejecutar el archivo: " + ex.Message);
                        }

                    }
                }
            }

            }
         
                
           

        


        //sem pasó por aqui
        private void validarUsuario(String usuario, String password)
        {
            Boolean respuesta = clsUsuarioBL.Instancia.validar_usuario(usuario, password);

            //GERARDO - 20/01/24
            if (txtUsuario.Text == "VIGILA1" && txtPassword.Text == "123") { respuesta = true; } //LARREA 1
            if (txtUsuario.Text == "VIGILA2" && txtPassword.Text == "123") { respuesta = true; } //LARREA 2
            if (txtUsuario.Text == "VIGILA3" && txtPassword.Text == "123") { respuesta = true; } //SALAVERRY
            if (txtUsuario.Text == "VIGILA4" && txtPassword.Text == "123") { respuesta = true; } //LIMA
            if (txtUsuario.Text == "VIGILA5" && txtPassword.Text == "123") { respuesta = true; } //ENCALADA

            if (txtUsuario.Text == "MODULO-1" && txtPassword.Text == "123456") { respuesta = true; }
            if (txtUsuario.Text == "MODULO-2" && txtPassword.Text == "123456") { respuesta = true; }
            if (txtUsuario.Text == "MODULO-3" && txtPassword.Text == "123456") { respuesta = true; }

            if (txtUsuario.Text == "MEDICO" && txtPassword.Text == "123456") { respuesta = true; }

            if (txtUsuario.Text == "LOCAL" && txtPassword.Text == "123456") { respuesta = true; }

            if (respuesta)
            {
                if (txtUsuario.Text == "VIGILA1" && txtPassword.Text == "123")
                {
                    clsUsuario obj = new clsUsuario();
                    obj.usuario = "VIGILA1";
                    obj.nombres = "VIGILANTES - LARREA1";
                    obj.idUsuario = 9999;
                    Utilitario.Instancia.SesionUsuario = obj;
                }

                if (txtUsuario.Text == "VIGILA2" && txtPassword.Text == "123")
                {
                    clsUsuario obj = new clsUsuario();
                    obj.usuario = "VIGILA2";
                    obj.nombres = "VIGILANTES - LARREA2";
                    obj.idUsuario = 9998;
                    Utilitario.Instancia.SesionUsuario = obj;
                }

                if (txtUsuario.Text == "VIGILA3" && txtPassword.Text == "123")
                {
                    clsUsuario obj = new clsUsuario();
                    obj.usuario = "VIGILA3";
                    obj.nombres = "VIGILANTES - SALAVERRY";
                    obj.idUsuario = 9997;
                    Utilitario.Instancia.SesionUsuario = obj;
                }

                if (txtUsuario.Text == "VIGILA4" && txtPassword.Text == "123")
                {
                    clsUsuario obj = new clsUsuario();
                    obj.usuario = "VIGILA4";
                    obj.nombres = "VIGILANTES - LIMA";
                    obj.idUsuario = 9996;
                    Utilitario.Instancia.SesionUsuario = obj;
                }

                if (txtUsuario.Text == "VIGILA5" && txtPassword.Text == "123")
                {
                    clsUsuario obj = new clsUsuario();
                    obj.usuario = "VIGILA5";
                    obj.nombres = "VIGILANTES - ENCALADA";
                    obj.idUsuario = 9995;
                    Utilitario.Instancia.SesionUsuario = obj;
                }

                if (txtUsuario.Text == "MODULO-1" && txtPassword.Text == "123456")
                {
                    clsUsuario obj = new clsUsuario();
                    obj.usuario = "MODULO-1";
                    obj.nombres = "MECANICOS 1";
                    obj.idUsuario = 9990;
                    Utilitario.Instancia.SesionUsuario = obj;
                }

                if (txtUsuario.Text == "MODULO-2" && txtPassword.Text == "123456")
                {
                    clsUsuario obj = new clsUsuario();
                    obj.usuario = "MODULO-2";
                    obj.nombres = "MECANICOS 2";
                    obj.idUsuario = 9991;
                    Utilitario.Instancia.SesionUsuario = obj;
                }

                if (txtUsuario.Text == "MODULO-3" && txtPassword.Text == "123456")
                {
                    clsUsuario obj = new clsUsuario();
                    obj.usuario = "MODULO-3";
                    obj.nombres = "MECANICOS 3";
                    obj.idUsuario = 9992;
                    Utilitario.Instancia.SesionUsuario = obj;
                }

                if (txtUsuario.Text == "MEDICO" && txtPassword.Text == "123456")
                {
                    clsUsuario obj = new clsUsuario();
                    obj.usuario = "MEDICO";
                    obj.nombres = "MÉDICO OCUPACIONAL";
                    obj.idUsuario = 9994;
                    Utilitario.Instancia.SesionUsuario = obj;
                }

                if (txtUsuario.Text == "LOCAL" && txtPassword.Text == "123456")
                {
                    clsUsuario obj = new clsUsuario();
                    obj.usuario = "LOCAL";
                    obj.nombres = "USUARIO LOCAL";
                    obj.idUsuario = 9989;
                    Utilitario.Instancia.SesionUsuario = obj;
                }

                Menu frmMenu = new Menu();
                this.Hide();
                frmMenu.Show();
            }
            else
            {
                MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {

            //base.OnKeyPress(e);

            if (char.IsLetter(e.KeyChar))
            {
                e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
            }


            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtPassword.Focus();
            }

        }

        //sem pasó por aqui
        public string EncryptString(string inputString)
        {
            MemoryStream memStream = null;
            try
            {
                byte[] key = { };
                byte[] IV = { 12, 21, 43, 17, 57, 35, 67, 27 };
                string encryptKey = "aXb2uy4z"; // MUST be 8 characters
                key = Encoding.UTF8.GetBytes(encryptKey);
                byte[] byteInput = Encoding.UTF8.GetBytes(inputString);
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                memStream = new MemoryStream();
                ICryptoTransform transform = provider.CreateEncryptor(key, IV);
                CryptoStream cryptoStream = new CryptoStream(memStream, transform, CryptoStreamMode.Write);
                cryptoStream.Write(byteInput, 0, byteInput.Length);
                cryptoStream.FlushFinalBlock();

            }
            catch (Exception ex)
            {

            }
            String x = Convert.ToBase64String(memStream.ToArray());
            return Convert.ToBase64String(memStream.ToArray());
        }

        public void DecryptString(string inputString)
        {
            MemoryStream memStream = null;
            try
            {
                byte[] key = { };
                byte[] IV = { 12, 21, 43, 17, 57, 35, 67, 27 };
                string encryptKey = "aXb2uy4z"; // MUST be 8 characters
                key = Encoding.UTF8.GetBytes(encryptKey);
                byte[] byteInput = new byte[inputString.Length];
                byteInput = Convert.FromBase64String(inputString);
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                memStream = new MemoryStream();
                ICryptoTransform transform = provider.CreateDecryptor(key, IV);
                CryptoStream cryptoStream = new CryptoStream(memStream, transform, CryptoStreamMode.Write);
                cryptoStream.Write(byteInput, 0, byteInput.Length);
                cryptoStream.FlushFinalBlock();
            }
            catch (Exception ex)
            {
            }

            Encoding encoding1 = Encoding.UTF8;
            string a = encoding1.GetString(memStream.ToArray());
        }


        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnLogin.Focus();
            }
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {
            //txtUsuario.Text = txtUsuario.Text.ToUpper();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            validarUsuario(txtUsuario.Text, txtPassword.Text);
        }

        private void btnCambiarRed_Click(object sender, EventArgs e)
        {
           /* try
            {
                if (btnCambiarRed.Text == "LOCAL")
                {
                    btnCambiarRed.Text = "REMOTO";
                    btnCambiarRed.BackColor = Color.Green;
                    btnCambiarRed.ForeColor = Color.White;

                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                    builder.TransactionBinding = "LOCAL";

                    TextBox servidor = new TextBox(); ;
                    clsDBBL.Instancia.IP_Servidor_CambiarPublico(builder, ref servidor);



                    MessageBox.Show("Probando conexion exitosa a la Instancia: " + servidor.Text);
                    Comun.Utilitario.Instancia.TextoMenuServidor = "Servidor: " + servidor.Text + " - Base Datos: " + "spring";

                    this.DialogResult = DialogResult.OK;
                    // Application.Restart();
                }
                else
                {
                    btnCambiarRed.Text = "REMOTO";
                    btnCambiarRed.BackColor = Color.DarkRed;
                    btnCambiarRed.ForeColor = Color.White;


                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                    builder.TransactionBinding = "REMOTO";
                    TextBox servidor = new TextBox();

                    clsDBBL.Instancia.IP_Servidor_CambiarPublico(builder, ref servidor);

                    MessageBox.Show("Probando conexion exitosa a la Instancia: " + servidor.Text);
                    Comun.Utilitario.Instancia.TextoMenuServidor = "Servidor: " + servidor.Text + " - Base Datos: " + "spring";

                    this.DialogResult = DialogResult.OK;
                    //Application.Restart();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

        private void toggleSwitch1_Click(object sender, EventArgs e)
        {
           
        }

        private void toggleSwitch1_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                string tipoConexion = config.AppSettings.Settings["tipoConexion"].Value;


           
                if (toggleSwitch1.IsOn)
                {
            
                    config.AppSettings.Settings["tipoConexion"].Value = "publico";
                    config.Save(ConfigurationSaveMode.Modified, true);
                    Properties.Settings.Default.Save();
                    tipoConexion =  config.AppSettings.Settings["tipoConexion"].Value;
                    Comun.Utilitario.Instancia.TextoMenuServidor = "Servidor: 190.116.64.132,29692 - Base Datos: " + "spring";

                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);


                    foreach (XmlNode childNode in xmlDoc.DocumentElement)
                    {
                        if (childNode.Name.Equals("appSettings"))
                        {
                            foreach (XmlNode item in childNode.ChildNodes)
                            {
                                if (item.Attributes[0].Value == "tipoConexion")
                                {
                                    item.Attributes[1].Value = "publico";
                                }
                                    
                            }
                        }
                    }

                    xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                    ConfigurationManager.RefreshSection("appSettings");
                }
                else
                {
              
                    config.AppSettings.Settings["tipoConexion"].Value = "local";
                    config.Save(ConfigurationSaveMode.Modified, true);
                    Properties.Settings.Default.Save();
          
                    tipoConexion = config.AppSettings.Settings["tipoConexion"].Value;
                    Comun.Utilitario.Instancia.TextoMenuServidor = "Servidor: 172.16.0.11 - Base Datos: " + "spring";

                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                    foreach (XmlNode childNode in xmlDoc.DocumentElement)
                    {
                        if (childNode.Name.Equals("appSettings"))
                        {
                            foreach (XmlNode item in childNode.ChildNodes)
                            {
                                if (item.Attributes[0].Value == "tipoConexion")
                                {
                                    item.Attributes[1].Value = "publico";
                                }

                            }
                        }
                    }

                    xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                    ConfigurationManager.RefreshSection("appSettings");
                }



                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
