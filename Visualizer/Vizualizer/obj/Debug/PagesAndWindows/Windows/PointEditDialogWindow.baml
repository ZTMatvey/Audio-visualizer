   M S B A M L   `   `   `  ����E  AVisualizer, Version=1.3.1.2, Culture=neutral, PublicKeyToken=nullW0clr-namespace:Visualizer.PagesAndWindows.Windows"Visualizer.PagesAndWindows.Windows  5   	   6    0Visualizer.PagesAndWindows.PointEditDialogWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Blocal0clr-namespace:Visualizer.PagesAndWindows.Windows     5   	   -   5          � Title$  PointEditDialogWindow��5   	    � WindowStyle$
 None=�5	   	    � Topmost . $	��150��5   	   $	��300��5   	    � 
ResizeMode$ NoResize=�5   	    � AllowsTransparency . 
������  � Left$	 350��5   	   
 � Top$	 300��6   .��5      �� -   5        $��windowFrame��5   [      MainBackGroundColor#���   6   $
��Auto��6@   $
��Auto��6M     MainBorderColor#���  5      $��5q�5       �� CornerRadius$ 15��5      ._�5   
   � -   5        $��MainDialogGrid��5       � RowDefinitions 5      �� 5      �� 5      �� 5      $
��.75* �6    .+�5      � 	 � ColumnDefinitions	 5      �� 5      $
��auto �6'   �� 5      .+�5       �� $��X:��5!      $��Center=�5"      $��Center=�5#      $
��Wrap=�5$      $��10q�5%      ��5&      �� 
 �� Path$
 FontSize� �� Mode$ TwoWay=��� 5'      -   5        $��XTextBox��5)      $��1��5(      $��10q�5*      ��5,      �� $
 FontSize�$ TwoWay=�5-      � 5.      $��1��6   	 5/      �� 50      $
��auto �6'   �� 51      .+�53      �� -   5        $��MessageTextBlock��56      $��Y:��54      $��10q�55      $��Center=�57      $��Center=�58      $
��Wrap=�59      ��5:      �� $
 FontSize�$ TwoWay=��� 5;      -   5        $��YTextBox��5=      $��1��5<      $��10q�5>      ��5@      �� $
 FontSize�$ TwoWay=�5A      �� 5B      -   5        $��2��5B      $��5q�6"   $��Сохранить��6-   6A   5C      5D      5E      