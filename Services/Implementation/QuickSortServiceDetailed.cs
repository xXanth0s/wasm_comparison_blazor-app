using BlazorApp.Models;
using BlazorApp.Services.Interfaces;
using System;

namespace BlazorApp.Services.Implementation
{
    public class QuickSortServiceDetailed: IQuickSortService<SortableData>
	{
		public void sort(ref SortableData[] array, string key)
		{
			quicksort(0, array.Length - 1, ref array);
		}
		private void quicksort(int links, int rechts, ref SortableData[] daten)
		{
			if (links < rechts)
			{
				int teiler = teile(links, rechts, ref daten);
				quicksort(links, teiler - 1, ref daten);
				quicksort(teiler + 1, rechts, ref daten);
			}
		}
		private int teile(int links, int rechts, ref SortableData[] daten) 
		{
			int i = links;
			//Starte mit j links vom Pivotelement
			int j = rechts - 1;
			SortableData pivot = daten[rechts];
			var pivotValue = pivot.ID;
			

			do
			{
				//Suche von links ein Element, welches größer als das Pivotelement ist
				while (String.Compare(daten[i].ID, pivotValue) <= 0 && i < rechts)
				{
					i += 1;
				}

				//Suche von rechts ein Element, welches kleiner als das Pivotelement ist
				while (String.Compare(daten[j].ID, pivotValue) >= 0 && j > links) { 
					j -= 1;
				}

				if (i < j)
				{
					SortableData z = daten[i];
					daten[i] = daten[j];
					// tausche daten[i] mit daten[j]
					daten[j] = z;
				}

			} while (i < j);
			//solange i an j nicht vorbeigelaufen ist 

			// Tausche Pivotelement (daten[rechts]) mit neuer endgültiger Position (daten[i])

			if (String.Compare(daten[i].ID, pivotValue) > 0)
			{
				SortableData z = daten[i];
				daten[i] = daten[rechts];
				// tausche daten[i] mit daten[rechts]
				daten[rechts] = z;
			}
			return i;
		}
	}

	
}
