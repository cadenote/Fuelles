// Fuelles is a forked work of Bellows Program obtained from www.franksworkshop.com Copyright (C) 2008, Frank Tkalcevic.
// Bellows - bellows fold pattern printer, based on US Patent No 6,054,194,
// Mathematically optimized family of ultra low distortion bellow fold patterns, Nathan R. Kane.

//Fuelles Copyright (C) 2019 , Jose Jimenez <cadenote@hotmail.com>
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Fuelles
{
	public sealed class FuellesConfigElement : ConfigurationSection
	{
		public FuellesConfigElement()
		{
		}

		public enum EBellowsShape
		{
			MediaCubierta=1,
			CajaCerrada=2,
            Abierta=3
		};

		[ConfigurationProperty("Name", DefaultValue = "Default", IsRequired = true, IsKey = true)]
		public string Name
		{
			get
			{
				return (string)this["Name"];
			}
			set
			{
				this["Name"] = value;
			}
		}

		[ConfigurationProperty("BellowsShape", DefaultValue = EBellowsShape.MediaCubierta)]
		//[IntegerValidator(MinValue = 1, MaxValue = 2)]
		public EBellowsShape BellowsShape
		{
			get
			{
				return (EBellowsShape)this["BellowsShape"];
			}
			set
			{
				this["BellowsShape"] = value;
			}
		}

		[ConfigurationProperty("Inversions", DefaultValue = 2)]
		//[IntegerValidator(MinValue = 1, MaxValue = 4)]
		public int Inversions
		{
			get
			{
				return (int)this["Inversions"];
			}
			set
			{
				this["Inversions"] = value;
			}
		}

		[ConfigurationProperty("Width", DefaultValue = 50.0)]
		public double Width
		{
			get
			{
				return (double)this["Width"];
			}
			set
			{
				this["Width"] = value;
			}
		}
		[ConfigurationProperty("Height", DefaultValue = 80.0)]
		public double Height
		{
			get
			{
				return (double)this["Height"];
			}
			set
			{
				this["Height"] = value;
			}
		}
		[ConfigurationProperty("Length", DefaultValue = 150.0)]
		public double Length
		{
			get
			{
				return (double)this["Length"];
			}
			set
			{
				this["Length"] = value;
			}
		}
		[ConfigurationProperty("FoldWidth", DefaultValue = 10.0)]
		public double FoldWidth
		{
			get
			{
				return (double)this["FoldWidth"];
			}
			set
			{
				this["FoldWidth"] = value;
			}
		}
		[ConfigurationProperty("MountFolds", DefaultValue = 4)]
		//[IntegerValidator(MinValue = 0, MaxValue = 10)]
		public int MountFolds
		{
			get
			{
				return (int)this["MountFolds"];
			}
			set
			{
				this["MountFolds"] = value;
			}
		}
		[ConfigurationProperty("AlternateFolds", DefaultValue = false)]
		public bool AlternateFolds
		{
			get
			{
				return (bool)this["AlternateFolds"];
			}
			set
			{
				this["AlternateFolds"] = value;
			}
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
