   M S B A M L   `   `   `  ���� E  AVisualizer, Version=1.3.1.2, Culture=neutral, PublicKeyToken=nullW0clr-namespace:Visualizer.PagesAndWindows.Windows"Visualizer.PagesAndWindows.Windows  4    .Visualizer.PagesAndWindows.TextBoxDialogWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     Blocal0clr-namespace:Visualizer.PagesAndWindows.Windows     -     � WindowStyle$
  None=� � Topmost .  � Title$ CustomMessageBox��$	��300��$	��250�� � 
ResizeMode$ NoResize=� � AllowsTransparency . 
������  � Left$	 350��
 � Top$	 300��.���� -   $��windowFrame��   MainBackGroundColor#���   $
��Auto��$
��Auto��  MainBorderColor#���  $��5q� �� CornerRadius$ 15��._�� -   $��MainDialogGrid�� � RowDefinitions �� $	��.5* ��� �� $	��.5* �.+��� -   $��MessageTextBlock��$��Center=�$��Center=�$
��Wrap=����� 	 �� Path$	 FontSize�
 �� Mode$
 TwoWay=��� -   $��MainTextBox��$��15 30 15 30q�$��1������ $	 FontSize�$
 TwoWay=��� -   $��2��$��15q�$
��Ок��