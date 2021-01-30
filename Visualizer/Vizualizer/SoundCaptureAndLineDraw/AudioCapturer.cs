using CSCore;
using CSCore.CoreAudioAPI;
using CSCore.DSP;
using CSCore.SoundIn;
using CSCore.SoundOut;
using CSCore.Streams;
using CSCore.Streams.Effects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Threading;
using Visualizer.SoundCaptureAndLineDraw.LineDraw;
using Visualizer.Visual;

namespace VizualizerAC
{
    public enum Mode
    {
        [Description("WasapiCapture")] WasapiCapture = 1,
        [Description("WasapiLoopbackCapture")] WasapiLoopbackCapture = 2,
        [Description("LineIn")] LineIn = 3,
    }
    internal enum CaptureMode
    {
        [Description("WasapiCapture")] WasapiCapture = 1,
        [Description("WasapiLoopbackCapture")] WasapiLoopbackCapture = 2,
        [Description("LineIn")] LineIn = 3,
    }
    public enum FieldType
    {
        Rainbow,
        Custom
    }
    public class AudioCapturer
    {
        private System.Timers.Timer timer1;
        private WasapiCapture _soundIn;
        private WaveIn _soundLineIn;
        private ISoundOut _soundOut;
        private IWaveSource _source;
        private PitchShifter _pitchShifter;
        private LineSpectrum _lineSpectrum;

        public bool HighQuality { get; set; } = true;

        public Mode Mode { get; set; } = Mode.WasapiLoopbackCapture;
        public string AudioSource { get; set; }
        public int DeviceIndex { get; set; } = 0;
        public string FileName { get; set; }
        public int Interval { get; set; } = 40;
        public bool UseAverage { get; set; } = true;
        public int BarCount { get; set; } = 90;
        public int MaximumFrequency { get; set; } = 10000;
        public int BarSpacing { get; set; } = 0;
        public bool IsXLogScale { get; set; } = true;
        public ScalingStrategy ScalingStrategy { get; set; }
        public LineSpectrum.DrawLineStrategies DrawLineStrategy { get; set; }

        private void SetupSampleSource(ISampleSource aSampleSource)
        {
            BasicSpectrumProvider spectrumProvider = new BasicSpectrumProvider(aSampleSource.WaveFormat.Channels, aSampleSource.WaveFormat.SampleRate, FftSize.Fft4096);
            LineSpectrum lineSpectrum = new LineSpectrum(
                FftSize.Fft4096,
                DrawLineStrategy,
                Metadata.SMetadata.FieldMetadata.ShapeType,
                Metadata.SMetadata.FieldMetadata.BindingPenSizeToLineSize, 
                Metadata.SMetadata.FieldMetadata.PenSize);
            lineSpectrum.SpectrumProvider = spectrumProvider;
            lineSpectrum.UseAverage = UseAverage;
            lineSpectrum.BarCount = BarCount;
            lineSpectrum.BarSpacing = BarSpacing;
            lineSpectrum.IsXLogScale = IsXLogScale;
            lineSpectrum.MaximumFrequency = MaximumFrequency;
            lineSpectrum.ScalingStrategy = ScalingStrategy;
            _lineSpectrum = lineSpectrum;
            SingleBlockNotificationStream sampleSource = new SingleBlockNotificationStream(aSampleSource);
            sampleSource.SingleBlockRead += (EventHandler<SingleBlockReadEventArgs>)((s, a) => spectrumProvider.Add(a.Left, a.Right));
            _source = sampleSource.ToWaveSource(16);
        }
        private void StartWasapiLoopbackCapture()
        {
            MMDeviceCollection source = MMDeviceEnumerator.EnumerateDevices(DataFlow.Render, DeviceState.Active);
            if (!source.Any())
            {
                Console.WriteLine("No devices found.");
            }
            else
            {
                MMDevice mmDevice = source[DeviceIndex];
                _soundIn = (WasapiCapture)new WasapiLoopbackCapture();
                _soundIn.Device = mmDevice;
                _soundIn.Initialize();
                SoundInSource waveSource = new SoundInSource(_soundIn);
                SetupSampleSource((ISampleSource)waveSource.ToSampleSource().AppendSource((x => new PitchShifter(x)), out _pitchShifter));
                byte[] buffer = new byte[this._source.WaveFormat.BytesPerSecond / 2];
                waveSource.DataAvailable += (EventHandler<DataAvailableEventArgs>)((s, aEvent) =>
                {
                    do
                        ;
                    while (this._source.Read(buffer, 0, buffer.Length) > 0);
                });
                _soundIn.Start();
            }
        }

