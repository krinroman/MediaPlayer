   M S B A M L   `   `   `  ���� I  EWpfMediaPlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:WpfMediaPlayerWpfMediaPlayer  A%clr-namespace:WpfMediaPlayer.ControlsWpfMediaPlayer.Controls      WpfMediaPlayer.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     .localclr-namespace:WpfMediaPlayer     :controls%clr-namespace:WpfMediaPlayer.Controls     -     �Name$  MainWindowPlayer�� � Title$ Media Player�� � SizeToContent$ Height=�$	��800��$��Stretch=�$��Stretch=� � Icon$+ %Images/windows_media_player_25521.ico��.��� -    �Name$ MainGrid��$��Stretch=� � RowDefinitions �� $��20 ��� �� $��50 �.+�o� $	��Top=�.@�m� $��Файл��.@�m� -   $��Открыть файл��m� -   $1��+Открыть папку с музыкой���� -    ��Name$ listView��$��1��$y�Hidden=�$��Center=�r� -    r�Name$ mediaElement��$��1��$	��Top=�$y�Hidden=� r� LoadedBehavior$ Manual=�	 r� UnloadedBehavior$	 Manual=�$��Center=�� $��2��$��Bottom=� �� $
��Auto ��� $
��Auto �
 � ColumnDefinitions
 �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� -    ��Name$ NameMediaFile��$��0��$��8��$
��Auto��$��Center=��� -    ��Name$ 
BackButton��$��0��$��1��$��5q�{�.  .���� $��Images/previoustrack.png��$��20��$��20��-   'WpfMediaPlayer.Controls.PlayPauseButton  -	     Name$ 	playPause��-   'WpfMediaPlayer.Controls.PlayPauseButton   Click$ PlayPause_OnClick��$��1��$��1��$��Center=�$��Center=�   
PauseShape   	PlayShape# [    	PlayShape   PausedShape# [ $��5q� �� 	IsDefault .  �� -
   $��2��$��1��  StopRectangle#��[ $��Center=�$��Center=�$��5q��� -   $ 
NextButton��$��3��$��1��$��5q�{�.  .���� $ ��Images/nexttrackbutton.png��$��20��$��20��*   $WpfMediaPlayer.Controls.PlayTimeline  -     Name$ playTimeline��$��4��$��1��$��Center=�#  WpfMediaPlayer.PlayTimelineEx   MediaElement ��  �� ElementName$ mediaElement���� -    ��Name$ Volume��$��5��$��1��$��Stretch=�$��Center=�$��Center=�.��Громкость:�� -    ��Name$ volumeSlider��$��6��$��1��$	��100�� � Value$	 0.5Q� � Maximum$ 1Q�$��Center=� �� IsMoveToPointEnabled . �� -   $ VolumePercent��$��7��$��1��$��Center=�$��30��50%