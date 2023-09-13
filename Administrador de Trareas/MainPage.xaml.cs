using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using System.Threading.Tasks;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace Administrador_de_Trareas
{
    public sealed partial class MainPage : Page
    {
        private List<Process> processes = new List<Process>();
        private int currentProcessIndex = 0;
        private int quantumValue = 1; // Valor inicial del quantum
        private Dictionary<Process, double> cellPositions = new Dictionary<Process, double>();

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void AddProcess_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // Obtener valores de los campos de entrada
            string name = processNameTextBox.Text.Trim();
            int resources;
            int quantum;

            // Verificar si alguno de los campos está vacío
            if (string.IsNullOrWhiteSpace(name) ||
                !int.TryParse(resourceTextBox.Text.Trim(), out resources) ||
                !int.TryParse(quantumSlider.Value.ToString(), out quantum) ||
                resources <= 0)
            {
                ShowErrorMessage("Por favor, complete todos los campos correctamente.");
                return; // Salir de la función sin agregar el proceso
            }

            // Verificar si el proceso con el mismo nombre ya existe
            if (processes.Any(p => p.Name == name))
            {
                ShowErrorMessage($"Ya existe un proceso con el nombre '{name}'.");
                return; // Salir de la función sin agregar el proceso
            }

            // Asignar un color único al proceso
            Color color = GetRandomColor();

            // Crear y agregar el proceso a la lista
            Process process = new Process(name, resources, quantum, color);
            processes.Add(process);

            // Limpiar campos de entrada
            processNameTextBox.Text = "";
            resourceTextBox.Text = "";

            // Actualizar la lista de la interfaz de usuario
            processListView.ItemsSource = null; // Limpiar la vista anterior
            processListView.ItemsSource = processes;
        }
    

