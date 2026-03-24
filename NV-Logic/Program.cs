using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;

namespace NV2_Final
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex {
        public float X, Y, Z;
    }

    public class EngineWindow : Form
    {
        // Importy z wymuszonymi nazwami (EntryPoint)
        [DllImport("nv_core.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "check_core_status")]
        public static extern void check_core_status();

        [DllImport("nv_core.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "get_cube_edges")]
        public static extern void get_cube_edges(float x, float y, float z, [Out] Vertex[] vertices);

        private Label statusLabel;
        private Button actionButton;

        public EngineWindow()
        {
            this.Text = "NV-2.0 ENGINE | 3D PROTOCOL | CORE: RUST";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(15, 15, 15);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            statusLabel = new Label {
                Text = "ŁADOWANIE SILNIKA...",
                ForeColor = Color.Yellow,
                Font = new Font("Consolas", 11, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 20)
            };
            this.Controls.Add(statusLabel);

            actionButton = new Button {
                Text = "POBIERZ DANE 3D Z RUSTA",
                Size = new Size(220, 45),
                Location = new Point(20, 60),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.Cyan
            };
            actionButton.FlatAppearance.BorderColor = Color.Cyan;
            actionButton.Click += OnActionClick;
            this.Controls.Add(actionButton);

            InitEngine();
        }

        private void InitEngine()
        {
            try {
                // Ręczne sprawdzenie ścieżki dla pewności
                string dllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "nv_core.dll");
                if (File.Exists(dllPath)) {
                    NativeLibrary.Load(dllPath); 
                    check_core_status();
                    statusLabel.Text = "SYSTEM STABLE | RUST BRIDGE: OK";
                    statusLabel.ForeColor = Color.Lime;
                } else {
                    statusLabel.Text = "BŁĄD: BRAK NV_CORE.DLL W FOLDERZE BIN!";
                    statusLabel.ForeColor = Color.Red;
                }
            } catch (Exception ex) {
                statusLabel.Text = "BŁĄD DLL: " + ex.Message;
                statusLabel.ForeColor = Color.Red;
            }
        }

        private void OnActionClick(object? sender, EventArgs e)
        {
            Vertex[] cube = new Vertex[8];
            get_cube_edges(0, 0, 0, cube);
            
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Wierzchołki pobrane pomyślnie z Rusta:");
            foreach (var v in cube)
                sb.AppendLine($"X:{v.X} Y:{v.Y} Z:{v.Z}");

            MessageBox.Show(sb.ToString(), "Debug Silnika");
        }
    }

    class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new EngineWindow());
        }
    }
}