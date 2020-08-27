using Excel.Items;
using Excel.Properties;
using Excel.ContainersExceptions;
using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.Linq;

namespace Excel.Containers
{
    public class ExcelStackPanel : ExcelItem
    {
        private List<ExcelItem> items = new List<ExcelItem>();
        private int spaceBetween = 1;

        public List<ExcelItem> Items
        {
            get => items;
            set
            {
                if(value == null)
                {
                    throw new ExcelStackPanelException($"{nameof(Items)} can't be null");
                }

                items = value;
            }
        }
        public int SpaceBetween
        {
            get => spaceBetween;
            set
            {
                if (value < 0)
                {
                    throw new ExcelStackPanelException($"Space between excel items can't be less than 0");
                }

                spaceBetween = value;
            }
        }
        public new ExcelItemSize Size
        {
            get
            {
                switch(Orientation)
                {
                    case Orientation.Horizintal:
                        return new ExcelItemSize(
                            items.Select(x => x.Size.Width + spaceBetween).Sum() - SpaceBetween,
                            items.Select(x => x.Size.Height).Max());
                    case Orientation.Vertical:
                        return new ExcelItemSize(
                        items.Select(x => x.Size.Width).Max(),
                        items.Select(x => x.Size.Height + spaceBetween).Sum() - SpaceBetween);
                    default:
                        throw new ExcelStackPanelException($"{Orientation} Orientation is not supportable");
                }
            } 
        }
        
        public Orientation Orientation { get; set; }

        public ExcelStackPanel(List<ExcelItem> items, Orientation orientation = Orientation.Vertical) : this(orientation)
        {
            Items = items;
        }

        public ExcelStackPanel(Orientation orientation = Orientation.Vertical)
        {
            Orientation = orientation;
        }

        internal override void AddItem(Worksheet worksheet)
        {
            int x = Position.CellCoordNumberX;
            int y = Position.CellCoordNumberY;

            foreach(ExcelItem item in items)
            {
                item.Position.CellCoordNumberX = x;
                item.Position.CellCoordNumberY = y;

                switch (Orientation)
                {
                    case Orientation.Horizintal: x += item.Size.Width + spaceBetween; break;
                    case Orientation.Vertical: y += item.Size.Height + spaceBetween; break;
                    default:
                        throw new ExcelStackPanelException($"{Orientation} Orientation is not supportable");
                }

                item.AddItem(worksheet);
            }
        }
    }
}
