
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management; // Add reference: System.Management
using System.Runtime.InteropServices;
using System.Threading;

public static class NyomtatasHelper
{
	/// <summary>
	/// Visszaadja az alapértelmezett nyomtató nevét (Win32 API / WMI nélkül).
	/// </summary>
	public static string GetDefaultPrinterName()
	{
		using (var pd = new System.Drawing.Printing.PrintDocument())
		{
			return pd.PrinterSettings.PrinterName;
		}
	}

	/// <summary>
	/// Lekéri az aktuális job ID-ket a megadott nyomtató spoolerébõl.
	/// </summary>
	public static HashSet<uint> GetExistingJobIds(string printerName)
	{
		var set = new HashSet<uint>();
		var query = new SelectQuery("Win32_PrintJob");
		using (var searcher = new ManagementObjectSearcher(query))
		{
			foreach (ManagementObject job in searcher.Get())
			{
				var name = (job["Name"] as string) ?? ""; // "PrinterName,JobID" formátum
				if (!name.StartsWith(printerName + ",", StringComparison.OrdinalIgnoreCase))
					continue;

				if (uint.TryParse(name.Split(',').LastOrDefault(), out uint jobId))
				{
					set.Add(jobId);
				}
			}
		}
		return set;
	}

	/// <summary>
	/// Megkeresi az elsõ új jobot az adott printeren azelõtt felvett ID-khez képest.
	/// </summary>
	public static uint? FindNewJobId(string printerName, HashSet<uint> oldIds, TimeSpan waitForAppearance, int pollMs = 300)
	{
		var start = DateTime.UtcNow;
		while (DateTime.UtcNow - start < waitForAppearance)
		{
			var nowIds = GetExistingJobIds(printerName);
			var newOnes = nowIds.Except(oldIds).ToList();
			if (newOnes.Count > 0)
				return newOnes[0];

			Thread.Sleep(pollMs);
		}
		return null;
	}

	/// <summary>
	/// Megvárja, hogy a megadott job ID a sorból eltûnjön / befejezõdjön.
	/// </summary>
	public static bool WaitForJobCompletion(string printerName, uint jobId, TimeSpan timeout, int pollMs = 500)
	{
		var start = DateTime.UtcNow;

		while (DateTime.UtcNow - start < timeout)
		{
			if (!TryGetJob(printerName, jobId, out var job))
			{
				// Nincs már a sorban -> vagy kinyomtatódott, vagy törölték
				return true;
			}

			try
			{
				// Állapotok kiolvashatók (nem mindig megbízható minden drivernél)
				var status = (job["Status"] as string) ?? "";
				var jobStatus = (job["JobStatus"] as string) ?? "";
				var isPrinted = (job["Printed"] as bool?) ?? false; // nem minden rendszer tölti
				var pagesPrinted = (job["PagesPrinted"] as uint?) ?? 0;
				var totalPages = (job["TotalPages"] as uint?) ?? 0;

				// Heurisztikák:
				// - ha Printed == true (ritka, de elõfordul)
				// - ha PagesPrinted == TotalPages és a job azonnal eltûnik utána
				// - ha a job eltûnik a következõ ciklusban
				if (isPrinted || (totalPages > 0 && pagesPrinted >= totalPages))
				{
					// még adunk egy rövid idõt, hátha eltûnik természetesen a sorból
					Thread.Sleep(500);
				}
			}
			finally
			{
				job?.Dispose();
			}

			Thread.Sleep(pollMs);
		}

		// Timeout
		return false;
	}

	private static bool TryGetJob(string printerName, uint jobId, out ManagementObject jobObj)
	{
		jobObj = null;
		var query = new SelectQuery("Win32_PrintJob");
		using (var searcher = new ManagementObjectSearcher(query))
		{
			foreach (ManagementObject job in searcher.Get())
			{
				var name = (job["Name"] as string) ?? "";
				if (name.Equals($"{printerName},{jobId}", StringComparison.OrdinalIgnoreCase))
				{
					jobObj = job;
					return true;
				}
				job.Dispose();
			}
		}
		return false;
	}
}
