using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddOn_Krosmaga___Blou_fire.Data
{
	class ValueModification
	{
		KrosmagaReader reader;

		private bool _hasValueBefore;
		private int _valueBefore;
		private bool _hasModification;
		private int _modification;
		private bool _hasValueAfter;
		private int _valueAfter;

		public bool HasValueBefore
		{
			get { return _hasValueBefore; }

			set { _hasValueBefore = value; }
		}

		public int ValueBefore
		{
			get { return _valueBefore; }

			set { _valueBefore = value; }
		}

		public bool HasModification
		{
			get { return _hasModification; }

			set { _hasModification = value; }
		}

		public int Modification
		{
			get { return _modification; }

			set { _modification = value; }
		}

		public bool HasValueAfter
		{
			get { return _hasValueAfter; }

			set { _hasValueAfter = value; }
		}

		public int ValueAfter
		{
			get { return _valueAfter; }

			set { _valueAfter = value; }
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
						ValueBefore = (int) reader.ReadRawVarint32();
						continue;
					}
				}
				else
				{
					if (tag == 16)
					{
						Modification = (int) reader.ReadRawVarint32();
						continue;
					}
					if (tag == 24)
					{
						ValueAfter = (int) reader.ReadRawVarint32();
						continue;
					}
				}
			}
		}
	}
}