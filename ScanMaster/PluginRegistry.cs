using System;
using System.Collections;

using ScanMaster.Acquire.Plugins;

namespace ScanMaster.Acquire.Plugin
{
	/// <summary>
	/// The plugin registry keeps track of the installed plugins. It's rather simple
	/// - it just keeps track of a table of plugins for each plugin type. It has some
	/// helper methods to get a plugin instance. When writing new plugins, they must
	/// be added to the constructor of this class by hand (simple but not to difficult).
	/// </summary>
	public class PluginRegistry
	{
		private static PluginRegistry registry;

		private Hashtable scanOutputPlugins = new Hashtable();
		private Hashtable switchOutputPlugins = new Hashtable();
		private Hashtable patternPlugins = new Hashtable();
		private Hashtable yagPlugins = new Hashtable();
		private Hashtable shotGathererPlugins = new Hashtable();
		private Hashtable analogInputPlugins = new Hashtable();

		private PluginRegistry()
		{
			// scanOutputPlugins
			scanOutputPlugins.Add("No scan", typeof(NullOutputPlugin));
			scanOutputPlugins.Add("Analog output", typeof(DAQMxAnalogOutputPlugin));
			scanOutputPlugins.Add("Synth frequency output", typeof(SynthFrequencyOutputPlugin));
			scanOutputPlugins.Add("Synth amplitude output", typeof(SynthAmplitudeOutputPlugin));
			scanOutputPlugins.Add("PG parameter scan", typeof(PGOutputPlugin));
			// switchOutputPlugins
			switchOutputPlugins.Add("No switch", typeof(NullSwitchPlugin));
			// patternPlugins
			patternPlugins.Add("No pattern", typeof(NullPGPlugin));
			patternPlugins.Add("Pulsed rf scan", typeof(PulsedRFScanPatternPlugin));
			patternPlugins.Add("Common raman", typeof(CommonRamanPatternPlugin));
			patternPlugins.Add("Pump-probe", typeof(PumpProbePatternPlugin));
			patternPlugins.Add("Deceleration", typeof(DecelerationPatternPlugin));
			// yagPlugins
			yagPlugins.Add("No YAG", typeof(NullYAGPlugin));
			yagPlugins.Add("YAG on", typeof(DefaultYAGPlugin));
			yagPlugins.Add("Not-so-Brilliant YAG", typeof(NotInTheLeastBitBrilliantYAGPlugin));
			// shotGathererPlugins
			shotGathererPlugins.Add("Constant, fake data", typeof(NullShotGathererPlugin));
			shotGathererPlugins.Add("Analog gatherer", typeof(AnalogShotGathererPlugin));
			shotGathererPlugins.Add("Modulated Analog gatherer", typeof(ModulatedAnalogShotGathererPlugin));
			shotGathererPlugins.Add("Buffered event counting gatherer", typeof(BufferedEventCountingShotGathererPlugin));
			// analog input plugins
			analogInputPlugins.Add("No analog input", typeof(NullAnalogInputPlugin));
			analogInputPlugins.Add("Analog input", typeof(DAQMxAnalogInputPlugin));
		}

		public static PluginRegistry GetRegistry()
		{
			if (registry == null) registry = new PluginRegistry();
			return registry;
		}

		public ScanOutputPlugin GetOutputPlugin(String type)
		{
			return (ScanOutputPlugin)InstantiatePlugin(scanOutputPlugins, type);
		}

		public String[] GetOutputPlugins()
		{
			return GetPluginNameList(scanOutputPlugins);
		}

		public SwitchOutputPlugin GetSwitchPlugin(String type)
		{
			return (SwitchOutputPlugin)InstantiatePlugin(switchOutputPlugins, type);
		}

		public String[] GetSwitchPlugins()
		{
			return GetPluginNameList(switchOutputPlugins);
		}

		public PatternPlugin GetPatternPlugin(String type)
		{
			return (PatternPlugin)InstantiatePlugin(patternPlugins, type);
		}

		public String[] GetPatternPlugins()
		{
			return GetPluginNameList(patternPlugins);
		}

		public YAGPlugin GetYAGPlugin(String type)
		{
			return (YAGPlugin)InstantiatePlugin(yagPlugins, type);
		}

		public String[] GetYAGPlugins()
		{
			return GetPluginNameList(yagPlugins);
		}

		public ShotGathererPlugin GetShotGathererPlugin(String type)
		{
			return (ShotGathererPlugin)InstantiatePlugin(shotGathererPlugins, type);
		}

		public String[] GetShotGathererPlugins()
		{
			return GetPluginNameList(shotGathererPlugins);
		}


		public AnalogInputPlugin GetAnalogPlugin(String type)
		{
			return (AnalogInputPlugin)InstantiatePlugin(analogInputPlugins, type);
		}
		
		public String[] GetAnalogPlugins()
		{
			return GetPluginNameList(analogInputPlugins);
		}

		private object InstantiatePlugin(Hashtable plugins, String type)
		{
			Type pluginType = (Type)plugins[type];
			System.Reflection.ConstructorInfo info = pluginType.GetConstructor(new Type[] {});
			return info.Invoke(new object[] {});
		}

		private String[] GetPluginNameList(Hashtable plugins)
		{
			String[] keys = new String[plugins.Count];
			plugins.Keys.CopyTo(keys,0);
			return keys;
		}
	}
}