        private void StartWasapiCapture()
        {
            MMDeviceCollection source = MMDeviceEnumerator.EnumerateDevices(DataFlow.Capture, DeviceState.Active);
            if (!source.Any<MMDevice>())
            {
                Console.WriteLine("No devices found.");
            }
            else
            {
                MMDevice mmDevice = source[DeviceIndex];
                _soundIn = new WasapiCapture();
                _soundIn.Device = mmDevice;
                _soundIn.Initialize();
                SoundInSource waveSource = new SoundInSource(_soundIn);
                SetupSampleSource((ISampleSource)waveSource.ToSampleSource().AppendSource((x => new PitchShifter(x)), out _pitchShifter));
                byte[] buffer = new byte[_source.WaveFormat.BytesPerSecond / 2];
                waveSource.DataAvailable += ((s, aEvent) =>
                {
                    do
                        ;
                    while (_source.Read(buffer, 0, buffer.Length) > 0);
                });
                _soundIn.Start();
            }
        }

        private void StartLineIn()
        {
            IEnumerable<WaveInDevice> source = WaveInDevice.EnumerateDevices();
            if (!source.Any())
            {
                Console.WriteLine("No devices found.");
            }
            else
            {
                int deviceIndex = DeviceIndex;
                WaveInDevice waveInDevice = source.ElementAt(deviceIndex);
                _soundLineIn = new WaveIn();
                _soundLineIn.Device = waveInDevice;
                _soundLineIn.Initialize();
                SoundInSource waveSource = new SoundInSource(_soundLineIn);
                SetupSampleSource(waveSource.ToSampleSource());
                byte[] buffer = new byte[_source.WaveFormat.BytesPerSecond / 2];
                waveSource.DataAvailable += ((s, aEvent) =>
                {
                    do
                        ;
                    while (this._source.Read(buffer, 0, buffer.Length) > 0);
                });
                this._soundLineIn.Start();
            }
        }

