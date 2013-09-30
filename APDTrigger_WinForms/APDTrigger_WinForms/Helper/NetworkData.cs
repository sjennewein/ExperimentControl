using fastJSON;

namespace APDTrigger_WinForms.Helper
{
    public class NetworkData
    {
        public readonly int Atoms;
        public readonly int NoAtoms;
        public readonly int CyclesDone;                
        public readonly int RunsDone;
        public readonly int TotalRuns;
        public readonly double RecaptureRate;

        public NetworkData(int atoms, int noAtoms, int cyclesDone, int runsDone, int totalRuns, double recaptureRate)
        {
            Atoms = atoms;
            NoAtoms = noAtoms;
            CyclesDone = cyclesDone;
            RecaptureRate = recaptureRate;
            RunsDone = runsDone;
            TotalRuns = totalRuns;
        }

        public string Serialize()
        {

            var parameter = new JSONParameters();
            parameter.UseExtensions = false;
            return JSON.Instance.ToJSON(this, parameter);

        }
    }
}