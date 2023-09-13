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

        private async void ShowErrorMessage(string message)
        {
            var dialog = new Windows.UI.Popups.MessageDialog(message);
            await dialog.ShowAsync();
        }

        private async void Process_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (processes.Count == 0)
            {
                // No hay procesos para procesar
                return;
            }

            // Obtener el Canvas del XAML
            var canvas = cpuCanvas;
            int quantumValue = int.Parse(quantumValueText.Text);
            // Inicializar una lista para llevar un seguimiento de las celdas registradas en el Canvas
            List<Rectangle> rectangles = new List<Rectangle>();

            // Recorrer la lista de procesos en un ciclo infinito (Round Robin)
            while (true)
            {
                foreach (var process in processes.ToList()) // Usar ToList() para evitar modificaciones concurrentes
                {
                    // Crear un rectángulo con el color del proceso
                    var rect = new Rectangle
                    {
                        Width = 20,
                        Height = 20,
                        Fill = new SolidColorBrush(process.Color),
                    };

                    // Agregar el rectángulo a la lista y al Canvas
                    rectangles.Add(rect);
                    canvas.Children.Add(rect);

                    // Posicionar el rectángulo en el Canvas (al principio)
                    Canvas.SetTop(rect, 0); // Posición fija en la parte superior
                    Canvas.SetLeft(rect, 0);

                    // Reducir los recursos del proceso según el quantum
                    int quantumResources = quantumValue * 10;
                    process.Resources -= quantumResources;

                    // Mover todas las celdas registradas (incluyendo el rectángulo en blanco) a la derecha
                    foreach (var existingRect in rectangles)
                    {
                        Canvas.SetLeft(existingRect, Canvas.GetLeft(existingRect) + rect.Width);
                    }

                    // Verificar si el proceso se ha completado o quedó en negativo
                    if (process.Resources < 0 & process.Resources != 0)
                    {
                        // Agregar un rectángulo en blanco (color blanco)
                        var emptyRect = new Rectangle
                        {
                            Width = 20,
                            Height = 20,
                            Fill = new SolidColorBrush(Colors.White),
                        };

                        // Agregar el rectángulo en blanco a la lista y al Canvas
                        rectangles.Add(emptyRect);
                        canvas.Children.Add(emptyRect);

                        // Mover todas las celdas registradas (incluyendo el rectángulo en blanco) a la derecha
                        foreach (var existingRect in rectangles)
                        {
                            Canvas.SetLeft(existingRect, Canvas.GetLeft(existingRect) + emptyRect.Width);
                        }

                        // Esperar 500 milisegundos (0.5 segundos)
                        await Task.Delay(500);

                        // Quitar el proceso completado de la lista y el rectángulo del Canvas
                        processes.Remove(process);

                    }
                    else if (process.Resources == 0)
                    {
                        // Quitar el proceso completado de la lista y el rectángulo del Canvas
                        processes.Remove(process);
                    }
                    else
                    {
                        // Esperar 500 milisegundos (0.5 segundos)
                        await Task.Delay(500);
                    }
                }

                // Verificar si todos los procesos se han completado
                if (processes.Count == 0)
                {
                    break; // Salir del ciclo si no hay procesos en la cola
                }
            }
        }



        private Color GetRandomColor()
        {
            Random rand = new Random();
            byte[] colorBytes = new byte[3];
            rand.NextBytes(colorBytes);

            return Color.FromArgb(255, colorBytes[0], colorBytes[1], colorBytes[2]);
        }

        private void QuantumSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            int quantumValue = (int)quantumSlider.Value;
            quantumValueText.Text = $"Quantum seleccionado: {quantumValue}";
        }

        private void DrawProcess(Process process, Color color)
        {
            int width = 50;
            int height = 30;

            Rectangle rectangle = new Rectangle
            {
                Width = width,
                Height = height,
                Fill = new SolidColorBrush(color)
            };

            // Posición y tamaño del proceso en el gráfico de CPU
            double left = currentProcessIndex * (width + 10);
            double top = 0;
            Canvas.SetLeft(rectangle, left);
            Canvas.SetTop(rectangle, top);

            cpuCanvas.Children.Add(rectangle);
            currentProcessIndex++;
        }


    }
}

public class Process
{
    public string Name { get; set; }
    public int Resources { get; set; }
    public int Quantum { get; set; }
    public Color Color { get; set; } // Propiedad para el color

    public Process(string name, int resources, int quantum, Color color)
    {
        Name = name;
        Resources = resources;
        Quantum = quantum;
        Color = color; // Asignar el color
    }
}
