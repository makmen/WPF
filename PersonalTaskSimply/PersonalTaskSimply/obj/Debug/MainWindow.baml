   M S B A M L   `   `   `  ����|  xSystem.Windows.Controls.DataVisualization.Toolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35�{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit2System.Windows.Controls.DataVisualization.Charting  5       PersonalTaskSimply#   PersonalTaskSimply.MainWindow   �chartingToolkit{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  
MainWindow��5   	   $	��780��6   $	��525��6)   ���	#FF4B6FBB.��5      �  � ColumnDefinitions 5	   
   �� 5
      $
��auto �6   6.   �� 5      $��* �6   6+    � RowDefinitions 5   
   �� 5      $
��auto �6   6,   �� 5      $��4* �6   6*   �� 5      $��* �6   6)   �� 5      $��5* �6   6*   �� 5      $��4* �6   6*   �� 5      $��* �6   6)   .+�5   
   �� .��6   Personal information6&   �� 5   
   $��1��6   .��6    Additional information68   �� 5   
   -   5        $��	lbPersons��5      $��1��6#   $��5q�60   6k   �� 5   
   -   5        $��	tbPersons��5      $��1��6$   $��1��61   $��5q�6A    �� 
IsReadOnly . 6`   �� 5   
   $��2��6    �� Orientation$ 
Horizontal=�6"   $��2��6;   $��5q�6O   .+�5      �� -   5        $��btnAdd��5      $	��5 7q�6#   $
��30 0q�60   .��6T   Add6Y   �� 5      -   5        $��btnEdit��5      $	��5 7q�6%   $
��30 0q�62   .��6W   Edit6]   �� 5      -   5        $��	btnDelete��5      $	��5 7q�6'   $
��30 0q�64   .��6[   Delete6c   5      �� 5   
   -   5        $��lbRate��5      $��3��6      	LabelRate#��[  6+   $��Center=�5      $��Center=�6-   6J   >   8System.Windows.Controls.DataVisualization.Charting.Chart  5    
   -   5          Name$ ColumnSeries��5!      $��3��5"      $��1��6   
���   �   Series. 5#      E   ?System.Windows.Controls.DataVisualization.Charting.ColumnSeries  H   BSystem.Windows.Controls.DataVisualization.Charting.DataPointSeries   DependentValuePath$ Value��5$         IndependentValuePath$	 Key��5%   
   	   IsSelectionEnabled	 . 
   ItemsSource
 5&      �� 5'      5(      �� 5*   
   -   5        $��lbTasks��5*      $��4��6!   $��5q�6.   6g   �� 5+   
   -	   5        $��tbTasks��5+      $��4��6"   $��1��6/   $��5q�6?    . 6^   �� 5,   
   $��5��6   $ 
Horizontal=�6"   $��2��6;   $��5q�6O   .+�5-      �� -
   5        $��
btnAddTask��5-      $	��5 7q�6'   $
��30 0q�64   .��6\   
Add Task6f   �� 5.      -   5        $��btnEditTask��5.      $	��5 7q�6)   $
��30 0q�66   .��6_   	Edit Task6j   �� 5/      -   5        $��btnDeleteTask��5/      $	��5 7q�6+   $
��30 0q�68   .��6c   Delete Task6p   50      51      52      