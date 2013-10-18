using System;

namespace DigitalOutput.Model
{
    public class ModelCard
    {
        public ModelPattern[] Patterns;
        public Timing SampleRate; //in Hz
        public string[] ChannelDescription;
        public string Flow;
    }
}