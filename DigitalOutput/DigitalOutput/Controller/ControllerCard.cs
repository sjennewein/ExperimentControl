﻿using System.IO;
using DigitalOutput.Model;
using fastJSON;

namespace DigitalOutput.Controller
{
    public class ControllerCard
    {
        private readonly ModelCard _model;
        public ControllerPattern[] Patterns;

        public ControllerCard(ModelCard model)
        {
            _model = model;
            Patterns = new ControllerPattern[_model.Patterns.Length];
            for (int iPattern = 0; iPattern < _model.Patterns.Length; iPattern++)
            {
                ModelPattern modelPattern = _model.Patterns[iPattern];
                Patterns[iPattern] = new ControllerPattern(modelPattern);
            }
        }

        public void Save(Stream fileStream)
        {
            
            string json = JSON.Instance.ToJSON(_model);

            using (var writer = new StreamWriter(fileStream))
            {
                writer.Write(json);
                writer.Close();
            }
        }
    }
}