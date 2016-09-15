# Gu.Wpf.Media
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE.md) 
[![NuGet](https://img.shields.io/nuget/v/Gu.Wpf.Media.svg)](https://www.nuget.org/packages/Gu.Wpf.Media/)
[![Build status](https://ci.appveyor.com/api/projects/status/eg96glf1osi8on6y/branch/master?svg=true)](https://ci.appveyor.com/project/JohanLarsson/gu-wpf-media/branch/master)

Wrapper for System.Windows.Controls.MediaElement.

# Contents
- [1. MediaElementWrapper](#1-mediaelementwrapper)
  - [1.1 Properties](#11-properties)
    - [1.1.1 State (`MediaState`)](#111-state--mediastate)
    - [1.1.2. Position (`Timespan?`)](#112-position--timespan)
    - [1.1.3. Length (`Timespan?`)](#113-length--timespan)
    - [1.1.4. CanPauseMedia (`bool?`)](#114-canpausemedia--bool)
    - [1.1.5. NaturalVideoHeight (`int?`)](#115-naturalvideoheight--int)
    - [1.1.6. NaturalVideoWidth (`int?`)](#116-naturalvideowidth--int)
    - [1.1.7. HasAudio (`bool?`)](#117-hasaudio--bool)
    - [1.1.8. HasVideo (`bool?`)](#118-hasvideo--bool)
    - [1.1.8. HasMedia (`bool`)](#118-hasmedia--bool)
    - [1.1.9. SpeedRatio (`double`)](#119-speedratio--double)
    - [1.1.10. IsBuffering (`bool`)](#1110-isbuffering--bool)
    - [1.1.11. DownloadProgress (`double`)](#1111-downloadprogress--double)
    - [1.1.12. BufferingProgress (`double`)](#1112-bufferingprogress--double)
    - [1.1.13. VolumeIncrement (`double`)](#1113-volumeincrement--double)
    - [1.1.14. VideoFormats](#1114-videoformats)
    - [1.1.15. AudioFormats](#1115-audioformats)
    - [1.1.16. Source (`Uri`)](#1116-source--uri)
    - [1.1.17. Volume (`double`)](#1117-volume--double)
    - [1.1.18. Balance (`double`)](#1118-balance--double)
    - [1.1.19. IsMuted (`bool`)](#1119-ismuted--bool)
    - [1.1.20. ScrubbingEnabled (`bool`)](#1120-scrubbingenabled--bool)
    - [1.1.21. Stretch (`Stretch`)](#1121-stretch--stretch)
    - [1.1.22. StretchDirection (`StretchDirection`)](#1122-stretchdirection--stretchdirection)
  - [1.2. Events](#12-events)
    - [1.2.1. MediaFailed](#121-mediafailed)
    - [1.2.2. MediaOpened](#122-mediaopened)
    - [1.2.3. BufferingStarted](#123-bufferingstarted)
    - [1.2.4. BufferingEnded](#124-bufferingended)
    - [1.2.5. ScriptCommand](#125-scriptcommand)
    - [1.2.6. MediaEnded](#126-mediaended)
- [1.3. MediaCommands](#13-mediacommands)
- [2. Icon](#2-icon)
- [3. Drag](#3-drag)
- [4. TimeSpanToStringConverter](#4-timespantostringconverter)
- [5. Commands](#5-commands)
- [6. Sample](#6-sample)

# 1. MediaElementWrapper
## 1.1 Properties
The wrapper wraps the properties of System.Windows.Controls.MediaElement and adds a couple of new properties.
Mapped properties are dependency properties that are updated when needed.

### 1.1.1 State (`MediaState`)
The current `MediaState` of the player.

### 1.1.2. Position (`Timespan?`)
The current position in the media, `null` if no media is loaded.
Twoway bindable and updates every 0.1 s when playing.

### 1.1.3. Length (`Timespan?`)
The length of the current media, `null`if no media is loaded.

### 1.1.4. CanPauseMedia (`bool?`)
Mapped to System.Windows.Controls.MediaElement.CanPause, `null`if no media is loaded.

### 1.1.5. NaturalVideoHeight (`int?`)
Mapped to System.Windows.Controls.MediaElement.NaturalVideoHeight, `null`if no media is loaded.

### 1.1.6. NaturalVideoWidth (`int?`)
Mapped to System.Windows.Controls.MediaElement.NaturalVideoWidth, `null`if no media is loaded.

### 1.1.7. HasAudio (`bool?`)
Mapped to System.Windows.Controls.MediaElement.HasAudio, `null`if no media is loaded.

### 1.1.8. HasVideo (`bool?`)
Mapped to System.Windows.Controls.MediaElement.HasVideo, `null`if no media is loaded.

### 1.1.8. HasMedia (`bool`)
Returns true if media is loaded.

### 1.1.9. SpeedRatio (`double`)
Mapped to System.Windows.Controls.MediaElement.SpeedRatio.

### 1.1.10. IsBuffering (`bool`)
Mapped to System.Windows.Controls.MediaElement.IsBuffering.

### 1.1.11. DownloadProgress (`double`)
Mapped to System.Windows.Controls.MediaElement.DownloadProgress.
Updated every 1 s when buffering.

### 1.1.12. BufferingProgress (`double`)
Mapped to System.Windows.Controls.MediaElement.BufferingProgress.
Updated every 1 s when buffering.

### 1.1.13. VolumeIncrement (`double`)
How much volume is changed when MediaCommands.IncreaseVolume & MediaCommands.DecreaseVolume are invoked.
Default 0.05;

### 1.1.14. VideoFormats
A list of video file formats for convenience.
*.dat; *.wmv; *.3g2; *.3gp; *.3gp2; *.3gpp; *.amv; *.asf;  *.avi; *.bin; *.cue; *.divx; *.dv; *.flv; *.gxf; *.iso; *.m1v; *.m2v; *.m2t; *.m2ts; *.m4v; *.mkv; *.mov; *.mp2; *.mp2v; *.mp4; *.mp4v; *.mpa; *.mpe; *.mpeg; *.mpeg1; *.mpeg2; *.mpeg4; *.mpg; *.mpv2; *.mts; *.nsv; *.nuv; *.ogg; *.ogm; *.ogv; *.ogx; *.ps; *.rec; *.rm; *.rmvb; *.tod; *.ts; *.tts; *.vob; *.vro; *.webm

Usage:

```c#
OpenFileDialog openFileDialog = new OpenFileDialog
{
    Filter = $"Media files|{this.MediaElement.VideoFormats}|All files (*.*)|*.*"
};

if (openFileDialog.ShowDialog() == true)
{
    this.MediaElement.Source = new Uri(openFileDialog.FileName);
}
```

### 1.1.15. AudioFormats
A list of audio file formats for convenience.
*.mp3; *.wma; *.aac; *.adt; *.adts; *.m4a; *.wav; *.aif; *.aifc; *.aiff; *.cda

Usage:

```c#
OpenFileDialog openFileDialog = new OpenFileDialog
{
    Filter = $"Media files|{this.MediaElement.AudioFormats}|All files (*.*)|*.*"
};

if (openFileDialog.ShowDialog() == true)
{
    this.MediaElement.Source = new Uri(openFileDialog.FileName);
}
```

### 1.1.16. Source (`Uri`)
Mapped to System.Windows.Controls.MediaElement.Source.
When source changes play is invoked to trigger load. Then pause is invoked in the MediaOpened event.
This results in the video paused at the first frame as initial state after setting `Source`
Subscribe to `MediaOpened` if you want to start playing on load.

### 1.1.17. Volume (`double`)
Mapped to System.Windows.Controls.MediaElement.Volume.

### 1.1.18. Balance (`double`)
Mapped to System.Windows.Controls.MediaElement.Balance.

### 1.1.19. IsMuted (`bool`)
Mapped to System.Windows.Controls.MediaElement.IsMuted.

### 1.1.20. ScrubbingEnabled (`bool`)
Mapped to System.Windows.Controls.MediaElement.ScrubbingEnabled.

### 1.1.21. Stretch (`Stretch`)
Mapped to System.Windows.Controls.MediaElement.Stretch.

### 1.1.22. StretchDirection (`StretchDirection`)
Mapped to System.Windows.Controls.MediaElement.StretchDirection.

## 1.2. Events

### 1.2.1. MediaFailed
Mapped to System.Windows.Controls.MediaElement.MediaFailed.

### 1.2.2. MediaOpened
Mapped to System.Windows.Controls.MediaElement.MediaOpened.

### 1.2.3. BufferingStarted
Mapped to System.Windows.Controls.MediaElement.BufferingStarted.

### 1.2.4. BufferingEnded
Mapped to System.Windows.Controls.MediaElement.BufferingEnded.

### 1.2.5. ScriptCommand
Mapped to System.Windows.Controls.MediaElement.ScriptCommand.

### 1.2.6. MediaEnded
Mapped to System.Windows.Controls.MediaElement.MediaEnded.

# 1.3. MediaCommands
Has command bindings for:
  - MediaCommands.Play
  - MediaCommands.Pause
  - MediaCommands.Stop
  - MediaCommands.TogglePlayPause
  - MediaCommands.Rewind
  - MediaCommands.IncreaseVolume
  - MediaCommands.DecreaseVolume
  - MediaCommands.MuteVolume

# 2. Icon
Exposes a `Gemoetry` attached property.
```xaml
<Button media:Icon.Geometry="{StaticResource {x:Static media:Geometries.PlayGeometryKey}}"
        Command="Play"
        CommandTarget="{Binding ElementName=MediaElement}" />
```

# 3. Drag
Exposes a `PauseWhileDragging` attached property.
When binidng this to a `MediaElementWrapper`playback is paused while dragging.

```xaml
<Slider x:Name="ProgressSlider"
        Grid.Row="0"
        media:Drag.PauseWhileDragging="{Binding ElementName=MediaElement}"
        Maximum="{Binding ElementName=MediaElement,
                            Path=Length,
                            Converter={x:Static demo:TimeSpanToSecondsConverter.Default}}"
        Minimum="0"
        Style="{StaticResource {x:Static media:Styles.ProgressSliderStyleKey}}"
        Value="{Binding ElementName=MediaElement,
                        Path=Position,
                        Converter={x:Static demo:TimeSpanToSecondsConverter.Default}}" />
```

# 4. TimeSpanToStringConverter
Converts Timespans like this:
|Time|Result|
|---|---|
|null|-:--|
|00:00:01|0:01|
|00:00:12|0:12|
|00:01:23|1:23|
|00:12:34|12:23|
|01:23:45|1:23:45|


# 5. Commands
- Commands.ToggleMute
- Commands.ToggleFullScreen

MediaElementWrapper has a command bindings for:
  - Commands.ToggleMute

# 6. Sample

```xaml
<Window x:Class="Gu.Wpf.Media.Demo.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:media="https://github.com/JohanLarsson/Gu.Wpf.Media"
        Title="MainWindow"
        MinWidth="300"
        Background="Black"
        SizeToContent="WidthAndHeight"
        mc:Ignorable="d">
    <Window.InputBindings>
        <KeyBinding Key="Space"
                    Command="TogglePlayPause"
                    CommandTarget="{Binding ElementName=MediaElement}" />
        <KeyBinding Key="F11" Command="media:Commands.ToggleFullScreen" />
    </Window.InputBindings>
    <Window.CommandBindings>
        <CommandBinding Command="ApplicationCommands.Open" Executed="OpenExecuted" />
        <CommandBinding Command="media:Commands.ToggleFullScreen" Executed="OnToggleFullScreenExecuted" />
    </Window.CommandBindings>
    <Grid>
        <media:MediaElementWrapper x:Name="MediaElement"
                                   ScrubbingEnabled="True"
                                   Stretch="None" />

        <Grid VerticalAlignment="Bottom">
            <Grid.RowDefinitions>
                <RowDefinition Height="Auto" />
                <RowDefinition Height="Auto" />
            </Grid.RowDefinitions>
            <Slider x:Name="ProgressSlider"
                    Grid.Row="0"
                    media:Drag.PauseWhileDragging="{Binding ElementName=MediaElement}"
                    Maximum="{Binding ElementName=MediaElement,
                                      Path=Length,
                                      Converter={x:Static media:NullableTimeSpanToSecondsConverter.Default}}"
                    Minimum="0"
                    Style="{StaticResource {x:Static media:Styles.ProgressSliderStyleKey}}"
                    Value="{Binding ElementName=MediaElement,
                                    Path=Position,
                                    Converter={x:Static media:NullableTimeSpanToSecondsConverter.Default}}" />
            
            <Grid Grid.Row="1">
                <Grid.Resources>
                    <Style BasedOn="{StaticResource {x:Static media:Styles.PlayerButtonBaseStyleKey}}" TargetType="{x:Type Button}" />
                </Grid.Resources>
                <Grid.ColumnDefinitions>
                    <ColumnDefinition Width="Auto" />
                    <ColumnDefinition Width="Auto" />
                    <ColumnDefinition Width="Auto" />
                    <ColumnDefinition />
                    <ColumnDefinition Width="Auto" />
                    <ColumnDefinition Width="Auto" />
                </Grid.ColumnDefinitions>

                <ToggleButton x:Name="PlayPauseButton"
                              Grid.Column="0"
                              IsChecked="{Binding ElementName=MediaElement,
                                                  Path=IsPlaying}"
                              IsEnabled="{Binding ElementName=MediaElement,
                                                  Path=HasMedia}">
                    <ToggleButton.Style>
                        <Style BasedOn="{StaticResource {x:Static media:Styles.PlayerButtonBaseStyleKey}}" TargetType="{x:Type ToggleButton}">
                            <Setter Property="media:Icon.Geometry" Value="{StaticResource {x:Static media:Geometries.PauseGeometryKey}}" />
                            <Style.Triggers>
                                <Trigger Property="IsChecked" Value="False">
                                    <Setter Property="media:Icon.Geometry" Value="{StaticResource {x:Static media:Geometries.PlayGeometryKey}}" />
                                </Trigger>
                            </Style.Triggers>
                        </Style>
                    </ToggleButton.Style>
                </ToggleButton>

                <ToggleButton x:Name="ToggleMutedButton"
                              Grid.Column="1"
                              IsChecked="{Binding ElementName=MediaElement,
                                                  Path=IsMuted}"
                              IsEnabled="{Binding ElementName=MediaElement,
                                                  Path=HasMedia}">
                    <ToggleButton.Style>
                        <Style BasedOn="{StaticResource {x:Static media:Styles.PlayerButtonBaseStyleKey}}" TargetType="{x:Type ToggleButton}">
                            <Setter Property="media:Icon.Geometry" Value="{StaticResource {x:Static media:Geometries.UnMuteGeometryKey}}" />
                            <Style.Triggers>
                                <Trigger Property="IsChecked" Value="False">
                                    <Setter Property="media:Icon.Geometry" Value="{StaticResource {x:Static media:Geometries.MuteGeometryKey}}" />
                                </Trigger>
                            </Style.Triggers>
                        </Style>
                    </ToggleButton.Style>
                </ToggleButton>

                <TextBlock x:Name="ProgressTextBlock"
                           Grid.Column="2"
                           VerticalAlignment="Center"
                           Foreground="{Binding ElementName=ToggleMutedButton,
                                                Path=Foreground}"
                           Opacity="{Binding ElementName=ToggleMutedButton,
                                             Path=Opacity}">
                    <TextBlock.Text>
                        <MultiBinding StringFormat="{}{0} / {1}">
                            <Binding Converter="{x:Static media:TimeSpanToStringConverter.Default}"
                                     ElementName="MediaElement"
                                     Path="Position" />
                            <Binding Converter="{x:Static media:TimeSpanToStringConverter.Default}"
                                     ElementName="MediaElement"
                                     Path="Length" />
                        </MultiBinding>
                    </TextBlock.Text>
                </TextBlock>

                <Button Grid.Column="4"
                        media:Icon.Geometry="{StaticResource {x:Static media:Geometries.FolderOpenGeometryKey}}"
                        Command="ApplicationCommands.Open" />

                <Button Grid.Column="5"
                        HorizontalAlignment="Right"
                        media:Icon.Geometry="{StaticResource {x:Static media:Geometries.FullScreenGeometryKey}}"
                        Command="media:Commands.ToggleFullScreen"
                        IsEnabled="{Binding ElementName=MediaElement,
                                            Path=HasMedia}"
                        Padding="12,6,6,6" />
            </Grid>
        </Grid>
    </Grid>
</Window>
```

With code behind:

```c#
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;

public partial class MainWindow : Window
{
    private Stretch stretch;

    public MainWindow()
    {
        this.InitializeComponent();
    }

    private void OpenExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Filter = $"Media files|{this.MediaElement.VideoFormats}|All files (*.*)|*.*"
        };

        if (openFileDialog.ShowDialog() == true)
        {
            this.MediaElement.Source = new Uri(openFileDialog.FileName);
        }
    }

    private void OnToggleFullScreenExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        if (this.WindowStyle == WindowStyle.SingleBorderWindow)
        {
            this.stretch = this.MediaElement.Stretch;
            this.MediaElement.Stretch = Stretch.Uniform;
            this.WindowStyle = WindowStyle.None;
            this.SizeToContent = SizeToContent.Manual;
            this.WindowState = WindowState.Maximized;
        }
        else
        {
            this.MediaElement.Stretch = this.stretch;
            this.WindowStyle = WindowStyle.SingleBorderWindow;
            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.WindowState = WindowState.Normal;
        }
    }
}
```

Check out the demo project for more samples.
Sample video from https://pixabay.com/

