#region License and Terms
// SuperLinq - Extensions to LINQ to Objects
// Copyright (c) 2017 Leandro F. Vieira (leandromoh). All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

namespace SuperLinq;

using System;
using System.Collections.Generic;

static partial class MoreEnumerable
{
	/// <summary>
	/// Returns a sequence generated by applying a state to the generator function,
	/// and from its result, determines if the sequence should have a next element, its value,
	/// and the next state in the recursive call.
	/// </summary>
	/// <typeparam name="TState">Type of state elements.</typeparam>
	/// <typeparam name="T">Type of the elements generated by the generator function.</typeparam>
	/// <typeparam name="TResult">The type of the elements of the result sequence.</typeparam>
	/// <param name="state">The initial state.</param>
	/// <param name="generator">
	/// Function that takes a state and computes the next state and the next element of the sequence.
	/// </param>
	/// <param name="predicate">
	/// Function to determine if the unfolding should continue based the
	/// result of the <paramref name="generator"/> function.
	/// </param>
	/// <param name="stateSelector">
	/// Function to select the state from the output of the <paramref name="generator"/> function.
	/// </param>
	/// <param name="resultSelector">
	/// Function to select the result from the output of the <paramref name="generator"/> function.
	/// </param>
	/// <returns>A sequence containing the results generated by the <paramref name="resultSelector"/> function.</returns>
	/// <remarks>
	/// This operator uses deferred execution and streams its results.
	/// </remarks>

	public static IEnumerable<TResult> Unfold<TState, T, TResult>(
		TState state,
		Func<TState, T> generator,
		Func<T, bool> predicate,
		Func<T, TState> stateSelector,
		Func<T, TResult> resultSelector)
	{
		if (generator == null) throw new ArgumentNullException(nameof(generator));
		if (predicate == null) throw new ArgumentNullException(nameof(predicate));
		if (stateSelector == null) throw new ArgumentNullException(nameof(stateSelector));
		if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

		return _(); IEnumerable<TResult> _()
		{
			while (true)
			{
				var step = generator(state);

				if (!predicate(step))
					yield break;

				yield return resultSelector(step);
				state = stateSelector(step);
			}
		}
	}
}
