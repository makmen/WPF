   M S B A M L   `   `   `  ����|  xSystem.Windows.Controls.DataVisualization.Toolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35�{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit2System.Windows.Controls.DataVisualization.Charting  5   	    ExchangeClient   ExchangeClient.RunWindow   �vc{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  	RunWindow��5   	   $	��400��6   $	��800��6(   
���   �.��5      �  � ColumnDefinitions 5	   
   �� 5
      $��* �6   6+   �� 5      $��3* �6   6,    � RowDefinitions 5   
   �� 5      $
��auto �6   6,   �� 5      $��3* �6   6*   �� 5      $
��auto �6   6,   .+�5   
   �� -   5        $��	lbWelcome��5      $��2��6!   $��5q�65   6B   �� 5   
   -   5        $��	lstEvents��5   *   $��1��6   $��5q�6   6=   �� 5   
   $��2��6    �� Orientation$ 
Horizontal=�6"   $��5q�6;   .+�5      �� -   5        $��btnAdd��5      $��5q�6#   $��17 10q�6.   .��6S   Add6X   �� 5      -   5        $��btnEdit��5      $��5q�6%   $��16 10q�60   .��6V   Edit6\   �� 5      -   5        $��	btnDelete��5      $��5q�6'   $��10 10q�62   .��6Z   Delete6b   5      � 5   
   $��1��6   $��1��6   $��5q�6,    5      �� 5      $
��auto �6    60   �� 5      $��* �6    6-    5      �� 5      $��* �6#   6/   �� 5       $��* �6#   6/   �� 5!      $��* �6#   6/   .+�5#      �� $��Right=�6   $��0 12 5 0q�60   .��6B    Обновлять через:6U   �� 5$      -   5        $��	cmbUpdate��5$      $��1��6(   $
��0 10q�68   @�5%      �� 5&       �� 
IsSelected . .��65   1 секунду6@   �� 5'      .��6#   5 секунд6-   �� 5(      .��6#   10 секунд6.   5*      �� 5+      -   5        $��btnRes��5+      $��2��6#   $
��2 10q�63   6X   >   8System.Windows.Controls.DataVisualization.Charting.Chart  5.      -	   5          Name$ 
AreaSeries��5.         Title$ Series��6+   $��1��5/      $��3��6$      Series 51      C   =System.Windows.Controls.DataVisualization.Charting.AreaSeries  H   BSystem.Windows.Controls.DataVisualization.Charting.DataPointSeries   DependentValuePath$ Value��52      	   IndependentValuePath$		 Key��53      
   IsSelectionEnabled
 .    ItemsSource 54      �� 55      57      59      5;      5<      