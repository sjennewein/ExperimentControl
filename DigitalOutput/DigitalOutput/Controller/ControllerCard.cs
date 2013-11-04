using DigitalOutput.Hardware;
using DigitalOutput.Model;
using Hulahoop.Controller;
using Ionic.Zip;
using fastJSON;

namespace DigitalOutput.Controller
{
    public class ControllerCard
    {
        private readonly Buffer _hardwareBuffer;
        private readonly ModelCard _model;
        public ControllerPattern[] Patterns;

        public ControllerCard(ModelCard model, Buffer buffer)
        {
            _model = model;
            Patterns = new ControllerPattern[_model.Patterns.Length];
            for (int iPattern = 0; iPattern < _model.Patterns.Length; iPattern++)
            {
                ModelPattern modelPattern = _model.Patterns[iPattern];
                Patterns[iPattern] = new ControllerPattern(modelPattern);
            }
        }

        public string Flow
        {
            get { return _model.Flow; }
            set { _model.Flow = value; }
        }

        public void Start()
        {
            _hardwareBuffer.Start(JSON.Instance.ToJSON(_model));
        }

        public void Stop()
        {
            _hardwareBuffer.Stop();
        }

        public void Save(string fileName)
        {
            string json = JSON.Instance.ToJSON(_model);
            using (var zip = new ZipFile())
            {
                zip.AddEntry("DigitalData.txt", json);
                HoopManager.Save(zip);
                zip.Save(fileName);
            }
        }

        public void CopyToBuffer()
        {
            string data = JSON.Instance.ToJSON(_model);
            _hardwareBuffer.UpdateData(data);
        }

        /// <summary>
        /// Saves Values for UNDO
        /// </summary>
        public void StoreSyncedValues()
        {
            foreach (ControllerPattern pattern in Patterns)
            {
                pattern.StoreSyncedValues();
            }
        }

        /// <summary>
        /// Restores Values for UNDO
        /// </summary>
        public void RestoreSyncedValues()
        {
            foreach (ControllerPattern pattern in Patterns)
            {
                pattern.RestoreSyncedValues();
            }
        }
    }
}