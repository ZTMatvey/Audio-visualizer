   M S B A M L   `   `   `  ����E  AVisualizer, Version=1.3.1.2, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Visualizer.PagesAndWindowsVisualizer.PagesAndWindows  5      Y1clr-namespace:Visualizer.PagesAndWindows.Controls#Visualizer.PagesAndWindows.Controls  5      -    'Visualizer.PagesAndWindows.MainSettings   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:Visualizer.PagesAndWindows     5      Fcontrols1clr-namespace:Visualizer.PagesAndWindows.Controls     5      .��5
      � -   5        $��MGrid��5
   $   
������   � ColumnDefinitions  5   
   �� 5      �� 5      �� 5       � RowDefinitions 5   
   �� 5      �� 5      $��25 �6   �� 5      �� 5      $��25 �6   �� 5      �� 5      $��25 �6   �� 5      �� 5      $��25 �6   �� 5      �� 5      $��50 �6   .+�5   
   �� $��Center=�5      $��0��5      $��Right=�5       $��Устройство:��5!      $
��Wrap=�5#      ��5"      ��  �� Path$ FontSize� �� Mode$ TwoWay=�5#      �� 5$   
   -   5        $��	DevicesCB��5'      $��15q�5%      $��1��5&      $��0��6   ��5(      �� $ ItemFontSize�$ TwoWay=��� 5+   
   $��	Тема:��5,      $��2��5.      $��Right=�5/      $��Center=�50      $
��Wrap=�51      ��5-      �� $ FontSize�$ TwoWay=�51      �� 52   
   -   5        $��ThemeComboBox��57      $��15q�54      $��1��55      $��2��56      ��53      �� $ ItemFontSize�$ TwoWay=�57      �� 59   
   $��4��5;      $��Right=�5<      $��Center=�5=      $
��Wrap=�5>      $ ��Размер текста:��5?      ��5:      �� $ FontSize�$ TwoWay=�5?      7   1Visualizer.PagesAndWindows.Controls.SliderControl  5A   
   -   5          Name$ FontSizeUserControl��5B         MinValue$ 5��5C         MaxValue$	 100��6   $��4��5D      $��1��6   �� 5F   
   $��6��5H      $��Right=�5I      $��Center=�5J      $
��Wrap=�5K      $&�� Размер элементов:��5L      ��5G      �� $ FontSize�$ TwoWay=�5L        5N   
   -   5        $ ItemFontSizeUserControl��5O      $ 5��5P      $	 100��6   $��6��5Q      $��1��6   �� 5S   
   -   5        $��8��5T      $��1��6   $��10q�5V      $1��+Настройки визуализации��5W      ��5U      �� $ ItemFontSize�$ TwoWay=�5X      5Y      5Z      