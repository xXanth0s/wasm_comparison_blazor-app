using BlazorApp.Services.Interfaces;
using System;

namespace BlazorApp.Services.Implementation
{
    public class QuickSortService<T>: IQuickSortService<T>
	{
		public void sort(ref T[] array, string key)
		{
			quicksort(0, array.Length - 1, ref array, key);
		}
		private void quicksort(int links, int rechts, ref T[] daten, string key)
		{
			if (links < rechts)
			{
				int teiler = teile(links, rechts, ref daten, key);
				quicksort(links, teiler - 1, ref daten, key);
				quicksort(teiler + 1, rechts, ref daten, key);
			}
		}
		private int teile(int links, int rechts, ref T[] daten, string key) 
		{
			var type = typeof(T).GetProperty(key);
			int i = links;
			//Starte mit j links vom Pivotelement
			int j = rechts - 1;
			T pivot = daten[rechts];
			var pivotValue = (string)typeof(T).GetProperty(key).GetValue(pivot);
			

			do
			{
				var t = (string)type.GetValue(daten[i]);
				//Suche von links ein Element, welches größer als das Pivotelement ist
				while (String.Compare((string)type.GetValue(daten[i]), pivotValue) <= 0 && i < rechts)
				{
					i += 1;
				}

				//Suche von rechts ein Element, welches kleiner als das Pivotelement ist
				while (String.Compare((string)type.GetValue(daten[j]), pivotValue) >= 0 && j > links) { 
					j -= 1;
				}

				if (i < j)
				{
					T z = daten[i];
					daten[i] = daten[j];
					// tausche daten[i] mit daten[j]
					daten[j] = z;
				}

			} while (i < j);
			//solange i an j nicht vorbeigelaufen ist 

			// Tausche Pivotelement (daten[rechts]) mit neuer endgültiger Position (daten[i])

			if (String.Compare((string)type.GetValue(daten[i]), pivotValue) > 0)
			{
				T z = daten[i];
				daten[i] = daten[rechts];
				// tausche daten[i] mit daten[rechts]
				daten[rechts] = z;
			}
			return i;
		}
	}

	
}
