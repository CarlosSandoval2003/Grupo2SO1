   M S B A M L   `   `   `  ����  AdministradorDeTareasNuevo    Administrador.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Administrador de Tareas��5   	   $	��500��6)   $	��800��66   .��5      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��processDataGrid��5      $��10q�6*      AutoGenerateColumns$
 True��66      SelectionMode$ Single=�5         SelectionUnit$ FullRow=�6&      
IsReadOnly$
 True��6>      RowStyle 5	      �� 5
       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6    �� Triggers 5      P� 5       P� Property �@J�IsMouseOver	 P� Value$
	 True��69   . �5      �� 
 �� Property
 � �� �� Value �#E0E0E06;   5      5       � ContextMenu 5      �� 5      .@�5      m� -   5        $��Terminar Proceso��5      69   5      5      �� 5   
   -   5        $��terminateButton��5   ,   $��Terminar Proceso��6   $��Right=�6C   $��Bottom=�6_   $	��120��6z   $��30��6�   $��	0,0,10,10q�6�   6�   5      5      