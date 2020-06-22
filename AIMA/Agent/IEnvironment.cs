using System;
using System.Collections.Generic;

namespace AIMA.Agent
{
	public interface IEnvironment<P, A>
	{
		/**
	 * Returns the Agents belonging to this Environment.
	 * 
	 * @return The Agents belonging to this Environment.
	 */
		IList<IAgent<P, A>> Agents { get; }

		/**
	 * Add an agent to the Environment.
	 * 
	 * @param agent
	 *            the agent to be added.
	 */
		void AddAgent(IAgent<P, A> agent);

		/**
	 * Remove an agent from the environment.
	 * 
	 * @param agent
	 *            the agent to be removed.
	 */
		void RemoveAgent(IAgent<P, A> agent);

		/**
	 * Returns the EnvironmentObjects that exist in this Environment.
	 * 
	 * @return the EnvironmentObjects that exist in this Environment.
	 */
		IList<IEnvironmentObject> EnvironmentObjects { get; }

		/**
	 * Add an EnvironmentObject to the Environment.
	 * 
	 * @param eo
	 *            the EnvironmentObject to be added.
	 */
		void AddEnvironmentObject(IEnvironmentObject eo);

		/**
	 * Remove an EnvironmentObject from the Environment.
	 * 
	 * @param eo
	 *            the EnvironmentObject to be removed.
	 */
		void RemoveEnvironmentObject(IEnvironmentObject eo);

		/**
	 * Move the Environment one time step forward.
	 */
		void Step();

		/**
	 * Returns <code>true</code> if the Environment is finished with its current
	 * task(s).
	 * 
	 * @return <code>true</code> if the Environment is finished with its current
	 *         task(s).
	 */
		bool Done { get; }

		/**
	 * Retrieve the performance measure associated with an Agent.
	 * 
	 * @param agent
	 *            the Agent for which a performance measure is to be retrieved.
	 * @return the performance measure associated with the Agent.
	 */
		double GetPerformanceMeasure(IAgent<P, A> agent);

		/**
		 * Add a listener which is notified about environment changes.
		 * 
		 * @param listener
		 *            the listener to be added.
		 */
		// void AddEnvironmentListener(EnvironmentListener<? super P, ? super A> listener);

		/**
		 * Remove a listener.
		 * 
		 * @param listener
		 *            the listener to be removed.
		 */
		// void removeEnvironmentListener(EnvironmentListener<? super P, ? super A> listener);

		/**
	 * Notify all environment listeners of a message.
	 * 
	 * @param msg
	 *            the message to notify the registered listeners with.
	 */
		// void notify(String msg);
	}
}