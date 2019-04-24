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
	public class FuellesCollection : ConfigurationElementCollection
	{
		public FuellesCollection()
		{
			//BellowsConfigElement e = (BellowsConfigElement)CreateNewElement();
			//Add(e);
		}

		public override ConfigurationElementCollectionType CollectionType
		{
			get
				{
					return ConfigurationElementCollectionType.AddRemoveClearMap;
				}
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return new FuellesConfigElement();
		}

		protected override Object GetElementKey(ConfigurationElement element)
		{
			return ((FuellesConfigElement)element).Name;
		}

		public FuellesConfigElement this[int index]
		{
			get
			{
				return (FuellesConfigElement)BaseGet(index);
			}
			set
			{
				if (BaseGet(index) != null)
				{
					BaseRemoveAt(index);
				}
				BaseAdd(index, value);
			}
		}

		new public FuellesConfigElement this[string Name]
		{
			get
			{
				return (FuellesConfigElement)BaseGet(Name);
			}
		}

		public int IndexOf(FuellesConfigElement e)
		{
			return BaseIndexOf(e);
		}

		public void Add(FuellesConfigElement e)
		{
			BaseAdd(e);
		}
		protected override void BaseAdd(ConfigurationElement element)
		{
			BaseAdd(element, false);
		}

		public void Remove(FuellesConfigElement e)
		{
			if (BaseIndexOf(e) >= 0)
				BaseRemove(e.Name);
		}

		public void RemoveAt(int index)
		{
			BaseRemoveAt(index);
		}

		public void Remove(string name)
		{
			BaseRemove(name);
		}

		public void Clear()
		{
			BaseClear();
		}
	}
}
