using System;

namespace Task3
{
    public class Laptop : ComputerTechnics
    {
        private double diagonalInches;
        private double ramSizeGB;
        private double romSizeGB;
        private string cpuModel;
        public bool IsGaming { get; private set; }
        public bool IsHasKeyboardBacklight { get; private set; }
        public double DiagonalInches
        {
            get => diagonalInches;
            set
            {
                if (value < 10)
                {
                    throw new ArgumentException("Laptop diagonal can't be less than 10\"");
                }
                else
                {
                    diagonalInches = value;
                }
            }
        }
        public double RamSizeGB
        {
            get => ramSizeGB;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Size of RAM in laptop can't be less than 0 GB");
                }
                else
                {
                    ramSizeGB = value;
                }
            }
        }
        public double RomSizeGB
        {
            get => romSizeGB;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Size of ROM in laptop can't be less than 0 GB");
                }
                else
                {
                    romSizeGB = value;
                }
            }
        }
        public string CpuModel
        {
            get => cpuModel;
            set
            {
                if(string.IsNullOrEmpty(value))
                {
                    throw new Exception("Cpu model can't be empty");
                }
                else
                {
                    cpuModel = value;
                }
            }
        }

        public Laptop(string modelName, double price) : base(modelName, price) { }

        public Laptop(string modelName, double price, double diagonalInches, double ramGB, double romGB, string cpuModel) : base(modelName, price)
        {
            DiagonalInches = diagonalInches;
            RamSizeGB = ramGB;
            RomSizeGB = romGB;
            CpuModel = cpuModel;
        }

        public static Laptop operator +(Laptop l1, Laptop l2)
        {
            return new Laptop($"{l1.Name} - {l2.Name}", (l1.Price + l2.Price) / 2);
        }
    }
}
