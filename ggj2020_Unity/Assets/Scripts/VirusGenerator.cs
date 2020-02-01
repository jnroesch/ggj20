using Game.Virus;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Game
{
	public class VirusGenerator
	{
		private System.Random _random;

		public VirusGenerator()
		{
			_random = new System.Random();
		}

		public VirusAction CreateRandomVirusAction()
		{
			return GetRandomVirus("Game.Virus");
		}

		private IEnumerable<Type> GetTypes(string nameSpace)
		{
			var types = from type in Assembly.GetExecutingAssembly().GetTypes()
						where type.IsClass && type.Namespace == nameSpace && !type.IsAbstract
						select type;
			return types;
		}

		private VirusAction GetRandomVirus(string nameSpace)
		{
			var possibleViruses = GetTypes(nameSpace).ToList();
			var index = _random.Next(possibleViruses.Count());

			return (VirusAction)Activator.CreateInstance(possibleViruses[index]);
		}
	}
}