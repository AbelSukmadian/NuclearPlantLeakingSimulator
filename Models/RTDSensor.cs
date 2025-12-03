using System;

namespace NuclearLeakSim_WinForms.Models
{
    public class RTDSensor : SensorBase
    {
        // Physical constants for PT100
        public double Alpha { get; set; } = 0.00385; // 1/°C
        public double R0 { get; set; } = 100.0;      // ohm at 0°C

        // HAPUS properti TauThermal dan Kgain
        // public double TauThermal { get; set; } = 5.0; 
        // public double Kgain { get; set; } = 1.0;      

        public RTDSensor()
        {
            Name = "PT100 RTD";
            SampleRate = 10;
            // Gunakan nilai default dari base class
            // Tau = TauThermal; // HAPUS
            // K = Kgain;      // HAPUS
        }

        public override double[] Generate(double[] time)
        {
            int n = time.Length;
            double[] Tenv = new double[n];
            for (int i = 0; i < n; i++)
                Tenv[i] = 20.0 + 5.0 * Math.Sin(2.0 * Math.PI * 0.01 * time[i]);

            double dt = (n > 1) ? (time[1] - time[0]) : 1.0;
            double[] Ts = new double[n];
            Ts[0] = Tenv[0];

            // PERBAIKAN: Gunakan 'this.Tau' (dari slider) bukan 'TauThermal'
            double tau = Math.Max(this.Tau, 1e-6); // Hindari pembagian nol
            for (int i = 1; i < n; i++)
            {
                Ts[i] = Ts[i - 1] + (dt / tau) * (Tenv[i - 1] - Ts[i - 1]);
            }

            double[] output = new double[n];
            for (int i = 0; i < n; i++)
            {
                double R = R0 * (1.0 + Alpha * (Ts[i] - 0.0));
                // PERBAIKAN: Gunakan 'this.K' (dari slider) bukan 'Kgain'
                output[i] = this.K * ((R - R0) / R0);
            }

            return output;
        }
    }
}