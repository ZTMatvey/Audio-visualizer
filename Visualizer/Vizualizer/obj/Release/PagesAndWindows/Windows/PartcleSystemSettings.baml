   M S B A M L   `   `   `  ���� E  AVisualizer, Version=1.3.1.2, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Visualizer.PagesAndWindowsVisualizer.PagesAndWindows  6    0Visualizer.PagesAndWindows.PartcleSystemSettings   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     :local(clr-namespace:Visualizer.PagesAndWindows     -     � Title$4  .Настройка системы частиц��$	��800��$	��450�� � WindowStyle$
 None=� � Topmost .  � AllowsTransparency . 
������ .���� -   $��windowFrame��   MainBackGroundColor#���   $
��Auto��$
��Auto��  MainBorderColor#���  $��5q� �� CornerRadius$ 15��._��  � RowDefinitions �� $
��auto ��� .+��� $4��.Настройка системы частиц��$��Center=�$
��Left=�$��15 2 2 2q�����  �� Path$ ItemFontSize� �� Mode$ TwoWay=��� -   $��25��$��Right=�$��5q�$��1q�$��X��$��15(����� *ActualHeight+ �� RelativeSource �� *Self+� $��1�� �� $
��.75* ��� $
��.35* ��� $
��.15* ��� $
��auto ��� $
��.15* ��� $
��auto ��� $
��.25* �.+�� .+��  �� �� $��10 ��� �� $��10 ��� �� $��10 ��� �� $��10 ��� �� $��10 ��� �� $��10 ��� .+��� $%��Настройка частиц��$��Center=�$��Center=����� $ FontSize�$ TwoWay=�� $��2��	 � ColumnDefinitions	 �� $	��.5* ��� .+��� $/��)Показывать ли частицы:������ $ FontSize�$ TwoWay=��� -   $��ShouldShowParticlesCB��$��1��$
��Left=�$��5q�� $��4��	 �� $
��.75* ��� �� $	��.5* �.+��� $7��1Максиамльное число частиц:������ $ FontSize�$ TwoWay=��� -   $��MaxParticlesCountTB��$��1��$��2��$��10q����� $ ItemFontSize�$ TwoWay=�� $��6��	 �� $
��.75* ��� �� $	��.5* �.+��� $7��1Минимальный размер частиц:������ $ FontSize�$ TwoWay=��� -   $��MinParticlesSizeTB��$��1��$��2��$��10q����� $ ItemFontSize�$ TwoWay=�� $��8��	 �� $
��.75* ��� �� $	��.5* �.+��� $9��3Максиамльный размер частиц:������ $ FontSize�$ TwoWay=��� -   $��MaxParticlesSizeTB��$��1��$��2��$��10q����� $ ItemFontSize�$ TwoWay=�� $��10��	 �� $
��.75* ��� �� $	��.5* �.+��� $;��5Минимальная скорость частиц:������ $ FontSize�$ TwoWay=��� -   $��MinParticlesSpeedTB��$��1��$��2��$��10q����� $ ItemFontSize�$ TwoWay=�� $��12��	 �� $
��.75* ��� �� $	��.5* �.+��� $=��7Максиамльная скорость частиц:������ $ FontSize�$ TwoWay=��� -	   $��MaxParticlesSpeedTB��$��1��$��2��$��10q����� $ ItemFontSize�$ TwoWay=�� $��1�� �� $	��.5* ��� �� $
��auto ��� �� $
��auto �.+��� $	��Top=�
��������� $��2��$��Тип частиц:��$��Center=�$��Center=����� $ FontSize�$ TwoWay=�� $��1��	 �� �� $	��.5* �.+��� -
   $��TypeOfParticlesCB��$��15q����� $ ItemFontSize�$ TwoWay=��� -   $��ParticleEditButton��$��1��$��15q�$ ��Редактировать���� $��2��$	��Top=�
��������� -   $��3��$;��5Открыть окно создания частиц��$��15q����� $ ItemFontSize�$ TwoWay=��� $��4��$	��Top=�
��������� -   $��1��$E��?Открыть окно редактирования ветра��$��2��$��15q����� $ ItemFontSize�$ TwoWay=��� $��3��$	��Top=�
��������� -   $��1��$K��EОткрыть окно редактирования свечения��$��4��$��15q����� $ ItemFontSize�$ TwoWay=��� $��5��$	��Top=�
��������� -   $��1��$��Сохранить��$��6��$��15q����� $ ItemFontSize�$ TwoWay=