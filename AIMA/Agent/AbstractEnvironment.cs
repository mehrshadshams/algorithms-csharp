using System;
using System.Collections.Generic;
using System.Linq;
using Optional;
using Optional.Unsafe;

namespace AIMA.Agent
{
    /**
 * @param <P> Type which is used to represent percepts
 * @param <A> Type which is used to represent actions
 * @author Ravi Mohan
 * @author Ciaran O'Reilly
 * @author Ruediger Lunde
 */
public abstract class AbstractEnvironment<P, A> : IEnvironment<P, A> {
    // Note: Use LinkedHashSet's in order to ensure order is respected.
	// get methods provide access to these elements via a List interface.
	protected readonly ISet<IAgent<P, A>> agents = new HashSet<IAgent<P, A>>();
	protected readonly ISet<IEnvironmentObject> envObjects = new HashSet<IEnvironmentObject>();

	// protected ISet<EnvironmentListener<? super P, ? super A>> listeners = new LinkedHashSet<>();
	protected readonly IDictionary<IAgent<P, A>, Double> performanceMeasures = new Dictionary<IAgent<P, A>, double>();

	// Return as a List but also ensure the caller cannot modify
	public IList<IAgent<P, A>> Agents => new List<IAgent<P, A>>(agents);

	public virtual void AddAgent(IAgent<P, A> agent) {
		agents.Add(agent);
		AddEnvironmentObject(agent);
		// notify(agent);
	}

	public virtual void RemoveAgent(IAgent<P, A> agent) {
		agents.Remove(agent);
		RemoveEnvironmentObject(agent);
	}

	// Return as a List but also ensure the caller cannot modify
	public IList<IEnvironmentObject> EnvironmentObjects => new List<IEnvironmentObject>(envObjects);

	public void AddEnvironmentObject(IEnvironmentObject eo) {
		envObjects.Add(eo);
	}

	
	public void RemoveEnvironmentObject(IEnvironmentObject eo) {
		envObjects.Remove(eo);
	}
	
	/**
	 * Move the Environment n time steps forward.
	 * 
	 * @param n
	 *            the number of time steps to move the Environment forward.
	 */ 
	public void Step(int n) {
		for (int i = 0; i < n; i++)
			Step();
	}
	
	/**
		 * Step through time steps until the Environment has no more tasks.
		 */ 
	public void StepUntilDone() {
		while (!Done)
		{
			Step();
		}
	}

	/**
	 * Central template method for controlling agent simulation. The concrete
	 * behavior is determined by the primitive operations
	 * {@link #getPerceptSeenBy(Agent)}, {@link #execute(Agent, Object)},
	 * and {@link #createExogenousChange()}.
	 */
	
	public void Step() {
		foreach (var agent in agents) {
			if (agent.Alive) {
				var percept = GetPerceptSeenBy(agent);
				Option<A> anAction = agent.Act(percept);
				if (anAction.HasValue) {
					Execute(agent, anAction.ValueOrDefault());
					// notify(agent, percept, anAction.get());
				} else {
					ExecuteNoOp(agent);
				}
			}
		}
		CreateExogenousChange();
	}

	/**
	 * Returns true if the current task was cancelled or no agent is alive anymore.
	 */
	
	public bool Done
	{
		get { return !agents.Any(a => a.Alive); }
	}

	//
	// Primitive operations to be implemented by subclasses:

	public abstract void Execute(IAgent<P, A> agent, A action);

	public abstract P GetPerceptSeenBy(IAgent<P, A> agent);

	/**
	 * Method for implementing dynamic environments in which not all changes are
	 * directly caused by agent action execution. The default implementation
	 * does nothing.
	 */
	protected void CreateExogenousChange() {
	}

	/**
	 * Method is called when an agent doesn't select an action when asked. Default implementation does nothing.
	 * Subclasses can for example modify the isDone status.
	 */
	protected void ExecuteNoOp(IAgent<P, A> agent) {
	}


	//
	// Other methods of environment interface:

	
	public double GetPerformanceMeasure(IAgent<P, A> agent) {
		if (!performanceMeasures.ContainsKey(agent))
		{
			performanceMeasures[agent] = 0.0;
		}

		return performanceMeasures[agent];
	}

	
	// public void addEnvironmentListener(EnvironmentListener<? super P, ? super A> listener) {
	// 	listeners.add(listener);
	// }
	//
	//
	// public void removeEnvironmentListener(EnvironmentListener listener) {
	// 	listeners.Remove(listener);
	// }

	
	// public void notify(String msg) {
	// 	listeners.forEach(listener -> listener.notify(msg));
	// }
	//
	// //
	// // Helper methods:
	//
	// protected void updatePerformanceMeasure(Agent<?, ?> forAgent, double addTo) {
	// 	performanceMeasures.put(forAgent, getPerformanceMeasure(forAgent) + addTo);
	// }
	//
	// protected void notify(Agent<?, ?> agent) {
	// 	listeners.forEach(listener -> listener.agentAdded(agent, this));
	// }
	//
	// protected void notify(Agent<?, ?> agent, P percept, A action) {
	// 	listeners.forEach(listener -> listener.agentActed(agent, percept, action, this));
	// }
}
}