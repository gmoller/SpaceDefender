using System.Collections.Generic;

namespace GameLibrary
{
    public class AnimationSequence
    {
        private readonly List<int> _frames;
        private int _currentFramePointer;

        public string Id { get; private set; }
        public int StartFrame { get; private set; }

        public AnimationSequence(string id, List<int> frames)
        {
            _frames = frames;
            _currentFramePointer = 0;
            Id = id;
            StartFrame = frames[0];
        }

        public int GetNextFrame()
        {
            _currentFramePointer++;

            if (_currentFramePointer > _frames.Count - 1)
            {
                _currentFramePointer = 0;
                return -1;
            }

            return _frames[_currentFramePointer];
        }
    }
}