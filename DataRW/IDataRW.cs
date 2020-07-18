using ColorMaterial;

namespace DataRW
{
    public interface IDataRW
    {
        ColorizedMaterialFigure[] Read();
        void Write(ColorizedMaterialFigure[] figures);
    }
}
