﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Tulpep.NotificationWindow;
using System.Diagnostics;

namespace DecoTools_V2._0
{
    public partial class FormCccam : Form
    {
        public FormCccam()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            FormPrincipal not = new FormPrincipal();

            while (chkCccam.Checked == false && chkOscam.Checked == false && chkNcam.Checked == false && chkGcam.Checked == false)
            {
                not.PopupNotificacion(Properties.Resources.error, "Debe seleccionar una emuladora.", "Softcam");
                return;
            }
            while (txbCccam.Text == "")
            {
                not.PopupNotificacion(Properties.Resources.error, "Debe contener una cline como mínimo.", "Softcam");
                return;
            }
            if (txbCccam.Text.Contains("  "))
            {
                not.PopupNotificacion(Properties.Resources.error, "Alguna de sus lineas contiene un doble espacio.", "Softcam");
                return;
            }

            if (txbCccam.Lines.Length > 4)
            {
                not.PopupNotificacion(Properties.Resources.advertencia, "Mas de 4 lineas puede causar colapso en su emuladora.", "Softcam");
            }
            if (chkCccam.Checked == true)
            {
                //SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                //saveFileDialog1.FileName = DecoTools_V2._0.Program.nombreArchivo;
                //saveFileDialog1.ShowDialog();

                //if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                //{
                //DecoTools_V2._0.Program.rutaArchivo = saveFileDialog1.FileName;
                string rutaNueva = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\CCcam.cfg";
                File.WriteAllText(rutaNueva, txbCccam.Text);
                not.PopupNotificacion(Properties.Resources.info, "Sus clines para el protocolo CCcam se han descargado en el escritorio", "CCcam");

                //}
                //else
                //    not.PopupNotificacion(Properties.Resources.error, "Cancelado por el usuario.", "CCcam");
            }

