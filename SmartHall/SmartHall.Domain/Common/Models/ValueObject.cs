﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Domain.Common.Models
{
	public abstract class ValueObject : IEquatable<ValueObject>
	{
		public abstract IEnumerable<object> GetEqualityComponents();

		public bool Equals(ValueObject other)
		{
			return Equals(other as object);
		}

		public override bool Equals(object obj)
		{
			if (obj is null or not ValueObject)
			{
				return false;
			}

			var valueObject = obj as ValueObject;

			bool isEquantityComponentsEqual = GetEqualityComponents()
				.SequenceEqual(valueObject.GetEqualityComponents());

			return isEquantityComponentsEqual;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(GetEqualityComponents());
		}
	}
}
