using System;

namespace NuclearLeakSim_WinForms.Models
{
    public class LeakScenario
    {
        // scenario parameters (public supaya bisa diset dari UI nanti)
        public double Duration { get; set; } = 12.0;
        public double PressureSpikeStart { get; set; } = 2.0;
        public double PressureSpikeEnd { get; set; } = 2.5;
        public double PressureSpikeAmp { get; set; } = 25.0;

        public double AcousticStart { get; set; } = 2.2;
        public double AcousticEnd { get; set; } = 3.0;
        public double AcousticAmp { get; set; } = 1.0;

        public double HumidityStart { get; set; } = 2.5;
        public double HumidityEnd { get; set; } = 12.0;
        public double HumidityLow { get; set; } = 40.0;
        public double HumidityHigh { get; set; } = 75.0;

        public double TempStart { get; set; } = 2.5;
        public double TempEnd { get; set; } = 6.0;
        public double TempLow { get; set; } = 20.0;
        public double TempHigh { get; set; } = 26.0;

        public double RadStart { get; set; } = 2.0;
        public double RadEnd { get; set; } = 12.0;
        public double RadBaseLambda { get; set; } = 2.0;  // events/s baseline
        public double RadLeakLambda { get; set; } = 40.0; // events/s during leak

        // Generate environment signals on provided time vector
        public (double[] Tenv, double[] Penv, double[] Henv, double[] AcousticExc, double[] RadLambda) Generate(double[] time)
        {
            int n = time.Length;
            double[] Tenv = new double[n];
            double[] Penv = new double[n];
            double[] Henv = new double[n];
            double[] Acoustic = new double[n];
            double[] RadLambda = new double[n];

            for (int i = 0; i < n; i++)
            {
                double t = time[i];

                // Temperature: baseline TempLow, ramp up to TempHigh between TempStart..TempEnd
                if (t < TempStart) Tenv[i] = TempLow;
                else if (t >= TempEnd) Tenv[i] = TempHigh;
                else
                {
                    double frac = (t - TempStart) / (TempEnd - TempStart);
                    Tenv[i] = TempLow + frac * (TempHigh - TempLow);
                }

                // Pressure: step/spike between PressureSpikeStart..End, else 0 baseline
                if (t >= PressureSpikeStart && t <= PressureSpikeEnd)
                {
                    // shaped spike (smooth): use half-sine
                    double frac = (t - PressureSpikeStart) / (PressureSpikeEnd - PressureSpikeStart);
                    Penv[i] = PressureSpikeAmp * Math.Sin(Math.PI * frac);
                }
                else
                    Penv[i] = 0.0;

                // Humidity: start rising at HumidityStart toward HumidityHigh slowly
                if (t < HumidityStart) Henv[i] = HumidityLow;
                else if (t >= HumidityEnd) Henv[i] = HumidityHigh;
                else
                {
                    double frac = (t - HumidityStart) / (HumidityEnd - HumidityStart);
                    Henv[i] = HumidityLow + frac * (HumidityHigh - HumidityLow);
                }

                // Acoustic excitation: burst between AcousticStart..End, we model as amplitude * band-limited noise
                if (t >= AcousticStart && t <= AcousticEnd)
                    Acoustic[i] = AcousticAmp * Math.Sin(2.0 * Math.PI * 1500.0 * t) * Math.Exp(-5.0 * (t - AcousticStart)); // damped tone
                else
                    Acoustic[i] = 0.0;

                // Radiation rate (lambda): baseline to leak
                if (t < RadStart) RadLambda[i] = RadBaseLambda;
                else if (t >= RadEnd) RadLambda[i] = RadBaseLambda;
                else
                {
                    double frac = (t - RadStart) / (RadEnd - RadStart);
                    RadLambda[i] = RadBaseLambda + frac * (RadLeakLambda - RadBaseLambda);
                }
            }

            return (Tenv, Penv, Henv, Acoustic, RadLambda);
        }
    }
}
