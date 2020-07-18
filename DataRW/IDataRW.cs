using FiguresProcessing;
using System;

namespace DataRW
{
    interface IDataRW
    {
        ColorizedMaterialFigure[] Read();
        void Write(ColorizedMaterialFigure[] figures);
    }
}
