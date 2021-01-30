using CSCore.DSP;
using System;
using System.Collections.Generic;

namespace VizualizerAC
{
    public class BasicSpectrumProvider : FftProvider, ISpectrumProvider
    {
        private readonly List<object> _contexts = new List<object>();
        private readonly int _sampleRate;

        public BasicSpectrumProvider(int channels, int sampleRate, FftSize fftSize)
          : base(channels, fftSize)
        {
            if (sampleRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(sampleRate));
            this._sampleRate = sampleRate;
        }

        public int GetFftBandIndex(float frequency)
        {
            int fftSize = (int)this.FftSize;
            double num = (double)this._sampleRate / 2.0;
            return (int)((double)frequency / num * (double)(fftSize / 2));
        }

        public bool GetFftData(float[] fftResultBuffer, object context)
        {
            if (this._contexts.Contains(context))
                return false;
            this._contexts.Add(context);
            this.GetFftData(fftResultBuffer);
            return true;
        }
        public override void Add(float left, float right)
        {
            base.Add(left, right);
            this._contexts.Clear();
        }
    }
}
