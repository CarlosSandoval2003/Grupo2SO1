using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Administrador
{
    public partial class MainWindow : Window
    {
        private List<ProcessInfo> processes = new List<ProcessInfo>();
        private List<int> selectedProcessIds = new List<int>();
        private PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");



        public MainWindow()
        {
            InitializeComponent();

            // Configurar el DataGrid
            processDataGrid.AutoGenerateColumns = true;
            processDataGrid.SelectionMode = DataGridSelectionMode.Extended;

            // Actualizar la lista de procesos al inicio
            UpdateProcessList();

            // Iniciar un temporizador para actualizar la lista de procesos cada segundo
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += (s, e) => UpdateProcessList();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Start();
        }

        private void UpdateProcessList()
        {
            // Obtener la lista de procesos actual
            var currentProcesses = Process.GetProcesses();

            // Guardar los IDs de los procesos seleccionados
            selectedProcessIds.Clear();
            foreach (var selectedItem in processDataGrid.SelectedItems)
            {
                var selectedProcess = selectedItem as ProcessInfo;
                if (selectedProcess != null)
                {
                    selectedProcessIds.Add(selectedProcess.PID);
                }
            }

            // Eliminar procesos que ya no existen
            var processesToRemove = processes.Where(p => currentProcesses.All(cp => cp.Id != p.PID)).ToList();
            foreach (var processToRemove in processesToRemove)
            {
                processes.Remove(processToRemove);
            }

            // Actualizar el uso de CPU para los procesos existentes
            foreach (var processInfo in processes)
            {
                try
                {
                    var process = Process.GetProcessById(processInfo.PID);
                    processInfo.CPU = cpuCounter.NextValue() / Environment.ProcessorCount;
                }
                catch (Exception)
                {
                    // Manejar errores al intentar acceder a la información de CPU
                }
            }

            // Agregar nuevos procesos
            var processesToAdd = currentProcesses.Where(cp => processes.All(p => p.PID != cp.Id)).ToList();
            foreach (var processToAdd in processesToAdd)
            {
                try
                {
                    processes.Add(new ProcessInfo
                    {
                        Nombre = processToAdd.ProcessName,
                        PID = processToAdd.Id,
                        CPU = cpuCounter.NextValue() / Environment.ProcessorCount, // Inicializar en 0
                        Memoria = processToAdd.PrivateMemorySize64 / (1024 * 1024)
                    });
                }
                catch (Exception)
                {
                    // Ignorar los procesos a los que no se puede acceder
                }
            }

            // Actualizar el DataGrid
            processDataGrid.ItemsSource = null;
            processDataGrid.ItemsSource = processes;

            // Restaurar la selección de elementos
            foreach (var process in processes)
            {
                if (selectedProcessIds.Contains(process.PID))
                {
                    processDataGrid.SelectedItems.Add(process);
                }
            }
        }


        private void TerminateSelectedProcesses(IEnumerable<int> processIds)
        {
        }

        private void TerminateMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TerminateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private IEnumerable<int> GetSelectedProcessIds()
        {
            var selectedProcesses = processDataGrid.SelectedItems.Cast<ProcessInfo>();
            return selectedProcesses.Select(p => p.PID);
        }
    }

    public class ProcessInfo
    {
        public string Nombre { get; set; }
        public int PID { get; set; }
        public double CPU { get; set; }
        public long Memoria { get; set; }

    }

}

