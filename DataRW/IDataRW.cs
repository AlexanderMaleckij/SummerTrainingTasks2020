using FiguresProcessing;
using System;

namespace DataRW
{
    public interface IDataRW
    {
        ColorizedMaterialFigure[] Read();
        void Write(ColorizedMaterialFigure[] figures);
    }
}
