using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.IO;
using ZXing;
using ZXing.Rendering;
using WindowsInput;

namespace pcontrol2._0
{
    public partial class pcontrol : Form
    {
        private Type Renderer { get; set; }
        private int PORT = 11000;
        private Thread UDPthread;

        public pcontrol()
        {
            InitializeComponent();
            //String ip = Util.getIP();
            String ip = "192.168.1.104"; // need to be replaced by your own IP address
            String msg = Util.encrypt(ip + ":" + PORT, "batmanbegins");
            renderQR(msg);

            Console.WriteLine(ip);
           
            UDPthread = new Thread(() =>Util.listenUDP(PORT));
            UDPthread.Start();
        }


        private void renderQR(String text)
        {
            Renderer = typeof(BitmapRenderer);
            try
            {
                var writer = new BarcodeWriter
                {
                    Format = BarcodeFormat.QR_CODE,
                    Options = new ZXing.Common.EncodingOptions
                    {
                        Height = qrBox.Height,
                        Width = qrBox.Width
                    },
                    Renderer = (IBarcodeRenderer<Bitmap>)Activator.CreateInstance(Renderer)
                };
                qrBox.Image = writer.Write(text);

            }
            catch (Exception exc)
            {
                MessageBox.Show(this, exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void pcontrol_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            Util.done = true;
            Console.WriteLine("closed");
            UDPthread.Join();
        }

    }

    public class Util {
        public static String getIP()
        {
            IPHostEntry host;
            //String theIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "";
        }
        public static RijndaelManaged getRijndaelManaged(String secretKey)
        {
            var keyBytes = new Byte[16];
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
            Array.Copy(secretKeyBytes, keyBytes, Math.Min(keyBytes.Length, secretKeyBytes.Length));
            return new RijndaelManaged
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                KeySize = 128,
                BlockSize = 128,
                Key = keyBytes,
                IV = keyBytes
            };
        }
        public static byte[] encrypt(byte[] plainBytes, RijndaelManaged rijndaelManaged)
        {
            return rijndaelManaged.CreateEncryptor()
                .TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        }
        public static String encrypt(String plainText, String key)
        {
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(encrypt(plainBytes, getRijndaelManaged(key)));
        }

        
        public static Boolean done = false;
        public static void listenUDP(int port) {
            
            UdpClient client = new UdpClient(port);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, port);

            byte[] received_byte_array;
            handleBtnDelegate handleBtn,
            keydown = new handleBtnDelegate(InputSimulator.SimulateKeyDown),
            keyup = new handleBtnDelegate(InputSimulator.SimulateKeyUp);

            
            try
            {
                while (!done)
                {
                    Console.WriteLine("done:" + done);
                    received_byte_array = client.Receive(ref groupEP);
                    byte btn = received_byte_array[0], stat = received_byte_array[1];
                    if (stat == 1)
                    {
                        handleBtn = keydown;
                    }
                    else {
                        handleBtn = keyup;
                    }
                    switch (btn) { 
                        case 1:
                            handleBtn(VirtualKeyCode.VK_W);
                            break;
                        case 2:
                            handleBtn(VirtualKeyCode.VK_D);
                            break;
                        case 3:
                            handleBtn(VirtualKeyCode.VK_S);
                            break;
                        case 4:
                            handleBtn(VirtualKeyCode.VK_A);
                            break;
                        case 5:
                            handleBtn(VirtualKeyCode.VK_J);
                            break;
                        case 6:
                            handleBtn(VirtualKeyCode.VK_K);
                            
                            break;
                        default:
                            Console.WriteLine("default");
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            client.Close(); 
        
        }
    }

    public delegate void handleBtnDelegate(WindowsInput.VirtualKeyCode btn);
}
