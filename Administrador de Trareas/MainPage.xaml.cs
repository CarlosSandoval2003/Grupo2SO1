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
        private double currentVerticalPosition = 0;
        private Dictionary<Process, double> verticalPositions = new Dictionary<Process, double>();
        int filas = 1;
        int indiceDeseado = 0;
        int indiceDeseado1 = 0;
        int indiceDeseado2 = 0;
        int indiceDeseado3 = 0;
        int indiceDeseado4 = 0;
        int indiceDeseado5 = 0;
        int indiceDeseado6 = 0;
        int indiceDeseado7 = 0;
        int indiceDeseado8 = 0;
        int change = 0;





        public MainPage()
        {
            this.InitializeComponent();
            // Inicializar el diccionario de posiciones verticales
            foreach (var process in processes)
            {
                verticalPositions[process] = 0; // Iniciar en la parte superior
            }
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
            int quantumValue = int.Parse(quantumValueText.Text); // Obtener el valor del quantum

            // Inicializar una lista para llevar un seguimiento de las celdas registradas en el Canvas
            List<Rectangle> rectangles = new List<Rectangle>();

            // Crear una función asincrónica para procesar los procesos
            async Task ProcessProcessesAsync()
            {
                // Recorrer la lista de procesos en un ciclo infinito (Round Robin)
                while (true)
                {
                    foreach (var process in processes.ToList()) // Usar ToList() para evitar modificaciones concurrentes
                    {

                        // Repetir el ciclo de resta de 10 recursos según el quantum seleccionado
                        for (int i = 0; i < quantumValue && process.Resources > 0; i++) // Verificar si quedan recursos
                        {
                            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                            {
                                // Crear un rectángulo con el color del proceso
                                var rect = new Rectangle
                                {
                                    Width = 100,
                                    Height = 40,
                                    Fill = new SolidColorBrush(process.Color),
                                };

                                // Agregar el rectángulo a la lista y al Canvas
                                rectangles.Add(rect);
                                canvas.Children.Add(rect);

                                // Posicionar el rectángulo en el Canvas (al principio)
                                Canvas.SetTop(rect, 0); // Posición fija en la parte superior
                                Canvas.SetLeft(rect, 0);


                                // Reducir 10 recursos del proceso en cada ciclo
                                process.Resources -= 10;

                                // Mover todas las celdas registradas (incluyendo el rectángulo en blanco) a la derecha
                                foreach (var existingRect in rectangles)
                                {
                                    Canvas.SetLeft(existingRect, Canvas.GetLeft(existingRect) + rect.Width);
                                }

                                change++;

                                if (change >= 15)
                                {
                                    var actualRect = rectangles[indiceDeseado];
                                    Canvas.SetTop(actualRect, 80); // Posición fija en la parte superior
                                    Canvas.SetLeft(actualRect, 100);
                                    indiceDeseado++;
                                }

                                if (change >= 29)
                                {
                                    var actualRect = rectangles[indiceDeseado1];
                                    Canvas.SetTop(actualRect, 160); // Posición fija en la parte superior
                                    Canvas.SetLeft(actualRect, 100);
                                    indiceDeseado1++;
                                }

                                if (change >= 43)
                                {
                                    var actualRect = rectangles[indiceDeseado2];
                                    Canvas.SetTop(actualRect, 240); // Posición fija en la parte superior
                                    Canvas.SetLeft(actualRect, 100);
                                    indiceDeseado2++;
                                }

                                if (change >= 57)
                                {
                                    var actualRect = rectangles[indiceDeseado3];
                                    Canvas.SetTop(actualRect, 320); // Posición fija en la parte superior
                                    Canvas.SetLeft(actualRect, 100);
                                    indiceDeseado3++;
                                }

                                if (change >= 71)
                                {
                                    var actualRect = rectangles[indiceDeseado4];
                                    Canvas.SetTop(actualRect, 400); // Posición fija en la parte superior
                                    Canvas.SetLeft(actualRect, 100);
                                    indiceDeseado4++;
                                }

                                if (change >= 85)
                                {
                                    var actualRect = rectangles[indiceDeseado5];
                                    Canvas.SetTop(actualRect, 480); // Posición fija en la parte superior
                                    Canvas.SetLeft(actualRect, 100);
                                    indiceDeseado5++;
                                }

                                if (change >= 99)
                                {
                                    var actualRect = rectangles[indiceDeseado6];
                                    Canvas.SetTop(actualRect, 560); // Posición fija en la parte superior
                                    Canvas.SetLeft(actualRect, 100);
                                    indiceDeseado6++;
                                }

                                if (change >= 113)
                                {
                                    var actualRect = rectangles[indiceDeseado7];
                                    Canvas.SetTop(actualRect, 640); // Posición fija en la parte superior
                                    Canvas.SetLeft(actualRect, 100);
                                    indiceDeseado7++;
                                }

                                if (change >= 128)
                                {
                                    var actualRect = rectangles[indiceDeseado8];
                                    Canvas.SetTop(actualRect, 720); // Posición fija en la parte superior
                                    Canvas.SetLeft(actualRect, 100);
                                    indiceDeseado8++;
                                }
                            });

                            // Verificar si el proceso se ha completado o quedó en negativo
                            if (process.Resources <= 0)
                            {
                                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                                {
                                    // Agregar un rectángulo en blanco (color blanco) por cada repetición pendiente
                                    for (int j = i + 1; j < quantumValue; j++)
                                    {
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

                                    }

                                    // Quitar el proceso completado de la lista y el rectángulo del Canvas
                                    processes.Remove(process);
                                });

                                // Esperar 500 milisegundos (0.5 segundos)
                                await Task.Delay(500);
                            }
                            else
                            {
                                // Esperar 500 milisegundos (0.5 segundos) antes de repetir el ciclo
                                await Task.Delay(500);
                            }
                        }
                    }
                }
            }

            // Ejecutar la función asincrónica en segundo plano
            await Task.Run(() => ProcessProcessesAsync());
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


    }
}

public class Process
{
    public string Name { get; set; }
    public int Resources { get; set; }
    public int Quantum { get; set; }
    public Color Color { get; set; }
    public int Cambio { get; set; }
    public double VerticalPosition { get; set; }

    public Process(string name, int resources, int quantum, Color color)
    {
        Name = name;
        Resources = resources;
        Quantum = quantum;
        Color = color;
        Cambio = 0;
        VerticalPosition = 0;
    }
}