            if (chkOscam.Checked == true)
            {
                string archivoFinal = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\oscam.server";
                //SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                //saveFileDialog1.FileName = "oscam.server";
                ////saveFileDialog1.ShowDialog();
                //if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                //{
                //string rutaNueva = saveFileDialog1.FileName;
                string rutaNueva = Path.Combine(Application.StartupPath, "rutaNueva.txt");
                string ruta = Path.Combine(Application.StartupPath, "prueba.txt");
                File.WriteAllText(ruta, txbOscam.Text);
                string texto = File.ReadAllText(ruta);
                string textoOrigen = texto;
                string auxiliar = Path.Combine(Application.StartupPath, "auxiliar.txt");
                File.WriteAllText(auxiliar, txbCccam.Text);
                int contador = 1;
                string cadena = null;
                StreamReader reader = new StreamReader(auxiliar);
                byte[] serverOscam = null;
                serverOscam = Properties.Resources.oscam;
                string plantillaOscam = System.Text.Encoding.UTF8.GetString(serverOscam);
                File.AppendAllText(rutaNueva, plantillaOscam + Environment.NewLine + Environment.NewLine);

                cadena = reader.ReadLine();

                do
                {

                    try
                    {
                        string[] clines = cadena.Split(' ');
                        string valor0 = clines[0];
                        if (clines.Length < 5)
                        {
                            not.PopupNotificacion(Properties.Resources.error, "Las clines deben contener 5 elementos.", "Oscam");
                            File.Delete(ruta);
                            reader.Close();
                            File.Delete(auxiliar);
                            File.Delete(rutaNueva);
                            return;
                        }
                        if (valor0 != "C:" & valor0 != "c:")
                        {
                            not.PopupNotificacion(Properties.Resources.error, "Las clines deben empezar por C: en todas sus líneas.", "Oscam");
                            File.Delete(ruta);
                            reader.Close();
                            File.Delete(auxiliar);
                            File.Delete(rutaNueva);
                            return;
                        }
                        string host = clines[1];
                        string puerto = clines[2];
                        string user = clines[3];
                        string pass = clines[4];
                        texto = texto.Replace("LINEA", "LINEA" + contador);
                        texto = texto.Replace("description=remote_cccam", "description=remote_cccam" + contador);
                        texto = texto.Replace("HOST,PUERTO", host + "," + puerto);
                        texto = texto.Replace("USUARIO", user);
                        texto = texto.Replace("CONTRASEÑA", pass);
                        contador++;
                    }
                    catch
                    {
                        not.PopupNotificacion(Properties.Resources.info, "Falta poco.", "Oscam");
                    }
                    File.AppendAllText(rutaNueva, texto);
                    File.AppendAllText(rutaNueva, Environment.NewLine + Environment.NewLine);
                    texto = textoOrigen;
                    cadena = reader.ReadLine();

                } while (cadena != null);

                reader.Close();
                StreamReader oscam = new StreamReader(rutaNueva);
                string final = File.ReadAllText(rutaNueva);
                File.WriteAllText(archivoFinal, final);
                oscam.Close();
                File.Delete(rutaNueva);
                File.Delete(ruta);
                File.Delete(auxiliar);
                not.PopupNotificacion(Properties.Resources.info, "Sus clines para el protocolo Oscam se han descargado en el escritorio", "Oscam");
                //}
                //else
                //    not.PopupNotificacion(Properties.Resources.error, "Cancelado por el usuario.", "Oscam");
            }
            if (chkNcam.Checked == true)
            {

                //SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                //saveFileDialog1.FileName = "ncam.server";
                ////saveFileDialog1.ShowDialog();
                //if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                //{
                //string rutaNueva = saveFileDialog1.FileName;
                string archivoFinal = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\ncam.server";
                string rutaNueva = Path.Combine(Application.StartupPath, "rutaNueva.txt");
                string ruta = Path.Combine(Application.StartupPath, "prueba.txt");
                File.WriteAllText(ruta, txbNcam1.Text);
                string texto = File.ReadAllText(ruta);
                string textoOrigen = texto;
                string auxiliar = Path.Combine(Application.StartupPath, "auxiliar.txt");
                File.WriteAllText(auxiliar, txbCccam.Text);
                int contador = 1;
                string cadena = null;
                StreamReader reader = new StreamReader(auxiliar);
                byte[] serverNcam = null;
                serverNcam = Properties.Resources.ncam;
                string plantillaNcam = System.Text.Encoding.UTF8.GetString(serverNcam);
                File.AppendAllText(rutaNueva, plantillaNcam + Environment.NewLine + Environment.NewLine);
                cadena = reader.ReadLine();
                do
                {

                    try
                    {
                        string[] clines = cadena.Split(' ');
                        string valor0 = clines[0];
                        if (clines.Length < 5)
                        {
                            not.PopupNotificacion(Properties.Resources.error, "Las clines deben contener 5 elementos.", "Ncam");
                            File.Delete(ruta);
                            reader.Close();
                            File.Delete(auxiliar);
                            File.Delete(rutaNueva);
                            return;
                        }
                        if (valor0 != "C:" & valor0 != "c:")
                        {
                            not.PopupNotificacion(Properties.Resources.error, "Las clines deben empezar por C: en todas sus líneas.", "Ncam");
                            File.Delete(ruta);
                            reader.Close();
                            File.Delete(auxiliar);
                            File.Delete(rutaNueva);
                            return;
                        }
                        string host = clines[1];
                        string port = clines[2];
                        string user = clines[3];
                        string pass = clines[4];
                        texto = texto.Replace("Server_", "Server_" + contador);
                        //texto = texto.Replace("description=remote_cccam", "description=remote_cccam" + contador);
                        texto = texto.Replace("ip,puerto", host + "," + port);
                        texto = texto.Replace("usuario", user);
                        texto = texto.Replace("contraseña", pass);
                        contador++;
                    }
                    catch
                    {
                        not.PopupNotificacion(Properties.Resources.info, "Falta poco.", "Ncam");
                    }
                    File.AppendAllText(rutaNueva, texto);
                    File.AppendAllText(rutaNueva, Environment.NewLine + Environment.NewLine);
                    texto = textoOrigen;
                    cadena = reader.ReadLine();

                } while (cadena != null);

                reader.Close();
                StreamReader ncam = new StreamReader(rutaNueva);
                string final = File.ReadAllText(rutaNueva);
                File.WriteAllText(archivoFinal, final);
                ncam.Close();
                File.Delete(rutaNueva);
                File.Delete(ruta);
                File.Delete(auxiliar);
                not.PopupNotificacion(Properties.Resources.info, "Sus clines para el protocolo Ncam se han descargado en el escritorio", "Ncam");

            }

            if (chkCccam.Checked == true) { chkCccam.Checked = false; }
            if (chkOscam.Checked == true) { chkOscam.Checked = false; }
            if (chkNcam.Checked == true) { chkNcam.Checked = false; }
            if (chkGcam.Checked == true) { chkGcam.Checked = false; }


            //llamamos al formftp para cargar los archivos al decodificador mediante un messagebox

            FormMessageBox pregunta = new FormMessageBox();
            //pregunta.ShowDialog();
            pregunta.MostrarBotonesAceptar(true);
            pregunta.MensajeDeco("¿Desea cargar el archivo CCcam.cfg creado a su decodificador?", "CCcam.cfg");
            pregunta.ShowDialog();
            bool respuesta = pregunta.Aceptar();
            if (respuesta == true)
            {
                FormFtp ftp = new FormFtp();
                //ftp.Show();
                ftp.ShowDialog();
                this.Refresh();
            }
        }
    }
}
