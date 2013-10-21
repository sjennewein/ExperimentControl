using System;
using System.Diagnostics;
using System.Threading;
using DigitalOutput.Model;
using NationalInstruments.DAQmx;
using fastJSON;
using Timing = DigitalOutput.Model.Timing;

namespace DigitalOutput.Hardware
{
    public class Buffer
    {
        private ModelCard _data;
        private Thread _myWorker;
        private UInt32[] _outputSequence;
        private bool _run = true;
        private bool _running;
        private string _serializedData;
        private bool _updated;


        public void UpdateData(string newData)
        {
            _serializedData = newData;
            _updated = true;
        }

        public void WorkingLoop()
        {
            while (_run)
            {
                _running = true;               

                if (_updated)
                {
                    _data = (ModelCard) JSON.Instance.ToObject(_serializedData);
                    _outputSequence = Translator.GenerateOutput(_data);
                    _updated = false;
                }

                //initialize output card
                var digitalOutputTask = new Task("PCI6534");
                digitalOutputTask.DOChannels.CreateChannel("/Dev2/port0_32", "",
                                                           ChannelLineGrouping.OneChannelForAllLines);

                double sampleRate = _data.SampleRate.GetFrequency(Timing.Frequency.Hz);

                digitalOutputTask.Timing.ConfigureSampleClock("", sampleRate,
                                                              SampleClockActiveEdge.Rising,
                                                              SampleQuantityMode.FiniteSamples);
                digitalOutputTask.Triggers.StartTrigger.ConfigureDigitalEdgeTrigger("/Dev2/PFI6",
                                                                                    DigitalEdgeStartTriggerEdge.Rising);

                //write output to card
                var writer = new DigitalSingleChannelWriter(digitalOutputTask.Stream);
                writer.WriteMultiSamplePort(false, _outputSequence);


                //start and wait until everything is done
                digitalOutputTask.Start();
                digitalOutputTask.WaitUntilDone(60000);

                //free hardware
                digitalOutputTask.Stop();
                digitalOutputTask.Dispose();
            }
            _running = false;
        }

        public void Start(string data)
        {
            if (!_running)
            {
                _serializedData = data;
                _updated = true;
                _myWorker = new Thread(WorkingLoop);
                _myWorker.Start();
            }
            _run = true;
        }

        public void Stop()
        {
            _run = false;
        }
    }
}