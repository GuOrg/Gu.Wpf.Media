# Gu.Wpf.Media
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE.md) 
[![NuGet](https://img.shields.io/nuget/v/Gu.Wpf.Media.svg)](https://www.nuget.org/packages/Gu.Wpf.Media/)
[![Build status](https://ci.appveyor.com/api/projects/status/eg96glf1osi8on6y/branch/master?svg=true)](https://ci.appveyor.com/project/JohanLarsson/gu-wpf-media/branch/master)

Wrapper for System.Windows.Controls.MediaElement.

# Contents
- [1. Properties](#1-properties)
  - [1.1. State (`MediaState`)](#11-state--mediastate)
  - [1.2. Position (`Timespan?`)](#12-position--timespan)
  - [1.3. Length (`Timespan?`)](#13-length--timespan)
  - [1.4. CanPauseMedia (`bool?`)](#14-canpausemedia--bool)
  - [1.5. NaturalVideoHeight (`int?`)](#15-naturalvideoheight--int)
  - [1.6. NaturalVideoWidth (`int?`)](#16-naturalvideowidth--int)
  - [1.7. HasAudio (`bool?`)](#17-hasaudio--bool)
  - [1.8. HasVideo (`bool?`)](#18-hasvideo--bool)
  - [1.9. SpeedRatio (`double`)](#19-speedratio--double)
  - [1.10. IsBuffering (`bool`)](#110-isbuffering--bool)
  - [1.11. DownloadProgress (`double`)](#111-downloadprogress--double)
  - [1.12. BufferingProgress (`double`)](#112-bufferingprogress--double)
  - [1.13. VolumeIncrement (`double`)](#113-volumeincrement--double)
  - [1.14. VideoFormats](#114-videoformats)
  - [1.15. AudioFormats](#115-audioformats)
  - [1.16. Source (`Uri`)](#116-source--uri)
  - [1.17. Volume (`double`)](#117-volume--double)
  - [1.18. Balance (`double`)](#118-balance--double)
  - [1.19. IsMuted (`bool`)](#119-ismuted--bool)
  - [1.20. ScrubbingEnabled (`bool`)](#120-scrubbingenabled--bool)
  - [1.21. Stretch (`Stretch`)](#121-stretch--stretch)
  - [1.22. StretchDirection (`StretchDirection`)](#122-stretchdirection--stretchdirection)
- [2. Events](#2-events)
  - [2.1. MediaFailed](#21-mediafailed)
  - [2.2. MediaOpened](#22-mediaopened)
  - [2.3. BufferingStarted](#23-bufferingstarted)
  - [2.4. BufferingEnded](#24-bufferingended)
  - [2.5. ScriptCommand](#25-scriptcommand)
  - [2.6. MediaEnded](#26-mediaended)
- [3. MediaCommands](#3-mediacommands)
- [4. Sample](#4-sample)

# 1. Properties
The wrapper wraps the properties of System.Windows.Controls.MediaElement and adds a couple of new properties.
Mapped properties are dependency properties that are updated when needed.

## 1.1. State (`MediaState`)
The current `MediaState` of the player.

## 1.2. Position (`Timespan?`)
The current position in the media, `null` if no media is loaded.
Twoway bindable and updates every 0.1 s when playing.

## 1.3. Length (`Timespan?`)
The length of the current media, `null`if no media is loaded.

## 1.4. CanPauseMedia (`bool?`)
Mapped to System.Windows.Controls.MediaElement.CanPause, `null`if no media is loaded.

## 1.5. NaturalVideoHeight (`int?`)
Mapped to System.Windows.Controls.MediaElement.NaturalVideoHeight, `null`if no media is loaded.

## 1.6. NaturalVideoWidth (`int?`)
Mapped to System.Windows.Controls.MediaElement.NaturalVideoWidth, `null`if no media is loaded.

## 1.7. HasAudio (`bool?`)
Mapped to System.Windows.Controls.MediaElement.HasAudio, `null`if no media is loaded.

## 1.8. HasVideo (`bool?`)
Mapped to System.Windows.Controls.MediaElement.HasVideo, `null`if no media is loaded.

## 1.9. SpeedRatio (`double`)
Mapped to System.Windows.Controls.MediaElement.SpeedRatio.

## 1.10. IsBuffering (`bool`)
Mapped to System.Windows.Controls.MediaElement.IsBuffering.

## 1.11. DownloadProgress (`double`)
Mapped to System.Windows.Controls.MediaElement.DownloadProgress.
Updated every 1 s when buffering.

## 1.12. BufferingProgress (`double`)
Mapped to System.Windows.Controls.MediaElement.BufferingProgress.
Updated every 1 s when buffering.

## 1.13. VolumeIncrement (`double`)
How much volume is changed when MediaCommands.IncreaseVolume & MediaCommands.DecreaseVolume are invoked.
Default 0.05;

## 1.14. VideoFormats
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

## 1.15. AudioFormats
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

## 1.16. Source (`Uri`)
Mapped to System.Windows.Controls.MediaElement.Source.
When source changes play is invoked to trigger load. Then pause is invoked in the MediaOpened event.
This results in the video paused at the first frame as initial state after setting `Source`
Subscribe to `MediaOpened` if you want to start playing on load.

## 1.17. Volume (`double`)
Mapped to System.Windows.Controls.MediaElement.Volume.

## 1.18. Balance (`double`)
Mapped to System.Windows.Controls.MediaElement.Balance.

## 1.19. IsMuted (`bool`)
Mapped to System.Windows.Controls.MediaElement.IsMuted.

## 1.20. ScrubbingEnabled (`bool`)
Mapped to System.Windows.Controls.MediaElement.ScrubbingEnabled.

## 1.21. Stretch (`Stretch`)
Mapped to System.Windows.Controls.MediaElement.Stretch.

## 1.22. StretchDirection (`StretchDirection`)
Mapped to System.Windows.Controls.MediaElement.StretchDirection.

# 2. Events

## 2.1. MediaFailed
Mapped to System.Windows.Controls.MediaElement.MediaFailed.

## 2.2. MediaOpened
Mapped to System.Windows.Controls.MediaElement.MediaOpened.

## 2.3. BufferingStarted
Mapped to System.Windows.Controls.MediaElement.BufferingStarted.

## 2.4. BufferingEnded
Mapped to System.Windows.Controls.MediaElement.BufferingEnded.

## 2.5. ScriptCommand
Mapped to System.Windows.Controls.MediaElement.ScriptCommand.

## 2.6. MediaEnded
Mapped to System.Windows.Controls.MediaElement.MediaEnded.

# 3. MediaCommands
Adds command bindings for:
  - MediaCommands.Play
  - MediaCommands.Pause
  - MediaCommands.Stop
  - MediaCommands.TogglePlayPause
  - MediaCommands.Rewind
  - MediaCommands.IncreaseVolume
  - MediaCommands.DecreaseVolume
  - MediaCommands.MuteVolume

# 4. Sample

```xaml
<Window x:Class="Gu.Wpf.Media.Demo.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:demo="clr-namespace:Gu.Wpf.Media.Demo"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:media="https://github.com/JohanLarsson/Gu.Wpf.Media"
        Title="MainWindow"
        Width="300"
        Height="300"
        MinWidth="300"
        SizeToContent="WidthAndHeight"
        mc:Ignorable="d">
    <Window.InputBindings>
        <KeyBinding Key="Space"
                    Command="TogglePlayPause"
                    CommandTarget="{Binding ElementName=MediaElement}" />
    </Window.InputBindings>
    <Window.CommandBindings>
        <CommandBinding Command="ApplicationCommands.Open" Executed="OpenExecuted" />
    </Window.CommandBindings>
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto" />
            <RowDefinition Height="*" />
            <RowDefinition Height="Auto" />
        </Grid.RowDefinitions>
        <ToolBar>
            <Button Command="ApplicationCommands.Open" Content="Open"/>
            <Separator />
            <Button Command="MediaCommands.Play" CommandTarget="{Binding ElementName=MediaElement}" Content="Play"/>
            <Button Command="MediaCommands.Pause" CommandTarget="{Binding ElementName=MediaElement}" Content="Pause"/>
            <Button Command="MediaCommands.Stop" CommandTarget="{Binding ElementName=MediaElement}" Content="Stop"/>
        </ToolBar>

        <media:MediaElementWrapper x:Name="MediaElement"
                                   Grid.Row="1"
                                   MediaOpened="OnMediaOpened"
                                   ScrubbingEnabled="True" />

        <StatusBar Grid.Row="2">
            <StatusBar.ItemsPanel>
                <ItemsPanelTemplate>
                    <Grid>
                        <Grid.ColumnDefinitions>
                            <ColumnDefinition Width="*" />
                            <ColumnDefinition Width="Auto" />
                        </Grid.ColumnDefinitions>
                    </Grid>
                </ItemsPanelTemplate>
            </StatusBar.ItemsPanel>

            <StatusBarItem Grid.Column="0" HorizontalContentAlignment="Stretch">
                <Slider x:Name="ProgressSlider"
                        Maximum="{Binding ElementName=MediaElement,
                                          Path=Length,
                                          Converter={x:Static demo:TimeSpanToSecondsConverter.Default}}"
                        Minimum="0"
                        Thumb.DragCompleted="OnProgressSliderDragCompleted"
                        Thumb.DragStarted="OnProgressSliderDragStarted"
                        Value="{Binding ElementName=MediaElement,
                                        Path=Position,
                                        Converter={x:Static demo:TimeSpanToSecondsConverter.Default}}" />
            </StatusBarItem>

            <StatusBarItem Grid.Column="1">
                <TextBlock x:Name="ProgressTextBlock">
                    <TextBlock.Text>
                        <MultiBinding StringFormat="{}{0}/{1}">
                            <Binding ElementName="MediaElement" Path="Position" />
                            <Binding ElementName="MediaElement" Path="Length" />
                        </MultiBinding>
                    </TextBlock.Text>
                </TextBlock>
            </StatusBarItem>
        </StatusBar>
    </Grid>
</Window>
```

With code behind:

```c#
public partial class MainWindow : Window
{
    private MediaState mediaState;

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

    private void OnProgressSliderDragStarted(object sender, DragStartedEventArgs e)
    {
        this.mediaState = this.MediaElement.State;
        this.MediaElement.Pause();
    }

    private void OnProgressSliderDragCompleted(object sender, DragCompletedEventArgs e)
    {
        if (this.mediaState == MediaState.Play)
        {
            this.MediaElement.Play();
        }
    }
}
```

Check out the demo project for more samples.

