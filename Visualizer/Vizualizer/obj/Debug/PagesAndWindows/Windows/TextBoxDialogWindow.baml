   M S B A M L   `   `   `  ����E  AVisualizer, Version=1.3.1.2, Culture=neutral, PublicKeyToken=nullW0clr-namespace:Visualizer.PagesAndWindows.Windows"Visualizer.PagesAndWindows.Windows  5   	   4    .Visualizer.PagesAndWindows.TextBoxDialogWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Blocal0clr-namespace:Visualizer.PagesAndWindows.Windows     5   	   -   5          � WindowStyle$
  None=�5   H    � Topmost .  � Title$ CustomMessageBox��5   	   $	��300��5	   	   $	��250��5
   	    � 
ResizeMode$ NoResize=�5   	    � AllowsTransparency . 
������  � Left$	 350��5   	   
 � Top$	 300��6   .��5      �� -   5        $��windowFrame��5   [      MainBackGroundColor#���   6   $
��Auto��6@   $
��Auto��6M     MainBorderColor#���  5      $��5q�5       �� CornerRadius$ 15��5      ._�5   
   � -   5        $��MainDialogGrid��5       � RowDefinitions 5      �� 5      $	��.5* �6    �� 5      �� 5      $	��.5* �6    .+�5      �� -   5        $��MessageTextBlock��5      $��Center=�5      $��Center=�5      $
��Wrap=�5      ��5      �� 	 �� Path$	 FontSize�
 �� Mode$
 TwoWay=��� 5      -   5        $��MainTextBox��5       $��15 30 15 30q�5!      $��1��5#      ��5"      �� $	 FontSize�$
 TwoWay=�5#      �� 5$      -   5        $��2��5$      $��15q�6"   $
��Ок��6.   6;   5%      5&      5'      