        public static ObservableCollection<string> GetDevices(Mode mode = Mode.WasapiLoopbackCapture)
        {
            ObservableCollection<string> observableCollection = new ObservableCollection<string>();
            try
            {
                CaptureMode captureMode;
                switch (mode)
                {
                    case Mode.WasapiLoopbackCapture:
                        captureMode = CaptureMode.WasapiLoopbackCapture;
                        break;
                    case Mode.LineIn:
                        IEnumerable<WaveInDevice> source1 = WaveInDevice.EnumerateDevices();
                        if (!source1.Any<WaveInDevice>())
                            Console.WriteLine("No devices found.");
                        Console.WriteLine("Select device:");
                        foreach (WaveInDevice waveInDevice in source1)
                            observableCollection.Add(waveInDevice.Name);
                        return observableCollection;
                    default:
                        captureMode = CaptureMode.WasapiCapture;
                        break;
                }
                MMDeviceCollection source2 = MMDeviceEnumerator.EnumerateDevices(captureMode == CaptureMode.WasapiCapture ? DataFlow.Capture : DataFlow.Render, DeviceState.Active);
                if (!source2.Any<MMDevice>())
                {
                    Console.WriteLine("No devices found.");
                    return observableCollection;
                }
                Console.WriteLine("Select device:");
                for (int index = 0; index < source2.Count; ++index)
                    observableCollection.Add(source2[index].FriendlyName);
                return observableCollection;
            }
            catch (Exception ex)
            { }
            finally
            { }
            return observableCollection;
        }
        public int GetDeviceDefaultIndex(Mode mode = Mode.WasapiLoopbackCapture)
        {
            try
            {
                if (mode == Mode.LineIn)
                    return -1;
                ObservableCollection<string> observableCollection = new ObservableCollection<string>();
                CaptureMode captureMode = mode != Mode.WasapiLoopbackCapture ? CaptureMode.WasapiCapture : CaptureMode.WasapiLoopbackCapture;
                MMDeviceEnumerator deviceEnumerator1 = new MMDeviceEnumerator();
                DataFlow dataFlow = captureMode == CaptureMode.WasapiCapture ? DataFlow.Capture : DataFlow.Render;
                MMDevice defaultAudioEndpoint = deviceEnumerator1.GetDefaultAudioEndpoint(dataFlow, Role.Console);
                using (MMDeviceEnumerator deviceEnumerator2 = new MMDeviceEnumerator())
                {
                    using (MMDeviceCollection deviceCollection = deviceEnumerator2.EnumAudioEndpoints(dataFlow, DeviceState.Active))
                    {
                        int num = 0;
                        foreach (MMDevice mmDevice in deviceCollection)
                        {
                            if (defaultAudioEndpoint.DeviceID == mmDevice.DeviceID)
                                return num;
                            ++num;
                        }
                    }
                }
                return -1;
            }
            catch (Exception ex)
            { }
            finally
            { }
            return -1;
        }
        public string GetEnumValue(Enum value)
        {
            MemberInfo element = ((IEnumerable<MemberInfo>)value.GetType().GetMember(value.ToString())).FirstOrDefault<MemberInfo>();
            return (object)element != null ? element.GetCustomAttribute<DescriptionAttribute>()?.Description : (string)null;
        }
        public T GetEnumValue<T>(string str) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new Exception("T must be an Enumeration type.");
            T obj1 = ((T[])Enum.GetValues(typeof(T)))[0];
            if (!string.IsNullOrEmpty(str))
            {
                foreach (T obj2 in (T[])Enum.GetValues(typeof(T)))
                {
                    if (obj2.ToString().ToUpper().Equals(str.ToUpper()))
                    {
                        obj1 = obj2;
                        break;
                    }
                }
            }
            return obj1;
        }
        public FieldType FieldType { get; set; }
        public void Start()
        {
            Stop();
            if (Mode == Mode.WasapiCapture)
                StartWasapiCapture();
            else if (Mode == Mode.LineIn)
                StartLineIn();
            else
                StartWasapiLoopbackCapture();
            timer1 = new System.Timers.Timer();
            switch (FieldType)
            {
                case FieldType.Rainbow:
                    timer1.Elapsed += new ElapsedEventHandler(timer1_Tick_2);
                    break;
                case FieldType.Custom:
                    timer1.Elapsed += new ElapsedEventHandler(timer1_Tick_1);
                    break;
            }
            timer1.Interval = Interval;
            timer1.Enabled = true;
        }
        public Size Size { get; set; }
        private void timer1_Tick_2(object sender, ElapsedEventArgs e)
        {

            timer1.Enabled = false;
            UpdateBars();
            timer1.Enabled = true;
        }
        public delegate void UpdHandler(SpectrumBase.SpectrumPointData[] spectrumPointData);
        public event UpdHandler UpdBars;
        public event UpdHandler FieldWasUpdated;
        private void UpdateBars()
        {
            var spd = _lineSpectrum.GetSpectrumPointData(Size);
            UpdBars?.Invoke(spd);
            FieldWasUpdated?.Invoke(spd);
        }
        private void timer1_Tick_1(object source, ElapsedEventArgs e)
        {
            timer1.Enabled = false;
            GenerateLineSpectrum();
            timer1.Enabled = true;

        }
        public System.Windows.Controls.Image Image { get; set; }
        public Dispatcher Dispatcher { get; set; }
        public List<Color> Colors { get; set; }
        private void GenerateLineSpectrum()
        {
            Size size = new Size();
            Vizualizer.MainWindow.MDispatcher.Invoke(new Action(() => { size = new Size((int)Image.ActualWidth, (int)Image.ActualHeight); }));

            Bitmap spectrumLine = _lineSpectrum.CreateSpectrumLine(size, Colors, Metadata.SMetadata.GraphicsMetadata);
            if (spectrumLine == null)
                return;
            Dispatcher.Invoke(new Action(() => { Image.Source = spectrumLine.ImageSourceForBitmap(); }));
            FieldWasUpdated?.Invoke(_lineSpectrum.LastSpectrumData);

        }
        public void Stop()
        {
            if (timer1 != null)
            {
                timer1.Stop();
                timer1.Dispose();
                timer1 = null;
            }
            if (_soundOut != null)
            {
                _soundOut.Stop();
                _soundOut.Dispose();
                _soundOut = null;
            }
            if (_soundIn != null)
            {
                _soundIn.Stop();
                _soundIn.Dispose();
                _soundIn = null;
            }
            if (_soundLineIn != null)
            {
                _soundLineIn.Stop();
                _soundLineIn.Dispose();
                _soundLineIn = null;
            }
            if (_source == null)
                return;
            _source.Dispose();
            _source = null;
        }
    }
}
