   M S B A M L   `   `   `  ���� E  AVisualizer, Version=1.3.1.2, Culture=neutral, PublicKeyToken=nullK*clr-namespace:Visualizer.Resources.SlidersVisualizer.Resources.Sliders  W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   RepeatButtonTransparent  �� 
TargetType �� Property �� Value �� 
TargetType  ScrollBarThumbHorizontal ��Name �� RadiusX �� RadiusY  ScrollBar.Static.Thumb J� SnapsToDevicePixels �� Triggers	 P� Property
 P� Value �� 
TargetName  ScrollBar.MouseOver.Thumb  ScrollBar.Pressed.Thumb ! SliderThumb.Static.Foreground �� Color $  SliderThumb.MouseOver.Background   SliderThumb.MouseOver.Border " SliderThumb.Pressed.Background 	 SliderThumb.Pressed.Border #
 SliderThumb.Disabled.Background  SliderThumb.Disabled.Border ! SliderThumb.Static.Background  SliderThumb.Static.Border  SliderThumbHorizontalTop � UseLayoutRounding �� Stretch  SliderThumbHorizontalBottom  SliderThumb.Track.Border   SliderThumb.Track.Background   SliderThumbHorizontalDefault  SliderHorizontal ��Name � RowDefinitions m�Name m� Fill m� 	Placement ]�Name ]� DecreaseRepeatButton �� DecreaseLarge ]� IncreaseRepeatButton �� IncreaseLarge ]� Thumb n�Name � OverridesDefaultStyle  SliderThumbVerticalLeft  SliderThumbVerticalRight  SliderThumbVerticalDefault  SliderVertical � ColumnDefinitions  GreenSliderStyle �� Triggers�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     4local*clr-namespace:Visualizer.Resources.Sliders  %l  &	        &	 �     7  7  7  &	 �    &	 �    &	 
    &	     &		 0    &	
 C    &	 X    &	 m    &	 �    &	 �    7  7  7  7  7  7 	 7 
 7  &	 A    7  7  7  7  7  7 	 7 
 7  &	 �    &	     &	     7  7  7  7  7  7 	 7 
 7  &	 �    7  7  7  7  7   7   7  7  7  &	 �    7  7  7  7  7  7 	 7 
 7  &	 m    7  7  7  7  7  7 	 7 
 7  &	     7  7  7  7  7  7 	 7 
 7  &	 �    7  7  7   7   7  7  7  &	 �    7  7  7  �� "  ��.���  �@�OverridesDefaultStyle . ��  � ��
 ���� ��  � |� .  ��  � �� .  ��  � �� �� " ��.R��� #��z��#��z��#��z���� "  n�.���  �@�OverridesDefaultStyle . ��  � �� .  ��  � �� �� " n�.R��� $ 	rectangle��$ 5��$ 5��8��  #��z�� . #��z�� P� 	 �@J�IsMouseOver
 . . ��� $ 	rectangle��8   � ��P� 	 �@n�
IsDragging
 . . ��� $ 	rectangle��8   � ���� $ #ff2617���� $ #00e061���� $ #00a648���� $ #0d8f45���� $ #00632b���� $ 	#FFF0F0F0���� $ 	#FFD9D9D9���� $ #69ffaa���� $ #37a667���� " n�.R�� $��Center=� . $��Center=�.+��� $
 grip��$��20��$��10��$ 3��$ 3��8��  $
 Fill=� . 8�� $��1�� . $��Center=� P� 	 �@J�IsMouseOver
 . . ��� $
 grip��8   � ���� $
 grip��8   � ��P� 	 �@n�
IsDragging
 . . ��� $
 grip��8   � ���� $
 grip��8   � ��P� 	 � {�
 .  . ��� $
 grip��8   � ���� $
 grip��8   � ���� " n�.R�� $��Center=� . $��Center=�.+��� $
 grip��$��20��$��10��$ 3��$ 3��8��  $
 Fill=� . 8�� $��1�� . $��Center=� P� 	 �@J�IsMouseOver
 . . ��� $
 grip��8   � ���� $
 grip��8   � ��P� 	 �@n�
IsDragging
 . . ��� $
 grip��8   � ���� $
 grip��8   � ��P� 	 � {�
 .  . ��� $
 grip��8   � ���� $
 grip��8   � ���� $ 	#FFD6D6D6���� $ 	#FFE7EAEA���� " n�.R�� $��Center=� . $��Center=�.+��� $
 grip��$��17��$��10��$ 3��$ 3��8��  $
 Fill=� . 8�� $��1�� . $��Center=� P� 	 �@J�IsMouseOver
 . . ��� $
 grip��8   � ���� $
 grip��8   � ��P� 	 �@n�
IsDragging
 . . ��� $
 grip��8   � ���� $
 grip��8   � ��P� 	 � {�
 .  . ��� $
 grip��8   � ���� $
 grip��8   � ���� " ��.R��� $ border��#��z��#��z��#��z�� . ._��  �� $
��Auto ��� $
��Auto �#��z���� $
��Auto �.+�m� $ TopTick��# z��$��4��$��0,0,0,2q�$	 Top=�$��0��$y�	Collapsed=�m� $ 
BottomTick��# z��$��4��$��0,2,0,0q�$ Bottom=�$��2��$y�	Collapsed=��� $ TrackBackground��8��  $��1q�8�� $	��4.0��$	��5,0q�$��1��$��center=�._��� $��-6,-1q�.+��� $ PART_SelectionRange��#��� ��$	��4.0��$y�Hidden=�]� $ 
PART_Track��$��1�� �� #��Z 8�� $��5��8�� 8��  �� #��Z 8��  n� $ Thumb��|�.  $��18�� . 8�� $��Center=�$��11�� P� 	 �@��TickPlacement$
 TopLeft=�. ��� $ TopTick�� � y�$ Visible=��� $ Thumb��8   � ���� $ TrackBackground�� � ��$ 5,2,5,0q�P� 	 �@��TickPlacement$
 BottomRight=�. ��� $ 
BottomTick�� � y�$ Visible=��� $ Thumb��8   � ���� $ TrackBackground�� � ��$ 5,0,5,2q�P� 	 �@��TickPlacement$

 Both=�. ��� $ TopTick�� � y�$ Visible=��� $ 
BottomTick�� � y�$ Visible=�P� 	 �@��IsSelectionRangeEnabled
 . . ��� $ PART_SelectionRange�� � y�$ Visible=�P� 	 �@J�IsKeyboardFocused
 . . ��� $ Thumb�� � ��
 ��  ��� " n�.R�� $��Center=� . $��Center=�.+��� $
 grip��$��20��$��10��$ 3��$ 3��8��  $
 Fill=� . 8�� $��1�� . $��Center=� P� 	 �@J�IsMouseOver
 . . ��� $
 grip��8   � ���� $
 grip��8   � ��P� 	 �@n�
IsDragging
 . . ��� $
 grip��8   � ���� $
 grip��8   � ��P� 	 � {�
 .  . ��� $
 grip��8   � ���� $
 grip��8   � ���� " n�.R�� $��Center=� . $��Center=�.+��� $
 grip��$��20��$��10��$ 3��$ 3��8��  $
 Fill=� . 8�� $��1�� . $��Center=� P� 	 �@J�IsMouseOver
 . . ��� $
 grip��8   � ���� $
 grip��8   � ��P� 	 �@n�
IsDragging
 . . ��� $
 grip��8   � ���� $
 grip��8   � ��P� 	 � {�
 .  . ��� $
 grip��8   � ���� $
 grip��8   � ���� " n�.R�� $��Center=� . $��Center=�.+��� $
 grip��$��20��$��10��$ 3��$ 3��8��  $
 Fill=� . 8�� $��1�� . $��Center=� P� 	 �@J�IsMouseOver
 . . ��� $
 grip��8   � ���� $
 grip��8   � ��P� 	 �@n�
IsDragging
 . . ��� $
 grip��8   � ���� $
 grip��8   � ��P� 	 � {�
 .  . ��� $
 grip��8   � ���� $
 grip��8   � ���� " ��.R��� $ border��#��z��#��z��#��z�� . ._��  �� $
��Auto ��� #��z��$
��Auto ��� $
��Auto �.+�m� $ TopTick��$��0��# z��$��0,0,2,0q�$
 Left=�$y�	Collapsed=�$��4��m� $ 
BottomTick��$��2��# z��$��2,0,0,0q�$ Right=�$y�	Collapsed=�$��4���� $ TrackBackground��8��  $��1q�8�� $��1��$��center=�$	��0,5q�$	��4.0��._��� $��-1,-6q�.+��� $ PART_SelectionRange��#��� ��$y�Hidden=�$	��4.0��]� $ 
PART_Track��$��1�� �� #��Z 8��  �� #��Z 8��  n� $ Thumb��|�.  $��11�� . 8�� $	��Top=�$��18�� P� 	 �@��TickPlacement$
 TopLeft=�. ��� $ TopTick�� � y�$ Visible=��� $ Thumb��8   � ���� $ TrackBackground�� � ��$ 2,5,0,5q�P� 	 �@��TickPlacement$
 BottomRight=�. ��� $ 
BottomTick�� � y�$ Visible=��� $ Thumb��8   � ���� $ TrackBackground�� � ��$ 0,5,2,5q�P� 	 �@��TickPlacement$

 Both=�. ��� $ TopTick�� � y�$ Visible=��� $ 
BottomTick�� � y�$ Visible=�P� 	 �@��IsSelectionRangeEnabled
 . . ��� $ PART_SelectionRange�� � y�$ Visible=�P� 	 �@J�IsKeyboardFocused
 . . ��� $ Thumb�� � ��
 ��  ��� "  ��.���  �@��IsPressAndHoldEnabled .  ��  � ��
 ���� ��  � ��
 ���� ��  � ��8   ��  � ��8   P� 	 �@��Orientation$
 Vertical=�. ���  � ��8  