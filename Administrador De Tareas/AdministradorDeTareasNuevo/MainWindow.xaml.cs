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

