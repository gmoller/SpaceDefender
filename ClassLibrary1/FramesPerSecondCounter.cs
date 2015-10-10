using System.Collections.Generic;
using System.Linq;

namespace GameLibrary
{
    public class FramesPerSecondCounter
    {
        private readonly Queue<double> _sampleBuffer = new Queue<double>();

        public double AverageFramesPerSecond { get; private set; }
        private double CurrentFramesPerSecond { get; set; }
        private int MaximumSamples { get; set; }

        public FramesPerSecondCounter(int maximumSamples = 100)
        {
            MaximumSamples = maximumSamples;
        }

        public void Reset()
        {
            _sampleBuffer.Clear();
        }

        public void Update(double deltaTime)
        {
            CurrentFramesPerSecond = 1.0f / deltaTime;

            _sampleBuffer.Enqueue(CurrentFramesPerSecond);

            if (_sampleBuffer.Count > MaximumSamples)
            {
                _sampleBuffer.Dequeue();
                AverageFramesPerSecond = _sampleBuffer.Average(i => i);
            }
            else
            {
                AverageFramesPerSecond = CurrentFramesPerSecond;
            }
        }
    }
}