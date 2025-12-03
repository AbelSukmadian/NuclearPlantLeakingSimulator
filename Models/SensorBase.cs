using System;

namespace NuclearLeakSim_WinForms.Models
{
    /// <summary>
    /// Kelas dasar untuk semua sensor.
    /// Menyediakan parameter umum (Gain, Tau, SampleRate) dan kontrak untuk metode Generate().
    /// </summary>
    public abstract class SensorBase
    {
        // --- Properti umum ---
        public string Name { get; protected set; } = "Generic Sensor";

        /// <summary>
        /// Gain atau sensitivitas sensor (non-dimensional)
        /// </summary>
        public double K { get; set; } = 1.0;

        /// <summary>
        /// Konstanta waktu (detik) untuk respon eksponensial sensor.
        /// </summary>
        public double Tau { get; set; } = 0.5;

        /// <summary>
        /// Frekuensi sampling (Hz). Digunakan dalam domain waktu dan transformasi FFT.
        /// </summary>
        public int SampleRate { get; set; } = 1000;

        // --- Method virtual ---
        /// <summary>
        /// Menghasilkan sinyal keluaran sensor terhadap waktu.
        /// </summary>
        /// <param name="time">Array waktu (detik)</param>
        /// <returns>Array sinyal keluaran (unit sesuai sensor)</returns>
        public abstract double[] Generate(double[] time);

        // --- Validasi ringan ---
        protected void SanitizeParameters()
        {
            if (K <= 0 || double.IsNaN(K) || double.IsInfinity(K))
                K = 1.0;
            if (Tau <= 0 || double.IsNaN(Tau) || double.IsInfinity(Tau))
                Tau = 0.001;
            if (SampleRate <= 0)
                SampleRate = 100;
        }

        /// <summary>
        /// Helper umum yang memastikan output bebas NaN/Inf.
        /// </summary>
        protected double[] Clean(double[] data)
        {
            if (data == null) return Array.Empty<double>();
            for (int i = 0; i < data.Length; i++)
            {
                if (double.IsNaN(data[i]) || double.IsInfinity(data[i]))
                    data[i] = 0.0;
            }
            return data;
        }
    }
}
