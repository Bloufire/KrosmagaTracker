using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddOn_Krosmaga___Blou_fire.Enums;

namespace AddOn_Krosmaga___Blou_fire.Data
{
	class Location
	{
		KrosmagaReader reader;

		private bool _hasLocType;
		private Enums.LocationType _locType;
		private bool _hasCell;
		private Data.CellCoord _cell;
		private bool _hasRowIndex;
		private int _rowIndex;
		private bool _hasColumnIndex;
		private int _columnIndex;

		public bool HasLocType
		{
			get { return _hasLocType; }

			set { _hasLocType = value; }
		}

		public LocationType LocType
		{
			get { return _locType; }

			set { _locType = value; }
		}

		public bool HasCell
		{
			get { return _hasCell; }

			set { _hasCell = value; }
		}

		internal CellCoord Cell
		{
			get { return _cell; }

			set { _cell = value; }
		}

		public bool HasRowIndex
		{
			get { return _hasRowIndex; }

			set { _hasRowIndex = value; }
		}

		public int RowIndex
		{
			get { return _rowIndex; }

			set { _rowIndex = value; }
		}

		public bool HasColumnIndex
		{
			get { return _hasColumnIndex; }

			set { _hasColumnIndex = value; }
		}

		public int ColumnIndex
		{
			get { return _columnIndex; }

			set { _columnIndex = value; }
		}

		public void Decode(byte[] array)
		{
			reader = new KrosmagaReader();
			reader.SetData(array);
			int tag;

			while (reader.B.BaseStream.Position < reader.B.BaseStream.Length && (tag = reader.ReadTag()) != 0)
			{
				if (tag <= 8)
				{
					if (tag == 8)
					{
						LocType = (Enums.LocationType) Enum.Parse(typeof(Enums.LocationType), reader.ReadRawVarint32().ToString());
						continue;
					}
				}
				else
				{
					if (tag == 18)
					{
						Data.CellCoord cellCoord = new Data.CellCoord();
						int size = (int) reader.ReadRawVarint32();
						cellCoord.Decode(reader.ReadMessage(size));
						Cell = cellCoord;
						continue;
					}
					if (tag == 24)
					{
						RowIndex = (int) reader.ReadRawVarint32();
						continue;
					}
					if (tag == 32)
					{
						ColumnIndex = (int) reader.ReadRawVarint32();
						continue;
					}
				}
			}
		}
	}
}