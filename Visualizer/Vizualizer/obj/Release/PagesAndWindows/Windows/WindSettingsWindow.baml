   M S B A M L   `   `   `  ���� E  AVisualizer, Version=1.3.1.2, Culture=neutral, PublicKeyToken=nullW0clr-namespace:Visualizer.PagesAndWindows.Windows"Visualizer.PagesAndWindows.Windows  3    -Visualizer.PagesAndWindows.WindSettingsWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     Blocal0clr-namespace:Visualizer.PagesAndWindows.Windows     -     � Title$-  'Редактирование ветра��$	��450��$	��450�� � Topmost .  � WindowStyle$
 None=� � AllowsTransparency . 
������ .���� -   $��windowFrame��   MainBackGroundColor#���   $
��Auto��$
��Auto��  MainBorderColor#���  $��5q� �� CornerRadius$ 15��._��  � RowDefinitions �� $
��auto ��� .+��� $-��'Редактирование ветра��$��Center=�$
��Left=�$��15 2 2 2q�����  �� Path$ ItemFontSize� �� Mode$ TwoWay=��� -   $��25��$��Right=�$��5q�$��1q�$��X��$��15(����� *ActualHeight+ �� RelativeSource �� *Self+� $��1�� �� �� $
��.25* �.+�� .+��  �� $
��.75* ��� $��10 ��� �� $��10 ��� �� $��10 ��� �� $��10 ��� �� $��10 ��� �� $��10 ��� .+��� $#��Настройка ветра��$��Center=�$��Center=�� $��2��	 � ColumnDefinitions	 �� $	��.5* ��� .+�(� .���� $-��'Присутсвует ли ветер:���� -   $��ShouldShowParticlesCB��$��1��$
��Left=�$��5q�

 � Tag$
 
NonDisable��� $��4��	 �� $
��.75* ��� �� $	��.5* �.+�(� .���� $5��/Максиамльное время ветра:���� -   $��MaxWindTimeTB��$��1��$��2��$��10q�� $��6��	 �� $
��.75* ��� �� $	��.5* �.+�(� .���� $3��-Минимальное время ветра:���� -   $��MinWindTimeTB��$��1��$��2��$��10q�� $��8��	 �� $
��.75* ��� �� $	��.5* �.+�(� .���� $3��-Максиамльная сила ветра:���� -   $��MaxWindForceTB��$��1��$��2��$��10q�� $��10��	 �� $
��.75* ��� �� $	��.5* �.+�(� .���� $1��+Минимальная сила ветра:���� -   $��MinWindForceTB��$��1��$��2��$��10q�� $��12��	 �� $
��.75* ��� �� $	��.5* �.+�(� .���� $2��,Шанс на появление ветра:���� -	   $��ChanceForWindTB��$��1��$��2��$��10q��� -
   $��1��$��15q�$��Сохранить��