using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnumerableCondition : IEnumerable
{
	private Func<bool> _condition;
	private Func<IEnumerable> _actionIfTrue;
	private Func<IEnumerable> _actionIfFalse;

	public EnumerableCondition(Func<bool> condition, Func<IEnumerable> actionIfTrue, Func<IEnumerable> actionIfFalse)
	{
		_condition = condition;
		_actionIfTrue = actionIfTrue;
		_actionIfFalse = actionIfFalse;
	}

	public IEnumerator GetEnumerator() => new EnumeratorCondition(_condition, _actionIfTrue, _actionIfFalse);
}

public class EnumeratorCondition : IEnumerator
{
	private Func<bool> _condition;
	private Func<IEnumerable> _actionIfTrue;
	private Func<IEnumerable> _actionIfFalse;

	private bool checkCondition;
	private bool _conditionValue;
	private IEnumerator _action;

	public EnumeratorCondition(Func<bool> condition, Func<IEnumerable> actionIfTrue, Func<IEnumerable> actionIfFalse)
	{
		_condition = condition;
		_actionIfTrue = actionIfTrue;
		_actionIfFalse = actionIfFalse;
	}

	public object Current => throw new NotImplementedException();

	public bool MoveNext()
	{
		if (_action == null && !checkCondition)
		{
			_conditionValue = _condition();
			_action = _conditionValue ? _actionIfTrue().GetEnumerator() : _actionIfFalse().GetEnumerator();
			checkCondition = true;
		}

		return _action != null && _action.MoveNext();
	}

	public void Reset()
	{

	}

	public override string ToString()
	{
		if (_action != null)
		{
			var action = EnumerableUtils.CleanIEnumeratorToString(_action.ToString());
			return $"Condition was {_conditionValue} then\n {action}";
		}
		else
			return "ERREUR EnumeratorCondition?";
	}
}
